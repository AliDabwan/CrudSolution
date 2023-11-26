using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTest
{
    public class CalculationTest
    {
        [Fact]
        public void AddTest()
        {
            //Aarrange
            int input1=3,input2=4;
            int expected = 7;

            //Act
            int actual=Calculation.ADD(input1,input2);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
