using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise_1
{   public class TaskModel
    {

        public string _opis { get; }
        public DateTime _start { get; }
        public DateTime? _end { get; }
        public bool? _allday { get; }
        public bool? _imp { get; }
        public TaskModel(string opis, DateTime start, DateTime? end, bool? allday, bool? imp)
        {
            this._opis = opis;
            this._start = start;
            this._end = end;
            this._allday = allday;
            this._imp = imp;

        }
       

    }
    public class Color
    {
        public ConsoleColor _color ;
        public Color(ConsoleColor color)
        {
            this._color = color;

        }
        public void changecolor( )
        {
            Console.ForegroundColor = _color;

        }

    }
    static class ConsoleEX
    {
        public static void Write(string text, ConsoleColor color)
        {
            Console.WriteLine(text, color);

        }
        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.WriteLine("\n" + text, color);

        }

    }
    public class TaskMethods
    {
        public static List<TaskModel> tasklist { get; } = new List<TaskModel>();
        public static void AddTask()
        {

            string Opis;
            DateTime Start;
            DateTime? End;
            bool? Allday;
            bool? Imp;
            Console.WriteLine("Podaj opis zadania: ");
            Opis = Console.ReadLine();
            Console.WriteLine("Podaj datę rozpoczęcia zadania(dd-mm-rrrr): ");
            Start = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Zadanie całodniowe? tak - T Nie - N: ");
            string command = Console.ReadLine();
            if (command == "T")
            {   
                Allday = true;
                Console.WriteLine("chcesz dodać datę zakończenia? tak - T Nie - N: ");
                command = Console.ReadLine();
                if (command == "T")
                {
                    Console.WriteLine("Podaj datę zakończenia zadania(dd-mm-rrrr): ");
                    End = Convert.ToDateTime(Console.ReadLine());
                }
                else if (command == "N")
                {
                    End = null;
                }
                else
                {
                    Console.WriteLine("Zła komenda");
                    End = null;
                }

            }
            else if (command == "N")
            {
                Allday = false;
                Console.WriteLine("Podaj datę zakończenia zadania(dd-mm-rrrr): ");
                End = Convert.ToDateTime(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Zła komenda");
                Allday = null;
                End = null;

            }
            Console.WriteLine("Zadanie ważne? tak - T Nie - N: ");
            command = Console.ReadLine();
            if (command == "T")
            {

                Imp = true;

            }
            else if (command == "N")
            {
                Imp = false;

            }
            else
            {
                Console.WriteLine("Zła komenda");
                Imp = null;
            }
            Console.WriteLine("Na pewno dodać zadanie? tak - T Nie - N:");
            command = Console.ReadLine();
            if (command == "T")
            {
                
                TaskModel task = new TaskModel(Opis, Start, End, Allday, Imp);
                tasklist.Add(task);
            }
            else if (command == "N")
            {


            }
            else
            {
                Console.WriteLine("Zła komenda");
            }




        }
        public static void Showtask()
        {
            
            Console.WriteLine("opis    | Data rozpoczęcia      | Data zakończenia | całodniowe | ważne | ");
            var list = tasklist.OrderByDescending(TaskModel => TaskModel._imp).ThenBy(TaskModel => TaskModel._start).ThenBy(TaskModel => TaskModel._opis).ToList();
            foreach (var i in list)
            {
                
                Console.WriteLine( i._opis+"     |"+ i._start.ToString("dd-mm-yyyy") + "     |" + i._end?.ToString("dd-mm-yyyy") + "     |" + i._allday.ToString() + "     |" + i._imp.ToString());
            }
        }
       
    }
    class Program
    {
        
       
        static void Main(string[] args)
        {   
            string command = null;
            do
            {
                Console.WriteLine("Wpisz komende: (exit => wyjście)");
                command = Console.ReadLine();
                switch (command)
                {
                    case "zmien kolor":

                        Console.WriteLine("kolor po ang:");
                            command = Console.ReadLine();
                            Color color = new Color((ConsoleColor)Enum.Parse(typeof(ConsoleColor), command));
                            color.changecolor();
                        break;
                    case "add":

                        TaskMethods.AddTask();
                        
                        break;
                    case "show":
                        TaskMethods.Showtask();
                        break;
                        
                    



                    default: break;
                }

            }
            while (command != "exit");
        }
    }
}
