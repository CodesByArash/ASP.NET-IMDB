namespace api.Interfaces.IRepositories;

public interface IRateRepository:IRepository<Rate>
{
    public Task<List<Rate>> GetMediaRatesAsync(int mediaId);
}
