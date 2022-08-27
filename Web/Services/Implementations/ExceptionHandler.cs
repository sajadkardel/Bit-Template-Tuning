using System.Diagnostics;
using BTT.App.Shared;
using BTT.Shared.Marker;

namespace BTT.App.Services.Implementations;

public class ExceptionHandler : IExceptionHandler, IScopedDependency
{
    public void Handle(Exception exception, IDictionary<string, object?>? parameters = null)
    {
#if DEBUG
        MessageBox.Show(exception.ToString(), "Error");
        Console.WriteLine(exception.ToString());
        Debugger.Break();
#else
        if (exception is KnownException)
        {
            MessageBox.Show(exception.Message, "Error");
        }
        else
        {
            MessageBox.Show("Unknown error.", "Error");
        }
#endif

    }
}
