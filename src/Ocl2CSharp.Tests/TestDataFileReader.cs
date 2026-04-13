namespace Ocl2CSharp.Tests
{
	public class TestDataFileReader
	{
		internal enum Reading
		{
			None,
			Ocl,
			CSharp
		}


		public static IEnumerable<TestData> Read(string filename)
		{
			var lines = File.ReadAllLines(filename);
			var currentlyReading = Reading.None;
			var data = new TestData();
		
			foreach (var line in lines)
			{

				if (line.StartsWith("# ", StringComparison.Ordinal))
				{
					// Emit previous rule if complete
					if (!string.IsNullOrEmpty(data.Name))
					{
						yield return data;	
					}

					data = new TestData()
					{
						Name = line[2..].Trim()
					};
					continue;
				}


				if (line.Trim().StartsWith("If statement", StringComparison.OrdinalIgnoreCase))
				{
					data.GenerateIfStatement = true;
				}

				if (line.TrimStart().StartsWith("```"))
				{
					currentlyReading = Reading.None;
					var trimmed = line.TrimEnd();
					if (trimmed.EndsWith("OCL", StringComparison.OrdinalIgnoreCase))
					{
						currentlyReading = Reading.Ocl;
					}
					else if (trimmed.EndsWith("CSharp", StringComparison.OrdinalIgnoreCase))
					{
						currentlyReading = Reading.CSharp;
					}
					continue;
				}

				switch (currentlyReading)
				{
					case Reading.Ocl:
						data.Ocl += $" {line}";
						break;
					case Reading.CSharp:
						data.CSharp += $" {line}";
						break;
				}

			}

			// Emit last rule
			if (!string.IsNullOrEmpty(data.Name))
			{
				yield return data;
			}
		}

	}
}
