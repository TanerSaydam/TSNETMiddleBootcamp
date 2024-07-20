namespace TodoCleanArchitecture.Application.Services;
public interface ICacheService
{
    void Set<T>(string key, T value);
    void Remove(string key);
    void TryGetValue<T>(string key, out T? value);
}
