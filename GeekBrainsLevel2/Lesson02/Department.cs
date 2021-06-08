using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson02
{
    class Department: IEnumerable
    {
        internal string Name { get; set; }
        internal BaseEmployee[] Employees {get; set;}
        public Department(string name, BaseEmployee[] employees)
        {
            Name = name;
            Employees = employees;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Employees.Length; i++)
                yield return Employees[i];
        }
    }
}
