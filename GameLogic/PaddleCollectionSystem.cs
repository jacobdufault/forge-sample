using Forge.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GameSample {
    /// <summary>
    /// A system that merely collects a list of all paddles in the game. This may be unnecessary in
    /// a future version of Forge, but for now it works well as a sample for some more of the
    /// triggers.
    /// </summary>
    /// <remarks>
    /// We serialize the object as a reference so that the BallCollisionSystem is properly restored.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class PaddleCollectionSystem : BaseSystem, Trigger.Added, Trigger.Removed {
        /// <summary>
        /// Every paddle in the game.
        /// </summary>
        [JsonProperty]
        public List<IEntity> Paddles = new List<IEntity>();

        public void OnAdded(IEntity entity) {
            Paddles.Add(entity);
        }

        public void OnRemoved(IEntity entity) {
            Paddles.Remove(entity);
        }

        public Type[] RequiredDataTypes {
            get { return new[] { typeof(PositionData), typeof(PaddleData) }; }
        }
    }
}