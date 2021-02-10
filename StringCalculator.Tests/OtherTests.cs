using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace StringCalculator.Tests
{
    public class OtherTests
    {
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
            var expected = new[] { "", ",", "\n3" };
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
            Assert.Equal(1, matchedDelimiters.Count);
            Assert.Equal(1, delimiterList.Count);
            Assert.Equal("1,2", delimiterList[0]);
        }
        [Fact]
        public void Regex_Match_Returns_False_When_No_Match()
        {
            // arrange
            var input = "1,2,3";
            var pattern = @"\/\/\[(.+){1,3}\]\n";
            Regex regex = new Regex(pattern);
            var delimiter = "";
            // act
            Match match = regex.Match(input);
            if (match.Success)
            {
                delimiter = match.Groups[1].Value;
            }
            // assert
            Assert.Equal(false, match.Success);
        }
        [Fact]
        public void String_Tests()
        {
            string s1 = "He said, \"This is the last \u0063hance\x0021\"";
            string s2 = @"He said, ""This is the last \u0063hance\x0021""";
            Assert.Equal("He said, \"This is the last chance!\"", s1);
            //Assert.Equal("He said, \"This is the last \\u0063hance\\x0021", s2);
            Assert.Equal(@"Nancy said Hello World! \ smiled.", "Nancy said Hello World! \\ smiled.");
            Assert.Equal(@"c:\documents\files\u0066.txt", "c:\\documents\\files\\u0066.txt");
        }
        [Fact]
        public void More_String_Tests()
        {
            // arrange
            string regularStringLiteral = "String literal 4\r\nAnother line";
            string verbatimStringLiteral = @"String literal 4
Another line";
            // act

            // assert
            Assert.Equal(verbatimStringLiteral, regularStringLiteral);
            Assert.Equal(@"
", "\r\n");
            Assert.Equal("", String.Empty);
        }
        [Fact]
        public void String_Substrings()
        {
            // arrange
            string s3 = "Visual C# Express";
            // act

            // assert
            Assert.Equal("C#", s3.Substring(7, 2));
            Assert.Equal("Visual Basic Express", s3.Replace("C#", "Basic"));
            Assert.Equal(7, s3.IndexOf("C"));
        }
        [Fact]
        public void String_Interpolation()
        {
            // arrange
            var jh = (firstName: "Jupiter", lastName: "Hammon", born: 1711, published: 1761);
            var name = "Linda";

            // act

            // assert
            Assert.Equal("Jupiter Hammon was an African American poet born in 1711.",
                $"{jh.firstName} {jh.lastName} was an African American poet born in {jh.born}.");
            Assert.Equal("He was first published in 1761 at the age of 50.",
                $"He was first published in {jh.published} at the age of {jh.published - jh.born}.");
            Assert.Equal("He'd be over 300 years old today.",
                $"He'd be over {Math.Round((2018d - jh.born) / 100d) * 100d} years old today.");
            Assert.Equal("Linda", $"{name}");
        }
        [Fact]
        public void Regex_Matches_Delimiter()
        {
            // arrange
            var pattern = "\\[(.)\\]";
            Regex regex = new Regex(pattern);
            var input = "[*][%]";
            var delimiterList = new List<string>();
            // act
            foreach (Match m in regex.Matches(input))
            {
                delimiterList.Add(m.Groups[1].Value); 
            }
            // assert
            Assert.Equal("*", delimiterList[0]); 
        }
        [Fact]
        public void testName()
        {
            // arrange
            string pattern = @"\b\w+es\b";
            Regex rgx = new Regex(pattern);
            string sentence = "Who writes these notes?";
            var newList = new List<string>();
            // act
            foreach (Match match in rgx.Matches(sentence))
            {
                newList.Add(match.Groups[0].Value);
            }
            // assert
            Assert.Equal("writes", newList[0]);
        }

    }
}