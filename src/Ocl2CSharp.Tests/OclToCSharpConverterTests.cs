using Ocl2CSharp;

namespace Ocl2CSharp.Tests;

public class OclToCSharpConverterTests
{
    // Helper that converts a standalone OCL expression (not wrapped in a context)
    // by using it directly as an oclExpression inside a trivial inv clause.
    private static string ConvertExpression(string oclExpr)
    {
        // Wrap in a dummy context/invariant so the parser gets a full oclFile
        string wrapped = $"context Dummy inv result: {oclExpr}";
        string result = OclToCSharpConverter.Convert(wrapped);
        // Extract just the body after "=> " and before the trailing ";"
        int arrow = result.IndexOf("=> ") + 3;
        int semi = result.LastIndexOf(';');
        return result[arrow..semi].Trim();
    }

    // ------------------------------------------------------------------ literals

    [Fact]
    public void IntegerLiteral_ReturnsAsIs()
    {
        Assert.Equal("42", ConvertExpression("42"));
    }

    [Fact]
    public void RealLiteral_ReturnsAsIs()
    {
        Assert.Equal("3.14", ConvertExpression("3.14"));
    }

    [Fact]
    public void BooleanLiteral_True_ReturnsTrue()
    {
        Assert.Equal("true", ConvertExpression("true"));
    }

    [Fact]
    public void BooleanLiteral_False_ReturnsFalse()
    {
        Assert.Equal("false", ConvertExpression("false"));
    }

    [Fact]
    public void NullLiteral_ReturnsNull()
    {
        Assert.Equal("null", ConvertExpression("null"));
    }

    [Fact]
    public void StringLiteral_ConvertsSingleQuotesToDoubleQuotes()
    {
        Assert.Equal("\"hello\"", ConvertExpression("'hello'"));
    }

    [Fact]
    public void StringLiteral_EscapedSingleQuote_IsUnescaped()
    {
        // OCL: 'it\'s' -> C#: "it's"
        Assert.Equal("\"it's\"", ConvertExpression("'it\\'s'"));
    }

    // ------------------------------------------------------------------ arithmetic

    [Fact]
    public void Addition_GeneratesParenthesizedPlusExpression()
    {
        Assert.Equal("(1 + 2)", ConvertExpression("1 + 2"));
    }

    [Fact]
    public void Subtraction_GeneratesParenthesizedMinusExpression()
    {
        Assert.Equal("(3 - 1)", ConvertExpression("3 - 1"));
    }

    [Fact]
    public void Multiplication_GeneratesParenthesizedStarExpression()
    {
        Assert.Equal("(2 * 3)", ConvertExpression("2 * 3"));
    }

    [Fact]
    public void Division_GeneratesParenthesizedSlashExpression()
    {
        Assert.Equal("(10 / 2)", ConvertExpression("10 / 2"));
    }

    [Fact]
    public void Modulo_ConvertsMod_ToPercent()
    {
        Assert.Equal("(10 % 3)", ConvertExpression("10 mod 3"));
    }

    [Fact]
    public void IntegerDivision_ConvertsDiv_ToSlash()
    {
        Assert.Equal("(10 / 3)", ConvertExpression("10 div 3"));
    }

    [Fact]
    public void UnaryMinus_GeneratesNegation()
    {
        Assert.Equal("-5", ConvertExpression("-5"));
    }

    // ------------------------------------------------------------------ comparisons

    [Fact]
    public void Equality_ConvertsEqualSign_ToDoubleEquals()
    {
        Assert.Equal("(x == 5)", ConvertExpression("x = 5"));
    }

    [Fact]
    public void Inequality_ConvertsAngleBrackets_ToNotEquals()
    {
        Assert.Equal("(x != 5)", ConvertExpression("x <> 5"));
    }

    [Fact]
    public void LessThan_ReturnsAsIs()
    {
        Assert.Equal("(x < 5)", ConvertExpression("x < 5"));
    }

    [Fact]
    public void GreaterThan_ReturnsAsIs()
    {
        Assert.Equal("(x > 5)", ConvertExpression("x > 5"));
    }

    [Fact]
    public void LessThanOrEqual_ReturnsAsIs()
    {
        Assert.Equal("(x <= 5)", ConvertExpression("x <= 5"));
    }

    [Fact]
    public void GreaterThanOrEqual_ReturnsAsIs()
    {
        Assert.Equal("(x >= 5)", ConvertExpression("x >= 5"));
    }

    // ------------------------------------------------------------------ logical operators

    [Fact]
    public void And_ConvertsToDoubleAmpersand()
    {
        Assert.Equal("(true && false)", ConvertExpression("true and false"));
    }

    [Fact]
    public void Or_ConvertsToDoublePipe()
    {
        Assert.Equal("(true || false)", ConvertExpression("true or false"));
    }

    [Fact]
    public void Xor_ConvertsToCaretOperator()
    {
        Assert.Equal("(true ^ false)", ConvertExpression("true xor false"));
    }

    [Fact]
    public void Implies_ConvertsToNegatedOrExpression()
    {
        Assert.Equal("(!a || b)", ConvertExpression("a implies b"));
    }

    [Fact]
    public void Not_ConvertsToBangOperator()
    {
        Assert.Equal("!true", ConvertExpression("not true"));
    }

