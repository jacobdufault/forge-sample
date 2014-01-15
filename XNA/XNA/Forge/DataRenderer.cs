using Forge.Collections;
using Forge.Entities;
using Forge.Utilities;
using XNA.GameSample;

namespace Forge.XNA {
    /// <summary>
    /// Base MonoBehavior type that can be used to implement custom data renderers. DataRenderers
    /// need to be annotated with a CustomDataRegistry attribute.
    /// </summary>
    public abstract class DataRenderer : IVisualizable {
        protected IQueryableEntity _entity;

        public void Initialize(IQueryableEntity entity) {
            _entity = entity;
            Visualizer.Instance.Add(this);

            OnInitialize();
        }

        public void Dispose() {
            Visualizer.Instance.Remove(this);

            OnDisposed();
        }

        /// <summary>
        /// Called when the renderer has been initialized. The entity that this renderer will
        /// operate on is populated under _entity.
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// Called when the renderer has been destroyed.
        /// </summary>
        protected abstract void OnDisposed();

        /// <summary>
        /// Called when the renderer should draw itself.
        /// </summary>
        public abstract void Draw(ExtendedSpriteBatch spriteBatch, float percentage);

        private UnorderedListMetadata _visualizationMetadata = new UnorderedListMetadata();
        public UnorderedListMetadata VisualizationMetadata {
            get { return _visualizationMetadata; }
        }

        protected static Vector2r Interpolate(Vector2r start, Vector2r end, float percentage) {
            return new Vector2r(
                Interpolate(start.X, end.X, percentage),
                Interpolate(start.Z, end.Z, percentage));
        }

        protected static Real Interpolate(Real start, Real end, float percentage) {
            Real delta = end - start;
            return start + (delta * percentage);

            //return (start * (1 - percentage)) + (end * percentage);
        }
    }
}