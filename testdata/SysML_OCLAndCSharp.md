# ValidateEventOccurrenceUsageReference
### OCL
``` OCL 
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(OccurrenceUsage)
```
### C#
``` CSharp 
(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is OccurrenceUsage))
```
# DeriveCalculationUsageCalculation
### OCL
``` OCL 
action->selectByKind(CalculationUsage)
```
### C#
``` CSharp 
action.OfType<CalculationUsage>()
```
# ValidateSubjectMembershipOwningType
### OCL
``` OCL 
owningType.oclIsType(RequirementDefinition) or
owningType.oclIsType(RequiremenCaseRequirementDefinition) or
owningType.oclIsType(CaseDefinition) or
owningType.oclIsType(CaseUsage)
```
### C#
``` CSharp 
(owningType is RequirementDefinition) || (owningType is RequiremenCaseRequirementDefinition) || (owningType is CaseDefinition) || (owningType is CaseUsage)
```
# DeriveFeatureReferenceExpressionReferent
### OCL
``` OCL 
    let nonParameterMemberships : Sequence(Membership) = ownedMembership->reject(oclIsKindOf(ParameterMembership)) in
    if nonParameterMemberships->isEmpty() or
       not nonParameterMemberships->first().memberElement.oclIsKindOf(Feature)
    then null
    else nonParameterMemberships->first().memberElement.oclAsType(Feature)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.Where(item => !(item is ParameterMembership)).Select(nonParameterMemberships => (!nonParameterMemberships.Any() ||
!(nonParameterMemberships.First().memberElement is Feature) ? null :
((Feature)nonParameterMemberships.First().memberElement)))
```
# DeriveTypeOwnedIntersecting
### OCL
``` OCL 
ownedRelationship->selectByKind(Intersecting)
```
### C#
``` CSharp 
ownedRelationship.OfType<Intersecting>()
```
# ValidatePortDefinitionConjugatedPortDefinition
### OCL
``` OCL 
not oclIsKindOf(ConjugatedPortDefinition) implies
    ownedMember->
        selectByKind(ConjugatedPortDefinition)->
        size() = 1
```
### C#
``` CSharp 
(!(!(this is ConjugatedPortDefinition)) || ownedMember.OfType<ConjugatedPortDefinition>().Count() == 1)
```
# CheckFeatureValuationSpecialization
### OCL
``` OCL 
direction = null and
ownedSpecializations->forAll(isImplied) implies
    ownedMembership->
        selectByKind(FeatureValue)->
        forAll(fv | specializes(fv.value.result))
```
### C#
``` CSharp 
(!(direction == null && ownedSpecializations.All(item => item.isImplied)) || ownedMembership.OfType<FeatureValue>().All(fv => specializes(fv.value.result)))
```
# DeriveFeatureOwnedTypeFeaturing
### OCL
``` OCL 
ownedRelationship->selectByKind(TypeFeaturing)->
    select(tf | tf.featureOfType = self)
```
### C#
``` CSharp 
ownedRelationship.OfType<TypeFeaturing>().Where(tf => tf.featureOfType == this)
```
# CheckMetadataAccessExpressionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::metadataAccessEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::metadataAccessEvaluations")
```
# ValidateConstructorExpressionOwnedFeatures
### OCL
``` OCL 
ownedFeatures->excluding(result)->isEmpty()
```
### C#
``` CSharp 
!ownedFeatures.Where(item => item != result).Any()
```
# ValidateAssociationBinarySpecialization
### OCL
``` OCL 
associationEnds->size() > 2 implies
    not specializesFromLibrary('Links::BinaryLink')
```
### C#
``` CSharp 
(!(associationEnds.Count() > 2) || !specializesFromLibrary("Links::BinaryLink"))
```
# DeriveCaseDefinitionSubjectParameter
### OCL
``` OCL 
    let subjectMems : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjectMems->isEmpty() then null
    else subjectMems->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
featureMembership.OfType<SubjectMembership>().Select(subjectMems => (!subjectMems.Any() ? null : subjectMems.First().ownedSubjectParameter))
```
# DeriveDefinitionOwnedConnection
### OCL
``` OCL 
ownedUsage->selectByKind(ConnectorAsUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ConnectorAsUsage>()
```
# DeriveDefinitionOwnedConstraint
### OCL
``` OCL 
ownedUsage->selectByKind(ConstraintUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ConstraintUsage>()
```
# CheckOccurrenceDefinitionMultiplicitySpecialization
### OCL
``` OCL 
isIndividual implies
    multiplicity <> null and
    multiplicity.specializesFromLibrary('Base::zeroOrOne')
```
### C#
``` CSharp 
(!(isIndividual) || multiplicity != null) && multiplicity.specializesFromLibrary("Base::zeroOrOne")
```
# ValidateSpecificationSpecificNotConjugated
### OCL
``` OCL 
not specific.isConjugated
```
### C#
``` CSharp 
!specific.isConjugated
```
# DeriveInstantiationExpressionInstantiatedType
### OCL
``` OCL 
instantiatedType()
```
### C#
``` CSharp 
instantiatedType()
```
# ValidateConnectorBinarySpecialization
### OCL
``` OCL 
connectorEnds->size() > 2 implies
    not specializesFromLibrary('Links::BinaryLink')
```
### C#
``` CSharp 
(!(connectorEnds.Count() > 2) || !specializesFromLibrary("Links::BinaryLink"))
```
# DeriveElementOwnedElement
### OCL
``` OCL 
ownedRelationship.ownedRelatedElement
```
### C#
``` CSharp 
ownedRelationship.ownedRelatedElement
```
# CheckTransitionUsageStateSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(StateDefinition) or
 owningType.oclIsKindOf(StateUsage)) and
source <> null and source.oclIsKindOf(StateUsage) implies
    specializesFromLibrary('States::StateAction::stateTransitions')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is StateDefinition) || (owningType is StateUsage)) && source != null &&
(source is StateUsage)) || specializesFromLibrary("States::StateAction::stateTransitions"))
```
# DeriveRequirementDefinitionFramedConcern
### OCL
``` OCL 
featureMembership->
    selectByKind(FramedConcernMembership).
    ownedConcern
```
### C#
``` CSharp 
featureMembership.OfType<FramedConcernMembership>().Select(item => item.ownedConcern)
```
# CheckUsageVariationUsageSpecialization
### OCL
``` OCL 
owningVariationUsage <> null implies
    specializes(owningVariationUsage)
```
### C#
``` CSharp 
(!(owningVariationUsage != null) || specializes(owningVariationUsage))
```
# DeriveFeatureChainExpressionTargetFeature
### OCL
``` OCL 
    let nonParameterMemberships : Sequence(Membership) = ownedMembership->
        reject(oclIsKindOf(ParameterMembership)) in
    if nonParameterMemberships->isEmpty() or
       not nonParameterMemberships->first().memberElement.oclIsKindOf(Feature)
    then null
    else nonParameterMemberships->first().memberElement.oclAsType(Feature)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.Where(item => !(item is ParameterMembership)).Select(nonParameterMemberships => (!nonParameterMemberships.Any() ||
!(nonParameterMemberships.First().memberElement is Feature) ? null :
((Feature)nonParameterMemberships.First().memberElement)))
```
# DeriveUsageNestedAllocation
### OCL
``` OCL 
nestedUsage->selectByKind(AllocationUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<AllocationUsage>()
```
# ValidateStateDefinitionParallelSubactions
### OCL
``` OCL 
isParallel implies
    ownedAction.incomingTransition->isEmpty() and
    ownedAction.outgoingTransition->isEmpty()
```
### C#
``` CSharp 
(!(isParallel) || !ownedAction.incomingTransition.Any()) && !ownedAction.outgoingTransition.Any()
```
# ValidateInvocationExpressionOwnedFeatures
### OCL
``` OCL 
ownedFeature->forAll(f |
    f <> result implies 
        f.direction = FeatureDirectionKind::_'in')
```
### C#
``` CSharp 
ownedFeature.All(f => (!(f != result) || f.direction == FeatureDirectionKind.in))
```
# DeriveAnnotationAnnotatingElement
### OCL
``` OCL 
    if ownedAnnotatingElement <> null then ownedAnnotatingElement
    else owningAnnotatingElement
    endif
```
### C#
``` CSharp 
(ownedAnnotatingElement != null ? ownedAnnotatingElement : owningAnnotatingElement)
```
# DeriveStepBehavior
### OCL
``` OCL 
type->selectByKind(Behavior)
```
### C#
``` CSharp 
type.OfType<Behavior>()
```
# ValidateSatisfyRequirementUsageReference
### OCL
``` OCL 
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(RequirementUsage)
```
### C#
``` CSharp 
(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is RequirementUsage))
```
# ValidateTypeOwnedIntersectingNotOne
### OCL
``` OCL 
ownedIntersecting->size() <> 1
```
### C#
``` CSharp 
ownedIntersecting.Count() != 1
```
# DeriveTypeInheritedFeature
### OCL
``` OCL 
inheritedMemberships->
    selectByKind(FeatureMembership).memberFeature
```
### C#
``` CSharp 
inheritedMemberships.OfType<FeatureMembership>().Select(item => item.memberFeature)
```
# DeriveViewUsageExposedElement
### OCL
``` OCL 
ownedImport->selectByKind(Expose).
    importedMemberships(Set{}).memberElement->
    select(elm | includeAsExposed(elm))->
    asOrderedSet()
```
### C#
``` CSharp 
ownedImport.OfType<Expose>().importedMemberships(new HashSet<dynamic> {  }).memberElement.Where(elm => includeAsExposed(elm)).Distinct().ToHashSet()
```
# DeriveViewDefinitionView
### OCL
``` OCL 
usage->selectByKind(ViewUsage)
```
### C#
``` CSharp 
usage.OfType<ViewUsage>()
```
# CheckAnalysisCaseUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('AnalysisCases::analysisCases')
```
### C#
``` CSharp 
specializesFromLibrary("AnalysisCases::analysisCases")
```
# CheckVerificationCaseUsageSubVerificationCaseSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
    (owningType.oclIsKindOf(VerificationCaseDefinition) or
     owningType.oclIsKindOf(VerificationCaseUsage)) implies 
    specializesFromLibrary('VerificationCases::VerificationCase::subVerificationCases')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is VerificationCaseDefinition) || (owningType is VerificationCaseUsage))) || specializesFromLibrary("VerificationCases::VerificationCase::subVerificationCases"))
```
# DeriveNamespaceOwnedMembership
### OCL
``` OCL 
ownedRelationship->selectByKind(Membership)
```
### C#
``` CSharp 
ownedRelationship.OfType<Membership>()
```
# CheckTransitionUsagePayloadSpecialization
### OCL
``` OCL 
triggerAction->notEmpty() implies
    let payloadParameter : Feature = inputParameter(2) in
    payloadParameter <> null and
    payloadParameter.subsetsChain(triggerAction->at(1), triggerPayloadParameter())
```
### C#
``` CSharp 
(!(triggerAction.Any()) || inputParameter(2).Select(payloadParameter => payloadParameter != null && payloadParameter.subsetsChain(triggerAction->ElementAt(0), triggerPayloadParameter())))
```
# ValidateFeatureValueOverriding
### OCL
``` OCL 
featureWithValue.redefinition.redefinedFeature->
    closure(redefinition.redefinedFeature).valuation->
    forAll(isDefault)
```
### C#
``` CSharp 
// TODO
featureWithValue.redefinition.redefinedFeature.Any(item => item.valuation.All(item => item.isDefault))
```
# ValidateFlowEndOwningType
### OCL
``` OCL 
owningType <> null and owningType.oclIsKindOf(Flow)
```
### C#
``` CSharp 
owningType != null && (owningType is Flow)
```
# CheckPayloadFeatureRedefinition
### OCL
``` OCL 
redefinesFromLibrary('Transfers::Transfer::payload')
```
### C#
``` CSharp 
redefinesFromLibrary("Transfers::Transfer::payload")
```
# DeriveNamespaceImportImportedElement
### OCL
``` OCL 
importedNamespace
```
### C#
``` CSharp 
importedNamespace
```
# DeriveElementName
### OCL
``` OCL 
effectiveName()
```
### C#
``` CSharp 
effectiveName()
```
# ValidateUsageVariationOwnedFeatureMembership
### OCL
``` OCL 
isVariation implies ownedFeatureMembership->isEmpty()
```
### C#
``` CSharp 
(!(isVariation) || !ownedFeatureMembership.Any())
```
# ValidateSubsettingUniquenessConformance
### OCL
``` OCL 
subsettedFeature.isUnique implies subsettingFeature.isUnique
```
### C#
``` CSharp 
(!(subsettedFeature.isUnique) || subsettingFeature.isUnique)
```
# DeriveUsageNestedVerificationCase
### OCL
``` OCL 
nestedUsage->selectByKind(VerificationCaseUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<VerificationCaseUsage>()
```
# CheckRequirementUsageRequirementVerificationSpecialization
### OCL
``` OCL 
owningFeatureMembership <> null and
owningFeatureMembership.oclIsKindOf(RequirementVerificationMembership) implies
    specializesFromLibrary('VerificationCases::VerificationCase::obj::requirementVerifications')
```
### C#
``` CSharp 
(!(owningFeatureMembership != null && (owningFeatureMembership is RequirementVerificationMembership)) || specializesFromLibrary("VerificationCases::VerificationCase::obj::requirementVerifications"))
```
# DeriveAnnotationOwnedAnnotatingElement
### OCL
``` OCL 
    let ownedAnnotatingElements : Sequence(AnnotatingElement) = 
        ownedRelatedElement->selectByKind(AnnotatingElement) in
    if ownedAnnotatingElements->isEmpty() then null
    else ownedAnnotatingElements->first()
    endif
```
### C#
``` CSharp 
ownedRelatedElement.OfType<AnnotatingElement>().Select(ownedAnnotatingElements => (!ownedAnnotatingElements.Any() ? null : ownedAnnotatingElements.First()))
```
# DeriveAllocationDefinitionAllocation
### OCL
``` OCL 
usage->selectAsKind(AllocationUsage)
```
### C#
``` CSharp 
usage.OfType<AllocationUsage>().Select(item => item.selectAsKind)
```
# ValidateConnectorRelatedFeatures
### OCL
``` OCL 
not isAbstract implies relatedFeature->size() >= 2
```
### C#
``` CSharp 
(!(!isAbstract) || relatedFeature.Count() >= 2)
```
# DeriveUsageMayTimeVary
### OCL
``` OCL 
    owningType <> null and
    owningType.specializesFromLibrary('Occurrences::Occurrence') and
    not (
        isPortion or
        specializesFromLibrary('Links::SelfLink') or
        specializesFromLibrary('Occurrences::HappensLink') or
        isComposite and specializesFromLibrary('Actions::Action')
    )
```
### C#
``` CSharp 
owningType != null && owningType.specializesFromLibrary("Occurrences::Occurrence") && !(isPortion || specializesFromLibrary("Links::SelfLink") || specializesFromLibrary("Occurrences::HappensLink") || isComposite && specializesFromLibrary("Actions::Action"))
```
# ValidateFeatureOwnedCrossSubsetting
### OCL
``` OCL 
ownedSubsetting->selectByKind(CrossSubsetting)->size() <= 1
```
### C#
``` CSharp 
ownedSubsetting.OfType<CrossSubsetting>().Count() <= 1
```
# CheckAllocationUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Allocations::allocations')
```
### C#
``` CSharp 
specializesFromLibrary("Allocations::allocations")
```
# DeriveOwningNamespace
### OCL
``` OCL 
    if owningMembership = null then null
    else owningMembership.membershipOwningNamespace
    endif
```
### C#
``` CSharp 
(owningMembership == null ? null : owningMembership.membershipOwningNamespace)
```
# DeriveUsageNestedPart
### OCL
``` OCL 
nestedUsage->selectByKind(PartUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<PartUsage>()
```
# DeriveNamespaceMembers
### OCL
``` OCL 
membership.memberElement
```
### C#
``` CSharp 
membership.memberElement
```
# CheckExhibitStateUsageSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(PartDefinition) or
 owningType.oclIsKindOf(PartUsage)) implies
    specializesFromLibrary('Parts::Part::exhibitedStates')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is PartDefinition) || (owningType is PartUsage))) || specializesFromLibrary("Parts::Part::exhibitedStates"))
```
# DeriveTypeOwnedConjugator
### OCL
``` OCL 
    let ownedConjugators: Sequence(Conjugator) = 
        ownedRelationship->selectByKind(Conjugation) in
    if ownedConjugators->isEmpty() then null 
    else ownedConjugators->at(1) endif
```
### C#
``` CSharp 
ownedRelationship.OfType<Conjugation>().Select(ownedConjugators => (!ownedConjugators.Any() ? null :
ownedConjugators.ElementAt(0)))
```
# ValidateActorMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(RequirementUsage) or
owningType.oclIsKindOf(RequirementDefinition) or
owningType.oclIsKindOf(CaseDefinition) or
owningType.oclIsKindOf(CaseUsage)
```
### C#
``` CSharp 
(owningType is RequirementUsage) || (owningType is RequirementDefinition) || (owningType is CaseDefinition) || (owningType is CaseUsage)
```
# DeriveVerificationCaseUsageVerifiedRequirement
### OCL
``` OCL 
    if objectiveRequirement = null then OrderedSet{}
    else 
        objectiveRequirement.featureMembership->
            selectByKind(RequirementVerificationMembership).
            verifiedRequirement->asOrderedSet()
    endif
```
### C#
``` CSharp 
(objectiveRequirement == null ? new List<dynamic> {  } : objectiveRequirement.featureMembership.OfType<RequirementVerificationMembership>().Select(item => item.verifiedRequirement).Distinct().ToHashSet())
```
# CheckConcernUsageFramedConcernSpecialization
### OCL
``` OCL 
owningFeatureMembership <> null and
owningFeatureMembership.oclIsKindOf(FramedConcernMembership) implies
    specializesFromLibrary('Requirements::RequirementCheck::concerns')
```
### C#
``` CSharp 
(!(owningFeatureMembership != null && (owningFeatureMembership is FramedConcernMembership)) || specializesFromLibrary("Requirements::RequirementCheck::concerns"))
```
# ValidateBehaviorSpecialization
### OCL
``` OCL 
ownedSpecialization.general->forAll(not oclIsKindOf(Structure))
```
### C#
``` CSharp 
ownedSpecialization.general.All(item => !(item is Structure))
```
# CheckUseCaseUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('UseCases::useCases')
```
### C#
``` CSharp 
specializesFromLibrary("UseCases::useCases")
```
# CheckFlowSpecialization
### OCL
``` OCL 
specializesFromLibrary('Transfers::transfers')
```
### C#
``` CSharp 
specializesFromLibrary("Transfers::transfers")
```
# DeriveViewUsageViewCondition
### OCL
``` OCL 
ownedMembership->
    selectByKind(ElementFilterMembership).
    condition
```
### C#
``` CSharp 
ownedMembership.OfType<ElementFilterMembership>().Select(item => item.condition)
```
# DeriveOwningMembershipOwnedMemberName
### OCL
``` OCL 
ownedMemberElement.name
```
### C#
``` CSharp 
ownedMemberElement.name
```
# DeriveOwningMembershipOwnedMemberShortName
### OCL
``` OCL 
ownedMemberElement.shortName
```
### C#
``` CSharp 
ownedMemberElement.shortName
```
# CheckPartUsageStakeholderSpecialization
### OCL
``` OCL 
owningFeatureMembership <> null and
owningFeatureMembership.oclIsKindOf(StakeholderMembership) implies
    specializesFromLibrary('Requirements::RequirementCheck::stakeholders')
```
### C#
``` CSharp 
(!(owningFeatureMembership != null && (owningFeatureMembership is StakeholderMembership)) || specializesFromLibrary("Requirements::RequirementCheck::stakeholders"))
```
# CheckRenderingUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Views::renderings')
```
### C#
``` CSharp 
specializesFromLibrary("Views::renderings")
```
# CheckConnectorObjectSpecialization
### OCL
``` OCL 
association->exists(oclIsKindOf(AssociationStructure)) implies
    specializesFromLibrary('Objects::linkObjects')
```
### C#
``` CSharp 
(!(association.Any(item => (item is AssociationStructure))) || specializesFromLibrary("Objects::linkObjects"))
```
# DeriveVewRenderingMembershipReferencedRendering
### OCL
``` OCL 
    let referencedFeature : Feature = 
        ownedRendering.referencedFeatureTarget() in
    if referencedFeature = null then ownedRendering
    else if referencedFeature.oclIsKindOf(RenderingUsage) then
        refrencedFeature.oclAsType(RenderingUsage)
    else null
    endif endif
```
### C#
``` CSharp 
ownedRendering.referencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? ownedRendering :
((referencedFeature is RenderingUsage) ? ((RenderingUsage)refrencedFeature) : null)))
```
# ValidateFeatureOwnedReferenceSubsetting
### OCL
``` OCL 
ownedSubsetting->selectByKind(ReferenceSubsetting)->size() <= 1
```
### C#
``` CSharp 
ownedSubsetting.OfType<ReferenceSubsetting>().Count() <= 1
```
# ValidateFeatureCrossFeatureSpecialization
### OCL
``` OCL 
crossFeature <> null implies
    ownedRedefinition.redefinedFeature.crossFeature->
            forAll(f | f <> null implies crossFeature.specializes(f))
```
### C#
``` CSharp 
(!(crossFeature != null) || ownedRedefinition.redefinedFeature.crossFeature.All(f => (!(f != null) || crossFeature.specializes(f))))
```
# ValidateExpressionResultExpressionMembership
### OCL
``` OCL 
membership->selectByKind(ResultExpressionMembership)->size() <= 1
```
### C#
``` CSharp 
membership.OfType<ResultExpressionMembership>().Count() <= 1
```
# CheckItemDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Items::Item')
```
### C#
``` CSharp 
specializesFromLibrary("Items::Item")
```
# CheckForLoopActionUsageVarRedefinition
### OCL
``` OCL 
loopVariable <> null and
loopVariable.redefinesFromLibrary('Actions::ForLoopAction::var')
```
### C#
``` CSharp 
loopVariable != null && loopVariable.redefinesFromLibrary("Actions::ForLoopAction::var")
```
# DeriveMembershipMemberElementId
### OCL
``` OCL 
memberElement.elementId
```
### C#
``` CSharp 
memberElement.elementId
```
# ValidateAnnotationAnnotatedElementOwnership
### OCL
``` OCL 
(owningAnnotatedElement <> null) = (ownedAnnotatingElement <> null)
```
### C#
``` CSharp 
(owningAnnotatedElement != null) == (ownedAnnotatingElement != null)
```
# CheckAssignmentActionUsageAccessedFeatureRedefinition
### OCL
``` OCL 
let targetParameter : Feature = inputParameter(1) in
targetParameter <> null and
targetParameter.ownedFeature->notEmpty() and
targetParameter->first().ownedFeature->notEmpty() and
targetParameter->first().ownedFeature->first().
    redefines('AssigmentAction::target::startingAt::accessedFeature')
```
### C#
``` CSharp 
inputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.ownedFeature.Any() && targetParameter.First().ownedFeature.Any() && targetParameter.First().ownedFeature.First().redefines("AssigmentAction::target::startingAt::accessedFeature"))
```
# ValidateTransitionUsageSuccession
### OCL
``` OCL 
let successions : Sequence(Successions) = 
    ownedMember->selectByKind(Succession) in
successions->notEmpty() and
successions->at(1).targetFeature.featureTarget->
    forAll(oclIsKindOf(ActionUsage))
```
### C#
``` CSharp 
ownedMember.OfType<Succession>().Select(successions => successions.Any() && successions.ElementAt(0).targetFeature.featureTarget.All(item => (item is ActionUsage)))
```
# CheckRequirementUsageObjectiveRedefinition
### OCL
``` OCL 
owningfeatureMembership <> null and
owningfeatureMembership.oclIsKindOf(ObjectiveMembership) implies
    owningType.ownedSpecialization.general->forAll(gen |
        (gen.oclIsKindOf(CaseDefinition) implies
            redefines(gen.oclAsType(CaseDefinition).objectiveRequirement)) and
        (gen.oclIsKindOf(CaseUsage) implies
            redefines(gen.oclAsType(CaseUsage).objectiveRequirement))
```
### C#
``` CSharp 
(!(owningfeatureMembership != null && (owningfeatureMembership is ObjectiveMembership)) || owningType.ownedSpecialization.general.All(gen => ((!((gen is CaseDefinition)) || redefines(((CaseDefinition)gen).objectiveRequirement))) && ((!((gen is CaseUsage)) || redefines(((CaseUsage)gen).objectiveRequirement)))))
```
# CheckTransitionUsageSuccessionSourceSpecialization
### OCL
``` OCL 
succession.sourceFeature = source
```
### C#
``` CSharp 
succession.sourceFeature == source
```
# ValidatePerformActionUsageReference
### OCL
``` OCL 
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(ActionUsage)
```
### C#
``` CSharp 
(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is ActionUsage))
```
# CheckDecisionNodeSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::Action::decisions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::Action::decisions")
```
# DeriveConnectorRelatedFeature
### OCL
``` OCL 
connectorEnd.ownedReferenceSubsetting->
    select(s | s <> null).subsettedFeature
```
### C#
``` CSharp 
connectorEnd.ownedReferenceSubsetting.Where(s => s != null).subsettedFeature
```
# DeriveConnectorSourceFeature
### OCL
``` OCL 
    if relatedFeature->isEmpty() then null 
    else relatedFeature->first() 
    endif
```
### C#
``` CSharp 
(!relatedFeature.Any() ? null : relatedFeature.First())
```
# ValidateRequirementDefinitionSubjectParameterPosition
### OCL
``` OCL 
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp 
input.Any() && input.First() == subjectParameter
```
# CheckFeatureOwnedCrossFeatureSpecialization
### OCL
``` OCL 
isOwnedCrossFeature() implies
    owner.oclAsType(Feature).type->forAll(t | self.specializes(t))
```
### C#
``` CSharp 
(!(isOwnedCrossFeature()) || ((Feature)owner).type.All(t => this.specializes(t)))
```
# ValidateEndFeatureMembershipIsEnd
### OCL
``` OCL 
ownedMemberFeature.isEnd
```
### C#
``` CSharp 
ownedMemberFeature.isEnd
```
# CheckSelectExpressionResultSpecialization
### OCL
``` OCL 
arguments->notEmpty() implies
    result.specializes(arguments->first().result)
```
### C#
``` CSharp 
(!(arguments.Any()) || result.specializes(arguments.First().result))
```
# ValidateFlowEndNestedFeature
### OCL
``` OCL 
ownedFeature->size() = 1
```
### C#
``` CSharp 
ownedFeature.Count() == 1
```
# CheckCaseUsageSubcaseSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and 
    (owningType.oclIsKindOf(CaseDefinition) or
     owningType.oclIsKindOf(CaseUsage)) implies
    specializesFromLibrary('Cases::Case::subcases')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is CaseDefinition) || (owningType is CaseUsage))) || specializesFromLibrary("Cases::Case::subcases"))
```
# DeriveUsageNestedConcern
### OCL
``` OCL 
nestedUsage->selectByKind(ConcernUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ConcernUsage>()
```
# DeriveActionDefinitionAction
### OCL
``` OCL 
usage->selectByKind(ActionUsage)
```
### C#
``` CSharp 
usage.OfType<ActionUsage>()
```
# CheckVerificationCaseSpecialization
### OCL
``` OCL 
specializesFromLibrary('VerificationCases::VerificationCase')
```
### C#
``` CSharp 
specializesFromLibrary("VerificationCases::VerificationCase")
```
# DeriveDefinitionOwnedFlow
### OCL
``` OCL 
ownedUsage->selectByKind(FlowConnectionUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<FlowConnectionUsage>()
```
# CheckTransitionUsageActionSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(ActionDefinition) or
 owningType.oclIsKindOf(ActionUsage)) and
