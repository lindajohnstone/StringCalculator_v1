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
                var delimiter = " ";
                var delimiterList = new List<string>();
                /* var pattern = @"//(.)+\n";
                if (integers.StartsWith("//") && integers.Contains("\n"))
                {
                    var newString = Regex.Split(integers, pattern);
                    delimiter = newString[1];
                    var nums = newString[2];// integers with delimiter
                } */
                var regex = new Regex("\\d(.)");
                Match match = regex.Match(integers);
                if (match.Success)
                {
                    delimiter = match.Groups[1].Value;
                    // var delimiterString = Regex.Replace(integers, "\\d", " ");
                    // var delimiterArray = delimiterString.Distinct().ToArray();
                    // foreach (var limiter in delimiterArray)
                    // {
                    //     delimiterList.Add(delimiter);
                    // }
                }
                // var numbers = new List<string>();
                // foreach (var limiter in delimiterList)
                // {
                //     var nums = integers.Split(delimiter);
                //     foreach(var num in nums)
                //     {
                //         numbers.Add(num);
                //     }
                // }
                var numbers = integers.Split(delimiter);
                
                var sum = 0; 
                List<int> numbersToAdd = GetNumbers(numbers.ToArray());
                ValidateNegativeNumbers(numbersToAdd);
                List<int> bigNumbers = IgnoreBigNumbers(numbersToAdd);
                sum = Sum(bigNumbers);
                return sum;
            }
            return 0;
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