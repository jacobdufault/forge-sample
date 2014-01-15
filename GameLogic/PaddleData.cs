using Forge.Entities;
using Newtonsoft.Json;

namespace GameSample {
    /// <summary>
    /// Data type used to mark an object as a paddle.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PaddleData : Data.NonVersioned {
    }
}