namespace Notes.Application.Interfaces.ApplicationServices;

public interface IJsonSerializer
{
    string Serialize(object obj);
    
    T? Deserialize<T>(string json);
}