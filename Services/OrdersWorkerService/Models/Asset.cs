namespace OrdersWorkerService.Models;

public class Asset
{
    public int id { get; set; }
    public int portfolioId { get; set; }
    public int? amount { get; set; }
    public int? currencyId { get; set; }

    public Asset(int id, int portfolioId, int amount, int currencyId)
    {
        this.id = id;
        this.portfolioId = portfolioId;
        this.amount = amount;
        this.currencyId = currencyId;
    }

    public Asset(OrderDto orderDto)
    {
        this.id = orderDto.Id;
        this.portfolioId = orderDto.PortfolioId;
        this.amount = orderDto.Count;
        this.currencyId = orderDto.Quotecurrencyid;
    }
}