source <> null and not source.oclIsKindOf(StateUsage) implies
    specializesFromLibrary('Actions::Action::decisionTransitions')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is ActionDefinition) || (owningType is ActionUsage)) && source != null && !(source is StateUsage)) || specializesFromLibrary("Actions::Action::decisionTransitions"))
```
# DeriveUsageNestedOccurrence
### OCL
``` OCL 
nestedUsage->selectByKind(OccurrenceUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<OccurrenceUsage>()
```
# ValidateRequirementVerificationMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(RequirementUsage) and
owningType.owningFeatureMembership <> null and
owningType.owningFeatureMembership.oclIsKindOf(ObjectiveMembership)
```
### C#
``` CSharp 
(owningType is RequirementUsage) && owningType.owningFeatureMembership != null && (owningType.owningFeatureMembership is ObjectiveMembership)
```
# DeriveCaseUsageSubjectParameter
### OCL
``` OCL 
    let subjects : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjects->isEmpty() then null
    else subjects->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
featureMembership.OfType<SubjectMembership>().Select(subjects => (!subjects.Any() ? null :
subjects.First().ownedSubjectParameter))
```
# DeriveFeatureOwnedRedefinition
### OCL
``` OCL 
ownedSubsetting->selectByKind(Redefinition)
```
### C#
``` CSharp 
ownedSubsetting.OfType<Redefinition>()
```
# CheckAcceptActionUsageSpecialization
### OCL
``` OCL 
not isTriggerAction() implies
    specializesFromLibrary('Actions::acceptActions')
```
### C#
``` CSharp 
(!(!isTriggerAction()) || specializesFromLibrary("Actions::acceptActions"))
```
# CheckOccurrenceUsageSnapshotSpecialization
### OCL
``` OCL 
portionKind = PortionKind::snapshot implies
    specializesFromLibrary('Occurrences::Occurrence::snapshots')
```
### C#
``` CSharp 
(!(portionKind == PortionKind.snapshot) || specializesFromLibrary("Occurrences::Occurrence::snapshots"))
```
# DeriveDefinitionOwnedUsage
### OCL
``` OCL 
ownedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
ownedFeature.OfType<Usage>()
```
# DeriveUsageNestedConstraint
### OCL
``` OCL 
nestedUsage->selectByKind(ConstraintUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ConstraintUsage>()
```
# DeriveRequirementDefinitionRequiredConstraint
### OCL
``` OCL 
ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::requirement).
    ownedConstraint
```
### C#
``` CSharp 
ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => kind == RequirementConstraintKind.requirement).ownedConstraint
```
# DeriveUsageNestedFlow
### OCL
``` OCL 
nestedUsage->selectByKind(FlowConnectionUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<FlowConnectionUsage>()
```
# DeriveBehaviorStep
### OCL
``` OCL 
feature->selectByKind(Step)
```
### C#
``` CSharp 
feature.OfType<Step>()
```
# DeriveTypeDirectedFeature
### OCL
``` OCL 
feature->select(f | directionOf(f) <> null)
```
### C#
``` CSharp 
feature.Where(f => directionOf(f) != null)
```
# DeriveUsageNestedState
### OCL
``` OCL 
nestedUsage->selectByKind(StateUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<StateUsage>()
```
# ValidateTransitionFeatureMembershipGuardExpression
### OCL
``` OCL 
kind = TransitionFeatureKind::guard implies
    transitionFeature.oclIsKindOf(Expression) and
    let guard : Expression = transitionFeature.oclIsKindOf(Expression) in
    guard.result.specializesFromLibrary('ScalarValues::Boolean') and
    guard.result.multiplicity <> null and
    guard.result.multiplicity.hasBounds(1,1)
```
### C#
``` CSharp 
(!(kind == TransitionFeatureKind.guard) || (transitionFeature is Expression)) && guard is Expression == (transitionFeature is Expression)
```
# DeriveFeatureOwnedReferenceSubsetting
### OCL
``` OCL 
    let referenceSubsettings : OrderedSet(ReferenceSubsetting) =
        ownedSubsetting->selectByKind(ReferenceSubsetting) in
    if referenceSubsettings->isEmpty() then null
    else referenceSubsettings->first() endif
```
### C#
``` CSharp 
ownedSubsetting.OfType<ReferenceSubsetting>().Select(referenceSubsettings => (!referenceSubsettings.Any() ? null :
referenceSubsettings.First()))
```
# ValidateExhibitStateUsageReference
### OCL
``` OCL 
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(StateUsage)
```
### C#
``` CSharp 
(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is StateUsage))
```
# ValidateInvocationExpressionInstantiatedType
### OCL
``` OCL 
instantiatedType.oclIsKindOf(Behavior) or
instantiatedType.oclIsKindOf(Feature) and
    instantiatedType.type->exists(oclIsKindOf(Behavior)) and
    instantiatedType.type->size(1)
```
### C#
``` CSharp 
(instantiatedType is Behavior) || (instantiatedType is Feature) && instantiatedType.type.Any(item => (item is Behavior)) && instantiatedType.type.Count()
```
# ValidateFeatureChainingFeatureNotOne
### OCL
``` OCL 
chainingFeature->size() <> 1
```
### C#
``` CSharp 
chainingFeature.Count() != 1
```
# DeriveRequirementDefinitionAssumedConstraint
### OCL
``` OCL 
ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::assumption).
    ownedConstraint
```
### C#
``` CSharp 
ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => item.kind == RequirementConstraintKind.assumption).ownedConstraint
```
# ValidateControlNodeIncomingSuccessions
### OCL
``` OCL 
targetConnector->selectByKind(Succession)->
    collect(connectorEnd->at(2).multiplicity)->
    forAll(targetMult | 
        multiplicityHasBounds(targetMult, 1, 1))
```
### C#
``` CSharp 
targetConnector.OfType<Succession>().Select(item => connectorEnd.ElementAt(1)).All(targetMult => multiplicityHasBounds(targetMult, 1, 1))
```
# DeriveDefinitionVariantMembership
### OCL
``` OCL 
ownedMembership->selectByKind(VariantMembership)
```
### C#
``` CSharp 
ownedMembership.OfType<VariantMembership>()
```
# CheckAssertConstraintUsageSpecialization
### OCL
``` OCL 
if isNegated then
    specializesFromLibrary('Constraints::negatedConstraintChecks')
else
    specializesFromLibrary('Constraints::assertedConstraintChecks')
endif
```
### C#
``` CSharp 
(isNegated ? specializesFromLibrary("Constraints::negatedConstraintChecks") : specializesFromLibrary("Constraints::assertedConstraintChecks"))
```
# CheckItemUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Items::items')
```
### C#
``` CSharp 
specializesFromLibrary("Items::items")
```
# DeriveFeatureCrossFeature
### OCL
``` OCL 
    if ownedCrossSubsetting = null then null
    else 
        let chainingFeatures: Sequence(Feature) = 
            ownedCrossSubsetting.crossedFeature.chainingFeature in
        if chainingFeatures->size() < 2 then null
        else chainingFeatures->at(2)
    endif
```
### C#
``` CSharp 
(ownedCrossSubsetting == null ? null : (ownedCrossSubsetting.crossedFeature.chainingFeature).Select(chainingFeatures =>
(chainingFeatures.Count() < 2 ? null : chainingFeatures.ElementAt(1))))
```
# ValidateTypeOwnedUnioningNotOne
### OCL
``` OCL 
ownedUnioning->size() <> 1
```
### C#
``` CSharp 
ownedUnioning.Count() != 1
```
# CheckFeatureCrossingSpecialization
### OCL
``` OCL 
ownedCrossFeature() <> null implies
    crossFeature = ownedCrossFeature()
```
### C#
``` CSharp 
(!(ownedCrossFeature() != null) || crossFeature == ownedCrossFeature())
```
# ValidateControlNodeOwningType
### OCL
``` OCL 
owningType <> null and 
(owningType.oclIsKindOf(ActionDefinition) or
 owningType.oclIsKindOf(ActionUsage))
```
### C#
``` CSharp 
owningType != null && ((owningType is ActionDefinition) || (owningType is ActionUsage))
```
# ValidateAssignmentActionUsageReferent
### OCL
``` OCL 
ownedMembership->exists(
    not oclIsKindOf(OwningMembership) and 
    memberElement.oclIsKindOf(Feature))
```
### C#
``` CSharp 
ownedMembership.Any(item => ! (item.OwningMembership) && (memberElement is Feature))
```
# CheckUseCaseDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('UseCases::UseCase')
```
### C#
``` CSharp 
specializesFromLibrary("UseCases::UseCase")
```
# DeriveFeatureFeaturingType
### OCL
``` OCL 
    let featuringTypes : OrderedSet(Type) = 
        featuring.type->asOrderedSet() in
    if chainingFeature->isEmpty() then featuringTypes
    else
        featuringTypes->
            union(chainingFeature->first().featuringType)->
            asOrderedSet()
    endif
```
### C#
``` CSharp 
featuring.type.Distinct().ToHashSet().Select(featuringTypes => (!chainingFeature.Any() ? featuringTypes :
featuringTypes.Union(chainingFeature.First().featuringType).Distinct().ToHashSet()))
```
# CheckLiteralIntegerSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::literalIntegerEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::literalIntegerEvaluations")
```
# DeriveCaseDefinitionActorParameter
### OCL
``` OCL 
featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
featureMembership.OfType<ActorMembership>()
```
# ValidateAssignmentActionUsage
### OCL
``` OCL 
referent <> null implies referent.featureTarget.mayTimeVary
```
### C#
``` CSharp 
(!(referent != null) || referent.featureTarget.mayTimeVary)
```
# CheckPortUsageSubportSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(PortDefinition) or
 owningType.oclIsKindOf(PortUsage)) implies
    specializesFromLibrary('Ports::Port::subports')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is PortDefinition) || (owningType is PortUsage))) || specializesFromLibrary("Ports::Port::subports"))
```
# CheckSuccessionFlowUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Flows::successionFlows')
```
### C#
``` CSharp 
specializesFromLibrary("Flows::successionFlows")
```
# CheckRequirementUsageSubrequirementSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
    (owningType.oclIsKindOf(RequirementDefinition) or
     owningType.oclIsKindOf(RequirementUsage)) implies
    specializesFromLibrary('Requirements::RequirementCheck::subrequirements')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is RequirementDefinition) || (owningType is RequirementUsage))) || specializesFromLibrary("Requirements::RequirementCheck::subrequirements"))
```
# DeriveUsageVariantMembership
### OCL
``` OCL 
ownedMembership->selectByKind(VariantMembership)
```
### C#
``` CSharp 
ownedMembership.OfType<VariantMembership>()
```
# CheckTerminateActionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::terminateSubactions')
```
### C#
``` CSharp 
(!(isSubactionUsage()) || specializesFromLibrary("Actions::Action::terminateSubactions"))
```
# DeriveFunctionResult
### OCL
``` OCL 
    let resultParams : Sequence(Feature) =
        featureMemberships->
            selectByKind(ReturnParameterMembership).
            ownedMemberParameter in
    if resultParams->notEmpty() then resultParams->first()
    else null
    endif
```
### C#
``` CSharp 
featureMemberships.OfType<ReturnParameterMembership>().Select(resultParams => (resultParams.Any() ? resultParams.First() :
null))
```
# CheckBooleanExpressionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::booleanEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::booleanEvaluations")
```
# ValidateConjugatedPortDefinitionConjugatedPortDefinitionIsEmpty
### OCL
``` OCL 
null
```
### C#
``` CSharp 
null
```
# DeriveUsageNestedUsage
### OCL
``` OCL 
ownedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
ownedFeature.OfType<Usage>()
```
# CheckFeatureReferenceExpressionBindingConnector
### OCL
``` OCL 
ownedMember->selectByKind(BindingConnector)->exists(b |
    b.relatedFeatures->includes(targetFeature) and
    b.relatedFeatures->includes(result))
```
### C#
``` CSharp 
ownedMember.OfType<BindingConnector>().Any(b => b.relatedFeatures.Contains(targetFeature) && b.relatedFeatures.Contains(result))
```
# CheckViewpointUsageViewpointSatisTestionSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(ViewDefinition) or
 owningType.oclIsKindOf(ViewUsage)) implies
    specializesFromLibrary('Views::View::viewpointSatisTestions')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is ViewDefinition) || (owningType is ViewUsage))) || specializesFromLibrary("Views::View::viewpointSatisTestions"))
```
# CheckITestionUsageSpecialization
### OCL
``` OCL 
if elseAction = null then
    specializesFromLibrary('Actions::ifThenActions')
else
    specializesFromLibrary('Actions::ifThenElseActions')
endif
```
### C#
``` CSharp 
(elseAction == null ? specializesFromLibrary("Actions::ifThenActions") : specializesFromLibrary("Actions::ifThenElseActions"))
```
# CheckFeatureSubobjectSpecialization
### OCL
``` OCL 
isComposite and
ownedTyping.type->includes(oclIsKindOf(Structure)) and
owningType <> null and
(owningType.oclIsKindOf(Structure) or
 owningType.type->includes(oclIsKindOf(Structure))) implies
    specializesFromLibrary('Occurrence::Occurrence::suboccurrences')
```
### C#
``` CSharp 
(!(isComposite && ownedTyping.type.Contains((Structure)) && owningType != null && ((owningType is Structure) || owningType.type.Contains((Structure)))) || specializesFromLibrary("Occurrence::Occurrence::suboccurrences"))
```
# CheckMultiplicityRangeExpressionTypeFeaturing
### OCL
``` OCL 
bound->forAll(b | b.featuringType = self.featuringType)
```
### C#
``` CSharp 
bound.All(b => b.featuringType == this.featuringType)
```
# DeriveViewDefinitionViewRendering
### OCL
``` OCL 
    let renderings: OrderedSet(ViewRenderingMembership) =
        featureMembership->selectByKind(ViewRenderingMembership) in
    if renderings->isEmpty() then null
    else renderings->first().referencedRendering
    endif
```
### C#
``` CSharp 
featureMembership.OfType<ViewRenderingMembership>().Select(renderings => (!renderings.Any() ? null :
renderings.First().referencedRendering))
```
# DeriveTransitionUsageGuardExpression
### OCL
``` OCL 
ownedFeatureMembership->
    selectByKind(TransitionFeatureMembership)->
    select(kind = TransitionFeatureKind::trigger).transitionFeature->
    selectByKind(Expression)
```
### C#
``` CSharp 
ownedFeatureMembership.OfType<TransitionFeatureMembership>().Where(item => item.kind == TransitionFeatureKind.trigger).transitionFeature.OfType<Expression>()
```
# CheckSuccessionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Occurrences::happensBeforeLinks')
```
### C#
``` CSharp 
specializesFromLibrary("Occurrences::happensBeforeLinks")
```
# DeriveRenderingDefinitionRendering
### OCL
``` OCL 
usages->selectByKind(RenderingUsage)
```
### C#
``` CSharp 
usages.OfType<RenderingUsage>()
```
# CheckSatisfyRequirementUsageBindingConnector
### OCL
``` OCL 
ownedMember->selectByKind(BindingConnector)->
    select(b |
        b.relatedElement->includes(subjectParameter) and
        b.relatedElement->exists(r | r <> subjectParameter))->
    size() = 1
```
### C#
``` CSharp 
ownedMember.OfType<BindingConnector>().Where(b => b.relatedElement.Contains(subjectParameter) && b.relatedElement.Any(r => r != subjectParameter)).Count() == 1
```
# CheckNullExpressionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::nullEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::nullEvaluations")
```
# ValidateFeatureReferenceExpressionResult
### OCL
``` OCL 
result.owningType = self
```
### C#
``` CSharp 
result.owningType == this
```
# DeriveFeatureType
### OCL
``` OCL 
    let types : OrderedSet(Types) = OrderedSet{self}->
        -- Note: The closure operation automatically handles circular relationships.
        closure(typingFeatures()).typing.type->asOrderedSet() in
    types->reject(t1 | types->exist(t2 | t2 <> t1 and t2.specializes(t1)))
