using System;
using System.Collections.Generic;

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
                var numbers = integers.Split(',', '\n', ';', '*');
                var numList = new List<int>();
                var sum = FindNumbers(numbers, numList);
                ThrowsException(numList);
                return sum;
            }
            return 0;
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

        private static int FindNumbers(string[] numbers, List<int> numList)
        {
            var sum = 0;
            List<int> numbersToAdd = GetNumbers(numbers);
            List<int> bigNumbers = FindBigNumbers(numbersToAdd);
            FindNegativeNumbers(numList, numbersToAdd);
            sum = AddNumbers(sum, bigNumbers);
            return sum;
        }

        private static int AddNumbers(int sum, List<int> bigNumbers)
        {
            foreach (int number in bigNumbers)
            {
                sum = sum + number;
            }
            return sum;
        }

        private static void FindNegativeNumbers(List<int> numList, List<int> numbersToAdd)
        {
            foreach (int num in numbersToAdd)
            {
                if (num < 0) numList.Add(num);
            }
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

        private static int IgnoreBigNumbers(int num)
        {
            if (num >= 1000) num = 0;
            return num;
        }
    }
}