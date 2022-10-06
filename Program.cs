using ClassroomStart.Data;
using ClassroomStart.Models;
using Microsoft.EntityFrameworkCore;

Console.Title = " ======================exsm3943-project====================================== ";

Console.ForegroundColor = ConsoleColor.White;


MainMenu();

static void MainMenu()
{
    var question = "1) New or Existing Customers\n2) Exit";
    Console.WriteLine(question);

    var options = new int[] { 1, 2 };
    var answer = Answer(question, options);
    switch (answer)
    {
        case 1: NewCustomer(); break;
        case 2: Environment.Exit(0); break;
    }
}

static void NewCustomer()
{
    var context = new DatabaseContext();

    Console.WriteLine("Enter customer number");
    int customerNumber = ValidIntInput(false, 0, 0, "Customer Number");
    var customer = context.Customers.Find(customerNumber);
    if (customer == null)
    {
        Console.WriteLine("Enter First Name");
        var firstName = ValidString(true, 1, 30, "First Name");

        Console.WriteLine("Enter Last Name");
        var lastName = ValidString(true, 1, 50, "Last Name");

        Console.WriteLine("Enter Home Address");
        var homeAddress = ValidString(true, 1, 50, "Home Address");

        Console.WriteLine("Enter Phone Number");
        var phoneNumber = ValidLongInput(true, 10, 10, "Phone Number");

        customer = new Customer(customerNumber, firstName, lastName, homeAddress, phoneNumber);

        context.Customers.Add(customer);
        context.SaveChanges();
    }
    ProductMenu(customerNumber);

    MainMenu();
}

static void ProductMenu(int customerNumber)
{
    var context = new DatabaseContext();
    var customer = context.Customers.Find(customerNumber);
    var products = context.Inventories.ToList();

    var productList = "Choose a Product\n...................";

    var productDict = new Dictionary<int, Inventory>();

    var options = new int[products.Count];
    for (int i = 0; i < products.Count; i++)
    {
        var product = products[i];
        int index = i + 1;
        options[i] = index;
        productList += $"\n{index}. {product.ProductName}\t{product.QuantityOnHand}";
        productDict[index] = product;
    }

    Console.WriteLine(productList);

    var productIndex = Answer(productList, options);
    var selectedProduct = productDict[productIndex];

    int productQuantity = 0;

    do
    {
        Console.WriteLine("Enter a Quantity");
        productQuantity = ValidIntInput(true, 1, int.MaxValue, "Quantity");
        if (productQuantity > selectedProduct.QuantityOnHand)
        {
            Console.WriteLine("Too much");
        }
    } while (productQuantity > selectedProduct.QuantityOnHand);


    var order = new Order()
    {
        Customer = customer,
        Inventory = selectedProduct,
        CustomerId = customer.CustomerId,
        ProductId = selectedProduct.ProductId,
        QuantitySold = productQuantity,
        TotalPrice = selectedProduct.SalePrice * productQuantity
    };

    selectedProduct.QuantityOnHand -= productQuantity;

    context.Orders.Add(order);
    context.Inventories.Update(selectedProduct);
    context.SaveChanges();
}

static int Answer(string question, int[] options)
{
        int answer = 0;

        var input = Console.ReadLine();
        if (!int.TryParse(input, out int option))
        {
            Console.WriteLine("Wrong input!");
            answer = Answer(question, options);
        }
        else
        {
            if (!options.Contains(option))
            {
                Console.WriteLine("Wrong option");
                answer = Answer(question, options);
            }
            else
            {
                answer = option;
            }
        }

     return answer;
}

static int ValidIntInput(bool validateLength, int min, int max, string propertyName)
{
        int answer = 0;

        var input = Console.ReadLine();
        if (!int.TryParse(input, out int option))
        {
            Console.WriteLine($"{propertyName} should be a number!");
            answer = ValidIntInput(validateLength, min, max, propertyName);
        }
        else
        {
            if (validateLength)
            {
                var optionLength = option.ToString().Length;
                if (optionLength < min || optionLength > max)
                {
                    Console.WriteLine($"{propertyName} must not be less than {min} and greater than {max}");
                    answer = ValidIntInput(validateLength, min, max, propertyName);
                }
                else
                {
                    answer = option;
                }
            }
            else
            {
                answer = option;
            }
        }

        return answer;
}

static long ValidLongInput(bool validateLength, int min, int max, string propertyName)
{
    long answer = 0;

    var input = Console.ReadLine();
    if (!Int64.TryParse(input, out long option))
    {
        Console.WriteLine($"{propertyName} should be a number!");
        answer = ValidLongInput(validateLength, min, max, propertyName);
    }
    else
    {
        if (validateLength)
        {
            var optionLength = option.ToString().Length;
            if (optionLength < min || optionLength > max)
            {
                Console.WriteLine($"{propertyName} must not be less than {min} or greater than {max} digits");
                answer = ValidLongInput(validateLength, min, max, propertyName);
            }
            else
            {
                answer = option;
            }
        }
        else
        {
            answer = option;
        }
    }

    return answer;
}
    
static string ValidString(bool validateLength, int min, int max, string propertyName)
{
        string answer = string.Empty;
        var input = Console.ReadLine();
        if (input == null || string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine($"{propertyName} must not be null or empty");
            answer = ValidString(validateLength, min, max, propertyName);
        }
        else
        {
            if (validateLength)
            {
                var optionLength = input.Length;
                if (optionLength < min || optionLength > max)
                {
                    Console.WriteLine($"{propertyName} must not be less than {min} and greater than {max}");
                    answer = ValidString(validateLength, min, max, propertyName);
                }
                else
                {
                    answer = input;
                }
            }
            else
            {
                answer = input;
            }
        }

    return answer;
}


/*
public partial class Program
{
    static string? password = null;
}
*/
