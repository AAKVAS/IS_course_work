using PasswordEvaluatorLib;
using Xunit;

namespace PasswordEvaluatorLibTest
{
    public class PasswordEvaluatorTest
    {
        [Fact]
        public void EvaluatePassword_0_ReturnedWeak()
        {
            string password = "0";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_z_ReturnedWeak()
        {
            string password = "z";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_Space_ReturnedWeak()
        {
            string password = " ";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_A_ReturnedWeak()
        {
            string password = "A";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_0ZaPlus_ReturnedWeak()
        {
            string password = "0Za+";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_012AbcPlus_ReturnedWeak()
        {
            string password = "01Abc+";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_012AbcPlusMinus_ReturnedStrong()
        {
            string password = "012Abc+-";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Strong;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_012345678_ReturnedWeak()
        {
            string password = "012345678";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_A01234567_ReturnedWeak()
        {
            string password = "A01234567";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_Az0123456_ReturnedMedium()
        {
            string password = "Az0123456";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Medium;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_ְ0123456789_ReturnedWeak()
        {
            string password = "ְ0123456789";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_ְÿ0123456_ReturnedMedium()
        {
            string password = "ְÿ0123456";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Medium;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_ֿאנמכü0123456Plus_ReturnedStrong()
        {
            string password = "ֿאנמכü0123456+";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Strong;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_012345678901234_ReturnedWeak()
        {
            string password = "012345678901234";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Weak;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }


        [Fact]
        public void EvaluatePassword_A012345678901234_ReturnedMedium()
        {
            string password = "A012345678901234";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Medium;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_Aa012345678901234_ReturnedStrong()
        {
            string password = "Aa012345678901234";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Strong;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }

        [Fact]
        public void EvaluatePassword_AbPlus012345678901234_ReturnedStrong()
        {
            string password = "Ab+012345678901234";
            PasswordEvaluator passwordEvaluator = new PasswordEvaluator();

            PasswordComplexity excpeted = PasswordComplexity.Strong;
            PasswordComplexity actual = passwordEvaluator.EvaluatePassword(password);
            Assert.Equal(excpeted, actual);
        }
    }
}