# Ocl2CSharp

A converter for OCL code to C#.
Many OCL contraint used in UML, SysMLv2,... are used to define derived properties and/or constraints for validation.
Any implementation of those meta models will have the need to add these OCL contraints into the code.

Using the ANTRL4 parser coupled with a visitor pattern should be able to convert majority of cases.