using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace newbuy.Domain.Models;

[Table("products")]
public class Product
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ProductType? ProductType { get; set; }
    public Guid ProductTypeId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}