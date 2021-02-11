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
        public void Step_1_Returns_Zero_If_Parameter_Is_Empty_String()
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
        public void Step_2_A_Single_Number_Returns_That_Number(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("3,5", 8)]
        public void Step_3_Two_Numbers_Return_The_Sum_Of_The_Numbers(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("3,5,3,9", 20)]
        public void Step_4_Any_Amount_Of_Numbers_Returns_The_Sum_Of_Those_Numbers(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Theory]
        [InlineData("1,2\n3", 6)]
        [InlineData("3\n5\n3,9", 20)]
        public void Step_5_New_Line_Breaks_And_Commas_Should_Be_Interchangeable_Between_Numbers(string actual, int expected)
        {
            Assert.Equal(expected, new Calculator().Add(actual));
        }
        [Fact]
        public void Step_5_Delimiter_And_New_Line_Interchangeable_Between_Numbers()
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
        public void Step_6_Custom_Delimiter_Begins_DoubleSlashes_Ends_NewLine()
        {
            Assert.Equal(3, new Calculator().Add("//;\n1;2"));
        }
        [Fact]
        public void Step_7_Calling_Add_With_A_Negative_Number_Will_Throw_An_Exception()
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = Assert.Throws<ArgumentException>(() => calculate.Add("-1,2,-3"));
            // assert
            Assert.Equal("Negatives not allowed: -1, -3", result.Message);
        }
        
        [Fact]
        public void Step_8_Numbers_Greater_Or_Equal_To_1000_Should_Be_Ignored()
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("1000,1001,2");
            // assert
            Assert.Equal(2, result);
        }
        [Fact]
        public void Step_9_Delimiters_Can_Be_Of_Any_Length()
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[***]\n1***2***3");
            // assert
            Assert.Equal(6, result);
        }
        [Theory]
        [InlineData("//[*][%]\n1*2%3", 6)]
       // [InlineData("//[*][%][$]\n1*2%3$4", 10)]
        public void Step_10_Allow_Multiple_Delimiters(string input, int expected)
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add(input);
            // assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Step_10_More_Than_One_Regex()
        {
            var input = "//[*][%]\n1*2%3";
            var newPattern = "\\[(.)\\]";
            Regex newRegex = new Regex(newPattern);
            var delimiterList = new List<string>();
            // act
            Match match = Regex.Match(input, newPattern);
            while (match.Success)
            {
                delimiterList.Add(match.Groups[1].Value);
                match = match.NextMatch();
            }
            // assert
            Assert.Equal("*", delimiterList[0]);
            Assert.Equal("%", delimiterList[1]);
        }
        
        [Fact]
        public void Step_11_Handle_Multiple_Delimiters_With_Length_Longer_Than_One_Character() 
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[***][#][%]\n1***2#3%4");
            // assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void Step_11_Regex_Matches_Multiple_Number_Of_Same_Delimiter()
        {
            // arrange
            var input = "//[***]\n1***2***3";
            var pattern = "//[(.+)]\n";
            Regex regex = new Regex(pattern);
            var delimiter = "";
            // act
            Match match = regex.Match(input);
            if (match.Success)
            {
                delimiter = match.Groups[1].Value;
            }
            // assert
            Assert.Equal("***", delimiter);
        }
        
        [Fact]
        public void Step_12_Handle_Delimiters_That_Have_Numbers_As_Part_Of_Them_Number_Cannot_Be_On_Edge() 
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[*1*][%]\n1*1*2%3");
            // assert
            Assert.Equal(6, result);
        } 
    }
}
