using System.Security.Cryptography;
using System.Text;

namespace UATP.RapidPay.Shared
{
// TODO this class should be updated with newer cryptography standars 
    public static class Encrypter
    {
        #region Settings
        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";

        #endregion
        public static string Encrypt(
            string value, 
            string password, 
            string salt = "aseweias38490a76", 
            string vector = "8947az44a5wlkwyt")
        {
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            return Encrypt<AesManaged>(value, password, salt, vector);
#pragma warning restore SYSLIB0021 // Type or member is obsolete
        }

        public static string Encrypt<T>(string value, string password,
                string salt = "aseweias38490a76",
                string vector = "8947az44a5wlkwyt")
                where T : SymmetricAlgorithm, new()
        {

            byte[] vectorBytes = Encoding.ASCII.GetBytes(vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }
    }
}
