
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Battleships.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum MoveResultEnum
{
    Water,Hit,Sunk
}