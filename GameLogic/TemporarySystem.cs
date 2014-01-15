using Forge.Entities;
using Forge.Networking.AutomaticTurnGame;
using Newtonsoft.Json;
using System;

namespace GameSample {
    /// <summary>
    /// Entities will no longer be destroyed by the temporary system.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class StopDestroyingInput : IGameInput, IGameCommand { }

    /// <summary>
    /// Entities will be destroyed by the temporary system.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class StartDestroyingInput : IGameInput, IGameCommand { }

    /// <summary>
    /// The temporary system removes entities who have expired according to their temporary data.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TemporarySystem : BaseSystem, Trigger.Update, Trigger.GlobalInput {
        [JsonProperty("Enabled")]
        private bool _enabled;

        public void OnUpdate(IEntity entity) {
            if (_enabled == false) {
                return;
            }

            if (entity.Current<TemporaryData>().TicksRemaining <= 0) {
                entity.Destroy();
            }

            entity.Modify<TemporaryData>().TicksRemaining--;
        }

        public Type[] RequiredDataTypes {
            get { return new[] { typeof(TemporaryData) }; }
        }

        public Type[] InputTypes {
            get { return new[] { typeof(StopDestroyingInput), typeof(StartDestroyingInput) }; }
        }

        public void OnGlobalInput(IGameInput input) {
            if (input is StopDestroyingInput) {
                _enabled = false;
            }

            else if (input is StartDestroyingInput) {
                _enabled = true;
            }
        }
    }
}