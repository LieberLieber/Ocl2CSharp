using Ocl2CSharp;

namespace Ocl2CSharp.Tests;

public class OclToCSharpConverterTests
{
    // -------------------------------------------------------------------------
    // Invariant tests
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_SimpleComparison_IsConverted()
    {
        var ocl = "context Person inv agePositive: age >= 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("public bool AgePositive()", result);
        Assert.Contains("return age >= 0;", result);
    }

    [Fact]
    public void Invariant_WithoutName_UsesDefaultName()
    {
        var ocl = "context Person inv: age >= 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("public bool Invariant()", result);
        Assert.Contains("return age >= 0;", result);
    }

    [Fact]
    public void Invariant_LogicalAnd_IsConverted()
    {
        var ocl = "context Person inv validAge: age >= 0 and age <= 150";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("return age >= 0 && age <= 150;", result);
    }

    [Fact]
    public void Invariant_LogicalOr_IsConverted()
    {
        var ocl = "context Person inv hasContact: email <> null or phone <> null";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("return email != null || phone != null;", result);
    }

    [Fact]
    public void Invariant_NotOperator_IsConverted()
    {
        var ocl = "context Person inv notNull: not name = null";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("!name == null", result);
    }

    [Fact]
    public void Invariant_EqualityOperator_IsConverted()
    {
        var ocl = "context Person inv named: name = 'Alice'";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("name == \"Alice\"", result);
    }

    [Fact]
    public void Invariant_InequalityOperator_IsConverted()
    {
        var ocl = "context Person inv notNull: name <> null";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("name != null", result);
    }

    [Fact]
    public void Invariant_ArithmeticExpression_IsConverted()
    {
        var ocl = "context Account inv balanced: balance + interest >= 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("return balance + interest >= 0;", result);
    }

    [Fact]
    public void Invariant_MultiplicationExpression_IsConverted()
    {
        var ocl = "context Account inv positive: balance * rate > 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("return balance * rate > 0;", result);
    }

    [Fact]
    public void Invariant_ModOperator_IsConverted()
    {
        var ocl = "context Account inv evenBalance: balance mod 2 = 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("balance % 2 == 0", result);
    }

    // -------------------------------------------------------------------------
    // Conditional expression
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_ConditionalExpression_IsConverted()
    {
        var ocl = "context Person inv adultOrMinor: if age >= 18 then isAdult = true else isAdult = false endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("age >= 18 ? isAdult == true : isAdult == false", result);
    }

    // -------------------------------------------------------------------------
    // Collection operations
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_SelectOperation_IsConverted()
    {
        var ocl = "context Company inv adultEmployees: employees->select(e | e.age >= 18)->notEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains(".Where(e => e.age >= 18)", result);
        Assert.Contains(".Any()", result);
    }

    [Fact]
    public void Invariant_ForAllOperation_IsConverted()
    {
        var ocl = "context Company inv allAdult: employees->forAll(e | e.age >= 18)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains(".All(e => e.age >= 18)", result);
    }

    [Fact]
    public void Invariant_ExistsOperation_IsConverted()
    {
        var ocl = "context Company inv hasCEO: employees->exists(e | e.role = 'CEO')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains(".Any(e => e.role == \"CEO\")", result);
    }

    [Fact]
    public void Invariant_CollectOperation_IsConverted()
    {
        var ocl = "context Company inv collectNames: employees->collect(e | e.name)->notEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains(".Select(e => e.name)", result);
        Assert.Contains(".Any()", result);
    }

    [Fact]
    public void Invariant_SizeOperation_IsConverted()
    {
        var ocl = "context Company inv hasEmployees: employees->size() > 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("employees.Count() > 0", result);
    }

    [Fact]
    public void Invariant_IsEmptyOperation_IsConverted()
    {
        var ocl = "context Company inv noContracts: contracts->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("!contracts.Any()", result);
    }

    [Fact]
    public void Invariant_NotEmptyOperation_IsConverted()
    {
        var ocl = "context Company inv hasContracts: contracts->notEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("contracts.Any()", result);
    }

