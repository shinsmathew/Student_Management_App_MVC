namespace Student_Management_App_MVC.Services.Interfaces
{
    public interface ICacheService
    {
            Task<T> GetRedisCacheAsync<T>(string key);
            Task SetRedisCacheAsync<T>(string key, T value, TimeSpan timeToLive);
            Task RemoveRedisCacheAsync(string key);
        
    }
}
