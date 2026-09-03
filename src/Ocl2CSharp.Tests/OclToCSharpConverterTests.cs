using NUnit.Framework;
using Ocl2CSharp;
using System.Linq;

namespace Ocl2CSharp.Tests;

public class OclToCSharpConverterTests
{
    // -------------------------------------------------------------------------
    // Logical operators
    // -------------------------------------------------------------------------

    [Test]
    public void LogicalAnd_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0 and age <= 150", new ConversionOptions());
        Assert.AreEqual("Age >= 0 && Age <= 150;", result);
    }

    [Test]
    public void LogicalAnd_Ampersand_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0 & age <= 150", new ConversionOptions());
        Assert.AreEqual("Age >= 0 && Age <= 150;", result);
    }

    [Test]
    public void LogicalOr_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("email <> null or phone <> null", new ConversionOptions());
        Assert.AreEqual("Email != null || Phone != null;", result);
    }

    [Test]
    public void LogicalXor_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("a or b xor c", new ConversionOptions());
        Assert.That(result, Does.Contain("^"));
    }

    [Test]
    public void ImpliesOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 18 implies canVote = true", new ConversionOptions());
        Assert.That(result, Does.Contain("!(Age >= 18)"));
        Assert.That(result, Does.Contain("CanVote == true"));
    }

    [Test]
    public void ImpliesArrow_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 18 => canVote = true", new ConversionOptions());
        Assert.That(result, Does.Contain("!(Age >= 18)"));
        Assert.That(result, Does.Contain("CanVote == true"));
    }

    // -------------------------------------------------------------------------
    // Equality / comparison operators
    // -------------------------------------------------------------------------

    [Test]
    public void Equality_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = 'Alice'", new ConversionOptions());
        Assert.AreEqual("Name == \"Alice\";", result);
    }

    [Test]
    public void Inequality_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null", new ConversionOptions());
        Assert.AreEqual("Name != null;", result);
    }

    [Test]
    public void Inequality_SlashEquals_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value /= 0", new ConversionOptions());
        Assert.AreEqual("Value != 0;", result);
    }

    [Test]
    public void LessThan_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Age < 18", new ConversionOptions());
        Assert.AreEqual("Age < 18;", result);
    }

    [Test]
    public void GreaterThanOrEqual_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Age >= 0", new ConversionOptions());
        Assert.AreEqual("Age >= 0;", result);
    }

    // -------------------------------------------------------------------------
    // Arithmetic operators
    // -------------------------------------------------------------------------

    [Test]
    public void Addition_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Balance + Interest >= 0", new ConversionOptions());
        Assert.AreEqual("Balance + Interest >= 0;", result);
    }

    [Test]
    public void Multiplication_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Balance * Rate > 0", new ConversionOptions());
        Assert.AreEqual("Balance * Rate > 0;", result);
    }

    [Test]
    public void ModOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance mod 2 = 0", new ConversionOptions());
        Assert.AreEqual("Balance % 2 == 0;", result);
    }

    [Test]
    public void DivOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("total div numItems > 0", new ConversionOptions());
        Assert.AreEqual("Total / NumItems > 0;", result);
    }

    // -------------------------------------------------------------------------
    // Unary operators
    // -------------------------------------------------------------------------

    [Test]
    public void NotOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("not active", new ConversionOptions());
        Assert.AreEqual("!Active;", result);
    }

    [Test]
    public void UnaryMinus_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("-Balance > 0", new ConversionOptions());
        Assert.AreEqual("-Balance > 0;", result);
    }

    // -------------------------------------------------------------------------
    // Conditional expression
    // -------------------------------------------------------------------------

    [Test]
    public void ConditionalExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif", new ConversionOptions());
        Assert.AreEqual("(Age >= 18 ? IsAdult : IsMinor);", result);
    }

    [Test]
    public void ConditionalExpression_UseIfStatement_EmitsIfElseBlock()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif", new ConversionOptions { UseIfStatement = true }).Replace("\r", "");
        Assert.AreEqual("if (Age >= 18)\n{\n\tIsAdult;\n}\nelse\n{\n\tIsMinor;\n}", result);
    }
    
    [Test]
    public void ConditionalExpression_UseIfStatement_EmitsIfElseBlock_WithReturn()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif", new ConversionOptions { UseIfStatement = true, CodeWithReturn = true}).Replace("\r", "");
        Assert.AreEqual("if (Age >= 18)\n{\n\treturn IsAdult;\n}\nelse\n{\n\treturn IsMinor;\n}", result);
    }

    [Test]
    public void ConditionalExpression_DefaultMode_EmitsTernary()
    {
        // Verify that the default (no second argument) still produces ternary output.
        var result = OclToCSharpConverter.Convert("if x > 0 then positive else nonPositive endif", new ConversionOptions());
        Assert.AreEqual("(X > 0 ? Positive : NonPositive);", result);
    }

    // -------------------------------------------------------------------------
    // Literals
    // -------------------------------------------------------------------------

    [Test]
    public void NullLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null", new ConversionOptions());
        Assert.AreEqual("Name != null;", result);
    }

    [Test]
    public void BooleanLiteralTrue_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = true", new ConversionOptions());
        Assert.AreEqual("Active == true;", result);
    }

    [Test]
    public void BooleanLiteralFalse_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = false", new ConversionOptions());
        Assert.AreEqual("Active == false;", result);
    }

    [Test]
    public void IntLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Balance > 0", new ConversionOptions());
        Assert.AreEqual("Balance > 0;", result);
    }

    [Test]
    public void StringLiteralSingleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = 'Alice'", new ConversionOptions());
        Assert.AreEqual("Name == \"Alice\";", result);
    }

    [Test]
    public void StringLiteralDoubleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = \"Alice\"", new ConversionOptions());
        Assert.AreEqual("Name == \"Alice\";", result);
    }

    [Test]
    public void EnumerationLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("status = Status::Active", new ConversionOptions());
        Assert.AreEqual("Status == Status.Active;", result);
    }

    // -------------------------------------------------------------------------
    // Navigation / member access
    // -------------------------------------------------------------------------

    [Test]
    public void MemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ceo.name <> null", new ConversionOptions());
        Assert.AreEqual("Ceo.Name != null;", result);
    }

    [Test]
    public void NestedMemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("company.ceo.name <> null", new ConversionOptions());
        Assert.AreEqual("Company.Ceo.Name != null;", result);
    }

    // -------------------------------------------------------------------------
    // Collection operations — arrow (->)
    // -------------------------------------------------------------------------

    [Test]
    public void SelectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->select(e | e.age >= 18)->notEmpty()", new ConversionOptions());
        Assert.That(result, Does.Contain(".Where(e => e.Age >= 18)"));
        Assert.That(result, Does.Contain(".NotEmpty()"));
    }

    [Test]
    public void ForAllOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->forAll(e | e.age >= 18)", new ConversionOptions());
        Assert.AreEqual("Employees.All(e => e.Age >= 18);", result);
    }

    [Test]
    public void ExistsOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->exists(e | e.role = 'CEO')", new ConversionOptions());
        Assert.AreEqual("Employees.Exists(e => e.Role == \"CEO\");", result);
    }

    [Test]
    public void CollectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->collect(e | e.name)->notEmpty()", new ConversionOptions());
        Assert.That(result, Does.Contain(".Select(e => e.Name)"));
        Assert.That(result, Does.Contain(".NotEmpty()"));
    }

    [Test]
    public void SizeOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->size() > 0", new ConversionOptions());
        Assert.AreEqual("Employees.Count() > 0;", result);
    }

    [Test]
    public void IsEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->isEmpty()", new ConversionOptions());
        Assert.AreEqual("Contracts.IsEmpty();", result);
    }

    [Test]
    public void NotEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->notEmpty()", new ConversionOptions());
        Assert.AreEqual("Contracts.NotEmpty();", result);
    }

    [Test]
    public void IncludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->includes(alice)", new ConversionOptions());
        Assert.AreEqual("Employees.Contains(Alice);", result);
    }

    [Test]
    public void ExcludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->excludes(alice)", new ConversionOptions());
        Assert.AreEqual("!Employees.Contains(Alice);", result);
    }

    [Test]
    public void SumOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("salaries->sum() > 0", new ConversionOptions());
        Assert.AreEqual("Salaries.Sum() > 0;", result);
    }

    [Test]
    public void MaxOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->max()", new ConversionOptions());
        Assert.AreEqual("Scores.Max();", result);
    }

    [Test]
    public void MinOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->min()", new ConversionOptions());
        Assert.AreEqual("Scores.Min();", result);
    }

    [Test]
    public void RejectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->reject(e | e.age < 18)->isEmpty()", new ConversionOptions());
        Assert.That(result, Does.Contain(".Reject(e => e.Age < 18)"));
        Assert.That(result, Does.Contain(".IsEmpty()"));
    }

    [Test]
    public void AsSetOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("items->asSet()", new ConversionOptions());
        Assert.AreEqual("Items.ToHashSet();", result);
    }

    [Test]
    public void AsSequenceOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("items->asSequence()", new ConversionOptions());
        Assert.AreEqual("Items.ToList();", result);
    }

    [Test]
    public void ToUpperCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toUpperCase()", new ConversionOptions());
        Assert.AreEqual("Name.ToUpper();", result);
    }

    [Test]
    public void ToLowerCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toLowerCase()", new ConversionOptions());
        Assert.AreEqual("Name.ToLower();", result);
    }

    [Test]
    public void ToIntegerOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value->toInteger()", new ConversionOptions());
        Assert.AreEqual("Convert.ToInt32(Value);", result);
    }

    // -------------------------------------------------------------------------
    // Dot (.) OCL operations
    // -------------------------------------------------------------------------

    [Test]
    public void OclIsUndefined_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name.oclIsUndefined() = false", new ConversionOptions());
        Assert.AreEqual("(Name == null) == false;", result);
    }

    [Test]
    public void OclIsKindOf_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("obj.oclIsKindOf(Person)", new ConversionOptions());
        Assert.AreEqual("(Obj is Person);", result);
    }

    // -------------------------------------------------------------------------
    // Collection literals
    // -------------------------------------------------------------------------

    [Test]
    public void SetLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Set{1, 2, 3}->includes(status)", new ConversionOptions());
        Assert.That(result, Does.Contain("new HashSet<dynamic> "));
        Assert.That(result, Does.Contain(".Contains(Status)"));
    }

    [Test]
    public void OrderedSetLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("OrderedSet{1, 2, 3}->size() > 0", new ConversionOptions());
        Assert.That(result, Does.Contain("new List<dynamic> "));
        Assert.That(result, Does.Contain(".Count()"));
    }

    [Test]
    public void AsSet_UsesOclExtension()
    {
        var result = OclToCSharpConverter.Convert("items->asSet()", new ConversionOptions());
        Assert.That(result, Does.Contain(".ToHashSet()"));
    }

    [Test]
    public void AsOrderedSet_UsesOclExtension()
    {
        var result = OclToCSharpConverter.Convert("items->asOrderedSet()", new ConversionOptions());
        Assert.That(result, Does.Contain(".AsOrderedSet()"));
    }

    [Test]
    public void SequenceLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Sequence{1, 2, 3}->size() > 0", new ConversionOptions());
        Assert.That(result, Does.Contain("new List<dynamic>"));
        Assert.That(result, Does.Contain(".Count()"));
    }

    [Test]
    public void BagLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Bag{1, 2, 3}->size() > 0", new ConversionOptions());
        Assert.That(result, Does.Contain("new List<dynamic>"));
    }

    // -------------------------------------------------------------------------
    // Let expressions
    // -------------------------------------------------------------------------

    [Test]
    public void LetExpression_WithTypedBinding_ReturnsLinqExpression()
    {
        var ocl =
            "let renderings: OrderedSet(ViewRenderingMembership) = " +
            "featureMembership->selectByKind(ViewRenderingMembership) in " +
            "if renderings->isEmpty() then null " +
            "else renderings->first().referencedRendering endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "FeatureMembership.OfType<ViewRenderingMembership>().Select(renderings => (renderings.IsEmpty() ? null : renderings.First().ReferencedRendering));",
            result);
    }

    // -------------------------------------------------------------------------
    // Parenthesized expression
    // -------------------------------------------------------------------------

    [Test]
    public void ParenthesizedExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("(Age + 1) > 0", new ConversionOptions());
		Assert.AreEqual("(Age + 1) > 0;", result);
	}

    // -------------------------------------------------------------------------
    // Chained operations
    // -------------------------------------------------------------------------

    [Test]
    public void ChainedSelectAndForAll_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->select(e | e.active = true)->forAll(e | e.age >= 18)", new ConversionOptions());
        Assert.That(result, Does.Contain(".Where(e => e.Active == true)"));
        Assert.That(result, Does.Contain(".All(e => e.Age >= 18)"));
    }


    [Test]
    public void SelectByKind_IsCoverted()
    {
		var result = OclToCSharpConverter.Convert("ownedMembership->selectByKind(VariantMembership)", new ConversionOptions());
		Assert.AreEqual("OwnedMembership.OfType<VariantMembership>();", result);
	}


    // -------------------------------------------------------------------------
    // SysML OCL constraints (from SysML_OCLAndCSharp.md, up to CheckRequirementUsageObjectiveRedefinition)
    // -------------------------------------------------------------------------

    [Test]
    public void SysML_ValidateEventOccurrenceUsageReference()
    {
        var ocl =
            "referencedFeatureTarget() <> null implies\n" +
            "    referencedFeatureTarget().oclIsKindOf(OccurrenceUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is OccurrenceUsage));",
            result);
    }

    [Test]
    public void SysML_DeriveCalculationUsageCalculation()
    {
        var ocl =
            "calculation = action->selectByKind(CalculationUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "Calculation == Action.OfType<CalculationUsage>();",
            result);
    }

    [Test]
    public void SysML_ValidateSubjectMembershipOwningType()
    {
        var ocl =
            "owningType.oclIsType(RequirementDefinition) or\n" +
            "owningType.oclIsType(RequiremenCaseRequirementDefinition) or\n" +
            "owningType.oclIsType(CaseDefinition) or\n" +
            "owningType.oclIsType(CaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(OwningType is RequirementDefinition) || (OwningType is RequiremenCaseRequirementDefinition) || (OwningType is CaseDefinition) || (OwningType is CaseUsage);",
            result);
    }

    [Test]
    public void SysML_DeriveFeatureReferenceExpressionReferent()
    {
        var ocl =
            "referent =\n" +
            "    let nonParameterMemberships : Sequence(Membership) = ownedMembership->\n" +
            "        reject(oclIsKindOf(ParameterMembership)) in\n" +
            "    if nonParameterMemberships->isEmpty() or\n" +
            "       not nonParameterMemberships->first().memberElement.oclIsKindOf(Feature)\n" +
            "    then null\n" +
            "    else nonParameterMemberships->first().memberElement.oclAsType(Feature)\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "Referent == OwnedMembership.Reject(item => (item is ParameterMembership)).Select(nonParameterMemberships => (nonParameterMemberships.IsEmpty() || !(nonParameterMemberships.First().MemberElement is Feature) ? null : (nonParameterMemberships.First().MemberElement as Feature)));",
            result);
    }

    [Test]
    public void SysML_DeriveTypeOwnedIntersecting()
    {
        var ocl =
            "ownedRelationship->selectByKind(Intersecting)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedRelationship.OfType<Intersecting>();",
            result);
    }

    [Test]
    public void SysML_ValidatePortDefinitionConjugatedPortDefinition()
    {
        var ocl =
            "not oclIsKindOf(ConjugatedPortDefinition) implies\n" +
            "    ownedMember->\n" +
            "        selectByKind(ConjugatedPortDefinition)->\n" +
            "        size() = 1";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(!(this is ConjugatedPortDefinition)) || OwnedMember.OfType<ConjugatedPortDefinition>().Count() == 1);",
            result);
    }

    [Test]
    public void SysML_CheckFeatureValuationSpecialization()
    {
        var ocl =
            "direction = null and\n" +
            "ownedSpecializations->forAll(isImplied) implies\n" +
            "    ownedMembership->\n" +
            "        selectByKind(FeatureValue)->\n" +
            "        forAll(fv | specializes(fv.value.result))";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(Direction == null && OwnedSpecializations.All(item => item.IsImplied)) || OwnedMembership.OfType<FeatureValue>().All(fv => Specializes(fv.Value.Result)));",
            result);
    }

    [Test]
    public void SysML_DeriveFeatureOwnedTypeFeaturing()
    {
        var ocl =
            "ownedTypeFeaturing = ownedRelationship->selectByKind(TypeFeaturing)->\n" +
            "    select(tf | tf.featureOfType = self)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedTypeFeaturing == OwnedRelationship.OfType<TypeFeaturing>().Where(tf => tf.FeatureOfType == this);",
            result);
    }

    [Test]
    public void SysML_CheckMetadataAccessExpressionSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Performances::metadataAccessEvaluations')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SpecializesFromLibrary(\"Performances::metadataAccessEvaluations\");",
            result);
    }

    [Test]
    public void SysML_ValidateConstructorExpressionOwnedFeatures()
    {
        var ocl =
            "ownedFeatures->excluding(result)->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedFeatures.Excluding(Result).IsEmpty();",
            result);
    }

    [Test]
    public void SysML_ValidateAssociationBinarySpecialization()
    {
        var ocl =
            "associationEnds->size() > 2 implies\n" +
            "    not specializesFromLibrary('Links::BinaryLink')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(AssociationEnds.Count() > 2) || !SpecializesFromLibrary(\"Links::BinaryLink\"));",
            result);
    }

    [Test]
    public void SysML_DeriveCaseDefinitionSubjectParameter()
    {
        var ocl =
            "subjectParameter =\n" +
            "    let subjectMems : OrderedSet(SubjectMembership) = \n" +
            "        featureMembership->selectByKind(SubjectMembership) in\n" +
            "    if subjectMems->isEmpty() then null\n" +
            "    else subjectMems->first().ownedSubjectParameter\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SubjectParameter == FeatureMembership.OfType<SubjectMembership>().Select(subjectMems => (subjectMems.IsEmpty() ? null : subjectMems.First().OwnedSubjectParameter));",
            result);
    }

    [Test]
    public void SysML_DeriveDefinitionOwnedConnection()
    {
        IEnumerable<string> test;
        var ocl =
            "ownedConnection = ownedUsage->selectByKind(ConnectorAsUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedConnection == OwnedUsage.OfType<ConnectorAsUsage>();",
            result);
    }

    [Test]
    public void SysML_DeriveDefinitionOwnedConstraint()
    {
        var ocl =
            "ownedConstraint = ownedUsage->selectByKind(ConstraintUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedConstraint == OwnedUsage.OfType<ConstraintUsage>();",
            result);
    }

    [Test]
    public void SysML_CheckOccurrenceDefinitionMultiplicitySpecialization()
    {
        var ocl =
            "isIndividual implies\n" +
            "    multiplicity <> null and\n" +
            "    multiplicity.specializesFromLibrary('Base::zeroOrOne')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(IsIndividual) || Multiplicity != null) && Multiplicity.SpecializesFromLibrary(\"Base::zeroOrOne\");",
            result);
    }

    [Test]
    public void SysML_ValidateSpecificationSpecificNotConjugated()
    {
        var ocl =
            "not specific.isConjugated";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "!Specific.IsConjugated;",
            result);
    }

    [Test]
    public void SysML_DeriveInstantiationExpressionInstantiatedType()
    {
        var ocl =
            "instantiatedType = instantiatedType()";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "InstantiatedType == InstantiatedType();",
            result);
    }

    [Test]
    public void SysML_ValidateConnectorBinarySpecialization()
    {
        var ocl =
            "connectorEnds->size() > 2 implies\n" +
            "    not specializesFromLibrary('Links::BinaryLink')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(ConnectorEnds.Count() > 2) || !SpecializesFromLibrary(\"Links::BinaryLink\"));",
            result);
    }

    [Test]
    public void SysML_DeriveElementOwnedElement()
    {
        var ocl =
            "ownedElement = ownedRelationship.ownedRelatedElement";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedElement == OwnedRelationship.OwnedRelatedElement;",
            result);
    }

    [Test]
    public void SysML_CheckTransitionUsageStateSpecialization()
    {
        var ocl =
            "isComposite and owningType <> null and\n" +
            "(owningType.oclIsKindOf(StateDefinition) or\n" +
            " owningType.oclIsKindOf(StateUsage)) and\n" +
            "source <> null and source.oclIsKindOf(StateUsage) implies\n" +
            "    specializesFromLibrary('States::StateAction::stateTransitions')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(IsComposite && OwningType != null && ((OwningType is StateDefinition) || (OwningType is StateUsage)) && Source != null && (Source is StateUsage)) || SpecializesFromLibrary(\"States::StateAction::stateTransitions\"));",
            result);
    }

    [Test]
    public void SysML_DeriveRequirementDefinitionFramedConcern()
    {
        var ocl =
            "framedConcern = featureMembership->\n" +
            "    selectByKind(FramedConcernMembership).\n" +
            "    ownedConcern";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "FramedConcern == FeatureMembership.OfType<FramedConcernMembership>().Select(item => item.OwnedConcern);",
            result);
    }

    [Test]
    public void SysML_CheckUsageVariationUsageSpecialization()
    {
        var ocl =
            "owningVariationUsage <> null implies\n" +
            "    specializes(owningVariationUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(OwningVariationUsage != null) || Specializes(OwningVariationUsage));",
            result);
    }

    [Test]
    public void SysML_DeriveFeatureChainExpressionTargetFeature()
    {
        var ocl =
            "targetFeature =\n" +
            "    let nonParameterMemberships : Sequence(Membership) = ownedMembership->\n" +
            "        reject(oclIsKindOf(ParameterMembership)) in\n" +
            "    if nonParameterMemberships->isEmpty() or\n" +
            "       not nonParameterMemberships->first().memberElement.oclIsKindOf(Feature)\n" +
            "    then null\n" +
            "    else nonParameterMemberships->first().memberElement.oclAsType(Feature)\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "TargetFeature == OwnedMembership.Reject(item => (item is ParameterMembership)).Select(nonParameterMemberships => (nonParameterMemberships.IsEmpty() || !(nonParameterMemberships.First().MemberElement is Feature) ? null : (nonParameterMemberships.First().MemberElement as Feature)));",
            result);
    }

    [Test]
    public void SysML_DeriveUsageNestedAllocation()
    {
        var ocl =
            "nestedAllocation = nestedUsage->selectByKind(AllocationUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "NestedAllocation == NestedUsage.OfType<AllocationUsage>();",
            result);
    }

    [Test]
    public void SysML_ValidateStateDefinitionParallelSubactions()
    {
        var ocl =
            "isParallel implies\n" +
            "    ownedAction.incomingTransition->isEmpty() and\n" +
            "    ownedAction.outgoingTransition->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(IsParallel) || OwnedAction.IncomingTransition.IsEmpty()) && OwnedAction.OutgoingTransition.IsEmpty();",
            result);
    }

    [Test]
    public void SysML_ValidateInvocationExpressionOwnedFeatures()
    {
        var ocl =
            "ownedFeature->forAll(f | \n" +
            "    f <> result implies \n" +
            "        f.direction = FeatureDirectionKind::_'in')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedFeature.All(f => (!(f != Result) || f.Direction == FeatureDirectionKind.In));",
            result);
    }

    [Test]
    public void SysML_DeriveAnnotationAnnotatingElement()
    {
        var ocl =
            "annotatingElement =\n" +
            "    if ownedAnnotatingElement <> null then ownedAnnotatingElement\n" +
            "    else owningAnnotatingElement\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "AnnotatingElement == (OwnedAnnotatingElement != null ? OwnedAnnotatingElement : OwningAnnotatingElement);",
            result);
    }

    [Test]
    public void SysML_DeriveStepBehavior()
    {
        var ocl =
            "behavior = type->selectByKind(Behavior)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "Behavior == Type.OfType<Behavior>();",
            result);
    }

    [Test]
    public void SysML_ValidateSatisfyRequirementUsageReference()
    {
        var ocl =
            "referencedFeatureTarget() <> null implies\n" +
            "    referencedFeatureTarget().oclIsKindOf(RequirementUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is RequirementUsage));",
            result);
    }

    [Test]
    public void SysML_ValidateTypeOwnedIntersectingNotOne()
    {
        var ocl =
            "ownedIntersecting->size() <> 1";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedIntersecting.Count() != 1;",
            result);
    }

    [Test]
    public void SysML_DeriveTypeInheritedFeature()
    {
        var ocl =
            "inheritedFeature = inheritedMemberships->\n" +
            "    selectByKind(FeatureMembership).memberFeature";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "InheritedFeature == InheritedMemberships.OfType<FeatureMembership>().Select(item => item.MemberFeature);",
            result);
    }

    [Test]
    public void SysML_DeriveViewUsageExposedElement()
    {
        var ocl =
            "exposedElement = ownedImport->selectByKind(Expose).\n" +
            "    importedMemberships(Set{}).memberElement->\n" +
            "    select(elm | includeAsExposed(elm))->\n" +
            "    asOrderedSet()";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "ExposedElement == OwnedImport.OfType<Expose>().ImportedMemberships(new HashSet<dynamic> {  }).MemberElement.Where(elm => IncludeAsExposed(elm)).AsOrderedSet();",
            result);
    }

    [Test]
    public void SysML_DeriveViewDefinitionView()
    {
        var ocl =
            "view = usage->selectByKind(ViewUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "View == Usage.OfType<ViewUsage>();",
            result);
    }

    [Test]
    public void SysML_CheckAnalysisCaseUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('AnalysisCases::analysisCases')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SpecializesFromLibrary(\"AnalysisCases::analysisCases\");",
            result);
    }

    [Test]
    public void SysML_CheckVerificationCaseUsageSubVerificationCaseSpecialization()
    {
        var ocl =
            "isComposite and owningType <> null and\n" +
            "    (owningType.oclIsKindOf(VerificationCaseDefinition) or\n" +
            "     owningType.oclIsKindOf(VerificationCaseUsage)) implies \n" +
            "    specializesFromLibrary('VerificationCases::VerificationCase::subVerificationCases')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(IsComposite && OwningType != null && ((OwningType is VerificationCaseDefinition) || (OwningType is VerificationCaseUsage))) || SpecializesFromLibrary(\"VerificationCases::VerificationCase::subVerificationCases\"));",
            result);
    }

    [Test]
    public void SysML_DeriveNamespaceOwnedMembership()
    {
        var ocl =
            "ownedMembership = ownedRelationship->selectByKind(Membership)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedMembership == OwnedRelationship.OfType<Membership>();",
            result);
    }

    [Test]
    public void SysML_CheckTransitionUsagePayloadSpecialization()
    {
        var ocl =
            "triggerAction->notEmpty() implies\n" +
            "    let payloadParameter : Feature = inputParameter(2) in\n" +
            "    payloadParameter <> null and\n" +
            "    payloadParameter.subsetsChain(triggerAction->at(1), triggerPayloadParameter())";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(TriggerAction.NotEmpty()) || InputParameter(2).Select(payloadParameter => payloadParameter != null && payloadParameter.SubsetsChain(TriggerAction.ElementAt(0), TriggerPayloadParameter())));",
            result);
    }

    [Test]
    public void SysML_ValidateFeatureValueOverriding()
    {
        var ocl =
            "featureWithValue.redefinition.redefinedFeature->\n" +
            "    closure(redefinition.redefinedFeature).valuation->\n" +
            "    forAll(isDefault)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "FeatureWithValue.Redefinition.RedefinedFeature.Closure(item => item.Redefinition.RedefinedFeature).Valuation.All(item => item.IsDefault);",
            result);
    }

    [Test]
    public void SysML_ValidateFlowEndOwningType()
    {
        var ocl =
            "owningType <> null and owningType.oclIsKindOf(Flow)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwningType != null && (OwningType is Flow);",
            result);
    }

    [Test]
    public void SysML_CheckPayloadFeatureRedefinition()
    {
        var ocl =
            "redefinesFromLibrary('Transfers::Transfer::payload')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "RedefinesFromLibrary(\"Transfers::Transfer::payload\");",
            result);
    }

    [Test]
    public void SysML_DeriveNamespaceImportImportedElement()
    {
        var ocl =
            "importedElement = importedNamespace";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "ImportedElement == ImportedNamespace;",
            result);
    }

    [Test]
    public void SysML_DeriveElementName()
    {
        var ocl =
            "name = effectiveName()";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "Name == EffectiveName();",
            result);
    }

    [Test]
    public void SysML_ValidateUsageVariationOwnedFeatureMembership()
    {
        var ocl =
            "isVariation implies ownedFeatureMembership->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(IsVariation) || OwnedFeatureMembership.IsEmpty());",
            result);
    }

    [Test]
    public void SysML_ValidateSubsettingUniquenessConformance()
    {
        var ocl =
            "subsettedFeature.isUnique implies subsettingFeature.isUnique";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(SubsettedFeature.IsUnique) || SubsettingFeature.IsUnique);",
            result);
    }

    [Test]
    public void SysML_DeriveUsageNestedVerificationCase()
    {
        var ocl =
            "nestedVerificationCase = nestedUsage->selectByKind(VerificationCaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "NestedVerificationCase == NestedUsage.OfType<VerificationCaseUsage>();",
            result);
    }

    [Test]
    public void SysML_CheckRequirementUsageRequirementVerificationSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(RequirementVerificationMembership) implies\n" +
            "    specializesFromLibrary('VerificationCases::VerificationCase::obj::requirementVerifications')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(OwningFeatureMembership != null && (OwningFeatureMembership is RequirementVerificationMembership)) || SpecializesFromLibrary(\"VerificationCases::VerificationCase::obj::requirementVerifications\"));",
            result);
    }

    [Test]
    public void SysML_DeriveAnnotationOwnedAnnotatingElement()
    {
        var ocl =
            "ownedAnnotatingElement =\n" +
            "    let ownedAnnotatingElements : Sequence(AnnotatingElement) = \n" +
            "        ownedRelatedElement->selectByKind(AnnotatingElement) in\n" +
            "    if ownedAnnotatingElements->isEmpty() then null\n" +
            "    else ownedAnnotatingElements->first()\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedAnnotatingElement == OwnedRelatedElement.OfType<AnnotatingElement>().Select(ownedAnnotatingElements => (ownedAnnotatingElements.IsEmpty() ? null : ownedAnnotatingElements.First()));",
            result);
    }

    [Test]
    public void SysML_DeriveAllocationDefinitionAllocation()
    {
        var ocl =
            "allocation = usage->selectAsKind(AllocationUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "Allocation == Usage.OfType<AllocationUsage>();",
            result);
    }

    [Test]
    public void SysML_ValidateConnectorRelatedFeatures()
    {
        var ocl =
            "not isAbstract implies relatedFeature->size() >= 2";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(!IsAbstract) || RelatedFeature.Count() >= 2);",
            result);
    }

    [Test]
    public void SysML_DeriveUsageMayTimeVary()
    {
        var ocl =
            "mayTimeVary =\n" +
            "    owningType <> null and\n" +
            "    owningType.specializesFromLibrary('Occurrences::Occurrence') and\n" +
            "    not (\n" +
            "        isPortion or\n" +
            "        specializesFromLibrary('Links::SelfLink') or\n" +
            "        specializesFromLibrary('Occurrences::HappensLink') or\n" +
            "        isComposite and specializesFromLibrary('Actions::Action')\n" +
            "    )";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "MayTimeVary == OwningType != null && OwningType.SpecializesFromLibrary(\"Occurrences::Occurrence\") && !(IsPortion || SpecializesFromLibrary(\"Links::SelfLink\") || SpecializesFromLibrary(\"Occurrences::HappensLink\") || IsComposite && SpecializesFromLibrary(\"Actions::Action\"));",
            result);
    }

    [Test]
    public void SysML_ValidateFeatureOwnedCrossSubsetting()
    {
        var ocl =
            "ownedSubsetting->selectByKind(CrossSubsetting)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedSubsetting.OfType<CrossSubsetting>().Count() <= 1;",
            result);
    }

    [Test]
    public void SysML_CheckAllocationUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Allocations::allocations')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SpecializesFromLibrary(\"Allocations::allocations\");",
            result);
    }

    [Test]
    public void SysML_DeriveOwningNamespace()
    {
        var ocl =
            "owningNamespace =\n" +
            "    if owningMembership = null then null\n" +
            "    else owningMembership.membershipOwningNamespace\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwningNamespace == (OwningMembership == null ? null : OwningMembership.MembershipOwningNamespace);",
            result);
    }

    [Test]
    public void SysML_DeriveUsageNestedPart()
    {
        var ocl =
            "nestedPart = nestedUsage->selectByKind(PartUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "NestedPart == NestedUsage.OfType<PartUsage>();",
            result);
    }

    [Test]
    public void SysML_DeriveNamespaceMembers()
    {
        var ocl =
            "member = membership.memberElement";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "Member == Membership.MemberElement;",
            result);
    }

    [Test]
    public void SysML_CheckExhibitStateUsageSpecialization()
    {
        var ocl =
            "owningType <> null and\n" +
            "(owningType.oclIsKindOf(PartDefinition) or\n" +
            " owningType.oclIsKindOf(PartUsage)) implies\n" +
            "    specializesFromLibrary('Parts::Part::exhibitedStates')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(OwningType != null && ((OwningType is PartDefinition) || (OwningType is PartUsage))) || SpecializesFromLibrary(\"Parts::Part::exhibitedStates\"));",
            result);
    }

    [Test]
    public void SysML_DeriveTypeOwnedConjugator()
    {
        var ocl =
            "ownedConjugator =\n" +
            "    let ownedConjugators: Sequence(Conjugator) = \n" +
            "        ownedRelationship->selectByKind(Conjugation) in\n" +
            "    if ownedConjugators->isEmpty() then null \n" +
            "    else ownedConjugators->at(1) endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedConjugator == OwnedRelationship.OfType<Conjugation>().Select(ownedConjugators => (ownedConjugators.IsEmpty() ? null : ownedConjugators.ElementAt(0)));",
            result);
    }

    [Test]
    public void SysML_ValidateActorMembershipOwningType()
    {
        var ocl =
            "owningType.oclIsKindOf(RequirementUsage) or\n" +
            "owningType.oclIsKindOf(RequirementDefinition) or\n" +
            "owningType.oclIsKindOf(CaseDefinition) or\n" +
            "owningType.oclIsKindOf(CaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(OwningType is RequirementUsage) || (OwningType is RequirementDefinition) || (OwningType is CaseDefinition) || (OwningType is CaseUsage);",
            result);
    }

    [Test]
    public void SysML_DeriveVerificationCaseUsageVerifiedRequirement()
    {
        var ocl =
            "verifiedRequirement =\n" +
            "    if objectiveRequirement = null then OrderedSet{}\n" +
            "    else \n" +
            "        objectiveRequirement.featureMembership->\n" +
            "            selectByKind(RequirementVerificationMembership).\n" +
            "            verifiedRequirement->asOrderedSet()\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "VerifiedRequirement == (ObjectiveRequirement == null ? new List<dynamic> {  } : ObjectiveRequirement.FeatureMembership.OfType<RequirementVerificationMembership>().Select(item => item.VerifiedRequirement).AsOrderedSet());",
            result);
    }

    [Test]
    public void SysML_CheckConcernUsageFramedConcernSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(FramedConcernMembership) implies\n" +
            "    specializesFromLibrary('Requirements::RequirementCheck::concerns')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(OwningFeatureMembership != null && (OwningFeatureMembership is FramedConcernMembership)) || SpecializesFromLibrary(\"Requirements::RequirementCheck::concerns\"));",
            result);
    }

    [Test]
    public void SysML_ValidateBehaviorSpecialization()
    {
        var ocl =
            "ownedSpecialization.general->forAll(not oclIsKindOf(Structure))";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedSpecialization.General.All(item => !(item is Structure));",
            result);
    }

    [Test]
    public void SysML_CheckUseCaseUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('UseCases::useCases')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SpecializesFromLibrary(\"UseCases::useCases\");",
            result);
    }

    [Test]
    public void SysML_CheckFlowSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Transfers::transfers')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SpecializesFromLibrary(\"Transfers::transfers\");",
            result);
    }

    [Test]
    public void SysML_DeriveViewUsageViewCondition()
    {
        var ocl =
            "viewCondition = ownedMembership->\n" +
            "    selectByKind(ElementFilterMembership).\n" +
            "    condition";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "ViewCondition == OwnedMembership.OfType<ElementFilterMembership>().Select(item => item.Condition);",
            result);
    }

    [Test]
    public void SysML_DeriveOwningMembershipOwnedMemberName()
    {
        var ocl =
            "ownedMemberName = ownedMemberElement.name";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedMemberName == OwnedMemberElement.Name;",
            result);
    }

    [Test]
    public void SysML_DeriveOwningMembershipOwnedMemberShortName()
    {
        var ocl =
            "ownedMemberShortName = ownedMemberElement.shortName";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedMemberShortName == OwnedMemberElement.ShortName;",
            result);
    }

    [Test]
    public void SysML_CheckPartUsageStakeholderSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(StakeholderMembership) implies\n" +
            "    specializesFromLibrary('Requirements::RequirementCheck::stakeholders')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(OwningFeatureMembership != null && (OwningFeatureMembership is StakeholderMembership)) || SpecializesFromLibrary(\"Requirements::RequirementCheck::stakeholders\"));",
            result);
    }

    [Test]
    public void SysML_CheckRenderingUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Views::renderings')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SpecializesFromLibrary(\"Views::renderings\");",
            result);
    }

    [Test]
    public void SysML_CheckConnectorObjectSpecialization()
    {
        var ocl =
            "association->exists(oclIsKindOf(AssociationStructure)) implies\n" +
            "    specializesFromLibrary('Objects::linkObjects')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(Association.Exists(item => (item is AssociationStructure))) || SpecializesFromLibrary(\"Objects::linkObjects\"));",
            result);
    }

    [Test]
    public void SysML_DeriveVewRenderingMembershipReferencedRendering()
    {
        var ocl =
            "referencedRendering =\n" +
            "    let referencedFeature : Feature = \n" +
            "        ownedRendering.referencedFeatureTarget() in\n" +
            "    if referencedFeature = null then ownedRendering\n" +
            "    else if referencedFeature.oclIsKindOf(RenderingUsage) then\n" +
            "        refrencedFeature.oclAsType(RenderingUsage)\n" +
            "    else null\n" +
            "    endif endif";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "ReferencedRendering == OwnedRendering.ReferencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? OwnedRendering : ((referencedFeature is RenderingUsage) ? (RefrencedFeature as RenderingUsage) : null)));",
            result);
    }

    [Test]
    public void SysML_ValidateFeatureOwnedReferenceSubsetting()
    {
        var ocl =
            "ownedSubsetting->selectByKind(ReferenceSubsetting)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedSubsetting.OfType<ReferenceSubsetting>().Count() <= 1;",
            result);
    }

    [Test]
    public void SysML_ValidateFeatureCrossFeatureSpecialization()
    {
        var ocl =
            "crossFeature <> null implies\n" +
            "    ownedRedefinition.redefinedFeature.crossFeature->\n" +
            "            forAll(f | f <> null implies crossFeature.specializes(f))";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(CrossFeature != null) || OwnedRedefinition.RedefinedFeature.CrossFeature.All(f => (!(f != null) || CrossFeature.Specializes(f))));",
            result);
    }

    [Test]
    public void SysML_ValidateExpressionResultExpressionMembership()
    {
        var ocl =
            "membership->selectByKind(ResultExpressionMembership)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "Membership.OfType<ResultExpressionMembership>().Count() <= 1;",
            result);
    }

    [Test]
    public void SysML_CheckItemDefinitionSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Items::Item')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "SpecializesFromLibrary(\"Items::Item\");",
            result);
    }

    [Test]
    public void SysML_CheckForLoopActionUsageVarRedefinition()
    {
        var ocl =
            "loopVariable <> null and\n" +
            "loopVariable.redefinesFromLibrary('Actions::ForLoopAction::var')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "LoopVariable != null && LoopVariable.RedefinesFromLibrary(\"Actions::ForLoopAction::var\");",
            result);
    }

    [Test]
    public void SysML_DeriveMembershipMemberElementId()
    {
        var ocl =
            "memberElementId = memberElement.elementId";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "MemberElementId == MemberElement.ElementId;",
            result);
    }

    [Test]
    public void SysML_ValidateAnnotationAnnotatedElementOwnership()
    {
        var ocl =
            "(owningAnnotatedElement <> null) = (ownedAnnotatingElement <> null)";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(OwningAnnotatedElement != null) == (OwnedAnnotatingElement != null);",
            result);
    }

    [Test]
    public void SysML_CheckAssignmentActionUsageAccessedFeatureRedefinition()
    {
        var ocl =
            "let targetParameter : Feature = inputParameter(1) in\n" +
            "targetParameter <> null and\n" +
            "targetParameter.ownedFeature->notEmpty() and\n" +
            "targetParameter->first().ownedFeature->notEmpty() and\n" +
            "targetParameter->first().ownedFeature->first().\n" +
            "    redefines('AssigmentAction::target::startingAt::accessedFeature')";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "InputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.OwnedFeature.NotEmpty() && targetParameter.First().OwnedFeature.NotEmpty() && targetParameter.First().OwnedFeature.First().Redefines(\"AssigmentAction::target::startingAt::accessedFeature\"));",
            result);
    }

    [Test]
    public void SysML_ValidateTransitionUsageSuccession()
    {
        var ocl =
            "let successions : Sequence(Successions) = \n" +
            "    ownedMember->selectByKind(Succession) in\n" +
            "successions->notEmpty() and\n" +
            "successions->at(1).targetFeature.featureTarget->\n" +
            "    forAll(oclIsKindOf(ActionUsage))";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "OwnedMember.OfType<Succession>().Select(successions => successions.NotEmpty() && successions.ElementAt(0).TargetFeature.FeatureTarget.All(item => (item is ActionUsage)));",
            result);
    }

    [Test]
    public void SysML_CheckRequirementUsageObjectiveRedefinition()
    {
        var ocl =
            "owningfeatureMembership <> null and\n" +
            "owningfeatureMembership.oclIsKindOf(ObjectiveMembership) implies\n" +
            "    owningType.ownedSpecialization.general->forAll(gen |\n" +
            "        (gen.oclIsKindOf(CaseDefinition) implies\n" +
            "            redefines(gen.oclAsType(CaseDefinition).objectiveRequirement)) and\n" +
            "        (gen.oclIsKindOf(CaseUsage) implies\n" +
            "            redefines(gen.oclAsType(CaseUsage).objectiveRequirement))";
        var result = OclToCSharpConverter.Convert(ocl, new ConversionOptions());
        Assert.AreEqual(
            "(!(OwningfeatureMembership != null && (OwningfeatureMembership is ObjectiveMembership)) || OwningType.OwnedSpecialization.General.All(gen => ((!((gen is CaseDefinition)) || Redefines((gen as CaseDefinition).ObjectiveRequirement))) && ((!((gen is CaseUsage)) || Redefines((gen as CaseUsage).ObjectiveRequirement)))));",
            result);
    }
}
