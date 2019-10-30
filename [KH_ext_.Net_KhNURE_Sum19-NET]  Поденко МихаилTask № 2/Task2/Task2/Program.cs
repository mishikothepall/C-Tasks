using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {

  //----------------------Интерфейсы-----------------------------------------------

        interface IFly {
            string Fly();
        }
        interface ISwim {
            string Swim();
        }
        interface IRun {
            string Run();
        }
        interface IEngine {
            string Engine();
        }

//-------------------Абстрактный класс------------------------------------------

        abstract class Some_Entity {

            public string Name;
            public abstract void AboutMe();
        }

//-----------------------Самолет-----------------------------------------------

        class Plane : Some_Entity, IFly, ISwim, IEngine
        {
            private string[] fb = {"Каталина", "БЕ-6", "Turbo Otter", "BV-238" };
            public Plane(string name) {
                Name = name;
            }
            public override void AboutMe()
            {
                string otpt = string.Empty;
                if (Array.IndexOf(fb, Name)>=0)
                {
                    otpt = $"{Engine()}, {Fly()}, {Swim()}";
                }
                else
                {
                    otpt = $"{Engine()}, {Fly()}";
                }
                Console.WriteLine($"Я {Name} и я умею: {otpt}");
            }

            public string Engine()
            {
                return "BRUM-BRUM-BRUM";
            }

            public string Fly()
            {
                return "fly";
            }


            public string Swim()
            {
                return $"{Name} is a flying boat";
            }
        }

//----------------------------Орел------------------------------------

        class Eagle : Some_Entity, IFly
        {
            public Eagle(string name) {
                Name = name;
            }
            public override void AboutMe()
            {
                Console.WriteLine($"Я {Name} и я умею: {Fly()}");
            }

            public string Fly()
            {
                return "fly";
            }

        }

//--------------------------Утка-----------------------------------

        class Duck : Some_Entity, IFly, ISwim
        {
            public Duck(string name) {
                Name = name;
            }

            public override void AboutMe()
            {
                Console.WriteLine($"Я {Name} и я умею: {Fly()}, {Swim()}");
            }

            public string Fly()
            {
                return "fly";
            }

            public string Swim()
            {
                return "swim";
            }
        }

//-------------------------Курица----------------------------------

        class Chicken : Some_Entity, IRun, ISwim
        {
            public Chicken(string name) {
                Name = name;
            }
            public override void AboutMe()
            {
                Console.WriteLine($"Я {Name} и я умею: {Run()}, {Swim()}");
            }

          

            public string Run()
            {
                return "run";
            }

            public string Swim()
            {
                return "swim"; ;
            }
        }

//---------------------Моторная лодка---------------------------

        class M_boat : Some_Entity, ISwim, IEngine
        {
            public M_boat(string name) {
                Name = name;
            }
            public override void AboutMe()
            {
                Console.WriteLine($"Я {Name} и я умею: {Engine()}, {Swim()}");
            }

            public string Engine()
            {
                return "BRUM-BRUM-BRUM";
            }

            public string Swim()
            {
                return "swim";
            }
        }

//------------------------Заяц------------------------------------

        class Bunny : Some_Entity, IRun, ISwim
        {
            public Bunny(string name) {
                Name = name;
            }
            public override void AboutMe()
            {
                Console.WriteLine($"Я {Name} и я умею: {Run()}, {Swim()}");
            }

            public string Run()
            {
                return "run";
            }
            public string Swim()
            {
                return "swim";
            }
        }

//-----------------------------Main-----------------------------

        static void Main(string[] args)
        {
            Plane p = new Plane("Бройлер 747");
            Plane fb = new Plane("Turbo Otter");
            Eagle e = new Eagle("Орел"); 
            Duck d = new Duck("Утка");
            Chicken ch = new Chicken("Polly");
            M_boat mb = new M_boat("Zodiac");
            Bunny bun = new Bunny("Bunny");

            ISwim[] swim ={ fb, d, ch, mb, bun };
            IFly[] fly= { p, fb, e, d };

            Console.WriteLine("\n-----------------Swimming objects------------------\n");

            foreach (Some_Entity se in swim) {
                se.AboutMe();
            }

            Console.WriteLine("\n----------------Flying objects---------------------\n");

            foreach (Some_Entity se in fly) {
                se.AboutMe();
            }

            Console.ReadLine();
        }

    }
}
