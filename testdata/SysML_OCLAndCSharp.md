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
calculation = action->selectByKind(CalculationUsage)
```
### C#
``` CSharp 
calculation == action.OfType<CalculationUsage>()
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
owningType is RequirementDefinition || owningType is RequiremenCaseRequirementDefinition || owningType is CaseDefinition || owningType is CaseUsage
```
# DeriveFeatureReferenceExpressionReferent
### OCL
``` OCL 
referent =
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
referent == ownedMembership.Where(it => !((ParameterMembership))).Select(nonParameterMemberships => (!nonParameterMemberships.Any() || !(nonParameterMemberships.First().memberElement is Feature) ? null : ((Feature)nonParameterMemberships.First().memberElement)))
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
(!(!(ConjugatedPortDefinition)) || ownedMember.OfType<ConjugatedPortDefinition>().Count() == 1)
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
(!(direction == null && ownedSpecializations.All(it => isImplied)) || ownedMembership.OfType<FeatureValue>().All(fv => specializes(fv.value.result)))
```
# DeriveFeatureOwnedTypeFeaturing
### OCL
``` OCL 
ownedTypeFeaturing = ownedRelationship->selectByKind(TypeFeaturing)->
    select(tf | tf.featureOfType = self)
```
### C#
``` CSharp 
ownedTypeFeaturing == ownedRelationship.OfType<TypeFeaturing>().Where(tf => tf.featureOfType == self)
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
!ownedFeatures.Where(it => it != result).Any()
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
subjectParameter =
    let subjectMems : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjectMems->isEmpty() then null
    else subjectMems->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
subjectParameter == featureMembership.OfType<SubjectMembership>().Select(subjectMems => (!subjectMems.Any() ? null : subjectMems.First().ownedSubjectParameter))
```
# DeriveDefinitionOwnedConnection
### OCL
``` OCL 
ownedConnection = ownedUsage->selectByKind(ConnectorAsUsage)
```
### C#
``` CSharp 
ownedConnection == ownedUsage.OfType<ConnectorAsUsage>()
```
# DeriveDefinitionOwnedConstraint
### OCL
``` OCL 
ownedConstraint = ownedUsage->selectByKind(ConstraintUsage)
```
### C#
``` CSharp 
ownedConstraint == ownedUsage.OfType<ConstraintUsage>()
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
instantiatedType = instantiatedType()
```
### C#
``` CSharp 
instantiatedType == instantiatedType()
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
ownedElement = ownedRelationship.ownedRelatedElement
```
### C#
``` CSharp 
ownedElement == ownedRelationship.ownedRelatedElement
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
(!(isComposite && owningType != null && ((owningType is StateDefinition) || (owningType is StateUsage)) && source != null && (source is StateUsage)) || specializesFromLibrary("States::StateAction::stateTransitions"))
```
# DeriveRequirementDefinitionFramedConcern
### OCL
``` OCL 
framedConcern = featureMembership->
    selectByKind(FramedConcernMembership).
    ownedConcern
```
### C#
``` CSharp 
framedConcern == featureMembership.OfType<FramedConcernMembership>()
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
targetFeature =
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
targetFeature == ownedMembership.Where(it => !((ParameterMembership))).Select(nonParameterMemberships => (!nonParameterMemberships.Any() || !(nonParameterMemberships.First().memberElement is Feature) ? null : ((Feature)nonParameterMemberships.First().memberElement)))
```
# DeriveUsageNestedAllocation
### OCL
``` OCL 
nestedAllocation = nestedUsage->selectByKind(AllocationUsage)
```
### C#
``` CSharp 
nestedAllocation == nestedUsage.OfType<AllocationUsage>()
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
ownedFeature.All(f => (!(f != result) || f.direction == FeatureDirectionKind._))
```
# DeriveAnnotationAnnotatingElement
### OCL
``` OCL 
annotatingElement =
    if ownedAnnotatingElement <> null then ownedAnnotatingElement
    else owningAnnotatingElement
    endif
```
### C#
``` CSharp 
annotatingElement == (ownedAnnotatingElement != null ? ownedAnnotatingElement : owningAnnotatingElement)
```
# DeriveStepBehavior
### OCL
``` OCL 
behavior = type->selectByKind(Behavior)
```
### C#
``` CSharp 
behavior == type.OfType<Behavior>()
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
inheritedFeature = inheritedMemberships->
    selectByKind(FeatureMembership).memberFeature
```
### C#
``` CSharp 
inheritedFeature == inheritedMemberships.OfType<FeatureMembership>()
```
# DeriveViewUsageExposedElement
### OCL
``` OCL 
exposedElement = ownedImport->selectByKind(Expose).
    importedMemberships(Set{}).memberElement->
    select(elm | includeAsExposed(elm))->
    asOrderedSet()
```
### C#
``` CSharp 
exposedElement == ownedImport.OfType<Expose>().importedMemberships(new HashSet<dynamic> {  }).memberElement.Where(elm => includeAsExposed(elm)).Distinct().ToHashSet()
```
# DeriveViewDefinitionView
### OCL
``` OCL 
view = usage->selectByKind(ViewUsage)
```
### C#
``` CSharp 
view == usage.OfType<ViewUsage>()
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
ownedMembership = ownedRelationship->selectByKind(Membership)
```
### C#
``` CSharp 
ownedMembership == ownedRelationship.OfType<Membership>()
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
(!(triggerAction.Any()) || payloadParameter is Feature == inputParameter(2))
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
/* closure */ featureWithValue.redefinition.redefinedFeature.valuation.All(it => isDefault)
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
importedElement = importedNamespace
```
### C#
``` CSharp 
importedElement == importedNamespace
```
# DeriveElementName
### OCL
``` OCL 
name = effectiveName()
```
### C#
``` CSharp 
name == effectiveName()
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
nestedVerificationCase = nestedUsage->selectByKind(VerificationCaseUsage)
```
### C#
``` CSharp 
nestedVerificationCase == nestedUsage.OfType<VerificationCaseUsage>()
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
ownedAnnotatingElement =
    let ownedAnnotatingElements : Sequence(AnnotatingElement) = 
        ownedRelatedElement->selectByKind(AnnotatingElement) in
    if ownedAnnotatingElements->isEmpty() then null
    else ownedAnnotatingElements->first()
    endif
```
### C#
``` CSharp 
ownedAnnotatingElement == ownedRelatedElement.OfType<AnnotatingElement>().Select(ownedAnnotatingElements => (!ownedAnnotatingElements.Any() ? null : ownedAnnotatingElements.First()))
```
# DeriveAllocationDefinitionAllocation
### OCL
``` OCL 
allocation = usage->selectAsKind(AllocationUsage)
```
### C#
``` CSharp 
allocation == usage.selectAsKind(AllocationUsage).selectAsKind
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
mayTimeVary =
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
mayTimeVary == owningType != null && owningType.specializesFromLibrary("Occurrences::Occurrence") && !(isPortion || specializesFromLibrary("Links::SelfLink") || specializesFromLibrary("Occurrences::HappensLink") || isComposite && specializesFromLibrary("Actions::Action"))
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
owningNamespace =
    if owningMembership = null then null
    else owningMembership.membershipOwningNamespace
    endif
```
### C#
``` CSharp 
owningNamespace == (owningMembership == null ? null : owningMembership.membershipOwningNamespace)
```
# DeriveUsageNestedPart
### OCL
``` OCL 
nestedPart = nestedUsage->selectByKind(PartUsage)
```
### C#
``` CSharp 
nestedPart == nestedUsage.OfType<PartUsage>()
```
# DeriveNamespaceMembers
### OCL
``` OCL 
member = membership.memberElement
```
### C#
``` CSharp 
member == membership.memberElement
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
ownedConjugator =
    let ownedConjugators: Sequence(Conjugator) = 
        ownedRelationship->selectByKind(Conjugation) in
    if ownedConjugators->isEmpty() then null 
    else ownedConjugators->at(1) endif
```
### C#
``` CSharp 
ownedConjugator == ownedRelationship.OfType<Conjugation>().Select(ownedConjugators => (!ownedConjugators.Any() ? null : ownedConjugators.ElementAt(1 - 1)))
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
verifiedRequirement =
    if objectiveRequirement = null then OrderedSet{}
    else 
        objectiveRequirement.featureMembership->
            selectByKind(RequirementVerificationMembership).
            verifiedRequirement->asOrderedSet()
    endif
```
### C#
``` CSharp 
verifiedRequirement == (objectiveRequirement == null ? new List<dynamic> {  } : objectiveRequirement.featureMembership.OfType<RequirementVerificationMembership>().Distinct().ToHashSet())
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
ownedSpecialization.general.All(it => !(Structure))
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
viewCondition = ownedMembership->
    selectByKind(ElementFilterMembership).
    condition
