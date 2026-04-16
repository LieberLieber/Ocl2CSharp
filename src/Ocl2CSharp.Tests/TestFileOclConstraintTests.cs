using NUnit.Framework;

namespace Ocl2CSharp.Tests;

[TestFixture]
public class TestFileOclConstraintTests
{
	private static readonly string TestDataPath1 =
		Path.Combine(AppContext.BaseDirectory, "SysML_OCLAndCSharp.md");


	private static readonly string TestDataPath2 =
		Path.Combine(AppContext.BaseDirectory, "Consolidated_OCLAndCSharp.md");

	public static IEnumerable<object[]> GetSysMLOclRules1()
	{
		foreach (var data in TestDataFileReader.Read(TestDataPath1))
		{
			yield return [data];
		}
	}

	[Test]
	[TestCaseSource(nameof(GetSysMLOclRules1))]
	public void SysMLRules(TestData data)
	{
		var result = OclToCSharpConverter.Convert(data.Ocl, data.GenerateIfStatement).Trim();

		Assert.That(result, Is.EqualTo(data.CSharp.Trim()));
	}


	public static IEnumerable<object[]> GetSysMLOclRules2()
	{
		foreach (var data in TestDataFileReader.Read(TestDataPath2))
		{
			yield return [data];
		}
	}

	[Test]
	[TestCaseSource(nameof(GetSysMLOclRules2))]
	public void ConsolidatedRules(TestData data)
	{
		var result = OclToCSharpConverter.Convert(data.Ocl, data.GenerateIfStatement).Trim();
#if DEBUG
		TestContext.WriteLine("Ocl");
		TestContext.WriteLine(data.Ocl);
		TestContext.WriteLine("C#");
		TestContext.WriteLine(data.CSharp);
		TestContext.WriteLine("Generated");
		TestContext.WriteLine(result);
#endif
		Assert.That(result, Is.EqualTo(data.CSharp.Trim()));
	}
}
