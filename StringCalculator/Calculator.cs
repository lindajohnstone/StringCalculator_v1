using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator
    {
        public Calculator()
        {
        }

        public int Add(string integers)
        {
            if (integers != " ")
            {
                //var delimiter = " ";
                String delimiter = ExtractDelimiter(integers);
                if (delimiter != null)
                {
                    var newPattern = "\\[(.)\\]";
                    Regex newRegex = new Regex(newPattern);// TODO: replacing lines 24 - 31 with while (match.Success) in document fails step 9
                    var delimiterList = new List<string>();
                    foreach (Match match in newRegex.Matches(integers))
                    {
                        delimiterList.Add(delimiter);
                    }
                    // need to tell how to find more
                    // My logic for Step 10 is as follows:
                    // If the first regex(//[(.+)?]+\n) finds a match
                    // •	Check the second regex([(.+)] 
                    // •	If a match found, check if there are more than one
                    // •	If more than one match found,
                    // •	Create a new list(outside loops)
                    // •	Add each delimiter value to list
                    // •	Foreach through the list to add the numbers to the numbers list 
                    // •	If only one match 
                    // •	Add the value of the first group
                }
                else
                {
                    var regex = new Regex("\\d+(.)");
                    Match match = regex.Match(integers);
                    if (match.Success)
                    {
                        delimiter = match.Groups[1].Value;
                    }
                }
                // replace \n with delimiter
                string value = Regex.Replace(integers, "\n", delimiter);// TODO: is this a valid way to solve the problem
                // for each delimiter in delimiter list:
                var numbers = value.Split(delimiter);

                var sum = 0;
                List<int> numbersToAdd = GetNumbers(numbers);
                ValidateNegativeNumbers(numbersToAdd);
                List<int> bigNumbers = IgnoreBigNumbers(numbersToAdd);
                sum = Sum(bigNumbers);
                return sum;
            }
            return 0;
        }

        public string ExtractDelimiter(string src) // TODO: originally private static string - changed for testing purposes
        {
            var pattern = new Regex("//\\[(.+)?\\]+\n");
            Match matchResults = pattern.Match(src);
            return matchResults.Success ? matchResults.Groups[1].Value : null;
        }

        private static List<int> GetNumbers(string[] numbers)
        {
            var numbersToAdd = new List<int>();
            foreach (string number in numbers)
            {
                Int32.TryParse(number, out var num);
                numbersToAdd.Add(num);
            }
            return numbersToAdd;
        }

        private static void ValidateNegativeNumbers(List<int> numbersToAdd)
        {
            List<int> numList = FindNegativeNumbers(numbersToAdd);
            ThrowsException(numList);
        }

        private static List<int> FindNegativeNumbers(List<int> numbersToAdd)
        {
            var numList = new List<int>();
            foreach (int num in numbersToAdd)
            {
                if (num < 0) numList.Add(num);
            }
            return numList;
        }

        private static void ThrowsException(List<int> numList)
        {
            if (numList.Count > 0)
            {
                var message = "Negatives not allowed: ";
                message = String.Concat(message, String.Join(", ", numList));
                throw new ArgumentException(message);
            }
        }
        private static List<int> IgnoreBigNumbers(List<int> numbersToAdd) // TODO: new name? adds only small numbers to list; big numbers are disgarded
        {
            var numbers = new List<int>();
            foreach (int num in numbersToAdd)
            {
                if (num < 1000) numbers.Add(num);
            }
            return numbers;
        }

        private static int Sum(List<int> numbers)
        {
            var sum = 0;
            foreach (var number in numbers)
            {
                sum = sum + number; 
            }
            return sum;
        } 
    }
}