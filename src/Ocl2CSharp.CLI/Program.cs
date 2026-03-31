using System.ComponentModel;
using Ocl2CSharp;
using Spectre.Console;
using Spectre.Console.Cli;

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

/// <summary>
/// CLI command that converts OCL constraints to C# code.
/// </summary>
internal sealed class ConvertCommand : Command<ConvertCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("OCL expression string to convert.")]
        [CommandOption("-e|--expression")]
        public string? Expression { get; init; }

        [Description("Path to a file containing OCL expressions to convert.")]
        [CommandOption("-f|--file")]
        public string? FilePath { get; init; }

        [Description("Output file path. If omitted, output is written to stdout.")]
        [CommandOption("-o|--output")]
        public string? OutputPath { get; init; }

        [Description("Emit OCL if…then…else…endif as a multiline C# if/else statement instead of a ternary ?: expression.")]
        [CommandOption("--if-statement")]
        public bool UseIfStatement { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(settings.Expression) && string.IsNullOrWhiteSpace(settings.FilePath))
        {
            AnsiConsole.MarkupLine("[red]Error:[/] Provide --expression or --file.");
            return 1;
        }

        cancellationToken.ThrowIfCancellationRequested();

        string oclCode;
        if (!string.IsNullOrWhiteSpace(settings.FilePath))
        {
            if (!File.Exists(settings.FilePath))
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] File not found: {settings.FilePath}");
                return 1;
            }
            oclCode = File.ReadAllText(settings.FilePath);
        }
        else
        {
            oclCode = settings.Expression!;
        }

        cancellationToken.ThrowIfCancellationRequested();

        string csharpCode;
        try
        {
            csharpCode = OclToCSharpConverter.Convert(oclCode, settings.UseIfStatement);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Conversion error:[/] {Markup.Escape(ex.Message)}");
            return 2;
        }

        cancellationToken.ThrowIfCancellationRequested();

        if (!string.IsNullOrWhiteSpace(settings.OutputPath))
        {
            File.WriteAllText(settings.OutputPath, csharpCode);
            AnsiConsole.MarkupLine($"[green]Output written to:[/] {settings.OutputPath}");
        }
        else
        {
            AnsiConsole.WriteLine(csharpCode);
        }

        return 0;
    }
}
