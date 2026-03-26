# Converter

Your task as a coding agent is to implement a converter from OCL to C#.
The idea is that you create the functionality that gets a OCL string, converts it into C# and returns it as a string.

- The converter should be put into the `src\Ocl2CSharp.sln` Visual Studio solution.
- The `src\Ocl2CSharp\Ocl2CShapr.csproj` contains a `Generated` folder with a ANTLR4 parser code.
- Create a vistor pattern derived from the `OCLVisitor` to implement the converter.
- Tests should be added for some trivial cases
- A CLI should be added added for further testing

