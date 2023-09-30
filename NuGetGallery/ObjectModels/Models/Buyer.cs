namespace ObjectModels.Models;

public class Buyer
{
    public int Id { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }

    public Gender Gender { get; set; }

    public Guid CartId { get; set; }
    public required List<Order> Orders { get; set; }
}