```
### C#
``` CSharp 
viewCondition == ownedMembership.OfType<ElementFilterMembership>()
```
# DeriveOwningMembershipOwnedMemberName
### OCL
``` OCL 
ownedMemberName = ownedMemberElement.name
```
### C#
``` CSharp 
ownedMemberName == ownedMemberElement.name
```
# DeriveOwningMembershipOwnedMemberShortName
### OCL
``` OCL 
ownedMemberShortName = ownedMemberElement.shortName
```
### C#
``` CSharp 
ownedMemberShortName == ownedMemberElement.shortName
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
(!(association.Any(it => (AssociationStructure))) || specializesFromLibrary("Objects::linkObjects"))
```
# DeriveVewRenderingMembershipReferencedRendering
### OCL
``` OCL 
referencedRendering =
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
referencedRendering == ownedRendering.referencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? ownedRendering : ((referencedFeature is RenderingUsage) ? ((RenderingUsage)refrencedFeature) : null)))
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
memberElementId = memberElement.elementId
```
### C#
``` CSharp 
memberElementId == memberElement.elementId
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
ownedMember.OfType<Succession>().Select(successions => successions.Any() && successions.ElementAt(1 - 1).featureTarget.All(it => (ActionUsage)))
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
relatedFeature = connectorEnd.ownedReferenceSubsetting->
    select(s | s <> null).subsettedFeature
```
### C#
``` CSharp 
relatedFeature == connectorEnd.ownedReferenceSubsetting.Where(s => s != null).subsettedFeature
```
# DeriveConnectorSourceFeature
### OCL
``` OCL 
sourceFeature = 
    if relatedFeature->isEmpty() then null 
    else relatedFeature->first() 
    endif
```
### C#
``` CSharp 
sourceFeature == !relatedFeature.Any()
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
(!(isOwnedCrossFeature()) || ((Feature)owner).type.All(t => self.specializes(t)))
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
nestedConcern = nestedUsage->selectByKind(ConcernUsage)
```
### C#
``` CSharp 
nestedConcern == nestedUsage.OfType<ConcernUsage>()
```
# DeriveActionDefinitionAction
### OCL
``` OCL 
action = usage->selectByKind(ActionUsage)
```
### C#
``` CSharp 
action == usage.OfType<ActionUsage>()
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
ownedFlow = ownedUsage->selectByKind(FlowConnectionUsage)
```
### C#
``` CSharp 
ownedFlow == ownedUsage.OfType<FlowConnectionUsage>()
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
nestedOccurrence = nestedUsage->selectByKind(OccurrenceUsage)
```
### C#
``` CSharp 
nestedOccurrence == nestedUsage.OfType<OccurrenceUsage>()
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
subjectParameter =
    let subjects : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjects->isEmpty() then null
    else subjects->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
subjectParameter == subjects is (SubjectMembership) == featureMembership.OfType<SubjectMembership>()
```
# DeriveFeatureOwnedRedefinition
### OCL
``` OCL 
ownedRedefinition = ownedSubsetting->selectByKind(Redefinition)
```
### C#
``` CSharp 
ownedRedefinition == ownedSubsetting.OfType<Redefinition>()
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
ownedUsage = ownedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
ownedUsage == ownedFeature.OfType<Usage>()
```
# DeriveUsageNestedConstraint
### OCL
``` OCL 
nestedConstraint = nestedUsage->selectByKind(ConstraintUsage)
```
### C#
``` CSharp 
nestedConstraint == nestedUsage.OfType<ConstraintUsage>()
```
# DeriveRequirementDefinitionRequiredConstraint
### OCL
``` OCL 
requiredConstraint = ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::requirement).
    ownedConstraint
```
### C#
``` CSharp 
requiredConstraint == ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(it => kind == RequirementConstraintKind.requirement).ownedConstraint
```
# DeriveUsageNestedFlow
### OCL
``` OCL 
nestedFlow = nestedUsage->selectByKind(FlowConnectionUsage)
```
### C#
``` CSharp 
nestedFlow == nestedUsage.OfType<FlowConnectionUsage>()
```
# DeriveBehaviorStep
### OCL
``` OCL 
step = feature->selectByKind(Step)
```
### C#
``` CSharp 
step == feature.OfType<Step>()
```
# DeriveTypeDirectedFeature
### OCL
``` OCL 
directedFeature = feature->select(f | directionOf(f) <> null)
```
### C#
``` CSharp 
directedFeature == feature.Where(f => directionOf(f) != null)
```
# DeriveUsageNestedState
### OCL
``` OCL 
nestedState = nestedUsage->selectByKind(StateUsage)
```
### C#
``` CSharp 
nestedState == nestedUsage.OfType<StateUsage>()
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
ownedReferenceSubsetting =
    let referenceSubsettings : OrderedSet(ReferenceSubsetting) =
        ownedSubsetting->selectByKind(ReferenceSubsetting) in
    if referenceSubsettings->isEmpty() then null
    else referenceSubsettings->first() endif
```
### C#
``` CSharp 
ownedReferenceSubsetting == referenceSubsettings is (ReferenceSubsetting) == ownedSubsetting.OfType<ReferenceSubsetting>()
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
(instantiatedType is Behavior) || (instantiatedType is Feature) && instantiatedType.type.Any(it => (Behavior)) && instantiatedType.type.Count()
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
assumedConstraint = ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::assumption).
    ownedConstraint
```
### C#
``` CSharp 
assumedConstraint == ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(it => kind == RequirementConstraintKind.assumption).ownedConstraint
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
targetConnector.OfType<Succession>().Select(it => connectorEnd.ElementAt(2 - 1)).All(targetMult => multiplicityHasBounds(targetMult, 1, 1))
```
# DeriveDefinitionVariantMembership
### OCL
``` OCL 
variantMembership = ownedMembership->selectByKind(VariantMembership)
```
### C#
``` CSharp 
variantMembership == ownedMembership.OfType<VariantMembership>()
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
crossFeature =
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
crossFeature == ownedCrossSubsetting == null
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
ownedMembership.Any(it => !(OwningMembership) && (memberElement is Feature))
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
featuringType =
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
featuringType == featuringTypes is (Type) == featuring.type.Distinct().ToHashSet()
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
actorParameter = featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
actorParameter == featureMembership.OfType<ActorMembership>()
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
variantMembership = ownedMembership->selectByKind(VariantMembership)
```
### C#
``` CSharp 
variantMembership == ownedMembership.OfType<VariantMembership>()
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
result =
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
result == resultParams is (Feature) == featureMemberships.OfType<ReturnParameterMembership>()
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
conjugatedPortDefinition = null
```
### C#
``` CSharp 
conjugatedPortDefinition == null
```
# DeriveUsageNestedUsage
### OCL
``` OCL 
nestedUsage = ownedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
nestedUsage == ownedFeature.OfType<Usage>()
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
# CheckViewpointUsageViewpointSatisfactionSpecialization
### OCL
``` OCL 
isComposite and owningType <> null and
(owningType.oclIsKindOf(ViewDefinition) or
 owningType.oclIsKindOf(ViewUsage)) implies
    specializesFromLibrary('Views::View::viewpointSatisfactions')
```
### C#
``` CSharp 
(!(isComposite && owningType != null && ((owningType is ViewDefinition) || (owningType is ViewUsage))) || specializesFromLibrary("Views::View::viewpointSatisfactions"))
```
# CheckIfActionUsageSpecialization
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
bound.All(b => b.featuringType == self.featuringType)
```
# DeriveViewDefinitionViewRendering
### OCL
``` OCL 
viewRendering =
    let renderings: OrderedSet(ViewRenderingMembership) =
        featureMembership->selectByKind(ViewRenderingMembership) in
    if renderings->isEmpty() then null
    else renderings->first().referencedRendering
    endif
```
### C#
``` CSharp 
viewRendering == renderings is (ViewRenderingMembership) == featureMembership.OfType<ViewRenderingMembership>()
```
# DeriveTransitionUsageGuardExpression
### OCL
``` OCL 
guardExpression = ownedFeatureMembership->
    selectByKind(TransitionFeatureMembership)->
    select(kind = TransitionFeatureKind::trigger).transitionFeature->
    selectByKind(Expression)
```
### C#
``` CSharp 
guardExpression == ownedFeatureMembership.OfType<TransitionFeatureMembership>().Where(it => kind == TransitionFeatureKind.trigger).transitionFeature.OfType<Expression>()
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
rendering = usages->selectByKind(RenderingUsage)
```
### C#
``` CSharp 
rendering == usages.OfType<RenderingUsage>()
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
result.owningType == self
```
# DeriveFeatureType
### OCL
``` OCL 
type = 
    let types : OrderedSet(Types) = OrderedSet{self}->
        -- Note: The closure operation automatically handles circular relationships.
        closure(typingFeatures()).typing.type->asOrderedSet() in
    types->reject(t1 | types->exist(t2 | t2 <> t1 and t2.specializes(t1)))
```
### C#
``` CSharp 
type == types is (Types) == /* closure */ new List<dynamic> { self }.typing.type.Distinct().ToHashSet()
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
(!(ownedRelationship.Any(it => isImplied)) || isImpliedIncluded)
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
documentation = ownedElement->selectByKind(Documentation)
```
### C#
``` CSharp 
documentation == ownedElement.OfType<Documentation>()
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
nestedUsage.Where(it => !((PortUsage))).All(it => !isComposite)
```
# DeriveTypeOwnedUnioning
### OCL
``` OCL 
ownedUnioning =
    ownedRelationship->selectByKind(Unioning)
