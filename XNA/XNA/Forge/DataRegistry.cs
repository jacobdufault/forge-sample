using Forge.Collections;
using Forge.Entities;
using System;
using System.Collections.Generic;

namespace Forge.XNA {
    /// <summary>
    /// Specifies that the given type should be used as a custom data renderer.
    /// </summary>
    public class CustomDataRegistryAttribute : Attribute {
        /// <summary>
        /// The type of data that the annotated type renders.
        /// </summary>
        public Type DataType;

        public CustomDataRegistryAttribute(Type dataType) {
            DataType = dataType;
        }
    }

    public static class DataRegistry {
        /// <summary>
        /// Returns all types that have a CustomDataRegistryAttribute attribute.
        /// </summary>
        private static IEnumerable<Forge.Utilities.Tuple<Type, AttributeType>> GetTypesWithAttribute<AttributeType>()
            where AttributeType : Attribute {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (Type type in assembly.GetTypes()) {
                    object[] attributes = type.GetCustomAttributes(typeof(AttributeType), true);
                    if (attributes.Length > 0) {
                        yield return Forge.Utilities.Tuple.Create(type, (AttributeType)attributes[0]);
                    }
                    if (attributes.Length > 1) {
                        throw new InvalidOperationException("Too many satisfying attributes");
                    }
                }
            }
        }

        static DataRegistry() {
            foreach (var tuple in GetTypesWithAttribute<CustomDataRegistryAttribute>()) {
                DataAccessor dataAccessor = new DataAccessor(tuple.Item2.DataType);
                int id = dataAccessor.Id;

                Type type = tuple.Item1;
                if (type.IsSubclassOf(typeof(DataRenderer))) {
                    _renderers[id] = type;
                }
            }
        }

        /// <summary>
        /// Fast lookup from DataAccessor id to renderer type.
        /// </summary>
        private static SparseArray<Type> _renderers = new SparseArray<Type>();

        /// <summary>
        /// Attempts to create a new DataRenderer instance for the given type.
        /// </summary>
        public static bool TryGetRenderer(DataAccessor dataType, out DataRenderer renderer) {
            int id = dataType.Id;

            if (_renderers.ContainsKey(id)) {
                Type rendererType = _renderers[id];
                renderer = (DataRenderer)Activator.CreateInstance(rendererType);
                return true;
            }

            renderer = null;
            return false;
        }
    }
}