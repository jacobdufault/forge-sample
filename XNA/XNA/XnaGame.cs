using Forge.XNA;
using GameSample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Reflection;

namespace XNA.GameSample {
    public class XnaGame : Microsoft.Xna.Framework.Game {
        private ExtendedSpriteBatch _spriteBatch;
        private GameEngineManager _gameEngine;
        private GraphicsDeviceManager _graphics;

        public XnaGame() {
            Assembly.LoadFile(Path.GetFullPath("GameLogic.dll"));
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run. This
        /// is where it can query for any required services and load any non-graphic related
        /// content. Calling base.Initialize will enumerate through any components and initialize
        /// them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            // get the JSON that the engine will be initialized with
            string snapshotJson = File.ReadAllText("../../../../../Assets/snapshot.json");
            string templateJson = File.ReadAllText("../../../../../Assets/templates.json");

            _gameEngine = new GameEngineManager(snapshotJson, templateJson, targetUpdatesPerSecond: 15);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent() {
            _spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world, checking for collisions,
        /// gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D1)) {
                _gameEngine.SendCommand(new StartDestroyingInput());
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2)) {
                _gameEngine.SendCommand(new StopDestroyingInput());
            }

            // update with the elapsed seconds
            _gameEngine.Update(gameTime.ElapsedGameTime.Milliseconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _gameEngine.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}