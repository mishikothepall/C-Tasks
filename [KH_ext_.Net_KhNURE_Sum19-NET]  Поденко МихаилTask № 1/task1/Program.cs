using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Student {

    public string Name { get; set; }
    public int Mark { get; set; }

        public Student(string name, int mark) {
            Name = name;
            Mark = mark;
        }
    }

    class Rating {
        Student[] stud;
        public Rating() {
            stud = new Student[0];
        }

        public void Add(Student st) {
            Student[] std = new Student[stud.Length+1];
            for (int k=0; k<stud.Length; k++) {
            if (st.Name.Equals(stud[k].Name)&&st.Mark==stud[k].Mark) {
                    return;
                }
            }
            for (int i =0; i<stud.Length; i++) {
               
                std[i] = stud[i];
            }
            std[stud.Length] = st;
            stud = std;
        }

        public void Print() {

            




            //for (int j = 0; j < stud.Length; j++)
            //{
            //    for (int i = 0; i < stud.Length - 1; i++)
            //    {
            //        if (!stud[i].Equals(stud[i + 1]) && stud[i].Mark < stud[i + 1].Mark)
            //        {
            //            Change(ref stud[i], ref stud[i + 1]);

            //        }

            //    }

            //}

            Array.Sort(stud, (s1, s2) => -1 * s1.Name.CompareTo(s2.Name));
            Array.Sort(stud, (s1, s2) => -1 * s1.Mark.CompareTo(s2.Mark));

            for (int k = 0; k < stud.Length; k++)
            {
                Console.WriteLine("{0} {1}", stud[k].Name, stud[k].Mark);
            }
        }

        private void Change(ref Student st1, ref Student st2 ) {
            Student temp = st1;
            st1 = st2;
            st2 = temp;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Rating rt = new Rating();
            rt.Add(new Student("Яковлев", 80));
            rt.Add(new Student("Петров", 85));
            rt.Add(new Student("Андреев", 85));
            rt.Add(new Student("Федоров", 75));
            rt.Add(new Student("Богданов", 94));
            rt.Add(new Student("Петров", 85));
 
            rt.Print();
            Console.ReadLine();
        }
    }
}
