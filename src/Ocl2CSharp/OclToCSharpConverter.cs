using System.Text;
using Antlr4.Runtime;

namespace Ocl2CSharp;

/// <summary>
/// Converts OCL (Object Constraint Language) expressions to equivalent C# code.
/// Uses the ANTLR4 generated parser and the visitor pattern.
/// </summary>
public class OclToCSharpConverter : OCLBaseVisitor<string>
{
    /// <summary>
    /// Converts an OCL string containing one or more context specifications to C# code.
    /// </summary>
    /// <param name="oclCode">The OCL source text.</param>
    /// <returns>The equivalent C# code as a string.</returns>
    public static string Convert(string oclCode)
    {
        var inputStream = new AntlrInputStream(oclCode);
        var lexer = new OCLLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new OCLParser(tokenStream);

        var tree = parser.multipleContextSpecifications();
        var visitor = new OclToCSharpConverter();
        return visitor.Visit(tree);
    }

    // -------------------------------------------------------------------------
    // Top-level rules
    // -------------------------------------------------------------------------

    public override string VisitMultipleContextSpecifications(OCLParser.MultipleContextSpecificationsContext context)
    {
        var sb = new StringBuilder();
        foreach (var inv in context.singleInvariant())
            sb.AppendLine(Visit(inv));
        foreach (var der in context.singleDerivedAttribute())
            sb.AppendLine(Visit(der));
        return sb.ToString().TrimEnd();
    }

    public override string VisitSingleInvariant(OCLParser.SingleInvariantContext context)
    {
        // context ClassName inv invName?: expression
        var className = context.ID(0).GetText();
        var invName = context.ID().Length > 1 ? context.ID(1).GetText() : "Invariant";
        var expression = Visit(context.expression());

        var methodName = char.ToUpper(invName[0]) + invName.Substring(1);
        return $"public bool {methodName}()\n{{\n    return {expression};\n}}";
    }

    public override string VisitSingleDerivedAttribute(OCLParser.SingleDerivedAttributeContext context)
    {
        // context qualified_name : type (init: expression)? derive: expression
        var qualifiedName = Visit(context.qualified_name());
        var typeName = Visit(context.type());
        var expressions = context.expression();
        var deriveExpr = Visit(expressions[expressions.Length - 1]);

        // qualified_name is Class::attribute, extract just the attribute name
        var parts = qualifiedName.Split('.');
        var attrName = parts[parts.Length - 1];

        return $"public {typeName} {attrName}\n{{\n    get {{ return {deriveExpr}; }}\n}}";
    }

    // -------------------------------------------------------------------------
    // Type
    // -------------------------------------------------------------------------

    public override string VisitType(OCLParser.TypeContext context)
    {
        var child0 = context.GetChild(0).GetText();
        return child0 switch
        {
            "Sequence" => $"IList<{Visit(context.type(0))}>",
            "Set" => $"ISet<{Visit(context.type(0))}>",
            "Bag" => $"IList<{Visit(context.type(0))}>",
            "OrderedSet" => $"ISet<{Visit(context.type(0))}>",
            "Ref" => $"{Visit(context.type(0))}",
            "Map" => $"IDictionary<{Visit(context.type(0))}, {Visit(context.type(1))}>",
            "Function" => $"Func<{Visit(context.type(0))}, {Visit(context.type(1))}>",
            _ => Visit(context.identifier()),
        };
    }

    // -------------------------------------------------------------------------
    // Expressions
    // -------------------------------------------------------------------------

    public override string VisitExpression(OCLParser.ExpressionContext context)
    {
        return VisitChildren(context) ?? string.Empty;
    }

    public override string VisitConditionalExpression(OCLParser.ConditionalExpressionContext context)
    {
        // if condition then thenExpr else elseExpr endif
        var condition = Visit(context.expression(0));
        var thenExpr = Visit(context.expression(1));
        var elseExpr = Visit(context.expression(2));
        return $"({condition} ? {thenExpr} : {elseExpr})";
    }

    public override string VisitLetExpression(OCLParser.LetExpressionContext context)
    {
        // let var1 = expr1, var2 = expr2 in body
        // Translate to immediately-invoked lambda style isn't practical in C#,
        // so we emit a block with local variables (as a comment + inline expression for simple cases).
        var bindings = context.letBinding();
        var bodyExpr = Visit(context.expression());
        if (bindings.Length == 1)
        {
            var binding = Visit(bindings[0]);
            return $"(({binding}) => {bodyExpr})()";
        }

        var parts = string.Join(", ", Array.ConvertAll(bindings, b => Visit(b)));
        return $"/* let {parts} in */ {bodyExpr}";
    }

