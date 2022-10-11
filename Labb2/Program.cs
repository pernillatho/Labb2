using Labb2;
using System.Xml.Linq;

Customer? loggedInCustomer = null;

List<Customer> _customers = new List<Customer>();
_customers.Add(new Customer("Knatte", "123"));
_customers.Add(new Customer("Fnatte", "321"));
_customers.Add(new Customer("Tjatte", "312"));

List<Product> products = new List<Product>();
products.Add(new Product() { Id = 1, Name = "Olives", Price = 12.5 });
products.Add(new Product() { Id = 2, Name = "Artichoke", Price = 20.5 });
products.Add(new Product() { Id = 3, Name = "Sun-dried tomatoes", Price = 29.5 });
products.Add(new Product() { Id = 4, Name = "Truffle", Price = 29.5 });
products.Add(new Product() { Id = 5, Name = "Pistachios", Price = 29.5 });

bool runProgram = true;
bool continuee = true;

while (runProgram)
{
    Console.Clear();
    Console.WriteLine("---Main menu---");
    Console.WriteLine("[1] - Sign in");
    Console.WriteLine("[2] - Register new customer");
    Console.WriteLine("[3] - Exit program");
    var inputMenuOne = Console.ReadLine();

    switch (inputMenuOne)
    {
        case "1":
            {
                AskForUserNameAndPassword();

                if (loggedInCustomer != null)
                {
                    while (continuee)
                    {
                        Console.WriteLine($"---Logged in---");
                        Console.WriteLine("[1] - Add to cart");
                        Console.WriteLine("[2] - View Cart");
                        Console.WriteLine("[3] - Checkout - pay");
                        Console.WriteLine("[4] - Log out");
                        var inputMenuTwo = Console.ReadLine();

                        switch (inputMenuTwo)
                        {
                            case "1":
                                AddItems();
                                break;
                            case "2":
                                GoToCart();
                                break;
                            case "3":
                                Checkout();
                                break;
                            case "4":
                                continuee = false;
                                break;
                            default:
                                Console.WriteLine("You can only choose a number between 1-4. Try again!");
                                Thread.Sleep(1500);
                                break;
                        }
                    }
                }
                break;
            }
        case "2":
            AddNewCustomer();
            break;
        case "3":
            if (inputMenuOne == "3")
                runProgram = false;
            break;
        default:
        {
            Console.Clear();
            Console.WriteLine("You can only choose a number between 1-3. Try again!");
            Thread.Sleep(1500);
            break;
        }
    }

    void AskForUserNameAndPassword()
    {
        Console.Clear();
        Console.WriteLine("---Sign in---");
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        loggedInCustomer = _customers.FirstOrDefault(p => p.Name.Equals(name));

        if (loggedInCustomer != null)
        {
            var wrongPassword = true;
            while (wrongPassword)
            {
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                if (!loggedInCustomer.VerifyPassword(password))
                {
                    Console.WriteLine("Wrong password, try again!");
                    Thread.Sleep(1000);
                }
                else
                {
                    wrongPassword = false;
                }
            }

        }
        else if (!name.Equals(String.Empty))
        {
            Console.Clear();
            Console.WriteLine("The customer name does not exist!\n");
            Console.WriteLine("[1] - Register new Customer ");
            Console.WriteLine("[2] - Back to Main menu ");
            var inputName = Console.ReadLine();

            if (inputName != "1") return;
            AddNewCustomer();
        }
        else
        {
            Console.WriteLine("Try again, your name have to contain letters or numbers.");
            Thread.Sleep(2000);
        }
    }
    continuee = true;
    Console.Clear();
}

void AddNewCustomer()
{
    Console.Clear();
    Console.WriteLine("---Register new customer---");
    Console.Write("Enter name: ");
    string newName = Console.ReadLine();

    if (newName != string.Empty)
    {
        Console.Write("Enter password: ");
        string newPassword = Console.ReadLine();
        
        if (newPassword == string.Empty)
        {
            Console.WriteLine("Try again, your password have to contain letters or numbers.");
            Thread.Sleep(1800);
            AddNewCustomer();
        }
        else
        {
            _customers.Add(new Customer(newName, newPassword));
            Console.WriteLine("You are registered, press enter to log in.");
            Console.ReadKey();
        }

    }
    else if (newName.Equals(String.Empty))
    {
        Console.Clear();
        Console.WriteLine("Try again, your name have to contain letters or numbers.\n");
        Thread.Sleep(2000);
    }
}

void AddItems()
{ 
    bool wantedItem = false;
    int inputItem = 0;
    Console.WriteLine("---Add items---");
    products.ForEach(item => Console.WriteLine(item));
    
    while (!wantedItem)
    {
        Console.Write("Choose which item: ");
        wantedItem = int.TryParse(Console.ReadLine(), out inputItem);
        if (!products.Any(p => p.Id == inputItem))
        {
            Console.WriteLine("Wrong ID, try again.");
            Thread.Sleep(1000);
            AddItems();
        }
        else
        {
            wantedItem = false;

        }

        int inputHowMany = 0;
        while (!wantedItem)
        {
            Console.Write("Choose how many: ");
            wantedItem = int.TryParse(Console.ReadLine(), out inputHowMany);
            if (!wantedItem)
                Console.WriteLine("Enter an Integer!");
        }

        foreach (var item in products.Where(item => item.Id == inputItem))
        {

            for (int i = 0; i < inputHowMany; i++)
            {
                loggedInCustomer.Cart.Add(item);
            }

            Console.WriteLine();
        }
    }


    //Console.WriteLine("---Add items---");
    //products.ForEach(item => Console.WriteLine(item));
    //Console.WriteLine();

    //Console.Write("Choose which item: ");
    //var inputItem = int.Parse(Console.ReadLine());
    //Console.Write("Choose how many: ");

    //var inputHowMany = int.Parse(Console.ReadLine());

    //foreach (var item in products.Where(item => item.Id == inputItem))
    //{

    //    for (int i = 0; i < inputHowMany; i++)
    //    {
    //        loggedInCustomer.Cart.Add(item);
    //    }

    //    Console.WriteLine();
    //}


}

void GoToCart()
{
    Console.WriteLine("---View Cart---");

    var productType = loggedInCustomer.Cart.Select(p => p).Distinct().ToList();

    foreach (var item in productType)
    {
        var number = loggedInCustomer.Cart.Count(p => p == item);
        var productPrice = number * item.Price;

        Console.WriteLine($"{item} SEK/st. Quantity: {number}. Total price: {productPrice} SEK.");
    }

    Console.WriteLine();
    loggedInCustomer.TotalPrice();
}

void Checkout ()
{
    Console.WriteLine("---Check out---");
    loggedInCustomer.TotalPrice();
    Console.WriteLine("[1] - Pay\n[2] - Menu");

    var inputCheckOut = Console.ReadLine();

    if (inputCheckOut == "1")
    {
        Console.Clear();
        Console.WriteLine("Payed");
        Environment.Exit(0);
    }
    Console.Clear();
}