using System;
using System.Collections.Generic;

namespace StringCalculator
{
    public class Calculator
    {
        public Calculator()
        {
        }

        public int Add(string v)
        {
            if (v != " ") 
            {
                var sum = 0;
                var numbers = v.Split(',', '\n', ';');
                var numList = new List<int>();
                foreach (string number in numbers)
                {
                    Int32.TryParse(number, out var num);
                    if (num < 0) numList.Add(num);
                    sum = sum + num;
                }
                if (numList.Count > 0)
                {
                    var message = "Negatives not allowed: ";
                    message = String.Concat(message, String.Join(", ", numList));
                    throw new ArgumentException(message);
                }
                return sum;
            }
            return 0;
        }
    }
}