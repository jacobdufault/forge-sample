using Forge.Entities;
using Forge.Utilities;
using Newtonsoft.Json;
using System;

namespace GameSample {
    /// <summary>
    /// The movement system ensures that objects do not leave the map and also moves them according
    /// to their velocity.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class MovementSystem : BaseSystem, Trigger.Update {
        /// <summary>
        /// Every update, we want to ensure that objects are inside the map and we also want to move
        /// the object according to its velocity.
        /// </summary>
        public void OnUpdate(IEntity entity) {
            MapBoundsData mapBounds = GlobalEntity.Current<MapBoundsData>();

            RespondToMapBounds(mapBounds, entity);
            IntegratePosition(entity);
        }

        /// <summary>
        /// Moves an object to its next position according to its current velocity.
        /// </summary>
        private static void IntegratePosition(IEntity entity) {
            PositionData pos = entity.Current<PositionData>();
            MovementData mov = entity.Current<MovementData>();

            entity.Modify<PositionData>().Position = new Bound(
                pos.Position.X + mov.Velocity.X,
                pos.Position.Z + mov.Velocity.Z,
                pos.Position.Radius
            );
        }

        /// <summary>
        /// Changes an objects velocity if it has left the map.
        /// </summary>
        private static void RespondToMapBounds(MapBoundsData mapBounds, IEntity entity) {
            PositionData pos = entity.Current<PositionData>();
            MovementData mov = entity.Current<MovementData>();

            if (((pos.Position.Z - pos.Position.Radius) < -mapBounds.Height && mov.Velocity.Z < 0) ||
                ((pos.Position.Z + pos.Position.Radius) > mapBounds.Height && mov.Velocity.Z > 0)) {
                entity.Modify<MovementData>().MultVelocity(1, -1);
            }
            if (((pos.Position.X - pos.Position.Radius) < -mapBounds.Width && mov.Velocity.X < 0) ||
                ((pos.Position.X + pos.Position.Radius) > mapBounds.Width && mov.Velocity.X > 0)) {
                entity.Modify<MovementData>().MultVelocity(-1, 1);
            }
        }

        /// <summary>
        /// We only want entities that have both MovementData and PositionData.
        /// </summary>
        public Type[] RequiredDataTypes {
            get {
                return new[] {
                    typeof(MovementData), typeof(PositionData)
                };
            }
        }
    }
}