using Forge.Entities;
using Forge.Networking.AutomaticTurnGame;
using Forge.Networking.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using XNA.GameSample;

namespace Forge.XNA {
    public class GameEngineManager {
        private IGameEngine _gameEngine;
        public NetworkContext _networkContext;
        public AutomaticTurnGame _turnGame;

        public IGameEngine Engine {
            get {
                return _gameEngine;
            }
        }

        public GameEngineManager(string snapshotJson, string templateJson, int targetUpdatesPerSecond) {
            // allocate the engine
            // TODO: examine the maybe, make sure the game actually loaded
            _gameEngine = GameEngineFactory.CreateEngine(snapshotJson, templateJson).Value;

            // create the event monitors
            CreateEventMonitors(_gameEngine.EventNotifier);

            _networkContext = NetworkContext.CreateServer(new Player("test-player"), "");
            _turnGame = new AutomaticTurnGame(_networkContext, targetUpdatesPerSecond);
        }

        public void SendCommand(IGameCommand command) {
            _turnGame.SendCommand(new List<IGameCommand>() { command });
        }

        public void Update(float elapsedMilliseconds) {
            // update network and the turn game
            _networkContext.Update();
            _turnGame.Update(elapsedMilliseconds);

            // try to update the game
            List<IGameCommand> commands;
            if (_turnGame.TryUpdate(out commands)) {
                _gameEngine.Update(commands.Cast<IGameInput>()).Wait();
                _gameEngine.SynchronizeState().Wait();
                _gameEngine.DispatchEvents();
            }
        }

        public void Draw(ExtendedSpriteBatch spriteBatch) {
            Visualizer.Instance.Draw(spriteBatch, _turnGame);
        }

        /// <summary>
        /// Discovers allocatable event monitors, allocates them, and then initializes them with the
        /// given event notifier.
        /// </summary>
        /// <param name="eventNotifier">The event notifier to initialize the monitors with</param>
        private static void CreateEventMonitors(IEventNotifier eventNotifier) {
            var monitors =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where typeof(IEventMonitor).IsAssignableFrom(type)
                where type.IsAbstract == false
                where type.IsInterface == false
                where type.IsClass == true
                where Attribute.IsDefined(type, typeof(EventMonitorAutomaticInstantiationAttribute))
                select (IEventMonitor)Activator.CreateInstance(type, /*nonPublic:*/ true);

            foreach (IEventMonitor monitor in monitors) {
                monitor.Initialize(eventNotifier);
            }
        }
    }
}