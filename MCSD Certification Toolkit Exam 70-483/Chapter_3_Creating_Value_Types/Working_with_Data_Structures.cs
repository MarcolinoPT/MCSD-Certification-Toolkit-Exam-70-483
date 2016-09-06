using System;

namespace ConsoleApplication16.Chapter_3_Creating_Value_Types
{
    class Working_with_Data_Structures
    {
        public struct Student
        {
            public string firstName;
            public string lastName;
            public int BirthYear { get; set; }
            public char initial;
            public double score1;
            public double score2;
            public double score3;
            public double score4;
            public double score5;
            public double average;
            //public int Value = 0; Fiels cannot be initialized on a struct body

            //public Student() { } Parameterless constructor is not allowed
            private int age;
            public NickName StudentNickName;

            public Student(int birthYear)
            {
                // Fields can be initialized only using the constructor or after the struct is declared
                this.BirthYear = birthYear;
                this.firstName = "John";
                this.lastName = "Doe";
                this.initial = default(char);
                this.score1 = default(double);
                this.score2 = default(double);
                this.score3 = default(double);
                this.score4 = default(double);
                this.score5 = default(double);
                this.average = default(double);
                this.age = 18; // Private members can be initialized using only the constructor
                this.StudentNickName = new NickName(); // If your struct contains a reference type (class) as one of its members, you must call the reference type’s constructor explicitly.
            }
        }

        public class NickName
        {

        }

        public struct Book
        {
            public string title;
            public string category;
            public string author;
            public int numPages;
            public int currentPage;
            public double ISBN;
            public string coverStyle;
            public Book(string title, string category, string author, int numPages, int currentPage, double isbn, string cover)
            {
                this.title = title;
                this.category = category;
                this.author = author;
                this.numPages = numPages;
                this.currentPage = currentPage;
                this.ISBN = isbn;
                this.coverStyle = cover;
            }
            public void nextPage()
            {
                if (currentPage != numPages)
                {
                    currentPage++;
                    Console.WriteLine("Current page is now: " + this.currentPage);
                }
                else
                {
                    Console.WriteLine("At end of book.");
                }
            }
            public void prevPage()
            {
                if (currentPage != 1)
                {
                    currentPage--;
                    Console.WriteLine("Current page is now: " + this.currentPage);
                }
                else
                {
                    Console.WriteLine("At the beginning of the book.");
                }
            }
        }
    }
}