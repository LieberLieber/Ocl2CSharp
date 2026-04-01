using Ocl2CSharp;

namespace Ocl2CSharp.Tests;

/// <summary>
/// Tests for the SysML OCL constraints and derived properties defined in testdata/SysML_OCL.txt.
/// Each test corresponds to one rule in that file.
/// </summary>
public class SysML_OclTests
{
    // -------------------------------------------------------------------------
    // VariantMemberships
    // -------------------------------------------------------------------------

    [Fact]
    public void VariantMemberships_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ownedMembership->selectByKind(VariantMembership)");
        Assert.Equal("ownedMembership.OfType<VariantMembership>()", result);
    }

    // -------------------------------------------------------------------------
    // ViewRendering
    // -------------------------------------------------------------------------

    [Fact]
    public void ViewRendering_IsConverted()
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

    // -------------------------------------------------------------------------
    // AllFeaturesHaveType
    // -------------------------------------------------------------------------

    [Fact]
    public void AllFeaturesHaveType_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ownedFeature->forAll(f | f.type->notEmpty())");
        Assert.Equal("ownedFeature.All(f => f.type.Any())", result);
    }

    // -------------------------------------------------------------------------
    // MemberNamesUnique
    // -------------------------------------------------------------------------

    [Fact]
    public void MemberNamesUnique_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("member->isUnique(m | m.name)");
        Assert.Equal("(member.Select(m => m.name).Distinct().Count() == member.Count())", result);
    }

    // -------------------------------------------------------------------------
    // OwnedRelationshipsValid
    // -------------------------------------------------------------------------

    [Fact]
    public void OwnedRelationshipsValid_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ownedRelationship->forAll(r | r.owner = self)");
        Assert.Equal("ownedRelationship.All(r => r.owner == self)", result);
    }

    // -------------------------------------------------------------------------
    // DirectedFeaturesHaveDirection
    // -------------------------------------------------------------------------

    [Fact]
    public void DirectedFeaturesHaveDirection_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("directedFeature->forAll(df | df.direction <> null)");
        Assert.Equal("directedFeature.All(df => df.direction != null)", result);
    }

    // -------------------------------------------------------------------------
    // OwnedMembershipNotEmpty
    // -------------------------------------------------------------------------

    [Fact]
    public void OwnedMembershipNotEmpty_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ownedMembership->notEmpty()");
        Assert.Equal("ownedMembership.Any()", result);
    }

    // -------------------------------------------------------------------------
    // OwnedFeatureCount
    // -------------------------------------------------------------------------

    [Fact]
    public void OwnedFeatureCount_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("ownedFeature->size() >= 0");
        Assert.Equal("ownedFeature.Count() >= 0", result);
    }

    // -------------------------------------------------------------------------
    // FeatureDirectionConsistency
    // -------------------------------------------------------------------------

    [Fact]
    public void FeatureDirectionConsistency_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("if isEnd then direction <> null else direction = null endif");
        Assert.Equal("(isEnd ? direction != null : direction == null)", result);
    }

    // -------------------------------------------------------------------------
    // ConnectorEndCount
    // -------------------------------------------------------------------------

    [Fact]
    public void ConnectorEndCount_IsConverted()
    {
        var result = OclToCSharpConverter.Convert("connectorEnd->size() >= 2");
        Assert.Equal("connectorEnd.Count() >= 2", result);
    }
}
