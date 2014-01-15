using Forge.Entities;
using Forge.Utilities;
using Newtonsoft.Json;

namespace GameSample {
    /// <summary>
    /// Provides an Entity with a position and a radius. Because the data extends Versioned, we can
    /// query the previous state of the data.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PositionData : Data.Versioned<PositionData> {
        /// <summary>
        /// The position and radius of the object.
        /// </summary>
        [JsonProperty]
        public Bound Position;

        /// <summary>
        /// There is another PositionData instance that we should copy values from into this
        /// instance.
        /// </summary>
        public override void CopyFrom(PositionData source) {
            this.Position = source.Position;
        }
    }
}