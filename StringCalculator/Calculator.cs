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
                var numbers = Regex.Split(integers, @"\D+");
                var numList = new List<int>();
                var sum = 0;
                List<int> numbersToAdd = GetNumbers(numbers);
                FindNegativeNumbers(numbersToAdd);
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

        private static List<int> FindNegativeNumbers(List<int> numbersToAdd)
        {
            var numList = new List<int>();
            foreach (int num in numbersToAdd)
            {
                if (num < 0) numList.Add(num);
            }
            return ThrowsException(numList);
        }

        private static List<int> ThrowsException(List<int> numList)
        {
            if (numList.Count > 0)
            {
                var message = "Negatives not allowed: ";
                message = String.Concat(message, String.Join(", ", numList));
                throw new ArgumentException(message);
            }
            return numList;
        }
        private static List<int> FindBigNumbers(List<int> numbersToAdd)
        {
            var bigNumbers = new List<int>();
            foreach (int num in numbersToAdd)
            {
                var smallNumber = IgnoreBigNumbers(num);
                bigNumbers.Add(smallNumber);
            }
            return bigNumbers;
        }

        private static int IgnoreBigNumbers(int num)
        {
            if (num >= 1000) num = 0;
            return num;
        }

        private static int AddNumbers(List<int> bigNumbers)
        {
            var sum = 0;
            foreach (int number in bigNumbers)
            {
                sum = sum + number; // TODO: why does this work?
            }
            return sum;
        } 
    }
}