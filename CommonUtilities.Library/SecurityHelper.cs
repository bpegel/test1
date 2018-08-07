using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.Library
{
    public class SecurityHelper
    {
        public static string GetPassword(string FileLocation)
        {
            if (System.IO.File.Exists(FileLocation))
            {
                try
                {
                    string FileContents = System.IO.File.ReadAllText(FileLocation).Trim();
                    return GetPasswordFromEncryptedString(FileContents);
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static string GetPasswordFromEncryptedString(string EncryptedString)
        {
            if (!string.IsNullOrEmpty(EncryptedString))
            {
                try
                {
                    byte[] ba = System.Convert.FromBase64String(EncryptedString);
                    byte[] b = System.Security.Cryptography.ProtectedData.Unprotect(ba, null, System.Security.Cryptography.DataProtectionScope.LocalMachine);
                    string Password = Encoding.Unicode.GetString(b);

                    return Password;
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static string GetEncryptedStringForPassword(string PlainPassword)
        {
            if (!string.IsNullOrEmpty(PlainPassword))
            {
                try
                {
                    byte[] ba = Encoding.Unicode.GetBytes(PlainPassword);
                    byte[] b = System.Security.Cryptography.ProtectedData.Protect(ba, null, System.Security.Cryptography.DataProtectionScope.LocalMachine);
                    string Password = System.Convert.ToBase64String(b);

                    return Password;
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
