using Forge.Collections;
using Forge.Networking.AutomaticTurnGame;
using XNA.GameSample;

namespace Forge.XNA {
    public class Visualizer {
        public static Visualizer Instance = new Visualizer();

        /// <summary>
        /// Should the visualizer interpolate between frames?
        /// </summary>
        public bool Interpolate = true;

        /// <summary>
        /// The list of items that need to be rendered (or rather, have their rendering states
        /// updated) .
        /// </summary>
        protected UnorderedList<IVisualizable> _visualizable = new UnorderedList<IVisualizable>();

        /// <summary>
        /// Add the given item to the list of items which will receive visualization events.
        /// </summary>
        /// <param name="visualizable">The visualized item.</param>
        public void Add(IVisualizable visualizable) {
            _visualizable.Add(visualizable, visualizable.VisualizationMetadata);
        }

        /// <summary>
        /// Removes the given item from the list of items that receive visualization events.
        /// </summary>
        /// <param name="visualizable">The visualized item.</param>
        public void Remove(IVisualizable visualizable) {
            _visualizable.Remove(visualizable, visualizable.VisualizationMetadata);
        }

        /// <summary>
        /// Draws every visualizable element.
        /// </summary>
        public void Draw(ExtendedSpriteBatch spriteBatch, AutomaticTurnGame game) {
            float interpolate = 1.0f;
            if (Interpolate) {
                interpolate = game.InterpolationPercentage;
            }

            foreach (IVisualizable visualizable in _visualizable) {
                visualizable.Draw(spriteBatch, interpolate);
            }
        }
    }

    /// <summary>
    /// Interface for objects which should visualize themselves from the entity system.
    /// </summary>
    public interface IVisualizable {
        /// <summary>
        /// Used by the visualizer for fast removes.
        /// </summary>
        UnorderedListMetadata VisualizationMetadata {
            get;
        }

        /// <summary>
        /// Draw the object.
        /// </summary>
        /// <param name="percentage">How much to interpolate from the previous state to the current
        /// state (a value between [0, 1]).</param>
        void Draw(ExtendedSpriteBatch spriteBatch, float percentage);
    }
}