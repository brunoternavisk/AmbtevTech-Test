namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public Guid Id { get; private set; }
    public Guid SaleId { get; private set; } // Relationship with Sale
    public Guid ProductId { get; private set; } // Product sold
    public string ProductDescription { get; private set; } // Product name
    public int Quantity { get; private set; } // Quantity purchased
    public decimal UnitPrice { get; private set; } // Unit price
    public decimal Discount { get; private set; } // Discount amount applied
    public decimal TotalAmount { get; private set; } // Item Total (after discount)

    protected SaleItem() { }

    public SaleItem(Guid productId, string productDescription, int quantity, decimal unitPrice)
    {
        if (quantity > 20)
            throw new InvalidOperationException("It is not possible to sell more than 20 units of the same product.");

        Id = Guid.NewGuid();
        ProductId = productId;
        ProductDescription = productDescription;
        Quantity = quantity;
        UnitPrice = unitPrice;

        Discount = CalculateDiscount();
        TotalAmount = (UnitPrice * Quantity) - Discount;
    }

    private decimal CalculateDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
            return (UnitPrice * Quantity) * 0.20m;
        if (Quantity >= 4)
            return (UnitPrice * Quantity) * 0.10m;

        return 0m;
    }
}