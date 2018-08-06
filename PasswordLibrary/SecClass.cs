using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.IO;
namespace PasswordLibrary
{
    public class SecClass
    {
        private SecClass()
        { }

        public static string GetPassword(string Location)
        {
            if (System.IO.File.Exists(Location))
            {
                try
                {
                    string FileContents = System.IO.File.ReadAllText(Location).Trim();
                    byte[] ba = System.Convert.FromBase64String(FileContents);
                    byte[] b = System.Security.Cryptography.ProtectedData.Unprotect(ba, null, System.Security.Cryptography.DataProtectionScope.LocalMachine);
                    string Password = System.Text.Encoding.Unicode.GetString(b);

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
