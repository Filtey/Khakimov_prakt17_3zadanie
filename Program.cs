using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Khakimov_prakt17_3zadanie
{
    class Student
    {
        public string Fam { get; set; }
        public string Name { get; set; }
        public string Otch { get; set; }
        public string group { get; set; }
        public double mark { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = File.OpenText("file.txt");        

            string s;

            List<Student> students = new List<Student>();
            try
            {
                while ((s = sr.ReadLine()) != null)
                {
                    string[] arr = s.Split(' ');
                    students.Add(new Student
                    {
                        Fam = arr[0],
                        Name = arr[1],
                        Otch = arr[2],
                        group = arr[3],
                        mark = Convert.ToDouble(arr[4])
                    });
                }
                sr.Close();



                Student min = students.Aggregate((a, b) => a.mark < b.mark ? a : b);
                Student max = students.Aggregate((a, b) => a.mark > b.mark ? a : b);
                double sred = Math.Round(students.Average(n => n.mark), 2);

                Console.WriteLine("Студент с самым маленьким баллом:");
                Console.WriteLine(min.Fam + " " + min.Name + " " + min.Otch + " " + min.group + " " + min.mark);

                Console.WriteLine("\nСтудент с самым большим баллом:");
                Console.WriteLine(max.Fam + " " + max.Name + " " + max.Otch + " " + max.group + " " + max.mark);

                Console.WriteLine("\nСреднее значение баллов среди студентов равно " + sred);

                var sortirovka = from t in students orderby t.Fam select t;

                StreamWriter sw = File.CreateText("rezult.txt");
                sw.WriteLine("Сортировка по фамилии:\n");

                foreach (Student ss in sortirovka)
                    sw.WriteLine($"{ss.Fam} {ss.Name} {ss.Otch} {ss.group} {ss.mark}");
                sw.Close();
                Console.WriteLine("Отсортированный список успешно записан в файл!");
            }
            catch (FileNotFoundException) { Console.WriteLine("Файл не найден!"); }
            catch (IndexOutOfRangeException) { Console.WriteLine("Неправильно записаны данные в файле!"); }
            catch (FormatException) { Console.WriteLine("Неверный формат значений баллов студентов!"); }
            Console.ReadKey();
        }
    }

}
