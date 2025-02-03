using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; } 
    public string SaleNumber { get; private set; }
    public DateTime SaleDate { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid BranchId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }
    public List<SaleItem> Items { get; private set; }

    protected Sale() { } 
    
    [JsonConstructor]
    public Sale(Guid id, string saleNumber, Guid customerId, Guid branchId, List<SaleItem> items)
    {
        Id = id;  // Keeps the ID received in the JSON (does not generate a new one)
        SaleNumber = saleNumber;
        SaleDate = DateTime.UtcNow;
        CustomerId = customerId;
        BranchId = branchId;
        Items = items ?? new List<SaleItem>();
        IsCancelled = false;
        CalculateTotal();
    }
    
    public Sale(string saleNumber, Guid customerId, Guid branchId, List<SaleItem> items)
    {
        Id = Guid.NewGuid(); // Generates a new ID only for new sales
        SaleNumber = saleNumber;
        SaleDate = DateTime.UtcNow;
        CustomerId = customerId;
        BranchId = branchId;
        Items = items ?? new List<SaleItem>();
        IsCancelled = false;
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        TotalAmount = Items.Sum(item => item.TotalAmount);
    }
}