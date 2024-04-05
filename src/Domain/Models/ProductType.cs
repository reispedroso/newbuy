using System.ComponentModel.DataAnnotations.Schema;

namespace newbuy.Domain.Models;

[Table("product_type")]
public class ProductType
{
    public Guid Id { get; set; }
    public string? TypeName { get; set; }
}