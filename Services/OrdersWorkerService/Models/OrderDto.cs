using System.Text.Json.Serialization;

namespace OrdersWorkerService.Models;

public class OrderDto
{
    public int Id { get; set; }
    public int Userid { get; set; }
    public int PortfolioId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderKind Kind { get; set; }
    public int? Count { get; set; }
    public decimal? Price { get; set; }
    public int? Basecurrencyid { get; set; }
    public int? Quotecurrencyid { get; set; }
    public bool Isdeleted { get; set; }
    

    public OrderDto(int id,
        int userid,
        int portfolioId,
        OrderKind kind,
        int? count,
        decimal? price,
        int? basecurrencyid,
        int? quotecurrencyid)
    {
        this.Id = id;
        this.Userid = userid;
        this.PortfolioId = portfolioId;
        this.Kind = kind;
        this.Count = count;
        this.Price = price;
        this.Basecurrencyid = basecurrencyid;
        this.Quotecurrencyid = quotecurrencyid;
    }

    public OrderDto(int userid,
        int portfolioId,
        OrderKind kind,
        int? count,
        decimal? price,
        int? basecurrencyid,
        int? quotecurrencyid)
    {
        this.Userid = userid;
        this.PortfolioId = portfolioId;
        this.Kind = kind;
        this.Count = count;
        this.Price = price;
        this.Basecurrencyid = basecurrencyid;
        this.Quotecurrencyid = quotecurrencyid;
    }

    public OrderDto()
    {

    }
}    