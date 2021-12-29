using CSB.Core.Entities.Responses;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSB.Core.Services
{
    internal class ValidationService : IValidationService
    {
        public ServiceResponse ValidateEmailAddress(string eMailAddress)
        {
            if (string.IsNullOrWhiteSpace(eMailAddress))
                return ServiceResponse.Fail("E-posta adresi boş olamaz");

            Regex regexEMailAddress = new Regex(
                    @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                    RegexOptions.CultureInvariant | RegexOptions.Singleline);
            if (!regexEMailAddress.IsMatch(eMailAddress))
                return ServiceResponse.Fail("E-posta adresinin formatı uygun değildir");

            return ServiceResponse.Success();
        }

        public ServiceResponse ValidatePhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                return ServiceResponse.Fail("Telefon numarası adresi boş olamaz");

            Regex regexPhoneNumber = new Regex(@"^(0(\d{3})(\d{3})(\d{2})(\d{2}))$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            if (!regexPhoneNumber.IsMatch(phoneNumber))
                return ServiceResponse.Fail("Telefon numarasının formatı uygun değildir");

            return ServiceResponse.Success();
        }

        public ServiceResponse ValidateTRIdentityNumber(string identityNumber)
        {
            if (string.IsNullOrWhiteSpace(identityNumber))
                return ServiceResponse.Fail("T.C. kimlik numarası boş olamaz.");

            identityNumber = identityNumber.Trim();

            if (identityNumber.Length == 11 && !identityNumber.StartsWith("0"))
            {
                if (identityNumber.All(digit => char.IsDigit(digit)))
                {
                    int oddSum = 0;
                    int evenSum = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        if ((i + 1) % 2 == 0)
                        {
                            evenSum += int.Parse(identityNumber[i].ToString(), CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            oddSum += int.Parse(identityNumber[i].ToString(), CultureInfo.InvariantCulture);
                        }
                    }

                    int tenthDigit = ((oddSum * 7) - evenSum) % 10;
                    if (identityNumber.Substring(9, 1) == tenthDigit.ToString(CultureInfo.InvariantCulture))
                    {
                        int eleventhDigit = identityNumber.Substring(0, 10).Sum(digit => int.Parse(digit.ToString(), CultureInfo.InvariantCulture)) % 10;
                        if (identityNumber.Substring(10, 1) == eleventhDigit.ToString(CultureInfo.InvariantCulture))
                        {
                            return ServiceResponse.Success();
                        }
                    }
                }
            }

            return ServiceResponse.Fail("T.C. kimlik numarası geçerli değildir");
        }
    }
}