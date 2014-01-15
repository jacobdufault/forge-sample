using Forge.Entities;
using Neon.Unity;
using UnityEngine;

namespace GameSample {
    /// <summary>
    /// Prepares the GameObject that contains the MapBoundsData so that the dimensions of the map
    /// are visible.
    /// </summary>
    [CustomDataRegistry(typeof(MapBoundsData))]
    public class MapBoundsRenderer : DataRenderer {
        protected override void OnInitialize() {
            MapBoundsData data = _entity.Current<MapBoundsData>();
            transform.localScale = new Vector3(data.Width * 2, 1, data.Height * 2);
            transform.position = new Vector3(0, -2, 0);
        }

        public override void UpdateVisualization(float percentage) {
        }
    }
}