using Forge.Entities;
using Newtonsoft.Json;

namespace GameSample {
    /// <summary>
    /// An entity that contains TemporaryData will be removed after a certain number of game
    /// updates.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TemporaryData : Data.NonVersioned {
        /// <summary>
        /// The number of ticks that this object has left alive.
        /// </summary>
        [JsonProperty]
        public int TicksRemaining;
    }
}