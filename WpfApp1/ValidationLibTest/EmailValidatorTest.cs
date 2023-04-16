using ValidationLib;
using Xunit;

namespace ValidationLibTest
{
    public class EmailValidatorTest
    {
        [Fact]
        public void IsValid_Null_ReturnedFalse()
        {
            string str = null;
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_Empty_ReturnedFalse()
        {
            string str = "";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_Spaces_ReturnedFalse()
        {
            string str = "  ";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_Tab_ReturnedFalse()
        {
            string str = "\t";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_B_ReturnedFalse()
        {
            string str = "B";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_BAt_ReturnedFalse()
        {
            string str = "B@";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_BAtB_ReturnedFalse()
        {
            string str = "B@B";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_BAtBPoint_ReturnedFalse()
        {
            string str = "B@.";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_BAtBPointru_ReturnedFalse()
        {
            string str = "B@B.ru";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_bAtbPointru_ReturnedTrue()
        {
            string str = "b@b.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_normalAtmailPointru_ReturnedTrue()
        {
            string str = "normal@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_normal_AtmailPointru_ReturnedTrue()
        {
            string str = "normal_@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_normalPlusAtmailPointru_ReturnedTrue()
        {
            string str = "normal+@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_normalSharpAstriskAt999mailPointru_ReturnedTrue()
        {
            string str = "normal#*@mail999.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_normalAmpersandApostropheExclamationAtmailPointru_ReturnedTrue()
        {
            string str = "normal&'!@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_aDashSlashEqualsQuestionMarkCircumflexusAAtmailPointru_ReturnedTrue()
        {
            string str = "a-/=?^@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_aSpaceAtmailPointru_ReturnedFalse()
        {
            string str = "a @mail.ru";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_BracePipeBraceTildeAtmailPointru_ReturnedTrue()
        {
            string str = "{|}~@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_aaPointaaAtmailPointru_ReturnedTrue()
        {
            string str = "aa.aa@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_aPointaPointaPointaAtmailPointru_ReturnedTrue()
        {
            string str = "a.a.a.a@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_PointaaPointaaAtmailPointru_ReturnedFalse()
        {
            string str = ".aa.aa@mail.ru";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_aaPointaaPointAtmailPointru_ReturnedFalse()
        {
            string str = "aa.aa.@mail.ru";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_aaPointPointaaAtmailPointru_ReturnedFalse()
        {
            string str = "aa..aa@mail.ru";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_normal777AtmailPointru_ReturnedTrue()
        {
            string str = "normal777@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_31AtmailPointru_ReturnedTrue()
        {
            string str = "31@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_AtmailPointru_ReturnedFalse()
        {
            string str = "@mail.ru";
            Assert.False(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_255Symbols_ReturnedTrue()
        {
            string str = "";
            for (int i = 0; i < 247; i++)
            {
                str += "o";
            }
            str += "@mail.ru";
            Assert.True(EmailValidatior.IsValid(str));
        }

        [Fact]
        public void IsValid_256Symbols_ReturnedFalse()
        {
            string str = "";
            for (int i = 0; i < 248; i++)
            {
                str += "o";
            }
            str += "@mail.ru";
            Assert.False(EmailValidatior.IsValid(str));
        }
    }
}
