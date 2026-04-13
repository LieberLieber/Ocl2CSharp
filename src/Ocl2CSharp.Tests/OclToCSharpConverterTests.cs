using NUnit.Framework;
using Ocl2CSharp;

namespace Ocl2CSharp.Tests;

public class OclToCSharpConverterTests
{
    // -------------------------------------------------------------------------
    // Logical operators
    // -------------------------------------------------------------------------

    [Test]
    public void LogicalAnd_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0 and age <= 150");
        Assert.AreEqual("age >= 0 && age <= 150", result);
    }

    [Test]
    public void LogicalAnd_Ampersand_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0 & age <= 150");
        Assert.AreEqual("age >= 0 && age <= 150", result);
    }

    [Test]
    public void LogicalOr_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("email <> null or phone <> null");
        Assert.AreEqual("email != null || phone != null", result);
    }

    [Test]
    public void LogicalXor_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("a or b xor c");
        Assert.That(result, Does.Contain("^"));
    }

    [Test]
    public void ImpliesOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 18 implies canVote = true");
        Assert.That(result, Does.Contain("!(age >= 18)"));
        Assert.That(result, Does.Contain("canVote == true"));
    }

    [Test]
    public void ImpliesArrow_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 18 => canVote = true");
        Assert.That(result, Does.Contain("!(age >= 18)"));
        Assert.That(result, Does.Contain("canVote == true"));
    }

    // -------------------------------------------------------------------------
    // Equality / comparison operators
    // -------------------------------------------------------------------------

    [Test]
    public void Equality_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = 'Alice'");
        Assert.AreEqual("name == \"Alice\"", result);
    }

    [Test]
    public void Inequality_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null");
        Assert.AreEqual("name != null", result);
    }

    [Test]
    public void Inequality_SlashEquals_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value /= 0");
        Assert.AreEqual("value != 0", result);
    }

    [Test]
    public void LessThan_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age < 18");
        Assert.AreEqual("age < 18", result);
    }

    [Test]
    public void GreaterThanOrEqual_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0");
        Assert.AreEqual("age >= 0", result);
    }

    // -------------------------------------------------------------------------
    // Arithmetic operators
    // -------------------------------------------------------------------------

    [Test]
    public void Addition_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance + interest >= 0");
        Assert.AreEqual("balance + interest >= 0", result);
    }

    [Test]
    public void Multiplication_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance * rate > 0");
        Assert.AreEqual("balance * rate > 0", result);
    }

    [Test]
    public void ModOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance mod 2 = 0");
        Assert.AreEqual("balance % 2 == 0", result);
    }

    [Test]
    public void DivOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("total div numItems > 0");
        Assert.AreEqual("total / numItems > 0", result);
    }

    // -------------------------------------------------------------------------
    // Unary operators
    // -------------------------------------------------------------------------

    [Test]
    public void NotOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("not active");
        Assert.AreEqual("!active", result);
    }

    [Test]
    public void UnaryMinus_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("-balance > 0");
        Assert.AreEqual("-balance > 0", result);
    }

    // -------------------------------------------------------------------------
    // Conditional expression
    // -------------------------------------------------------------------------

    [Test]
    public void ConditionalExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif");
        Assert.AreEqual("(age >= 18 ? isAdult : isMinor)", result);
    }

    [Test]
    public void ConditionalExpression_UseIfStatement_EmitsIfElseBlock()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif", useIfStatement: true).Replace("\r", "");
        Assert.Equal("if (age >= 18)\n{\n\treturn isAdult;\n}\nelse\n{\n\treturn isMinor;\n}", result);
    }

    [Test]
    public void ConditionalExpression_DefaultMode_EmitsTernary()
    {
        // Verify that the default (no second argument) still produces ternary output.
        var result = OclToCSharpConverter.Convert("if x > 0 then positive else nonPositive endif");
        Assert.AreEqual("(x > 0 ? positive : nonPositive)", result);
    }

    // -------------------------------------------------------------------------
    // Literals
    // -------------------------------------------------------------------------

    [Test]
    public void NullLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null");
        Assert.AreEqual("name != null", result);
    }

    [Test]
    public void BooleanLiteralTrue_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = true");
        Assert.AreEqual("active == true", result);
    }

    [Test]
    public void BooleanLiteralFalse_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = false");
        Assert.AreEqual("active == false", result);
    }

    [Test]
    public void IntLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance > 0");
        Assert.AreEqual("balance > 0", result);
    }

    [Test]
    public void StringLiteralSingleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = 'Alice'");
        Assert.AreEqual("name == \"Alice\"", result);
    }

    [Test]
    public void StringLiteralDoubleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = \"Alice\"");
        Assert.AreEqual("name == \"Alice\"", result);
    }

    [Test]
    public void EnumerationLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("status = Status::Active");
        Assert.AreEqual("status == Status.Active", result);
    }

    // -------------------------------------------------------------------------
    // Navigation / member access
    // -------------------------------------------------------------------------

    [Test]
    public void MemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ceo.name <> null");
        Assert.AreEqual("ceo.name != null", result);
    }

    [Test]
    public void NestedMemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("company.ceo.name <> null");
        Assert.AreEqual("company.ceo.name != null", result);
    }

    // -------------------------------------------------------------------------
    // Collection operations — arrow (->)
    // -------------------------------------------------------------------------

    [Test]
    public void SelectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->select(e | e.age >= 18)->notEmpty()");
        Assert.That(result, Does.Contain(".Where(e => e.age >= 18)"));
        Assert.That(result, Does.Contain(".Any()"));
    }

    [Test]
    public void ForAllOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->forAll(e | e.age >= 18)");
        Assert.AreEqual("employees.All(e => e.age >= 18)", result);
    }

    [Test]
    public void ExistsOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->exists(e | e.role = 'CEO')");
        Assert.AreEqual("employees.Any(e => e.role == \"CEO\")", result);
    }

    [Test]
    public void CollectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->collect(e | e.name)->notEmpty()");
        Assert.That(result, Does.Contain(".Select(e => e.name)"));
        Assert.That(result, Does.Contain(".Any()"));
    }

    [Test]
    public void SizeOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->size() > 0");
        Assert.AreEqual("employees.Count() > 0", result);
    }

    [Test]
    public void IsEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->isEmpty()");
        Assert.AreEqual("!contracts.Any()", result);
    }

    [Test]
    public void NotEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->notEmpty()");
        Assert.AreEqual("contracts.Any()", result);
    }

    [Test]
    public void IncludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->includes(alice)");
        Assert.AreEqual("employees.Contains(alice)", result);
    }

    [Test]
    public void ExcludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->excludes(alice)");
        Assert.AreEqual("!employees.Contains(alice)", result);
    }

    [Test]
    public void SumOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("salaries->sum() > 0");
        Assert.AreEqual("salaries.Sum() > 0", result);
    }

    [Test]
    public void MaxOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->max()");
        Assert.AreEqual("scores.Max()", result);
    }

    [Test]
    public void MinOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->min()");
        Assert.AreEqual("scores.Min()", result);
    }

    [Test]
    public void RejectOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->reject(e | e.age < 18)->isEmpty()");
        Assert.That(result, Does.Contain(".Where(e => !(e.age < 18))"));
        Assert.That(result, Does.Contain("!employees"));
    }

    [Test]
    public void AsSetOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("items->asSet()");
        Assert.AreEqual("items.ToHashSet()", result);
    }

    [Test]
    public void AsSequenceOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("items->asSequence()");
        Assert.AreEqual("items.ToList()", result);
    }

    [Test]
    public void ToUpperCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toUpperCase()");
        Assert.AreEqual("name.ToUpper()", result);
    }

    [Test]
    public void ToLowerCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toLowerCase()");
        Assert.AreEqual("name.ToLower()", result);
    }

    [Test]
    public void ToIntegerOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value->toInteger()");
        Assert.AreEqual("Convert.ToInt32(value)", result);
    }

    // -------------------------------------------------------------------------
    // Dot (.) OCL operations
    // -------------------------------------------------------------------------

    [Test]
    public void OclIsUndefined_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name.oclIsUndefined() = false");
        Assert.AreEqual("(name == null) == false", result);
    }

    [Test]
    public void OclIsKindOf_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("obj.oclIsKindOf(Person)");
        Assert.AreEqual("(obj is Person)", result);
    }

    // -------------------------------------------------------------------------
    // Collection literals
    // -------------------------------------------------------------------------

    [Test]
    public void SetLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Set{1, 2, 3}->includes(status)");
        Assert.That(result, Does.Contain("new HashSet<dynamic>"));
        Assert.That(result, Does.Contain(".Contains(status)"));
    }

    [Test]
    public void SequenceLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Sequence{1, 2, 3}->size() > 0");
        Assert.That(result, Does.Contain("new List<dynamic>"));
        Assert.That(result, Does.Contain(".Count()"));
    }

    [Test]
    public void BagLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("Bag{1, 2, 3}->size() > 0");
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.AreEqual(
            "featureMembership.OfType<ViewRenderingMembership>().Select(renderings => (!renderings.Any() ? null : renderings.First().referencedRendering))",
            result);
    }

    // -------------------------------------------------------------------------
    // Parenthesized expression
    // -------------------------------------------------------------------------

    [Test]
    public void ParenthesizedExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("(age + 1) > 0");
		Assert.AreEqual("(age + 1) > 0", result);
	}

    // -------------------------------------------------------------------------
    // Chained operations
    // -------------------------------------------------------------------------

    [Test]
    public void ChainedSelectAndForAll_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->select(e | e.active = true)->forAll(e | e.age >= 18)");
        Assert.That(result, Does.Contain(".Where(e => e.active == true)"));
        Assert.That(result, Does.Contain(".All(e => e.age >= 18)"));
    }


    [Test]
    public void SelectByKind_IsCoverted()
    {
		var result = OclToCSharpConverter.Convert("ownedMembership->selectByKind(VariantMembership)");
		Assert.AreEqual("ownedMembership.OfType<VariantMembership>()", result);
	}


    // -------------------------------------------------------------------------
    // SysML OCL constraints (from SysML_OCLAndCSharp.md, up to CheckRequirementUsageObjectiveRedefinition)
    // -------------------------------------------------------------------------

    [Fact]
    public void SysML_ValidateEventOccurrenceUsageReference()
    {
        var ocl =
            "referencedFeatureTarget() <> null implies\n" +
            "    referencedFeatureTarget().oclIsKindOf(OccurrenceUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is OccurrenceUsage))",
            result);
    }

    [Fact]
    public void SysML_DeriveCalculationUsageCalculation()
    {
        var ocl =
            "calculation = action->selectByKind(CalculationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "calculation == action.OfType<CalculationUsage>()",
            result);
    }

    [Fact]
    public void SysML_ValidateSubjectMembershipOwningType()
    {
        var ocl =
            "owningType.oclIsType(RequirementDefinition) or\n" +
            "owningType.oclIsType(RequiremenCaseRequirementDefinition) or\n" +
            "owningType.oclIsType(CaseDefinition) or\n" +
            "owningType.oclIsType(CaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "owningType is RequirementDefinition || owningType is RequiremenCaseRequirementDefinition || owningType is CaseDefinition || owningType is CaseUsage",
            result);
    }

    [Fact]
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "referent == ownedMembership.Where(it => !((ParameterMembership))).Select(nonParameterMemberships => (!nonParameterMemberships.Any() || !(nonParameterMemberships.First().memberElement is Feature) ? null : ((Feature)nonParameterMemberships.First().memberElement)))",
            result);
    }

    [Fact]
    public void SysML_DeriveTypeOwnedIntersecting()
    {
        var ocl =
            "ownedRelationship->selectByKind(Intersecting)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedRelationship.OfType<Intersecting>()",
            result);
    }

    [Fact]
    public void SysML_ValidatePortDefinitionConjugatedPortDefinition()
    {
        var ocl =
            "not oclIsKindOf(ConjugatedPortDefinition) implies\n" +
            "    ownedMember->\n" +
            "        selectByKind(ConjugatedPortDefinition)->\n" +
            "        size() = 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(!(ConjugatedPortDefinition)) || ownedMember.OfType<ConjugatedPortDefinition>().Count() == 1)",
            result);
    }

    [Fact]
    public void SysML_CheckFeatureValuationSpecialization()
    {
        var ocl =
            "direction = null and\n" +
            "ownedSpecializations->forAll(isImplied) implies\n" +
            "    ownedMembership->\n" +
            "        selectByKind(FeatureValue)->\n" +
            "        forAll(fv | specializes(fv.value.result))";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(direction == null && ownedSpecializations.All(it => isImplied)) || ownedMembership.OfType<FeatureValue>().All(fv => specializes(fv.value.result)))",
            result);
    }

    [Fact]
    public void SysML_DeriveFeatureOwnedTypeFeaturing()
    {
        var ocl =
            "ownedTypeFeaturing = ownedRelationship->selectByKind(TypeFeaturing)->\n" +
            "    select(tf | tf.featureOfType = self)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedTypeFeaturing == ownedRelationship.OfType<TypeFeaturing>().Where(tf => tf.featureOfType == self)",
            result);
    }

    [Fact]
    public void SysML_CheckMetadataAccessExpressionSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Performances::metadataAccessEvaluations')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "specializesFromLibrary(\"Performances::metadataAccessEvaluations\")",
            result);
    }

    [Fact]
    public void SysML_ValidateConstructorExpressionOwnedFeatures()
    {
        var ocl =
            "ownedFeatures->excluding(result)->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "!ownedFeatures.Where(it => it != result).Any()",
            result);
    }

    [Fact]
    public void SysML_ValidateAssociationBinarySpecialization()
    {
        var ocl =
            "associationEnds->size() > 2 implies\n" +
            "    not specializesFromLibrary('Links::BinaryLink')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(associationEnds.Count() > 2) || !specializesFromLibrary(\"Links::BinaryLink\"))",
            result);
    }

    [Fact]
    public void SysML_DeriveCaseDefinitionSubjectParameter()
    {
        var ocl =
            "subjectParameter =\n" +
            "    let subjectMems : OrderedSet(SubjectMembership) = \n" +
            "        featureMembership->selectByKind(SubjectMembership) in\n" +
            "    if subjectMems->isEmpty() then null\n" +
            "    else subjectMems->first().ownedSubjectParameter\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "subjectParameter == featureMembership.OfType<SubjectMembership>().Select(subjectMems => (!subjectMems.Any() ? null : subjectMems.First().ownedSubjectParameter))",
            result);
    }

    [Fact]
    public void SysML_DeriveDefinitionOwnedConnection()
    {
        var ocl =
            "ownedConnection = ownedUsage->selectByKind(ConnectorAsUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedConnection == ownedUsage.OfType<ConnectorAsUsage>()",
            result);
    }

    [Fact]
    public void SysML_DeriveDefinitionOwnedConstraint()
    {
        var ocl =
            "ownedConstraint = ownedUsage->selectByKind(ConstraintUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedConstraint == ownedUsage.OfType<ConstraintUsage>()",
            result);
    }

    [Fact]
    public void SysML_CheckOccurrenceDefinitionMultiplicitySpecialization()
    {
        var ocl =
            "isIndividual implies\n" +
            "    multiplicity <> null and\n" +
            "    multiplicity.specializesFromLibrary('Base::zeroOrOne')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(isIndividual) || multiplicity != null) && multiplicity.specializesFromLibrary(\"Base::zeroOrOne\")",
            result);
    }

    [Fact]
    public void SysML_ValidateSpecificationSpecificNotConjugated()
    {
        var ocl =
            "not specific.isConjugated";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "!specific.isConjugated",
            result);
    }

    [Fact]
    public void SysML_DeriveInstantiationExpressionInstantiatedType()
    {
        var ocl =
            "instantiatedType = instantiatedType()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "instantiatedType == instantiatedType()",
            result);
    }

    [Fact]
    public void SysML_ValidateConnectorBinarySpecialization()
    {
        var ocl =
            "connectorEnds->size() > 2 implies\n" +
            "    not specializesFromLibrary('Links::BinaryLink')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(connectorEnds.Count() > 2) || !specializesFromLibrary(\"Links::BinaryLink\"))",
            result);
    }

    [Fact]
    public void SysML_DeriveElementOwnedElement()
    {
        var ocl =
            "ownedElement = ownedRelationship.ownedRelatedElement";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedElement == ownedRelationship.ownedRelatedElement",
            result);
    }

    [Fact]
    public void SysML_CheckTransitionUsageStateSpecialization()
    {
        var ocl =
            "isComposite and owningType <> null and\n" +
            "(owningType.oclIsKindOf(StateDefinition) or\n" +
            " owningType.oclIsKindOf(StateUsage)) and\n" +
            "source <> null and source.oclIsKindOf(StateUsage) implies\n" +
            "    specializesFromLibrary('States::StateAction::stateTransitions')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(isComposite && owningType != null && ((owningType is StateDefinition) || (owningType is StateUsage)) && source != null && (source is StateUsage)) || specializesFromLibrary(\"States::StateAction::stateTransitions\"))",
            result);
    }

    [Fact]
    public void SysML_DeriveRequirementDefinitionFramedConcern()
    {
        var ocl =
            "framedConcern = featureMembership->\n" +
            "    selectByKind(FramedConcernMembership).\n" +
            "    ownedConcern";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "framedConcern == featureMembership.OfType<FramedConcernMembership>()",
            result);
    }

    [Fact]
    public void SysML_CheckUsageVariationUsageSpecialization()
    {
        var ocl =
            "owningVariationUsage <> null implies\n" +
            "    specializes(owningVariationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(owningVariationUsage != null) || specializes(owningVariationUsage))",
            result);
    }

    [Fact]
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "targetFeature == ownedMembership.Where(it => !((ParameterMembership))).Select(nonParameterMemberships => (!nonParameterMemberships.Any() || !(nonParameterMemberships.First().memberElement is Feature) ? null : ((Feature)nonParameterMemberships.First().memberElement)))",
            result);
    }

    [Fact]
    public void SysML_DeriveUsageNestedAllocation()
    {
        var ocl =
            "nestedAllocation = nestedUsage->selectByKind(AllocationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "nestedAllocation == nestedUsage.OfType<AllocationUsage>()",
            result);
    }

    [Fact]
    public void SysML_ValidateStateDefinitionParallelSubactions()
    {
        var ocl =
            "isParallel implies\n" +
            "    ownedAction.incomingTransition->isEmpty() and\n" +
            "    ownedAction.outgoingTransition->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(isParallel) || !ownedAction.incomingTransition.Any()) && !ownedAction.outgoingTransition.Any()",
            result);
    }

    [Fact]
    public void SysML_ValidateInvocationExpressionOwnedFeatures()
    {
        var ocl =
            "ownedFeature->forAll(f |\n" +
            "    f <> result implies \n" +
            "        f.direction = FeatureDirectionKind::_'in')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedFeature.All(f => (!(f != result) || f.direction == FeatureDirectionKind._))",
            result);
    }

    [Fact]
    public void SysML_DeriveAnnotationAnnotatingElement()
    {
        var ocl =
            "annotatingElement =\n" +
            "    if ownedAnnotatingElement <> null then ownedAnnotatingElement\n" +
            "    else owningAnnotatingElement\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "annotatingElement == (ownedAnnotatingElement != null ? ownedAnnotatingElement : owningAnnotatingElement)",
            result);
    }

    [Fact]
    public void SysML_DeriveStepBehavior()
    {
        var ocl =
            "behavior = type->selectByKind(Behavior)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "behavior == type.OfType<Behavior>()",
            result);
    }

    [Fact]
    public void SysML_ValidateSatisfyRequirementUsageReference()
    {
        var ocl =
            "referencedFeatureTarget() <> null implies\n" +
            "    referencedFeatureTarget().oclIsKindOf(RequirementUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is RequirementUsage))",
            result);
    }

    [Fact]
    public void SysML_ValidateTypeOwnedIntersectingNotOne()
    {
        var ocl =
            "ownedIntersecting->size() <> 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedIntersecting.Count() != 1",
            result);
    }

    [Fact]
    public void SysML_DeriveTypeInheritedFeature()
    {
        var ocl =
            "inheritedFeature = inheritedMemberships->\n" +
            "    selectByKind(FeatureMembership).memberFeature";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "inheritedFeature == inheritedMemberships.OfType<FeatureMembership>()",
            result);
    }

    [Fact]
    public void SysML_DeriveViewUsageExposedElement()
    {
        var ocl =
            "exposedElement = ownedImport->selectByKind(Expose).\n" +
            "    importedMemberships(Set{}).memberElement->\n" +
            "    select(elm | includeAsExposed(elm))->\n" +
            "    asOrderedSet()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "exposedElement == ownedImport.OfType<Expose>().importedMemberships(new HashSet<dynamic> {  }).memberElement.Where(elm => includeAsExposed(elm)).Distinct().ToHashSet()",
            result);
    }

    [Fact]
    public void SysML_DeriveViewDefinitionView()
    {
        var ocl =
            "view = usage->selectByKind(ViewUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "view == usage.OfType<ViewUsage>()",
            result);
    }

    [Fact]
    public void SysML_CheckAnalysisCaseUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('AnalysisCases::analysisCases')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "specializesFromLibrary(\"AnalysisCases::analysisCases\")",
            result);
    }

    [Fact]
    public void SysML_CheckVerificationCaseUsageSubVerificationCaseSpecialization()
    {
        var ocl =
            "isComposite and owningType <> null and\n" +
            "    (owningType.oclIsKindOf(VerificationCaseDefinition) or\n" +
            "     owningType.oclIsKindOf(VerificationCaseUsage)) implies \n" +
            "    specializesFromLibrary('VerificationCases::VerificationCase::subVerificationCases')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(isComposite && owningType != null && ((owningType is VerificationCaseDefinition) || (owningType is VerificationCaseUsage))) || specializesFromLibrary(\"VerificationCases::VerificationCase::subVerificationCases\"))",
            result);
    }

    [Fact]
    public void SysML_DeriveNamespaceOwnedMembership()
    {
        var ocl =
            "ownedMembership = ownedRelationship->selectByKind(Membership)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedMembership == ownedRelationship.OfType<Membership>()",
            result);
    }

    [Fact]
    public void SysML_CheckTransitionUsagePayloadSpecialization()
    {
        var ocl =
            "triggerAction->notEmpty() implies\n" +
            "    let payloadParameter : Feature = inputParameter(2) in\n" +
            "    payloadParameter <> null and\n" +
            "    payloadParameter.subsetsChain(triggerAction->at(1), triggerPayloadParameter())";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(triggerAction.Any()) || payloadParameter is Feature == inputParameter(2))",
            result);
    }

    [Fact]
    public void SysML_ValidateFeatureValueOverriding()
    {
        var ocl =
            "featureWithValue.redefinition.redefinedFeature->\n" +
            "    closure(redefinition.redefinedFeature).valuation->\n" +
            "    forAll(isDefault)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "/* closure */ featureWithValue.redefinition.redefinedFeature.valuation.All(it => isDefault)",
            result);
    }

    [Fact]
    public void SysML_ValidateFlowEndOwningType()
    {
        var ocl =
            "owningType <> null and owningType.oclIsKindOf(Flow)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "owningType != null && (owningType is Flow)",
            result);
    }

    [Fact]
    public void SysML_CheckPayloadFeatureRedefinition()
    {
        var ocl =
            "redefinesFromLibrary('Transfers::Transfer::payload')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "redefinesFromLibrary(\"Transfers::Transfer::payload\")",
            result);
    }

    [Fact]
    public void SysML_DeriveNamespaceImportImportedElement()
    {
        var ocl =
            "importedElement = importedNamespace";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "importedElement == importedNamespace",
            result);
    }

    [Fact]
    public void SysML_DeriveElementName()
    {
        var ocl =
            "name = effectiveName()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "name == effectiveName()",
            result);
    }

    [Fact]
    public void SysML_ValidateUsageVariationOwnedFeatureMembership()
    {
        var ocl =
            "isVariation implies ownedFeatureMembership->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(isVariation) || !ownedFeatureMembership.Any())",
            result);
    }

    [Fact]
    public void SysML_ValidateSubsettingUniquenessConformance()
    {
        var ocl =
            "subsettedFeature.isUnique implies subsettingFeature.isUnique";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(subsettedFeature.isUnique) || subsettingFeature.isUnique)",
            result);
    }

    [Fact]
    public void SysML_DeriveUsageNestedVerificationCase()
    {
        var ocl =
            "nestedVerificationCase = nestedUsage->selectByKind(VerificationCaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "nestedVerificationCase == nestedUsage.OfType<VerificationCaseUsage>()",
            result);
    }

    [Fact]
    public void SysML_CheckRequirementUsageRequirementVerificationSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(RequirementVerificationMembership) implies\n" +
            "    specializesFromLibrary('VerificationCases::VerificationCase::obj::requirementVerifications')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(owningFeatureMembership != null && (owningFeatureMembership is RequirementVerificationMembership)) || specializesFromLibrary(\"VerificationCases::VerificationCase::obj::requirementVerifications\"))",
            result);
    }

    [Fact]
    public void SysML_DeriveAnnotationOwnedAnnotatingElement()
    {
        var ocl =
            "ownedAnnotatingElement =\n" +
            "    let ownedAnnotatingElements : Sequence(AnnotatingElement) = \n" +
            "        ownedRelatedElement->selectByKind(AnnotatingElement) in\n" +
            "    if ownedAnnotatingElements->isEmpty() then null\n" +
            "    else ownedAnnotatingElements->first()\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedAnnotatingElement == ownedRelatedElement.OfType<AnnotatingElement>().Select(ownedAnnotatingElements => (!ownedAnnotatingElements.Any() ? null : ownedAnnotatingElements.First()))",
            result);
    }

    [Fact]
    public void SysML_DeriveAllocationDefinitionAllocation()
    {
        var ocl =
            "allocation = usage->selectAsKind(AllocationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "allocation == usage.selectAsKind(AllocationUsage).selectAsKind",
            result);
    }

    [Fact]
    public void SysML_ValidateConnectorRelatedFeatures()
    {
        var ocl =
            "not isAbstract implies relatedFeature->size() >= 2";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(!isAbstract) || relatedFeature.Count() >= 2)",
            result);
    }

    [Fact]
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "mayTimeVary == owningType != null && owningType.specializesFromLibrary(\"Occurrences::Occurrence\") && !(isPortion || specializesFromLibrary(\"Links::SelfLink\") || specializesFromLibrary(\"Occurrences::HappensLink\") || isComposite && specializesFromLibrary(\"Actions::Action\"))",
            result);
    }

    [Fact]
    public void SysML_ValidateFeatureOwnedCrossSubsetting()
    {
        var ocl =
            "ownedSubsetting->selectByKind(CrossSubsetting)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedSubsetting.OfType<CrossSubsetting>().Count() <= 1",
            result);
    }

    [Fact]
    public void SysML_CheckAllocationUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Allocations::allocations')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "specializesFromLibrary(\"Allocations::allocations\")",
            result);
    }

    [Fact]
    public void SysML_DeriveOwningNamespace()
    {
        var ocl =
            "owningNamespace =\n" +
            "    if owningMembership = null then null\n" +
            "    else owningMembership.membershipOwningNamespace\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "owningNamespace == (owningMembership == null ? null : owningMembership.membershipOwningNamespace)",
            result);
    }

    [Fact]
    public void SysML_DeriveUsageNestedPart()
    {
        var ocl =
            "nestedPart = nestedUsage->selectByKind(PartUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "nestedPart == nestedUsage.OfType<PartUsage>()",
            result);
    }

    [Fact]
    public void SysML_DeriveNamespaceMembers()
    {
        var ocl =
            "member = membership.memberElement";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "member == membership.memberElement",
            result);
    }

    [Fact]
    public void SysML_CheckExhibitStateUsageSpecialization()
    {
        var ocl =
            "owningType <> null and\n" +
            "(owningType.oclIsKindOf(PartDefinition) or\n" +
            " owningType.oclIsKindOf(PartUsage)) implies\n" +
            "    specializesFromLibrary('Parts::Part::exhibitedStates')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(owningType != null && ((owningType is PartDefinition) || (owningType is PartUsage))) || specializesFromLibrary(\"Parts::Part::exhibitedStates\"))",
            result);
    }

    [Fact]
    public void SysML_DeriveTypeOwnedConjugator()
    {
        var ocl =
            "ownedConjugator =\n" +
            "    let ownedConjugators: Sequence(Conjugator) = \n" +
            "        ownedRelationship->selectByKind(Conjugation) in\n" +
            "    if ownedConjugators->isEmpty() then null \n" +
            "    else ownedConjugators->at(1) endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedConjugator == ownedRelationship.OfType<Conjugation>().Select(ownedConjugators => (!ownedConjugators.Any() ? null : ownedConjugators.ElementAt(1 - 1)))",
            result);
    }

    [Fact]
    public void SysML_ValidateActorMembershipOwningType()
    {
        var ocl =
            "owningType.oclIsKindOf(RequirementUsage) or\n" +
            "owningType.oclIsKindOf(RequirementDefinition) or\n" +
            "owningType.oclIsKindOf(CaseDefinition) or\n" +
            "owningType.oclIsKindOf(CaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(owningType is RequirementUsage) || (owningType is RequirementDefinition) || (owningType is CaseDefinition) || (owningType is CaseUsage)",
            result);
    }

    [Fact]
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "verifiedRequirement == (objectiveRequirement == null ? new List<dynamic> {  } : objectiveRequirement.featureMembership.OfType<RequirementVerificationMembership>().Distinct().ToHashSet())",
            result);
    }

    [Fact]
    public void SysML_CheckConcernUsageFramedConcernSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(FramedConcernMembership) implies\n" +
            "    specializesFromLibrary('Requirements::RequirementCheck::concerns')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(owningFeatureMembership != null && (owningFeatureMembership is FramedConcernMembership)) || specializesFromLibrary(\"Requirements::RequirementCheck::concerns\"))",
            result);
    }

    [Fact]
    public void SysML_ValidateBehaviorSpecialization()
    {
        var ocl =
            "ownedSpecialization.general->forAll(not oclIsKindOf(Structure))";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedSpecialization.general.All(it => !(Structure))",
            result);
    }

    [Fact]
    public void SysML_CheckUseCaseUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('UseCases::useCases')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "specializesFromLibrary(\"UseCases::useCases\")",
            result);
    }

    [Fact]
    public void SysML_CheckFlowSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Transfers::transfers')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "specializesFromLibrary(\"Transfers::transfers\")",
            result);
    }

    [Fact]
    public void SysML_DeriveViewUsageViewCondition()
    {
        var ocl =
            "viewCondition = ownedMembership->\n" +
            "    selectByKind(ElementFilterMembership).\n" +
            "    condition";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "viewCondition == ownedMembership.OfType<ElementFilterMembership>()",
            result);
    }

    [Fact]
    public void SysML_DeriveOwningMembershipOwnedMemberName()
    {
        var ocl =
            "ownedMemberName = ownedMemberElement.name";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedMemberName == ownedMemberElement.name",
            result);
    }

    [Fact]
    public void SysML_DeriveOwningMembershipOwnedMemberShortName()
    {
        var ocl =
            "ownedMemberShortName = ownedMemberElement.shortName";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedMemberShortName == ownedMemberElement.shortName",
            result);
    }

    [Fact]
    public void SysML_CheckPartUsageStakeholderSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(StakeholderMembership) implies\n" +
            "    specializesFromLibrary('Requirements::RequirementCheck::stakeholders')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(owningFeatureMembership != null && (owningFeatureMembership is StakeholderMembership)) || specializesFromLibrary(\"Requirements::RequirementCheck::stakeholders\"))",
            result);
    }

    [Fact]
    public void SysML_CheckRenderingUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Views::renderings')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "specializesFromLibrary(\"Views::renderings\")",
            result);
    }

    [Fact]
    public void SysML_CheckConnectorObjectSpecialization()
    {
        var ocl =
            "association->exists(oclIsKindOf(AssociationStructure)) implies\n" +
            "    specializesFromLibrary('Objects::linkObjects')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(association.Any(it => (AssociationStructure))) || specializesFromLibrary(\"Objects::linkObjects\"))",
            result);
    }

    [Fact]
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "referencedRendering == ownedRendering.referencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? ownedRendering : ((referencedFeature is RenderingUsage) ? ((RenderingUsage)refrencedFeature) : null)))",
            result);
    }

    [Fact]
    public void SysML_ValidateFeatureOwnedReferenceSubsetting()
    {
        var ocl =
            "ownedSubsetting->selectByKind(ReferenceSubsetting)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedSubsetting.OfType<ReferenceSubsetting>().Count() <= 1",
            result);
    }

    [Fact]
    public void SysML_ValidateFeatureCrossFeatureSpecialization()
    {
        var ocl =
            "crossFeature <> null implies\n" +
            "    ownedRedefinition.redefinedFeature.crossFeature->\n" +
            "            forAll(f | f <> null implies crossFeature.specializes(f))";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(crossFeature != null) || ownedRedefinition.redefinedFeature.crossFeature.All(f => (!(f != null) || crossFeature.specializes(f))))",
            result);
    }

    [Fact]
    public void SysML_ValidateExpressionResultExpressionMembership()
    {
        var ocl =
            "membership->selectByKind(ResultExpressionMembership)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "membership.OfType<ResultExpressionMembership>().Count() <= 1",
            result);
    }

    [Fact]
    public void SysML_CheckItemDefinitionSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Items::Item')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "specializesFromLibrary(\"Items::Item\")",
            result);
    }

    [Fact]
    public void SysML_CheckForLoopActionUsageVarRedefinition()
    {
        var ocl =
            "loopVariable <> null and\n" +
            "loopVariable.redefinesFromLibrary('Actions::ForLoopAction::var')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "loopVariable != null && loopVariable.redefinesFromLibrary(\"Actions::ForLoopAction::var\")",
            result);
    }

    [Fact]
    public void SysML_DeriveMembershipMemberElementId()
    {
        var ocl =
            "memberElementId = memberElement.elementId";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "memberElementId == memberElement.elementId",
            result);
    }

    [Fact]
    public void SysML_ValidateAnnotationAnnotatedElementOwnership()
    {
        var ocl =
            "(owningAnnotatedElement <> null) = (ownedAnnotatingElement <> null)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(owningAnnotatedElement != null) == (ownedAnnotatingElement != null)",
            result);
    }

    [Fact]
    public void SysML_CheckAssignmentActionUsageAccessedFeatureRedefinition()
    {
        var ocl =
            "let targetParameter : Feature = inputParameter(1) in\n" +
            "targetParameter <> null and\n" +
            "targetParameter.ownedFeature->notEmpty() and\n" +
            "targetParameter->first().ownedFeature->notEmpty() and\n" +
            "targetParameter->first().ownedFeature->first().\n" +
            "    redefines('AssigmentAction::target::startingAt::accessedFeature')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "inputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.ownedFeature.Any() && targetParameter.First().ownedFeature.Any() && targetParameter.First().ownedFeature.First().redefines(\"AssigmentAction::target::startingAt::accessedFeature\"))",
            result);
    }

    [Fact]
    public void SysML_ValidateTransitionUsageSuccession()
    {
        var ocl =
            "let successions : Sequence(Successions) = \n" +
            "    ownedMember->selectByKind(Succession) in\n" +
            "successions->notEmpty() and\n" +
            "successions->at(1).targetFeature.featureTarget->\n" +
            "    forAll(oclIsKindOf(ActionUsage))";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "ownedMember.OfType<Succession>().Select(successions => successions.Any() && successions.ElementAt(1 - 1).featureTarget.All(it => (ActionUsage)))",
            result);
    }

    [Fact]
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.Equal(
            "(!(owningfeatureMembership != null && (owningfeatureMembership is ObjectiveMembership)) || owningType.ownedSpecialization.general.All(gen => ((!((gen is CaseDefinition)) || redefines(((CaseDefinition)gen).objectiveRequirement))) && ((!((gen is CaseUsage)) || redefines(((CaseUsage)gen).objectiveRequirement)))))",
            result);
    }
}