    // ------------------------------------------------------------------ control flow

    [Fact]
    public void IfThenElse_ConvertsToCSharpTernary()
    {
        Assert.Equal("((x > 0) ? 1 : -1)", ConvertExpression("if x > 0 then 1 else -1 endif"));
    }

    // ------------------------------------------------------------------ self / navigation

    [Fact]
    public void Self_ConvertsToThis()
    {
        Assert.Equal("this", ConvertExpression("self"));
    }

    [Fact]
    public void DotNavigation_PropertyAccess_GeneratesDotAccess()
    {
        Assert.Equal("this.name", ConvertExpression("self.name"));
    }

    [Fact]
    public void DotNavigation_MethodCall_GeneratesMethodCall()
    {
        Assert.Equal("this.getName()", ConvertExpression("self.getName()"));
    }

    [Fact]
    public void DotNavigation_MethodCallWithArgs_GeneratesMethodCall()
    {
        Assert.Equal("items.get(0)", ConvertExpression("items.get(0)"));
    }

    // ------------------------------------------------------------------ collection operations

    [Fact]
    public void CollectionIsEmpty_ConvertsToNotAny()
    {
        Assert.Equal("!items.Any()", ConvertExpression("items->isEmpty()"));
    }

    [Fact]
    public void CollectionNotEmpty_ConvertsToAny()
    {
        Assert.Equal("items.Any()", ConvertExpression("items->notEmpty()"));
    }

    [Fact]
    public void CollectionSize_ConvertsToCount()
    {
        Assert.Equal("items.Count()", ConvertExpression("items->size()"));
    }

    [Fact]
    public void CollectionSelect_ConvertsToWhere()
    {
        Assert.Equal("items.Where(x)", ConvertExpression("items->select(x)"));
    }

    [Fact]
    public void CollectionReject_ConvertsToNegatedWhere()
    {
        Assert.Equal("items.Where(x => !(x))", ConvertExpression("items->reject(x)"));
    }

    [Fact]
    public void CollectionCollect_ConvertsToSelect()
    {
        Assert.Equal("items.Select(x)", ConvertExpression("items->collect(x)"));
    }

    [Fact]
    public void CollectionForAll_ConvertsToAll()
    {
        Assert.Equal("items.All(x)", ConvertExpression("items->forAll(x)"));
    }

    [Fact]
    public void CollectionExists_ConvertsToAny()
    {
        Assert.Equal("items.Any(x)", ConvertExpression("items->exists(x)"));
    }

    [Fact]
    public void CollectionSum_ConvertsToSum()
    {
        Assert.Equal("numbers.Sum()", ConvertExpression("numbers->sum()"));
    }

    [Fact]
    public void CollectionIncludes_ConvertsToContains()
    {
        Assert.Equal("items.Contains(x)", ConvertExpression("items->includes(x)"));
    }

    [Fact]
    public void CollectionExcludes_ConvertsToNegatedContains()
    {
        Assert.Equal("!items.Contains(x)", ConvertExpression("items->excludes(x)"));
    }

    // ------------------------------------------------------------------ let expression

    [Fact]
    public void LetExpression_ConvertsToImmediatelyInvokedLambda()
    {
        Assert.Equal("((x) => (x + 2))(1)", ConvertExpression("let x = 1 in x + 2"));
    }

    // ------------------------------------------------------------------ context / invariant

    [Fact]
    public void InvariantWithName_GeneratesNamedBoolMethod()
    {
        string ocl = "context Person inv ageIsPositive: age > 0";
        string result = OclToCSharpConverter.Convert(ocl).Trim();
        Assert.Equal("// context Person\npublic bool ageIsPositive() => (age > 0);", result);
    }

    [Fact]
    public void InvariantWithoutName_GeneratesDefaultBoolMethod()
    {
        string ocl = "context Person inv: age > 0";
        string result = OclToCSharpConverter.Convert(ocl).Trim();
        Assert.Equal("// context Person\npublic bool Invariant() => (age > 0);", result);
    }

    [Fact]
    public void MultipleInvariants_GenerateMultipleMethods()
    {
        string ocl = "context Person\n  inv nameNotEmpty: name <> ''\n  inv agePositive: age > 0";
        string result = OclToCSharpConverter.Convert(ocl).Trim();
        Assert.Contains("public bool nameNotEmpty()", result);
        Assert.Contains("public bool agePositive()", result);
    }

    // ------------------------------------------------------------------ collection literals

    [Fact]
    public void SetLiteral_ConvertsToHashSet()
    {
        Assert.StartsWith("new HashSet<object>", ConvertExpression("Set{1, 2, 3}"));
    }

    [Fact]
    public void SequenceLiteral_ConvertsToList()
    {
        Assert.StartsWith("new List<object>", ConvertExpression("Sequence{1, 2, 3}"));
    }

    // ------------------------------------------------------------------ chained operations

    [Fact]
    public void ChainedCollectionOps_GeneratesChainedLinq()
    {
        // items->select(x)->size() -> items.Where(x).Count()
        Assert.Equal("items.Where(x).Count()", ConvertExpression("items->select(x)->size()"));
    }

    [Fact]
    public void ComplexExpression_ArithmeticWithComparison()
    {
        Assert.Equal("((a + b) > c)", ConvertExpression("a + b > c"));
    }
}
