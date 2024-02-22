using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Console_Calculator
{
    internal static class Calculator
    {
        public static void Calculate()
        {
            while (true)
            {                
                Console.WriteLine("Enter a Number");
                double num1 = 0;
                bool isValid = false;
                while (!isValid)
                {
                    isValid = double.TryParse(Console.ReadLine()!, out num1);
                    if (!isValid)
                        Console.WriteLine("Enter valid number");
                }
                isValid = false;
                Console.WriteLine("Enter Operator (+, -, *, /)");
                string op = Console.ReadLine()!;
                while (op != "-" && op != "+" && op != "/" && op != "*")
                {
                    Console.WriteLine("Enter valid operator");
                    op = Console.ReadLine()!;
                }
                Console.WriteLine("Enter a Number");
                double num2 = 0;
                while (!isValid)
                {
                    isValid = double.TryParse(Console.ReadLine()!, out num2);
                    if (!isValid)
                        Console.WriteLine("Enter valid number");
                }
                switch (op)
                {
                    case "+":
                        Console.WriteLine(num1 + num2);
                        break;
                    case "-":
                        Console.WriteLine(num1 - num2);
                        break;
                    case "*":
                        Console.WriteLine(num1 * num2);
                        break;
                    case "/":
                        Console.WriteLine(num1 / num2);
                        break;
                }
                Console.WriteLine("again? (y/n)");
                string exit = Console.ReadLine()!;
                while (exit != "n" && exit != "y")
                {
                    Console.WriteLine("y/n");
                    exit = Console.ReadLine()!;
                }
                if (exit == "n")
                {
                    break;
                }
            }
        }
    }
}
