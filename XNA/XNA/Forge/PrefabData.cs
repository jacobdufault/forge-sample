using Forge.Entities;
using Newtonsoft.Json;

// PrefabData must always be in namespace Forge to retain compatibility across rendering engines.
namespace Forge {
    /// <summary>
    /// Data that specifies that an IEntity should use a prefab as its base GameObject instead of an
    /// empty one.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PrefabData : Data.NonVersioned {
        /// <summary>
        /// The prefab to use for getting the base GameObject that will render the IEntity instance
        /// that this data instance is attached to.
        /// </summary>
        [JsonProperty("PrefabResourcePath")]
        public string PrefabResourcePath;
    }
}