    public override string VisitLetBinding(OCLParser.LetBindingContext context)
    {
        // ID (':' type)? '=' expression
        var name = context.ID().GetText();
        var expr = Visit(context.expression());
        if (context.type() != null)
        {
            var typeName = Visit(context.type());
            return $"{typeName} {name} = {expr}";
        }
        return $"var {name} = {expr}";
    }

    public override string VisitLogicalExpression(OCLParser.LogicalExpressionContext context)
    {
        var operands = context.equalityExpression();
        if (operands.Length == 1)
            return Visit(operands[0]);

        var sb = new StringBuilder(Visit(operands[0]));
        for (int i = 1; i < operands.Length; i++)
        {
            var op = context.GetChild(2 * i - 1).GetText();
            var csharpOp = op switch
            {
                "and" or "&" => "&&",
                "or" => "||",
                "xor" => "^",
                "=>" or "implies" => "||",  // implies: !left || right — simplified here
                _ => op,
            };

            if (op is "=>" or "implies")
            {
                // a implies b  ↔  !a || b
                var left = sb.ToString();
                sb.Clear();
                sb.Append($"(!({left}) || {Visit(operands[i])})");
            }
            else
            {
                sb.Append($" {csharpOp} {Visit(operands[i])}");
            }
        }
        return sb.ToString();
    }

    public override string VisitEqualityExpression(OCLParser.EqualityExpressionContext context)
    {
        var operands = context.additiveExpression();
        if (operands.Length == 1)
            return Visit(operands[0]);

        var sb = new StringBuilder(Visit(operands[0]));
        for (int i = 1; i < operands.Length; i++)
        {
            var op = context.GetChild(2 * i - 1).GetText();
            var csharpOp = op switch
            {
                "=" => "==",
                "/=" or "<>" => "!=",
                "<" => "<",
                ">" => ">",
                ">=" => ">=",
                "<=" => "<=",
                ":" => "is",
                "/:" => "is not",
                "<:" => "/* subtype */",
                _ => op,
            };
            sb.Append($" {csharpOp} {Visit(operands[i])}");
        }
        return sb.ToString();
    }

    public override string VisitAdditiveExpression(OCLParser.AdditiveExpressionContext context)
    {
        var operands = context.multiplicativeExpression();
        if (operands.Length == 1)
            return Visit(operands[0]);

        var sb = new StringBuilder(Visit(operands[0]));
        for (int i = 1; i < operands.Length; i++)
        {
            var op = context.GetChild(2 * i - 1).GetText();
            var csharpOp = op switch
            {
                ".." => "/* .. */",
                "|->" => "/* |-> */",
                _ => op,
            };
            sb.Append($" {csharpOp} {Visit(operands[i])}");
        }
        return sb.ToString();
    }

    public override string VisitMultiplicativeExpression(OCLParser.MultiplicativeExpressionContext context)
    {
        var operands = context.unaryExpression();
        if (operands.Length == 1)
            return Visit(operands[0]);

        var sb = new StringBuilder(Visit(operands[0]));
        for (int i = 1; i < operands.Length; i++)
        {
            var op = context.GetChild(2 * i - 1).GetText();
            var csharpOp = op switch
            {
                "mod" => "%",
                "div" => "/",
                _ => op,
            };
            sb.Append($" {csharpOp} {Visit(operands[i])}");
        }
        return sb.ToString();
    }

    public override string VisitUnaryExpression(OCLParser.UnaryExpressionContext context)
    {
        if (context.navigationExpression() != null)
            return Visit(context.navigationExpression());

        var op = context.GetChild(0).GetText();
        var inner = Visit(context.unaryExpression());
        var csharpOp = op switch
        {
            "not" => "!",
            "-" => "-",
            "+" => "+",
            "?" => "/* ? */",
            "!" => "!",
            _ => op,
        };
        return $"{csharpOp}{inner}";
    }

    public override string VisitNavigationExpression(OCLParser.NavigationExpressionContext context)
    {
        var primary = Visit(context.primaryFactor());
        var suffixes = context.postfixSuffix();
        if (suffixes.Length == 0)
            return primary;

        var result = primary;
        foreach (var suffix in suffixes)
            result = ApplyPostfixSuffix(result, suffix);
        return result;
    }

