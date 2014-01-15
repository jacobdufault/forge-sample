using Forge.Entities;
using Forge.Utilities;
using Newtonsoft.Json;
using System;

namespace GameSample {
    /// <summary>
    /// If a ball hits a paddle, then the ball reverses direction.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BallCollisionSystem : BaseSystem, Trigger.Update {
        /// <summary>
        /// The system that we get our paddles from.
        /// </summary>
        [JsonProperty]
        private PaddleCollectionSystem _paddles;

        public BallCollisionSystem(PaddleCollectionSystem paddles) {
            _paddles = paddles;
        }

        /// <summary>
        /// We want to ensure that our paddle system executes before us, as we are using data from
        /// it.
        /// </summary>
        protected override SystemExecutionOrdering GetExecutionOrdering(ISystem system) {
            if (system == _paddles) {
                return SystemExecutionOrdering.AfterOther;
            }

            return base.GetExecutionOrdering(system);
        }

        public void OnUpdate(IEntity ball) {
            // If we just modified the velocity of the ball, then don't modify it again until the
            // cooldown has finished.
            if (ball.Current<BallData>().CollisionDisabled > 0) {
                ball.Modify<BallData>().CollisionDisabled--;
                return;
            }

            foreach (IEntity paddle in _paddles.Paddles) {
                Bound paddlePosition = paddle.Current<PositionData>().Position;
                Bound ballPosition = ball.Current<PositionData>().Position;

                // change the direction of the ball if it's colliding with a paddle
                if (paddlePosition.Intersects(ballPosition)) {
                    ball.Modify<MovementData>().MultVelocity(-1, 1);
                    ball.Modify<BallData>().CollisionDisabled = 5;
                    break;
                }
            }
        }

        /// <summary>
        /// The ball collision system only cares about entities that have PongData, MovementData,
        /// and PositionData.
        /// </summary>
        public Type[] RequiredDataTypes {
            get {
                return new Type[] {
                    typeof(BallData), typeof(MovementData), typeof(PositionData)
                };
            }
        }
    }
}