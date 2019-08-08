using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Exercise_1
{
    public class TaskModel
    {

        private string _opis = ""; // opis 
        public DateTime _start;
        public DateTime? _end;
        public bool? _allday = false;
        public bool? _imp = false;
        private bool _correctData = true;
        public bool CorrectData { get; private set; }

        public string Opis
        {
            get { return _opis; }
            set
            {
                if (value.Length < 21 && value.Length > 2)
                {
                    _opis = value;
                }
                else
                {
                    ConsoleEx.WriteLine("Tytuł musi zawierać od 3 do 20 znaków", ConsoleColor.Red);
                    CorrectData = false;
                }
            }
        }



        public TaskModel(string opis, DateTime start, DateTime? end, bool? allday, bool? imp)
        {
            CorrectData = true;
            Opis = opis;
            this._start = start;
            this._end = end;
            this._allday = allday;
            this._imp = imp;

        }
        public TaskModel(string opis, DateTime start, bool? allday, bool? imp)
        {
            CorrectData = true;
            Opis = opis;
            this._start = start;
            this._allday = allday;
            this._imp = imp;

        }

        public string Export()
        {
            var csv = new StringBuilder();
            csv.Append($"{Opis}|{_start}|");
            if (_allday == true)
            {
                csv.Append("");
            }
            else
            {
                csv.Append(_end);
            }
            csv.Append($"|{_allday}");
            csv.Append($"|{_imp}");
            return csv.ToString();
        }

    }



    public class Color
    {
        public ConsoleColor _color;
        public Color(ConsoleColor color)
        {
            this._color = color;

        }
        public void changecolor()
        {
            Console.ForegroundColor = _color;

        }

    }
    public static class ConsoleEx
    {
        public static void WriteLine(string message, ConsoleColor color)
        {
            ConsoleColor orgColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{message}");
            Console.ForegroundColor = orgColor;
        }

        public static void Write(string message, ConsoleColor color)
        {
            ConsoleColor orgColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{message}");
            Console.ForegroundColor = orgColor;
        }
    }
    public class TaskMethods
    {
        public static List<TaskModel> tasklist = new List<TaskModel>();
        public static void AddTaskFromString(string taskString)
        {
            if (taskString != "")
            {
                var taskStringArray = taskString.Split("|");
                string Opis = taskStringArray[0];
                string Start = taskStringArray[1];
                string End = taskStringArray[2];

                bool? Allday = bool.Parse(taskStringArray[3]);
                bool? Imp = bool.Parse(taskStringArray[4]);
                DateTime checkedDTE;
                DateTime checkedDT = DateTime.Parse(Start);
                if (Allday == true)
                {
                    DateTime.TryParse(End, out checkedDTE);
                }
                else
                {
                    DateTime.TryParse(End, out checkedDTE);
                }
                
                var taskOK = false;
                TaskModel task = new TaskModel(Opis, checkedDT, checkedDTE, Allday, Imp);
                tasklist.Add(task);

            }
        }
        public static void AddTask()
        {

            string Opis;
            string Start;
            string End;
            bool? Allday;
            bool? Imp;
            DateTime checkedDT = DateTime.Now;
            DateTime checkedDTE = DateTime.MinValue;
            var taskOK = false;
            Console.WriteLine("Podaj opis zadania: ");
            Opis = Console.ReadLine();
            Console.WriteLine("Podaj datę rozpoczęcia zadania(YYYY-MM-DD HH:mm): ");
            Start = Console.ReadLine();
            if (Start.Length != 16 || !DateTime.TryParse(Start, out checkedDT))
            {
                taskOK = false;
                ConsoleEx.WriteLine(
                    "Data rozpoczęcia musi być w formacie YYYY-MM-DD HH:mm;",
                    ConsoleColor.Red);
                return;
            }


            Console.WriteLine("Zadanie całodniowe? tak - T Nie - N: ");
            string command = Console.ReadLine();
            if (command == "T")
            {
                Allday = true;
                Console.WriteLine("chcesz dodać datę zakończenia? tak - T Nie - N: ");
                command = Console.ReadLine().ToUpper();
                if (command == "T")
                {
                    Console.WriteLine("Podaj datę zakończenia zadania(YYYY-MM-DD HH:mm): ");
                    End = Console.ReadLine();
                    if (End.Length != 16 || !DateTime.TryParse(End, out checkedDTE))
                    {
                        taskOK = false;
                        ConsoleEx.WriteLine(
                            "Data zakonczenia musi być w formacie YYYY-MM-DD HH:mm;",
                            ConsoleColor.Red);
                        return;
                    }

                }
                else if (command == "N")
                {
                    End = null;
                }
                else
                {
                    Console.WriteLine("Zła komenda", ConsoleColor.Red);
                    End = null;
                    return;
                }

            }
            else if (command == "N")
            {
                Allday = false;
                Console.WriteLine("Podaj datę zakończenia zadania(YYYY-MM-DD HH:mm): ");
                End = Console.ReadLine();
                if (End.Length != 16 || !DateTime.TryParse(End, out checkedDTE))
                {
                    taskOK = false;
                    ConsoleEx.WriteLine(
                        "Data zakonczenia musi być w formacie YYYY-MM-DD HH:mm;",
                        ConsoleColor.Red);
                    return;
                }
            }
            else
            {
                Console.WriteLine("Zła komenda", ConsoleColor.Red);
                Allday = null;
                End = null;
                return;

            }
            Console.WriteLine("Zadanie ważne? tak - T Nie - N: ");
            command = Console.ReadLine().ToUpper();
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
                Console.WriteLine("Zła komenda", ConsoleColor.Red);
                Imp = null;
                return;
            }
            Console.WriteLine("Na pewno dodać zadanie? tak - T Nie - N:");
            command = Console.ReadLine().ToUpper();
            if (command == "T")
            {

                TaskModel task = new TaskModel(Opis, checkedDT, checkedDTE, Allday, Imp);
                tasklist.Add(task);
                ConsoleEx.WriteLine("Zadanie dodano", ConsoleColor.Green);
            }
            else if (command == "N")
            {
                ConsoleEx.WriteLine("Zadanie nie dodane", ConsoleColor.Red);

            }
            else
            {
                Console.WriteLine("Zła komenda", ConsoleColor.Red);
                return;
            }




        }



        public static void Showtask(int close = 0)
        {

            var tmpConsoleBackground = Console.BackgroundColor;
            var tmpForegroundColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            var today = DateTime.Today;
            var closeDate = today.AddDays(5);
            string impor = "ważne";
            string nimpor = "nieważne";
            List<TaskModel> VItasks = new List<TaskModel>();

            List<TaskModel> Ntasks = new List<TaskModel>();
            Console.Write($"{"nazwa".PadRight(12)}|");
            Console.Write($"{"Data rozpoczęcia".PadRight(21)}| {"data zakończenia".PadRight(21)}| ");
            Console.WriteLine($"{"Ważność".PadRight(15)}");
            foreach (var t in tasklist)
            {
                if ((t._allday != null && t._start >= today && t._start < closeDate)
                    || (t._end >= today && t._end < closeDate) || close==0)
                {
                    if (t._imp == true)
                        VItasks.Add(t);
                    else
                        Ntasks.Add(t);
                }
            }

            for (var i = 0; i < 2; i++)
            {
                var tmpTask = VItasks;

                if (i == 1)
                    tmpTask = Ntasks;

                foreach (var t in tmpTask)
                {
                    Console.Write($"{t.Opis.PadRight(12)}| ");
                    Console.Write($"{t._start.ToString().PadRight(20)}| ");
                    if (t._allday != null && t._allday == true)
                    {
                        Console.Write($"{"całodniowe".ToString().PadRight(21)}");
                    }
                    else
                    {
                        Console.Write($"{t._end.ToString().PadRight(21)}");
                    }
                    if (t._imp == true)
                        Console.WriteLine($"| {impor.PadRight(15)}");
                    else
                        Console.WriteLine($"| {nimpor.PadRight(15)}");
                }

            }

            Console.BackgroundColor = tmpConsoleBackground;
            Console.ForegroundColor = tmpForegroundColor;
        }

        public static void RemoweTask()
        {
            Console.WriteLine("Podaj tytuł usuwanego zadania");
            var remove = Console.ReadLine();
            var tmpTasks = tasklist;
            var i = 0;
            foreach (var t in tmpTasks.ToList())
            {
                if (t.Opis == remove)
                {
                    tasklist.RemoveAt(i);
                    ConsoleEx.WriteLine("Zadanie usunięto", ConsoleColor.Yellow);
                }
                i++;
            }
        }



        public static void SaveTask()
        {
            var tasksToSave = new StringBuilder();
            var filePath = @"C:\Users\barte\Desktop\prework\workshop_1\Work_01\Exercise_1\Data.csv";
            foreach (var t in tasklist)
            {
                tasksToSave.Append(t.Export());
                tasksToSave.Append("\n");
            }
            if (File.Exists(filePath))
            {
                File.AppendAllText(filePath, tasksToSave.ToString());
            }
            else
            {
                
                File.WriteAllText(filePath, tasksToSave.ToString());
            }
            Console.WriteLine("zapisano");
        }

        public static void LoadTasks()
        {
            var filePath = @"C:\Users\barte\Desktop\prework\workshop_1\Work_01\Exercise_1\Data.csv";
            var textFromFile = File.ReadAllLines(filePath);
            int licznik = 0;
            foreach (var txt in textFromFile)
            {
                var csv = txt.Split('|');
                var addTask = true;
                foreach (var t in tasklist)
                {
                    if (t.Opis == csv[0])
                    {
                        addTask = false;
                    }
                }

                if (addTask)
                {
                   
                    AddTaskFromString(string.Join('|', csv));
                    licznik++;
                }
               
                else
                {
                    ConsoleEx.WriteLine($"Nie dopisano zadania o tytule '{csv[0]}'", ConsoleColor.Red);
                    ConsoleEx.WriteLine("Zadanie o tym tytule już istnieje w liście", ConsoleColor.Red);
                }
            }
            ConsoleEx.WriteLine("Wczytano " + licznik.ToString() + " zadan", ConsoleColor.Green);

        }


    }
        class Program
        {


            static void Main(string[] args)
            {
                string command = null;
            Console.WriteLine("Witam w Task Manger. \n Aby uzyskać pomoc wpisz help");
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
                        case "remove":
                            TaskMethods.RemoweTask();
                            break;
                        case "save":
                            TaskMethods.SaveTask();
                            break;
                        case "load":
                            TaskMethods.LoadTasks();
                            break;
                        case "close":
                            TaskMethods.Showtask(5);
                            break;
                        case "help":
                            Console.WriteLine("add (dodawanie zadania)"+"\n"+"remove (usuwanie zadania)"+ "\n"+ "show (listowanie zapisanych zadań)"+"\n"+
                            "save (zapisywanie zadań do pliku)"+"\n"+"load (wczytywanie zadań z pliku"+ "\n"+
                            "close (listowanie zadań w najbliższych 5 dniach)");
                            break;


                    default: break;
                    }

                }
                while (command != "exit");
            }
        }
    }