```
### C#
``` CSharp 
/* closure */ new List<dynamic> { this }.typing.type.Distinct().ToHashSet().Select(types => types.Where(t1 =>
!(types.exist(t2).exist != t1 && t2.specializes(t1))))
```
# CheckPartDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Parts::Part')
```
### C#
``` CSharp 
specializesFromLibrary("Parts::Part")
```
# ValidateAttributeUsageIsReference
### OCL
``` OCL 
isReference
```
### C#
``` CSharp 
isReference
```
# ValidateElementIsImpliedIncluded
### OCL
``` OCL 
ownedRelationship->exists(isImplied) implies isImpliedIncluded
```
### C#
``` CSharp 
(!(ownedRelationship.Any(item => item.isImplied)) || isImpliedIncluded)
```
# ValidateAssociationRelatedTypes
### OCL
``` OCL 
not isAbstract implies relatedType->size() >= 2
```
### C#
``` CSharp 
(!(!isAbstract) || relatedType.Count() >= 2)
```
# DeriveElementDocumentation
### OCL
``` OCL 
ownedElement->selectByKind(Documentation)
```
### C#
``` CSharp 
ownedElement.OfType<Documentation>()
```
# ValidateSubsettingFeaturingTypes
### OCL
``` OCL 
subsettingFeature.canAccess(subsettedFeature)
```
### C#
``` CSharp 
subsettingFeature.canAccess(subsettedFeature)
```
# ValidatePortUsageNestedUsagesNotComposite
### OCL
``` OCL 
nestedUsage->
    reject(oclIsKindOf(PortUsage))->
    forAll(not isComposite)
```
### C#
``` CSharp 
nestedUsage.Where(item => !((item.PortUsage))).All(item2 => !item2.isComposite)
```
# DeriveTypeOwnedUnioning
### OCL
``` OCL 
    ownedRelationship->selectByKind(Unioning)
```
### C#
``` CSharp 
ownedRelationship.OfType<Unioning>()
```
# DeriveRequirementUsageText
### OCL
``` OCL 
documentation.body
```
### C#
``` CSharp 
documentation.body
```
# ValidateBindingConnectorIsBinary
### OCL
``` OCL 
relatedFeature->size() = 2
```
### C#
``` CSharp 
relatedFeature.Count() == 2
```
# DeriveCaseUsageObjectiveRequirement
### OCL
``` OCL 
    let objectives: OrderedSet(RequirementUsage) = 
        featureMembership->
            selectByKind(ObjectiveMembership).
            ownedRequirement in
    if objectives->isEmpty() then null
    else objectives->first().ownedObjectiveRequirement
    endif
```
### C#
``` CSharp 
featureMembership.OfType<ObjectiveMembership>().Select(objectives => (!objectives.Any() ? null :
objectives.First().ownedObjectiveRequirement))
```
# DeriveRequirementDefinitionActorParameter
### OCL
``` OCL 
featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
featureMembership.OfType<ActorMembership>()
```
# DeriveConstructorExpressionArgument
### OCL
``` OCL 
instantiatedType.feature->collect(f | 
    result.ownedFeatures->select(redefines(f)).valuation->
    select(v | v <> null).value
)
```
### C#
``` CSharp 
instantiatedType.feature.Select(f => result.ownedFeatures.Where(item => redefines(f)).valuation.Where(v => v != null).value)
```
# ValidateTypeOwnedMultiplicity
### OCL
``` OCL 
ownedMember->selectByKind(Multiplicity)->size() <= 1
```
### C#
``` CSharp 
ownedMember.OfType<Multiplicity>().Count() <= 1
```
# CheckPortUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Ports::ports')
```
### C#
``` CSharp 
specializesFromLibrary("Ports::ports")
```
# DeriveITestionUsageIfArgument
### OCL
``` OCL 
    let parameter : Feature = inputParameter(1) in
    if parameter <> null and parameter.oclIsKindOf(Expression) then
        parameter.oclAsType(Expression)
    else
        null
    endif
```
### C#
``` CSharp 
inputParameter(1).Select(parameter => (parameter != null && (parameter is Expression) ? ((Expression)parameter) : null))
```
# ValidateIncludeUseCaseUsageReference
### OCL
``` OCL 
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(UseCaseUsage)
```
### C#
``` CSharp 
(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is UseCaseUsage))
```
# DeriveAssertConstraintUsageAssertedConstraint
### OCL
``` OCL 
    if referencedFeatureTarget() = null then self
    else if referencedFeatureTarget().oclIsKindOf(ConstraintUsage) then
        referencedFeatureTarget().oclAsType(ConstraintUsage)
    else null
    endif endif
```
### C#
``` CSharp 
(referencedFeatureTarget() == null ? this : ((referencedFeatureTarget() is ConstraintUsage) ?
((ConstraintUsage)referencedFeatureTarget()) : null))
```
# DeriveTypeInput
### OCL
``` OCL 
feature->select(f | 
    let direction: FeatureDirectionKind = directionOf(f) in
    direction = FeatureDirectionKind::_'in' or
    direction = FeatureDirectionKind::inout)
```
### C#
``` CSharp 
feature.Where(f => directionOf(f).Select(direction => direction == FeatureDirectionKind._)) || direction == FeatureDirectionKind.inout
```
# DeriveViewUsageSatisfiedViewpoint
### OCL
``` OCL 
ownedRequirement->
    selectByKind(ViewpointUsage)->
    select(isComposite)
```
### C#
``` CSharp 
ownedRequirement.OfType<ViewpointUsage>().Where(item => item.isComposite)
```
# CheckAcceptActionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() and not isTriggerAction() implies
    specializesFromLibrary('Actions::Action::acceptSubactions')
```
### C#
``` CSharp 
(!(isSubactionUsage() && !isTriggerAction()) || specializesFromLibrary("Actions::Action::acceptSubactions"))
```
# CheckJoinNodeSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::Action::join')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::Action::join")
```
# CheckAnalysisCaseUsageSubAnalysisCaseSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
    (owningType.oclIsKindOf(AnalysisCaseDefinition) or
     owningType.oclIsKindOf(AnalysisCaseUsage)) implies
    specializesFromLibrary('AnalysisCases::AnalysisCase::subAnalysisCases')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is AnalysisCaseDefinition) || (owningType is AnalysisCaseUsage))) || specializesFromLibrary("AnalysisCases::AnalysisCase::subAnalysisCases"))
```
# ValidateFeatureReferenceExpressionReferentIsFeature
### OCL
``` OCL 
let membership : Membership = 
    ownedMembership->reject(m | m.oclIsKindOf(ParameterMembership)) in
membership->notEmpty() and
membership->at(1).memberElement.oclIsKindOf(Feature)
```
### C#
``` CSharp 
ownedMembership.Where(m => !((m is ParameterMembership))).Select(membership => membership.Any() && (membership.ElementAt(0) is Feature))
```
# CheckFeatureChainExpressionResultSpecialization
### OCL
``` OCL 
let inputParameters : Sequence(Feature) = 
    ownedFeatures->select(direction = _'in') in
let sourceTargetFeature : Feature = 
    owningExpression.sourceTargetFeature() in
sourceTargetFeature <> null and
result.subsetsChain(inputParameters->first(), sourceTargetFeature) and
result.owningType = self
```
### C#
``` CSharp 
ownedFeatures.Where(item => item.direction == _).Select(inputParameters => owningExpression.sourceTargetFeature().Select(sourceTargetFeature => sourceTargetFeature != null && result.subsetsChain(inputParameters.First(), sourceTargetFeature) && result.owningType == this))
```
# DeriveUsageNestedTransition
### OCL
``` OCL 
nestedUsage->selectByKind(TransitionUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<TransitionUsage>()
```
# DeriveTypeFeature
### OCL
``` OCL 
featureMembership.ownedMemberFeature
```
### C#
``` CSharp 
featureMembership.ownedMemberFeature
```
# ValidateControlNodeOutgoingSuccessions
### OCL
``` OCL 
sourceConnector->selectByKind(Succession)->
    collect(connectorEnd->at(1).multiplicity)->
    forAll(sourceMult | 
        multiplicityHasBounds(sourceMult, 1, 1))
```
### C#
``` CSharp 
// TODO
sourceConnector.OfType<Succession>().Select(item => item.connectorEnd.ElementAt(0)).All(sourceMult => multiplicityHasBounds(sourceMult, 1, 1))
```
# ValidateObjectiveMembershipOwningType
### OCL
``` OCL 
owningType.oclIsType(CaseDefinition) or
owningType.oclIsType(CaseUsage)
```
### C#
``` CSharp 
(owningType is CaseDefinition) || (owningType is CaseUsage)
```
# CheckMultiplicitySpecialization
### OCL
``` OCL 
specializesFromLibrary('Base::naturals')
```
### C#
``` CSharp 
specializesFromLibrary("Base::naturals")
```
# ValidateImportTopLevelVisibility
### OCL
``` OCL 
importOwningNamespace.owner = null implies 
    visibility = VisibilityKind::private
```
### C#
``` CSharp 
(!(importOwningNamespace.owner == null) || visibility == VisibilityKind.private)
```
# CheckStateDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('States::StateAction')
```
### C#
``` CSharp 
specializesFromLibrary("States::StateAction")
```
# ValidateOccurrenceUsageIsPortion
### OCL
``` OCL 
portionKind <> null implies isPortion
```
### C#
``` CSharp 
(!(portionKind != null) || isPortion)
```
# CheckConstraintDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Constraints::ConstraintCheck')
```
### C#
``` CSharp 
specializesFromLibrary("Constraints::ConstraintCheck")
```
# ValidateStateUsageStateSubactionKind
### OCL
``` OCL 
ownedMembership->
    selectByKind(StateSubactionMembership)->
    isUnique(kind)
```
### C#
``` CSharp 
(ownedMembership.OfType<StateSubactionMembership>().Select(item => item.kind).Distinct().Count() == ownedMembership.OfType<StateSubactionMembership>().Count())
```
# ValidateFeatureCrossFeatureType
### OCL
``` OCL 
crossFeature <> null implies
    crossFeature.type->asSet() = type->asSet()
```
### C#
``` CSharp 
(!(crossFeature != null) || crossFeature.type.ToHashSet() == type.ToHashSet())
```
# DeriveAssociationRelatedType
### OCL
``` OCL 
associationEnd.type
```
### C#
``` CSharp 
associationEnd.type
```
# ValidateDefinitionVariationOwnedFeatureMembership
### OCL
``` OCL 
isVariation implies ownedFeatureMembership->isEmpty()
```
### C#
``` CSharp 
(!(isVariation) || !ownedFeatureMembership.Any())
```
# DeriveRelationshipRelatedElement
### OCL
``` OCL 
source->union(target)
```
### C#
``` CSharp 
source.Union(target)
```
# DeriveUsageNestedInterface
### OCL
``` OCL 
nestedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ReferenceUsage>()
```
# ValidateDecisionNodeIncomingSuccessions
### OCL
``` OCL 
targetConnector->selectByKind(Succession)->size() <= 1
```
### C#
``` CSharp 
targetConnector.OfType<Succession>().Count() <= 1
```
# ValidateStateDefinitionStateSubactionKind
### OCL
``` OCL 
ownedMembership->
    selectByKind(StateSubactionMembership)->
    isUnique(kind)
```
### C#
``` CSharp 
(ownedMembership.OfType<StateSubactionMembership>().Select(item => item.kind).Distinct().Count() == ownedMembership.OfType<StateSubactionMembership>().Count())
```
# CheckMergeNodeSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::Action::merges')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::Action::merges")
```
# ValidateFeatureIsVariable
### OCL
``` OCL 
isVariable implies
    owningType <> null and 
    owningType.specializes('Occurrences::Occurrence')
```
### C#
``` CSharp 
(!(isVariable) || owningType != null) && owningType.specializes("Occurrences::Occurrence")
```
# CheckFeatureResultRedefinition
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(Function) and
    self = owningType.oclAsType(Function).result or
 owningType.oclIsKindOf(Expression) and
    self = owningType.oclAsType(Expression).result) implies
    owningType.ownedSpecialization.general->
        select(oclIsKindOf(Function) or oclIsKindOf(Expression))->
        forAll(supertype |
            redefines(
                if superType.oclIsKindOf(Function) then
                    superType.oclAsType(Function).result
                else
                    superType.oclAsType(Expression).result
                endif)
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is ) && this == (()owningType).result || (owningType is Expression) && this == ((Expression)owningType).result)) || (()owningType.ownedSpecialization.general.Where(item => (item is Function) || (item is Expression)).All(supertype => (redefines() is )).superType).result)
```
# DeriveAssociationTargetType
### OCL
``` OCL 
    if relatedType->size() < 2 then OrderedSet{}
    else 
        relatedType->
            subSequence(2, relatedType->size())->
            asOrderedSet() 
    endif
```
### C#
``` CSharp 
(relatedType.Count() < 2 ? new List<dynamic> {  } : relatedType.subSequence(2,
relatedType.Count()).subSequence.Distinct().ToHashSet())
```
# DeriveDefinitionOwnedCase
### OCL
``` OCL 
ownedUsage->selectByKind(CaseUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<CaseUsage>()
```
# ValidateControlNodeIsComposite
### OCL
``` OCL 
isComposite
```
### C#
``` CSharp 
isComposite
```
# CheckConstructorExpressionResultFeatureRedefinition
### OCL
``` OCL 
let features : OrderedSet(Feature) = instantiatedType.feature->
    select(owningMembership.visibility = VisibilityKind::public) in
result.ownedFeature->forAll(f | 
    f.ownedRedefinition.redefinedFeature->
        intersection(features)->size() = 1)
```
### C#
``` CSharp 
instantiatedType.feature.Where(item => owningMembership.visibility == VisibilityKind.public).Select(features => result.ownedFeature.All(f => f.ownedRedefinition.redefinedFeature.Intersect(features).Count() == 1))
```
# ValidateStateUsageParallelSubactions
### OCL
``` OCL 
isParallel implies
    nestedAction.incomingTransition->isEmpty() and
    nestedAction.outgoingTransition->isEmpty()
```
### C#
``` CSharp 
(!(isParallel) || !nestedAction.incomingTransition.Any()) && !nestedAction.outgoingTransition.Any()
```
# CheckRenderingDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Views::Rendering')
```
### C#
``` CSharp 
specializesFromLibrary("Views::Rendering")
```
# DeriveTypeOwnedFeatureMembership
### OCL
``` OCL 
ownedRelationship->selectByKind(FeatureMembership)
```
### C#
``` CSharp 
ownedRelationship.OfType<FeatureMembership>()
```
# DeriveUsageNestedPort
### OCL
``` OCL 
nestedUsage->selectByKind(PortUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<PortUsage>()
```
# ValidateFlowEndIsEnd
### OCL
``` OCL 
isEnd
```
### C#
``` CSharp 
isEnd
```
# CheckAssignmentActionUsageReferentRedefinition
### OCL
``` OCL 
let targetParameter : Feature = inputParameter(1) in
targetParameter <> null and
targetParameter.ownedFeature->notEmpty() and
targetParameter->first().ownedFeature->notEmpty() and
targetParameter->first().ownedFeature->first().redefines(referent)
```
### C#
``` CSharp 
inputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.ownedFeature.Any() && targetParameter.First().ownedFeature.Any() && targetParameter.First().ownedFeature.First().redefines(referent))
```
# CheckConcernUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Requirements::concernChecks')
```
### C#
``` CSharp 
specializesFromLibrary("Requirements::concernChecks")
```
# CheckUsageVariationDefinitionSpecialization
### OCL
``` OCL 
owningVariationDefinition <> null implies
    specializes(owningVariationDefinition)
```
### C#
``` CSharp 
(!(owningVariationDefinition != null) || specializes(owningVariationDefinition))
```
# CheckConstructorExpressionResultSpecialization
### OCL
``` OCL 
result.specializes(instantiatedType)
```
### C#
``` CSharp 
result.specializes(instantiatedType)
```
# CheckSendActionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::sendActions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::sendActions")
```
# DeriveUsageNestedEnumeration
### OCL
``` OCL 
nestedUsage->selectByKind(EnumerationUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<EnumerationUsage>()
```
# CheckCalculationUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Calculations::calculations')
```
### C#
``` CSharp 
specializesFromLibrary("Calculations::calculations")
```
# CheckOccurrenceDefinitionIndividualSpecialization
### OCL
``` OCL 
isIndividual implies specializesFromLibrary('Occurrences::Life')
```
### C#
``` CSharp 
(!(isIndividual) || specializesFromLibrary("Occurrences::Life"))
```
# ValidateReferenceUsageIsReference
### OCL
``` OCL 
isReference
```
### C#
``` CSharp 
isReference
```
# CheckForLoopActionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::forLoopActions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::forLoopActions")
```
# ValidateEnumerationDefinitionIsVariation
### OCL
``` OCL 
isVariation
```
### C#
``` CSharp 
isVariation
```
# ValidateForkNodeIncomingSuccessions
### OCL
``` OCL 
targetConnector->selectByKind(Succession)->size() <= 1
```
### C#
``` CSharp 
targetConnector.OfType<Succession>().Count() <= 1
```
# DeriveAnnotatingElementAnnotatedElement
### OCL
``` OCL 
 if annotation->notEmpty() then annotation.annotatedElement
 else Sequence{owningNamespace} endif
```
### C#
``` CSharp 
(annotation.Any() ? annotation.annotatedElement : new List<dynamic> { owningNamespace })
```
# DeriveLiteralExpressionIsModelLevelEvaluable
### OCL
``` OCL 
true
```
### C#
``` CSharp 
true
```
# DeriveElementQualifiedName
### OCL
``` OCL 
    if owningNamespace = null then null
    else if name <> null and 
        owningNamespace.ownedMember->
        select(m | m.name = name).indexOf(self) <> 1 then null
    else if owningNamespace.owner = null then escapedName()
    else if owningNamespace.qualifiedName = null or 
            escapedName() = null then null
    else owningNamespace.qualifiedName + '::' + escapedName()
    endif endif endif endif
```
### C#
``` CSharp 
(owningNamespace == null ? null : (name != null && owningNamespace.ownedMember.Where(m => m.name == name).IndexOf(this) != 1
? null : (owningNamespace.owner == null ? escapedName() : (owningNamespace.qualifiedName == null || escapedName() == null ?
null : owningNamespace.qualifiedName + "::" + escapedName()))))
```
# ValidateMetadataFeatureMetaclass
### OCL
``` OCL 
type->selectByKind(Metaclass).size() = 1
```
### C#
``` CSharp 
type.OfType<Metaclass>().Length == 1
```
# DeriveAssignmentUsageTargetArgument
### OCL
``` OCL 
argument(1)
```
### C#
``` CSharp 
argument(1)
```
# CheckConnectorBinarySpecialization
### OCL
``` OCL 
connectorEnd->size() = 2 implies
    specializesFromLibrary('Links::binaryLinks')
```
### C#
``` CSharp 
(!(connectorEnd.Count() == 2) || specializesFromLibrary("Links::binaryLinks"))
```
# CheckStepSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::performances')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::performances")
```
# DeriveFeatureOwnedCrossSubsetting
### OCL
``` OCL 
    let crossSubsettings: Sequence(CrossSubsetting) = 
        ownedSubsetting->selectByKind(CrossSubsetting) in
    if crossSubsettings->isEmpty() then null
    else crossSubsettings->first()
    endif
```
### C#
``` CSharp 
ownedSubsetting.OfType<CrossSubsetting>().Select(crossSubsettings => (!crossSubsettings.Any() ? null :
crossSubsettings.First()))
```
# ValidateAnnotationAnnotatingElement
### OCL
``` OCL 
ownedAnnotatingElement <> null xor owningAnnotatingElement <> null
```
### C#
``` CSharp 
ownedAnnotatingElement != null ^ owningAnnotatingElement != null
```
# CheckConstructorExpressionSpecialization
### OCL
``` OCL 
specializes('Performances::constructorEvaluations')
```
### C#
``` CSharp 
specializes("Performances::constructorEvaluations")
```
# ValidateAssertConstraintUsageReference
### OCL
``` OCL 
referencedFeaureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(ConstraintUsage)
```
### C#
``` CSharp 
(!(referencedFeaureTarget() != null) || (referencedFeatureTarget() is ConstraintUsage))
```
# ValidateFeaturePortionNotVariable
### OCL
``` OCL 
isPortion implies not isVariable
```
### C#
``` CSharp 
(!(isPortion) || !isVariable)
```
# DeriveTypeInheritedMembership
### OCL
``` OCL 
inheritedMemberships(Set{}, Set{}, false)
```
### C#
``` CSharp 
inheritedMemberships(new HashSet<dynamic> {  }, new HashSet<dynamic> {  }, false)
```
# DeriveTypeOwnedDisjoining
### OCL
``` OCL 
    ownedRelationship->selectByKind(Disjoining)
```
### C#
``` CSharp 
ownedRelationship.OfType<Disjoining>()
```
# DeriveUsageIsReference
### OCL
``` OCL 
not isComposite
```
### C#
``` CSharp 
!isComposite
```
# DeriveTerminateActionUsageTerminatedOccurrenceArgument
### OCL
``` OCL 
argument(1)
```
### C#
``` CSharp 
argument(1)
```
# ValidateAttributeDefinitionFeatures
### OCL
``` OCL 
feature->forAll(not isComposite)
```
### C#
``` CSharp 
feature.All(item => !item.isComposite)
```
# DeriveAssignmentActionUsageValueExpression
### OCL
``` OCL 
argument(2)
```
### C#
``` CSharp 
argument(2)
```
# ValidateFeatureConstantIsVariable
### OCL
``` OCL 
isConstant implies isVariable
```
### C#
``` CSharp 
(!(isConstant) || isVariable)
```
# CheckAssignmentActionUsageStartingAtRedefinition
### OCL
``` OCL 
let targetParameter : Feature = inputParameter(1) in
targetParameter <> null and
targetParameter.ownedFeature->notEmpty() and
targetParameter.ownedFeature->first().
    redefines('AssignmentAction::target::startingAt')
```
### C#
``` CSharp 
inputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.ownedFeature.Any() && targetParameter.ownedFeature.First().redefines("AssignmentAction::target::startingAt"))
```
# CheckForLoopActionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::forLoops')
```
### C#
``` CSharp 
(!(isSubactionUsage()) || specializesFromLibrary("Actions::Action::forLoops"))
```
# DeriveNamespaceOwnedMember
### OCL
``` OCL 
ownedMembership->selectByKind(OwningMembership).ownedMemberElement
```
### C#
``` CSharp 
ownedMembership.OfType<OwningMembership>()
```
# DeriveTypeMultiplicity
### OCL
``` OCL 
    let ownedMultiplicities: Sequence(Multiplicity) =
        ownedMember->selectByKind(Multiplicity) in
    if ownedMultiplicities->isEmpty() then null
    else ownedMultiplicities->first()
    endif
```
### C#
``` CSharp 
ownedMember.OfType<Multiplicity>().Select(ownedMultiplicities => (!ownedMultiplicities.Any() ? null : ownedMultiplicities.First()))
```
# DeriveDefinitionOwnedCalculation
### OCL
``` OCL 
ownedUsage->selectByKind(CalculationUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<CalculationUsage>()
```
# CheckAssignmentActionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::assignmentActions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::assignmentActions")
```
# ValidateCaseDefinitionOnlyOneObjective
### OCL
``` OCL 
featureMembership->
    selectByKind(ObjectiveMembership)->
    size() <= 1
```
### C#
``` CSharp 
featureMembership.OfType<ObjectiveMembership>().Count() <= 1
```
# DeriveTransitionUsageTarget
### OCL
``` OCL 
    if succession.targetFeature->isEmpty() then null
    else
        let targetFeature : Feature =
            succession.targetFeature->first().featureTarget in
        if not targetFeature.oclIsKindOf(ActionUsage) then null
        else targetFeature.oclAsType(ActionUsage)
        endif
    endif
```
### C#
``` CSharp 
(!succession.targetFeature.Any() ? null : (succession.targetFeature.First().featureTarget).Select(targetFeature =>
(!(targetFeature is ActionUsage) ? null : ((ActionUsage)targetFeature))))
```
# CheckMetadataUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Metadata::metadataItems')
```
### C#
``` CSharp 
specializesFromLibrary("Metadata::metadataItems")
```
# DeriveDefinitionOwnedItem
### OCL
``` OCL 
ownedUsage->selectByKind(ItemUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ItemUsage>()
```
# CheckActionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::subactions')
```
### C#
``` CSharp 
(!(isSubactionUsage()) || specializesFromLibrary("Actions::Action::subactions"))
```
# CheckMetadataFeatureSpecialization
### OCL
``` OCL 
specializesFromLibrary('Metaobjects::metaobjects')
```
### C#
``` CSharp 
specializesFromLibrary("Metaobjects::metaobjects")
```
# DeriveTypeOwnedEndFeature
### OCL
``` OCL 
ownedFeature->select(isEnd)
```
### C#
``` CSharp 
ownedFeature.Where(item => item.isEnd)
```
# CheckInvocationExpressionBehaviorBindingConnector
### OCL
``` OCL 
not instantiatedType.oclIsKindOf(Function) and
not (instantiatedType.oclIsKindOf(Feature) and 
     instantiatedType.oclAsType(Feature).type->exists(oclIsKindOf(Function))) implies
    ownedFeature.selectByKind(BindingConnector)->exists(
        relatedFeature->includes(self) and
        relatedFeature->includes(result))
```
### C#
``` CSharp 
(!(!(instantiatedType is ) && !((instantiatedType is Feature) && ((Feature)instantiatedType).type.Any(item => (item is Function)))) || ownedFeature.selectByKind(BindingConnector).Any(item => relatedFeature.Contains(this) && relatedFeature.Contains(result)))
```
# ValidateInstantiationExpressionResult
### OCL
``` OCL 
result.owningType = self
```
### C#
``` CSharp 
result.owningType == this
```
# DeriveUsageNestedMetadata
### OCL
``` OCL 
nestedUsage->selectByKind(MetadataUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<MetadataUsage>()
```
# ValidatePortDefinitionOwnedUsagesNotComposite
### OCL
``` OCL 
ownedUsage->
    reject(oclIsKindOf(PortUsage))->
    forAll(not isComposite)
```
### C#
``` CSharp 
ownedUsage.Where(item => !((item is PortUsage))).All(item2 => !item2.isComposite)
```
# CheckForkNodeSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::Action::forks')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::Action::forks")
```
# CheckTransitionUsageSuccessionBindingConnector
### OCL
``` OCL 
ownedMember->selectByKind(BindingConnector)->exists(b |
    b.relatedFeatures->includes(succession) and
    b.relatedFeatures->includes(resolveGlobal(
        'TransitionPerformances::TransitionPerformance::transitionLink')))
```
### C#
``` CSharp 
ownedMember.OfType<BindingConnector>().Any(b => b.relatedFeatures.Contains(succession) && b.relatedFeatures.Contains(resolveGlobal("TransitionPerformances::TransitionPerformance::transitionLink")))
```
# ValidatePortUsageIsReference
### OCL
``` OCL 
owningType = null or
not owningType.oclIsKindOf(PortDefinition) and
not owningType.oclIsKindOf(PortUsage) implies
    isReference
```
### C#
``` CSharp 
(!(owningType == null || !(owningType is PortDefinition) && !(owningType is PortUsage)) || isReference)
```
# CheckFeatureChainExpressionSourceTargetRedefinition
### OCL
``` OCL 
let sourceParameter : Feature = sourceTargetFeature() in
sourceTargetFeature <> null and
sourceTargetFeature.redefines(targetFeature)
```
### C#
``` CSharp 
sourceTargetFeature().Select(sourceParameter => sourceTargetFeature != null && sourceTargetFeature.redefines(targetFeature))
```
# DeriveFeatureFeatureTarget
### OCL
``` OCL 
if chainingFeature->isEmpty() then self else chainingFeature->last() endif
```
### C#
``` CSharp 
(!chainingFeature.Any() ? this : chainingFeature.Last())
```
# CheckVerificationCaseUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('VerificationCases::verificationCases')
```
### C#
``` CSharp 
specializesFromLibrary("VerificationCases::verificationCases")
```
# ValidateCaseUsageSubjectParameterPosition
### OCL
``` OCL 
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp 
input.Any() && input.First() == subjectParameter
```
# CheckActionUsageOwnedActionSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(PartDefinition) or
 owningType.oclIsKindOf(PartUsage)) implies
    specializesFromLibrary('Parts::Part::ownedActions')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is PartDefinition) || (owningType is PartUsage))) || specializesFromLibrary("Parts::Part::ownedActions"))
