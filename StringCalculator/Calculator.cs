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
                var sum = 0;
                var numbers = integers.Split(',', '\n', ';', '*');
                var numList = new List<int>();
                sum = FindNumbers(sum, numbers, numList);
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

        private static int FindNumbers(int sum, string[] numbers, List<int> numList)
        {
            foreach (string number in numbers)
            {
                Int32.TryParse(number, out var num);
                num = IgnoreBigNumbers(num);
                AddNegativeNumbersToList(numList, num);
                sum = sum + num;
            }
            return sum;
        }

        private static void AddNegativeNumbersToList(List<int> numList, int num)
        {
            if (num < 0) numList.Add(num);
        }

        private static int IgnoreBigNumbers(int num)
        {
            if (num >= 1000) num = 0;
            return num;
        }
    }
}