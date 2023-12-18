using System.ComponentModel.DataAnnotations;

namespace SimpleResults.Example.DataAnnotations;

public class Order
{
    [JsonIgnore]
    public string Id { get; set; }
    [Required, MinLength(10)]
    public string Description { get; set; }
    [Required, MinLength(10)]
    public string DeliveryAddress { get; set; }
    [Required]
    public List<OrderDetail> Details { get; set; }
}

public class OrderDetail
{
    [Required, MinLength(8)]
    public string Product { get; set; }
    [Required]
    public int? Amount { get; set; }
    [Required]
    public double? Price { get; set; }
}
