using System.Text;

namespace Ocl2CSharp.Tests
{
	public class TestData
	{
		private StringBuilder _ocl = new StringBuilder();
		private StringBuilder _cs = new StringBuilder();
		public string Name { get; set; }
		public string Ocl =>_ocl.ToString();
		public string CSharp => _cs.ToString();


		public void AppendCS(string s)
		{
			_cs.AppendLine(s);
		}

		public void AppendOcl(string s)
		{
			_ocl.AppendLine(s);
		}
		public bool GenerateIfStatement { get; set; } = false;

		public override string ToString()
		{
			return Name;
		}
	}
}
