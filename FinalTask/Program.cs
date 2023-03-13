using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Students");
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string dataFilePath = GetDataFilePathFromConsole();
            if (File.Exists(dataFilePath))
            {
                try
                {
                    Student[] students = LoadStudents(dataFilePath);
                    WriteStudentsByGroups(folderPath, students);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine(dataFilePath);
                Console.WriteLine("Файл не найден!");
            }
        }

        private static void WriteStudentsByGroups(string folderPath, Student[] students)
        {
            var groups = new Dictionary<string, Group>();
            foreach (var student in students)
            {
                Group group = null;
                if (groups.ContainsKey(student.Group))
                {
                    group = groups[student.Group];
                }
                else
                {
                    group = new Group(student.Group);
                    groups.Add(student.Group, group);
                }
                group.AddStudent(student);
            }

            foreach (var group in groups)
            {
                string gropFilePath = Path.Combine(folderPath, group.Key + ".txt");
                using (StreamWriter writer = new StreamWriter(gropFilePath))
                {
                    foreach (var student in group.Value.Students)
                    {
                        writer.WriteLine($"{student.Name}, {student.DateOfBirth.ToShortDateString()}");
                    }
                }
            }
        }

        private static Student[] LoadStudents(string dataFilePath)
        {
            using (FileStream stream = File.OpenRead(dataFilePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Student[])formatter.Deserialize(stream);
            }
        }

        private static string GetDataFilePathFromConsole()
        {
            Console.Write("Укажите путь к файлу со студентами: ");
            string result = Console.ReadLine();
            return result.Trim('\"');
        }
    }
}
