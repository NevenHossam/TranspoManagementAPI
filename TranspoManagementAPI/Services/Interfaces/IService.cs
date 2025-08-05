namespace TranspoManagementAPI.Services.Interfaces
{
    public interface IService<TResponse, TEntity, TRequest>
        where TEntity : class, new()
        where TRequest : class
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse?> GetByIdAsync(int id);
        Task<TResponse> CreateAsync(TRequest request);
        Task<bool> UpdateAsync(int id, TRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
