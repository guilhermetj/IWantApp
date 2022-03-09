﻿using Flunt.Validations;

namespace IWantApp.Domain.Products;


public class Product : Entity
{

    public string Name { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; }

    public decimal Price { get; private set; }


    private Product() { }


    public Product(string name,Category category, string description, bool hasStock, decimal price, string createBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        Price = price;
        Active = true;

        CreatedBy = createBy;
        EditedBy = createBy;
        CreatedOn = DateTime.Now;
        EditedOn  = DateTime.Now;

        Validate();
    }
    public void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Categoria não foi encontrada")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 3, "Description")
            .IsGreaterOrEqualsThan(Price, 1, "Price")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }
}
