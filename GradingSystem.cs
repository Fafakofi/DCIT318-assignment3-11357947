using System;
using System.Collections.Generic;
using System.IO;
public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Score { get; set; }
    public Student(int id, string fullname, int score)
    {
        Id = id;
        FullName = fullname;
        Score = score;
    }
    public string GetGrade()
    {
        if (Score >= 80 && Score <= 100)
            return "A";
        else if (Score >= 70 && Score <= 79)
            return "B";
        else if (Score >= 60 && Score <= 69)
            return "C";
        else if (Score >= 50 && Score <= 59)
            return "D";
        else
            return "F";
    }
   }
public class InvalidScoreFormatException : Exception
{
    public InvalidScoreFormatException(string message) : base(message) { }
}

public class MissingFieldException : Exception
{
    public MissingFieldException(string message) : base(message) { }
}
public class StudentResultProcessor
{
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        List<Student> students = new List<Student>();

        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            string line;
            int lineNumber = 0;

            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;

             
                string[] parts = line.Split(',');

                if (parts.Length != 3)
                {
                    throw new MissingFieldException($"Line {lineNumber}: Missing field(s) - Expected 3 fields.");
                }

                string idStr = parts[0].Trim();
                string name = parts[1].Trim();
                string scoreStr = parts[2].Trim();

                if (!int.TryParse(idStr, out int id))
                {
                    throw new FormatException($"Line {lineNumber}: Invalid ID format.");
                }
                if (!int.TryParse(scoreStr, out int score))
                {
                    throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format.");
                }

              
                students.Add(new Student(id, name, score));
            }
        }

        return students;
    }
        public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
            }
        }
    }
}