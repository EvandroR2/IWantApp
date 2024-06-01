using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace IWantApp.Domain.Produtcs;

public class Product : Entity
{
    public string Name { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }

    public string Description { get; set; } 

    public bool HasStock { get; set; }

    public bool Active { get; set; } = true;


    private Product()
    {
        // Este construtor vazio é necessário pelo Entity Framework
    }

    public Product(string name, Category category, string description, bool hasStock, string createBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        
        CreateBy = createBy;
        EditeBy = createBy;
        CreateOn = DateTime.Now;
        EditeOn = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 3, "Description")
            .IsNotNullOrEmpty(CreateBy, "CreateBy")
            .IsNotNullOrEmpty(EditeBy, "EditeBy");
        AddNotifications(contract);

    }

}


