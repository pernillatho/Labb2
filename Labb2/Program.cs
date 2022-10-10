//Skapa en konsollaplikation som skall agera som en enkel affär.
//När programmet  så ska man få se någon form av meny där man ska kunna välja
//att registrera ny kund eller logga in. Därefter ska ytterligare en meny visas där
//man ska kunna välja att handla, se kundvagn eller gå till kassan.

//Om man väljer att handla ska man få upp minst 3 olika produkter att köpa
//(t.ex. Korv, Dricka och Äpple). Man ska se pris för varje produkt och kunna lägga till vara i kundvagn.
//
//Kundvagnen ska visa alla produkter man lagt i den, styckpriset,
//antalet och totalpriset samt totala kostnaden för hela kundvagnen.
//Kundvagnen skall sparas i kund och finnas tillgänglig när man loggar in.

//När man ska Registrera en ny kund ska man ange Namn och lösenord.
//Dessa ska sparas och namnet ska inte gå att ändra.

//Väljer man Logga In så ska man skriva in namn och lösenord.
//Om användaren inte finns registrerad ska programmet skriva ut att kunden
//inte finns och fråga ifall man vill registrera ny kund. Om lösenordet inte
//stämmer så ska programmet skriva ut att lösenordet är fel och låta användaren försöka igen.

//Både kund och produkt ska vara separata klasser med Properties för information
//och metoder för att räkna ut totalpris och verifiera lösenord.

//I klassen Kund skall det finnas en ToString() som skriver ut Namn, lösenord och kundvagnen på
//ett snyggt sätt.

//Koden ska fungera enligt ovan beskrivning.
//Man ska kunna få ut korrekt totalpris och antal i kundvagnen.
//ToString() ska vara implementerad.
//Programmet skall fungera utan krasch.
//Det skall gå att skapa ny kund, lägga till föremål i kundvagnen, titta i sin kundvagn och sedan fortsätta handla.
//Log in ska fungera för minst 3 fördefinierade kunder.
//Kund1: Namn= "Knatte", Password= "123"
//Kund2: Namn= "Fnatte", Password= "321"
//Kund3: Namn= "Tjatte", Password= "213"
//Det skall gå att logga ut och in men inget krav på att skapade kunder skall finnas kvar emellan körningar.

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
var inputMenuTwo = string.Empty;

while (runProgram)
{
    Console.Clear();
    Console.WriteLine("---Main menu---");
    Console.WriteLine("[1] - Sign in");
    Console.WriteLine("[2] - Register new customer");
    Console.WriteLine("[3] - Exit program");
    var inputMenuOne = int.Parse(Console.ReadLine());

    switch (inputMenuOne)
    {
        case 1:
            {
                AskForUserNameAndPassword();


                if (loggedInCustomer != null)
                {

                    while (continuee)
                    {
                        Console.WriteLine($"---Welcome---");
                        Console.WriteLine("[1] - Add to cart");
                        Console.WriteLine("[2] - View Cart");
                        Console.WriteLine("[3] - Checkout - pay");
                        Console.WriteLine("[4] - Log out");
                        inputMenuTwo = Console.ReadLine();

                        switch (inputMenuTwo)
                        {
                            case "1":
                                AddItems();
                                break;
                            case "2":
                                GoToCart();
                                break;
                            case "3":
                                Console.WriteLine("---PAY---");
                                loggedInCustomer.TotalPrice();
                                break;
                            case "4":
                                continuee = false;
                                break;
                            default:
                                Console.WriteLine("You can only choose a number between 1-3. Try again!");
                                break;
                        }
                    }
                }
                else if (loggedInCustomer == null)
                {
                    Console.WriteLine("Customer does not exist! Want to register, press enter\n");
                    Console.ReadKey();
                }

                break;
            }
        case 2:
            AddNewCustomer();
            break;
        case 3:
            if (inputMenuOne == 3)
                runProgram = false;
            break;
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

            Console.Write("Password: ");
            string password = Console.ReadLine();
            if (!loggedInCustomer.VerifyPassword(password))
            {
                loggedInCustomer = null;
                Console.WriteLine("Wrong password");
                Console.ReadKey();
            }

            else if (loggedInCustomer.VerifyPassword(password))
            {
                Console.WriteLine("RÄTT LÖSENORD");
                Console.ReadKey();
            }

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
            Console.WriteLine("Try again, your password have to contain letters och numbers.");
        }

        //_customers.Add(new Customer() { Name = newName, Password = newPassword });
        _customers.Add(new Customer(newName, newPassword));
        Console.WriteLine("You are registered, press enter to log in.");
        Console.ReadKey();

    }
}

void AddItems()
{
    Console.Clear();
    Console.WriteLine("---Add items---");
    products.ForEach(item => Console.WriteLine(item));
    Console.WriteLine();
    Console.Write("Choose which item: ");
    int inputItem = int.Parse(Console.ReadLine());
    Console.Write("Choose how many: ");
    string input = Console.ReadLine();
    int inputHowMany = Int32.Parse(input);

    foreach (var item in products)
    {
        if (item.Id == inputItem)
        {
            for (int i = 0; i < inputHowMany; i++)
            {
                loggedInCustomer.Cart.Add(item);
            }
            //Console.WriteLine(inputHowMany + " " + item.Name + " is added to your cart.");
            Console.WriteLine();
        }
    }
    Console.Clear();


}

void GoToCart()
{
    Console.Clear();
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
