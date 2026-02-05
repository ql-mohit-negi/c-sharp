// See https://aka.ms/new-console-template for more information

class GradebookManager
{
    List<int> Grades;

    public GradebookManager()
    {
        Grades = new List<int>();
    }

    public void AddGrade(params int[] grades)
    {
        Grades.AddRange(grades);
    }

    public double CalculateAverage()
    {
        double avg = 0.0;

        foreach(int grade in Grades)
        {
            avg += grade;
        }

        return Grades.Count != 0 ? avg/Grades.Count : 0;
    }
    
    public void PrintReport()
    {
        Console.WriteLine("<------- Report ------->");
        Console.WriteLine($"Total number of grades: {Grades.Count}");
        for(int i= 0; i < Grades.Count; i++)
        {

            Console.WriteLine($"Grade {i+1}: {Grades[i]}");
        }
        Console.Write($"Grade Average: {CalculateAverage()}\n\n");
    }
}


class Program
{
    public static void Main(string[] args)
    {
        GradebookManager manager = new GradebookManager();
        Console.WriteLine("<------ GRADEBOOK MANAGER ----->\nChoose any one >> ");
        while (true)
        {
            Console.WriteLine("1. Add Single Grade\n2. Add Multiple Grades\n3. See Average\n4. Report\n5. Exit");
            bool exit = false;
            if(int.TryParse(Console.ReadLine(), out int user_choice))
            {
                switch (user_choice)
                {
                    case 1:
                        Console.Write("Enter grade out of 100 >> ");
                        if(int.TryParse(Console.ReadLine(), out int grade))
                        {
                            if(grade < 0 || grade > 100)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid grade. Grade should be in the range 0-100\n\n");
                                Console.ResetColor();
                            }
                            else manager.AddGrade(grade);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid grade. Grade must be in range 0-100.\n\n");
                            Console.ResetColor();
                        }
                        break;

                    case 2:
                        Console.Write("Enter the grades you want to add (space separated) and press enter to continue >> ");
                        string? input = Console.ReadLine();
                        try
                        {
                            int[] grades = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                            foreach (int _grade in grades)
                            {
                                if(_grade < 0 || _grade > 100)
                                {
                                    throw new FormatException();
                                }
                            }
                            manager.AddGrade(grades);
                        }
                        catch(FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid grade. Grade must be in range 0-100.\n\n");
                            Console.ResetColor();
                        }
                        catch(Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\nAn error occured while adding grades.\n");
                            Console.ResetColor();
                        }
                        break;

                    case 3:
                        double avg = manager.CalculateAverage();
                        Console.Write($"Average grades >> {avg}\n\n");
                        break;

                    case 4:
                        manager.PrintReport();
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid choice: {user_choice}. Please choose any in range 1-5.\n\n");
                        Console.ResetColor();
                        break;
                }

                if (exit) break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid choice. Please choose a valid number.\n\n");
                Console.ResetColor();
            }
        }
    }
}