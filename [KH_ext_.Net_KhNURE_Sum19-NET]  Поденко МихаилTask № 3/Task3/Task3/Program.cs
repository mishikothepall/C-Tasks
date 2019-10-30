using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{

   
    public class Accident:EventArgs {

        private int spd;
        Officer of;
        public int Speed {
            get { return spd; }
            set { spd = value; }
        }
     
    }

    class Driver{

    public event EventHandler<Accident> acc;

        public void Generate() {
            Accident accident = new Accident();
            Random rnd = new Random();
            int spd = rnd.Next(60, 120);
            accident.Speed = spd;
            acc?.Invoke(this, accident);
        }

        //Версия без класса Random

        public void Generate(int spd) {
            Accident accident = new Accident();
            accident.Speed = spd;
            acc?.Invoke(this,accident);
        }

    }

    class Officer 
    {
      
        public void FineGenerator(object ev, EventArgs e)
        {
            Accident ac = (Accident)e;
            int spd = ac.Speed;
            if (spd>75) {
                MakeDecision(spd);
            }
            else
            {
                Console.WriteLine("Удачной дороги!");
            }
        }

        public void MakeDecision(int spd) {
            int des = spd - 75;
            if (des > 0 && des <= 10)
            {
                Console.WriteLine("Штраф 100 грн");
            }
            else if (des > 10 && des <= 20)
            {
                Console.WriteLine("Штраф 200 грн");
            }
            else if (des >= 20)
            {
                Console.WriteLine("Штраф 500 грн");
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Driver driver = new Driver();          
            Officer officer = new Officer();
            driver.acc += officer.FineGenerator;
            driver.Generate();
            Console.ReadLine();
        }
    }
}