```
# DeriveAnalysisCaseDefinitionResultExpression
### OCL
``` OCL 
    let results : OrderedSet(ResultExpressionMembership) =
        featureMembersip->
            selectByKind(ResultExpressionMembership) in
    if results->isEmpty() then null
    else results->first().ownedResultExpression
    endif
```
### C#
``` CSharp 
featureMembersip.OfType<ResultExpressionMembership>().Select(results => (!results.Any() ? null :
results.First().ownedResultExpression))
```
# CheckExpressionResultBindingConnector
### OCL
``` OCL 
ownedMembership.selectByKind(ResultExpressionMembership)->
    forAll(mem | ownedFeature.selectByKind(BindingConnector)->
        exists(binding |
            binding.relatedFeature->includes(result) and
            binding.relatedFeature->includes(mem.ownedResultExpression.result)))
```
### C#
``` CSharp 
ownedMembership.selectByKind(ResultExpressionMembership).All(mem => ownedFeature.selectByKind(BindingConnector).Any(binding => binding.relatedFeature.Contains(result) && binding.relatedFeature.Contains(mem.ownedResultExpression.result)))
```
# CheckFlowUsageFlowSpecialization
### OCL
``` OCL 
ownedEndFeatures->notEmpty() implies
    specializesFromLibrary('Flows::flows')
```
### C#
``` CSharp 
(!(ownedEndFeatures.Any()) || specializesFromLibrary("Flows::flows"))
```
# DeriveDefinitionOwnedPort
### OCL
``` OCL 
ownedUsage->selectByKind(PortUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<PortUsage>()
```
# CheckAllocationDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Allocations::Allocation')
```
### C#
``` CSharp 
specializesFromLibrary("Allocations::Allocation")
```
# ValidateMetadataFeatureAnnotatedElement
### OCL
``` OCL 
let baseAnnotatedElementFeature : Feature =
    resolveGlobal('Metaobjects::Metaobject::annotatedElement').memberElement.
    oclAsType(Feature) in
let annotatedElementFeatures : OrderedSet(Feature) = feature->
    select(specializes(baseAnnotatedElementFeature))->
    excluding(baseAnnotatedElementFeature) in
annotatedElementFeatures->notEmpty() implies
    let annotatedElementTypes : Set(Feature) =
        annotatedElementFeatures.typing.type->asSet() in
    let metaclasses : Set(Metaclass) =
        annotatedElement.oclType().qualifiedName->collect(qn | 
            resolveGlobal(qn).memberElement.oclAsType(Metaclass)) in
   metaclasses->forAll(m | annotatedElementTypes->exists(t | m.specializes(t)))
```
### C#
``` CSharp 
((Feature)resolveGlobal("Metaobjects::Metaobject::annotatedElement").memberElement).Select(baseAnnotatedElementFeature => feature.Where(item => specializes(baseAnnotatedElementFeature)).Where(item2 => item2 != baseAnnotatedElementFeature).Select(annotatedElementFeatures => (!(annotatedElementFeatures.Any()) || annotatedElementTypes is (Feature) == annotatedElementFeatures.typing.type.ToHashSet())))
```
# CheckSendActionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::acceptSubactions')
```
### C#
``` CSharp 
(!(isSubactionUsage()) || specializesFromLibrary("Actions::Action::acceptSubactions"))
```
# CheckITestionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::ifSubactions')
```
### C#
``` CSharp 
(!(isSubactionUsage()) || specializesFromLibrary("Actions::Action::ifSubactions"))
```
# ValidateInstantiationExpressionInstantiatedType
### OCL
``` OCL 
instantiatedType() <> null
```
### C#
``` CSharp 
instantiatedType() != null
```
# DeriveTypeOwnedDifferencing
### OCL
``` OCL 
    ownedRelationship->selectByKind(Differencing)
```
### C#
``` CSharp 
ownedRelationship.OfType<Differencing>()
```
# DeriveNamespaceImportedMembership
### OCL
``` OCL 
importedMemberships(Set{})
```
### C#
``` CSharp 
importedMemberships(new HashSet<dynamic> {  })
```
# DeriveTransitionUsageSource
### OCL
``` OCL 
    let sourceFeature : Feature = sourceFeature() in
    if sourceFeature = null then null
    else sourceFeature.featureTarget.oclAsType(ActionUsage)
```
### C#
``` CSharp 
sourceFeature().Select(sourceFeature => (sourceFeature == null ? null : ((ActionUsage)sourceFeature.featureTarget)))
```
# CheckSuccessionFlowSpecialization
### OCL
``` OCL 
specializesFromLibrary('Transfers::flowTransfersBefore')
```
### C#
``` CSharp 
specializesFromLibrary("Transfers::flowTransfersBefore")
```
# DeriveUsageVariant
### OCL
``` OCL 
variantMembership.ownedVariantUsage
```
### C#
``` CSharp 
variantMembership.ownedVariantUsage
```
# CheckInvocationExpressionSpecialization
### OCL
``` OCL 
specializes(instantiatedType)
```
### C#
``` CSharp 
specializes(instantiatedType)
```
# CheckTerminateActionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::terminateActions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::terminateActions")
```
# DeriveWhileLoopActionUsageWhileArgument
### OCL
``` OCL 
    let parameter : Feature = inputParameter(1) in
    if parameter <> null and parameter.oclIsKindOf(Expression) then
        parameter.oclAsType(Expression)
    else
        null
    endif
```
### C#
``` CSharp 
inputParameter(1).Select(parameter => (parameter != null && (parameter is Expression) ? ((Expression)parameter) : null))
```
# DeriveSendActionUsageReceiverArgument
### OCL
``` OCL 
argument(3)
```
### C#
``` CSharp 
argument(3)
```
# ValidateFeatureChainExpressionOperator
### OCL
``` OCL 
'.'
```
### C#
``` CSharp 
"."
```
# ValidateFunctionResultExpressionMembership
### OCL
``` OCL 
membership->selectByKind(ResultExpressionMembership)->size() <= 1
```
### C#
``` CSharp 
membership.OfType<ResultExpressionMembership>().Count() <= 1
```
# DeriveFlowSourceOutputFeature
### OCL
``` OCL 
    if connectorEnd->isEmpty() or 
        connectorEnd.ownedFeature->isEmpty()
    then null
    else connectorEnd.ownedFeature->first()
    endif
```
### C#
``` CSharp 
(!connectorEnd.Any() || !connectorEnd.ownedFeature.Any() ? null : connectorEnd.ownedFeature.First())
```
# DeriveAnnotatingElementAnnotation
### OCL
``` OCL 
    if owningAnnotatingRelationship = null then ownedAnnotatingRelationship
    else owningAnnotatingRelationship->prepend(owningAnnotatingRelationship)
    endif
```
### C#
``` CSharp 
(owningAnnotatingRelationship == null ? ownedAnnotatingRelationship :
owningAnnotatingRelationship.Prepend(owningAnnotatingRelationship))
```
# ValidateTransitionUsageTriggerActions
### OCL
``` OCL 
source <> null and not source.oclIsKindOf(StateUsage) implies
    triggerAction->isEmpty()
```
### C#
``` CSharp 
(!(source != null && !(source is StateUsage)) || !triggerAction.Any())
```
# CheckActionDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::Action')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::Action")
```
# DeriveDefinitionOwnedAttribute
### OCL
``` OCL 
ownedUsage->selectByKind(AttributeUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<AttributeUsage>()
```
# CheckFeatureEndRedefinition
### OCL
``` OCL 
isEnd and owningType <> null implies
    let i : Integer = 
        owningType.ownedEndFeature->indexOf(self) in
    owningType.ownedSpecialization.general->
        forAll(supertype |
             supertype.endFeature->size() >= i implies
                redefines(supertype.endFeature->at(i))
```
### C#
``` CSharp 
(!(isEnd && owningType != null) || i is Integer == owningType.ownedEndFeature.ToList().IndexOf(this))
```
# DeriveAnnotatingElementOwnedAnnotatingRelationship
### OCL
``` OCL 
ownedRelationship->
    selectByKind(Annotation)->
    select(a | a.annotatedElement <> self)
```
### C#
``` CSharp 
ownedRelationship.OfType<Annotation>().Where(a => a.annotatedElement != this)
```
# ValidateVariantMembershipOwningNamespace
### OCL
``` OCL 
membershipOwningNamespace.oclIsKindOf(Definition) and
    membershipOwningNamespace.oclAsType(Definition).isVariation or
membershipOwningNamespace.oclIsKindOf(Usage) and
    membershipOwningNamespace.oclAsType(Usage).isVariation
```
### C#
``` CSharp 
(membershipOwningNamespace is Definition) && ((Definition)membershipOwningNamespace).isVariation || (membershipOwningNamespace is Usage) && ((Usage)membershipOwningNamespace).isVariation
```
# DeriveStateDefinitionExitAction
### OCL
``` OCL 
    let exitMemberships : Sequence(StateSubactionMembership) =
        ownedMembership->
            selectByKind(StateSubactionMembership)->
            select(kind = StateSubactionKind::exit) in
    if exitMemberships->isEmpty() then null
    else exitMemberships->at(1)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.OfType<StateSubactionMembership>().Where(item => item.kind == StateSubactionKind.exit).Select(exitMemberships =>
(!exitMemberships.Any() ? null : exitMemberships.ElementAt(0)))
```
# DeriveDefinitionOwnedAnalysisCase
### OCL
``` OCL 
ownedUsage->selectByKind(AnalysisCaseUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<AnalysisCaseUsage>()
```
# ValidateClassSpecialization
### OCL
``` OCL 
ownedSpecialization.general->
    forAll(not oclIsKindOf(DataType)) and
not oclIsKindOf(Association) implies
    ownedSpecialization.general->
        forAll(not oclIsKindOf(Association))
```
### C#
``` CSharp 
(!(ownedSpecialization.general.All(item => !(item is DataType)) && !(Association)) || ownedSpecialization.general.All(item => !(item is Association)))
```
# DeriveUsageUsage
### OCL
``` OCL 
feature->selectByKind(Usage)
```
### C#
``` CSharp 
feature.OfType<Usage>()
```
# CheckIncludeUseCaseSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(UseCaseDefinition) or
 owningType.oclIsKindOf(UseCaseUsage) implies
    specializesFromLibrary('UseCases::UseCase::includedUseCases')
```
### C#
``` CSharp 
owningType != null && ((!((owningType is UseCaseDefinition) || (owningType is UseCaseUsage)) || specializesFromLibrary("UseCases::UseCase::includedUseCases")))
```
# DeriveStateUsageDoAction
### OCL
``` OCL 
    let doMemberships : Sequence(StateSubactionMembership) =
        ownedMembership->
            selectByKind(StateSubactionMembership)->
            select(kind = StateSubactionKind::do) in
    if doMemberships->isEmpty() then null
    else doMemberships->at(1)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.OfType<StateSubactionMembership>().Where(item => item.kind == StateSubactionKind.do).Select(doMemberships => (!doMemberships.Any() ? null : doMemberships.ElementAt(0)))
```
# DeriveItemUsageItemDefinition
### OCL
``` OCL 
occurrenceDefinition->selectByKind(Structure)
```
### C#
``` CSharp 
occurrenceDefinition.OfType<Structure>()
```
# DeriveFeatureOwnedFeatureInverting
### OCL
``` OCL 
ownedRelationship->selectByKind(FeatureInverting)->
    select(fi | fi.featureInverted = self)
```
### C#
``` CSharp 
ownedRelationship.OfType<FeatureInverting>().Where(fi => fi.featureInverted == this)
```
# DeriveDefinitionUsage
### OCL
``` OCL 
feature->selectByKind(Usage)
```
### C#
``` CSharp 
feature.OfType<Usage>()
```
# ValidateAttributeUsageFeatures
### OCL
``` OCL 
feature->forAll(not isComposite)
```
### C#
``` CSharp 
feature.All(item => !item.isComposite)
```
# ValidateCaseDefinitionSubjectParameterPosition
### OCL
``` OCL 
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp 
input.Any() && input.First() == subjectParameter
```
# DeriveDefinitionOwnedView
### OCL
``` OCL 
ownedUsage->selectByKind(ViewUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ViewUsage>()
```
# DeriveElementOwner
### OCL
``` OCL 
owningRelationship.owningRelatedElement
```
### C#
``` CSharp 
owningRelationship.owningRelatedElement
```
# DeriveUsageNestedAttribute
### OCL
``` OCL 
nestedUsage->selectByKind(AttributeUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<AttributeUsage>()
```
# DeriveRequirementDefinitionStakeholderParameter
### OCL
``` OCL 
featureMembership->
    selectByKind(StakholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
featureMembership.OfType<StakholderMembership>()
```
# ValidateAssociationEndTypes
### OCL
``` OCL 
ownedEndFeature->forAll(type->size() = 1)
```
### C#
``` CSharp 
ownedEndFeature.All(item => item.type.Count() == 1)
```
# DeriveElementShortName
### OCL
``` OCL 
effectiveShortName()
```
### C#
``` CSharp 
effectiveShortName()
```
# DeriveAssociationSourceType
### OCL
``` OCL 
    if relatedType->isEmpty() then null
    else relatedType->first() endif
```
### C#
``` CSharp 
(!relatedType.Any() ? null : relatedType.First())
```
# ValidateTransitionFeatureMembershipEffectAction
### OCL
``` OCL 
TransitionFeatureKind::effect implies
    transitionFeature.oclIsKindOf(ActionUsage)
```
### C#
``` CSharp 
(!(TransitionFeatureKind.effect) || (transitionFeature is ActionUsage))
```
# DeriveViewUsageViewRendering
### OCL
``` OCL 
    let renderings: OrderedSet(ViewRenderingMembership) =
        featureMembership->selectByKind(ViewRenderingMembership) in
    if renderings->isEmpty() then null
    else renderings->first().referencedRendering
    endif
```
### C#
``` CSharp 
featureMembership.OfType<ViewRenderingMembership>().Select(renderings => (!renderings.Any() ? null :
renderings.First().referencedRendering))
```
# CheckPredicateSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::BooleanEvaluation')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::BooleanEvaluation")
```
# ValidatePartUsagePartDefinition
### OCL
``` OCL 
partDefinition->notEmpty()
```
### C#
``` CSharp 
partDefinition.Any()
```
# CheckFeatureOwnedCrossFeatureTypeFeaturing
### OCL
``` OCL 
isOwnedCrossFeature() implies
    let otherEnds : OrderedSet(Feature) = 
        owner.oclAsType(Feature).owningType.endFeature->excluding(self) in
    if (otherEnds->size() = 1) then
        featuringType = otherEnds->first().type
    else
        featuringType->size() = 1 and
        featuringType->first().isCartesianProduct() and
        featuringType->first().asCartesianProduct() = otherEnds.type and
        featuringType->first().allSupertypes()->includesAll(
            owner.oclAsType(Feature).ownedRedefinition.redefinedFeature->
               select(crossFeature() <> null).crossFeature().featuringType)      
    endif
```
### C#
``` CSharp 
(!(isOwnedCrossFeature()) || otherEnds is (Feature) == ((Feature)owner).owningType.endFeature.Where(item => item != this))
```
# CheckFeatureSuboccurrenceSpecialization
### OCL
``` OCL 
isComposite and
ownedTyping.type->includes(oclIsKindOf(Class)) and
owningType <> null and
(owningType.oclIsKindOf(Class) or
 owningType.oclIsKindOf(Feature) and
    owningType.oclAsType(Feature).type->
        exists(oclIsKindOf(Class))) implies
    specializesFromLibrary('Occurrence::Occurrence::suboccurrences')
```
### C#
``` CSharp 
(!(isComposite && ownedTyping.type.Contains((Class)) && owningType != null && ((owningType is Class) || (owningType is Feature) && ((Feature)owningType).type.Any(item => (item is Class)))) || specializesFromLibrary("Occurrence::Occurrence::suboccurrences"))
```
# CheckFeatureSpecialization
### OCL
``` OCL 
specializesFromLibrary('Base::things')
```
### C#
``` CSharp 
specializesFromLibrary("Base::things")
```
# CheckActionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::actions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::actions")
```
# DerivePortDefinitionConjugatedPortDefinition
### OCL
``` OCL 
let conjugatedPortDefinitions : OrderedSet(ConjugatedPortDefinition) =
    ownedMember->selectByKind(ConjugatedPortDefinition) in
if conjugatedPortDefinitions->isEmpty() then null
else conjugatedPortDefinitions->first()
endif
```
### C#
``` CSharp 
ownedMember.OfType<ConjugatedPortDefinition>().Select(conjugatedPortDefinitions => (!conjugatedPortDefinitions.Any() ? null : conjugatedPortDefinitions.First()))
```
# DeriveTypeUnioningType
### OCL
``` OCL 
ownedUnioning.unioningType
```
### C#
``` CSharp 
ownedUnioning.unioningType
```
# DeriveLoopActionUsageBodyAction
### OCL
``` OCL 
    let parameter : Feature = inputParameter(2) in
    if parameter <> null and parameter.oclIsKindOf(Action) then
        parameter.oclAsType(Action)
    else
        null
    endif
```
### C#
``` CSharp 
inputParameter(2).Select(parameter => (parameter != null && (parameter is Action) ? ((Action)parameter) : null))
```
# CheckInvocationExpressionBehaviorResultSpecialization
### OCL
``` OCL 
not instantiatedType.oclIsKindOf(Function) and
not (instantiatedType.oclIsKindOf(Feature) and 
     instantiatedType.oclAsType(Feature).type->exists(oclIsKindOf(Function))) implies
    result.specializes(instantiatedType)
```
### C#
``` CSharp 
(!(!(instantiatedType is Function) && !((instantiatedType is Feature) && ((Feature)instantiatedType).type.Any(item => (item is Function)))) || result.specializes(instantiatedType))
```
# CheckInterfaceUsageBinarySpecialization
### OCL
``` OCL 
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Interfaces::binaryInterfaces')
```
### C#
``` CSharp 
(!(ownedEndFeature.Count() == 2) || specializesFromLibrary("Interfaces::binaryInterfaces"))
```
# CheckCalculationDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Calculations::Calculation')
```
### C#
``` CSharp 
specializesFromLibrary("Calculations::Calculation")
```
# CheckFeatureDataValueSpecialization
### OCL
``` OCL 
ownedTyping.type->exists(selectByKind(DataType)) implies
    specializesFromLibrary('Base::dataValues')
```
### C#
``` CSharp 
(!(ownedTyping.type.Any(item => item.selectByKind(DataType))) || specializesFromLibrary("Base::dataValues"))
```
# CheckInvariantSpecialization
### OCL
``` OCL 
if isNegated then
    specializesFromLibrary('Performances::falseEvaluations')
else
    specializesFromLibrary('Performances::trueEvaluations')
endif
```
### C#
``` CSharp 
(isNegated ? specializesFromLibrary("Performances::falseEvaluations") : specializesFromLibrary("Performances::trueEvaluations"))
```
# DeriveStateDefinitionDoAction
### OCL
``` OCL 
    let doMemberships : Sequence(StateSubactionMembership) =
        ownedMembership->
            selectByKind(StateSubactionMembership)->
            select(kind = StateSubactionKind::do) in
    if doMemberships->isEmpty() then null
    else doMemberships->at(1)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.OfType<StateSubactionMembership>().Where(item => item.kind == StateSubactionKind.do).Select(doMemberships =>
(!doMemberships.Any() ? null : doMemberships.ElementAt(1 - 1)))
```
# ValidateTypeAtMostOneConjugator
### OCL
``` OCL 
ownedRelationship->selectByKind(Conjugation)->size() <= 1
```
### C#
``` CSharp 
ownedRelationship.OfType<Conjugation>().Count() <= 1
```
# DeriveDefinitionDirectedUsage
### OCL
``` OCL 
directedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
directedFeature.OfType<Usage>()
```
# DeriveTransitionUsageEffectAction
### OCL
``` OCL 
ownedFeatureMembership->
    selectByKind(TransitionFeatureMembership)->
    select(kind = TransitionFeatureKind::trigger).transitionFeatures->
    selectByKind(AcceptActionUsage)
```
### C#
``` CSharp 
ownedFeatureMembership.OfType<TransitionFeatureMembership>().Where(item => item.kind == TransitionFeatureKind.trigger).transitionFeatures.OfType<AcceptActionUsage>()
```
# ValidateExpressionResultParameterMembership
### OCL
``` OCL 
featureMembership->
    selectByKind(ReturnParameterMembership)->
    size() = 1
```
### C#
``` CSharp 
featureMembership.OfType<ReturnParameterMembership>().Count() == 1
```
# DeriveUsageNestedUseCase
### OCL
``` OCL 
nestedUsage->selectByKind(UseCaseUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<UseCaseUsage>()
```
# CheckOccurrenceUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Occurrences::occurrences')
```
### C#
``` CSharp 
specializesFromLibrary("Occurrences::occurrences")
```
# DeriveRequirementConstraintMembershipReferencedConstraint
### OCL
``` OCL 
    let referencedFeature : Feature = 
        ownedConstraint.referencedFeatureTarget() in
    if referencedFeature = null then ownedConstraint
    else if referencedFeature.oclIsKindOf(ConstraintUsage) then
        refrencedFeature.oclAsType(ConstraintUsage)
    else null
    endif endif
```
### C#
``` CSharp 
ownedConstraint.referencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? ownedConstraint :
((referencedFeature is ConstraintUsage) ? ((ConstraintUsage)refrencedFeature) : null)))
```
# ValidateRequirementUsageSubjectParameterPosition
### OCL
``` OCL 
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp 
input.Any() && input.First() == subjectParameter
```
# DeriveDefinitionOwnedRendering
### OCL
``` OCL 
ownedUsage->selectByKind(RenderingUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<RenderingUsage>()
```
# CheckFlowWithEndsSpecialization
### OCL
``` OCL 
ownedEndFeatures->notEmpty() implies
    specializesFromLibrary('Transfers::flowTransfers')
```
### C#
``` CSharp 
(!(ownedEndFeatures.Any()) || specializesFromLibrary("Transfers::flowTransfers"))
```
# ValidateRequirementConstraintMembershipIsComposite
### OCL
``` OCL 
ownedConstraint.isComposite
```
### C#
``` CSharp 
ownedConstraint.isComposite
```
# DeriveRequirementUsageFramedConcern
### OCL
``` OCL 
featureMembership->
    selectByKind(FramedConcernMembership).
    ownedConcern
```
### C#
``` CSharp 
featureMembership.OfType<FramedConcernMembership>()
```
# DeriveTypeEndFeature
### OCL
``` OCL 
feature->select(isEnd)
```
### C#
``` CSharp 
feature.Where(item => item.isEnd)
```
# ValidateTriggerInvocationExpressionAtArgument
### OCL
``` OCL 
TriggerKind::at implies
    argument->notEmpty() and
    argument->at(1).result.specializesFromLibrary('Time::TimeInstantValue')
```
### C#
``` CSharp 
(!(TriggerKind.at) || argument.Any()) && argument.ElementAt(0).specializesFromLibrary("Time::TimeInstantValue")
```
# ValidateOccurrenceUsageIndividualUsage
### OCL
``` OCL 
isIndividual implies individualDefinition <> null
```
### C#
``` CSharp 
(!(isIndividual) || individualDefinition != null)
```
# CheckStepSubperformanceSpecialization
### OCL
``` OCL 
owningType <> null and
    (owningType.oclIsKindOf(Behavior) or
     owningType.oclIsKindOf(Step)) and
    self.isComposite implies
    specializesFromLibrary('Performances::Performance::subperformance')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is Behavior) || (owningType is Step)) && this.isComposite) || specializesFromLibrary("Performances::Performance::subperformance"))
