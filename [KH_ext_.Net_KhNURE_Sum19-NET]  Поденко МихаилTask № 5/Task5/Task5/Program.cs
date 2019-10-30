using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Player
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Skill { get; set; }

        public Player(string name, int age, int skill)
        {
            Name = name;
            Age = age;
            Skill = skill;
        }
    }

    class Coach {
        public string Name { get; set; }
        public int Luck { get; set; }

        public Coach(string name, int luck) {
            Name = name;
            Luck = luck;
        }
    }

    class Team
    {
        public string Name { get; set; }
        public List<Player> players;
        public int TotalSkill { get; set; }
        public Coach Coach;

        public Team(string name, Coach c)
        {
            Name = name;
            Coach = c;
            players = new List<Player>();
        }

        public void AddPlayer(Player p)
        {
            TotalSkill += p.Skill;
            if (Coach.Luck > 0) {
                TotalSkill *= Coach.Luck;
            }
            players.Add(p);
           
        }

        public void InAlphOrder() {
            Console.WriteLine($"\n---------Список игроков команды {Name} в алфавитном порядке----------\n");
            var sort = players.OrderBy(p=>p.Name);
            foreach (Player p in sort) {
                Console.WriteLine(p.Name);
            }
        }

        public void OlderThanThirty() {
            Console.WriteLine($"\n---------Список игроков команды {Name} старше 30 отсортированный по навыку----------\n");
            Console.WriteLine("|Имя | Возраст | Навык |");
            var sort = from p in players where p.Age > 30 orderby p.Skill descending select p;
            foreach (Player p in sort) {
                Console.WriteLine("{0} - {1} - {2}", p.Name, p.Age, p.Skill);
            }
            
        }
    }

    class Judge {
        public string Name { get; set; }
        public int Preference { get; set; }
        public JuniorDotNet Friend;

        public Judge(string name, int pref, JuniorDotNet friend) {
            Name = name;
            Preference = pref;
            Friend = friend;
        }

        public void Violation(Team t) {
            Console.WriteLine($"Команда {t.Name} нарушала правила!");
        }
        public void Goal(Team t) {
            Console.WriteLine($"Команда {t.Name} забила гол!");
        }
    }