    public override string VisitPrimaryFactor(OCLParser.PrimaryFactorContext context)
    {
        if (context.setExpression() != null)
            return Visit(context.setExpression());
        return Visit(context.basicExpression());
    }

    public override string VisitBasicExpression(OCLParser.BasicExpressionContext context)
    {
        // Simple literal tokens
        if (context.NULL_LITERAL() != null) return "null";
        if (context.BOOLEAN_LITERAL() != null) return context.BOOLEAN_LITERAL().GetText();
        if (context.INT() != null) return context.INT().GetText();
        if (context.FLOAT_LITERAL() != null) return context.FLOAT_LITERAL().GetText();
        if (context.STRING1_LITERAL() != null) return context.STRING1_LITERAL().GetText();
        if (context.STRING2_LITERAL() != null)
        {
            // Convert single-quoted OCL string to double-quoted C# string
            var raw = context.STRING2_LITERAL().GetText();
            var inner = raw.Substring(1, raw.Length - 2).Replace("\\\"", "\"").Replace("\"", "\\\"");
            return $"\"{inner}\"";
        }
        if (context.ENUMERATION_LITERAL() != null)
        {
            // Class::Value → Class.Value
            return context.ENUMERATION_LITERAL().GetText().Replace("::", ".");
        }

        // Recursive / composite cases
        var nestedBasic = context.basicExpression();
        if (nestedBasic == null)
        {
            // ID or '(' expression ')'
            if (context.ID() != null) return context.ID().GetText();
            // parenthesized expression
            if (context.expression() != null)
                return $"({Visit(context.expression())})";
        }
        else
        {
            var baseExpr = Visit(nestedBasic);
            var sep = context.GetChild(1).GetText();
            if (sep == ".")
            {
                // member access: basicExpression '.' ID
                return $"{baseExpr}.{context.ID().GetText()}";
            }
            if (sep == "(")
            {
                // method call: basicExpression '(' expressionList? ')'
                var args = context.expressionList() != null ? Visit(context.expressionList()) : string.Empty;
                return $"{baseExpr}({args})";
            }
            if (sep == "[")
            {
                // index: basicExpression '[' expression ']'
                return $"{baseExpr}[{Visit(context.expression())}]";
            }
        }

        return context.GetText();
    }

    public override string VisitExpressionList(OCLParser.ExpressionListContext context)
    {
        var exprs = context.expression();
        return string.Join(", ", Array.ConvertAll(exprs, e => Visit(e)));
    }

    // -------------------------------------------------------------------------
    // Postfix suffix helper
    // -------------------------------------------------------------------------

