using System;

namespace newbuy.Domain.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime PurchaseDate { get; set; }
}
