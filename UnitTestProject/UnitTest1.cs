using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibraryRentOfEquipment;

namespace UnitTestProject
{
    [TestClass]
    public class CalculationsTest
    {
        [TestMethod]
        public void CostOfRent_CorrectNumbers_ReturnTrue()
        {
            //Arrange
            int NumberOfdays = 11;
            decimal Cost = 1000;          
            decimal ex = 11000;
            //Act
            decimal result = Calculations.CostOfRent(NumberOfdays, Cost);
            //Assert
            Assert.AreEqual(ex, result);
        }

        [TestMethod]
        public void CostOfRent_MinusDate_ReturnFalse()
        {
            //Arrange
            int NumberOfdays = -11;
            decimal Cost = 1000;
            decimal ex = 10000;
            //Act
            decimal result = Calculations.CostOfRent(NumberOfdays, Cost);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }

        [TestMethod]
        public void DaysInRent_CorrectNumbers_ReturnTrue()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal ex = 11;
            //Act
            decimal result = Calculations.DaysInRent(DateOfBegin,DateOfEnd);
            //Assert           
            Assert.AreEqual(ex, result);
        }
        [TestMethod]
        public void DaysInRent_MinusDate_ReturnFalse()
        {
            //Arrange
            int DateOfBegin = 20;
            int DateOfEnd = 10;
            decimal ex = 11;
            //Act
            decimal result = Calculations.DaysInRent(DateOfBegin, DateOfEnd);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }
        [TestMethod]
        public void Discount_CorrectNumbers_ReturnTrue()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost=1000;
            int FactDateOfEnd=15;
            decimal ex = 5970;
            //Act
            decimal result = Calculations.Discount(DateOfBegin,DateOfEnd,Cost,FactDateOfEnd);
            //Assert           
            Assert.AreEqual(ex, result);
        }
        [TestMethod]
        public void Discount_NotDiscountButPenality_ReturnFalse()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost = 1000;
            int FactDateOfEnd = 25;
            decimal ex = 5970;
            //Act
            decimal result = Calculations.Discount(DateOfBegin, DateOfEnd, Cost, FactDateOfEnd);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }
        [TestMethod]
        public void Discount_NotDiscountNotPenality_ReturnFalse()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost = 1000;
            int FactDateOfEnd = 20;
            decimal ex = 5970;
            //Act
            decimal result = Calculations.Discount(DateOfBegin, DateOfEnd, Cost, FactDateOfEnd);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }
        [TestMethod]
        public void Penality_CorrectNumbers_ReturnTrue()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost = 1000;
            int FactDateOfEnd = 25;
            decimal ex = 15960;
            //Act
            decimal result = Calculations.Penality(DateOfBegin, DateOfEnd, Cost, FactDateOfEnd);
            //Assert           
            Assert.AreEqual(ex, result);
        }
        [TestMethod]
        public void Penality_NotPenalityButDiscount_ReturnFalse()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost = 1000;
            int FactDateOfEnd = 15;
            decimal ex = 15960;
            //Act
            decimal result = Calculations.Discount(DateOfBegin, DateOfEnd, Cost, FactDateOfEnd);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }
        [TestMethod]
        public void Penality_NotPenalityNotDiscount_ReturnFalse()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost = 1000;
            int FactDateOfEnd = 20;
            decimal ex = 15960;
            //Act
            decimal result = Calculations.Discount(DateOfBegin, DateOfEnd, Cost, FactDateOfEnd);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }
    }
}
