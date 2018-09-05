using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

internal class GameObjectData
{
    public GameObjectData()
    {
    }

    public float[] Position { get; set; }
    public float[] Rotation { get; set; }
    public float[] Scale { get; set; }

    [JsonProperty("error")]
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyID Key { get; internal set; }
}