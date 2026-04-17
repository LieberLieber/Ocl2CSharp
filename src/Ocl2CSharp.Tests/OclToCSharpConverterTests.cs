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
        var result = OclToCSharpConverter.Convert("age >= 0 and age <= 150");
        Assert.That(result, Is.EqualTo("age >= 0 && age <= 150"));
    }

    [Test]
    public void LogicalAnd_Ampersand_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0 & age <= 150");
        Assert.That(result, Is.EqualTo("age >= 0 && age <= 150"));
    }

    [Test]
    public void LogicalOr_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("email <> null or phone <> null");
        Assert.That(result, Is.EqualTo("email != null || phone != null"));
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
        Assert.That(result, Is.EqualTo("name == \"Alice\""));
    }

    [Test]
    public void Inequality_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null");
        Assert.That(result, Is.EqualTo("name != null"));
    }

    [Test]
    public void Inequality_SlashEquals_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value /= 0");
        Assert.That(result, Is.EqualTo("value != 0"));
    }

    [Test]
    public void LessThan_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age < 18");
        Assert.That(result, Is.EqualTo("age < 18"));
    }

    [Test]
    public void GreaterThanOrEqual_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("age >= 0");
        Assert.That(result, Is.EqualTo("age >= 0"));
    }

    // -------------------------------------------------------------------------
    // Arithmetic operators
    // -------------------------------------------------------------------------

    [Test]
    public void Addition_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance + interest >= 0");
        Assert.That(result, Is.EqualTo("balance + interest >= 0"));
    }

    [Test]
    public void Multiplication_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance * rate > 0");
        Assert.That(result, Is.EqualTo("balance * rate > 0"));
    }

    [Test]
    public void ModOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance mod 2 = 0");
        Assert.That(result, Is.EqualTo("balance % 2 == 0"));
    }

    [Test]
    public void DivOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("total div numItems > 0");
        Assert.That(result, Is.EqualTo("total / numItems > 0"));
    }

    // -------------------------------------------------------------------------
    // Unary operators
    // -------------------------------------------------------------------------

    [Test]
    public void NotOperator_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("not active");
        Assert.That(result, Is.EqualTo("!active"));
    }

    [Test]
    public void UnaryMinus_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("-balance > 0");
        Assert.That(result, Is.EqualTo("-balance > 0"));
    }

    // -------------------------------------------------------------------------
    // Conditional expression
    // -------------------------------------------------------------------------

    [Test]
    public void ConditionalExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif");
        Assert.That(result, Is.EqualTo("(age >= 18 ? isAdult : isMinor)"));
    }

    [Test]
    public void ConditionalExpression_UseIfStatement_EmitsIfElseBlock()
    {
        var result = OclToCSharpConverter.Convert("if age >= 18 then isAdult else isMinor endif", useIfStatement: true).Replace("\r", "");
        Assert.That(result, Is.EqualTo("if (age >= 18)\n{\n\treturn isAdult;\n}\nelse\n{\n\treturn isMinor;\n}"));
    }

    [Test]
    public void ConditionalExpression_DefaultMode_EmitsTernary()
    {
        // Verify that the default (no second argument) still produces ternary output.
        var result = OclToCSharpConverter.Convert("if x > 0 then positive else nonPositive endif");
        Assert.That(result, Is.EqualTo("(x > 0 ? positive : nonPositive)"));
    }

    // -------------------------------------------------------------------------
    // Literals
    // -------------------------------------------------------------------------

    [Test]
    public void NullLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name <> null");
        Assert.That(result, Is.EqualTo("name != null"));
    }

    [Test]
    public void BooleanLiteralTrue_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = true");
        Assert.That(result, Is.EqualTo("active == true"));
    }

    [Test]
    public void BooleanLiteralFalse_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("active = false");
        Assert.That(result, Is.EqualTo("active == false"));
    }

    [Test]
    public void IntLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("balance > 0");
        Assert.That(result, Is.EqualTo("balance > 0"));
    }

    [Test]
    public void StringLiteralSingleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = 'Alice'");
        Assert.That(result, Is.EqualTo("name == \"Alice\""));
    }

    [Test]
    public void StringLiteralDoubleQuote_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name = \"Alice\"");
        Assert.That(result, Is.EqualTo("name == \"Alice\""));
    }

    [Test]
    public void EnumerationLiteral_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("status = Status::Active");
        Assert.That(result, Is.EqualTo("status == Status.Active"));
    }

    // -------------------------------------------------------------------------
    // Navigation / member access
    // -------------------------------------------------------------------------

    [Test]
    public void MemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ceo.name <> null");
        Assert.That(result, Is.EqualTo("ceo.name != null"));
    }

    [Test]
    public void NestedMemberAccess_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("company.ceo.name <> null");
        Assert.That(result, Is.EqualTo("company.ceo.name != null"));
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
        Assert.That(result, Is.EqualTo("employees.All(e => e.age >= 18)"));
    }

    [Test]
    public void ExistsOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->exists(e | e.role = 'CEO')");
        Assert.That(result, Is.EqualTo("employees.Any(e => e.role == \"CEO\")"));
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
        Assert.That(result, Is.EqualTo("employees.Count() > 0"));
    }

    [Test]
    public void IsEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->isEmpty()");
        Assert.That(result, Is.EqualTo("!contracts.Any()"));
    }

    [Test]
    public void NotEmptyOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("contracts->notEmpty()");
        Assert.That(result, Is.EqualTo("contracts.Any()"));
    }

    [Test]
    public void IncludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->includes(alice)");
        Assert.That(result, Is.EqualTo("employees.Contains(alice)"));
    }

    [Test]
    public void ExcludesOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("employees->excludes(alice)");
        Assert.That(result, Is.EqualTo("!employees.Contains(alice)"));
    }

    [Test]
    public void SumOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("salaries->sum() > 0");
        Assert.That(result, Is.EqualTo("salaries.Sum() > 0"));
    }

    [Test]
    public void MaxOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->max()");
        Assert.That(result, Is.EqualTo("scores.Max()"));
    }

    [Test]
    public void MinOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("scores->min()");
        Assert.That(result, Is.EqualTo("scores.Min()"));
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
        Assert.That(result, Is.EqualTo("items.ToHashSet()"));
    }

    [Test]
    public void AsSequenceOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("items->asSequence()");
        Assert.That(result, Is.EqualTo("items.ToList()"));
    }

    [Test]
    public void ToUpperCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toUpperCase()");
        Assert.That(result, Is.EqualTo("name.ToUpper()"));
    }

    [Test]
    public void ToLowerCaseOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name->toLowerCase()");
        Assert.That(result, Is.EqualTo("name.ToLower()"));
    }

    [Test]
    public void ToIntegerOperation_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("value->toInteger()");
        Assert.That(result, Is.EqualTo("Convert.ToInt32(value)"));
    }

    // -------------------------------------------------------------------------
    // Dot (.) OCL operations
    // -------------------------------------------------------------------------

    [Test]
    public void OclIsUndefined_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("name.oclIsUndefined() = false");
        Assert.That(result, Is.EqualTo("(name == null) == false"));
    }

    [Test]
    public void OclIsKindOf_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("obj.oclIsKindOf(Person)");
        Assert.That(result, Is.EqualTo("(obj is Person)"));
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
        Assert.That(result, Is.EqualTo(
            "featureMembership.OfType<ViewRenderingMembership>().Select(renderings => (!renderings.Any() ? null : renderings.First().referencedRendering))"));
 
    }

    // -------------------------------------------------------------------------
    // Parenthesized expression
    // -------------------------------------------------------------------------

    [Test]
    public void ParenthesizedExpression_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("(age + 1) > 0");
		Assert.That(result, Is.EqualTo("(age + 1) > 0"));
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
		Assert.That(result, Is.EqualTo("ownedMembership.OfType<VariantMembership>()"));
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is OccurrenceUsage))"));
 
    }

    [Test]
    public void SysML_DeriveCalculationUsageCalculation()
    {
        var ocl =
            "calculation = action->selectByKind(CalculationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "calculation == action.OfType<CalculationUsage>()"));
 
    }

    [Test]
    public void SysML_ValidateSubjectMembershipOwningType()
    {
        var ocl =
            "owningType.oclIsType(RequirementDefinition) or\n" +
            "owningType.oclIsType(RequiremenCaseRequirementDefinition) or\n" +
            "owningType.oclIsType(CaseDefinition) or\n" +
            "owningType.oclIsType(CaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(owningType is RequirementDefinition) || (owningType is RequiremenCaseRequirementDefinition) || (owningType is CaseDefinition) || (owningType is CaseUsage)"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "referent == ownedMembership.Where(item => !(item is ParameterMembership)).Select(nonParameterMemberships => (!nonParameterMemberships.Any() || !(nonParameterMemberships.First().memberElement is Feature) ? null : ((Feature)nonParameterMemberships.First().memberElement)))"));
 
    }

    [Test]
    public void SysML_DeriveTypeOwnedIntersecting()
    {
        var ocl =
            "ownedRelationship->selectByKind(Intersecting)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedRelationship.OfType<Intersecting>()"));
 
    }

    [Test]
    public void SysML_ValidatePortDefinitionConjugatedPortDefinition()
    {
        var ocl =
            "not oclIsKindOf(ConjugatedPortDefinition) implies\n" +
            "    ownedMember->\n" +
            "        selectByKind(ConjugatedPortDefinition)->\n" +
            "        size() = 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(!(this is ConjugatedPortDefinition)) || ownedMember.OfType<ConjugatedPortDefinition>().Count() == 1)"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(direction == null && ownedSpecializations.All(item => item.isImplied)) || ownedMembership.OfType<FeatureValue>().All(fv => specializes(fv.value.result)))"));
 
    }

    [Test]
    public void SysML_DeriveFeatureOwnedTypeFeaturing()
    {
        var ocl =
            "ownedTypeFeaturing = ownedRelationship->selectByKind(TypeFeaturing)->\n" +
            "    select(tf | tf.featureOfType = self)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedTypeFeaturing == ownedRelationship.OfType<TypeFeaturing>().Where(tf => tf.featureOfType == this)"));
 
    }

    [Test]
    public void SysML_CheckMetadataAccessExpressionSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Performances::metadataAccessEvaluations')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "specializesFromLibrary(\"Performances::metadataAccessEvaluations\")"));
 
    }

    [Test]
    public void SysML_ValidateConstructorExpressionOwnedFeatures()
    {
        var ocl =
            "ownedFeatures->excluding(result)->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "!ownedFeatures.Where(item => item != result).Any()"));
 
    }

    [Test]
    public void SysML_ValidateAssociationBinarySpecialization()
    {
        var ocl =
            "associationEnds->size() > 2 implies\n" +
            "    not specializesFromLibrary('Links::BinaryLink')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(associationEnds.Count() > 2) || !specializesFromLibrary(\"Links::BinaryLink\"))"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "subjectParameter == featureMembership.OfType<SubjectMembership>().Select(subjectMems => (!subjectMems.Any() ? null : subjectMems.First().ownedSubjectParameter))"));
 
    }

    [Test]
    public void SysML_DeriveDefinitionOwnedConnection()
    {
        IEnumerable<string> test;
        var ocl =
            "ownedConnection = ownedUsage->selectByKind(ConnectorAsUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedConnection == ownedUsage.OfType<ConnectorAsUsage>()"));
 
    }

    [Test]
    public void SysML_DeriveDefinitionOwnedConstraint()
    {
        var ocl =
            "ownedConstraint = ownedUsage->selectByKind(ConstraintUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedConstraint == ownedUsage.OfType<ConstraintUsage>()"));
 
    }

    [Test]
    public void SysML_CheckOccurrenceDefinitionMultiplicitySpecialization()
    {
        var ocl =
            "isIndividual implies\n" +
            "    multiplicity <> null and\n" +
            "    multiplicity.specializesFromLibrary('Base::zeroOrOne')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(isIndividual) || multiplicity != null) && multiplicity.specializesFromLibrary(\"Base::zeroOrOne\")"));
 
    }

    [Test]
    public void SysML_ValidateSpecificationSpecificNotConjugated()
    {
        var ocl =
            "not specific.isConjugated";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "!specific.isConjugated"));
 
    }

    [Test]
    public void SysML_DeriveInstantiationExpressionInstantiatedType()
    {
        var ocl =
            "instantiatedType = instantiatedType()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "instantiatedType == instantiatedType()"));
 
    }

    [Test]
    public void SysML_ValidateConnectorBinarySpecialization()
    {
        var ocl =
            "connectorEnds->size() > 2 implies\n" +
            "    not specializesFromLibrary('Links::BinaryLink')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(connectorEnds.Count() > 2) || !specializesFromLibrary(\"Links::BinaryLink\"))"));
 
    }

    [Test]
    public void SysML_DeriveElementOwnedElement()
    {
        var ocl =
            "ownedElement = ownedRelationship.ownedRelatedElement";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedElement == ownedRelationship.ownedRelatedElement"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(isComposite && owningType != null && ((owningType is StateDefinition) || (owningType is StateUsage)) && source != null && (source is StateUsage)) || specializesFromLibrary(\"States::StateAction::stateTransitions\"))"));
 
    }

    [Test]
    public void SysML_DeriveRequirementDefinitionFramedConcern()
    {
        var ocl =
            "framedConcern = featureMembership->\n" +
            "    selectByKind(FramedConcernMembership).\n" +
            "    ownedConcern";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "framedConcern == featureMembership.OfType<FramedConcernMembership>().Select(item => item.ownedConcern)"));
 
    }

    [Test]
    public void SysML_CheckUsageVariationUsageSpecialization()
    {
        var ocl =
            "owningVariationUsage <> null implies\n" +
            "    specializes(owningVariationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(owningVariationUsage != null) || specializes(owningVariationUsage))"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "targetFeature == ownedMembership.Where(item => !(item is ParameterMembership)).Select(nonParameterMemberships => (!nonParameterMemberships.Any() || !(nonParameterMemberships.First().memberElement is Feature) ? null : ((Feature)nonParameterMemberships.First().memberElement)))"));
 
    }

    [Test]
    public void SysML_DeriveUsageNestedAllocation()
    {
        var ocl =
            "nestedAllocation = nestedUsage->selectByKind(AllocationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "nestedAllocation == nestedUsage.OfType<AllocationUsage>()"));
 
    }

    [Test]
    public void SysML_ValidateStateDefinitionParallelSubactions()
    {
        var ocl =
            "isParallel implies\n" +
            "    ownedAction.incomingTransition->isEmpty() and\n" +
            "    ownedAction.outgoingTransition->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(isParallel) || !ownedAction.incomingTransition.Any()) && !ownedAction.outgoingTransition.Any()"));
 
    }

    [Test]
    public void SysML_ValidateInvocationExpressionOwnedFeatures()
    {
        var ocl =
            "ownedFeature->forAll(f |\n" +
            "    f <> result implies \n" +
            "        f.direction = FeatureDirectionKind::_'in')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedFeature.All(f => (!(f != result) || f.direction == FeatureDirectionKind._))"));
 
    }

    [Test]
    public void SysML_DeriveAnnotationAnnotatingElement()
    {
        var ocl =
            "annotatingElement =\n" +
            "    if ownedAnnotatingElement <> null then ownedAnnotatingElement\n" +
            "    else owningAnnotatingElement\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "annotatingElement == (ownedAnnotatingElement != null ? ownedAnnotatingElement : owningAnnotatingElement)"));
 
    }

    [Test]
    public void SysML_DeriveStepBehavior()
    {
        var ocl =
            "behavior = type->selectByKind(Behavior)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "behavior == type.OfType<Behavior>()"));
 
    }

    [Test]
    public void SysML_ValidateSatisfyRequirementUsageReference()
    {
        var ocl =
            "referencedFeatureTarget() <> null implies\n" +
            "    referencedFeatureTarget().oclIsKindOf(RequirementUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is RequirementUsage))"));
 
    }

    [Test]
    public void SysML_ValidateTypeOwnedIntersectingNotOne()
    {
        var ocl =
            "ownedIntersecting->size() <> 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedIntersecting.Count() != 1"));
 
    }

    [Test]
    public void SysML_DeriveTypeInheritedFeature()
    {
        var ocl =
            "inheritedFeature = inheritedMemberships->\n" +
            "    selectByKind(FeatureMembership).memberFeature";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "inheritedFeature == inheritedMemberships.OfType<FeatureMembership>().Select(item => item.memberFeature)"));
 
    }

    [Test]
    public void SysML_DeriveViewUsageExposedElement()
    {
        var ocl =
            "exposedElement = ownedImport->selectByKind(Expose).\n" +
            "    importedMemberships(Set{}).memberElement->\n" +
            "    select(elm | includeAsExposed(elm))->\n" +
            "    asOrderedSet()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "exposedElement == ownedImport.OfType<Expose>().importedMemberships(new HashSet<dynamic> {  }).memberElement.Where(elm => includeAsExposed(elm)).Distinct().ToHashSet()"));
 
    }

    [Test]
    public void SysML_DeriveViewDefinitionView()
    {
        var ocl =
            "view = usage->selectByKind(ViewUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "view == usage.OfType<ViewUsage>()"));
 
    }

    [Test]
    public void SysML_CheckAnalysisCaseUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('AnalysisCases::analysisCases')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "specializesFromLibrary(\"AnalysisCases::analysisCases\")"));
 
    }

    [Test]
    public void SysML_CheckVerificationCaseUsageSubVerificationCaseSpecialization()
    {
        var ocl =
            "isComposite and owningType <> null and\n" +
            "    (owningType.oclIsKindOf(VerificationCaseDefinition) or\n" +
            "     owningType.oclIsKindOf(VerificationCaseUsage)) implies \n" +
            "    specializesFromLibrary('VerificationCases::VerificationCase::subVerificationCases')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(isComposite && owningType != null && ((owningType is VerificationCaseDefinition) || (owningType is VerificationCaseUsage))) || specializesFromLibrary(\"VerificationCases::VerificationCase::subVerificationCases\"))"));
 
    }

    [Test]
    public void SysML_DeriveNamespaceOwnedMembership()
    {
        var ocl =
            "ownedMembership = ownedRelationship->selectByKind(Membership)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedMembership == ownedRelationship.OfType<Membership>()"));
 
    }

    [Test]
    public void SysML_CheckTransitionUsagePayloadSpecialization()
    {
        var ocl =
            "triggerAction->notEmpty() implies\n" +
            "    let payloadParameter : Feature = inputParameter(2) in\n" +
            "    payloadParameter <> null and\n" +
            "    payloadParameter.subsetsChain(triggerAction->at(1), triggerPayloadParameter())";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(triggerAction.Any()) || payloadParameter is Feature == inputParameter(2))"));
 
    }

    [Test]
    public void SysML_ValidateFeatureValueOverriding()
    {
        var ocl =
            "featureWithValue.redefinition.redefinedFeature->\n" +
            "    closure(redefinition.redefinedFeature).valuation->\n" +
            "    forAll(isDefault)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "featureWithValue.redefinition.redefinedFeature.Closure(item => item.redefinition.redefinedFeature).valuation.All(item => item.isDefault)"));
 
    }

    [Test]
    public void SysML_ValidateFlowEndOwningType()
    {
        var ocl =
            "owningType <> null and owningType.oclIsKindOf(Flow)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "owningType != null && (owningType is Flow)"));
 
    }

    [Test]
    public void SysML_CheckPayloadFeatureRedefinition()
    {
        var ocl =
            "redefinesFromLibrary('Transfers::Transfer::payload')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "redefinesFromLibrary(\"Transfers::Transfer::payload\")"));
 
    }

    [Test]
    public void SysML_DeriveNamespaceImportImportedElement()
    {
        var ocl =
            "importedElement = importedNamespace";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "importedElement == importedNamespace"));
 
    }

    [Test]
    public void SysML_DeriveElementName()
    {
        var ocl =
            "name = effectiveName()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "name == effectiveName()"));
 
    }

    [Test]
    public void SysML_ValidateUsageVariationOwnedFeatureMembership()
    {
        var ocl =
            "isVariation implies ownedFeatureMembership->isEmpty()";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(isVariation) || !ownedFeatureMembership.Any())"));
 
    }

    [Test]
    public void SysML_ValidateSubsettingUniquenessConformance()
    {
        var ocl =
            "subsettedFeature.isUnique implies subsettingFeature.isUnique";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(subsettedFeature.isUnique) || subsettingFeature.isUnique)"));
 
    }

    [Test]
    public void SysML_DeriveUsageNestedVerificationCase()
    {
        var ocl =
            "nestedVerificationCase = nestedUsage->selectByKind(VerificationCaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "nestedVerificationCase == nestedUsage.OfType<VerificationCaseUsage>()"));
 
    }

    [Test]
    public void SysML_CheckRequirementUsageRequirementVerificationSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(RequirementVerificationMembership) implies\n" +
            "    specializesFromLibrary('VerificationCases::VerificationCase::obj::requirementVerifications')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(owningFeatureMembership != null && (owningFeatureMembership is RequirementVerificationMembership)) || specializesFromLibrary(\"VerificationCases::VerificationCase::obj::requirementVerifications\"))"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedAnnotatingElement == ownedRelatedElement.OfType<AnnotatingElement>().Select(ownedAnnotatingElements => (!ownedAnnotatingElements.Any() ? null : ownedAnnotatingElements.First()))"));
 
    }

    [Test]
    public void SysML_DeriveAllocationDefinitionAllocation()
    {
        var ocl =
            "allocation = usage->selectAsKind(AllocationUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "allocation == usage.selectAsKind(AllocationUsage)"));
 
    }

    [Test]
    public void SysML_ValidateConnectorRelatedFeatures()
    {
        var ocl =
            "not isAbstract implies relatedFeature->size() >= 2";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(!isAbstract) || relatedFeature.Count() >= 2)"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "mayTimeVary == owningType != null && owningType.specializesFromLibrary(\"Occurrences::Occurrence\") && !(isPortion || specializesFromLibrary(\"Links::SelfLink\") || specializesFromLibrary(\"Occurrences::HappensLink\") || isComposite && specializesFromLibrary(\"Actions::Action\"))"));
 
    }

    [Test]
    public void SysML_ValidateFeatureOwnedCrossSubsetting()
    {
        var ocl =
            "ownedSubsetting->selectByKind(CrossSubsetting)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedSubsetting.OfType<CrossSubsetting>().Count() <= 1"));
 
    }

    [Test]
    public void SysML_CheckAllocationUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Allocations::allocations')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "specializesFromLibrary(\"Allocations::allocations\")"));
 
    }

    [Test]
    public void SysML_DeriveOwningNamespace()
    {
        var ocl =
            "owningNamespace =\n" +
            "    if owningMembership = null then null\n" +
            "    else owningMembership.membershipOwningNamespace\n" +
            "    endif";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "owningNamespace == (owningMembership == null ? null : owningMembership.membershipOwningNamespace)"));
 
    }

    [Test]
    public void SysML_DeriveUsageNestedPart()
    {
        var ocl =
            "nestedPart = nestedUsage->selectByKind(PartUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "nestedPart == nestedUsage.OfType<PartUsage>()"));
 
    }

    [Test]
    public void SysML_DeriveNamespaceMembers()
    {
        var ocl =
            "member = membership.memberElement";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "member == membership.memberElement"));
 
    }

    [Test]
    public void SysML_CheckExhibitStateUsageSpecialization()
    {
        var ocl =
            "owningType <> null and\n" +
            "(owningType.oclIsKindOf(PartDefinition) or\n" +
            " owningType.oclIsKindOf(PartUsage)) implies\n" +
            "    specializesFromLibrary('Parts::Part::exhibitedStates')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(owningType != null && ((owningType is PartDefinition) || (owningType is PartUsage))) || specializesFromLibrary(\"Parts::Part::exhibitedStates\"))"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedConjugator == ownedRelationship.OfType<Conjugation>().Select(ownedConjugators => (!ownedConjugators.Any() ? null : ownedConjugators.ElementAt(0)))"));
 
    }

    [Test]
    public void SysML_ValidateActorMembershipOwningType()
    {
        var ocl =
            "owningType.oclIsKindOf(RequirementUsage) or\n" +
            "owningType.oclIsKindOf(RequirementDefinition) or\n" +
            "owningType.oclIsKindOf(CaseDefinition) or\n" +
            "owningType.oclIsKindOf(CaseUsage)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(owningType is RequirementUsage) || (owningType is RequirementDefinition) || (owningType is CaseDefinition) || (owningType is CaseUsage)"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "verifiedRequirement == (objectiveRequirement == null ? new List<dynamic> {  } : objectiveRequirement.featureMembership.OfType<RequirementVerificationMembership>().Select(item => item.verifiedRequirement).Distinct().ToHashSet())"));
 
    }

    [Test]
    public void SysML_CheckConcernUsageFramedConcernSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(FramedConcernMembership) implies\n" +
            "    specializesFromLibrary('Requirements::RequirementCheck::concerns')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(owningFeatureMembership != null && (owningFeatureMembership is FramedConcernMembership)) || specializesFromLibrary(\"Requirements::RequirementCheck::concerns\"))"));
 
    }

    [Test]
    public void SysML_ValidateBehaviorSpecialization()
    {
        var ocl =
            "ownedSpecialization.general->forAll(not oclIsKindOf(Structure))";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedSpecialization.general.All(item => !(item is Structure))"));
 
    }

    [Test]
    public void SysML_CheckUseCaseUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('UseCases::useCases')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "specializesFromLibrary(\"UseCases::useCases\")"));
 
    }

    [Test]
    public void SysML_CheckFlowSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Transfers::transfers')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "specializesFromLibrary(\"Transfers::transfers\")"));
 
    }

    [Test]
    public void SysML_DeriveViewUsageViewCondition()
    {
        var ocl =
            "viewCondition = ownedMembership->\n" +
            "    selectByKind(ElementFilterMembership).\n" +
            "    condition";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "viewCondition == ownedMembership.OfType<ElementFilterMembership>().Select(item => item.condition)"));
 
    }

    [Test]
    public void SysML_DeriveOwningMembershipOwnedMemberName()
    {
        var ocl =
            "ownedMemberName = ownedMemberElement.name";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedMemberName == ownedMemberElement.name"));
 
    }

    [Test]
    public void SysML_DeriveOwningMembershipOwnedMemberShortName()
    {
        var ocl =
            "ownedMemberShortName = ownedMemberElement.shortName";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedMemberShortName == ownedMemberElement.shortName"));
 
    }

    [Test]
    public void SysML_CheckPartUsageStakeholderSpecialization()
    {
        var ocl =
            "owningFeatureMembership <> null and\n" +
            "owningFeatureMembership.oclIsKindOf(StakeholderMembership) implies\n" +
            "    specializesFromLibrary('Requirements::RequirementCheck::stakeholders')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(owningFeatureMembership != null && (owningFeatureMembership is StakeholderMembership)) || specializesFromLibrary(\"Requirements::RequirementCheck::stakeholders\"))"));
 
    }

    [Test]
    public void SysML_CheckRenderingUsageSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Views::renderings')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "specializesFromLibrary(\"Views::renderings\")"));
 
    }

    [Test]
    public void SysML_CheckConnectorObjectSpecialization()
    {
        var ocl =
            "association->exists(oclIsKindOf(AssociationStructure)) implies\n" +
            "    specializesFromLibrary('Objects::linkObjects')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(association.Any(item => (item is AssociationStructure))) || specializesFromLibrary(\"Objects::linkObjects\"))"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "referencedRendering == ownedRendering.referencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? ownedRendering : ((referencedFeature is RenderingUsage) ? ((RenderingUsage)refrencedFeature) : null)))"));
 
    }

    [Test]
    public void SysML_ValidateFeatureOwnedReferenceSubsetting()
    {
        var ocl =
            "ownedSubsetting->selectByKind(ReferenceSubsetting)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedSubsetting.OfType<ReferenceSubsetting>().Count() <= 1"));
 
    }

    [Test]
    public void SysML_ValidateFeatureCrossFeatureSpecialization()
    {
        var ocl =
            "crossFeature <> null implies\n" +
            "    ownedRedefinition.redefinedFeature.crossFeature->\n" +
            "            forAll(f | f <> null implies crossFeature.specializes(f))";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(crossFeature != null) || ownedRedefinition.redefinedFeature.crossFeature.All(f => (!(f != null) || crossFeature.specializes(f))))"));
 
    }

    [Test]
    public void SysML_ValidateExpressionResultExpressionMembership()
    {
        var ocl =
            "membership->selectByKind(ResultExpressionMembership)->size() <= 1";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "membership.OfType<ResultExpressionMembership>().Count() <= 1"));
 
    }

    [Test]
    public void SysML_CheckItemDefinitionSpecialization()
    {
        var ocl =
            "specializesFromLibrary('Items::Item')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "specializesFromLibrary(\"Items::Item\")"));
 
    }

    [Test]
    public void SysML_CheckForLoopActionUsageVarRedefinition()
    {
        var ocl =
            "loopVariable <> null and\n" +
            "loopVariable.redefinesFromLibrary('Actions::ForLoopAction::var')";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "loopVariable != null && loopVariable.redefinesFromLibrary(\"Actions::ForLoopAction::var\")"));
 
    }

    [Test]
    public void SysML_DeriveMembershipMemberElementId()
    {
        var ocl =
            "memberElementId = memberElement.elementId";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "memberElementId == memberElement.elementId"));
 
    }

    [Test]
    public void SysML_ValidateAnnotationAnnotatedElementOwnership()
    {
        var ocl =
            "(owningAnnotatedElement <> null) = (ownedAnnotatingElement <> null)";
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(owningAnnotatedElement != null) == (ownedAnnotatingElement != null)"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "inputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.ownedFeature.Any() && targetParameter.First().ownedFeature.Any() && targetParameter.First().ownedFeature.First().redefines(\"AssigmentAction::target::startingAt::accessedFeature\"))"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "ownedMember.OfType<Succession>().Select(successions => successions.Any() && successions.ElementAt(0).featureTarget.All(item => (item is ActionUsage)))"));
 
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
        var result = OclToCSharpConverter.Convert(ocl);
        Assert.That(result, Is.EqualTo(
            "(!(owningfeatureMembership != null && (owningfeatureMembership is ObjectiveMembership)) || owningType.ownedSpecialization.general.All(gen => ((!((gen is CaseDefinition)) || redefines(((CaseDefinition)gen).objectiveRequirement))) && ((!((gen is CaseUsage)) || redefines(((CaseUsage)gen).objectiveRequirement)))))"));
 
    }
}
