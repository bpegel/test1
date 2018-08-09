using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtilities.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.Library.Tests
{
    [TestClass()]
    public class SecurityHelperTests
    {
        [TestMethod()]
        public void ValidateEncryptionAndDecryptionForPasswordTest()
        {
            // Test to validte both encryption and decryption of password
            string myPassword = "Dummy45Password";
            string encryptedPassword = SecurityHelper.GetEncryptedStringForPassword(myPassword);
            string decryptedPassword = SecurityHelper.GetPasswordFromEncryptedString(encryptedPassword);

            Assert.AreEqual(myPassword, decryptedPassword);
        }
    }
}