```
### C#
``` CSharp 
ownedUnioning == ownedRelationship.OfType<Unioning>()
```
# DeriveRequirementUsageText
### OCL
``` OCL 
text = documentation.body
```
### C#
``` CSharp 
text == documentation.body
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
objectiveRequirement = 
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
objectiveRequirement == objectives is (RequirementUsage) == featureMembership.OfType<ObjectiveMembership>()
```
# DeriveRequirementDefinitionActorParameter
### OCL
``` OCL 
actorParameter = featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
actorParameter == featureMembership.OfType<ActorMembership>()
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
instantiatedType.feature.Select(f => result.ownedFeatures.Where(it => redefines(f)).valuation.Where(v => v != null).value)
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
# DeriveIfActionUsageIfArgument
### OCL
``` OCL 
ifArgument = 
    let parameter : Feature = inputParameter(1) in
    if parameter <> null and parameter.oclIsKindOf(Expression) then
        parameter.oclAsType(Expression)
    else
        null
    endif
```
### C#
``` CSharp 
ifArgument == parameter is Feature == inputParameter(1)
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
assertedConstraint =
    if referencedFeatureTarget() = null then self
    else if referencedFeatureTarget().oclIsKindOf(ConstraintUsage) then
        referencedFeatureTarget().oclAsType(ConstraintUsage)
    else null
    endif endif
```
### C#
``` CSharp 
assertedConstraint == referencedFeatureTarget() == null
```
# DeriveTypeInput
### OCL
``` OCL 
input = feature->select(f | 
    let direction: FeatureDirectionKind = directionOf(f) in
    direction = FeatureDirectionKind::_'in' or
    direction = FeatureDirectionKind::inout)
```
### C#
``` CSharp 
input == feature.Where(f => directionOf(f).Select(direction => direction == FeatureDirectionKind._)) || direction == FeatureDirectionKind.inout
```
# DeriveViewUsageSatisfiedViewpoint
### OCL
``` OCL 
satisfiedViewpoint = ownedRequirement->
    selectByKind(ViewpointUsage)->
    select(isComposite)
```
### C#
``` CSharp 
satisfiedViewpoint == ownedRequirement.OfType<ViewpointUsage>().Where(it => isComposite)
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
ownedMembership.Where(m => !((m is ParameterMembership))).Select(membership => membership.Any() && (membership.ElementAt(1 - 1) is Feature))
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
ownedFeatures.Where(it => direction == _).Select(inputParameters => owningExpression.sourceTargetFeature().Select(sourceTargetFeature => sourceTargetFeature != null && result.subsetsChain(inputParameters.First(), sourceTargetFeature) && result.owningType == self))
```
# DeriveUsageNestedTransition
### OCL
``` OCL 
nestedTransition = nestedUsage->selectByKind(TransitionUsage)
```
### C#
``` CSharp 
nestedTransition == nestedUsage.OfType<TransitionUsage>()
```
# DeriveTypeFeature
### OCL
``` OCL 
feature = featureMembership.ownedMemberFeature
```
### C#
``` CSharp 
feature == featureMembership.ownedMemberFeature
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
sourceConnector.OfType<Succession>().Select(it => connectorEnd.ElementAt(1 - 1)).All(sourceMult => multiplicityHasBounds(sourceMult, 1, 1))
```
# ValidateObjectiveMembershipOwningType
### OCL
``` OCL 
owningType.oclIsType(CaseDefinition) or
owningType.oclIsType(CaseUsage)
```
### C#
``` CSharp 
owningType is CaseDefinition || owningType is CaseUsage
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
(ownedMembership.OfType<StateSubactionMembership>().Select(it => kind).Distinct().Count() == ownedMembership.OfType<StateSubactionMembership>().Count())
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
relatedType = associationEnd.type
```
### C#
``` CSharp 
relatedType == associationEnd.type
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
relatedElement = source->union(target)
```
### C#
``` CSharp 
relatedElement == source.Union(target)
```
# DeriveUsageNestedInterface
### OCL
``` OCL 
nestedInterface = nestedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
nestedInterface == nestedUsage.OfType<ReferenceUsage>()
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
(ownedMembership.OfType<StateSubactionMembership>().Select(it => kind).Distinct().Count() == ownedMembership.OfType<StateSubactionMembership>().Count())
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
(!(owningType != null && ((owningType is ) && self == (()owningType).result || (owningType is Expression) && self == ((Expression)owningType).result)) || (()owningType.ownedSpecialization.general.Where(it => () || (Expression)).All(supertype => (redefines() is )).superType).result)
```
# DeriveAssociationTargetType
### OCL
``` OCL 
targetType =
    if relatedType->size() < 2 then OrderedSet{}
    else 
        relatedType->
            subSequence(2, relatedType->size())->
            asOrderedSet() 
    endif
```
### C#
``` CSharp 
targetType == relatedType.Count() < 2
```
# DeriveDefinitionOwnedCase
### OCL
``` OCL 
ownedCase = ownedUsage->selectByKind(CaseUsage)
```
### C#
``` CSharp 
ownedCase == ownedUsage.OfType<CaseUsage>()
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
instantiatedType.feature.Where(it => owningMembership.visibility == VisibilityKind.public).Select(features => result.ownedFeature.All(f => f.ownedRedefinition.redefinedFeature.Intersect(features).Count() == 1))
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
ownedFeatureMembership = ownedRelationship->selectByKind(FeatureMembership)
```
### C#
``` CSharp 
ownedFeatureMembership == ownedRelationship.OfType<FeatureMembership>()
```
# DeriveUsageNestedPort
### OCL
``` OCL 
nestedPort = nestedUsage->selectByKind(PortUsage)
```
### C#
``` CSharp 
nestedPort == nestedUsage.OfType<PortUsage>()
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
ownedNested = nestedUsage->selectByKind(EnumerationUsage)
```
### C#
``` CSharp 
ownedNested == nestedUsage.OfType<EnumerationUsage>()
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
annotatedElement = 
 if annotation->notEmpty() then annotation.annotatedElement
 else Sequence{owningNamespace} endif
```
### C#
``` CSharp 
annotatedElement == annotation.Any()
```
# DeriveLiteralExpressionIsModelLevelEvaluable
### OCL
``` OCL 
isModelLevelEvaluable = true
```
### C#
``` CSharp 
isModelLevelEvaluable == true
```
# DeriveElementQualifiedName
### OCL
``` OCL 
qualifiedName =
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
qualifiedName == owningNamespace == null
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
targetArgument = argument(1)
```
### C#
``` CSharp 
targetArgument == argument(1)
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
ownedCrossSubsetting =
    let crossSubsettings: Sequence(CrossSubsetting) = 
        ownedSubsetting->selectByKind(CrossSubsetting) in
    if crossSubsettings->isEmpty() then null
    else crossSubsettings->first()
    endif
```
### C#
``` CSharp 
ownedCrossSubsetting == crossSubsettings is (CrossSubsetting) == ownedSubsetting.OfType<CrossSubsetting>()
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
inheritedMembership = inheritedMemberships(Set{}, Set{}, false)
```
### C#
``` CSharp 
inheritedMembership == inheritedMemberships(new HashSet<dynamic> {  }, new HashSet<dynamic> {  }, false)
```
# DeriveTypeOwnedDisjoining
### OCL
``` OCL 
ownedDisjoining =
    ownedRelationship->selectByKind(Disjoining)
```
### C#
``` CSharp 
ownedDisjoining == ownedRelationship.OfType<Disjoining>()
```
# DeriveUsageIsReference
### OCL
``` OCL 
isReference = not isComposite
```
### C#
``` CSharp 
isReference == !isComposite
```
# DeriveTerminateActionUsageTerminatedOccurrenceArgument
### OCL
``` OCL 
terminatedOccurrenceArgument = argument(1)
```
### C#
``` CSharp 
terminatedOccurrenceArgument == argument(1)
```
# ValidateAttributeDefinitionFeatures
### OCL
``` OCL 
feature->forAll(not isComposite)
```
### C#
``` CSharp 
feature.All(it => !isComposite)
```
# DeriveAssignmentActionUsageValueExpression
### OCL
``` OCL 
valueExpression = argument(2)
```
### C#
``` CSharp 
valueExpression == argument(2)
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
ownedMember = ownedMembership->selectByKind(OwningMembership).ownedMemberElement
```
### C#
``` CSharp 
ownedMember == ownedMembership.OfType<OwningMembership>()
```
# DeriveTypeMultiplicity
### OCL
``` OCL 
multiplicity = 
    let ownedMultiplicities: Sequence(Multiplicity) =
        ownedMember->selectByKind(Multiplicity) in
    if ownedMultiplicities->isEmpty() then null
    else ownedMultiplicities->first()
    endif
```
### C#
``` CSharp 
multiplicity == ownedMultiplicities is (Multiplicity) == ownedMember.OfType<Multiplicity>()
```
# DeriveDefinitionOwnedCalculation
### OCL
``` OCL 
ownedCalculation = ownedUsage->selectByKind(CalculationUsage)
```
### C#
``` CSharp 
ownedCalculation == ownedUsage.OfType<CalculationUsage>()
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
target =
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
target == !succession.targetFeature.Any()
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
ownedItem = ownedUsage->selectByKind(ItemUsage)
```
### C#
``` CSharp 
ownedItem == ownedUsage.OfType<ItemUsage>()
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
ownedEndFeature = ownedFeature->select(isEnd)
```
### C#
``` CSharp 
ownedEndFeature == ownedFeature.Where(it => isEnd)
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
(!(!(instantiatedType is ) && !((instantiatedType is Feature) && ((Feature)instantiatedType).type.Any(it => ()))) || ownedFeature.selectByKind(BindingConnector).Any(it => relatedFeature.Contains(self) && relatedFeature.Contains(result)))
```
# ValidateInstantiationExpressionResult
### OCL
``` OCL 
result.owningType = self
```
### C#
``` CSharp 
result.owningType == self
```
# DeriveUsageNestedMetadata
### OCL
``` OCL 
nestedMetadata = nestedUsage->selectByKind(MetadataUsage)
```
### C#
``` CSharp 
nestedMetadata == nestedUsage.OfType<MetadataUsage>()
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
ownedUsage.Where(it => !((PortUsage))).All(it => !isComposite)
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
featureTarget = if chainingFeature->isEmpty() then self else chainingFeature->last() endif
```
### C#
``` CSharp 
featureTarget == !chainingFeature.Any()
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
resultExpression =
    let results : OrderedSet(ResultExpressionMembership) =
        featureMembersip->
            selectByKind(ResultExpressionMembership) in
    if results->isEmpty() then null
    else results->first().ownedResultExpression
    endif
```
### C#
``` CSharp 
resultExpression == results is (ResultExpressionMembership) == featureMembersip.OfType<ResultExpressionMembership>()
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
ownedPort = ownedUsage->selectByKind(PortUsage)
```
### C#
``` CSharp 
ownedPort == ownedUsage.OfType<PortUsage>()
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
((Feature)resolveGlobal("Metaobjects::Metaobject::annotatedElement").memberElement).Select(baseAnnotatedElementFeature => feature.Where(it => specializes(baseAnnotatedElementFeature)).Where(it => it != baseAnnotatedElementFeature).Select(annotatedElementFeatures => (!(annotatedElementFeatures.Any()) || annotatedElementTypes is (Feature) == annotatedElementFeatures.typing.type.ToHashSet())))
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
# CheckIfActionUsageSubactionSpecialization
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
ownedDifferencing =
    ownedRelationship->selectByKind(Differencing)
```
### C#
``` CSharp 
ownedDifferencing == ownedRelationship.OfType<Differencing>()
```
# DeriveNamespaceImportedMembership
### OCL
``` OCL 
importedMembership = importedMemberships(Set{})
```
### C#
``` CSharp 
importedMembership == importedMemberships(new HashSet<dynamic> {  })
```
# DeriveTransitionUsageSource
### OCL
``` OCL 
source =
    let sourceFeature : Feature = sourceFeature() in
    if sourceFeature = null then null
    else sourceFeature.featureTarget.oclAsType(ActionUsage)
```
### C#
``` CSharp 
source == sourceFeature is Feature == sourceFeature()
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
variant = variantMembership.ownedVariantUsage
```
### C#
``` CSharp 
variant == variantMembership.ownedVariantUsage
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
whileArgument =
    let parameter : Feature = inputParameter(1) in
    if parameter <> null and parameter.oclIsKindOf(Expression) then
        parameter.oclAsType(Expression)
    else
        null
    endif
```
### C#
``` CSharp 
whileArgument == parameter is Feature == inputParameter(1)
```
# DeriveSendActionUsageReceiverArgument
### OCL
``` OCL 
receiverArgument = argument(3)
```
### C#
``` CSharp 
receiverArgument == argument(3)
```
# ValidateFeatureChainExpressionOperator
### OCL
``` OCL 
operator = '.'
```
### C#
``` CSharp 
operator == "."
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
sourceOutputFeature =
    if connectorEnd->isEmpty() or 
        connectorEnd.ownedFeature->isEmpty()
    then null
    else connectorEnd.ownedFeature->first()
    endif
```
### C#
``` CSharp 
sourceOutputFeature == !connectorEnd.Any() || !connectorEnd.ownedFeature.Any()
```
# DeriveAnnotatingElementAnnotation
### OCL
``` OCL 
annotation = 
    if owningAnnotatingRelationship = null then ownedAnnotatingRelationship
    else owningAnnotatingRelationship->prepend(owningAnnotatingRelationship)
    endif
```
### C#
``` CSharp 
annotation == owningAnnotatingRelationship == null
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
ownedAttribute = ownedUsage->selectByKind(AttributeUsage)
```
### C#
``` CSharp 
ownedAttribute == ownedUsage.OfType<AttributeUsage>()
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
(!(isEnd && owningType != null) || i is Integer == owningType.ownedEndFeature.ToList().IndexOf(self))
```
# DeriveAnnotatingElementOwnedAnnotatingRelationship
### OCL
``` OCL 
ownedAnnotatingRelationship = ownedRelationship->
    selectByKind(Annotation)->
    select(a | a.annotatedElement <> self)
```
### C#
``` CSharp 
ownedAnnotatingRelationship == ownedRelationship.OfType<Annotation>().Where(a => a.annotatedElement != self)
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
exitAction = 
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
exitAction == exitMemberships is (StateSubactionMembership) == ownedMembership.OfType<StateSubactionMembership>().Where(it => kind == StateSubactionKind.exit)
```
# DeriveDefinitionOwnedAnalysisCase
### OCL
``` OCL 
ownedAnalysisCase = ownedUsage->selectByKind(AnalysisCaseUsage)
```
### C#
``` CSharp 
ownedAnalysisCase == ownedUsage.OfType<AnalysisCaseUsage>()
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
(!(ownedSpecialization.general.All(it => !(DataType)) && !(Association)) || ownedSpecialization.general.All(it => !(Association)))
```
# DeriveUsageUsage
### OCL
``` OCL 
usage = feature->selectByKind(Usage)
```
### C#
``` CSharp 
usage == feature.OfType<Usage>()
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
doAction =
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
doAction == doMemberships is (StateSubactionMembership) == ownedMembership.OfType<StateSubactionMembership>().Where(it => kind == StateSubactionKind.do)
```
# DeriveItemUsageItemDefinition
### OCL
``` OCL 
itemDefinition = occurrenceDefinition->selectByKind(Structure)
```
### C#
``` CSharp 
itemDefinition == occurrenceDefinition.OfType<Structure>()
```
# DeriveFeatureOwnedFeatureInverting
### OCL
``` OCL 
ownedFeatureInverting = ownedRelationship->selectByKind(FeatureInverting)->
    select(fi | fi.featureInverted = self)
```
### C#
``` CSharp 
ownedFeatureInverting == ownedRelationship.OfType<FeatureInverting>().Where(fi => fi.featureInverted == self)
```
# DeriveDefinitionUsage
### OCL
``` OCL 
usage = feature->selectByKind(Usage)
```
### C#
``` CSharp 
usage == feature.OfType<Usage>()
```
# ValidateAttributeUsageFeatures
### OCL
``` OCL 
feature->forAll(not isComposite)
```
### C#
``` CSharp 
feature.All(it => !isComposite)
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
ownedView = ownedUsage->selectByKind(ViewUsage)
```
### C#
``` CSharp 
ownedView == ownedUsage.OfType<ViewUsage>()
```
# DeriveElementOwner
### OCL
``` OCL 
owner = owningRelationship.owningRelatedElement
```
### C#
``` CSharp 
owner == owningRelationship.owningRelatedElement
```
# DeriveUsageNestedAttribute
### OCL
``` OCL 
nestedAttribute = nestedUsage->selectByKind(AttributeUsage)
```
### C#
``` CSharp 
nestedAttribute == nestedUsage.OfType<AttributeUsage>()
```
# DeriveRequirementDefinitionStakeholderParameter
### OCL
``` OCL 
stakeholderParameter = featureMembership->
    selectByKind(StakholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
stakeholderParameter == featureMembership.OfType<StakholderMembership>()
```
# ValidateAssociationEndTypes
### OCL
``` OCL 
ownedEndFeature->forAll(type->size() = 1)
```
### C#
``` CSharp 
ownedEndFeature.All(it => type.Count() == 1)
```
# DeriveElementShortName
### OCL
``` OCL 
shortName = effectiveShortName()
```
### C#
``` CSharp 
shortName == effectiveShortName()
```
# DeriveAssociationSourceType
### OCL
``` OCL 
sourceType =
    if relatedType->isEmpty() then null
    else relatedType->first() endif
```
### C#
``` CSharp 
sourceType == !relatedType.Any()
```
# ValidateTransitionFeatureMembershipEffectAction
### OCL
``` OCL 
kind = TransitionFeatureKind::effect implies
    transitionFeature.oclIsKindOf(ActionUsage)
```
### C#
``` CSharp 
(!(kind == TransitionFeatureKind.effect) || (transitionFeature is ActionUsage))
```
# DeriveViewUsageViewRendering
### OCL
``` OCL 
viewRendering =
    let renderings: OrderedSet(ViewRenderingMembership) =
        featureMembership->selectByKind(ViewRenderingMembership) in
    if renderings->isEmpty() then null
    else renderings->first().referencedRendering
    endif
```
### C#
``` CSharp 
viewRendering == renderings is (ViewRenderingMembership) == featureMembership.OfType<ViewRenderingMembership>()
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
(!(isOwnedCrossFeature()) || otherEnds is (Feature) == ((Feature)owner).owningType.endFeature.Where(it => it != self))
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
(!(isComposite && ownedTyping.type.Contains((Class)) && owningType != null && ((owningType is Class) || (owningType is Feature) && ((Feature)owningType).type.Any(it => (Class)))) || specializesFromLibrary("Occurrence::Occurrence::suboccurrences"))
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
conjugatedPortDefinition = 
let conjugatedPortDefinitions : OrderedSet(ConjugatedPortDefinition) =
    ownedMember->selectByKind(ConjugatedPortDefinition) in
if conjugatedPortDefinitions->isEmpty() then null
else conjugatedPortDefinitions->first()
endif
```
### C#
``` CSharp 
conjugatedPortDefinition == conjugatedPortDefinitions is (ConjugatedPortDefinition) == ownedMember.OfType<ConjugatedPortDefinition>()
```
# DeriveTypeUnioningType
### OCL
``` OCL 
unioningType = ownedUnioning.unioningType
```
### C#
``` CSharp 
unioningType == ownedUnioning.unioningType
```
# DeriveLoopActionUsageBodyAction
### OCL
``` OCL 
bodyAction =
    let parameter : Feature = inputParameter(2) in
    if parameter <> null and parameter.oclIsKindOf(Action) then
        parameter.oclAsType(Action)
    else
        null
    endif
```
### C#
``` CSharp 
bodyAction == parameter is Feature == inputParameter(2)
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
(!(!(instantiatedType is ) && !((instantiatedType is Feature) && ((Feature)instantiatedType).type.Any(it => ()))) || result.specializes(instantiatedType))
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
(!(ownedTyping.type.Any(it => selectByKind(DataType))) || specializesFromLibrary("Base::dataValues"))
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
doAction =
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
doAction == doMemberships is (StateSubactionMembership) == ownedMembership.OfType<StateSubactionMembership>().Where(it => kind == StateSubactionKind.do)
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
directedUsage = directedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
directedUsage == directedFeature.OfType<Usage>()
```
# DeriveTransitionUsageEffectAction
### OCL
``` OCL 
triggerAction = ownedFeatureMembership->
    selectByKind(TransitionFeatureMembership)->
    select(kind = TransitionFeatureKind::trigger).transitionFeatures->
    selectByKind(AcceptActionUsage)
```
### C#
``` CSharp 
triggerAction == ownedFeatureMembership.OfType<TransitionFeatureMembership>().Where(it => kind == TransitionFeatureKind.trigger).transitionFeatures.OfType<AcceptActionUsage>()
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
nestedUseCase = nestedUsage->selectByKind(UseCaseUsage)
```
### C#
``` CSharp 
nestedUseCase == nestedUsage.OfType<UseCaseUsage>()
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
referencedConstraint =
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
referencedConstraint == referencedFeature is Feature == ownedConstraint.referencedFeatureTarget()
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
ownedRendering = ownedUsage->selectByKind(RenderingUsage)
```
### C#
``` CSharp 
ownedRendering == ownedUsage.OfType<RenderingUsage>()
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
framedConcern = featureMembership->
    selectByKind(FramedConcernMembership).
    ownedConcern
```
### C#
``` CSharp 
framedConcern == featureMembership.OfType<FramedConcernMembership>()
```
# DeriveTypeEndFeature
### OCL
``` OCL 
endFeature = feature->select(isEnd)
```
### C#
``` CSharp 
endFeature == feature.Where(it => isEnd)
```
# ValidateTriggerInvocationExpressionAtArgument
### OCL
``` OCL 
kind = TriggerKind::at implies
    argument->notEmpty() and
    argument->at(1).result.specializesFromLibrary('Time::TimeInstantValue')
```
### C#
``` CSharp 
(!(kind == TriggerKind.at) || argument.Any()) && argument.ElementAt(1 - 1).specializesFromLibrary("Time::TimeInstantValue")
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
(!(owningType != null && ((owningType is Behavior) || (owningType is Step)) && self.isComposite) || specializesFromLibrary("Performances::Performance::subperformance"))
```
# DeriveCaseDefinitionObjectiveRequirement
### OCL
``` OCL 
objectiveRequirement = 
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
objectiveRequirement == objectives is (RequirementUsage) == featureMembership.OfType<ObjectiveMembership>()
```
# DeriveRequirementUsageRequiredConstraint
### OCL
``` OCL 
requiredConstraint = ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::requirement).
    ownedConstraint
```
### C#
``` CSharp 
requiredConstraint == ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(it => kind == RequirementConstraintKind.requirement).ownedConstraint
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
(!(isPortion && ownedTyping.type.Contains((Class)) && owningType != null && ((owningType is Class) || (owningType is Feature) && ((Feature)owningType).type.Any(it => (Class)))) || specializesFromLibrary("Occurrence::Occurrence::portions"))
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
nestedAction = nestedUsage->selectByKind(ActionUsage)
```
### C#
``` CSharp 
nestedAction == nestedUsage.OfType<ActionUsage>()
```
# DeriveDefinitionOwnedTransition
### OCL
``` OCL 
ownedTransition = ownedUsage->selectByKind(TransitionUsage)
```
### C#
``` CSharp 
ownedTransition == ownedUsage.OfType<TransitionUsage>()
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
nestedAnalysisCase = nestedUsage->selectByKind(AnalysisCaseUsage)
```
### C#
``` CSharp 
nestedAnalysisCase == nestedUsage.OfType<AnalysisCaseUsage>()
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
nestedView = nestedUsage->selectByKind(ViewUsage)
```
### C#
``` CSharp 
nestedView == nestedUsage.OfType<ViewUsage>()
```
# DeriveRequirementDefinitionSubjectParameter
### OCL
``` OCL 
subjectParameter =
    let subjects : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjects->isEmpty() then null
    else subjects->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
subjectParameter == subjects is (SubjectMembership) == featureMembership.OfType<SubjectMembership>()
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
(!(ownedTyping.type.Any(it => selectByKind(Class))) || specializesFromLibrary("Occurrences::occurrences"))
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
chainingFeature = ownedFeatureChaining.chainingFeature
```
### C#
``` CSharp 
chainingFeature == ownedFeatureChaining.chainingFeature
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
triggerAction.All(it => specializesFromLibrary("Actions::TransitionAction::accepter") && guardExpression.All(it => specializesFromLibrary("Actions::TransitionAction::guard") && effectAction.All(it => specializesFromLibrary("Actions::TransitionAction::effect"))))
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
owningType != null && !(owningFeatureMembership is ReturnParameterMembership) && owningType.ownedSpecialization.general.All(supertype => supertype.ownedFeature.Where(it => direction != null).Where(it => !((owningFeatureMembership is ReturnParameterMembership))).Select(ownedParameters => (!(ownedParameters.Count() >= i) || redefines(ownedParameters.ElementAt(i - 1)))))
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
(lowerBound == null ? ownedMember.Any() && ownedMember.ElementAt(1 - 1) == upperBound : ownedMember.Count() > 1 && ownedMember.ElementAt(1 - 1) == lowerBound && ownedMember.ElementAt(2 - 1) == upperBound)
```
# DeriveDefinitionOwnedState
### OCL
``` OCL 
ownedState = ownedUsage->selectByKind(StateUsage)
```
### C#
``` CSharp 
ownedState == ownedUsage.OfType<StateUsage>()
```
# ValidateExposeOwningNamespace
### OCL
``` OCL 
importOwningNamespace.oclIsType(ViewUsage)
```
### C#
``` CSharp 
importOwningNamespace.oclIsType(ViewUsage)
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
isComposite && owningType != null && ((!((owningType is Structure) || (owningType is Feature) && ((Feature)owningType).type.Any(it => (Structure))) || specializesFromLibrary("Objects::Object::ownedPerformance")))
```
# DeriveConjugatedPortTypingPortDefinition
### OCL
``` OCL 
portDefinition = conjugatedPortDefinition.originalPortDefinition
```
### C#
``` CSharp 
portDefinition == conjugatedPortDefinition.originalPortDefinition
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
state = action->selectByKind(StateUsage)
```
### C#
``` CSharp 
state == action.OfType<StateUsage>()
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
ownedAllocation = ownedUsage->selectByKind(AllocationUsage)
```
### C#
``` CSharp 
ownedAllocation == ownedUsage.OfType<AllocationUsage>()
```
# DeriveExpressionResult
### OCL
``` OCL 
result =
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
result == resultParams is (Feature) == featureMemberships.OfType<ReturnParameterMembership>()
```
# CheckFeatureObjectSpecialization
### OCL
``` OCL 
ownedTyping.type->exists(selectByKind(Structure)) implies
    specializesFromLibary('Objects::objects')
```
### C#
``` CSharp 
(!(ownedTyping.type.Any(it => selectByKind(Structure))) || specializesFromLibary("Objects::objects"))
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
# DeriveIfActionUsageThenAction
### OCL
``` OCL 
thenAction = 
    let parameter : Feature = inputParameter(2) in
    if parameter <> null and parameter.oclIsKindOf(ActionUsage) then
        parameter.oclAsType(ActionUsage)
    else
        null
    endif
