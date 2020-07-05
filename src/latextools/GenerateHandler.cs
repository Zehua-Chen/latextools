using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace latextools
{
    public class GenerateHandler : ICommandHandler
    {
        public async Task<int> InvokeAsync(InvocationContext context)
        {
            return await Task.Run(() => 0);
        }
    }
}
