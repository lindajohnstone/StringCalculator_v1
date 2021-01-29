using System;

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
                var numbers = v.Split(',', '\n');
                foreach (string number in numbers)
                {
                    Int32.TryParse(number, out var num);
                    sum = sum + num;
                }
                return sum;
            }
            return 0;
        }
    }
}