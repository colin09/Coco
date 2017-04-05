using System;
using ClassLibrary;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"The answer is {new Thing().Get(19, 23)}");
        }
    }
}
