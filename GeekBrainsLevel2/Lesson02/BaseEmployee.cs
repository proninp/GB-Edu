using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson02
{
    abstract class BaseEmployee: IComparable
    {
        internal string Name { get; set; }
        internal int Age { get; set; }
        internal double Salary { get; set; }
        public BaseEmployee(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }
        internal abstract double GetAvgMothlySalary();
        internal virtual void ShowInfo() => Console.WriteLine($"{Name} with average monthly salary of {GetAvgMothlySalary()}; As Employee;");

        public int CompareTo(object obj)
        {
            BaseEmployee baseEmployee = ((BaseEmployee)obj);
            int c = Name.CompareTo(baseEmployee.Name);
            return (c == 0) ? (Age - baseEmployee.Age) : c;
        }
    }
}