//6 этап (творческий)
    class JuniorDotNet {

        public void Remark() {
            string police = "Полиция: ДЕРЖИ ВОРА!!!! ЭТО ОН УКРАЛ ХЛЕБ ИЗ АТБ!!!!";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(police);
            string some = "Я НЕ ОТДАМ ХЛЕБ! \nЯ ДЖУН ДОТ НЕТ! \nЯ РАБОТАЮ ПОЛГОДА БЕЗ ЗАРПЛАТЫ! \nЯ ГОЛОДЕН!!!";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(some);
            string another = "**Вырывает ворота из земли и кидает их в полицию**";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(another);
            string some2 = "ЭЙ ЗАБЕРИТЕ СВОЙ МЕТАЛЛОЛОМ!!!";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(some2);
            Console.ForegroundColor = ConsoleColor.White;
            throw new BribeException();
        }
    }

    class Game
    {
        private Team t1;
        private Team t2;
        private Judge jd;
        public delegate void Violation(Team t);
        public delegate void Goal(Team t);
        public event Violation viol;
        public event Goal go;

        public Game(Team te1, Team te2, Judge j)
        {
            t1 = te1;
            t2 = te2;
            jd = j;
        }

        public void Play()
        {
            try
            {
                int ts1 = t1.TotalSkill;
                int ts2 = t2.TotalSkill;
                ValidateTeams(t1, t2);
                GetPrefs(ref ts1, ref ts2);
                if (ts1 > ts2 * 1.1)
                {
                    Console.WriteLine($"Выигрывает команда {t1.Name} со Скилом: {t1.TotalSkill} \nПроигрывает {t2.Name} со Скилом {t2.TotalSkill}");
                    go?.Invoke(t1);
                    viol?.Invoke(t2);
                }
                else
                if (ts1 * 1.1 < ts2)
                {
                    Console.WriteLine($"Выигрывает команда {t2.Name} со Скилом: {t2.TotalSkill} \nПроигрывает {t1.Name} со Скилом {t1.TotalSkill}");
                    go?.Invoke(t2);
                    viol?.Invoke(t1);
                }
                else
                {
                    Console.WriteLine($"Ничья \n {t1.TotalSkill} \n {t1.TotalSkill}");
                    go?.Invoke(t1);
                    go?.Invoke(t2);

                    viol?.Invoke(t1);
                    viol?.Invoke(t2);
                }
            }
            catch (GameExceptions ex) {
                Console.WriteLine(ex.Message);
            }
            catch (BribeException be) {
                Console.WriteLine(be.Message);
            }
        }

        private void ValidateTeams(Team tm1, Team tm2) {
            if (t1.players.Count < t2.players.Count)
            {
                Console.WriteLine("Команды не полные!");
                throw new GameExceptions(t1, t2);

            }
            else if (t1.players.Count > t2.players.Count)
            {
                Console.WriteLine("Команды не полные!");
                throw new GameExceptions(t2, t1);
            }

        }

        private void GetPrefs(ref int ts1, ref int ts2) {
            
            if (jd.Preference == 1)
            {
                Console.WriteLine($"Судья предпочитает команду {t1.Name}");
                ts1 *= 100;
            }
            else
            if (jd.Preference == 2)
            {
                Console.WriteLine($"Судья предпочитает команду {t2.Name}");
                ts2 *= 100;
            }
            else if (jd.Preference>2) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Судью ловят на взятке!!! \nНо удача на его стороне! \nНа поле появляется 'друг' нашего судьи. ");
                jd.Friend.Remark();
            }
            else if (jd.Preference == 0) {
                Console.WriteLine("Судья беспринципен");
            }
            
        }
    }

    class GameExceptions:Exception { 
        public GameExceptions(Team t1, Team t2) 
        : base(String.Format($"Команда {t1.Name} меньше чем {t2.Name}")) {
        }
    }

    class BribeException : Exception {
        public BribeException() 
            : base(String.Format("Воспользовавшись ситуацией продажный судья убегает на Мальдивы \nМатч отменяется!")) { }
    }

    class Program
    {
        private static Random rdn = new Random();

        static void Main(string[] args)
        {
            
            Coach c1 = new Coach("Coach1", GetRandomNumber());
            Team t1 = new Team("Барселона", c1);
            string[] names = { "Jordano", "Bruno", "Adriano", "Celentano", "Milo", "Andrew", "John", "Greg", "Random", "Postal", "Dude", "Wafler", "Pencilhead", "Dereck", "Dwayne", "Timoty", "I don't", "like", "football", "Elvis" };
            for (int i = 0; i < 20; i++)
            {
                Player p = new Player(names[i], GetRandomAge(), GetRandomNumber());
                t1.AddPlayer(p);
            }

            Coach c2 = new Coach("Coach2", GetRandomNumber());
            Team t2 = new Team("Нижние Васюки", c2);
            string[] names2 = { "Вася", "Петя", "Вова", "Жора", "Дима", "Егор", "Виталя", "Гена", "Эдуард", "Гоги", "Шмоги", "Исаак", "Дед Иван", "Петрович", "Дранкель", "Жранкель", "Гер Майор", "Сергей", "Леонид", "Баба Варя" };
            for (int i = 0; i < 20; i++)
            {
                Player p = new Player(names[i], GetRandomAge(), GetRandomNumber());
                t2.AddPlayer(p);
            }

            JuniorDotNet jdn = new JuniorDotNet();
            Judge j = new Judge("Prodajniy", Pref(), jdn);
            Game g = new Game(t1, t2, j);

            g.go += j.Goal;
            g.viol += j.Violation;
            g.Play();

            //t1.InAlphOrder();
            //t1.OlderThanThirty();
         
           
            Console.ReadLine();
        }

        public static int Pref() {
            return rdn.Next(0,4);
        }

        public static int GetRandomNumber()
        {
            Random rnd = new Random();
            int ini = 0;
            int fin = 0;
            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    ini = rnd.Next(0, 50);
                    continue;
                }
                fin = rnd.Next(51, 100);
            }
            return rdn.Next(ini, fin);
        }

        public static int GetRandomAge()
        {
            Random rnd = new Random();
            int ini = 0;
            int fin = 0;
            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    ini = rnd.Next(0, 25);
                    continue;
                }
                fin = rnd.Next(26, 50);
            }
            return rdn.Next(ini, fin);
        }

    }
}
