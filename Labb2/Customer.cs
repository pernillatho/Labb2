namespace Labb2;

public class Customer
{
    public string Name { get; private set; }
    private string Password { get; set; }


    private List<Product> _cart;
    public List<Product> Cart { get { return _cart; } }

    public Customer(string name, string password)
    {
        Name = name;
        Password = password;
        _cart = new List<Product>();
    }


    public bool VerifyPassword(string inputPassword)
    {

        if (Password == inputPassword)
        {
            return true;
        }

        return false;
    }


    public void TotalPrice()
    {
        double totalPrice = 0;
        foreach (var item in Cart)
        {
            totalPrice += item.Price;
        }

        Console.WriteLine("Total price for your shopping cart: " + totalPrice + " SEK.");
        Console.WriteLine();

    }
    public override string ToString()
    {
        return $"{Name}\n{Password}\n{Cart}";
    }
}