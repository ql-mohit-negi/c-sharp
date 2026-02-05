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

        public int Price
        {
            get;
            set;
        }
        public int Stock
        {
            get;
            set;
        }

        private Product(string name, int price, int stock)
        {
            totalProductsCreated++;
            Id = totalProductsCreated;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public static Product? ValidateAndConstructProduct(string name, int price, int stock)
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

        public void Sell()
        {
            Console.Write($"Available Stocks: {Stock}\nEnter the amount of stocks you want to sell >> ");
            if(int.TryParse(Console.ReadLine(), out int toSell))
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
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid entry !!\n");
                Console.ResetColor();
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Product? laptop = Product.ValidateAndConstructProduct("Mac M2 2022", 65000, 10);
            if(laptop != null)
            {
                laptop.Sell();
                Console.WriteLine($"Products sold: {Product.TotalProductsSold}");
            }
        }
    }
}
