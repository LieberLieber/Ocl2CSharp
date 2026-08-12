using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Spectre.Console;

namespace Ocl2CSharp.Tests;

[TestFixture]
public class CLITest
{
    public class TestData
    {
        public string[] Args { get; }
        public int ExitCode { get; }
        public Regex Output { get; } = null;

        public TestData(int exitCode, string output, params string[] args)
        {
            ExitCode = exitCode;
            if (output is not null)
            {
                Output = new Regex(output);
            }

            Args = args;
        }

        public override string ToString()
        {
            if (Args == null)
            {
                return "{null}";
            }

            return $"'{string.Join(" ", Args)}'";
        }
    }

    public static IEnumerable<TestData> TestCases()
    {
        yield return new TestData(-1, "Unknown command", "");
        yield return new TestData(-1, "Option 'file' is defined but no value has been provided", "-f");
        yield return new TestData(-1, "Option 'expression' is defined but no value has been provided", "-e");
        yield return new TestData(-1, "Option 'output' is defined but no value has been provided", "-e", "X", "-o");
        yield return new TestData(1, "Provide --expression or --file", null);
        yield return new TestData(1, "File not found: x", "-f", "x");
        yield return new TestData(0, @"USAGE:.*\s*ocl2csharp \[OPTIONS\]\s*EXAMPLES:", "-h");
        yield return new TestData(0, @"\d*.\d*.\d*", "-v");
        yield return new TestData(0, "X", "-e", "x");
        yield return new TestData(0, "Output written to: y", "-e", "X", "-o", "y");
        yield return new TestData(0, @"\(true \? A : B\)", "-e", "if true then a else b endif");
        yield return new TestData(0, @"if \(true\)\s*{\s*return A;\s*}\s*else\s*{\s*return B;\s*}", "-e", "if true then a else b endif", "--if-statement");
    }

    [OneTimeSetUp]
    public void Setup()
    {
        AnsiConsole.Record();
    }

    [TestCaseSource(nameof(TestCases))]
    public void Arguments(TestData testData)
    {
        string pre = AnsiConsole.ExportText();
        var result = CLI.Program.Main(testData.Args);
        string output = AnsiConsole.ExportText().Substring(pre.Length).Trim();
        if (testData.Output != null)
        {
            Assert.That(output, Does.Match(testData.Output));
        }

        Assert.That(result, Is.EqualTo(testData.ExitCode));
    }
}