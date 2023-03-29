using System;
using System.Collections.Generic;
using System.Linq;
//CREATED BY  AARON C /FYSH REWARDS
class Program
{
    static void Main()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("WELCOME TO LUHNCHECK");
                Console.WriteLine("This Console App will allow you to generate a list of numbers that follow the luhns algorithm's 0 checksum");
                Console.WriteLine("Luhns Algorithm is used to validate many different sets of numbers, one use case is credit cards.");
                Console.WriteLine("You can Use 'x' in replacement of a digit to generate all the possible outcomes of numbers.");
                Console.WriteLine("This generates a whole list of validated numbers for you");
                Console.WriteLine("ENJOY,");
                Console.WriteLine(" ");

                Console.WriteLine("created by Fysh Rewards");
                Console.WriteLine(" ");
                Console.Write("Enter up to 16 digits (replacing up to 8 digits with 'x' if needed): ");
                string input = Console.ReadLine().Replace(" ", "");

                List<string> solutions = GetLuhnValidSolutions(input);

                if (solutions.Count == 0)
                {
                    Console.WriteLine("No valid solutions found.");
                }
                else
                {
                    Console.WriteLine("Valid solutions:");
                    foreach (string solution in solutions)
                    {
                        Console.WriteLine(AddSpaces(solution));
                    }
                }
                Console.Write("Export valid solutions to a text file? (y/n): ");
                ConsoleKeyInfo key2 = Console.ReadKey();
                Console.WriteLine();

                if (key2.KeyChar == 'y' || key2.KeyChar == 'Y')
                {
                    Console.WriteLine("Enter file name (without extension): ");
                    string fileName = Console.ReadLine();
                    Console.WriteLine("Saving");

                    // Write solutions to text file
                    using (StreamWriter writer = new StreamWriter(fileName + ".txt"))
                    {
                        foreach (string solution in solutions)
                        {
                            writer.WriteLine(AddSpaces(solution));
                        }
                    }

                    Console.WriteLine("File " + fileName + " saved successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            Console.WriteLine();
            Console.Write("Press R to restart, any other key to exit: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();

            if (key.KeyChar != 'r' && key.KeyChar != 'R')
            {
                break;
            }

            Console.WriteLine();
        }
    }

    static List<string> GetLuhnValidSolutions(string input)
    {
        int numXs = input.Count(c => c == 'x');
        if (numXs > 8)
        {
            throw new ArgumentException("Input contains too many 'x' characters.");
        }

        List<string> solutions = new List<string>();

        for (int i = 0; i < Math.Pow(10, numXs); i++)
        {
            string numString = i.ToString().PadLeft(numXs, '0');
            int index = 0;

            char[] digits = input.ToCharArray();
            for (int j = 0; j < digits.Length; j++)
            {
                if (digits[j] == 'x')
                {
                    digits[j] = numString[index];
                    index++;
                }
            }

            if (IsValidLuhn(digits))
            {
                if (digits.Length == 15)
                {
                    solutions.Add("0" + new string(digits));
                }
                else if (digits.Length == 16)
                {
                    solutions.Add(new string(digits));
                }
            }
        }

        return solutions;
    }

    static bool IsValidLuhn(char[] digits)
    {
        int sum = 0;
        bool alternate = false;

        for (int i = digits.Length - 1; i >= 0; i--)
        {
            int digit = digits[i] - '0';

            if (alternate)
            {
                digit *= 2;

                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }

    static string AddSpaces(string input)
    {
        return string.Join(" ", SplitInParts(input, 4));
    }

    static IEnumerable<string> SplitInParts(string input, int partLength)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (partLength <= 0)
        {
            throw new ArgumentException("Part length must be positive.", nameof(partLength));
        }

        for (int i = 0; i < input.Length; i += partLength)
        {
            yield return input.Substring(i, Math.Min(partLength, input.Length - i));
        }

    }

}
