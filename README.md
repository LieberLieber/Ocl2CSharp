# Ocl2CSharp

A tool for translating [Object Constraint Language (OCL)](https://www.omg.org/spec/OCL/) expressions into executable C# code. It bridges the gap between formal UML model constraints and practical enforcement in .NET applications, enabling model-driven development workflows where constraints defined in UML models are automatically validated at runtime in C# code.

## What is OCL?

The **Object Constraint Language (OCL)** is a formal, declarative language defined by the [Object Management Group (OMG)](https://www.omg.org/) for specifying constraints and queries on UML models and other MOF-based metamodels. OCL is side-effect-free and allows you to express:

- **Invariants** – rules that must always hold for all instances of a class
- **Preconditions** – constraints that must be satisfied before an operation executes
- **Postconditions** – constraints that must hold after an operation completes
- **Derived attributes** – values computed from other model elements
- **Queries** – expressions for navigating and selecting model elements

OCL is a core part of the [Model Driven Architecture (MDA)](https://www.omg.org/mda/) and integrates with standards such as [QVT (Query/View/Transformation)](https://www.omg.org/spec/QVT/).

## OMG Specifications

| Specification | Version | Link |
|--------------|---------|------|
| Object Constraint Language (OCL) | 2.4 | [https://www.omg.org/spec/OCL/2.4/](https://www.omg.org/spec/OCL/2.4/) |
| OCL 2.4 Specification PDF | 2.4 | [https://www.omg.org/spec/OCL/2.4/PDF](https://www.omg.org/spec/OCL/2.4/PDF) |
| OCL Specification (all versions) | – | [https://www.omg.org/spec/OCL/](https://www.omg.org/spec/OCL/) |
| Unified Modeling Language (UML) | 2.5.1 | [https://www.omg.org/spec/UML/2.5.1/](https://www.omg.org/spec/UML/2.5.1/) |
| Meta Object Facility (MOF) | 2.5.1 | [https://www.omg.org/spec/MOF/2.5.1/](https://www.omg.org/spec/MOF/2.5.1/) |

## Related Resources

- [LieberLieber Software](https://www.lieberlieber.com/) – Developer of Ocl2CSharp
- [OMG Official Website](https://www.omg.org/) – Standards body for OCL, UML, and related specifications
- [Wikipedia: Object Constraint Language](https://en.wikipedia.org/wiki/Object_Constraint_Language)
- [Wikipedia: Unified Modeling Language](https://en.wikipedia.org/wiki/Unified_Modeling_Language)
