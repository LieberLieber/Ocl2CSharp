using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Ocl2CSharp.Generated;

namespace Ocl2CSharp;

/// <summary>
/// Converts OCL (Object Constraint Language) expressions to equivalent C# code
/// by implementing the visitor pattern derived from <see cref="OCLBaseVisitor{Result}"/>.
/// </summary>
public class OclToCSharpConverter : OCLBaseVisitor<string>
{
    // Maps OCL collection operations to C# LINQ method names
    private static readonly Dictionary<string, string> CollectionOperationMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["select"]       = "Where",
        ["collect"]      = "Select",
        ["forAll"]       = "All",
        ["exists"]       = "Any",
        ["size"]         = "Count",
        ["isEmpty"]      = "!Any",
        ["notEmpty"]     = "Any",
        ["sum"]          = "Sum",
        ["min"]          = "Min",
        ["max"]          = "Max",
        ["first"]        = "First",
        ["last"]         = "Last",
        ["includes"]     = "Contains",
        ["union"]        = "Union",
        ["intersection"] = "Intersect",
        ["asSet"]        = "Distinct",
        ["flatten"]      = "SelectMany",
        ["one"]          = "Count",
        ["any"]          = "FirstOrDefault",
    };

    /// <summary>
    /// Converts an OCL string to C# code.
    /// </summary>
    /// <param name="oclInput">The OCL source string.</param>
    /// <returns>The generated C# code string.</returns>
    public static string Convert(string oclInput)
    {
        var inputStream = new AntlrInputStream(oclInput);
        var lexer = new OCLLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new OCLParser(tokenStream);

        var tree = parser.oclFile();
        var converter = new OclToCSharpConverter();
        return converter.Visit(tree);
    }

    // ------------------------------------------------------------------ top-level

    public override string VisitOclFile(OCLParser.OclFileContext context)
    {
        var sb = new StringBuilder();
        foreach (var pkg in context.packageDeclaration())
            sb.AppendLine(Visit(pkg));
        foreach (var ctx in context.contextDeclaration())
            sb.AppendLine(Visit(ctx));
        return sb.ToString().TrimEnd();
    }

    public override string VisitPackageDeclaration(OCLParser.PackageDeclarationContext context)
    {
        string ns = Visit(context.pathName());
        var sb = new StringBuilder();
        sb.AppendLine($"namespace {ns}");
        sb.AppendLine("{");
        foreach (var ctx in context.contextDeclaration())
            sb.AppendLine("    " + Visit(ctx));
        sb.Append("}");
        return sb.ToString();
    }

    public override string VisitContextDeclaration(OCLParser.ContextDeclarationContext context)
    {
        return Visit(context.contextDeclarationTail());
    }

    public override string VisitContextDeclarationTail(OCLParser.ContextDeclarationTailContext context)
    {
        string className = Visit(context.pathName());

        if (context.classifierContextBody() != null)
        {
            string body = Visit(context.classifierContextBody());
            return $"// context {className}\n{body}";
        }
        else
        {
            return Visit(context.operationContextBody());
        }
    }

    public override string VisitClassifierContextBody(OCLParser.ClassifierContextBodyContext context)
    {
        var sb = new StringBuilder();
        foreach (var inv in context.invClause())
            sb.AppendLine(Visit(inv));
        foreach (var def in context.defClause())
            sb.AppendLine(Visit(def));
        return sb.ToString().TrimEnd();
    }

    public override string VisitInvClause(OCLParser.InvClauseContext context)
    {
        string name = context.NAME() != null ? context.NAME().GetText() : "Invariant";
        string expr = Visit(context.oclExpression());
        return $"public bool {name}() => {expr};";
    }

    public override string VisitDefClause(OCLParser.DefClauseContext context)
    {
        return Visit(context.defBody());
    }

    public override string VisitDefBody(OCLParser.DefBodyContext context)
    {
        string name = context.GetChild(0).GetText();
        string type = Visit(context.typeSpecifier());
        string expr = Visit(context.oclExpression());
        return $"public {type} {name}() => {expr};";
    }

    public override string VisitOperationContextBody(OCLParser.OperationContextBodyContext context)
    {
        string opName = Visit(context.operationName());
        string returnType = context.returnTypeSpecifier() != null
            ? Visit(context.returnTypeSpecifier())
            : "void";
        string parameters = context.formalParameterList() != null
            ? Visit(context.formalParameterList())
            : "";

        var sb = new StringBuilder();

        foreach (var pre in context.preClause())
            sb.AppendLine(Visit(pre));

        foreach (var post in context.postClause())
            sb.AppendLine(Visit(post));

        foreach (var body in context.bodyClause())
        {
            string bodyExpr = Visit(body);
            sb.AppendLine($"public {returnType} {opName}({parameters}) => {bodyExpr};");
        }

        return sb.ToString().TrimEnd();
    }

    public override string VisitPreClause(OCLParser.PreClauseContext context)
    {
        string name = context.NAME() != null ? context.NAME().GetText() : "Pre";
        string expr = Visit(context.oclExpression());
        return $"// pre {name}: {expr}";
    }

    public override string VisitPostClause(OCLParser.PostClauseContext context)
    {
        string name = context.NAME() != null ? context.NAME().GetText() : "Post";
        string expr = Visit(context.oclExpression());
        return $"// post {name}: {expr}";
    }

    public override string VisitBodyClause(OCLParser.BodyClauseContext context)
    {
        return Visit(context.oclExpression());
    }

    // ------------------------------------------------------------------ expressions

    public override string VisitOclExpression(OCLParser.OclExpressionContext context)
    {
        if (context.letExpression() != null)
            return Visit(context.letExpression());
        if (context.ifExpression() != null)
            return Visit(context.ifExpression());
        return Visit(context.logicalExpression());
    }

    public override string VisitLetExpression(OCLParser.LetExpressionContext context)
    {
        var decls = context.variableDeclaration();
        string body = Visit(context.oclExpression());

        // Wrap in a C# immediately-invoked lambda for each variable binding (innermost first)
        string result = body;
        for (int i = decls.Length - 1; i >= 0; i--)
        {
            string varName = decls[i].NAME().GetText();
            string value = Visit(decls[i].oclExpression());
            result = $"(({varName}) => {result})({value})";
        }
        return result;
    }

    public override string VisitVariableDeclaration(OCLParser.VariableDeclarationContext context)
    {
        // Used individually when visiting let bindings
        string name = context.NAME().GetText();
        string value = Visit(context.oclExpression());
        return $"var {name} = {value}";
    }

    public override string VisitIfExpression(OCLParser.IfExpressionContext context)
    {
        var exprs = context.oclExpression();
        string condition = Visit(exprs[0]);
        string thenBranch = Visit(exprs[1]);
        string elseBranch = Visit(exprs[2]);
        return $"({condition} ? {thenBranch} : {elseBranch})";
    }

    public override string VisitLogicalExpression(OCLParser.LogicalExpressionContext context)
    {
        if (context.ChildCount == 1)
            return Visit(context.notExpression());

        string left = Visit(context.logicalExpression());
        string op = context.GetChild(1).GetText();
        string right = Visit(context.notExpression());

        return op switch
        {
            "and"     => $"({left} && {right})",
            "or"      => $"({left} || {right})",
            "xor"     => $"({left} ^ {right})",
            "implies" => $"(!{left} || {right})",
            _         => $"({left} {op} {right})",
        };
    }

    public override string VisitNotExpression(OCLParser.NotExpressionContext context)
    {
        if (context.notExpression() != null)
            return $"!{Visit(context.notExpression())}";
        return Visit(context.relationalExpression());
    }

    public override string VisitRelationalExpression(OCLParser.RelationalExpressionContext context)
    {
        // base case: single additiveExpression
        if (context.ChildCount == 1)
            return Visit(context.additiveExpression(0));

        string left = Visit(context.additiveExpression(0));
        string op = context.GetChild(1).GetText();
        string right = Visit(context.additiveExpression(1));

        string csOp = op switch
        {
            "="  => "==",
            "<>" => "!=",
            _    => op,
        };

        return $"({left} {csOp} {right})";
    }

    public override string VisitAdditiveExpression(OCLParser.AdditiveExpressionContext context)
    {
        if (context.ChildCount == 1)
            return Visit(context.multiplicativeExpression());

        string left = Visit(context.additiveExpression());
        string op = context.GetChild(1).GetText();
        string right = Visit(context.multiplicativeExpression());
        return $"({left} {op} {right})";
    }

    public override string VisitMultiplicativeExpression(OCLParser.MultiplicativeExpressionContext context)
    {
        if (context.ChildCount == 1)
            return Visit(context.unaryExpression());

        string left = Visit(context.multiplicativeExpression());
        string op = context.GetChild(1).GetText();
        string right = Visit(context.unaryExpression());

        string csOp = op switch
        {
            "mod" => "%",
            "div" => "/",
            _     => op,
        };

        return $"({left} {csOp} {right})";
    }

    public override string VisitUnaryExpression(OCLParser.UnaryExpressionContext context)
    {
        if (context.ChildCount == 2 && context.GetChild(0).GetText() == "-")
            return $"-{Visit(context.unaryExpression())}";
        return Visit(context.postfixExpression());
    }

    public override string VisitPostfixExpression(OCLParser.PostfixExpressionContext context)
    {
        // base case
        if (context.ChildCount == 1)
            return Visit(context.primaryExpression());

        string receiver = Visit(context.postfixExpression());
        string separator = context.GetChild(1).GetText(); // '.' or '->'
        var call = context.callExpression();
        string memberName = call.NAME().GetText();

        if (separator == "->")
            return ConvertCollectionOperation(receiver, memberName, call);

        // Dot navigation
        if (call.ChildCount > 1)
        {
            // called with parentheses: NAME '(' argumentList? ')'
            string args = call.argumentList() != null ? Visit(call.argumentList()) : "";
            return $"{receiver}.{memberName}({args})";
        }
        return $"{receiver}.{memberName}";
    }

    public override string VisitPrimaryExpression(OCLParser.PrimaryExpressionContext context)
    {
        if (context.literalExpression() != null)
            return Visit(context.literalExpression());

        string first = context.GetChild(0).GetText();

        if (first == "self")
            return "this";

        if (first == "(")
            return $"({Visit(context.oclExpression())})";

        // NAME
        return first;
    }

    public override string VisitCallExpression(OCLParser.CallExpressionContext context)
    {
        string name = context.NAME().GetText();
        if (context.argumentList() != null)
            return $"{name}({Visit(context.argumentList())})";
        return name;
    }

    public override string VisitArgumentList(OCLParser.ArgumentListContext context)
    {
        return string.Join(", ", context.oclExpression().Select(Visit));
    }

    // ------------------------------------------------------------------ literals

    public override string VisitLiteralExpression(OCLParser.LiteralExpressionContext context)
    {
        if (context.collectionLiteralExpression() != null)
            return Visit(context.collectionLiteralExpression());

        string text = context.GetChild(0).GetText();

        // Convert OCL single-quoted strings to C# double-quoted strings
        if (text.StartsWith('\'') && text.EndsWith('\''))
        {
            string inner = text[1..^1]
                .Replace("\\\\", "\x00")   // temporarily replace \\
                .Replace("\\'", "'")        // unescape OCL escaped quote
                .Replace("\x00", "\\\\")   // restore \\
                .Replace("\"", "\\\"");    // escape double quotes for C#
            return $"\"{inner}\"";
        }

        return text;
    }

    public override string VisitCollectionLiteralExpression(OCLParser.CollectionLiteralExpressionContext context)
    {
        string kind = Visit(context.collectionKind());
        string parts = context.collectionLiteralParts() != null
            ? Visit(context.collectionLiteralParts())
            : "";

        return kind switch
        {
            "Set"        => $"new HashSet<object> {{ {parts} }}",
            "Bag"        => $"new List<object> {{ {parts} }}",
            "Sequence"   => $"new List<object> {{ {parts} }}",
            "OrderedSet" => $"new SortedSet<object> {{ {parts} }}",
            _            => $"new List<object> {{ {parts} }}",
        };
    }

    public override string VisitCollectionLiteralParts(OCLParser.CollectionLiteralPartsContext context)
    {
        return string.Join(", ", context.collectionLiteralPart().Select(Visit));
    }

    public override string VisitCollectionLiteralPart(OCLParser.CollectionLiteralPartContext context)
    {
        var exprs = context.oclExpression();
        if (exprs.Length == 2)
        {
            // Range: e1..e2  → Enumerable.Range(e1, e2 - e1 + 1)
            string from = Visit(exprs[0]);
            string to = Visit(exprs[1]);
            return $"/* {from}..{to} */";
        }
        return Visit(exprs[0]);
    }

    public override string VisitCollectionKind(OCLParser.CollectionKindContext context)
    {
        return context.GetChild(0).GetText();
    }

    // ------------------------------------------------------------------ types / names

    public override string VisitPathName(OCLParser.PathNameContext context)
    {
        return string.Join(".", context.NAME().Select(t => t.GetText()));
    }

    public override string VisitOperationName(OCLParser.OperationNameContext context)
    {
        return context.NAME().GetText();
    }

    public override string VisitReturnTypeSpecifier(OCLParser.ReturnTypeSpecifierContext context)
    {
        return Visit(context.typeSpecifier());
    }

    public override string VisitTypeSpecifier(OCLParser.TypeSpecifierContext context)
    {
        if (context.collectionTypeSpecifier() != null)
            return Visit(context.collectionTypeSpecifier());
        return Visit(context.pathName());
    }

    public override string VisitCollectionTypeSpecifier(OCLParser.CollectionTypeSpecifierContext context)
    {
        string kind = Visit(context.collectionKind());
        string inner = Visit(context.typeSpecifier());
        return kind switch
        {
            "Set"        => $"ISet<{inner}>",
            "Bag"        => $"IList<{inner}>",
            "Sequence"   => $"IList<{inner}>",
            "OrderedSet" => $"ISet<{inner}>",
            _            => $"IEnumerable<{inner}>",
        };
    }

    public override string VisitFormalParameterList(OCLParser.FormalParameterListContext context)
    {
        return string.Join(", ", context.formalParameter().Select(Visit));
    }

    public override string VisitFormalParameter(OCLParser.FormalParameterContext context)
    {
        string name = context.NAME().GetText();
        string type = Visit(context.typeSpecifier());
        return $"{type} {name}";
    }

    // ------------------------------------------------------------------ helpers

    /// <summary>
    /// Converts an OCL collection operation (via the <c>-&gt;</c> operator) to a C# LINQ call.
    /// </summary>
    private string ConvertCollectionOperation(string receiver, string operationName, OCLParser.CallExpressionContext call)
    {
        string? args = call.argumentList() != null ? Visit(call.argumentList()) : null;

        return operationName switch
        {
            "isEmpty"  => $"!{receiver}.Any()",
            "notEmpty" => $"{receiver}.Any()",
            "size"     => $"{receiver}.Count()",
            "sum"      => $"{receiver}.Sum()",
            "min"      => $"{receiver}.Min()",
            "max"      => $"{receiver}.Max()",
            "first"    => $"{receiver}.First()",
            "last"     => $"{receiver}.Last()",
            "asSet"    => $"{receiver}.Distinct()",
            "flatten"  => $"{receiver}.SelectMany(x => x)",
            "select"   when args != null => $"{receiver}.Where({args})",
            "reject"   when args != null => $"{receiver}.Where(x => !({args}))",
            "collect"  when args != null => $"{receiver}.Select({args})",
            "forAll"   when args != null => $"{receiver}.All({args})",
            "exists"   when args != null => $"{receiver}.Any({args})",
            "one"      when args != null => $"{receiver}.Count({args}) == 1",
            "any"      when args != null => $"{receiver}.FirstOrDefault({args})",
            "includes" when args != null => $"{receiver}.Contains({args})",
            "excludes" when args != null => $"!{receiver}.Contains({args})",
            "union"    when args != null => $"{receiver}.Union({args})",
            "intersection" when args != null => $"{receiver}.Intersect({args})",
            _ when args != null          => $"{receiver}.{operationName}({args})",
            _                            => $"{receiver}.{operationName}()",
        };
    }
}
