using System.Text.Json;

namespace Frutti.Server.Domain.Net;

public class PacketEnvelope
{
    public string Type { get; set; }

    public JsonElement Data { get; set; }
}