    private string ApplyPostfixSuffix(string target, OCLParser.PostfixSuffixContext context)
    {
        var op = context.GetChild(0).GetText();     // '.' or '->'
        var name = context.GetChild(1).GetText();   // operation name

        if (op == ".")
        {
            return name switch
            {
                "allInstances" => $"{target}.AllInstances()",
                "oclType" => $"{target}.GetType()",
                "oclIsUndefined" => $"({target} == null)",
                "oclIsInvalid" => $"({target} == null)",
                "oclIsNew" => $"/* oclIsNew({target}) */",
                "oclAsSet" => $"new HashSet<dynamic> {{ {target} }}",
                "oclIsTypeOf" => $"({target}.GetType() == typeof({Visit(context.expression(0))}))",
                "oclIsKindOf" => $"({target} is {Visit(context.expression(0))})",
                "oclAsType" => BuildOclAsType(target, context),
                "size" => $"{target}.Length",
                "max" => $"{target}.Max()",
                "min" => $"{target}.Min()",
                "indexOf" => $"{target}.IndexOf({Visit(context.expression(0))})",
                "at" => BuildAtAccess(target, context),
                _ => BuildDotMethodCall(target, context),
            };
        }
        else // "->"
        {
            return name switch
            {
                "size" => $"{target}.Count()",
                "isEmpty" => $"!{target}.Any()",
                "notEmpty" => $"{target}.Any()",
                "asSet" => $"{target}.ToHashSet()",
                "asBag" => $"{target}.ToList()",
                "asOrderedSet" => $"{target}.Distinct().ToHashSet()",
                "asSequence" => $"{target}.ToList()",
                "any" when context.identOptType() == null && context.expression().Length == 0 => $"{target}.FirstOrDefault()",
                "any" when context.identOptType() != null || context.expression().Length > 0 => BuildCollectionOp(target, "FirstOrDefault", context),
                "first" => BuildArrowSimpleOrChained(target, "First", context),
                "last" => BuildArrowSimpleOrChained(target, "Last", context),
                "reverse" => $"{target}.Reverse()",
                "floor" => $"Math.Floor({target})",
                "round" => $"Math.Round({target})",
                "abs" => $"Math.Abs({target})",
                "oclType" => $"{target}.GetType()",
                "oclIsUndefined" => $"({target} == null)",
                "oclIsInvalid" => $"({target} == null)",
                "oclIsNew" => $"/* oclIsNew({target}) */",
                "sum" => $"{target}.Sum()",
                "max" => $"{target}.Max()",
                "min" => $"{target}.Min()",
                "characters" => $"{target}.ToCharArray()",
                "toInteger" => $"Convert.ToInt32({target})",
                "toReal" => $"Convert.ToDouble({target})",
                "toBoolean" => $"Convert.ToBoolean({target})",
                "toUpperCase" => $"{target}.ToUpper()",
                "toLowerCase" => $"{target}.ToLower()",
                "union" => $"{target}.Union({Visit(context.expression(0))})",
                "intersection" => $"{target}.Intersect({Visit(context.expression(0))})",
                "includes" => $"{target}.Contains({Visit(context.expression(0))})",
                "excludes" => $"!{target}.Contains({Visit(context.expression(0))})",
                "including" => $"{target}.Append({Visit(context.expression(0))})",
                "excluding" => BuildExcluding(target, context),
                "includesAll" => $"{target}.IsSupersetOf({Visit(context.expression(0))})",
                "symmetricDifference" => BuildSymmetricDifference(target, context),
                "excludesAll" => $"!{target}.Intersect({Visit(context.expression(0))}).Any()",
                "prepend" => $"{target}.Prepend({Visit(context.expression(0))})",
                "append" => $"{target}.Append({Visit(context.expression(0))})",
                "count" => BuildCount(target, context),
                "indexOf" => $"{target}.ToList().IndexOf({Visit(context.expression(0))})",
                "equalsIgnoreCase" => $"{target}.Equals({Visit(context.expression(0))}, StringComparison.OrdinalIgnoreCase)",
                "oclAsType" => $"(({Visit(context.expression(0))}){target})",
                "at" => $"{target}.ElementAt({Visit(context.expression(0))} - 1)",
                "oclIsTypeOf" => $"({target}.GetType() == typeof({Visit(context.expression(0))}))",
                "oclIsKindOf" => $"({target} is {Visit(context.expression(0))})",
                "oclAsSet" => $"{target}.ToHashSet()",
                "collect" => BuildCollectionOp(target, "Select", context),
                "select" => BuildCollectionOp(target, "Where", context),
                "reject" => BuildReject(target, context),
                "forAll" => BuildCollectionOp(target, "All", context),
                "exists" => BuildCollectionOp(target, "Any", context),
                "one" => BuildOne(target, context),
                "closure" => BuildClosure(target, context),
                "sortedBy" => BuildCollectionOp(target, "OrderBy", context),
                "isUnique" => BuildIsUnique(target, context),
                "insertAt" => BuildInsertAt(target, context),
                "iterate" => BuildIterate(target, context),
                _ => BuildArrowGenericCall(target, context),
            };
        }
    }

    // -------------------------------------------------------------------------
    // Postfix helper methods
    // -------------------------------------------------------------------------

    private string BuildOclAsType(string target, OCLParser.PostfixSuffixContext context)
    {
        var expr = Visit(context.expression(0));
        // Check if there's a chained .ID after the cast
        var ids = context.ID();
        if (ids.Length > 0)
            return $"(({expr}){target}).{ids[0].GetText()}";
        return $"(({expr}){target})";
    }

    private string BuildAtAccess(string target, OCLParser.PostfixSuffixContext context)
    {
        var expr = Visit(context.expression(0));
        var ids = context.ID();
        var access = ids.Length > 0 ? $".{ids[0].GetText()}" : string.Empty;
        return $"{target}[{expr}]{access}";
    }

