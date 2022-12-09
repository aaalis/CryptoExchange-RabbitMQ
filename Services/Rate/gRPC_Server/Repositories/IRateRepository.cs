using gRPC_Server.Model;

namespace gRPC_Server.Repositories
{
    public interface IRateRepository
    {
        Task<IEnumerable<CurrencyRate>> GerRates(RateFilter filter);
        Task<CurrencyRate> GetRateById(int id);
        Task CreateRate(CurrencyRate rate);
        Task DeleteRate(int id);
        Task<int> GetRatesCount();
    }
}
