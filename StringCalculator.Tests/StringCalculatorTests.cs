using System;
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
        
    }
}
