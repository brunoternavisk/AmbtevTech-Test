namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart
{
    public int Id { get; private set; }
    public int UserId { get; set; }
    public DateTime Date { get; private set; }
    public List<CartProduct> Products { get; set; }

    protected Cart() { }

    public Cart(int userId, List<CartProduct> products)
    {
        UserId = userId;
        Date = DateTime.UtcNow;
        Products = products ?? new List<CartProduct>();
    }

    // ✅ Novo construtor para atualização
    public Cart(int id, int userId, List<CartProduct> products)
    {
        Id = id;
        UserId = userId;
        Date = DateTime.UtcNow;
        Products = products ?? new List<CartProduct>();
    }

    public void Update(int userId, List<CartProduct> products)
    {
        UserId = userId;
        Products = products ?? new List<CartProduct>();
    }
}
