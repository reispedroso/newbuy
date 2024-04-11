using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace newbuy.Domain.Models;

[Table("transactions")]
public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime PurchaseDate { get; set; }
}