```
# DeriveCaseDefinitionObjectiveRequirement
### OCL
``` OCL 
    let objectives: OrderedSet(RequirementUsage) = 
        featureMembership->
            selectByKind(ObjectiveMembership).
            ownedRequirement in
    if objectives->isEmpty() then null
    else objectives->first().ownedObjectiveRequirement
    endif
```
### C#
``` CSharp 
//TODO fix
featureMembership.OfType<ObjectiveMembership>().Select(objectives => objectives.ownedRequirement).Select(objectives => (!objectives.Any() ? null :
objectives.First().ownedObjectiveRequirement))
```
# DeriveRequirementUsageRequiredConstraint
### OCL
``` OCL 
ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::requirement).
    ownedConstraint
```
### C#
``` CSharp 
ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => item.kind == RequirementConstraintKind.requirement).ownedConstraint
```
# ValidateFlowPayloadFeature
### OCL
``` OCL 
ownedFeature->selectByKind(PayloadFeature)->size() <= 1
```
### C#
``` CSharp 
ownedFeature.OfType<PayloadFeature>().Count() <= 1
```
# CheckFeaturePortionSpecialization
### OCL
``` OCL 
isPortion and
ownedTyping.type->includes(oclIsKindOf(Class)) and
owningType <> null and
(owningType.oclIsKindOf(Class) or
 owningType.oclIsKindOf(Feature) and
    owningType.oclAsType(Feature).type->
        exists(oclIsKindOf(Class))) implies
    specializesFromLibrary('Occurrence::Occurrence::portions')
```
### C#
``` CSharp 
(!(isPortion && ownedTyping.type.Contains((Class)) && owningType != null && ((owningType is Class) || (owningType is Feature) && ((Feature)owningType).type.Any(item => (item is Class)))) || specializesFromLibrary("Occurrence::Occurrence::portions"))
```
# CheckInterfaceDefinitionBinarySpecialization
### OCL
``` OCL 
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Interfaces::BinaryInterface')
```
### C#
``` CSharp 
(!(ownedEndFeature.Count() == 2) || specializesFromLibrary("Interfaces::BinaryInterface"))
```
# CheckMetaclassSpecialization
### OCL
``` OCL 
specializesFromLibrary('Metaobjects::Metaobject')
```
### C#
``` CSharp 
specializesFromLibrary("Metaobjects::Metaobject")
```
# ValidateCaseUsageOnlyOneObjective
### OCL
``` OCL 
featureMembership->
    selectByKind(ObjectiveMembership)->
    size() <= 1
```
### C#
``` CSharp 
featureMembership.OfType<ObjectiveMembership>().Count() <= 1
```
# CheckAnalysisCaseDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('AnalysisCases::AnalysisCase')
```
### C#
``` CSharp 
specializesFromLibrary("AnalysisCases::AnalysisCase")
```
# ValidateUsageIsReferential
### OCL
``` OCL 
direction <> null or isEnd or featuringType->isEmpty() implies
    isReference
```
### C#
``` CSharp 
(!(direction != null || isEnd || !featuringType.Any()) || isReference)
```
# CheckAssociationStructureSpecialization
### OCL
``` OCL 
specializesFromLibrary('Objects::LinkObject')
```
### C#
``` CSharp 
specializesFromLibrary("Objects::LinkObject")
```
# DeriveUsageNestedAction
### OCL
``` OCL 
nestedUsage->selectByKind(ActionUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ActionUsage>()
```
# DeriveDefinitionOwnedTransition
### OCL
``` OCL 
ownedUsage->selectByKind(TransitionUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<TransitionUsage>()
```
# CheckConnectionDefinitionBinarySpecialization
### OCL
``` OCL 
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Connections::BinaryConnections')
```
### C#
``` CSharp 
(!(ownedEndFeature.Count() == 2) || specializesFromLibrary("Connections::BinaryConnections"))
```
# DeriveUsageNestedAnalysisCase
### OCL
``` OCL 
nestedUsage->selectByKind(AnalysisCaseUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<AnalysisCaseUsage>()
```
# CheckViewpointDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Views::Viewpoint')
```
### C#
``` CSharp 
specializesFromLibrary("Views::Viewpoint")
```
# ValidateViewRenderingMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(ViewDefinition) or
owningType.oclIsKindOf(ViewUsage)
```
### C#
``` CSharp 
(owningType is ViewDefinition) || (owningType is ViewUsage)
```
# DeriveUsageNestedView
### OCL
``` OCL 
nestedUsage->selectByKind(ViewUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ViewUsage>()
```
# DeriveRequirementDefinitionSubjectParameter
### OCL
``` OCL 
    let subjects : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjects->isEmpty() then null
    else subjects->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
featureMembership.OfType<SubjectMembership>().Select(subjects => (!subjects.Any() ? null : subjects.First().ownedSubjectParameter))
```
# ValidateParameterMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(Behavior) or owningType.oclIsKindOf(Step) or
owningType.owningMembership.oclIsKindOf(ReturnParameterMembership) and
    owningType.owningNamespace.oclIsKindOf(ConstructorExpression)
```
### C#
``` CSharp 
(owningType is Behavior) || (owningType is Step) || (owningType.owningMembership is ReturnParameterMembership) && (owningType.owningNamespace is ConstructorExpression)
```
# CheckStateUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('States::stateActions')
```
### C#
``` CSharp 
specializesFromLibrary("States::stateActions")
```
# ValidateRequirementUsageOnlyOneSubject
### OCL
``` OCL 
featureMembership->
    selectByKind(SubjectMembership)->
    size() <= 1
```
### C#
``` CSharp 
featureMembership.OfType<SubjectMembership>().Count() <= 1
```
# CheckFeatureOccurrenceSpecialization
### OCL
``` OCL 
ownedTyping.type->exists(selectByKind(Class)) implies
    specializesFromLibrary('Occurrences::occurrences')
```
### C#
``` CSharp 
(!(ownedTyping.type.Any(item => item.selectByKind(Class))) || specializesFromLibrary("Occurrences::occurrences"))
```
# CheckConstraintUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Constraints::constraintChecks')
```
### C#
``` CSharp 
specializesFromLibrary("Constraints::constraintChecks")
```
# DeriveFeatureChainingFeature
### OCL
``` OCL 
ownedFeatureChaining.chainingFeature
```
### C#
``` CSharp 
ownedFeatureChaining.chainingFeature
```
# ValidateCaseUsageOnlyOneSubject
### OCL
``` OCL 
featureMembership->
	selectByKind(SubjectMembership)->
	size() <= 1
```
### C#
``` CSharp 
featureMembership.OfType<SubjectMembership>().Count() <= 1
```
# CheckOccurrenceUsageTimeSliceSpecialization
### OCL
``` OCL 
portionKind = PortionKind::timeslice implies 
    specializesFromLibrary('Occurrences::Occurrence::timeSlices')
```
### C#
``` CSharp 
(!(portionKind == PortionKind.timeslice) || specializesFromLibrary("Occurrences::Occurrence::timeSlices"))
```
# CheckConnectionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Connections::connections')
```
### C#
``` CSharp 
specializesFromLibrary("Connections::connections")
```
# CheckTransitionUsageTransitionFeatureSpecialization
### OCL
``` OCL 
triggerAction->forAll(specializesFromLibrary('Actions::TransitionAction::accepter') and
guardExpression->forAll(specializesFromLibrary('Actions::TransitionAction::guard') and
effectAction->forAll(specializesFromLibrary('Actions::TransitionAction::effect'))
```
### C#
``` CSharp 
triggerAction.All(item => item.specializesFromLibrary("Actions::TransitionAction::accepter") && guardExpression.All(item => item.specializesFromLibrary("Actions::TransitionAction::guard") && effectAction.All(item => itemspecializesFromLibrary("Actions::TransitionAction::effect"))))
```
# CheckLiteralInfinitySpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::literalIntegerEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::literalIntegerEvaluations")
```
# CheckFeatureParameterRedefinition
### OCL
``` OCL 
owningType <> null and
not owningFeatureMembership.
    oclIsKindOf(ReturnParameterMembership) and
(owningType.oclIsKindOf(Behavior) or
 owningType.oclIsKindOf(Step) and
    (owningType.oclIsKindOf(InvocationExpression) implies
      not ownedRedefinition->exists(not isImplied)) 
implies
    let i : Integer =
        owningType.ownedFeature->select(direction <> null)->
            reject(owningFeatureMembership.
                oclIsKindOf(ReturnParameterMembership))->
            indexOf(self) in
    owningType.ownedSpecialization.general->
        forAll(supertype |
            let ownedParameters : Sequence(Feature) =
                supertype.ownedFeature->select(direction <> null)->
                     reject(owningFeatureMembership.
                         oclIsKindOf(ReturnParameterMembership)) in
            ownedParameters->size() >= i implies
                redefines(ownedParameters->at(i))
```
### C#
``` CSharp 
// TODO
owningType != null && !(owningFeatureMembership is ReturnParameterMembership) && owningType.ownedSpecialization.general.All(supertype => supertype.ownedFeature.Where(item => item.direction != null).Where(item2 => !((item.owningFeatureMembership is ReturnParameterMembership))).Select(ownedParameters => (!(ownedParameters.Count() >= i) || redefines(ownedParameters.ElementAt(i - 1)))))
```
# ValidateMultiplicityRangeBounds
### OCL
``` OCL 
if lowerBound = null then
    ownedMember->notEmpty() and
    ownedMember->at(1) = upperBound
else
    ownedMember->size() > 1 and
    ownedMember->at(1) = lowerBound and
    ownedMember->at(2) = upperBound
endif
```
### C#
``` CSharp 
(lowerBound == null ? ownedMember.Any() && ownedMember.ElementAt(0) == upperBound : ownedMember.Count() > 1 && ownedMember.ElementAt(0) == lowerBound && ownedMember.ElementAt(1) == upperBound)
```
# DeriveDefinitionOwnedState
### OCL
``` OCL 
ownedUsage->selectByKind(StateUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<StateUsage>()
```
# ValidateExposeOwningNamespace
### OCL
``` OCL 
importOwningNamespace.oclIsType(ViewUsage)
```
### C#
``` CSharp 
(importOwningNamespace is ViewUsage)
```
# CheckStepOwnedPerformanceSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(Structure) or
 owningType.oclIsKindOf(Feature) and
 owningType.oclAsType(Feature).type->
    exists(oclIsKindOf(Structure)) implies
    specializesFromLibrary('Objects::Object::ownedPerformance')
```
### C#
``` CSharp 
isComposite && owningType != null && ((!((owningType is Structure) || (owningType is Feature) && ((Feature)owningType).type.Any(item => (item is Structure))) || specializesFromLibrary("Objects::Object::ownedPerformance")))
```
# DeriveConjugatedPortTypingPortDefinition
### OCL
``` OCL 
conjugatedPortDefinition.originalPortDefinition
```
### C#
``` CSharp 
conjugatedPortDefinition.originalPortDefinition
```
# ValidateConjugatedPortDefinitionOriginalPortDefinition
### OCL
``` OCL 
ownedPortConjugator.originalPortDefinition = originalPortDefinition
```
### C#
``` CSharp 
ownedPortConjugator.originalPortDefinition == originalPortDefinition
```
# DeriveStateDefinitionState
### OCL
``` OCL 
action->selectByKind(StateUsage)
```
### C#
``` CSharp 
action.OfType<StateUsage>()
```
# ValidateOccurrenceUsagePortionKind
### OCL
``` OCL 
portionKind <> null implies
    owningType <> null and
    (owningType.oclIsKindOf(OccurrenceDefinition) or
     owningType.oclIsKindOf(OccurrenceUsage))
```
### C#
``` CSharp 
(!(portionKind != null) || owningType != null) && ((owningType is OccurrenceDefinition) || (owningType is OccurrenceUsage))
```
# DeriveDefinitionOwnedAllocation
### OCL
``` OCL 
ownedUsage->selectByKind(AllocationUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<AllocationUsage>()
```
# DeriveExpressionResult
### OCL
``` OCL 
    let resultParams : Sequence(Feature) =
        featureMemberships->
            selectByKind(ReturnParameterMembership).
            ownedMemberParameter in
    if resultParams->notEmpty() then resultParams->first()
    else null
    endif
```
### C#
``` CSharp 
featureMemberships.OfType<ReturnParameterMembership>().Select(resultParams => (resultParams.Any() ? resultParams.First() :
null))
```
# CheckFeatureObjectSpecialization
### OCL
``` OCL 
ownedTyping.type->exists(selectByKind(Structure)) implies
    specializesFromLibary('Objects::objects')
```
### C#
``` CSharp 
(!(ownedTyping.type.Any(item => item.kselectByKind(Structure))) || specializesFromLibary("Objects::objects"))
```
# ValidateInvocationExpressionParameterRedefinition
### OCL
``` OCL 
let parameters : OrderedSet(Feature) = instantiatedType.input in
input->forAll(inp | 
    inp.ownedRedefinition.redefinedFeature->
        intersection(parameters)->size() = 1)
```
### C#
``` CSharp 
(instantiatedType.input).Select(parameters => input.All(inp => inp.ownedRedefinition.redefinedFeature.Intersect(parameters).Count() == 1))
```
# ValidateEventOccurrenceUsageIsReference
### OCL
``` OCL 
isReference
```
### C#
``` CSharp 
isReference
```
# DeriveITestionUsageThenAction
### OCL
``` OCL 
    let parameter : Feature = inputParameter(2) in
    if parameter <> null and parameter.oclIsKindOf(ActionUsage) then
        parameter.oclAsType(ActionUsage)
    else
        null
    endif
```
### C#
``` CSharp 
inputParameter(2).Select(parameter => (parameter != null && (parameter is ActionUsage) ? ((ActionUsage)parameter) : null))
```
# CheckPerformActionUsageSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(PartDefinition) or
 owningType.oclIsKindOf(PartUsage)) implies
    specializesFromLibrary('Parts::Part::performedActions')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is PartDefinition) || (owningType is PartUsage))) || specializesFromLibrary("Parts::Part::performedActions"))
```
# DeriveTypeOwnedSpecialization
### OCL
``` OCL 
ownedRelationship->selectByKind(Specialization)->
    select(s | s.special = self)
