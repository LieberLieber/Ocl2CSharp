namespace Ocl2CSharp.Tests;

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

	[Theory]
	[MemberData(nameof(GetSysMLOclRules))]
	public void SysMLRule_ProducesExpectedCSharp(TestData data)
	{
		_ = data.Name; // used as the theory display name

		var result = OclToCSharpConverter.Convert(data.Ocl);

		Assert.Equal(data.CSharp, result);
	}
}
