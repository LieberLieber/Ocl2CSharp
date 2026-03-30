# Ocl2CSharp for SysML v2

A converter for OCL code to C#, intended primarily for SysML v2 use cases.

Many OCL constraints used in UML and especially SysML v2 are used to define derived properties and/or validation constraints.
Any implementation of those metamodels will need to translate these OCL constraints into executable code.

Using the ANTLR4 parser coupled with a visitor pattern should make it possible to convert the majority of cases.

## References

- ANTLR: https://www.antlr.org/
- OMG SysML v2: https://www.omg.org/spec/SysML
- OMG OCL: https://www.omg.org/spec/OCL

## License

This project is licensed under the [MIT License](LICENSE).