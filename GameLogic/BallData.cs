using Forge.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GameSample {
    /// <summary>
    /// Contains information specific to a ball.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BallData : Data.NonVersioned {
        /// <summary>
        /// How many game ticks is collision disabled for?
        /// </summary>
        [JsonProperty]
        public int CollisionDisabled;
    }
}