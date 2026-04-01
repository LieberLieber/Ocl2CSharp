using Ocl2CSharp;

namespace Ocl2CSharp.Tests;

public class SysMLOclConstraintTests
{
    private static readonly string TestDataPath =
        Path.Combine(AppContext.BaseDirectory, "SysML_OCLAndCSharp.md");

    public static IEnumerable<object[]> GetSysMLOclRules()
    {
        var content = File.ReadAllText(TestDataPath);
        var lines = content.Split('\n');

        string? currentName = null;
        string? oclCode = null;
        string? expectedCSharp = null;
        var blockLines = new List<string>();
        string? currentSection = null; // "OCL" or "CSharp"
        bool inCodeBlock = false;

        foreach (var rawLine in lines)
        {
            var line = rawLine.TrimEnd('\r');

            if (line.StartsWith("# ", StringComparison.Ordinal))
            {
                // Emit previous rule if complete
                if (currentName != null && oclCode != null && expectedCSharp != null)
                    yield return [currentName, oclCode, expectedCSharp];

                currentName = line[2..].Trim();
                oclCode = null;
                expectedCSharp = null;
                currentSection = null;
                inCodeBlock = false;
                blockLines.Clear();
            }
            else if (line.TrimStart().StartsWith("### OCL", StringComparison.Ordinal))
            {
                currentSection = "OCL";
                inCodeBlock = false;
                blockLines.Clear();
            }
            else if (line.TrimStart().StartsWith("### C#", StringComparison.Ordinal))
            {
                currentSection = "CSharp";
                inCodeBlock = false;
                blockLines.Clear();
            }
            else if (line.TrimStart().StartsWith("```", StringComparison.Ordinal))
            {
                if (!inCodeBlock)
                {
                    inCodeBlock = true;
                }
                else
                {
                    // End of code block
                    var code = string.Join("\n", blockLines).Trim();
                    if (currentSection == "OCL")
                        oclCode = code;
                    else if (currentSection == "CSharp")
                        expectedCSharp = code;
                    inCodeBlock = false;
                    blockLines.Clear();
                }
            }
            else if (inCodeBlock)
            {
                blockLines.Add(line);
            }
        }

        // Emit last rule
        if (currentName != null && oclCode != null && expectedCSharp != null)
            yield return [currentName, oclCode, expectedCSharp];
    }

    [Theory]
    [MemberData(nameof(GetSysMLOclRules))]
    public void SysMLRule_ProducesExpectedCSharp(string ruleName, string oclCode, string expectedCSharp)
    {
        _ = ruleName; // used as the theory display name

        var result = OclToCSharpConverter.Convert(oclCode);

        Assert.Equal(expectedCSharp, result);
    }
}
