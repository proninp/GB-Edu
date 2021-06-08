using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson02
{
    class Outsourcer : BaseEmployee
    {
        internal double SalaryPerHour { get; set; }
        public Outsourcer(string name, int age, double salaryPerHour) : base(name, age, 0) => SalaryPerHour = salaryPerHour;
        internal override double GetAvgMothlySalary() => SalaryPerHour * 20.8 * 8;
        internal override void ShowInfo() => Console.WriteLine($"{Name} with average monthly salary of {GetAvgMothlySalary()} and salary per hour of {SalaryPerHour}; As Outsourcer;");
    }
}
