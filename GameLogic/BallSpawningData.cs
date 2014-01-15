using Forge.Entities;
using Newtonsoft.Json;

namespace GameSample {
    [JsonObject(MemberSerialization.OptIn)]
    public class BallSpawningData : Data.NonVersioned {
        /// <summary>
        /// The maximum number of ticks before a spawn occurs.
        /// </summary>
        [JsonProperty]
        public int MaxTicks;

        /// <summary>
        /// The minimum number of ticks before a spawn occurs.
        /// </summary>
        [JsonProperty]
        public int MinTicks;

        /// <summary>
        /// The number of ticks remaining until the next spawn.
        /// </summary>
        [JsonProperty]
        public int Remaining;

        /// <summary>
        /// The ball template used to create new ball entities.
        /// </summary>
        [JsonProperty]
        public ITemplate Ball;
    }
}