using System;
using System.Threading.Tasks;

namespace Presently.MobileApp.Utilities.Abstractions
{
    public interface IRequestExceptionHandler
    {
        Task HandlerRequestTaskAsync(Func<Task> task);
        Task<TResponse> HandlerRequestTaskAsync<TResponse>(Func<Task<TResponse>> task);
    }
}
