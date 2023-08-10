using System.Text.Json;
using Notes.Application.Interfaces.ApplicationServices;

namespace Notes.Application.Services;

public class SystemTextJsonSerializer : IJsonSerializer
{
    public string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}