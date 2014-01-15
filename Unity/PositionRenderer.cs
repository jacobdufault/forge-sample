using Forge.Entities;
using Forge.Utilities;
using Neon.Unity;
using UnityEngine;

namespace GameSample {
    /// <summary>
    /// Performs interpolation and rendering for PositionData.
    /// </summary>
    [CustomDataRegistry(typeof(PositionData))]
    public class PositionRenderer : DataRenderer {
        protected override void OnInitialize() {
            PositionData data = _entity.Current<PositionData>();

            float radius = data.Position.Radius.AsFloat * 2;
            transform.localScale = new Vector3(radius, radius, radius);
        }

        public override void UpdateVisualization(float percentage) {
            Vector2r previous = ToVec(_entity.Previous<PositionData>().Position);
            Vector2r current = ToVec(_entity.Current<PositionData>().Position);

            float interpolatedX = Interpolate(previous.X.AsFloat, current.X.AsFloat, percentage);
            float interpolatedZ = Interpolate(previous.Z.AsFloat, current.Z.AsFloat, percentage);

            transform.position = new Vector3(interpolatedX, transform.position.y, interpolatedZ);
        }

        private static Vector2r ToVec(Bound bound) {
            return new Vector2r(bound.X, bound.Z);
        }
    }
}