```
### C#
``` CSharp 
ownedRelationship.OfType<Specialization>().Where(s => s.special == this)
```
# DeriveDefinitionOwnedViewpoint
### OCL
``` OCL 
ownedUsage->selectByKind(ViewpointUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ViewpointUsage>()
```
# ValidateTransitionUsageParameters
### OCL
``` OCL 
if triggerAction->isEmpty() then
    inputParameters()->size() >= 1
else
    inputParameters()->size() >= 2
endif
```
### C#
``` CSharp 
(!triggerAction.Any() ? inputParameters().Count() >= 1 : inputParameters().Count() >= 2)
```
# ValidateFunctionResultParameterMembership
### OCL
``` OCL 
featureMembership->
    selectByKind(ReturnParameterMembership)->
    size() = 1
```
### C#
``` CSharp 
featureMembership.OfType<ReturnParameterMembership>().Count() == 1
```
# CheckControlNodeSpecialization
### OCL
``` OCL 
specializesFromLibrary('Action::Action::controls')
```
### C#
``` CSharp 
specializesFromLibrary("Action::Action::controls")
```
# DeriveFeatureOwnedSubsetting
### OCL
``` OCL 
ownedSpecialization->selectByKind(Subsetting)
```
### C#
``` CSharp 
ownedSpecialization.OfType<Subsetting>()
```
# DeriveUsageNestedRendering
### OCL
``` OCL 
nestedUsage->selectByKind(RenderingUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<RenderingUsage>()
```
# ValidateForLoopActionUsageParameters
### OCL
``` OCL 
inputParameters()->size() = 2
```
### C#
``` CSharp 
inputParameters().Count() == 2
```
# DeriveUseCaseDefinitionIncludedUseCase
### OCL
``` OCL 
ownedUseCase->
    selectByKind(IncludeUseCaseUsage).
    useCaseIncluded
```
### C#
``` CSharp 
ownedUseCase.OfType<IncludeUseCaseUsage>()
```
# DeriveMetadataAccessExpressionReferencdElement
### OCL
``` OCL 
    let elements : Sequence(Element) = ownedMembership->
        reject(oclIsKindOf(FeatureMembership)).memberElement in
    if elements->isEmpty() then null
    else elements->first()
    endif
```
### C#
``` CSharp 
// TODO
(ownedMembership.Where(item => !(item is FeatureMembership).memberElement).Select(elements => (!elements.Any() ? null :
elements.First()))
```
# CheckUsageVariationUsageTypeFeaturing
### OCL
``` OCL 
owningVariationUsage <> null implies
    featuringType->asSet() = owningVariationUsage.featuringType->asSet()
```
### C#
``` CSharp 
(!(owningVariationUsage != null) || featuringType.ToHashSet() == owningVariationUsage.featuringType.ToHashSet())
```
# DeriveClassifierOwnedSubclassification
### OCL
``` OCL 
    ownedSpecialization->selectByKind(Subclassification)
```
### C#
``` CSharp 
ownedSpecialization.OfType<Subclassification>()
```
# ValidateMetadataFeatureMetaclassNotAbstract
### OCL
``` OCL 
not metaclass.isAbstract
```
### C#
``` CSharp 
!metaclass.isAbstract
```
# CheckFeatureValueBindingConnector
### OCL
``` OCL 
not isDefault implies
    featureWithValue.ownedMember->
        selectByKind(BindingConnector)->exists(b |
            b.relatedFeature->includes(featureWithValue) and
            b.relatedFeature->exists(f | 
                f.chainingFeature = Sequence{value, value.result}) and
            if not isInitial then 
                b.featuringType = featureWithValue.featuringType
            else 
                b.featuringType->exists(t |
                    t.oclIsKindOf(Feature) and
                    t.oclAsType(Feature).chainingFeature =
                        Sequence{
                            resolveGlobal('Base::things::that').
                                memberElement,
                            resolveGlobal('Occurrences::Occurrence::startShot').
                                memberElement
                        }
                )
            endif)
```
### C#
``` CSharp 
(!(!isDefault) || featureWithValue.ownedMember.OfType<BindingConnector>().Any(b => b.relatedFeature.Contains(featureWithValue) && b.relatedFeature.Any(f => f.chainingFeature == new List<dynamic> { value, value.result }) && ifisInitial).b.featuringType == featureWithValue.featuringType)
```
# CheckEventOccurrenceUsageSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(OccurrenceDefinition) or
 owningType.oclIsKindOf(OccurrenceUsage)) implies
    specializesFromLibrary('Occurrences::Occurrence::timeEnclosedOccurrences')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is OccurrenceDefinition) || (owningType is OccurrenceUsage))) || specializesFromLibrary("Occurrences::Occurrence::timeEnclosedOccurrences"))
```
# ValidateTriggerInvocationExpressionWhenArgument
### OCL
``` OCL 
TriggerKind::when implies
    argument->notEmpty() and
    argument->at(1).oclIsKindOf(FeatureReferenceExpression) and
    let referent : Feature = 
        argument->at(1).oclAsType(FeatureReferenceExpression).referent in
    referent.oclIsKindOf(Expression) and
    referent.oclAsType(Expression).result.specializesFromLibrary('ScalarValues::Boolean')
```
### C#
``` CSharp 
// TODO
(!(TriggerKind.when) || argument.Any()) && (argument.ElementAt(0) is FeatureReferenceExpression) && referent is Feature == ((FeatureReferenceExpression)argument.ElementAt(1 - 1)).referent
```
# ValidateFeatureValueIsInitial
### OCL
``` OCL 
isInitial implies featureWithValue.isVariable
```
### C#
``` CSharp 
(!(isInitial) || featureWithValue.isVariable)
```
# DeriveFlowPayloadFeature
### OCL
``` OCL 
    let payloadFeatures : Sequence(PayloadFeature) =
        ownedFeature->selectByKind(PayloadFeature) in
    if payloadFeatures->isEmpty() then null
    else payloadFeatures->first()
    endif
```
### C#
``` CSharp 
ownedFeature.OfType<PayloadFeature>().Select(payloadFeatures => (!payloadFeatures.Any() ? null : payloadFeatures.First()))
```
# DerivePartUsagePartDefinition
### OCL
``` OCL 
itemDefinition->selectByKind(PartDefinition)
```
### C#
``` CSharp 
itemDefinition.OfType<PartDefinition>()
```
# CheckInterfaceDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Interfaces::Interface')
```
### C#
``` CSharp 
specializesFromLibrary("Interfaces::Interface")
```
# CheckPartUsageActorSpecialization
### OCL
``` OCL 
owningFeatureMembership <> null and
owningFeatureMembership.oclIsKindOf(ActorMembership) implies
    if owningType.oclIsKindOf(RequirementDefinition) or 
       owningType.oclIsKindOf(RequirementUsage)
    then specializesFromLibrary('Requirements::RequirementCheck::actors')
    else specializesFromLibrary('Cases::Case::actors')
```
### C#
``` CSharp 
(!(owningFeatureMembership != null && (owningFeatureMembership is ActorMembership)) || (owningType is RequirementDefinition)) || (owningType is RequirementUsage)
```
# CheckMergeNodeIncomingSuccessionSpecialization
### OCL
``` OCL 
targetConnector->selectByKind(Succession)->
    forAll(subsetsChain(self, 
        resolveGlobal('ControlPerformances::MergePerformance::incomingHBLink')))
```
### C#
``` CSharp 
targetConnector.OfType<Succession>().All(item => item.subsetsChain(this, resolveGlobal("ControlPerformances::MergePerformance::incomingHBLink")))
```
# ValidateInvocationExpressionNoDuplicateParameterRedefinition
### OCL
``` OCL 
let features : OrderedSet(Feature) = instantiatedType.feature in
input->forAll(inp1 | input->forAll(inp2 |
    inp1 <> inp2 implies
        inp1.ownedRedefinition.redefinedFeature->
            intersection(inp2.ownedRedefinition.redefinedFeature)->
            intersection(features)->isEmpty()))
```
### C#
``` CSharp 
(instantiatedType.feature).Select(features => input.All(inp1 => input.All(inp2 => (!(inp1 != inp2) || !inp1.ownedRedefinition.redefinedFeature.Intersect(inp2.ownedRedefinition.redefinedFeature).Intersect(features).Any()))))
```
# DeriveTransitionUsageSuccession
### OCL
``` OCL 
ownedMember->selectByKind(Succession)->at(1)
```
### C#
``` CSharp 
ownedMember.OfType<Succession>().ElementAt(0)
```
# DeriveDefinitionOwnedMetadata
### OCL
``` OCL 
ownedUsage->selectByKind(MetadataUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<MetadataUsage>()
```
# DeriveRequirementUsageStakeholderParameter
### OCL
``` OCL 
featureMembership->
    selectByKind(AStakholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
featureMembership.OfType<AStakholderMembership>()
```
# CheckPortUsageOwnedPortSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(PartDefinition) or
 owningType.oclIsKindOf(PartUsage)) implies
    specializesFromLibrary('Parts::Part::ownedPorts')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is PartDefinition) || (owningType is PartUsage))) || specializesFromLibrary("Parts::Part::ownedPorts"))
```
# DeriveConnectorTargetFeature
### OCL
``` OCL 
    if relatedFeature->size() < 2 then OrderedSet{}
    else 
        relatedFeature->
            subSequence(2, relatedFeature->size())->
            asOrderedSet()
    endif
```
### C#
``` CSharp 
(relatedFeature.Count() < 2 ? new List<dynamic> {  } : relatedFeature.subSequence(2,
relatedFeature.Count()).subSequence.Distinct().ToHashSet())
```
# DeriveUsageNestedRequirement
### OCL
``` OCL 
nestedUsage->selectByKind(RequirementUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<RequirementUsage>()
```
# ValidateMergeNodeIncomingSuccessions
### OCL
``` OCL 
targetConnector->selectByKind(Succession)->
    collect(connectorEnd->at(1))->
    forAll(sourceMult |
        multiplicityHasBounds(sourceMult, 0, 1))
```
### C#
``` CSharp 
// TODO
targetConnector.OfType<Succession>().Select(item => item.connectorEnd.ElementAt(0)).All(sourceMult => multiplicityHasBounds(sourceMult, 0, 1))
```
# CheckCaseUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Cases::cases')
```
### C#
``` CSharp 
specializesFromLibrary("Cases::cases")
```
# CheckLiteralExpressionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::literalEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::literalEvaluations")
```
# CheckPartUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Parts::parts')
```
### C#
``` CSharp 
specializesFromLibrary("Parts::parts")
```
# DeriveStateDefinitionEntryAction
### OCL
``` OCL 
    let entryMemberships : Sequence(StateSubactionMembership) =
        ownedMembership->
            selectByKind(StateSubactionMembership)->
            select(kind = StateSubactionKind::entry) in
    if entryMemberships->isEmpty() then null
    else entryMemberships->at(1)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.OfType<StateSubactionMembership>().Where(item => item.kind == StateSubactionKind.entry).Select(entryMemberships =>
(!entryMemberships.Any() ? null : entryMemberships.ElementAt(0)))
```
# DeriveDefinitionOwnedInterface
### OCL
``` OCL 
ownedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ReferenceUsage>()
```
# DeriveDefinitionOwnedVerificationCase
### OCL
``` OCL 
ownedUsage->selectByKind(VerificationCaseUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<VerificationCaseUsage>()
```
# DeriveViewDefinitionSatisfiedViewpoint
### OCL
``` OCL 
ownedRequirement->
    selectByKind(ViewpointUsage)->
    select(isComposite)
```
### C#
``` CSharp 
ownedRequirement.OfType<ViewpointUsage>().Where(item => item.isComposite)
```
# CheckRequirementUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Requirements::requirementChecks')
```
### C#
``` CSharp 
specializesFromLibrary("Requirements::requirementChecks")
```
# DeriveConnectorDefaultFeaturingType
### OCL
``` OCL 
let commonFeaturingTypes : OrderedSet(Type) = 
    relatedFeature->closure(featuringType)->select(t | 
        relatedFeature->forAll(f | f.isFeaturedWithin(t))
    ) in
let nearestCommonFeaturingTypes : OrderedSet(Type) =
    commonFeaturingTypes->reject(t1 | 
        commonFeaturingTypes->exists(t2 | 
            t2 <> t1 and t2->closure(featuringType)->contains(t1)
    )) in
if nearestCommonFeaturingTypes->isEmpty() then null
else nearestCommonFeaturingTypes->first()
endif
```
### C#
``` CSharp 
/* closure */ relatedFeature.Where(t => relatedFeature.All(f => f.isFeaturedWithin(t))).Select(commonFeaturingTypes => commonFeaturingTypes.Where(t1 => !(commonFeaturingTypes.Any(t2 => t2 != t1 && /* closure */ t2.contains(t1).contains))).Select(nearestCommonFeaturingTypes => (!nearestCommonFeaturingTypes.Any() ? null : nearestCommonFeaturingTypes.First())))
```
# CheckFeatureOwnedCrossFeatureRedefinitionSpecialization
### OCL
``` OCL 
isOwnedCrossFeature() implies
    ownedSubsetting.subsettedFeature->includesAll(
        owner.oclAsType(Feature).ownedRedefinition.redefinedFeature->
            select(crossFeature <> null).crossFeature)
```
### C#
``` CSharp 
(!(isOwnedCrossFeature()) || ownedSubsetting.subsettedFeature.IsSupersetOf(((Feature)owner).ownedRedefinition.redefinedFeature.Where(item => item.crossFeature != null).crossFeature))
```
# DeriveFeatureOwnedTyping
### OCL
``` OCL 
ownedGeneralization->selectByKind(FeatureTyping)
```
### C#
``` CSharp 
ownedGeneralization.OfType<FeatureTyping>()
```
# CheckFlowDefinitionBinarySpecialization
### OCL
``` OCL 
flowEnd->size() = 2 implies
    specializesFromLibrary('Flows::Message')
```
### C#
``` CSharp 
(!(flowEnd.Count() == 2) || specializesFromLibrary("Flows::Message"))
```
# ValidateSendActionParameters
### OCL
``` OCL 
inputParameters()->size() >= 3
```
### C#
``` CSharp 
inputParameters().Count() >= 3
```
# DeriveDefinitionOwnedConcern
### OCL
``` OCL 
ownedUsage->selectByKind(ConcernUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ConcernUsage>()
```
# CheckFeatureReferenceExpressionResultSpecialization
### OCL
``` OCL 
result.owningType() = self and result.specializes(referent)
```
### C#
``` CSharp 
result.owningType() == this && result.specializes(referent)
```
# ValidateRedefinitionEndConformance
### OCL
``` OCL 
redefinedFeature.isEnd implies redefiningFeature.isEnd
```
### C#
``` CSharp 
(!(redefinedFeature.isEnd) || redefiningFeature.isEnd)
```
# DeriveUsageNestedConnection
### OCL
``` OCL 
nestedUsage->selectByKind(ConnectorAsUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ConnectorAsUsage>()
```
# CheckFunctionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::Evaluation')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::Evaluation")
```
# ValidateElementFilterMembershipConditionIsModelLevelEvaluable
### OCL
``` OCL 
condition.isModelLevelEvaluable
```
### C#
``` CSharp 
condition.isModelLevelEvaluable
```
# DeriveUseCaseUsageIncludedUseCase
### OCL
``` OCL 
ownedUseCase->
    selectByKind(IncludeUseCaseUsage).
    useCaseIncluded
```
### C#
``` CSharp 
ownedUseCase.OfType<IncludeUseCaseUsage>()
```
# CheckLiteralRationalSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::literalRationalEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::literalRationalEvaluations")
```
# CheckCalculationUsageSubcalculationSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(CalculationDefinition) or
 owningType.oclIsKindOf(CalculationUsage)) implies
    specializesFromLibrary('Calculations::Calculation::subcalculations')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is CalculationDefinition) || (owningType is CalculationUsage))) || specializesFromLibrary("Calculations::Calculation::subcalculations"))
```
# CheckMetadataDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Metadata::MetadataItem')
```
### C#
``` CSharp 
specializesFromLibrary("Metadata::MetadataItem")
```
# ValidateDecisionNodeOutgoingSuccessions
### OCL
``` OCL 
sourceConnector->selectAsKind(Succession)->
    collect(connectorEnd->at(2))->
    forAll(targetMult |
        multiplicityHasBounds(targetMult, 0, 1))
```
### C#
``` CSharp 
// TODO
sourceConnector.selectAsKind(Succession).selectAsKind.Select(item => item.connectorEnd.ElementAt(1)).All(targetMult => multiplicityHasBounds(targetMult, 0, 1)).
```
# CheckRenderingUsageSubrenderingSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(RenderingDefinition) or
 owningType.oclIsKindOf(RenderingUsage)) implies
    specializesFromLibrary('Views::Rendering::subrenderings')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is RenderingDefinition) || (owningType is RenderingUsage))) || specializesFromLibrary("Views::Rendering::subrenderings"))
```
# CheckFeatureFlowFeatureRedefinition
### OCL
``` OCL 
owningType <> null and
owningType.oclIsKindOf(FlowEnd) and
owningType.ownedFeature->at(1) = self implies
    let flowType : Type = owningType.owningType in
    flowType <> null implies
        let i : Integer = 
            flowType.ownedFeature.indexOf(owningType) in
        (i = 1 implies 
            redefinesFromLibrary('Transfers::Transfer::source::sourceOutput')) and
        (i = 2 implies
            redefinesFromLibrary('Transfers::Transfer::source::targetInput'))
