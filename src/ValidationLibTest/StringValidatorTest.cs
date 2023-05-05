using ValidationLib;
using Xunit;

namespace ValidationLibTest
{
    public class StringValidatorTest
    {
        [Fact]
        public void IsValid_Null_ReturnedFalse()
        {
            string str = null;
            Assert.False(StringValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Empty_ReturnedFalse()
        {
            string str = "";
            Assert.False(StringValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Spaces_ReturnedFalse()
        {
            string str = "  ";
            Assert.False(StringValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Tab_ReturnedFalse()
        {
            string str = "\t";
            Assert.False(StringValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_A_ReturnedTrue()
        {
            string str = "A";
            Assert.True(StringValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_255Symbols_ReturnedTrue()
        {
            string str = "";
            for (int i = 0; i < 255; i++)
            {
                str += "0";
            }
            Assert.True(StringValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_256Symbols_ReturnedFalse()
        {
            string str = "";
            for (int i = 0; i < 256; i++)
            {
                str += "0";
            }
            Assert.False(StringValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_259Symbols_ReturnedFalse()
        {
            string str = "";
            for (int i = 0; i < 259; i++)
            {
                str += "0";
            }
            Assert.False(StringValidator.IsValid(str));
        }
    }
}