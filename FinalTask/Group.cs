using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    internal class Group
    {
        private List<Student> students;

        public Group(string number)
        {
            Number = number;
            students = new List<Student>();
        }
        public string Number { get; }
        public IEnumerable<Student> Students => students;

        public void AddStudent(Student student) => students.Add(student);
    }
}
