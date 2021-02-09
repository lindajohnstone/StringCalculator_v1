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
                var pattern = new Regex("//[(.+){1,}][(.*){1,}]\n");
                Match m = pattern.Match(integers);
                if (!m.Success)
                {
                    // find delimiter
                    var regex = new Regex("\\d+(.)");
                    Match match = regex.Match(integers);
                    if (match.Success)
                    {
                        delimiter = match.Groups[1].Value;
                    }
                }
            // if line 20 is true
                if (m.Success)
                {
                     delimiter = m.Groups[1].Value;
                }
                // replace \n with delimiter
                string value = Regex.Replace(integers, "\n", delimiter);// TODO: is this a valid way to solve the problem
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