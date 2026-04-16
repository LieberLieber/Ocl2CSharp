using NUnit.Framework;

namespace Ocl2CSharp.Tests;

[TestFixture]
public class SysMLOclConstraintTests
{
	private static readonly string TestDataPath =
		Path.Combine(AppContext.BaseDirectory, "SysML_OCLAndCSharp.md");

	public static IEnumerable<object[]> GetSysMLOclRules()
	{
		foreach (var data in TestDataFileReader.Read(TestDataPath))
		{
			yield return [data];	
		}
	}

	[Test]
	[TestCaseSource(nameof(GetSysMLOclRules))]
	public void SysMLRule_ProducesExpectedCSharp(TestData data)
	{
		var result = OclToCSharpConverter.Convert(data.Ocl, data.GenerateIfStatement).Trim();

		Assert.That(result, Is.EqualTo(data.CSharp.Trim()));
	}
}
