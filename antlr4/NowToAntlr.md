# How To

How to convert the ANTLR4 syntax to C# code for parsing

- Download: [antlr-4.13.2-complete.jar](https://www.antlr.org/api/)
- Create C# parser form ANTLR4 file: 
	- `java -jar antlr-4.13.2-complete.jar -Dlanguage=CSharp -listener -visitor -package Ocl2CSharp OCL.g4`
- Copy the generated files to `src/Ocl2CSharp/Generated`