```
### C#
``` CSharp 
(!(owningType != null && (owningType is FlowEnd) && owningType.ownedFeature.ElementAt(0) == this) || flowType is Type == owningType.owningType)
```
# DeriveUsageNestedViewpoint
### OCL
``` OCL 
nestedUsage->selectByKind(ViewpointUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ViewpointUsage>()
```
# ValidateElementFilterMembershipConditionIsBoolean
### OCL
``` OCL 
condition.result.specializesFromLibrary('ScalarValues::Boolean')
```
### C#
``` CSharp 
condition.result.specializesFromLibrary("ScalarValues::Boolean")
```
# CheckPortDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Ports::Port')
```
### C#
``` CSharp 
specializesFromLibrary("Ports::Port")
```
# ValidateConnectionDefinitionIsSufficient
### OCL
``` OCL 
isSufficient
```
### C#
``` CSharp 
isSufficient
```
# DeriveElementIsLibraryElement
### OCL
``` OCL 
libraryNamespace() <> null
```
### C#
``` CSharp 
libraryNamespace() != null
```
# DeriveFlowTargetInputFeature
### OCL
``` OCL 
    if connectorEnd->size() < 2 or 
        connectorEnd->at(2).ownedFeature->isEmpty()
    then null
    else connectorEnd->at(2).ownedFeature->first()
    endif
```
### C#
``` CSharp 
(connectorEnd.Count() < 2 || !connectorEnd.ElementAt(1).Any() ? null : connectorEnd.ElementAt(1).First())
```
# ValidateRequirementDefinitionOnlyOneSubject
### OCL
``` OCL 
featureMembership->	
    selectByKind(SubjectMembership)->
    size() <= 1
```
### C#
``` CSharp 
featureMembership.OfType<SubjectMembership>().Count() <= 1
```
# ValidateDataTypeSpecialization
### OCL
``` OCL 
ownedSpecialization.general->
    forAll(not oclIsKindOf(Class) and 
           not oclIsKindOf(Association))
```
### C#
``` CSharp 
ownedSpecialization.general.All(item => !(item is Class) && !(item is Association))
```
# ValidateFeatureChainingFeatureConformance
### OCL
``` OCL 
Sequence{2..chainingFeature->size()}->forAll(i |
    chainingFeature->at(i).isFeaturedWithin(chainingFeature->at(i-1)))
```
### C#
``` CSharp 
new List<dynamic> { 2 /* .. */ chainingFeature.Count() }.All(i => chainingFeature.ElementAt(i - 1).isFeaturedWithin(chainingFeature.ElementAt(i - 1 - 1)))
```
# DeriveDefinitionOwnedUseCase
### OCL
``` OCL 
ownedUsage->selectByKind(UseCaseUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<UseCaseUsage>()
```
# ValidateCollectExpressionOperator
### OCL
``` OCL 
'collect'
```
### C#
``` CSharp 
"collect"
```
# CheckFeatureChainExpressionTargetRedefinition
### OCL
``` OCL 
let sourceParameter : Feature = sourceTargetFeature() in
sourceTargetFeature <> null and
sourceTargetFeature.redefinesFromLibrary('ControlFunctions::\'.\'::source::target')
```
### C#
``` CSharp 
sourceTargetFeature().Select(sourceParameter => sourceTargetFeature != null && sourceTargetFeature.redefinesFromLibrary("ControlFunctions::\'.\'::source::target"))
```
# DeriveDefinitionOwnedPart
### OCL
``` OCL 
ownedUsage->selectByKind(PartUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<PartUsage>()
```
# DeriveDefinitionVariant
### OCL
``` OCL 
variantMembership.ownedVariantUsage
```
### C#
``` CSharp 
variantMembership.ownedVariantUsage
```
# ValidateReturnParameterMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(Function) or owningType.oclIsKindOf(Expression)
```
### C#
``` CSharp 
(owningType is ) || (owningType is Expression)
```
# CheckAssignmentActionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::assignments')
```
### C#
``` CSharp 
(!(isSubactionUsage()) || specializesFromLibrary("Actions::Action::assignments"))
```
# DeriveStateUsageExitAction
### OCL
``` OCL 
    let exitMemberships : Sequence(StateSubactionMembership) =
        ownedMembership->
            selectByKind(StateSubactionMembership)->
            select(kind = StateSubactionKind::exit) in
    if exitMemberships->isEmpty() then null
    else exitMemberships->at(1)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.OfType<StateSubactionMembership>().Where(item => item.kind == StateSubactionKind.exit).Select(exitMemberships =>
(!exitMemberships.Any() ? null : exitMemberships.ElementAt(0)))
```
# ValidateUsageVariationIsAbstract
### OCL
``` OCL 
isVariation implies isAbstract
```
### C#
``` CSharp 
(!(isVariation) || isAbstract)
```
# ValidateResultExpressionMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(Function) or owningType.oclIsKindOf(Expression)
```
### C#
``` CSharp 
(owningType is ) || (owningType is Expression)
```
# CheckAcceptActionUsageTriggerActionSpecialization
### OCL
``` OCL 
isTriggerAction() implies
    specializesFromLibrary('Actions::TransitionAction::accepter')
```
### C#
``` CSharp 
(!(isTriggerAction()) || specializesFromLibrary("Actions::TransitionAction::accepter"))
```
# DeriveMultiplicityRangeLowerBound
### OCL
``` OCL 
    let ownedExpressions : Sequence(Expression) =
        ownedMember->selectByKind(Expression) in
    if ownedExpressions->size() < 2 then null
    else ownedExpressions->first()
    endif
```
### C#
``` CSharp 
ownedMember.OfType<Expression>().Select(ownedExpressions => (ownedExpressions.Count() < 2 ? null :
ownedExpressions.First()))
```
# DeriveDefinitionOwnedOccurrence
### OCL
``` OCL 
ownedUsage->selectByKind(OccurrenceUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<OccurrenceUsage>()
```
# ValidateIndexExpressionOperator
### OCL
``` OCL 
'#'
```
### C#
``` CSharp 
"#"
```
# DeriveAssignmentActionUsageReferent
### OCL
``` OCL 
    let unownedFeatures : Sequence(Feature) = ownedMembership->
        reject(oclIsKindOf(FeatureMembership)).memberElement->
        selectByKind(Feature) in
    if unownedFeatures->isEmpty() then null
    else unownedFeatures->first().oclAsType(Feature)
    endif
```
### C#
``` CSharp 
ownedMembership.Where(item => !((item is FeatureMembership))).memberElement.OfType<Feature>().Select(unownedFeatures =>
(!unownedFeatures.Any() ? null : ((Feature)unownedFeatures.First())))
```
# CheckExpressionTypeFeaturing
### OCL
``` OCL 
owningMembership <> null and 
owningMembership.oclIsKindOf(FeatureValue) implies
    let featureWithValue : Feature = 
        owningMembership.oclAsType(FeatureValue).featureWithValue in
    featuringType = featureWithValue.featuringType
```
### C#
``` CSharp 
(!(owningMembership != null && (owningMembership is FeatureValue)) || featureWithValue is Feature == ((FeatureValue)owningMembership).featureWithValue)
```
# DeriveDefinitionOwnedEnumeration
### OCL
``` OCL 
ownedUsage->selectByKind(EnumerationUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<EnumerationUsage>()
```
# DeriveRequirementUsageActorParameter
### OCL
``` OCL 
featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
featureMembership.OfType<ActorMembership>()
```
# ValidateDefinitionVariationIsAbstract
### OCL
``` OCL 
isVariation implies isAbstract
```
### C#
``` CSharp 
(!(isVariation) || isAbstract)
```
# CheckTransitionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::transitionActions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::transitionActions")
```
# DeriveAnalysisCaseUsageResultExpression
### OCL
``` OCL 
    let results : OrderedSet(ResultExpressionMembership) =
        featureMembersip->
            selectByKind(ResultExpressionMembership) in
    if results->isEmpty() then null
    else results->first().ownedResultExpression
    endif
```
### C#
``` CSharp 
featureMembersip.OfType<ResultExpressionMembership>().Select(results => (!results.Any() ? null :
results.First().ownedResultExpression))
```
# DeriveRequirementUsageAssumedConstraint
### OCL
``` OCL 
ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::assumption).
    ownedConstraint
```
### C#
``` CSharp 
ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => item.kind == RequirementConstraintKind.assumption).ownedConstraint
```
# ValidateMetadataFeatureBody
### OCL
``` OCL 
ownedFeature->closure(ownedFeature)->forAll(f |
    f.declaredName = null and f.declaredShortName = null and
    f.valuation <> null implies f.valuation.value.isModelLevelEvaluable and
    f.redefinition.redefinedFeature->size() = 1)
```
### C#
``` CSharp 
/* closure */ ownedFeature.All(f => (!(f.declaredName == null && f.declaredShortName == null && f.valuation != null) || f.valuation.value.isModelLevelEvaluable) && f.redefinition.redefinedFeature.Count() == 1)
```
# CheckFeatureEndSpecialization
### OCL
``` OCL 
isEnd and owningType <> null and
(owningType.oclIsKindOf(Association) or
 owningType.oclIsKindOf(Connector)) implies
    specializesFromLibrary('Links::Link::participant')
```
### C#
``` CSharp 
(!(isEnd && owningType != null && ((owningType is Association) || (owningType is Connector))) || specializesFromLibrary("Links::Link::participant"))
```
# DeriveMultiplicityRangeUpperBound
### OCL
``` OCL 
    let ownedExpressions : Sequence(Expression) =
        ownedMember->selectByKind(Expression) in
    if ownedExpressions->isEmpty() then null
    else if ownedExpressions->size() = 1 then ownedExpressions->at(1)
    else ownedExpressions->at(2)
    endif endif
```
### C#
``` CSharp 
ownedMember.OfType<Expression>().Select(ownedExpressions => (!ownedExpressions.Any() ? null : (ownedExpressions.Count() == 1
? ownedExpressions.ElementAt(0) : ownedExpressions.ElementAt(1))))
```
# DeriveStateUsageEntryAction
### OCL
``` OCL 
    let entryMemberships : Sequence(StateSubactionMembership) =
        ownedMembership->
            selectByKind(StateSubactionMembership)->
            select(kind = StateSubactionKind::entry) in
    if entryMemberships->isEmpty() then null
    else entryMemberships->at(1)
    endif
```
### C#
``` CSharp 
// TODO
ownedMembership.OfType<StateSubactionMembership>().Where(item => item.kind == StateSubactionKind.entry).Select(entryMemberships =>
(!entryMemberships.Any() ? null : entryMemberships.ElementAt(0)))
```
# CheckDataTypeSpecialization
### OCL
``` OCL 
specializesFromLibrary('Base::DataValue')
```
### C#
``` CSharp 
specializesFromLibrary("Base::DataValue")
```
# CheckViewUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Views::views')
```
### C#
``` CSharp 
specializesFromLibrary("Views::views")
```
# DeriveMembershipImportImportedElement
### OCL
``` OCL 
importedMembership.memberElement
```
### C#
``` CSharp 
importedMembership.memberElement
```
# ValidateFeatureEndMultiplicity
### OCL
``` OCL 
isEnd implies 
    multiplicities().allSuperTypes()->flatten()->
    selectByKind(MultiplicityRange)->exists(hasBounds(1,1))
```
### C#
``` CSharp 
(!(isEnd) || multiplicities().allSuperTypes().flatten().flatten.OfType<MultiplicityRange>().Any(item => item.hasBounds(1, 1)))
```
# ValidateRedefinitionFeaturingTypes
### OCL
``` OCL 
let anythingType: Type =
    redefiningFeature.resolveGlobal('Base::Anything').modelElement.oclAsType(Type) in 
-- Including "Anything" accounts for implicit featuringType of Features
-- with no explicit featuringType.
let redefiningFeaturingTypes: Set(Type) =
    if redefiningFeature.isVariable then Set{redefiningFeature.owningType}
    else redefiningFeature.featuringTypes->asSet()->including(anythingType) 
    endif in
let redefinedFeaturingTypes: Set(Type) =
    if redefinedFeature.isVariable then Set{redefinedFeature.owningType}
    else redefinedFeature.featuringTypes->asSet()->including(anythingType)
    endif in
redefiningFeaturingTypes <> redefinedFeaturingType
```
### C#
``` CSharp 
// TODO
((Type)redefiningFeature.resolveGlobal("Base::Anything").modelElement).Select(anythingType => (redefiningFeature.isVariable
? new HashSet<dynamic> { redefiningFeature.owningType } :
redefiningFeature.featuringTypes.ToHashSet().Append(anythingType)).Select(redefiningFeaturingTypes =>
(redefinedFeature.isVariable ? new HashSet<dynamic> { redefinedFeature.owningType } :
redefinedFeature.featuringTypes.ToHashSet().Append(anythingType)).Select(redefinedFeaturingTypes => redefiningFeaturingTypes
!= redefinedFeaturingType)))
```
# ValidateFeatureEndNotDerivedAbstractCompositeOrPortion
### OCL
``` OCL 
isEnd implies not (isDerived or isAbstract or isComposite or isPortion)
```
### C#
``` CSharp 
(!(isEnd) || !(isDerived || isAbstract || isComposite || isPortion))
```
# ValidateCrossSubsettingCrossedFeature
### OCL
``` OCL 
crossingFeature.isEnd and crossingFeature.owningType <> null implies
    let endFeatures: Sequence(Feature) = crossingFeature.owningType.endFeature in
    let chainingFeatures: Sequence(Feature) = crossedFeature.chainingFeature in
    chainingFeatures->size() = 2 and
    endFeatures->size() = 2 implies 
        chainingFeatures->at(1) = endFeatures->excluding(crossingFeature)->at(1)
```
### C#
``` CSharp 
(!(crossingFeature.isEnd && crossingFeature.owningType != null) || endFeatures is (Feature) == crossingFeature.owningType.endFeature)
```
# CheckIndexExpressionResultSpecialization
### OCL
``` OCL 
arguments->notEmpty() and 
not arguments->first().result.specializesFromLibrary('Collections::Array') implies
    result.specializes(arguments->first().result)
```
### C#
``` CSharp 
(!(arguments.Any() && !arguments.First().result.specializesFromLibrary("Collections::Array")) || result.specializes(arguments.First().result))
```
# DeriveMultiplicityRangeBound
### OCL
``` OCL 
    if upperBound = null then Sequence{}
    else if lowerBound = null then Sequence{upperBound}
    else Sequence{lowerBound, upperBound}
    endif endif
```
### C#
``` CSharp 
(upperBound == null ? new List<dynamic> {  } : (lowerBound == null ? new List<dynamic> { upperBound } : new List<dynamic> {
lowerBound, upperBound }))
```
# DeriveEventOccurrenceUsageEventOccurrence
### OCL
``` OCL 
    if referencedFeatureTarget() = null then self
    else if referencedFeatureTarget().oclIsKindOf(OccurrenceUsage) then
        referencedFeatureTarget().oclAsType(OccurrenceUsage)
    else null
    endif endif
```
### C#
``` CSharp 
(referencedFeatureTarget() == null ? this : ((referencedFeatureTarget() is OccurrenceUsage) ?
((OccurrenceUsage)referencedFeatureTarget()) : null))
```
# DeriveViewpointDefinitionViewpointStakeholder
### OCL
``` OCL 
framedConcern.featureMemberhsip->
    selectByKind(StakeholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
framedConcern.featureMemberhsip.OfType<StakeholderMembership>()
```
# CheckViewDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Views::View')
```
### C#
``` CSharp 
specializesFromLibrary("Views::View")
```
# CheckAssociationSpecialization
### OCL
``` OCL 
specializesFromLibrary('Links::Link')
```
### C#
``` CSharp 
specializesFromLibrary("Links::Link")
```
# DeriveAcceptActionUsageReceiverArgument
### OCL
``` OCL 
argument(2)
```
### C#
``` CSharp 
argument(2)
```
# ValidateMergeNodeOutgoingSuccessions
### OCL
``` OCL 
sourceConnector->selectAsKind(Succession)->size() <= 1
```
### C#
``` CSharp 
sourceConnector.selectAsKind(Succession).selectAsKind.Count() <= 1
```
# CheckWhileLoopActionUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Actions::whileLoopActions')
```
### C#
``` CSharp 
specializesFromLibrary("Actions::whileLoopActions")
```
# DeriveUsageNestedCase
### OCL
``` OCL 
nestedUsage->selectByKind(CaseUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<CaseUsage>()
```
# CheckExpressionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::evaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::evaluations")
```
# DeriveRequirementDefinitionText
### OCL
``` OCL 
documentation.body
```
### C#
``` CSharp 
documentation.body
```
# ValidateExposeIsImportAll
### OCL
``` OCL 
isImportAll
```
### C#
``` CSharp 
isImportAll
```
# CheckPartUsageSubpartSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(ItemDefinition) or
 owningType.oclIsKindOf(ItemUsage)) implies
    specializesFromLibrary('Items::Item::subparts')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is ItemDefinition) || (owningType is ItemUsage))) || specializesFromLibrary("Items::Item::subparts"))
```
# DeriveNamespaceOwnedImport
### OCL
``` OCL 
ownedRelationship->selectByKind(Import)
```
### C#
``` CSharp 
ownedRelationship.OfType<Import>()
```
# ValidateDefinitionVariationSpecialization
### OCL
``` OCL 
isVariation implies
    not ownedSpecialization.specific->exists(
        oclIsKindOf(Definition) and
        oclAsType(Definition).isVariation)
```
### C#
``` CSharp 
(!(isVariation) || !ownedSpecialization.specific.Any(item => (item is Definition) && (Definition).isVariation))
```
# CheckStateUsageOwnedStateSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(PartDefinition) or
 owningType.oclIsKindOf(PartUsage)) implies
    specializesFromLibrary('Parts::Part::ownedStates')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is PartDefinition) || (owningType is PartUsage))) || specializesFromLibrary("Parts::Part::ownedStates"))
```
# CheckUseCaseUsageSubUseCaseSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(UseCaseDefinition) or
 owningType.oclIsKindOf(UseCaseUsage)) implies
    specializesFromLibrary('UseCases::UseCase::subUseCases')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is UseCaseDefinition) || (owningType is UseCaseUsage))) || specializesFromLibrary("UseCases::UseCase::subUseCases"))
```
# CheckViewUsageSubviewSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(ViewDefinition) or
 owningType.oclIsKindOf(ViewUsage)) implies
    specializesFromLibrary('Views::View::subviews')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is ViewDefinition) || (owningType is ViewUsage))) || specializesFromLibrary("Views::View::subviews"))
```
# DeriveDefinitionOwnedRequirement
### OCL
``` OCL 
ownedUsage->selectByKind(RequirementUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<RequirementUsage>()
```
# DeriveCaseUsageActorParameter
### OCL
``` OCL 
featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
featureMembership.OfType<ActorMembership>()
```
# CheckLiteralStringSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::literalStringEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::literalStringEvaluations")
```
# ValidateFeatureMultiplicityDomain
### OCL
``` OCL 
multiplicity <> null implies multiplicity.featuringType = featuringType
```
### C#
``` CSharp 
(!(multiplicity != null) || multiplicity.featuringType == featuringType)
```
# ValidateJoinNodeOutgoingSuccessions
### OCL
``` OCL 
sourceConnector->selectByKind(Succession)->size() <= 1
```
### C#
``` CSharp 
sourceConnector.OfType<Succession>().Count() <= 1
```
# DeriveUsageNestedItem
### OCL
``` OCL 
nestedUsage->selectByKind(ItemUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ItemUsage>()
```
# ValidateViewUsageOnlyOneViewRendering
### OCL
``` OCL 
featureMembership->
    selectByKind(ViewRenderingMembership)->
    size() <= 1
```
### C#
``` CSharp 
featureMembership.OfType<ViewRenderingMembership>().Count() <= 1
```
# DeriveAcceptActionUsagePayloadArgument
### OCL
``` OCL 
argument(1)
```
### C#
``` CSharp 
argument(1)
```
# DeriveElementTextualRepresentation
### OCL
``` OCL 
ownedElement->selectByKind(TextualRepresentation)
```
### C#
``` CSharp 
ownedElement.OfType<TextualRepresentation>()
```
# CheckAcceptActionUsageReceiverBindingConnector
### OCL
``` OCL 
payloadArgument <> null and
payloadArgument.oclIsKindOf(TriggerInvocationExpression) implies
    let invocation : Expression =
        payloadArgument.oclAsType(Expression) in
    parameter->size() >= 2 and
    invocation.parameter->size() >= 2 and        
    ownedFeature->selectByKind(BindingConnector)->exists(b |
        b.relatedFeatures->includes(parameter->at(2)) and
        b.relatedFeatures->includes(invocation.parameter->at(2)))
```
### C#
``` CSharp 
(!(payloadArgument != null && (payloadArgument is TriggerInvocationExpression)) || invocation is Expression == ((Expression)payloadArgument))
```
# CheckFunctionResultBindingConnector
### OCL
``` OCL 
ownedMembership.selectByKind(ResultExpressionMembership)->
    forAll(mem | ownedFeature.selectByKind(BindingConnector)->
        exists(binding |
            binding.relatedFeature->includes(result) and
            binding.relatedFeature->includes(mem.ownedResultExpression.result)))
```
### C#
``` CSharp 
ownedMembership.selectByKind(ResultExpressionMembership).All(mem => ownedFeature.selectByKind(BindingConnector).Any(binding => binding.relatedFeature.Contains(result) && binding.relatedFeature.Contains(mem.ownedResultExpression.result)))
```
# DeriveTypeIntersectingType
### OCL
``` OCL 
ownedIntersecting.intersectingType
```
### C#
``` CSharp 
ownedIntersecting.intersectingType
```
# ValidateExposeVisibility
### OCL
``` OCL 
VisibilityKind::protected
```
### C#
``` CSharp 
VisibilityKind.protected
```
# ValidateMetadataAccessExpressionReferencedElement
### OCL
``` OCL 
ownedMembership->exists(not oclIsKindOf(FeatureMembership))
```
### C#
``` CSharp 
ownedMembership.Any(item => !(item is FeatureMembership))
```
# CheckBindingConnectorSpecialization
### OCL
``` OCL 
specializesFromLibrary('Links::selfLinks')
```
### C#
``` CSharp 
specializesFromLibrary("Links::selfLinks")
```
# DeriveViewDefinitionViewCondition
### OCL
``` OCL 
ownedMembership->
    selectByKind(ElementFilterMembership).
    condition
```
### C#
``` CSharp 
ownedMembership.OfType<ElementFilterMembership>()
```
# DeriveInvocationExpressionArgument
### OCL
``` OCL 
instantiatedType.input->collect(inp | 
    ownedFeatures->select(redefines(inp)).valuation->
    select(v | v <> null).value
)
```
### C#
``` CSharp 
instantiatedType.input.Select(inp => ownedFeatures.Where(item => item.redefines(inp)).valuation.Where(v => v != null).value)
```
# DeriveDefinitionOwnedAction
### OCL
``` OCL 
ownedUsage->selectByKind(ActionUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ActionUsage>()
```
# ValidateFlowDefinitionFlowEnds
### OCL
``` OCL 
flowEnd->size() <= 2
```
### C#
``` CSharp 
flowEnd.Count() <= 2
```
# DeriveMetadataFeatureMetaclass
### OCL
``` OCL 
    let metaclassTypes : Sequence(Type) = type->selectByKind(Metaclass) in
    if metaclassTypes->isEmpty() then null
    else metaClassTypes->first()
    endif
```
### C#
``` CSharp 
type.OfType<Metaclass>().Select(metaclassTypes => (!metaclassTypes.Any() ? null : metaClassTypes.First()))
```
# DeriveTypeOutput
### OCL
``` OCL 
feature->select(f | 
    let direction: FeatureDirectionKind = directionOf(f) in
    direction = FeatureDirectionKind::out or
    direction = FeatureDirectionKind::inout)
```
### C#
``` CSharp 
feature.Where(f => directionOf(f).Select(direction => direction == FeatureDirectionKind.out || direction == FeatureDirectionKind.inout))
```
# CheckMetadataFeatureSemanticSpecialization
### OCL
``` OCL 
isSemantic() implies
    let annotatedTypes : Sequence(Type) = 
        annotatedElement->selectAsKind(Type) in
    let baseTypes : Sequence(MetadataFeature) = 
        evaluateFeature(resolveGlobal(
            'Metaobjects::SemanticMetadata::baseType').
            memberElement.
            oclAsType(Feature))->
        selectAsKind(MetadataFeature) in
    annotatedTypes->notEmpty() and 
    baseTypes()->notEmpty() and 
    baseTypes()->first().isSyntactic() implies
        let annotatedType : Type = annotatedTypes->first() in
        let baseType : Element = baseTypes->first().syntaxElement() in
        if annotatedType.oclIsKindOf(Classifier) and 
            baseType.oclIsKindOf(Feature) then
            baseType.oclAsType(Feature).type->
                forAll(t | annotatedType.specializes(t))
        else if baseType.oclIsKindOf(Type) then
            annotatedType.specializes(baseType.oclAsType(Type))
        else
            true
        endif
```
### C#
``` CSharp 
(!(isSemantic()) || annotatedTypes is (Type) == annotatedElement.selectAsKind(Type).selectAsKind)
```
# CheckItemUsageSubitemSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(ItemDefinition) or
 owningType.oclIsKindOf(ItemUsage)) implies
    specializesFromLibrary('Items::Item::subitem')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is ItemDefinition) || (owningType is ItemUsage))) || specializesFromLibrary("Items::Item::subitem"))
```
# CheckSatisfyRequirementUsageSpecialization
### OCL
``` OCL 
if isNegated then
    specializesFromLibrary('Requirements::notSatisfiedRequirementChecks')
else
    specializesFromLibrary('Requirements::satisfiedRequirementChecks')
endif
```
### C#
``` CSharp 
(isNegated ? specializesFromLibrary("Requirements::notSatisfiedRequirementChecks") : specializesFromLibrary("Requirements::satisfiedRequirementChecks"))
```
# CheckViewpointUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Views::viewpoints')
```
### C#
``` CSharp 
specializesFromLibrary("Views::viewpoints")
```
# ValidateUsageVariationSpecialization
### OCL
``` OCL 
isVariation implies
    not ownedSpecialization.specific->exists(
        oclIsKindOf(Definition) and
        oclAsType(Definition).isVariation or
        oclIsKindOf(Usage) and
        oclAsType(Usage).isVariation)
```
### C#
``` CSharp 
(!(isVariation) || !ownedSpecialization.specific.Any(item => (item.Definition) && (Definition).isVariation || (Usage) && (Usage).isVariation))
```
# CheckFlowDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Flows::MessageAction')
```
### C#
``` CSharp 
specializesFromLibrary("Flows::MessageAction")
```
# DeriveAcceptActionUsagePayloadParameter
### OCL
``` OCL 
 if parameter->isEmpty() then null
 else parameter->first() endif
```
### C#
``` CSharp 
(!parameter.Any() ? null : parameter.First())
```
# ValidateTransitionFeatureMembershipTriggerAction
### OCL
``` OCL 
TransitionFeatureKind::trigger implies
    transitionFeature.oclIsKindOf(AcceptActionUsage)
```
### C#
``` CSharp 
(!(TransitionFeatureKind.trigger) || (transitionFeature is AcceptActionUsage))
```
# DeriveExpressionIsModelLevelEvaluable
### OCL
``` OCL 
modelLevelEvaluable(Set(Element){})
```
### C#
``` CSharp 
modelLevelEvaluable((Element))
```
# CheckCaseDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Cases::Case')
```
### C#
``` CSharp 
specializesFromLibrary("Cases::Case")
```
# ValidateMultiplicityRangeBoundResultTypes
### OCL
``` OCL 
bound->forAll(b |
    b.result.specializesFromLibrary('ScalarValues::Integer') and
    let value : UnlimitedNatural = valueOf(b) in
    value <> null implies value >= 0
)
```
### C#
``` CSharp 
(!(bound.All(b => b.result.specializesFromLibrary("ScalarValues::Integer") && value is UnlimitedNatural == valueOf(b)).value != null) || value >= 0)
```
# CheckWhileLoopActionUsageSubactionSpecialization
### OCL
``` OCL 
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::whileLoops')
```
### C#
``` CSharp 
(!(isSubactionUsage()) || specializesFromLibrary("Actions::Action::whileLoops"))
```
# DeriveSendActionUsageSenderArgument
### OCL
``` OCL 
argument(2)
```
### C#
``` CSharp 
argument(2)
```
# DeriveOccurrenceUsageIndividualDefinition
### OCL
``` OCL 
    let individualDefinitions : OrderedSet(OccurrenceDefinition) = 
        occurrenceDefinition->
            selectByKind(OccurrenceDefinition)->
            select(isIndividual) in
    if individualDefinitions->isEmpty() then null
    else individualDefinitions->first() endif
```
### C#
``` CSharp 
// TODO
occurrenceDefinition.OfType<OccurrenceDefinition>().Where(item => item.isIndividual).Select(individualDefinitions =>
(!individualDefinitions.Any() ? null : individualDefinitions.First()))
```
# ValidateCrossSubsettingCrossingFeature
### OCL
``` OCL 
crossingFeature.isEnd and
crossingFeature.owningType<>null and
crossingFeature.owningType.endFeature ->size() > 1
```
### C#
``` CSharp 
crossingFeature.isEnd && crossingFeature.owningType != null && crossingFeature.owningType.endFeature.Count() > 1
```
# CheckConstraintUsageRequirementConstraintSpecialization
### OCL
``` OCL 
owningFeatureMembership <> null and
owningFeatureMembership.oclIsKindOf(RequirementConstraintMembership) implies
    if owningFeatureMembership.oclAsType(RequirementConstraintMembership).kind = 
        RequirementConstraintKind::assumption then
        specializesFromLibrary('Requirements::RequirementCheck::assumptions')
    else
        specializesFromLibrary('Requirements::RequirementCheck::constraints')
    endif
```
### C#
``` CSharp 
(!(owningFeatureMembership != null && (owningFeatureMembership is RequirementConstraintMembership)) || ((RequirementConstraintMembership)owningFeatureMembership).kind == RequirementConstraintKind.assumption)
```
# CheckConnectorBinaryObjectSpecialization
### OCL
``` OCL 
connectorEnds->size() = 2 and
association->exists(oclIsKindOf(AssociationStructure)) implies
    specializesFromLibrary('Objects::binaryLinkObjects')
```
### C#
``` CSharp 
(!(connectorEnds.Count() == 2 && association.Any(item => (item is AssociationStructure))) || specializesFromLibrary("Objects::binaryLinkObjects"))
```
# DeriveWhileLoopActionUsageUntilArgument
### OCL
``` OCL 
    let parameter : Feature = inputParameter(3) in
    if parameter <> null and parameter.oclIsKindOf(Expression) then
        parameter.oclAsType(Expression)
    else
        null
    endif
```
### C#
``` CSharp 
inputParameter(3).Select(parameter => (parameter != null && (parameter is Expression) ? ((Expression)parameter) : null))
```
# DeriveFlowPayloadType
### OCL
``` OCL 
    if payloadFeature = null then Sequence{}
    else payloadFeature.type
    endif
```
### C#
``` CSharp 
(payloadFeature == null ? new List<dynamic> {  } : payloadFeature.type)
```
# CheckConnectionUsageBinarySpecialization
### OCL
``` OCL 
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Connections::binaryConnections')
```
### C#
``` CSharp 
(!(ownedEndFeature.Count() == 2) || specializesFromLibrary("Connections::binaryConnections"))
```
# CheckInterfaceUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Interfaces::interfaces')
```
### C#
``` CSharp 
specializesFromLibrary("Interfaces::interfaces")
```
# ValidateStateSubactionMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(StateDefinition) or
owningType.oclIsKindOf(StateUsage)
```
### C#
``` CSharp 
(owningType is StateDefinition) || (owningType is StateUsage)
```
# ValidateParameterMembershipParameterDirection
### OCL
``` OCL 
ownedMemberParameter.direction = parameterDirection()
```
### C#
``` CSharp 
ownedMemberParameter.direction == parameterDirection()
```
# CheckFeatureFeatureMembershipTypeFeaturing
### OCL
``` OCL 
owningFeatureMembership <> null implies
    featuringTypes->exists(t | isFeaturingType(t))
```
### C#
``` CSharp 
(!(owningFeatureMembership != null) || featuringTypes.Any(t => isFeaturingType(t)))
```
# DeriveVerificationCaseDefinitionVerifiedRequirement
### OCL
``` OCL 
    if objectiveRequirement = null then OrderedSet{}
    else 
        objectiveRequirement.featureMembership->
            selectByKind(RequirementVerificationMembership).
            verifiedRequirement->asOrderedSet()
    endif
```
### C#
``` CSharp 
(objectiveRequirement == null ? new List<dynamic> {  } : objectiveRequirement.featureMembership.OfType<RequirementVerificationMembership>().Distinct().ToHashSet())
```
# CheckDecisionNodeOutgoingSuccessionSpecialization
### OCL
``` OCL 
sourceConnector->selectByKind(Succession)->
    forAll(subsetsChain(self, 
        resolveGlobal('ControlPerformances::MergePerformance::outgoingHBLink')))
```
### C#
``` CSharp 
sourceConnector.OfType<Succession>().All(item => item.subsetsChain(this, resolveGlobal("ControlPerformances::MergePerformance::outgoingHBLink")))
```
# CheckStepEnclosedPerformanceSpecialization
### OCL
``` OCL 
owningType <> null and
    (owningType.oclIsKindOf(Behavior) or
     owningType.oclIsKindOf(Step)) implies
    specializesFromLibrary('Performances::Performance::enclosedPerformance')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is Behavior) || (owningType is Step))) || specializesFromLibrary("Performances::Performance::enclosedPerformance"))
