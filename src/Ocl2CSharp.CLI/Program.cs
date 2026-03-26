using Ocl2CSharp;

const string UsageText = """
Usage:
  ocl2csharp [options] [<file>]

Description:
  Converts OCL (Object Constraint Language) expressions or constraint files to C# code.

Arguments:
  <file>        Path to an OCL source file. If omitted, reads from standard input.

Options:
  -e, --expr <ocl>   Convert a single OCL expression supplied on the command line.
  -h, --help         Show this help message and exit.

Examples:
  ocl2csharp constraints.ocl
  echo "context Person inv agePositive: age > 0" | ocl2csharp
  ocl2csharp --expr "self.name <> ''"
""";

if (args.Length == 0 && Console.IsInputRedirected)
{
    // Piped input
    string input = await Console.In.ReadToEndAsync();
    Console.WriteLine(OclToCSharpConverter.Convert(input));
    return 0;
}

string? exprArg = null;
string? fileArg = null;

for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "-h":
        case "--help":
            Console.WriteLine(UsageText);
            return 0;

        case "-e":
        case "--expr":
            if (i + 1 >= args.Length)
            {
                Console.Error.WriteLine("Error: --expr requires an argument.");
                return 1;
            }
            exprArg = args[++i];
            break;

        default:
            if (args[i].StartsWith('-'))
            {
                Console.Error.WriteLine($"Error: Unknown option '{args[i]}'.");
                Console.Error.WriteLine(UsageText);
                return 1;
            }
            fileArg = args[i];
            break;
    }
}

string ocl;

if (exprArg != null)
{
    ocl = exprArg;
}
else if (fileArg != null)
{
    if (!File.Exists(fileArg))
    {
        Console.Error.WriteLine($"Error: File '{fileArg}' not found.");
        return 1;
    }
    ocl = await File.ReadAllTextAsync(fileArg);
}
else
{
    // No file, no --expr: show help
    Console.WriteLine(UsageText);
    return 0;
}

try
{
    string csharp = OclToCSharpConverter.Convert(ocl);
    Console.WriteLine(csharp);
    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Conversion error: {ex.Message}");
    return 1;
}
