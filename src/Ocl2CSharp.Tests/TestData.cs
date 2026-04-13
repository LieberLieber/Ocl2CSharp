namespace Ocl2CSharp.Tests
{
	public class TestData
	{
		public string Name { get; set; }
		public string Ocl { get; set; } = string.Empty;
		public string CSharp { get; set; } = string.Empty;

		public bool GenerateIfStatement { get; set; } = false;

		public override string ToString()
		{
			return Name;
		}
	}
}