```
# CheckActionUsageStateActionRedefinition
### OCL
``` OCL 
owningFeatureMembership <> null and
owningFeatureMembership.oclIsKindOf(StateSubactionMembership) implies
    let kind : StateSubactionKind = 
        owningFeatureMembership.oclAsType(StateSubactionMembership).kind in
    if kind = StateSubactionKind::entry then
        redefinesFromLibrary('States::StateAction::entryAction')
    else if kind = StateSubactionKind::do then
        redefinesFromLibrary('States::StateAction::doAction')
    else
        redefinesFromLibrary('States::StateAction::exitAction')
    endif endif
```
### C#
``` CSharp 
(!(owningFeatureMembership != null && (owningFeatureMembership is StateSubactionMembership)) || kind is StateSubactionKind == ((StateSubactionMembership)owningFeatureMembership).kind)
```
# CheckOccurrenceUsageSuboccurrenceSpecialization
### OCL
``` OCL 
isComposite and
owningType <> null and
(owningType.oclIsKindOf(Class) or
 owningType.oclIsKindOf(OccurrenceUsage) or
 owningType.oclIsKindOf(Feature) and
    owningType.oclAsType(Feature).type->
        exists(oclIsKind(Class))) implies
    specializesFromLibrary('Occurrences::Occurrence::suboccurrences')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is Class) || (owningType is OccurrenceUsage) || (owningType is Feature) && ((Feature)owningType).type.Any(item => (item is Class)))) || specializesFromLibrary("Occurrences::Occurrence::suboccurrences"))
```
# DeriveTransitionUsageTriggerAction
### OCL
``` OCL 
ownedFeatureMembership->
    selectByKind(TransitionFeatureMembership)->
    select(kind = TransitionFeatureKind::trigger).transitionFeature->
    selectByKind(AcceptActionUsage)
```
### C#
``` CSharp 
ownedFeatureMembership.OfType<TransitionFeatureMembership>().Where(item => item.kind == TransitionFeatureKind.trigger).transitionFeature.OfType<AcceptActionUsage>()
```
# ValidateAcceptActionUsageParameters
### OCL
``` OCL 
inputParameters()->size() >= 2
```
### C#
``` CSharp 
inputParameters().Count() >= 2
```
# CheckRequirementDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Requirements::RequirementCheck')
```
### C#
``` CSharp 
specializesFromLibrary("Requirements::RequirementCheck")
```
# ValidateFeatureEndIsConstant
### OCL
``` OCL 
isEnd and isVariable implies isConstant
```
### C#
``` CSharp 
(!(isEnd && isVariable) || isConstant)
```
# CheckAttributeUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Base::dataValues')
```
### C#
``` CSharp 
specializesFromLibrary("Base::dataValues")
```
# ValidateConstructorExpressionNoDuplicateFeatureRedefinition
### OCL
``` OCL 
let features : OrderedSet(Feature) = instantiatedType.feature->
    select(visibility = VisibilityKind::public) in
result.ownedFeature->forAll(f1 | result.ownedFeature->forAll(f2 |
    f1 <> f2 implies
        f1.ownedRedefinition.redefinedFeature->
            intersection(f2.ownedRedefinition.redefinedFeature)->
            intersection(features)->isEmpty()))
```
### C#
``` CSharp 
instantiatedType.feature.Where(item => item.visibility == VisibilityKind.public).Select(features => result.ownedFeature.All(f1 => result.ownedFeature.All(f2 => (!(f1 != f2) || !f1.ownedRedefinition.redefinedFeature.Intersect(f2.ownedRedefinition.redefinedFeature).Intersect(features).Any()))))
```
# CheckTransitionUsageSourceBindingConnector
### OCL
``` OCL 
ownedMember->selectByKind(BindingConnector)->exists(b |
    b.relatedFeatures->includes(source) and
    b.relatedFeatures->includes(inputParameter(1)))
```
### C#
``` CSharp 
ownedMember.OfType<BindingConnector>().Any(b => b.relatedFeatures.Contains(source) && b.relatedFeatures.Contains(inputParameter(1)))
```
# CheckFlowUsageSpecialization
### OCL
``` OCL 
specializesFromLibrary('Flows::messages')
```
### C#
``` CSharp 
specializesFromLibrary("Flows::messages")
```
# ValidateStakeholderMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(RequirementUsage) or
owningType.oclIsKindOf(RequirementDefinition)
```
### C#
``` CSharp 
(owningType is RequirementUsage) || (owningType is RequirementDefinition)
```
# DeriveSendActionUsagePayloadArgument
### OCL
``` OCL 
argument(1)
```
### C#
``` CSharp 
argument(1)
```
# ValidateOccurrenceUsageIndividualDefinition
### OCL
``` OCL 
occurrenceDefinition->
    selectByKind(OccurrenceDefinition)->
    select(isIndividual).size() <= 1
```
### C#
``` CSharp 
occurrenceDefinition.OfType<OccurrenceDefinition>().Where(item => item.isIndividual).Length <= 1
```
# DeriveTypeFeatureMembership
### OCL
``` OCL 
ownedFeatureMembership->union(
    inheritedMembership->selectByKind(FeatureMembership))
```
### C#
``` CSharp 
ownedFeatureMembership.Union(inheritedMembership.OfType<FeatureMembership>())
```
# ValidateRedefinitionDirectionConformance
### OCL
``` OCL 
let featuringTypes : Sequence(Type) =
    if redefiningFeature.isVariable then Sequence{redefiningFeature.owningType}
    else redefiningFeature.featuringType
    endif in
featuringTypes->forAll(t |
    let direction : FeatureDirectionKind = t.directionOf(redefinedFeature) in
    ((direction = FeatureDirectionKind::_'in' or 
      direction = FeatureDirectionKind::out) implies
         redefiningFeature.direction = direction)
    and 
    (direction = FeatureDirectionKind::inout implies
        redefiningFeature.direction <> null))
```
### C#
``` CSharp 
(redefiningFeature.isVariable ? new List<dynamic> { redefiningFeature.owningType } : redefiningFeature.featuringType).Select(featuringTypes => featuringTypes.All(t => t.directionOf(redefinedFeature).Select(direction => (!(("in" || direction == FeatureDirectionKind.out)) || redefiningFeature.direction == direction))) && ((!(direction == FeatureDirectionKind.inout) || redefiningFeature.direction != null)))
```
# CheckTypeSpecialization
### OCL
``` OCL 
specializesFromLibrary('Base::Anything')
```
### C#
``` CSharp 
specializesFromLibrary("Base::Anything")
```
# DeriveForLoopActionUsageLoopVariable
### OCL
``` OCL 
    if ownedFeature->isEmpty() or 
        not ownedFeature->first().oclIsKindOf(ReferenceUsage) then 
        null
    else 
        ownedFeature->first().oclAsType(ReferenceUsage)
    endif
```
### C#
``` CSharp 
(!ownedFeature.Any() || !(ownedFeature.First() is ReferenceUsage) ? null : ((ReferenceUsage)ownedFeature.First()))
```
# DeriveDefinitionOwnedReference
### OCL
``` OCL 
ownedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
ownedUsage.OfType<ReferenceUsage>()
```
# ValidateClassifierMultiplicityDomain
### OCL
``` OCL 
multiplicity <> null implies multiplicity.featuringType->isEmpty()
```
### C#
``` CSharp 
(!(multiplicity != null) || !multiplicity.featuringType.Any())
```
# DeriveSatisfyRequirementUsageSatisfyingFeature
### OCL
``` OCL 
    let bindings: BindingConnector = ownedMember->
        selectByKind(BindingConnector)->
        select(b | b.relatedElement->includes(subjectParameter)) in
    if bindings->isEmpty() or 
       bindings->first().relatedElement->exits(r | r <> subjectParameter) 
    then null
    else bindings->first().relatedElement->any(r | r <> subjectParameter)
    endif
```
### C#
``` CSharp 
// TODO
ownedMember.OfType<BindingConnector>().Where(b => b.relatedElement.Contains(subjectParameter)).Select(bindings =>
(!bindings.Any() || bindings.First().relatedElement.exits(r).exits != subjectParameter ? null :
bindings.First().relatedElement.FirstOrDefault(r => r != subjectParameter)))
```
# DeriveRequirementUsageSubjectParameter
### OCL
``` OCL 
    let subjects : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjects->isEmpty() then null
    else subjects->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
featureMembership.OfType<SubjectMembership>().Select(subjects => (!subjects.Any() ? null : subjects.First().ownedSubjectParameter))
```
# ValidateFramedConcernMembershipConstraintKind
### OCL
``` OCL 
RequirementConstraintKind::requirement
```
### C#
``` CSharp 
RequirementConstraintKind.requirement
```
# CheckConnectorTypeFeaturing
### OCL
``` OCL 
relatedFeature->forAll(f | 
    if featuringType->isEmpty() then f.isFeaturedWithin(null)
    else featuringType->forAll(t | f.isFeaturedWithin(t))
    endif)
```
### C#
``` CSharp 
relatedFeature.All(f => (!featuringType.Any() ? f.isFeaturedWithin(null) : featuringType.All(t => f.isFeaturedWithin(t))))
```
# DeriveFlowFlowEnd
### OCL
``` OCL 
connectorEnd->selectByKind(FlowEnd)
```
### C#
``` CSharp 
connectorEnd.OfType<FlowEnd>()
```
# CheckMultiplicityTypeFeaturing
### OCL
``` OCL 
if owningType <> null and owningType.oclIsKindOf(Feature) then
    featuringType = 
        owningType.oclAsType(Feature).featuringType
else
    featuringType->isEmpty()
endif
```
### C#
``` CSharp 
(owningType != null && (owningType is Feature) ? featuringType == ((Feature)owningType).featuringType : !featuringType.Any())
```
# CheckConstraintUsageCheckedConstraintSpecialization
### OCL
``` OCL 
owningType <> null and
(owningType.oclIsKindOf(ItemDefinition) or
 owningType.oclIsKindOf(ItemUsage)) implies
    specializesFromLibrary('Items::Item::checkedConstraints')
```
### C#
``` CSharp 
(!(owningType != null && ((owningType is ItemDefinition) || (owningType is ItemUsage))) || specializesFromLibrary("Items::Item::checkedConstraints"))
```
# DerivePackageFilterCondition
### OCL
``` OCL 
ownedMembership->
    selectByKind(ElementFilterMembership).condition
```
### C#
``` CSharp 
ownedMembership.OfType<ElementFilterMembership>()
```
# CheckAssociationStructureBinarySpecialization
### OCL
``` OCL 
endFeature->size() = 2 implies
    specializesFromLibrary('Objects::BinaryLinkObject')
```
### C#
``` CSharp 
(!(endFeature.Count() == 2) || specializesFromLibrary("Objects::BinaryLinkObject"))
```
# DeriveElementOwnedAnnotation
### OCL
``` OCL 
ownedRelationship->
    selectByKind(Annotation)->
    select(a | a.annotatedElement = self)
```
### C#
``` CSharp 
ownedRelationship.OfType<Annotation>().Where(a => a.annotatedElement == this)
```
# ValidateSelectExpressionOperator
### OCL
``` OCL 
'select'
```
### C#
``` CSharp 
"select"
```
# ValidateTypeOwnedDifferencingNotOne
### OCL
``` OCL 
ownedDifferencing->size() <> 1
```
### C#
``` CSharp 
ownedDifferencing.Count() != 1
```
# CheckStructureSpecialization
### OCL
``` OCL 
specializesFromLibrary('Objects::Object')
```
### C#
``` CSharp 
specializesFromLibrary("Objects::Object")
```
# ValidateRequirementVerificationMembershipKind
### OCL
``` OCL 
RequirementConstraintKind::requirement
```
### C#
``` CSharp 
RequirementConstraintKind.requirement
```
# CheckRenderingUsageRedefinition
### OCL
``` OCL 
owningFeatureMembership <> null and
owningFeatureMembership.oclIsKindOf(ViewRenderingMembership) implies
    redefinesFromLibrary('Views::View::viewRendering')
```
### C#
``` CSharp 
(!(owningFeatureMembership != null && (owningFeatureMembership is ViewRenderingMembership)) || redefinesFromLibrary("Views::View::viewRendering"))
```
# DeriveUsageNestedCalculation
### OCL
``` OCL 
nestedUsage->selectByKind(CalculationUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<CalculationUsage>()
```
# DeriveITestionUsageElseAction
### OCL
``` OCL 
    let parameter : Feature = inputParameter(3) in
    if parameter <> null and parameter.oclIsKindOf(ActionUsage) then
        parameter.oclAsType(ActionUsage)
    else
        null
    endif
```
### C#
``` CSharp 
inputParameter(3).Select(parameter => (parameter != null && (parameter is ActionUsage) ? ((ActionUsage)parameter) : null))
```
# ValidateForLoopActionUsageLoopVariable
### OCL
``` OCL 
ownedFeature->notEmpty() and
ownedFeature->at(1).oclIsKindOf(ReferenceUsage)
```
### C#
``` CSharp 
ownedFeature.Any() && (ownedFeature.ElementAt(0) is ReferenceUsage)
```
# ValidateWhileLoopActionUsage
### OCL
``` OCL 
inputParameters()->size() >= 2
```
### C#
``` CSharp 
inputParameters().Count() >= 2
```
# CheckConcernDefinitionSpecialization
### OCL
``` OCL 
specializesFromLibrary('Requirements::ConcernCheck')
```
### C#
``` CSharp 
specializesFromLibrary("Requirements::ConcernCheck")
```
# DeriveViewpointUsageViewpointStakeholder
### OCL
``` OCL 
framedConcern.featureMemberhsip->
    selectByKind(StakeholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
framedConcern.featureMemberhsip.OfType<StakeholderMembership>()
```
# CheckStateUsageSubstateSpecialization
### OCL
``` OCL 
isSubstateUsage(true) implies
    specializesFromLibrary('States::StateAction::substates')
```
### C#
``` CSharp 
(!(isSubstateUsage(true)) || specializesFromLibrary("States::StateAction::substates"))
```
# DeriveUsageDirectedUsage
### OCL
``` OCL 
directedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
directedFeature.OfType<Usage>()
```
# DeriveFeatureOwnedFeatureChaining
### OCL
``` OCL 
ownedRelationship->selectByKind(FeatureChaining)
```
### C#
``` CSharp 
ownedRelationship.OfType<FeatureChaining>()
```
# ValidateViewDefinitionOnlyOneViewRendering
### OCL
``` OCL 
featureMembership->
    selectByKind(ViewRenderingMembership)->
    size() <= 1
```
### C#
``` CSharp 
featureMembership.OfType<ViewRenderingMembership>().Count() <= 1
```
# ValidateFeatureChainExpressionConformance
### OCL
``` OCL 
argument->notEmpty() implies
    targetFeature.isFeaturedWithin(argument->first().result)
```
### C#
``` CSharp 
(!(argument.Any()) || targetFeature.isFeaturedWithin(argument.First().result))
```
# ValidateNamespaceDistinguishibility
### OCL
``` OCL 
membership->forAll(m1 | 
    membership->forAll(m2 | 
        m1 <> m2 implies m1.isDistinguishableFrom(m2)))
```
### C#
``` CSharp 
membership.All(m1 => membership.All(m2 => (!(m1 != m2) || m1.isDistinguishableFrom(m2))))
```
# ValidateRequirementConstraintMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(RequirementDefinition) or
owningType.oclIsKindOf(RequirementUsage)
```
### C#
``` CSharp 
(owningType is RequirementDefinition) || (owningType is RequirementUsage)
```
# CheckLiteralBooleanSpecialization
### OCL
``` OCL 
specializesFromLibrary('Performances::literalBooleanEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::literalBooleanEvaluations")
```
# CheckStateUsageExclusiveStateSpecialization
### OCL
``` OCL 
isSubstateUsage(false) implies
    specializesFromLibrary('States::StateAction::exclusiveStates')
```
### C#
``` CSharp 
(!(isSubstateUsage(false)) || specializesFromLibrary("States::StateAction::exclusiveStates"))
```
# CheckConnectionDefinitionSpecializations
### OCL
``` OCL 
specializesFromLibrary('Connections::Connection')
```
### C#
``` CSharp 
specializesFromLibrary("Connections::Connection")
```
# ValidateITestionUsageParameters
### OCL
``` OCL 
inputParameters()->size() >= 2
```
### C#
``` CSharp 
inputParameters().Count() >= 2
```
# CheckAssociationBinarySpecialization
### OCL
``` OCL 
associationEnd->size() = 2 implies
    specializesFromLibrary('Links::BinaryLink')
```
### C#
``` CSharp 
(!(associationEnd.Count() == 2) || specializesFromLibrary("Links::BinaryLink"))
```
# ValidateObjectiveMembershipIsComposite
### OCL
``` OCL 
ownedObjectiveRequirement.isComposite
```
### C#
``` CSharp 
ownedObjectiveRequirement.isComposite
```
# DeriveForLoopActionUsageSeqArgument
### OCL
``` OCL 
argument(1)
```
### C#
``` CSharp 
argument(1)
```
# ValidateTransitionFeatureMembershipOwningType
### OCL
``` OCL 
owningType.oclIsKindOf(TransitionUsage)
```
### C#
``` CSharp 
(owningType is TransitionUsage)
```
# DeriveUsageNestedReference
### OCL
``` OCL 
nestedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
nestedUsage.OfType<ReferenceUsage>()
```
# CheckClassSpecialization
### OCL
``` OCL 
specializesFromLibrary('Occurrences::Occurrence')
```
### C#
``` CSharp 
specializesFromLibrary("Occurrences::Occurrence")
```
# ValidateSubsettingConstantConformance
### OCL
``` OCL 
subsettedFeature.isConstant and subsettingFeature.isVariable implies 
    subsettingFeature.isConstant
```
### C#
``` CSharp 
(!(subsettedFeature.isConstant && subsettingFeature.isVariable) || subsettingFeature.isConstant)
```
# ValidateTriggerInvocationExpressionAfterArgument
### OCL
``` OCL 
TriggerKind::after implies
    argument->notEmpty() and
    argument->at(1).result.specializesFromLibrary('Quantities::ScalarQuantityValue') and
    let mRef : Element = 
        resolveGlobal('Quantities::TensorQuantityValue::mRef').ownedMemberElement in
    argument->at(1).result.feature->
        select(ownedRedefinition.redefinedFeature->
           closure(ownedRedefinition.redefinedFeature)->
           includes(mRef))->
        exists(specializesFromLibrary('ISQBase::DurationUnit'))
```
### C#
``` CSharp 
// TODO
(!(TriggerKind.after) || argument.Any()) && argument.ElementAt(1 -
1).specializesFromLibrary("Quantities::ScalarQuantityValue") && mRef is Element ==
resolveGlobal("Quantities::TensorQuantityValue::mRef").ownedMemberElement
```
# CheckConnectorSpecialization
### OCL
``` OCL 
specializesFromLibrary('Links::links')
```
### C#
``` CSharp 
specializesFromLibrary("Links::links")
```
# ValidateStructureSpecialization
### OCL
``` OCL 
ownedSpecialization.general->forAll(not oclIsKindOf(Behavior))
```
### C#
``` CSharp 
ownedSpecialization.general.All(item => !(item is Behavior))
```
