using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson02
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseEmployee[] employees = new BaseEmployee[]
            {
                new Employee("Vincent Vega", 45, 120000),
                new Employee("Forest Gump", 22, 23000),
                new Employee("Tyler Durden", 32, 75000),
                new Employee("Thomas Anderson", 30, 48000),
                new Employee("Marty McFly", 18, 21000),
                new Employee("Rick Deckard", 44, 80000),
                new Employee("Motoko Kusanagi", 21, 54321),
                new Outsourcer("Frodo Baggins", 20, 850),
                new Outsourcer("Neville Longbottom", 19, 780),
                new Outsourcer("Peter Parker", 18, 870),
                new Outsourcer("Barry Allen", 26, 920),
                new Outsourcer("Eren Yeager", 23, 800)
            };
            Department department = new Department("Characters", employees);
            Console.WriteLine("\nBefore sort:\n");
            foreach (BaseEmployee el in department)
                el.ShowInfo();
            Array.Sort(department.Employees);
            Console.WriteLine("\nAfter sort:\n");
            foreach (BaseEmployee el in department)
                el.ShowInfo();
            Console.ReadLine();
        }
    }
}
