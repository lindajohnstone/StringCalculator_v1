using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        [Fact]
        public void Returns_Zero_If_Parameter_Is_Empty_String()
        {
            // arrange
            // var calculate = new Calculator();
            //var expected = 0;
            // act
            //var result = calculate.Add("");
            // assert
            //Assert.Equal(expected, result);
            Assert.Equal(0, new Calculator().Add(""));
        }
        [Theory]
        [InlineData("1", 1)]
        [InlineData("3", 3)]
        public void A_Single_Number_Returns_That_Number(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("3,5", 8)]
        public void Two_Numbers_Return_The_Sum_Of_The_Numbers(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("3,5,3,9", 20)]
        public void Any_Amount_Of_Numbers_Returns_The_Sum_Of_Those_Numbers(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Theory]
        [InlineData("1,2\n3", 6)]
        [InlineData("3\n5\n3,9", 20)]
        public void New_Line_Breaks_And_Commas_Should_Be_Interchangeable_Between_Numbers(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Fact]
        public void Delimiter_And_New_Line_Interchangeable_Between_Numbers()
        {
            // arrange
            var calculate = new Calculator();
            var input = "1,2\n3";
            var regex = new Regex("\\d(.)+\\d");
            var delimiter = " ";
            Match match = regex.Match(input);
            if (match.Success)
            {
                delimiter = match.Groups[1].Value;
            }
            string value = Regex.Replace(input, "\n", delimiter);
            var expected = new[] { "1", "2", "3" };
            // act
            var result = value.Split(delimiter);
            // assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Custom_Delimiter_Begins_DoubleSlashes_Ends_NewLine()
        {
            Assert.Equal(3, new Calculator().Add("//;\n1;2"));
        }
        [Fact]
        public void Calling_Add_With_A_Negative_Number_Will_Throw_An_Exception()
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = Assert.Throws<ArgumentException>(() => calculate.Add("-1,2,-3"));
            // assert
            Assert.Equal("Negatives not allowed: -1, -3", result.Message);
        }
        [Fact]
        public void String_Methods()
        {
            var hello = "Hello";
            var world = "World";
            Assert.Equal("Hello World", ($"{hello} {world}"));
            Assert.Equal("Hello World", String.Concat(hello, " ", world));
            Assert.Equal("Hello World", String.Join(" ", new List<string>() { hello, world }));
        }
        [Fact]
        public void Numbers_Greater_Or_Equal_To_1000_Should_Be_Ignored()
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("1000,1001,2");
            // assert
            Assert.Equal(2, result);
        }
        [Fact]
        public void Delimiters_Can_Be_Of_Any_Length()
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[***]\n1***2***3");
            // assert
            Assert.Equal(6, result);
        }
        [Fact]
        public void Regex_Split()
        {
            // arrange
            var text = "1 One, 2 Two, 3 Three is good.";
            var values = Regex.Split(text, @"\D+");
            var newList = new List<string>();
            foreach (string value in values)
            {
                if (value != string.Empty) newList.Add(value);// removes empty string in array
            }
            // assert
            Assert.Equal(new string[] { "1", "2", "3" }, newList);
        }
        [Fact]
        public void Regex_Replace()
        {
            // arrange
            var text = "1 One, 2 Two, 3 Three is good.";
            string value = Regex.Replace(text, @"\D+", " ");
            // assert
            Assert.Equal(("1 2 3"), value.Trim(' '));// removes whitespace at start and end of string
        }
        [Fact]
        public void Allow_Multiple_Delimiters()
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[*][%]\n1*2%3");
            // assert
            Assert.Equal(6, result);
        }
        
        [Fact]
        public void Handle_Multiple_Delimiters_With_Length_Longer_Than_One_Character() 
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[***][#][%]\n1***2#3%4");
            // assert
            Assert.Equal(10, result);
        }
        
        [Fact]
        public void Handle_Delimiters_That_Have_Numbers_As_Part_Of_Them_Number_Cannot_Be_On_Edge() 
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[*1*][%]\n1*1*2%3");
            // assert
            Assert.Equal(6, result);
        } 
        [Fact]
        public void Regex_Patterns()
        {
            // arrange
            string input = "//;\n1;2";
            string pattern = @"//(.)+\n";
            string[] actual = Regex.Split(input, pattern);
            var expected = new[] { "", ";", "1;2" };
            // assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Regex_Number_With_Delimiter()
        {
            // arrange
            string input = "1,2\n3";
            string pattern = "\\d(.)+\\d";
            string[] actual = Regex.Split(input, pattern);
            var expected = new[] { "", ",", "\n3" };// does not pick up \n on its' own
            // assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Regex_Group_Dot_Value()
        {
            // arrange
            var input = "//;\n1;2";
            var pattern = @"//(.+)\n";
            Regex regex = new Regex(pattern);
            // act
            Match match = regex.Match(input);
            // assert
            Assert.Equal(";", match.Groups[1].Value);
        }
        [Fact]
        public void Regex_Matches()
        {
            // arrange
            var input = "1,2\n3";
            var pattern = "\\d(.)+\\d";
            Regex regex = new Regex(pattern);
            var delimiterList = new List<string>();
            // act
            MatchCollection matchedDelimiters = regex.Matches(input);
            Match matches = regex.Match(input);
            if (matches.Success)
            {
                foreach (Match m in regex.Matches(input))
                {
                    delimiterList.Add(m.Value);
                }
            }
            // assert
            //Assert.Equal(2, matchedDelimiters.Count); // fails - count is 1 - \n is not a match
            Assert.Equal(2, delimiterList.Count); //fails - as above
        }
        [Fact]
        public void Regex_Replace_Find_Delimiter_Count()
        {
            // arrange
            var input = "1,2\n3";
            var pattern = "\\d(.)+\\d";
            Regex regex = new Regex(pattern);
            List<string> delimiterList = new List<string>();
            // act
            Match matches = regex.Match(input);
            if (matches.Success)
            {
                foreach (Match m in regex.Matches(input, matches.Index + matches.Length))
                {
                    string delimiterString = Regex.Replace(input, "\\d", " ");
                    delimiterList.Add(delimiterString);
                }
            }
            // assert
            Assert.Equal(2, delimiterList.Count); //fails - count is 0
        }
    }
}
