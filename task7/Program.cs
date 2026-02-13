namespace task7
{
    enum Grades
    {
        A,
        B,
        C
    }
    class Student
    {
        public int Marks
        {
            get; set;
        }

        public Grades Grade
        {
            get; set;
        }
        public Student(int marks, Grades grade)
        {
            Marks = marks;
            Grade = grade;
        }
    }

    class StudentGenerator
    {
        static Random rnd = new();
        private static readonly object _lock = new();
        public static void Generator(int n, List<Student> students)
        {
            for(int i=0; i<n; i++)
            {
                lock (_lock)
                {
                    int marks = StudentGenerator.rnd.Next(1, 101);
                    if (marks < 50)
                    {
                        students.Add(new Student(marks, Grades.C));
                    }
                    else if (marks >= 50 && marks < 75)
                    {
                        students.Add(new Student(marks, Grades.B));
                    }
                    else
                    {
                        students.Add(new Student(marks, Grades.A));
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        { 
            List<Student> students = new();

            Thread t1 = new(() => StudentGenerator.Generator(25, students));
            Thread t2 = new(() => StudentGenerator.Generator(25, students));
            Thread t3 = new(() => StudentGenerator.Generator(25, students));
            Thread t4 = new(() => StudentGenerator.Generator(25, students));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            List<Student> top3Students = students.OrderByDescending(student => student.Marks).ToList();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i}\t{top3Students[i].Marks}\t{top3Students[i].Grade.ToString()}");
            }

            double avgGrade = students.Average(student => student.Marks);
            Console.WriteLine($"Average : {avgGrade}\n");

            List<Student> failures = students.Where(student => student.Marks < 50).ToList();
            Console.WriteLine("Failures");
            foreach (var student in failures)
            {
                Console.WriteLine($"{student.Marks}\t{student.Grade}");
            }
            Console.Write("\n");

            //challenge
            var gradeCounts = students.GroupBy(student => student.Grade).Select(group => new
            {
                Grade = group.Key,
                Count = group.Count()
            });

            Console.WriteLine("Grade\tCount");
            foreach (var gradeCount in gradeCounts)
            {
                Console.WriteLine($"{gradeCount.Grade}\t{gradeCount.Count}");
            }
        }
    }
}
