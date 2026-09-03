using Spectre.Console.Cli;

namespace Ocl2CSharp.CLI;
public class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandApp<ConvertCommand>();
        app.Configure(config =>
        {
            config.SetApplicationName("ocl2csharp");
            config.SetApplicationVersion("1.0.0");
            config.AddExample("--expression", "\"age >= 0\"");
            config.AddExample("--expression", "\"employees->select(e | e.active = true)->notEmpty()\"");
            config.AddExample("--file", "constraints.ocl");
            config.AddExample("--expression", "\"if age >= 18 then isAdult else isMinor endif\"", "--if-statement");
        });

        return app.Run(args);
    }
}