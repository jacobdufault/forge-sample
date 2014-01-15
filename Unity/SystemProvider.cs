using Forge.Entities;
using Neon.Unity;
using System.Collections.Generic;

namespace GameSample {
    /// <summary>
    /// Provides the systems that should be used when generating the level.
    /// </summary>
    public class SystemProvider : ISystemProvider {
        public IEnumerable<ISystem> GetSystems() {
            PaddleCollectionSystem paddles = new PaddleCollectionSystem();
            yield return paddles;
            yield return new BallCollisionSystem(paddles);
            yield return new MovementSystem();
            yield return new BallSpawningSystem();
            yield return new TemporarySystem();
        }
    }
}