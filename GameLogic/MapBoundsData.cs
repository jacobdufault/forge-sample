using Forge.Entities;
using Newtonsoft.Json;

namespace GameSample {
    /// <summary>
    /// Data that contains the dimensions of the map.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MapBoundsData : Data.NonVersioned {
        /// <summary>
        /// Width the map
        /// </summary>
        [JsonProperty]
        public int Width;

        /// <summary>
        /// Height of the map
        /// </summary>
        [JsonProperty]
        public int Height;
    }
}