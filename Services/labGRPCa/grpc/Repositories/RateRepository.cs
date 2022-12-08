using labGRPCa.Model;
using Microsoft.EntityFrameworkCore;

namespace labGRPCa.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly CurrencyContext _dbContext;

        public RateRepository(CurrencyContext currencyContext)
        {
            _dbContext = currencyContext;
        }
        public async Task CreateRate(CurrencyRate rate)
        {
            _dbContext.Rates.Add(rate);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteRate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CurrencyRate>> GerRates(RateFilter filter)
        {
            if (!await _dbContext.Rates.AnyAsync())
                return new List<CurrencyRate>();

            var ratesQuery = _dbContext.Rates.AsQueryable();
            if (filter.DateFrom == default)
                filter.DateFrom = await _dbContext.Rates.MinAsync(r => r.DateOfChange);

            if (filter.DateTo == default)
                filter.DateTo = await _dbContext.Rates.MaxAsync(r => r.DateOfChange);

            if (filter.PriceTo == default)
                filter.PriceTo = await _dbContext.Rates.MaxAsync(r => r.Price);

            if (filter.Currency != null)
                ratesQuery = ratesQuery.Where(r => r.Currency == filter.Currency);

            if (filter.Actions != null && filter.Actions.Count != 0)
                ratesQuery = ratesQuery.Where(r => filter.Actions.Contains(r.BackRefAction));

            if (filter.Limit == default)
                filter.Limit = await GetRatesCount();

            return await ratesQuery
                 .Where(r => r.DateOfChange >= filter.DateFrom.ToUniversalTime() && r.DateOfChange <= filter.DateTo.ToUniversalTime())
                 .Where(r => r.Price >= filter.PriceFrom && r.Price <= filter.PriceTo)
                 .Skip(filter.Offset)
                 .Take(filter.Limit)
                 .ToListAsync();
        }

        public Task<CurrencyRate> GetRateById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetRatesCount()
        {
            return await _dbContext.Rates.CountAsync();
        }
    }
}
