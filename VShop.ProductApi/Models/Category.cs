using Microsoft.VisualBasic;

namespace VShop.ProductApi.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }

    //Relation 1 : M
    public ICollection<Product>? Products { get; set; }
}
