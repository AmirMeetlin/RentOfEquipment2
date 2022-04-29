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
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost = 1000;          
            decimal ex = 11000;
            //Act
            decimal result = Calculations.CostOfRent(DateOfBegin, DateOfEnd, Cost);
            //Assert
            Assert.AreEqual(ex, result);
        }

        [TestMethod]
        public void CostOfRent_WithoutToday_ReturnFalse()
        {
            //Arrange
            int DateOfBegin = 10;
            int DateOfEnd = 20;
            decimal Cost = 1000;
            decimal ex = 10000;
            //Act
            decimal result = Calculations.CostOfRent(DateOfBegin, DateOfEnd, Cost);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }
        [TestMethod]
        public void CostOfRent_MinusDate_ReturnFalse()
        {
            //Arrange
            int DateOfBegin = 20;
            int DateOfEnd = 10;
            decimal Cost = 1000;
            decimal ex = 10000;
            //Act
            decimal result = Calculations.CostOfRent(DateOfBegin, DateOfEnd, Cost);
            //Assert           
            Assert.AreNotEqual(ex, result);
        }
        
    }
}
