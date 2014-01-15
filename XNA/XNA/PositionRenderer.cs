using Forge.Entities;
using Forge.Utilities;
using Forge.XNA;
using GameSample;
using Microsoft.Xna.Framework;

namespace XNA.GameSample {
    /// <summary>
    /// Renders entities which have position data
    /// </summary>
    [CustomDataRegistry(typeof(PositionData))]
    internal class PositionRenderer : DataRenderer {
        protected override void OnInitialize() {
        }

        protected override void OnDisposed() {
        }

        public override void Draw(ExtendedSpriteBatch spriteBatch, float percentage) {
            Bound prev = _entity.Previous<PositionData>().Position;
            Bound curr = _entity.Current<PositionData>().Position;

            // some magic constants that center the rendering in the window; the sample doesn't
            // bother to properly setup a coordinate system
            Real scale = 25;
            Real zoffset = 250;
            Real xoffset = 350;

            int x = (Interpolate(prev.X, curr.X, percentage) * scale + xoffset).AsInt;
            int z = (Interpolate(-prev.Z, -curr.Z, percentage) * scale + zoffset).AsInt;
            int radius = (Interpolate(prev.Radius, curr.Radius, percentage) * scale).AsInt;

            Rectangle rectangle = new Rectangle(x - radius, z - radius, 2 * radius, 2 * radius);
            spriteBatch.DrawRectangle(rectangle, Color.BlanchedAlmond);
        }
    }
}