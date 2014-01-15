using Forge.Entities;
using Forge.Utilities;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace GameSample {
    /// <summary>
    /// Data that contains movement information for an object. Because this data type extends
    /// ConcurrentVersioned, it can be modified multiple times per update.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MovementData : Data.ConcurrentVersioned<MovementData> {
        /// <summary>
        /// The velocity of the object. This can be modified by multiple threads at the same time,
        /// so we ensure that it is synchronized.
        /// </summary>
        [JsonProperty]
        public Vector2r Velocity {
            get;
            private set;
        }

        /// <summary>
        /// Multiples the velocity by the given factors.
        /// </summary>
        /// <param name="x">The x factor to multiply the velocity by.</param>
        /// <param name="z">The z factor to multiple the velocity by.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void MultVelocity(Real x, Real z) {
            Velocity = new Vector2r(Velocity.X * x, Velocity.Z * z);
        }

        public override void CopyFrom(MovementData source) {
            this.Velocity = source.Velocity;
        }

        /// <summary>
        /// If this data type needed to do some bookkeeping after each update (because modifications
        /// can happen concurrently), we can do it in this function. MovementData doesn't need any
        /// bookkeeping, so the function is empty.
        /// </summary>
        public override void ResolveConcurrentModifications() {
        }
    }
}