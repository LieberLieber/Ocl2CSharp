using Ocl2CSharp;
using Ocl2CSharp.Tests;

var path1 = "testdata/SysML_OCLAndCSharp.md";
var path2 = "testdata/Consolidated_OCLAndCSharp.md";

foreach (var path in new[] { path1, path2 })
{
    var suite = path.Contains("SysML") ? "SysMLRules" : "ConsolidatedRules";
    foreach (var data in TestDataFileReader.Read(path))
    {
        try
        {
            var result = OclToCSharpConverter.Convert(data.Ocl, data.GenerateIfStatement).Trim();
            var expected = data.CSharp.Trim();
            if (result != expected)
            {
                Console.WriteLine($"FAIL|{suite}|{data.Name}|{expected}|{result}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CRASH|{suite}|{data.Name}|{ex.Message}");
        }
    }
}
