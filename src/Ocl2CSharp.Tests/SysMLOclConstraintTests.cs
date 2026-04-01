using Ocl2CSharp;

namespace Ocl2CSharp.Tests;

public class SysMLOclConstraintTests
{
    private static readonly string TestDataPath =
        Path.Combine(AppContext.BaseDirectory, "SysML_OCL.txt");

    public static IEnumerable<object[]> GetSysMLOclRules()
    {
        var lines = File.ReadAllLines(TestDataPath);
        string? currentName = null;
        var bodyLines = new List<string>();

        foreach (var line in lines)
        {
            if (line.StartsWith("# ", StringComparison.Ordinal))
            {
                if (currentName != null)
                    yield return [currentName, string.Join("\n", bodyLines).Trim()];

                currentName = line[2..].Trim();
                bodyLines.Clear();
            }
            else
            {
                bodyLines.Add(line);
            }
        }

        if (currentName != null)
            yield return [currentName, string.Join("\n", bodyLines).Trim()];
    }

    [Theory]
    [MemberData(nameof(GetSysMLOclRules))]
    public void SysMLRule_CanBeConverted(string ruleName, string oclCode)
    {
        // Skip rules that are not yet defined
        if (string.IsNullOrWhiteSpace(oclCode) || oclCode.Trim() == "TBD")
            return;

        // Act – must not throw
        var result = OclToCSharpConverter.Convert(oclCode);

        // Assert – a non-empty C# expression must be produced
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        _ = ruleName; // used as the theory display name
    }
}
