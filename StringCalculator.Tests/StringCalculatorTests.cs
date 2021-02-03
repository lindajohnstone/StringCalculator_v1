using System;
using System.Collections.Generic;
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
        public void Support_Single_Character_Delimiter()
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
        public void Handle_Multiple_Delimiters_With_Length_Longer_Than_One_Character() // TODO: fails
        {
            // arrange
            var calculate = new Calculator();
            // act
            var result = calculate.Add("//[***][#][%]\n1***2#3%4");
            // assert
            Assert.Equal(10, result);
        }
        
        [Fact]
        public void Handle_Delimiters_That_Have_Numbers_As_Part_Of_Them_Number_Cannot_Be_On_Edge() // TODO: fails
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