    private string BuildDotMethodCall(string target, OCLParser.PostfixSuffixContext context)
    {
        // '.' ID '(' (expression (',' expression)*)? ')' ('.' ID)?
        // or '.' ID
        var ids = context.ID();
        if (ids.Length == 0)
            return $"{target}.{context.GetChild(1).GetText()}";

        var methodName = ids[0].GetText();
        var exprs = context.expression();

        // Check for '(' in the context — child at index 2 would be '('
        if (context.ChildCount > 2 && context.GetChild(2).GetText() == "(")
        {
            var args = string.Join(", ", Array.ConvertAll(exprs, e => Visit(e)));
            var suffix = ids.Length > 1 ? $".{ids[1].GetText()}" : string.Empty;
            return $"{target}.{methodName}({args}){suffix}";
        }

        // Simple '.' ID (property access)
        return $"{target}.{methodName}";
    }

    private string BuildArrowSimpleOrChained(string target, string csMethod, OCLParser.PostfixSuffixContext context)
    {
        var ids = context.ID();
        var suffix = ids.Length > 0 ? $".{ids[0].GetText()}" : string.Empty;
        return $"{target}.{csMethod}(){suffix}";
    }

    private string BuildCollectionOp(string target, string csMethod, OCLParser.PostfixSuffixContext context)
    {
        // ->op( (identOptType | identOptTypeList '|')? expression )
        var identOpt = context.identOptType();
        var identOptList = context.identOptTypeList();
        var exprs = context.expression();
        string lambda;
        if (identOpt != null && exprs.Length > 0)
        {
            var varName = identOpt.ID().GetText();
            lambda = $"{varName} => {Visit(exprs[0])}";
        }
        else if (identOptList != null && exprs.Length > 0)
        {
            // Use the first variable from the list as the primary lambda parameter
            var firstVar = identOptList.identOptType(0).ID().GetText();
            lambda = $"{firstVar} => {Visit(exprs[0])}";
        }
        else if (exprs.Length > 0)
        {
            lambda = $"it => {Visit(exprs[0])}";
        }
        else
        {
            return $"{target}.{csMethod}()";
        }

        // Handle possible chained .ID (for any, first, last)
        var ids = context.ID();
        var suffix = ids.Length > 0 ? $".{ids[0].GetText()}" : string.Empty;
        return $"{target}.{csMethod}({lambda}){suffix}";
    }

    private string BuildReject(string target, OCLParser.PostfixSuffixContext context)
    {
        var identOpt = context.identOptType();
        var exprs = context.expression();
        string lambda;
        if (identOpt != null && exprs.Length > 0)
        {
            var varName = identOpt.ID().GetText();
            lambda = $"{varName} => !({Visit(exprs[0])})";
        }
        else if (exprs.Length > 0)
        {
            lambda = $"it => !({Visit(exprs[0])})";
        }
        else
        {
            return $"{target}.Where(/* reject */ it => true)";
        }
        return $"{target}.Where({lambda})";
    }

    private string BuildExcluding(string target, OCLParser.PostfixSuffixContext context)
    {
        var expr = Visit(context.expression(0));
        return $"{target}.Where(it => it != {expr})";
    }

    private string BuildSymmetricDifference(string target, OCLParser.PostfixSuffixContext context)
    {
        var expr = Visit(context.expression(0));
        return $"{target}.Except({expr}).Union({expr}.Except({target}))";
    }

    private string BuildCount(string target, OCLParser.PostfixSuffixContext context)
    {
        var exprs = context.expression();
        if (exprs.Length > 0)
            return $"{target}.Count(it => it == {Visit(exprs[0])})";
        return $"{target}.Count()";
    }

    private string BuildOne(string target, OCLParser.PostfixSuffixContext context)
    {
        var identOpt = context.identOptType();
        var exprs = context.expression();
        string lambda;
        if (identOpt != null && exprs.Length > 0)
        {
            var varName = identOpt.ID().GetText();
            lambda = $"{varName} => {Visit(exprs[0])}";
        }
        else if (exprs.Length > 0)
        {
            lambda = $"it => {Visit(exprs[0])}";
        }
        else
        {
            return $"({target}.Count() == 1)";
        }
        return $"({target}.Count({lambda}) == 1)";
    }

