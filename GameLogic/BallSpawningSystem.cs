using Forge.Entities;
using Forge.Utilities;
using System;

namespace GameSample {
    /// <summary>
    /// Spawns balls every so often.
    /// </summary>
    public class BallSpawningSystem : BaseSystem, Trigger.GlobalPostUpdate {
        public void OnGlobalPostUpdate() {
            BallSpawningData spawningData = GlobalEntity.Current<BallSpawningData>();

            // can we spawn?
            if (spawningData.Remaining <= 0) {
                // create a new ball
                IEntity spawned = spawningData.Ball.Instantiate();

                // it isn't necessary to set the position of the spawned object to zero, but it
                // demos how to initialize a newly created entity
                PositionData spawnedPosition = spawned.AddOrModify<PositionData>();
                spawnedPosition.Position = new Bound(0, 0, spawnedPosition.Position.Radius);

                // reset the ticks until the next spawn
                GlobalEntity.Modify<BallSpawningData>().Remaining =
                    GameRandom.Range(spawningData.MinTicks, spawningData.MaxTicks);
            }

            // can't spawn yet; decrement the number of ticks until we can spawn again
            else {
                GlobalEntity.Modify<BallSpawningData>().Remaining--;
            }
        }
    }
}