using NUnit.Framework;

namespace Ocl2CSharp.Tests;

public class TestFileOclConstraintTests
{
	public static IEnumerable<object[]> GetOclData(string fileName)
	{
		var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

		foreach (var data in TestDataFileReader.Read(filePath))
		{
			yield return [data];
		}
	}

	[TestCaseSource(nameof(GetOclData), ["SysML_OCLAndCSharp.md"])]
	public void SysML(TestData data)
	{
		ConvertOcl(data);
	}
	[TestCaseSource(nameof(GetOclData), ["Consolidated_OCLAndCSharp.md"])]

	public void Consolidated(TestData data)
	{
		ConvertOcl(data);
	}

	private void ConvertOcl(TestData data)
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
		Assert.That(result, Is.EqualTo(data.CSharp.Trim()).IgnoreWhiteSpace);
	}
}
