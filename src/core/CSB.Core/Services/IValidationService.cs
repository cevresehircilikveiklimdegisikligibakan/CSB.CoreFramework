using CSB.Core.Entities.Responses;

namespace CSB.Core.Services
{
    public interface IValidationService
    {
        ServiceResponse ValidateTRIdentityNumber(string identityNumber);

        ServiceResponse ValidateEmailAddress(string eMailAddress);
        ServiceResponse ValidatePhoneNumber(string phoneNumber);
    }
}