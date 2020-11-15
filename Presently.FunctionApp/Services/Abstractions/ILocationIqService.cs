namespace Presently.FunctionApp.Services.Abstractions
{
    public interface ILocationIqService
    {
        string GetFormattedAddress(double latitude, double longitude);
    }
}