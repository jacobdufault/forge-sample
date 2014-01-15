using Forge.Collections;
using Forge.Entities;
using System;
using System.Collections.Generic;

namespace Forge.XNA {
    /// <summary>
    /// An IEventMonitor that manages the creation of EntityContainers and registering/removing data
    /// from those EntityContainers as the data state changes in entities.
    /// </summary>
    [EventMonitorAutomaticInstantiation]
    internal class EntityContainerEventMonitor : IEventMonitor {
        private SparseArray<SparseArray<DataRenderer>> _renderers = new SparseArray<SparseArray<DataRenderer>>();

        private SparseArray<DataRenderer> GetRenderers(IEntity entity) {
            SparseArray<DataRenderer> renderers;

            if (_renderers.TryGetValue(entity.UniqueId, out renderers) == false) {
                renderers = new SparseArray<DataRenderer>();
                _renderers[entity.UniqueId] = renderers;
            }

            return renderers;
        }

        private void OnDataAdded(AddedDataEvent addedData) {
            var accessor = new DataAccessor(addedData.AddedDataType);

            DataRenderer renderer;
            if (DataRegistry.TryGetRenderer(accessor, out renderer)) {
                SparseArray<DataRenderer> renderers = GetRenderers(addedData.Entity);
                renderer.Initialize(addedData.Entity);
                renderers[accessor.Id] = renderer;
            }
        }

        private void OnDataRemoved(RemovedDataEvent removedData) {
            SparseArray<DataRenderer> renderers;
            if (_renderers.TryGetValue(removedData.Entity.UniqueId, out renderers)) {
                var accessor = new DataAccessor(removedData.RemovedDataType);

                if (renderers.ContainsKey(accessor.Id)) {
                    DataRenderer renderer = renderers[accessor.Id];
                    renderers.Remove(accessor.Id);

                    renderer.Dispose();
                }
            }
        }

        private void OnEntityCreated(IEntity entity) {
            _renderers[entity.UniqueId] = new SparseArray<DataRenderer>();
        }

        private void OnEntityDestroyed(IEntity entity) {
            SparseArray<DataRenderer> renderers;
            if (_renderers.TryGetValue(entity.UniqueId, out renderers)) {

                foreach (KeyValuePair<int, DataRenderer> renderer in renderers) {
                    renderer.Value.Dispose();
                }
            }

            _renderers.Remove(entity.UniqueId);
        }

        public void Initialize(IEventNotifier notifier) {
            notifier.OnEvent<EntityAddedEvent>(evnt => {
                OnEntityCreated(evnt.Entity);
            });
            notifier.OnEvent<EntityRemovedEvent>(evnt => {
                OnEntityDestroyed(evnt.Entity);
            });
            notifier.OnEvent<AddedDataEvent>(OnDataAdded);
            notifier.OnEvent<RemovedDataEvent>(OnDataRemoved);
        }
    }
}