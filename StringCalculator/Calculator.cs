using System;
using System.Collections.Generic;
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
                var numbers = Regex.Split(integers, "[*#%\n]?[^-\\d]");
                var numList = new List<int>();
                var sum = 0;
                List<int> numbersToAdd = GetNumbers(numbers);
                ValidateNegativeNumbers(numbersToAdd);
                ThrowsException(numList);
                List<int> bigNumbers = FindBigNumbers(numbersToAdd);
                sum = AddNumbers(bigNumbers);
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
        private static List<int> FindBigNumbers(List<int> numbersToAdd)
        {
            var bigNumbers = new List<int>();
            foreach (int num in numbersToAdd)
            {
                if(num < 1000) bigNumbers.Add(num);
            }
            return bigNumbers;
        }

        private static int AddNumbers(List<int> bigNumbers)
        {
            var sum = 0;
            foreach (var number in bigNumbers)
            {
                sum = sum + number; 
            }
            return sum;
        } 
    }
}