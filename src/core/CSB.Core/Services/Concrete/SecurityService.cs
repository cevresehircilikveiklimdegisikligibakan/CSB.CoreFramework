using CSB.Core.Entities.Responses;
using System.Text;

namespace CSB.Core.Services
{
    internal class SecurityService : ISecurityService
    {
        public ServiceResponse<string> ConvertToMd5(string text)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] textBytes = System.Text.Encoding.ASCII.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(textBytes);

                StringBuilder hashedValue = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashedValue.Append(hashBytes[i].ToString("X2"));
                }
                return ServiceResponse<string>.Success(hashedValue.ToString());
            }
        }
    }
}