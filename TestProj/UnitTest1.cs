using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using app1;

namespace app1.Tests
{
    [TestClass]
    public class PressureParserTests
    {
        [TestMethod]
        public void NullInput()
        {
            string input = null;
            Pressure result = PressureParser.ParseVariousPressure(input);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void TestFormatException_StrToDouble()
        {
            string input = "2025.08.08 высота значение 50000 Манометр";
            Pressure result = PressureParser.ParseVariousPressure(input);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void TestFormatException_StrToInt()
        {
            string input = "2025.08.08 высота 100.5 пятьдесят Манометр";
            Pressure result = PressureParser.ParseVariousPressure(input);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void InvalidPressureValue()
        {
            string input = "2023.02.28 1020.75 invalid устрво";
            Pressure result = PressureParser.ParseVariousPressure(input);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestFormatException_IncorrectDate()
        {
            string input = "2023.13.45 1020.75 100000 устрво";
            Pressure result = PressureParser.ParseVariousPressure(input);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReturnCorrectArray()
        {
            string input = "2023.02.28 1020.75 100000 устрво";
            string[] result = PressureParser.SplitText(input);

            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("2023.02.28", result[0]);
            Assert.AreEqual("1020.75", result[1]);
            Assert.AreEqual("100000", result[2]);
            Assert.AreEqual("устрво", result[3]);
        }

        [TestMethod]
        public void ReturnCorrectPressureObject()
        {
            string input = "2023.02.28 1020,75 100000 устрво";
            Pressure result = PressureParser.ParseVariousPressure(input);

            Assert.AreEqual("2023.02.28", result.Date.ToString("yyyy.MM.dd"));
            Assert.AreEqual(1020, 75, result.Height);
            Assert.AreEqual(100000, result.Value);
            Assert.AreEqual("устрво", result.Device);
        }

        [TestMethod]
        public void WrongPartsCount()
        {
            string input = "2023.02.28 1020,75 100000";
            Pressure result = PressureParser.ParseVariousPressure(input);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EmptyInput_ReturnsNull()
        {
            string input = "";
            Pressure result = PressureParser.ParseVariousPressure(input);
            Assert.IsNull(result);
        }

    }
}