```
### C#
``` CSharp 
thenAction == parameter is Feature == inputParameter(2)
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
ownedSpecialization = ownedRelationship->selectByKind(Specialization)->
    select(s | s.special = self)
```
### C#
``` CSharp 
ownedSpecialization == ownedRelationship.OfType<Specialization>().Where(s => s.special == self)
```
# DeriveDefinitionOwnedViewpoint
### OCL
``` OCL 
ownedViewpoint = ownedUsage->selectByKind(ViewpointUsage)
```
### C#
``` CSharp 
ownedViewpoint == ownedUsage.OfType<ViewpointUsage>()
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
ownedSubsetting = ownedSpecialization->selectByKind(Subsetting)
```
### C#
``` CSharp 
ownedSubsetting == ownedSpecialization.OfType<Subsetting>()
```
# DeriveUsageNestedRendering
### OCL
``` OCL 
nestedRendering = nestedUsage->selectByKind(RenderingUsage)
```
### C#
``` CSharp 
nestedRendering == nestedUsage.OfType<RenderingUsage>()
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
includedUseCase = ownedUseCase->
    selectByKind(IncludeUseCaseUsage).
    useCaseIncluded
```
### C#
``` CSharp 
includedUseCase == ownedUseCase.OfType<IncludeUseCaseUsage>()
```
# DeriveMetadataAccessExpressionReferencdElement
### OCL
``` OCL 
referencedElement =
    let elements : Sequence(Element) = ownedMembership->
        reject(oclIsKindOf(FeatureMembership)).memberElement in
    if elements->isEmpty() then null
    else elements->first()
    endif