    [Fact]
    public void Invariant_IncludesOperation_IsConverted()
    {
        var ocl = "context Company inv hasAlice: employees->includes(alice)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("employees.Contains(alice)", result);
    }

    [Fact]
    public void Invariant_SumOperation_IsConverted()
    {
        var ocl = "context Company inv totalSalary: salaries->sum() > 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("salaries.Sum()", result);
    }

    // -------------------------------------------------------------------------
    // Literals
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_NullLiteral_IsConverted()
    {
        var ocl = "context Person inv hasName: name <> null";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("name != null", result);
    }

    [Fact]
    public void Invariant_BooleanLiteral_IsConverted()
    {
        var ocl = "context Person inv isActive: active = true";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("active == true", result);
    }

    [Fact]
    public void Invariant_IntLiteral_IsConverted()
    {
        var ocl = "context Account inv positiveBalance: balance > 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("balance > 0", result);
    }

    [Fact]
    public void Invariant_StringLiteral_IsConverted()
    {
        var ocl = "context Person inv namedAlice: name = 'Alice'";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("name == \"Alice\"", result);
    }

    [Fact]
    public void Invariant_EnumerationLiteral_IsConverted()
    {
        var ocl = "context Person inv hasStatus: status = Status::Active";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("status == Status.Active", result);
    }

    // -------------------------------------------------------------------------
    // Navigation / member access
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_MemberAccess_IsConverted()
    {
        var ocl = "context Company inv ceoExists: ceo.name <> null";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("ceo.name != null", result);
    }

    // -------------------------------------------------------------------------
    // Derived attribute
    // -------------------------------------------------------------------------

    [Fact]
    public void DerivedAttribute_SimpleExpression_IsConverted()
    {
        var ocl = "context Person::fullName : String derive: firstName + ' ' + lastName";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("public", result);
        Assert.Contains("fullName", result);
        Assert.Contains("get {", result);
        Assert.Contains("firstName", result);
    }

    // -------------------------------------------------------------------------
    // Multiple context specifications
    // -------------------------------------------------------------------------

    [Fact]
    public void MultipleInvariants_AreConverted()
    {
        var ocl = "context Person inv agePositive: age >= 0\ncontext Person inv hasName: name <> null";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("public bool AgePositive()", result);
        Assert.Contains("public bool HasName()", result);
    }

    // -------------------------------------------------------------------------
    // Implies operator
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_ImpliesOperator_IsConverted()
    {
        var ocl = "context Person inv adultImpliesVoter: age >= 18 implies canVote = true";
        var result = OclToCSharpConverter.Convert(ocl);
        // implies(a, b) => !a || b
        Assert.Contains("!(age >= 18)", result);
        Assert.Contains("canVote == true", result);
    }

    // -------------------------------------------------------------------------
    // Reject operation
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_RejectOperation_IsConverted()
    {
        var ocl = "context Company inv noMinors: employees->reject(e | e.age < 18)->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains(".Where(e => !(e.age < 18))", result);
        Assert.Contains("!employees", result);
    }

    // -------------------------------------------------------------------------
    // OclIsUndefined
    // -------------------------------------------------------------------------

    [Fact]
    public void Invariant_OclIsUndefined_IsConverted()
    {
        var ocl = "context Person inv nameNotNull: name.oclIsUndefined() = false";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("(name == null) == false", result);
    }

    // -------------------------------------------------------------------------
    // Collection set literal
    // -------------------------------------------------------------------------

    [Fact]
    public void SetLiteral_IsConverted()
    {
        var ocl = "context Person inv validStatus: Set{1, 2, 3}->includes(status)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("new HashSet<dynamic>", result);
        Assert.Contains(".Contains(status)", result);
    }

    [Fact]
    public void SequenceLiteral_IsConverted()
    {
        var ocl = "context Person inv seq: Sequence{1, 2, 3}->size() > 0";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Contains("new List<dynamic>", result);
        Assert.Contains(".Count()", result);
    }
}
