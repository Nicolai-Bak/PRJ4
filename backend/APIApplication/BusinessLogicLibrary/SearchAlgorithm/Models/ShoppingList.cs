namespace ApiApplication.Controllers;

public class ShoppingList
{
    public List<ShoppingListItem> Products { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Range { get; set; }
}