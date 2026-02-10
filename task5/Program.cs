namespace task5
{

    interface IPayable
    {
        void ProcessPayment(decimal amount);    
    }

    class CreditCardPayment : IPayable
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Initiating credit card payment...Paying ${amount}");
            Console.WriteLine("Payment completed.\n");
        }
    }

    class PayPalPayment : IPayable
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Initiating PayPal card payment...Paying ${amount}");
            Console.WriteLine("Payment completed.\n");
        }
    }

    //challenge
    class BitCoinPayment : IPayable
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Initiating bitcoin payment...Paying {amount} bitcoin");
            Console.WriteLine("Payment completed.\n");
        }
    }

    class PaymentGatewaySimulator
    {
        public void ProcessPayment(IPayable Processor)
        {
            Console.Write("Enter the amount to be processed >> ");
            if (decimal.TryParse(Console.ReadLine(), out decimal payment) && payment>0)
            {
                Processor.ProcessPayment(payment);
            }
            else
            {
                Console.WriteLine("\nInvalid amount. Terminating this payment.\n");
            }
        }

        public void Simulate()
        {
            while (true)
            {
                Console.WriteLine("\nChoose the payment method:\n1. Credit Card\n2. PayPal\n3. Bitcoin\n4. Exit\n");
                if(int.TryParse(Console.ReadLine(), out int user_choice))
                {
                    IPayable? paymentMethod = null;
                    if (user_choice == 4) break;
                    switch (user_choice)
                    {
                        case 1:
                            paymentMethod = new CreditCardPayment();
                            break;

                        case 2:
                            paymentMethod = new PayPalPayment();
                            break;

                        case 3:
                            paymentMethod = new BitCoinPayment();
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice!! Enter in range 1-4.\n");
                            break;
                    }

                    if(paymentMethod == null)
                    {
                        continue;
                    }

                    ProcessPayment(paymentMethod);
                }
                else
                {
                    Console.WriteLine("\nInvalid choice!! Enter a valid positive number.\n");
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            PaymentGatewaySimulator simulator = new();
            simulator.Simulate();
        }
    }
}
