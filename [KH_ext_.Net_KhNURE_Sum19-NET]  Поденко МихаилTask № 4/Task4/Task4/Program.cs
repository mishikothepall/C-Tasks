using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Employee {
        public string Name { get; set; }
        public int Age { get; set; }
        public HashSet<Projects> projects;
        public event EventHandler<Projects> emp_ev;

        public Employee(string name, int age) {
            Name = name;
            Age = age;
            projects = new HashSet<Projects>();
        }

        public void AddProj(Projects p) {
            p.emps.Add(this);
            projects.Add(p);
            emp_ev?.Invoke(this, p);
        }
    }

    class Projects:EventArgs {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Employee> emps;
        

        public Projects(string name, DateTime date) {
            Name = name;
            Date = date;
            emps = new List<Employee>();
        }

       
    }

    class Company {
        public string Name{ get; set; }
        private List<Employee> emps; // Cотрудники могут иметь одинаковые имена
        private HashSet<Projects> prj; //А название проекта должно быть уникальным
        

        public Company(string name) {
            Name = name;
            emps = new List<Employee>();
            prj = new HashSet<Projects>();
        }
        public void AttachPr(object ob, EventArgs e) {
           Projects p = (Projects)e;
           prj.Add(p);

        }
      
        public void AddEmp(Employee em) {
            emps.Add(em);
        }


        public void MoreThanTwo() {
            Console.WriteLine("\n---------------More than two-------------\n");

            var moreTwo = from emp in emps
                          where emp.projects.Count >= 2
                          orderby emp.Name
                          select emp;

            foreach (Employee em in moreTwo) {
                Console.WriteLine(em.Name);
            }
        }

        public void FromSpecDate(DateTime sd) {

            var spPro = from p in prj
                        where p.Date >= sd
                        orderby p.Date
                        select p;
            Console.WriteLine($"\n---------------Projects starting from {sd}----------------\n");

            foreach (Projects pr in spPro) {
                Console.WriteLine(pr.Name);
            }
        }

        public void LessThanYearYoungerThanThirty() {
            Console.WriteLine("\n-------------Less than a year---------------\n");

            var reject = from p in emps
                         where p.Age>30
                         select p.projects;

            var ft = reject.SelectMany((r) => r.ToHashSet());

            
                var getSetYears = from p in prj.Except(ft)
                                  where GetDiff(p.Date) <= 365
                                  select p;

                foreach (Projects p in getSetYears)
                {
                    Console.WriteLine(p.Name);
                }

          
          
        }

        public void TheOldestEmployeeWithSingleProject() {
            Console.WriteLine("\n-----The Oldest Employee With a Single Project---------\n");

            var maxAge = emps.Max((a)=>a.Age);
            var reject = from e in emps
                         where e.Age < maxAge
                         select e;

                var getEmpl = from e in emps.Except(reject) let dat = e.projects.ToList()
                              where (GetDiff(dat) <= 365 && GetDiff(dat) != -1) && e.projects.Count == 1
                              select e;
           
                foreach (Employee e in getEmpl)
                {
                    Console.WriteLine(e.Name);
                }

        }

        private int GetDiff(List<Projects> pr) {
            if (pr.Count >= 1)
            {
                DateTime now = DateTime.Today;
                TimeSpan span = now - pr[0].Date;
                return span.Days;
            }
            return -1; 
        }

        private int GetDiff(DateTime dt) {
            DateTime now = DateTime.Today;
            TimeSpan span = now - dt;
            return span.Days;
        }

    
    }

    public static class Extensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            Company c = new Company("EPAM");

            Projects p = new Projects("pro1", new DateTime(2015, 07, 12));
            Projects p1 = new Projects("pro2", new DateTime(2016, 08, 02));
            Projects p2 = new Projects("pro3", new DateTime(2013, 02, 22));
            Projects p3 = new Projects("pro4", new DateTime(2018, 09, 07));
            Projects p4 = new Projects("pro5", new DateTime(2018, 10, 09));
            Projects p5 = new Projects("pro6", new DateTime(2018, 11, 09));

            Employee johnny = new Employee("Johnny", 44);
            Employee tim = new Employee("Tim", 27);
            Employee anthony = new Employee("Anthony", 22);
            Employee gregor = new Employee("Gregory", 46);
            

            c.AddEmp(johnny);
            c.AddEmp(tim);
            c.AddEmp(anthony);
            c.AddEmp(gregor);
            

            johnny.emp_ev += c.AttachPr;
            tim.emp_ev += c.AttachPr;
            anthony.emp_ev += c.AttachPr;
            gregor.emp_ev += c.AttachPr;
            

            johnny.AddProj(p);
            johnny.AddProj(p1);
            johnny.AddProj(p2);
            johnny.AddProj(p3);

            tim.AddProj(p3);

            anthony.AddProj(p4);
            anthony.AddProj(p3);


            //gregor.AddProj(p4);
            gregor.AddProj(p5);

           

            c.MoreThanTwo();
            c.FromSpecDate(new DateTime(2016, 08, 02));
            c.LessThanYearYoungerThanThirty();
            c.TheOldestEmployeeWithSingleProject();

            Console.ReadLine();
        }
    }
}

