// See https://aka.ms/new-console-template for more information

namespace task3
{
    class Product
    {
        static int totalProductsSold = 0;
        public static int TotalProductsSold
        {
            get { return totalProductsSold; }
        }

        static int totalProductsCreated = 0;
        public int Id
        {
            get; init;
        }

        public string Name
        {
            get; set;
        }

        public decimal Price
        {
            get;
            set;
        }
        public int Stock
        {
            get;
            set;
        }

        private Product(string name, decimal price, int stock)
        {
            totalProductsCreated++;
            Id = totalProductsCreated;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public static Product? ValidateAndConstructProduct(string name, decimal price, int stock)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nProduct name cannot be null !!\n");
                Console.ResetColor();
                return null;
            }

            if (price < 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nPrice cannot be negative !!\n");
                Console.ResetColor();
                return null;
            }

            if(stock < 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nStock cannot be negative !!\n");
                Console.ResetColor();
                return null;
            }

            return new Product(name, price, stock);
        }

        public void Sell(int toSell)
        {
                if(Stock < toSell)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\nNot enough stock !!\n");
                    Console.ResetColor();
                }
                else
                {
                    Stock = Stock - toSell;
                    totalProductsSold += toSell;
                }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter the product name >> ");
            string? productName = Console.ReadLine();

            Console.Write("Enter the price >> ");
            if(!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Price must be a valid number!");
                Console.ResetColor();
                return;
            }

            Console.Write("Enter the stocks >> ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Stocks must be a valid number!");
                Console.ResetColor();
                return;
            }

            Product? laptop = Product.ValidateAndConstructProduct(productName, price, stock);
            if(laptop != null)
            {
                Console.Write($"Product\tPrice\tStock\n{laptop.Name}\t{laptop.Price}\t{laptop.Stock}\n\nEnter the amount of stocks you want to sell >> ");
                if(int.TryParse(Console.ReadLine(), out int toSell) && toSell>0)
                {
                    laptop.Sell(toSell);
                    Console.WriteLine($"Products sold: {Product.TotalProductsSold}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid entry !!\n");
                    Console.ResetColor();
                }
            }
        }
    }
}
