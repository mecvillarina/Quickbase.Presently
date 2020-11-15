namespace Presently.FunctionApp.Services.Abstractions
{
    public interface IAuthService
    {
        long? Login(string employeeId, string accessCode);
    }
}