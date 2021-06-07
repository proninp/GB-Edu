using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson02
{
    class Employee : BaseEmployee
    {
        public Employee(string name, int age, double salary): base(name, age, salary) { }
        internal override double GetAvgMothlySalary() => Salary;
    }
}
