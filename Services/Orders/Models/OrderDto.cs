namespace Orders.Models;

public class OrderDto : Order
{
    public int PortfolioId { get; set; }

    public OrderDto(Order order, int portfolioId)
    {
        Id = order.Id;
        Kind = order.Kind;
        Count = order.Count;
        Basecurrencyid = order.Basecurrencyid;
        Quotecurrencyid = order.Quotecurrencyid;
        Price = order.Price;
        Userid = order.Userid;
        Isdeleted = order.Isdeleted;
        PortfolioId = portfolioId;
    }

    public OrderDto(int id, int userid, OrderKind kind, int? count, decimal? price, int? basecurrencyid, int? quotecurrencyid) 
        : base(id, userid, kind, count, price, basecurrencyid, quotecurrencyid)
    {
    }

    public OrderDto(int userid, OrderKind kind, int? count, decimal? price, int? basecurrencyid, int? quotecurrencyid) 
        : base(userid, kind, count, price, basecurrencyid, quotecurrencyid)
    {
    }

    public OrderDto(int id,
                    OrderKind kind,
                    int count,
                    decimal price,
                    int basecurrencyid,
                    int quotocurrencyid,
                    bool isDeleted,
                    int portfolioId)
    {
        Id = id;
        Kind = kind;
        Count = count;
        Price = price;
        Basecurrencyid = basecurrencyid;
        Quotecurrencyid = quotocurrencyid;
        Isdeleted = isDeleted;
        PortfolioId = portfolioId;
    }

    public OrderDto()
    {
        
    }
}