    private string BuildClosure(string target, OCLParser.PostfixSuffixContext context)
    {
        var identOpt = context.identOptType();
        var exprs = context.expression();
        if (identOpt != null && exprs.Length > 0)
        {
            var varName = identOpt.ID().GetText();
            return $"/* closure */ {target}.SelectMany({varName} => {Visit(exprs[0])})";
        }
        return $"/* closure */ {target}";
    }

    private string BuildIsUnique(string target, OCLParser.PostfixSuffixContext context)
    {
        var identOpt = context.identOptType();
        var exprs = context.expression();
        string selector;
        if (identOpt != null && exprs.Length > 0)
        {
            var varName = identOpt.ID().GetText();
            selector = $"{varName} => {Visit(exprs[0])}";
        }
        else if (exprs.Length > 0)
        {
            selector = $"it => {Visit(exprs[0])}";
        }
        else
        {
            return $"({target}.Distinct().Count() == {target}.Count())";
        }
        return $"({target}.Select({selector}).Distinct().Count() == {target}.Count())";
    }

    private string BuildInsertAt(string target, OCLParser.PostfixSuffixContext context)
    {
        var exprs = context.expression();
        var pos = Visit(exprs[0]);
        var elem = Visit(exprs[1]);
        return $"/* insertAt */ {target}.Take({pos} - 1).Append({elem}).Concat({target}.Skip({pos} - 1))";
    }

    private string BuildIterate(string target, OCLParser.PostfixSuffixContext context)
    {
        // ->iterate( identifier ; identOptType = expression | expression )
        var accId = Visit(context.identifier());
        var iterVar = context.identOptType();
        var exprs = context.expression();
        var initExpr = Visit(exprs[0]);
        var bodyExpr = Visit(exprs[1]);
        if (iterVar != null)
        {
            var varName = iterVar.ID().GetText();
            return $"{target}.Aggregate({initExpr}, ({accId}, {varName}) => {bodyExpr})";
        }
        return $"{target}.Aggregate({initExpr}, ({accId}, it) => {bodyExpr})";
    }

    private string BuildArrowGenericCall(string target, OCLParser.PostfixSuffixContext context)
    {
        // '->' ID '(' (expression (',' expression)*)? ')' ('.' ID)?
        var name = context.GetChild(1).GetText();
        var exprs = context.expression();
        var args = string.Join(", ", Array.ConvertAll(exprs, e => Visit(e)));
        var ids = context.ID();
        var suffix = ids.Length > 0 ? $".{ids[0].GetText()}" : string.Empty;
        return $"{target}.{name}({args}){suffix}";
    }

    // -------------------------------------------------------------------------
    // IdentOptType / IdentOptTypeList
    // -------------------------------------------------------------------------

    public override string VisitIdentOptType(OCLParser.IdentOptTypeContext context)
    {
        var id = context.ID().GetText();
        if (context.type() != null)
            return $"{Visit(context.type())} {id}";
        return id;
    }

    public override string VisitIdentOptTypeList(OCLParser.IdentOptTypeListContext context)
    {
        var items = context.identOptType();
        return string.Join(", ", Array.ConvertAll(items, i => Visit(i)));
    }

    // -------------------------------------------------------------------------
    // Set/Collection literals
    // -------------------------------------------------------------------------

    public override string VisitSetExpression(OCLParser.SetExpressionContext context)
    {
        var kind = context.GetChild(0).GetText();
        var items = context.expressionList() != null ? Visit(context.expressionList()) : string.Empty;
        return kind switch
        {
            "Set{" => $"new HashSet<dynamic> {{ {items} }}",
            "OrderedSet{" => $"new List<dynamic> {{ {items} }}", // OrderedSet preserves insertion order
            "Bag{" => $"new List<dynamic> {{ {items} }}",
            "Sequence{" => $"new List<dynamic> {{ {items} }}",
            "Map{" => $"new Dictionary<dynamic, dynamic> {{ {items} }}",
            _ => $"new List<dynamic> {{ {items} }}",
        };
    }

    // -------------------------------------------------------------------------
    // Identifier / qualified_name
    // -------------------------------------------------------------------------

    public override string VisitIdentifier(OCLParser.IdentifierContext context)
    {
        return context.ID().GetText();
    }

    public override string VisitQualified_name(OCLParser.Qualified_nameContext context)
    {
        // ENUMERATION_LITERAL = ID '::' ID → convert to 'ClassName.attribute'
        return context.ENUMERATION_LITERAL().GetText().Replace("::", ".");
    }
}
