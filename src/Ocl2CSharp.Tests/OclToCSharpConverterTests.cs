using Ocl2CSharp;

namespace Ocl2CSharp.Tests;

public class OclToCSharpConverterTests
{
    // -------------------------------------------------------------------------
    // Logical operators
    // -------------------------------------------------------------------------

    [Fact]
    public void LogicalAnd_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0 and age <= 150");
        Assert.Equal("age >= 0 && age <= 150", result);
    }

    [Fact]
    public void LogicalAnd_Ampersand_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0 & age <= 150");
        Assert.Equal("age >= 0 && age <= 150", result);
    }

    [Fact]
    public void LogicalOr_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("email <> null or phone <> null");
        Assert.Equal("email != null || phone != null", result);
    }

    [Fact]
    public void LogicalXor_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("a or b xor c");
        Assert.Contains("^", result);
    }

    [Fact]
    public void ImpliesOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 18 implies canVote = true");
        Assert.Contains("!(age >= 18)", result);
        Assert.Contains("canVote == true", result);
    }

    [Fact]
    public void ImpliesArrow_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 18 => canVote = true");
        Assert.Contains("!(age >= 18)", result);
        Assert.Contains("canVote == true", result);
    }

    // -------------------------------------------------------------------------
    // Equality / comparison operators
    // -------------------------------------------------------------------------

    [Fact]
    public void Equality_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = 'Alice'");
        Assert.Equal("name == \"Alice\"", result);
    }

    [Fact]
    public void Inequality_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null");
        Assert.Equal("name != null", result);
    }

    [Fact]
    public void Inequality_SlashEquals_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value /= 0");
        Assert.Equal("value != 0", result);
    }

    [Fact]
    public void LessThan_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age < 18");
        Assert.Equal("age < 18", result);
    }

    [Fact]
    public void GreaterThanOrEqual_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0");
        Assert.Equal("age >= 0", result);
    }

    // -------------------------------------------------------------------------
    // Arithmetic operators
    // -------------------------------------------------------------------------

    [Fact]
    public void Addition_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance + interest >= 0");
        Assert.Equal("balance + interest >= 0", result);
    }

    [Fact]
    public void Multiplication_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance * rate > 0");
        Assert.Equal("balance * rate > 0", result);
    }

    [Fact]
    public void ModOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance mod 2 = 0");
        Assert.Equal("balance % 2 == 0", result);
    }

    [Fact]
    public void DivOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("total div numItems > 0");
        Assert.Equal("total / numItems > 0", result);
    }

    // -------------------------------------------------------------------------
    // Unary operators
    // -------------------------------------------------------------------------

    [Fact]
    public void NotOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("not active");
        Assert.Equal("!active", result);
    }

    [Fact]
    public void UnaryMinus_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("-balance > 0");
        Assert.Equal("-balance > 0", result);
    }

    // -------------------------------------------------------------------------
    // Conditional expression
    // -------------------------------------------------------------------------

    [Fact]
    public void ConditionalExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif");
        Assert.Equal("(age >= 18 ? isAdult : isMinor)", result);
    }

    // -------------------------------------------------------------------------
    // Literals
    // -------------------------------------------------------------------------

    [Fact]
    public void NullLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null");
        Assert.Equal("name != null", result);
    }

    [Fact]
    public void BooleanLiteralTrue_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = true");
        Assert.Equal("active == true", result);
    }

    [Fact]
    public void BooleanLiteralFalse_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = false");
        Assert.Equal("active == false", result);
    }

    [Fact]
    public void IntLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance > 0");
        Assert.Equal("balance > 0", result);
    }

    [Fact]
    public void StringLiteralSingleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = 'Alice'");
        Assert.Equal("name == \"Alice\"", result);
    }

    [Fact]
    public void StringLiteralDoubleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = \"Alice\"");
        Assert.Equal("name == \"Alice\"", result);
    }

    [Fact]
    public void EnumerationLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("status = Status::Active");
        Assert.Equal("status == Status.Active", result);
    }

    // -------------------------------------------------------------------------
    // Navigation / member access
    // -------------------------------------------------------------------------

    [Fact]
    public void MemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ceo.name <> null");
        Assert.Equal("ceo.name != null", result);
    }

    [Fact]
    public void NestedMemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("company.ceo.name <> null");
        Assert.Equal("company.ceo.name != null", result);
    }

    // -------------------------------------------------------------------------
    // Collection operations — arrow (->)
    // -------------------------------------------------------------------------

    [Fact]
    public void SelectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->select(e | e.age >= 18)->notEmpty()");
        Assert.Contains(".Where(e => e.age >= 18)", result);
        Assert.Contains(".Any()", result);
    }

    [Fact]
    public void ForAllOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->forAll(e | e.age >= 18)");
        Assert.Equal("employees.All(e => e.age >= 18)", result);
    }

    [Fact]
    public void ExistsOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->exists(e | e.role = 'CEO')");
        Assert.Equal("employees.Any(e => e.role == \"CEO\")", result);
    }

    [Fact]
    public void CollectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->collect(e | e.name)->notEmpty()");
        Assert.Contains(".Select(e => e.name)", result);
        Assert.Contains(".Any()", result);
    }

    [Fact]
    public void SizeOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->size() > 0");
        Assert.Equal("employees.Count() > 0", result);
    }

    [Fact]
    public void IsEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->isEmpty()");
        Assert.Equal("!contracts.Any()", result);
    }

    [Fact]
    public void NotEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->notEmpty()");
        Assert.Equal("contracts.Any()", result);
    }

    [Fact]
    public void IncludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->includes(alice)");
        Assert.Equal("employees.Contains(alice)", result);
    }

    [Fact]
    public void ExcludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->excludes(alice)");
        Assert.Equal("!employees.Contains(alice)", result);
    }

    [Fact]
    public void SumOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("salaries->sum() > 0");
        Assert.Equal("salaries.Sum() > 0", result);
    }

    [Fact]
    public void MaxOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->max()");
        Assert.Equal("scores.Max()", result);
    }

    [Fact]
    public void MinOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->min()");
        Assert.Equal("scores.Min()", result);
    }

    [Fact]
    public void RejectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->reject(e | e.age < 18)->isEmpty()");
        Assert.Contains(".Where(e => !(e.age < 18))", result);
        Assert.Contains("!employees", result);
    }

    [Fact]
    public void AsSetOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("items->asSet()");
        Assert.Equal("items.ToHashSet()", result);
    }

    [Fact]
    public void AsSequenceOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("items->asSequence()");
        Assert.Equal("items.ToList()", result);
    }

    [Fact]
    public void ToUpperCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toUpperCase()");
        Assert.Equal("name.ToUpper()", result);
    }

    [Fact]
    public void ToLowerCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toLowerCase()");
        Assert.Equal("name.ToLower()", result);
    }

    [Fact]
    public void ToIntegerOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value->toInteger()");
        Assert.Equal("Convert.ToInt32(value)", result);
    }

    // -------------------------------------------------------------------------
    // Dot (.) OCL operations
    // -------------------------------------------------------------------------

    [Fact]
    public void OclIsUndefined_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name.oclIsUndefined() = false");
        Assert.Equal("(name == null) == false", result);
    }

    [Fact]
    public void OclIsKindOf_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("obj.oclIsKindOf(Person)");
        Assert.Equal("(obj is Person)", result);
    }

    // -------------------------------------------------------------------------
    // Collection literals
    // -------------------------------------------------------------------------

    [Fact]
    public void SetLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Set{1, 2, 3}->includes(status)");
        Assert.Contains("new HashSet<dynamic>", result);
        Assert.Contains(".Contains(status)", result);
    }

    [Fact]
    public void SequenceLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Sequence{1, 2, 3}->size() > 0");
        Assert.Contains("new List<dynamic>", result);
        Assert.Contains(".Count()", result);
    }

    [Fact]
    public void BagLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Bag{1, 2, 3}->size() > 0");
        Assert.Contains("new List<dynamic>", result);
    }

    // -------------------------------------------------------------------------
    // Let expressions
    // -------------------------------------------------------------------------

    [Fact]
    public void LetExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("let x = age + 1 in x > 0");
        Assert.Equal("(age + 1).Select(x => x > 0)", result);
    }

    [Fact]
    public void LetExpression_WithTypedBinding_ReturnsLinqExpression()
    {
        var ocl =
            "let renderings: OrderedSet(ViewRenderingMembership) = " +
            "featureMembership->selectByKind(ViewRenderingMembership) in " +
            "if renderings->isEmpty() then null " +
            "else renderings->first().referencedRendering endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "featureMembership.OfType<ViewRenderingMembership>().Select(renderings => (!renderings.Any() ? null : renderings.First().referencedRendering))",
            result);
    }

    [Fact]
    public void LetExpression_MultipleBindings_ReturnsNestedLinqExpressions()
    {
        var result = OclToCSharpConverter.Convert("let x = age + 1, y = age - 1 in x > y");
        Assert.Equal("(age + 1).Select(x => (age - 1).Select(y => x > y))", result);
    }

    // -------------------------------------------------------------------------
    // Parenthesized expression
    // -------------------------------------------------------------------------

    [Fact]
    public void ParenthesizedExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("(age + 1) > 0");
		Assert.Equal("(age + 1) > 0", result);
	}

    // -------------------------------------------------------------------------
    // Chained operations
    // -------------------------------------------------------------------------

    [Fact]
    public void ChainedSelectAndForAll_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->select(e | e.active = true)->forAll(e | e.age >= 18)");
        Assert.Contains(".Where(e => e.active == true)", result);
        Assert.Contains(".All(e => e.age >= 18)", result);
    }


    [Fact]
    public void SelectByKind_IsCoverted()
    {
		var result = OclToCSharpConverter.Convert("ownedMembership->selectByKind(VariantMembership)");
		Assert.Equal("ownedMembership.OfType<VariantMembership>()", result);
	}
}

