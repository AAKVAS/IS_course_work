using ValidationLib;
using Xunit;

namespace ValidationLibTest
{
    public class PhoneNumberValidatorTest
    {
        [Fact]
        public void IsValid_Null_ReturnedFalse()
        {
            string str = null;
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Empty_ReturnedFalse()
        {
            string str = "";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Space_ReturnedFalse()
        {
            string str = " ";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Tab_ReturnedFalse()
        {
            string str = "\t";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_01_ReturnedTrue()
        {
            string str = "01";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_1_ReturnedTrue()
        {
            string str = "1";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8_800_555_35_35_ReturnedTrue()
        {
            string str = "8-800-555-35-35";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8p800p555_35_35_ReturnedTrue()
        {
            string str = "8(800)555-35-35";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8sp800sp555_35_35_ReturnedTrue()
        {
            string str = "8 (800) 555-35-35";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8s800sp555_35_35_ReturnedFalse()
        {
            string str = "8 800) 555-35-35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8sp800s555_35_35_ReturnedFalse()
        {
            string str = "8 (800 555-35-35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8sp800sp555_p35p_35_ReturnedFalse()
        {
            string str = "8 (800) 555-(35)-35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8sp800sp555_35s_s35_ReturnedFalse()
        {
            string str = "8 (800) 555-35 -35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8800555_35_35_ReturnedTrue()
        {
            string str = "8800555-35-35";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8sp800sp555__35_35_ReturnedFalse()
        {
            string str = "8 (800) 555--35-35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8sp800sp555а_35_35_ReturnedFalse()
        {
            string str = "8 (800) 555а-35-35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_8800_555s35_35_ReturnedTrue()
        {
            string str = "8 800-555 35-35";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Asterisk105sharp_ReturnedFalse()
        {
            string str = "*105#";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_255symbols_ReturnedTrue()
        {
            string str = "";
            for (int i = 0; i < 255; i++)
            {
                str += "8";
            }
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_256symbols_ReturnedFalse()
        {
            string str = "";
            for (int i = 0; i < 256; i++)
            {
                str += "8";
            }
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Plus7s800s555s35s35_ReturnedTrue()
        {
            string str = "+7 800 555 35 35";
            Assert.True(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_PlusPlus7s800s555s35s35_ReturnedFalse()
        {
            string str = "++7 800 555 35 35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_Plus7s800Plus555s35s35_ReturnedFalse()
        {
            string str = "+7 800+555 35 35";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }

        [Fact]
        public void IsValid_PhoneNumber_ReturnedFalse()
        {
            string str = "PhoneNumber";
            Assert.False(PhoneNumberValidator.IsValid(str));
        }
    }
}
