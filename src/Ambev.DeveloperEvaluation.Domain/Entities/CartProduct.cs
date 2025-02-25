using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class CartProduct
{
    private static int _cartProductIdCounter = 100; // Começando de um valor diferente para evitar conflitos
    
    public int Id { get; set; }  // ⚠️ Aqui está "Id", mas no JSON você enviou "productId"
    public int CartId { get; set; }
    public int ProductId { get; set; }  // O correto deveria ser este!
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; private set; } 
    public decimal TotalAmount { get; private set; } 

    protected CartProduct() { }

    public CartProduct(int cartId, int productId, int quantity, decimal unitPrice)
    {
        if (quantity > 20)
            throw new InvalidOperationException("It is not possible to sell more than 20 units of the same product.");

        CartId = cartId;
        ProductId = productId;
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