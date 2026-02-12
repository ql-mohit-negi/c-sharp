namespace task6
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
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new();
            Random rnd = new();
            for (int i = 0; i < 100; i++)
            {
                int marks = rnd.Next(1, 101);
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

            //Top 3 students
            List<Student> top3Students = students.OrderByDescending(student => student.Marks).ToList();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i}\t{top3Students[i].Marks}\t{top3Students[i].Grade.ToString()}");
            }

            // Average Grade
            double avgGrade = students.Average(student => student.Marks);
            Console.WriteLine($"Average : {avgGrade}\n");

            //Failures
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
