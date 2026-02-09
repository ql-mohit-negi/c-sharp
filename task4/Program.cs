namespace task4
{
    class RandomGenerator
    {
        private static readonly Random rnd = new();

        public static int GenerateRandomNumber(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }

    class ConsolePrinter
    {
        public static void PrintInRed(string mssg)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(mssg, "\n");
            Console.ResetColor();
        }

        public static void PrintInBlue(string mssg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(mssg, "\n");
            Console.ResetColor();
        }
    }
    class Validator
    {
        public static bool IsValidName(string name)
        {
            if (name.Length < 3 || name.Length > 15)
            {
                return false;
            }

            foreach (char c in name)
            {
                if (char.IsDigit(c))
                {
                    return false;
                }

                if (char.IsControl(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
    class Input
    {
        public static string GetName(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name) && Validator.IsValidName(name))
                {
                    return name;
                }
                else
                {
                    ConsolePrinter.PrintInRed("Please enter a valid name.");
                }
            }
        }

        public static int GetSalary(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (!int.TryParse(Console.ReadLine(), out int _salary) || _salary <= 0)
                {
                    ConsolePrinter.PrintInRed("Invalid employee pay. Employee pay should be a valid positive number.\n\n");
                }
                else return _salary;
            }
        }

        public static Employee? GetEmployeeType(string name, int salary)
        {
            while (true)
            {
                Console.Write("\nChoose employee type >> \n1. Full Time\n2. Contractor\n3. Manager\n");
                if (int.TryParse(Console.ReadLine(), out int empType))
                {
                    Employee? emp;
                    switch (empType)
                    {
                        case 1:
                            emp = new FullTime(name, salary);
                            return emp;

                        case 2:
                            emp = new Contractor(name, salary);
                            return emp;

                        case 3:
                            Console.Write("\nEnter the bonus >> ");
                            if (int.TryParse(Console.ReadLine(), out int bonus) && bonus>0)
                            {
                                emp = new Manager(name, salary, bonus);
                                return emp;
                            }
                            else
                            {
                                ConsolePrinter.PrintInRed("\nInvalid value for bonus.\n\n");
                            }
                            break;

                        default:
                            ConsolePrinter.PrintInRed("Invalid choice. Please choose in range 1-3\n\n");
                            break;
                    }
                }
                else
                {
                    ConsolePrinter.PrintInRed("\nPlease enter a valid choice.\n");
                }
            }
        }
    }
    abstract class Employee
    {
        static int _id = 0;
        
        public string Name
        {
            get;
            set;
        }

        public int ID
        {
            get;
            init;
        }

        public Employee(string name)
        {
            Employee._id++;
            ID = Employee._id;
            Name = name;
        }

        public abstract int CalculatePay();
        public abstract void ShowEmployee(); // Print Employee Details
    }

    class FullTime : Employee
    {   
        public int PayPerMonth
        {
            get;
            set;
        }

        public FullTime(string name, int pay) : base(name)
        {
            PayPerMonth = pay;
        }

        public override int CalculatePay()
        {
            int months = RandomGenerator.GenerateRandomNumber(1, 13);
            return PayPerMonth * months;
        }

        public override void ShowEmployee()
        {
            ConsolePrinter.PrintInBlue($"{ID}\t{Name}\tFull Time\t{PayPerMonth}/month\n");
        }
    }

    class Contractor : Employee
    {
        public int PayPerHour
        {
            get;
            set;
        }

        public Contractor(string name, int pay) : base(name)
        {
            PayPerHour = pay;
        }

        public override int CalculatePay()
        {
            int hours = RandomGenerator.GenerateRandomNumber(1, 25);
            return PayPerHour * hours;
        }

        public override void ShowEmployee()
        {
            ConsolePrinter.PrintInBlue($"{ID}\t{Name}\tContractor\t{PayPerHour}/hour\n");
        }
    }

    class Manager : FullTime
    {
        public int Bonus
        {
            get;
            init;
        }

        public Manager(string name, int salary, int bonus) : base(name, salary)
        {
            Bonus = bonus;
        }

        public override int CalculatePay()
        {
            int salaryBeforeBonus = base.CalculatePay();
            return salaryBeforeBonus + Bonus;
        }

        public override void ShowEmployee()
        {
            ConsolePrinter.PrintInBlue($"{ID}\t{Name}\tManager\t{PayPerMonth}/hour+{Bonus}\n");
        }
    }

    class EmployeePayrollSystem
    {
        List<Employee> employees;

        public EmployeePayrollSystem()
        {
            Console.WriteLine("<---- EMPLOYEE PAYROLL SYSTEM ---->");
            employees = new List<Employee>();
        }

        public void AddEmployee()
        {
            string? name = Input.GetName("\nEnter the employee name >> ");
            int salary = Input.GetSalary("\nEnter the salary >> ");
            Employee? emp = Input.GetEmployeeType(name, salary);

            employees.Add(emp);
        }

        public long CalculateTotalPayroll()
        {
            long totalPayroll = 0;
            foreach(var emp in employees)
            {
                totalPayroll += emp.CalculatePay();
            }

            return totalPayroll;
        }

        public void ShowEmployeeDetails()
        {

            if(employees.Count == 0)
            {
                ConsolePrinter.PrintInBlue("\nNo employees to display. Add employees first.\n\n");
            }
            else
            {
                ConsolePrinter.PrintInBlue($"\nID\tName\tDesignation\tSalary\n\n");
                foreach(var emp in employees)
                {
                    emp.ShowEmployee();
                }
            }
            Console.Write("\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmployeePayrollSystem eps = new EmployeePayrollSystem();
            while (true)
            {
                Console.Write("\nEnter your choice >> \n1. Add Employee\n2. Calculate Payroll\n3. Show Employee Details\n4. Exit\nYour choice >> ");
                if(int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    switch (userChoice)
                    {
                        case 1:
                            eps.AddEmployee();
                            break;

                        case 2:
                            long payroll = eps.CalculateTotalPayroll();
                            ConsolePrinter.PrintInBlue($"\nTotal payroll is of all the employees is ₹ {payroll}\n\n");
                            break;

                        case 3:
                            eps.ShowEmployeeDetails();
                            break;

                        case 4:
                            ConsolePrinter.PrintInBlue("\nExiting...\n");
                            return;

                        default:
                            ConsolePrinter.PrintInRed($"\nInvalid choice. Please choose in range 1-4.\n\n");
                            break;

                    }
                }
                else
                {
                    ConsolePrinter.PrintInRed("\nEnter a valid choice.\n\n");
                }
            }
        }
    }
}