```
### C#
``` CSharp 
referencedElement == elements is (Element) == ownedMembership.Where(it => !((FeatureMembership))).memberElement
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
ownedSubclassification = 
    ownedSpecialization->selectByKind(Subclassification)
```
### C#
``` CSharp 
ownedSubclassification == ownedSpecialization.OfType<Subclassification>()
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
kind = TriggerKind::when implies
    argument->notEmpty() and
    argument->at(1).oclIsKindOf(FeatureReferenceExpression) and
    let referent : Feature = 
        argument->at(1).oclAsType(FeatureReferenceExpression).referent in
    referent.oclIsKindOf(Expression) and
    referent.oclAsType(Expression).result.specializesFromLibrary('ScalarValues::Boolean')
```
### C#
``` CSharp 
(!(kind == TriggerKind.when) || argument.Any()) && (argument.ElementAt(1 - 1) is FeatureReferenceExpression) && referent is Feature == ((FeatureReferenceExpression)argument.ElementAt(1 - 1)).referent
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
payloadFeature =
    let payloadFeatures : Sequence(PayloadFeature) =
        ownedFeature->selectByKind(PayloadFeature) in
    if payloadFeatures->isEmpty() then null
    else payloadFeatures->first()
    endif
```
### C#
``` CSharp 
payloadFeature == payloadFeatures is (PayloadFeature) == ownedFeature.OfType<PayloadFeature>()
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
targetConnector.OfType<Succession>().All(it => subsetsChain(self, resolveGlobal("ControlPerformances::MergePerformance::incomingHBLink")))
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
succession = ownedMember->selectByKind(Succession)->at(1)
```
### C#
``` CSharp 
succession == ownedMember.OfType<Succession>().ElementAt(1 - 1)
```
# DeriveDefinitionOwnedMetadata
### OCL
``` OCL 
ownedMetadata = ownedUsage->selectByKind(MetadataUsage)
```
### C#
``` CSharp 
ownedMetadata == ownedUsage.OfType<MetadataUsage>()
```
# DeriveRequirementUsageStakeholderParameter
### OCL
``` OCL 
stakeholderParameter = featureMembership->
    selectByKind(AStakholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
stakeholderParameter == featureMembership.OfType<AStakholderMembership>()
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
targetFeature =
    if relatedFeature->size() < 2 then OrderedSet{}
    else 
        relatedFeature->
            subSequence(2, relatedFeature->size())->
            asOrderedSet()
    endif
```
### C#
``` CSharp 
targetFeature == relatedFeature.Count() < 2
```
# DeriveUsageNestedRequirement
### OCL
``` OCL 
nestedRequirement = nestedUsage->selectByKind(RequirementUsage)
```
### C#
``` CSharp 
nestedRequirement == nestedUsage.OfType<RequirementUsage>()
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
targetConnector.OfType<Succession>().Select(it => connectorEnd.ElementAt(1 - 1)).All(sourceMult => multiplicityHasBounds(sourceMult, 0, 1))
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
entryAction =
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
entryAction == entryMemberships is (StateSubactionMembership) == ownedMembership.OfType<StateSubactionMembership>().Where(it => kind == StateSubactionKind.entry)
```
# DeriveDefinitionOwnedInterface
### OCL
``` OCL 
ownedInterface = ownedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
ownedInterface == ownedUsage.OfType<ReferenceUsage>()
```
# DeriveDefinitionOwnedVerificationCase
### OCL
``` OCL 
ownedVerificationCase = ownedUsage->selectByKind(VerificationCaseUsage)
```
### C#
``` CSharp 
ownedVerificationCase == ownedUsage.OfType<VerificationCaseUsage>()
```
# DeriveViewDefinitionSatisfiedViewpoint
### OCL
``` OCL 
satisfiedViewpoint = ownedRequirement->
    selectByKind(ViewpointUsage)->
    select(isComposite)
```
### C#
``` CSharp 
satisfiedViewpoint == ownedRequirement.OfType<ViewpointUsage>().Where(it => isComposite)
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
(!(isOwnedCrossFeature()) || ownedSubsetting.subsettedFeature.IsSupersetOf(((Feature)owner).ownedRedefinition.redefinedFeature.Where(it => crossFeature != null).crossFeature))
```
# DeriveFeatureOwnedTyping
### OCL
``` OCL 
ownedTyping = ownedGeneralization->selectByKind(FeatureTyping)
```
### C#
``` CSharp 
ownedTyping == ownedGeneralization.OfType<FeatureTyping>()
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
ownedConcern = ownedUsage->selectByKind(ConcernUsage)
```
### C#
``` CSharp 
ownedConcern == ownedUsage.OfType<ConcernUsage>()
```
# CheckFeatureReferenceExpressionResultSpecialization
### OCL
``` OCL 
result.owningType() = self and result.specializes(referent)
```
### C#
``` CSharp 
result.owningType() == self && result.specializes(referent)
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
nestedConnection = nestedUsage->selectByKind(ConnectorAsUsage)
```
### C#
``` CSharp 
nestedConnection == nestedUsage.OfType<ConnectorAsUsage>()
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
includedUseCase = ownedUseCase->
    selectByKind(IncludeUseCaseUsage).
    useCaseIncluded
```
### C#
``` CSharp 
includedUseCase == ownedUseCase.OfType<IncludeUseCaseUsage>()
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
sourceConnector.selectAsKind(Succession).selectAsKind.Select(it => connectorEnd.ElementAt(2 - 1)).All(targetMult => multiplicityHasBounds(targetMult, 0, 1))
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
(!(owningType != null && (owningType is FlowEnd) && owningType.ownedFeature.ElementAt(1 - 1) == self) || flowType is Type == owningType.owningType)
```
# DeriveUsageNestedViewpoint
### OCL
``` OCL 
nestedViewpoint = nestedUsage->selectByKind(ViewpointUsage)
```
### C#
``` CSharp 
nestedViewpoint == nestedUsage.OfType<ViewpointUsage>()
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
isLibraryElement = libraryNamespace() <> null
```
### C#
``` CSharp 
isLibraryElement == libraryNamespace() != null
```
# DeriveFlowTargetInputFeature
### OCL
``` OCL 
targetInputFeature =
    if connectorEnd->size() < 2 or 
        connectorEnd->at(2).ownedFeature->isEmpty()
    then null
    else connectorEnd->at(2).ownedFeature->first()
    endif
```
### C#
``` CSharp 
targetInputFeature == connectorEnd.Count() < 2 || !connectorEnd.ElementAt(2 - 1).Any()
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
ownedSpecialization.general.All(it => !(Class) && !(Association))
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
ownedUseCase = ownedUsage->selectByKind(UseCaseUsage)
```
### C#
``` CSharp 
ownedUseCase == ownedUsage.OfType<UseCaseUsage>()
```
# ValidateCollectExpressionOperator
### OCL
``` OCL 
operator = 'collect'
```
### C#
``` CSharp 
operator == "collect"
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
ownedPart = ownedUsage->selectByKind(PartUsage)
```
### C#
``` CSharp 
ownedPart == ownedUsage.OfType<PartUsage>()
```
# DeriveDefinitionVariant
### OCL
``` OCL 
variant = variantMembership.ownedVariantUsage
```
### C#
``` CSharp 
variant == variantMembership.ownedVariantUsage
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
exitAction =
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
exitAction == exitMemberships is (StateSubactionMembership) == ownedMembership.OfType<StateSubactionMembership>().Where(it => kind == StateSubactionKind.exit)
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
lowerBound =
    let ownedExpressions : Sequence(Expression) =
        ownedMember->selectByKind(Expression) in
    if ownedExpressions->size() < 2 then null
    else ownedExpressions->first()
    endif
```
### C#
``` CSharp 
lowerBound == ownedExpressions is (Expression) == ownedMember.OfType<Expression>()
```
# DeriveDefinitionOwnedOccurrence
### OCL
``` OCL 
ownedOccurrence = ownedUsage->selectByKind(OccurrenceUsage)
```
### C#
``` CSharp 
ownedOccurrence == ownedUsage.OfType<OccurrenceUsage>()
```
# ValidateIndexExpressionOperator
### OCL
``` OCL 
operator = '#'
```
### C#
``` CSharp 
operator == "#"
```
# DeriveAssignmentActionUsageReferent
### OCL
``` OCL 
referent =
    let unownedFeatures : Sequence(Feature) = ownedMembership->
        reject(oclIsKindOf(FeatureMembership)).memberElement->
        selectByKind(Feature) in
    if unownedFeatures->isEmpty() then null
    else unownedFeatures->first().oclAsType(Feature)
    endif
```
### C#
``` CSharp 
referent == unownedFeatures is (Feature) == ownedMembership.Where(it => !((FeatureMembership))).memberElement.OfType<Feature>()
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
ownedEnumeration = ownedUsage->selectByKind(EnumerationUsage)
```
### C#
``` CSharp 
ownedEnumeration == ownedUsage.OfType<EnumerationUsage>()
```
# DeriveRequirementUsageActorParameter
### OCL
``` OCL 
actorParameter = featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
actorParameter == featureMembership.OfType<ActorMembership>()
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
resultExpression =
    let results : OrderedSet(ResultExpressionMembership) =
        featureMembersip->
            selectByKind(ResultExpressionMembership) in
    if results->isEmpty() then null
    else results->first().ownedResultExpression
    endif
```
### C#
``` CSharp 
resultExpression == results is (ResultExpressionMembership) == featureMembersip.OfType<ResultExpressionMembership>()
```
# DeriveRequirementUsageAssumedConstraint
### OCL
``` OCL 
assumedConstraint = ownedFeatureMembership->
    selectByKind(RequirementConstraintMembership)->
    select(kind = RequirementConstraintKind::assumption).
    ownedConstraint
```
### C#
``` CSharp 
assumedConstraint == ownedFeatureMembership.OfType<RequirementConstraintMembership>().Where(it => kind == RequirementConstraintKind.assumption).ownedConstraint
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
upperBound =
    let ownedExpressions : Sequence(Expression) =
        ownedMember->selectByKind(Expression) in
    if ownedExpressions->isEmpty() then null
    else if ownedExpressions->size() = 1 then ownedExpressions->at(1)
    else ownedExpressions->at(2)
    endif endif
```
### C#
``` CSharp 
upperBound == ownedExpressions is (Expression) == ownedMember.OfType<Expression>()
```
# DeriveStateUsageEntryAction
### OCL
``` OCL 
entryAction =
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
entryAction == entryMemberships is (StateSubactionMembership) == ownedMembership.OfType<StateSubactionMembership>().Where(it => kind == StateSubactionKind.entry)
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
importedElement = importedMembership.memberElement
```
### C#
``` CSharp 
importedElement == importedMembership.memberElement
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
(!(isEnd) || multiplicities().allSuperTypes().flatten().flatten.OfType<MultiplicityRange>().Any(it => hasBounds(1, 1)))
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
((Type)redefiningFeature.resolveGlobal("Base::Anything").modelElement).Select(anythingType => (redefiningFeature.isVariable ? new HashSet<dynamic> { redefiningFeature.owningType } : redefiningFeature.featuringTypes.ToHashSet().Append(anythingType)).Select(redefiningFeaturingTypes => (redefinedFeature.isVariable ? new HashSet<dynamic> { redefinedFeature.owningType } : redefinedFeature.featuringTypes.ToHashSet().Append(anythingType)).Select(redefinedFeaturingTypes => redefiningFeaturingTypes != redefinedFeaturingType)))
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
bound =
    if upperBound = null then Sequence{}
    else if lowerBound = null then Sequence{upperBound}
    else Sequence{lowerBound, upperBound}
    endif endif
```
### C#
``` CSharp 
bound == upperBound == null
```
# DeriveEventOccurrenceUsageEventOccurrence
### OCL
``` OCL 
eventOccurrence =
    if referencedFeatureTarget() = null then self
    else if referencedFeatureTarget().oclIsKindOf(OccurrenceUsage) then
        referencedFeatureTarget().oclAsType(OccurrenceUsage)
    else null
    endif endif
```
### C#
``` CSharp 
eventOccurrence == referencedFeatureTarget() == null
```
# DeriveViewpointDefinitionViewpointStakeholder
### OCL
``` OCL 
viewpointStakeholder = framedConcern.featureMemberhsip->
    selectByKind(StakeholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
viewpointStakeholder == framedConcern.featureMemberhsip.OfType<StakeholderMembership>()
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
receiverArgument = argument(2)
```
### C#
``` CSharp 
receiverArgument == argument(2)
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
nestedCase = nestedUsage->selectByKind(CaseUsage)
```
### C#
``` CSharp 
nestedCase == nestedUsage.OfType<CaseUsage>()
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
text = documentation.body
```
### C#
``` CSharp 
text == documentation.body
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
ownedImport = ownedRelationship->selectByKind(Import)
```
### C#
``` CSharp 
ownedImport == ownedRelationship.OfType<Import>()
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
(!(isVariation) || !ownedSpecialization.specific.Any(it => (Definition) && (Definition).isVariation))
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
ownedRequirement = ownedUsage->selectByKind(RequirementUsage)
```
### C#
``` CSharp 
ownedRequirement == ownedUsage.OfType<RequirementUsage>()
```
# DeriveCaseUsageActorParameter
### OCL
``` OCL 
actorParameter = featureMembership->
    selectByKind(ActorMembership).
    ownedActorParameter
```
### C#
``` CSharp 
actorParameter == featureMembership.OfType<ActorMembership>()
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
nestedItem = nestedUsage->selectByKind(ItemUsage)
```
### C#
``` CSharp 
nestedItem == nestedUsage.OfType<ItemUsage>()
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
payloadArgument = argument(1)
```
### C#
``` CSharp 
payloadArgument == argument(1)
```
# DeriveElementTextualRepresentation
### OCL
``` OCL 
textualRepresentation = ownedElement->selectByKind(TextualRepresentation)
```
### C#
``` CSharp 
textualRepresentation == ownedElement.OfType<TextualRepresentation>()
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
intersectingType = ownedIntersecting.intersectingType
```
### C#
``` CSharp 
intersectingType == ownedIntersecting.intersectingType
```
# ValidateExposeVisibility
### OCL
``` OCL 
visibility = VisibilityKind::protected
```
### C#
``` CSharp 
visibility == VisibilityKind.protected
```
# ValidateMetadataAccessExpressionReferencedElement
### OCL
``` OCL 
ownedMembership->exists(not oclIsKindOf(FeatureMembership))
```
### C#
``` CSharp 
ownedMembership.Any(it => !(FeatureMembership))
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
viewCondition = ownedMembership->
    selectByKind(ElementFilterMembership).
    condition
```
### C#
``` CSharp 
viewCondition == ownedMembership.OfType<ElementFilterMembership>()
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
instantiatedType.input.Select(inp => ownedFeatures.Where(it => redefines(inp)).valuation.Where(v => v != null).value)
```
# DeriveDefinitionOwnedAction
### OCL
``` OCL 
ownedAction = ownedUsage->selectByKind(ActionUsage)
```
### C#
``` CSharp 
ownedAction == ownedUsage.OfType<ActionUsage>()
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
metaclass = 
    let metaclassTypes : Sequence(Type) = type->selectByKind(Metaclass) in
    if metaclassTypes->isEmpty() then null
    else metaClassTypes->first()
    endif
```
### C#
``` CSharp 
metaclass == metaclassTypes is (Type) == type.OfType<Metaclass>()
```
# DeriveTypeOutput
### OCL
``` OCL 
output = feature->select(f | 
    let direction: FeatureDirectionKind = directionOf(f) in
    direction = FeatureDirectionKind::out or
    direction = FeatureDirectionKind::inout)
```
### C#
``` CSharp 
output == feature.Where(f => directionOf(f).Select(direction => direction == FeatureDirectionKind.out || direction == FeatureDirectionKind.inout))
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
(!(isVariation) || !ownedSpecialization.specific.Any(it => (Definition) && (Definition).isVariation || (Usage) && (Usage).isVariation))
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
payloadParameter = 
 if parameter->isEmpty() then null
 else parameter->first() endif
```
### C#
``` CSharp 
payloadParameter == !parameter.Any()
```
# ValidateTransitionFeatureMembershipTriggerAction
### OCL
``` OCL 
kind = TransitionFeatureKind::trigger implies
    transitionFeature.oclIsKindOf(AcceptActionUsage)
```
### C#
``` CSharp 
(!(kind == TransitionFeatureKind.trigger) || (transitionFeature is AcceptActionUsage))
```
# DeriveExpressionIsModelLevelEvaluable
### OCL
``` OCL 
isModelLevelEvaluable = modelLevelEvaluable(Set(Element){})
```
### C#
``` CSharp 
isModelLevelEvaluable == modelLevelEvaluable((Element))
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
senderArgument = argument(2)
```
### C#
``` CSharp 
senderArgument == argument(2)
```
# DeriveOccurrenceUsageIndividualDefinition
### OCL
``` OCL 
individualDefinition =
    let individualDefinitions : OrderedSet(OccurrenceDefinition) = 
        occurrenceDefinition->
            selectByKind(OccurrenceDefinition)->
            select(isIndividual) in
    if individualDefinitions->isEmpty() then null
    else individualDefinitions->first() endif
```
### C#
``` CSharp 
individualDefinition == individualDefinitions is (OccurrenceDefinition) == occurrenceDefinition.OfType<OccurrenceDefinition>().Where(it => isIndividual)
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
(!(connectorEnds.Count() == 2 && association.Any(it => (AssociationStructure))) || specializesFromLibrary("Objects::binaryLinkObjects"))
```
# DeriveWhileLoopActionUsageUntilArgument
### OCL
``` OCL 
untilArgument =
    let parameter : Feature = inputParameter(3) in
    if parameter <> null and parameter.oclIsKindOf(Expression) then
        parameter.oclAsType(Expression)
    else
        null
    endif
```
### C#
``` CSharp 
untilArgument == parameter is Feature == inputParameter(3)
```
# DeriveFlowPayloadType
### OCL
``` OCL 
payloadType =
    if payloadFeature = null then Sequence{}
    else payloadFeature.type
    endif
```
### C#
``` CSharp 
payloadType == payloadFeature == null
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
verifiedRequirement =
    if objectiveRequirement = null then OrderedSet{}
    else 
        objectiveRequirement.featureMembership->
            selectByKind(RequirementVerificationMembership).
            verifiedRequirement->asOrderedSet()
    endif
```
### C#
``` CSharp 
verifiedRequirement == objectiveRequirement == null
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
sourceConnector.OfType<Succession>().All(it => subsetsChain(self, resolveGlobal("ControlPerformances::MergePerformance::outgoingHBLink")))
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
(!(isComposite && owningType != null && ((owningType is Class) || (owningType is OccurrenceUsage) || (owningType is Feature) && ((Feature)owningType).type.Any(it => oclIsKind(Class)))) || specializesFromLibrary("Occurrences::Occurrence::suboccurrences"))
```
# DeriveTransitionUsageTriggerAction
### OCL
``` OCL 
triggerAction = ownedFeatureMembership->
    selectByKind(TransitionFeatureMembership)->
    select(kind = TransitionFeatureKind::trigger).transitionFeature->
    selectByKind(AcceptActionUsage)
```
### C#
``` CSharp 
triggerAction == ownedFeatureMembership.OfType<TransitionFeatureMembership>().Where(it => kind == TransitionFeatureKind.trigger).transitionFeature.OfType<AcceptActionUsage>()
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
instantiatedType.feature.Where(it => visibility == VisibilityKind.public).Select(features => result.ownedFeature.All(f1 => result.ownedFeature.All(f2 => (!(f1 != f2) || !f1.ownedRedefinition.redefinedFeature.Intersect(f2.ownedRedefinition.redefinedFeature).Intersect(features).Any()))))
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
payloadArgument = argument(1)
```
### C#
``` CSharp 
payloadArgument == argument(1)
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
occurrenceDefinition.OfType<OccurrenceDefinition>().Where(it => isIndividual).Length <= 1
```
# DeriveTypeFeatureMembership
### OCL
``` OCL 
featureMembership = ownedFeatureMembership->union(
    inheritedMembership->selectByKind(FeatureMembership))
```
### C#
``` CSharp 
featureMembership == ownedFeatureMembership.Union(inheritedMembership.OfType<FeatureMembership>())
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
loopVariable =
    if ownedFeature->isEmpty() or 
        not ownedFeature->first().oclIsKindOf(ReferenceUsage) then 
        null
    else 
        ownedFeature->first().oclAsType(ReferenceUsage)
    endif
```
### C#
``` CSharp 
loopVariable == !ownedFeature.Any() || !(ownedFeature.First() is ReferenceUsage)
```
# DeriveDefinitionOwnedReference
### OCL
``` OCL 
ownedReference = ownedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
ownedReference == ownedUsage.OfType<ReferenceUsage>()
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
satisfyingFeature =
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
satisfyingFeature == bindings is BindingConnector == ownedMember.OfType<BindingConnector>().Where(b => b.relatedElement.Contains(subjectParameter))
```
# DeriveRequirementUsageSubjectParameter
### OCL
``` OCL 
subjectParameter =
    let subjects : OrderedSet(SubjectMembership) = 
        featureMembership->selectByKind(SubjectMembership) in
    if subjects->isEmpty() then null
    else subjects->first().ownedSubjectParameter
    endif
```
### C#
``` CSharp 
subjectParameter == subjects is (SubjectMembership) == featureMembership.OfType<SubjectMembership>()
```
# ValidateFramedConcernMembershipConstraintKind
### OCL
``` OCL 
kind = RequirementConstraintKind::requirement
```
### C#
``` CSharp 
kind == RequirementConstraintKind.requirement
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
flowEnd = connectorEnd->selectByKind(FlowEnd)
```
### C#
``` CSharp 
flowEnd == connectorEnd.OfType<FlowEnd>()
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
filterCondition = ownedMembership->
    selectByKind(ElementFilterMembership).condition
```
### C#
``` CSharp 
filterCondition == ownedMembership.OfType<ElementFilterMembership>()
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
ownedAnnotation = ownedRelationship->
    selectByKind(Annotation)->
    select(a | a.annotatedElement = self)
```
### C#
``` CSharp 
ownedAnnotation == ownedRelationship.OfType<Annotation>().Where(a => a.annotatedElement == self)
```
# ValidateSelectExpressionOperator
### OCL
``` OCL 
operator = 'select'
```
### C#
``` CSharp 
operator == "select"
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
kind = RequirementConstraintKind::requirement
```
### C#
``` CSharp 
kind == RequirementConstraintKind.requirement
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
nestedCalculation = nestedUsage->selectByKind(CalculationUsage)
```
### C#
``` CSharp 
nestedCalculation == nestedUsage.OfType<CalculationUsage>()
```
# DeriveIfActionUsageElseAction
### OCL
``` OCL 
elseAction = 
    let parameter : Feature = inputParameter(3) in
    if parameter <> null and parameter.oclIsKindOf(ActionUsage) then
        parameter.oclAsType(ActionUsage)
    else
        null
    endif
```
### C#
``` CSharp 
elseAction == parameter is Feature == inputParameter(3)
```
# ValidateForLoopActionUsageLoopVariable
### OCL
``` OCL 
ownedFeature->notEmpty() and
ownedFeature->at(1).oclIsKindOf(ReferenceUsage)
```
### C#
``` CSharp 
ownedFeature.Any() && (ownedFeature.ElementAt(1 - 1) is ReferenceUsage)
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
viewpointStakeholder = framedConcern.featureMemberhsip->
    selectByKind(StakeholderMembership).
    ownedStakeholderParameter
```
### C#
``` CSharp 
viewpointStakeholder == framedConcern.featureMemberhsip.OfType<StakeholderMembership>()
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
directedUsage = directedFeature->selectByKind(Usage)
```
### C#
``` CSharp 
directedUsage == directedFeature.OfType<Usage>()
```
# DeriveFeatureOwnedFeatureChaining
### OCL
``` OCL 
ownedFeatureChaining = ownedRelationship->selectByKind(FeatureChaining)
```
### C#
``` CSharp 
ownedFeatureChaining == ownedRelationship.OfType<FeatureChaining>()
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
# ValidateIfActionUsageParameters
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
seqArgument = argument(1)
```
### C#
``` CSharp 
seqArgument == argument(1)
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
nestedReference = nestedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp 
nestedReference == nestedUsage.OfType<ReferenceUsage>()
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
kind = TriggerKind::after implies
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
(!(kind == TriggerKind.after) || argument.Any()) && argument.ElementAt(1 - 1).specializesFromLibrary("Quantities::ScalarQuantityValue") && mRef is Element == resolveGlobal("Quantities::TensorQuantityValue::mRef").ownedMemberElement
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
ownedSpecialization.general.All(it => !(Behavior))
```
