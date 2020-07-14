using System;
using System.CommandLine.Parsing;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace latextools
{
    public class NewHandler : ICommandHandler
    {
        public async Task<int> InvokeAsync(InvocationContext context)
        {
            ParseResult result = context.ParseResult;

            string? name = (string?)result.ValueForOption("--name");

            if (name == null)
            {
                Console.WriteLine("Get null");
            }
            else
            {
                Console.WriteLine(name);
            }


            return 0;
        }
    }
}
