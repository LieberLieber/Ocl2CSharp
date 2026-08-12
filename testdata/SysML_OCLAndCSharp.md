# ValidateEventOccurrenceUsageReference
### OCL
``` OCL
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(OccurrenceUsage)
```
### C#
``` CSharp
(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is OccurrenceUsage));
```
# DeriveCalculationUsageCalculation
### OCL
``` OCL
action->selectByKind(CalculationUsage)
```
### C#
``` CSharp
Action.OfType<CalculationUsage>();
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
(OwningType is RequirementDefinition) || (OwningType is RequiremenCaseRequirementDefinition) || (OwningType is CaseDefinition) || (OwningType is CaseUsage);
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
OwnedMembership.Reject(item => (item is ParameterMembership)).Select(nonParameterMemberships => (nonParameterMemberships.IsEmpty() || !(nonParameterMemberships.First().MemberElement is Feature) ? null : (nonParameterMemberships.First().MemberElement as Feature)));
```
# DeriveTypeOwnedIntersecting
### OCL
``` OCL
ownedRelationship->selectByKind(Intersecting)
```
### C#
``` CSharp
OwnedRelationship.OfType<Intersecting>();
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
(!(!(this is ConjugatedPortDefinition)) || OwnedMember.OfType<ConjugatedPortDefinition>().Count() == 1);
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
(!(Direction == null && OwnedSpecializations.All(item => item.IsImplied)) || OwnedMembership.OfType<FeatureValue>().All(fv => Specializes(fv.Value.Result)));
```
# DeriveFeatureOwnedTypeFeaturing
### OCL
``` OCL
ownedRelationship->selectByKind(TypeFeaturing)->
    select(tf | tf.featureOfType = self)
```
### C#
``` CSharp
OwnedRelationship.OfType<TypeFeaturing>().Where(tf => tf.FeatureOfType == this);
```
# CheckMetadataAccessExpressionSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::metadataAccessEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::metadataAccessEvaluations");
```
# ValidateConstructorExpressionOwnedFeatures
### OCL
``` OCL
ownedFeatures->excluding(result)->isEmpty()
```
### C#
``` CSharp
OwnedFeatures.Excluding(Result).IsEmpty();
```
# ValidateAssociationBinarySpecialization
### OCL
``` OCL
associationEnds->size() > 2 implies
    not specializesFromLibrary('Links::BinaryLink')
```
### C#
``` CSharp
(!(AssociationEnds.Count() > 2) || !SpecializesFromLibrary("Links::BinaryLink"));
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
FeatureMembership.OfType<SubjectMembership>().Select(subjectMems => (subjectMems.IsEmpty() ? null : subjectMems.First().OwnedSubjectParameter));
```
# DeriveDefinitionOwnedConnection
### OCL
``` OCL
ownedUsage->selectByKind(ConnectorAsUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ConnectorAsUsage>();
```
# DeriveDefinitionOwnedConstraint
### OCL
``` OCL
ownedUsage->selectByKind(ConstraintUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ConstraintUsage>();
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
(!(IsIndividual) || Multiplicity != null) && Multiplicity.SpecializesFromLibrary("Base::zeroOrOne");
```
# ValidateSpecificationSpecificNotConjugated
### OCL
``` OCL
not specific.isConjugated
```
### C#
``` CSharp
!Specific.IsConjugated;
```
# DeriveInstantiationExpressionInstantiatedType
### OCL
``` OCL
instantiatedType()
```
### C#
``` CSharp
InstantiatedType();
```
# ValidateConnectorBinarySpecialization
### OCL
``` OCL
connectorEnds->size() > 2 implies
    not specializesFromLibrary('Links::BinaryLink')
```
### C#
``` CSharp
(!(ConnectorEnds.Count() > 2) || !SpecializesFromLibrary("Links::BinaryLink"));
```
# DeriveElementOwnedElement
### OCL
``` OCL
ownedRelationship.ownedRelatedElement
```
### C#
``` CSharp
OwnedRelationship.OwnedRelatedElement;
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
(!(IsComposite && OwningType != null && ((OwningType is StateDefinition) || (OwningType is StateUsage)) && Source != null && (Source is StateUsage)) || SpecializesFromLibrary("States::StateAction::stateTransitions"));
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
FeatureMembership.OfType<FramedConcernMembership>().Select(item => item.OwnedConcern);
```
# CheckUsageVariationUsageSpecialization
### OCL
``` OCL
owningVariationUsage <> null implies
    specializes(owningVariationUsage)
```
### C#
``` CSharp
(!(OwningVariationUsage != null) || Specializes(OwningVariationUsage));
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
OwnedMembership.Reject(item => (item is ParameterMembership)).Select(nonParameterMemberships => (nonParameterMemberships.IsEmpty() || !(nonParameterMemberships.First().MemberElement is Feature) ? null : (nonParameterMemberships.First().MemberElement as Feature)));
```
# DeriveUsageNestedAllocation
### OCL
``` OCL
nestedUsage->selectByKind(AllocationUsage)
```
### C#
``` CSharp
NestedUsage.OfType<AllocationUsage>();
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
(!(IsParallel) || OwnedAction.IncomingTransition.IsEmpty()) && OwnedAction.OutgoingTransition.IsEmpty();
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
OwnedFeature.All(f => (!(f != Result) || f.Direction == FeatureDirectionKind.In));
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
(OwnedAnnotatingElement != null ? OwnedAnnotatingElement : OwningAnnotatingElement);
```
# DeriveStepBehavior
### OCL
``` OCL
type->selectByKind(Behavior)
```
### C#
``` CSharp
Type.OfType<Behavior>();
```
# ValidateSatisfyRequirementUsageReference
### OCL
``` OCL
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(RequirementUsage)
```
### C#
``` CSharp
(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is RequirementUsage));
```
# ValidateTypeOwnedIntersectingNotOne
### OCL
``` OCL
ownedIntersecting->size() <> 1
```
### C#
``` CSharp
OwnedIntersecting.Count() != 1;
```
# DeriveTypeInheritedFeature
### OCL
``` OCL
inheritedMemberships->
    selectByKind(FeatureMembership).memberFeature
```
### C#
``` CSharp
InheritedMemberships.OfType<FeatureMembership>().Select(item => item.MemberFeature);
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
OwnedImport.OfType<Expose>().ImportedMemberships(new HashSet<dynamic> {  }).MemberElement.Where(elm => IncludeAsExposed(elm)).AsOrderedSet();
```
# DeriveViewDefinitionView
### OCL
``` OCL
usage->selectByKind(ViewUsage)
```
### C#
``` CSharp
Usage.OfType<ViewUsage>();
```
# CheckAnalysisCaseUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('AnalysisCases::analysisCases')
```
### C#
``` CSharp
SpecializesFromLibrary("AnalysisCases::analysisCases");
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
(!(IsComposite && OwningType != null && ((OwningType is VerificationCaseDefinition) || (OwningType is VerificationCaseUsage))) || SpecializesFromLibrary("VerificationCases::VerificationCase::subVerificationCases"));
```
# DeriveNamespaceOwnedMembership
### OCL
``` OCL
ownedRelationship->selectByKind(Membership)
```
### C#
``` CSharp
OwnedRelationship.OfType<Membership>();
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
(!(TriggerAction.NotEmpty()) || InputParameter(2).Select(payloadParameter => payloadParameter != null && payloadParameter.SubsetsChain(TriggerAction.ElementAt(0), TriggerPayloadParameter())));
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
FeatureWithValue.Redefinition.RedefinedFeature.Closure(item => item.Redefinition.RedefinedFeature).Valuation.All(item => item.IsDefault);
```
# ValidateFlowEndOwningType
### OCL
``` OCL
owningType <> null and owningType.oclIsKindOf(Flow)
```
### C#
``` CSharp
OwningType != null && (OwningType is Flow);
```
# CheckPayloadFeatureRedefinition
### OCL
``` OCL
redefinesFromLibrary('Transfers::Transfer::payload')
```
### C#
``` CSharp
RedefinesFromLibrary("Transfers::Transfer::payload");
```
# DeriveNamespaceImportImportedElement
### OCL
``` OCL
importedNamespace
```
### C#
``` CSharp
ImportedNamespace;
```
# DeriveElementName
### OCL
``` OCL
effectiveName()
```
### C#
``` CSharp
EffectiveName();
```
# ValidateUsageVariationOwnedFeatureMembership
### OCL
``` OCL
isVariation implies ownedFeatureMembership->isEmpty()
```
### C#
``` CSharp
(!(IsVariation) || OwnedFeatureMembership.IsEmpty());
```
# ValidateSubsettingUniquenessConformance
### OCL
``` OCL
subsettedFeature.isUnique implies subsettingFeature.isUnique
```
### C#
``` CSharp
(!(SubsettedFeature.IsUnique) || SubsettingFeature.IsUnique);
```
# DeriveUsageNestedVerificationCase
### OCL
``` OCL
nestedUsage->selectByKind(VerificationCaseUsage)
```
### C#
``` CSharp
NestedUsage.OfType<VerificationCaseUsage>();
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
(!(OwningFeatureMembership != null && (OwningFeatureMembership is RequirementVerificationMembership)) || SpecializesFromLibrary("VerificationCases::VerificationCase::obj::requirementVerifications"));
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
OwnedRelatedElement.OfType<AnnotatingElement>().Select(ownedAnnotatingElements => (ownedAnnotatingElements.IsEmpty() ? null : ownedAnnotatingElements.First()));
```
# DeriveAllocationDefinitionAllocation
### OCL
``` OCL
usage->selectAsKind(AllocationUsage)
```
### C#
``` CSharp
Usage.OfType<AllocationUsage>();
```
# ValidateConnectorRelatedFeatures
### OCL
``` OCL
not isAbstract implies relatedFeature->size() >= 2
```
### C#
``` CSharp
(!(!IsAbstract) || RelatedFeature.Count() >= 2);
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
OwningType != null && OwningType.SpecializesFromLibrary("Occurrences::Occurrence") && !(IsPortion || SpecializesFromLibrary("Links::SelfLink") || SpecializesFromLibrary("Occurrences::HappensLink") || IsComposite && SpecializesFromLibrary("Actions::Action"));
```
# ValidateFeatureOwnedCrossSubsetting
### OCL
``` OCL
ownedSubsetting->selectByKind(CrossSubsetting)->size() <= 1
```
### C#
``` CSharp
OwnedSubsetting.OfType<CrossSubsetting>().Count() <= 1;
```
# CheckAllocationUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Allocations::allocations')
```
### C#
``` CSharp
SpecializesFromLibrary("Allocations::allocations");
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
(OwningMembership == null ? null : OwningMembership.MembershipOwningNamespace);
```
# DeriveUsageNestedPart
### OCL
``` OCL
nestedUsage->selectByKind(PartUsage)
```
### C#
``` CSharp
NestedUsage.OfType<PartUsage>();
```
# DeriveNamespaceMembers
### OCL
``` OCL
membership.memberElement
```
### C#
``` CSharp
Membership.MemberElement;
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
(!(OwningType != null && ((OwningType is PartDefinition) || (OwningType is PartUsage))) || SpecializesFromLibrary("Parts::Part::exhibitedStates"));
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
OwnedRelationship.OfType<Conjugation>().Select(ownedConjugators => (ownedConjugators.IsEmpty() ? null : ownedConjugators.ElementAt(0)));
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
(OwningType is RequirementUsage) || (OwningType is RequirementDefinition) || (OwningType is CaseDefinition) || (OwningType is CaseUsage);
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
(ObjectiveRequirement == null ? new List<dynamic> {  } : ObjectiveRequirement.FeatureMembership.OfType<RequirementVerificationMembership>().Select(item => item.VerifiedRequirement).AsOrderedSet());
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
(!(OwningFeatureMembership != null && (OwningFeatureMembership is FramedConcernMembership)) || SpecializesFromLibrary("Requirements::RequirementCheck::concerns"));
```
# ValidateBehaviorSpecialization
### OCL
``` OCL
ownedSpecialization.general->forAll(not oclIsKindOf(Structure))
```
### C#
``` CSharp
OwnedSpecialization.General.All(item => !(item is Structure));
```
# CheckUseCaseUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('UseCases::useCases')
```
### C#
``` CSharp
SpecializesFromLibrary("UseCases::useCases");
```
# CheckFlowSpecialization
### OCL
``` OCL
specializesFromLibrary('Transfers::transfers')
```
### C#
``` CSharp
SpecializesFromLibrary("Transfers::transfers");
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
OwnedMembership.OfType<ElementFilterMembership>().Select(item => item.Condition);
```
# DeriveOwningMembershipOwnedMemberName
### OCL
``` OCL
ownedMemberElement.name
```
### C#
``` CSharp
OwnedMemberElement.Name;
```
# DeriveOwningMembershipOwnedMemberShortName
### OCL
``` OCL
ownedMemberElement.shortName
```
### C#
``` CSharp
OwnedMemberElement.ShortName;
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
(!(OwningFeatureMembership != null && (OwningFeatureMembership is StakeholderMembership)) || SpecializesFromLibrary("Requirements::RequirementCheck::stakeholders"));
```
# CheckRenderingUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Views::renderings')
```
### C#
``` CSharp
SpecializesFromLibrary("Views::renderings");
```
# CheckConnectorObjectSpecialization
### OCL
``` OCL
association->exists(oclIsKindOf(AssociationStructure)) implies
    specializesFromLibrary('Objects::linkObjects')
```
### C#
``` CSharp
(!(Association.Exists(item => (item is AssociationStructure))) || SpecializesFromLibrary("Objects::linkObjects"));
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
OwnedRendering.ReferencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? OwnedRendering : ((referencedFeature is RenderingUsage) ? (RefrencedFeature as RenderingUsage) : null)));
```
# ValidateFeatureOwnedReferenceSubsetting
### OCL
``` OCL
ownedSubsetting->selectByKind(ReferenceSubsetting)->size() <= 1
```
### C#
``` CSharp
OwnedSubsetting.OfType<ReferenceSubsetting>().Count() <= 1;
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
(!(CrossFeature != null) || OwnedRedefinition.RedefinedFeature.CrossFeature.All(f => (!(f != null) || CrossFeature.Specializes(f))));
```
# ValidateExpressionResultExpressionMembership
### OCL
``` OCL
membership->selectByKind(ResultExpressionMembership)->size() <= 1
```
### C#
``` CSharp
Membership.OfType<ResultExpressionMembership>().Count() <= 1;
```
# CheckItemDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Items::Item')
```
### C#
``` CSharp
SpecializesFromLibrary("Items::Item");
```
# CheckForLoopActionUsageVarRedefinition
### OCL
``` OCL
loopVariable <> null and
loopVariable.redefinesFromLibrary('Actions::ForLoopAction::var')
```
### C#
``` CSharp
LoopVariable != null && LoopVariable.RedefinesFromLibrary("Actions::ForLoopAction::var");
```
# DeriveMembershipMemberElementId
### OCL
``` OCL
memberElement.elementId
```
### C#
``` CSharp
MemberElement.ElementId;
```
# ValidateAnnotationAnnotatedElementOwnership
### OCL
``` OCL
(owningAnnotatedElement <> null) = (ownedAnnotatingElement <> null)
```
### C#
``` CSharp
(OwningAnnotatedElement != null) == (OwnedAnnotatingElement != null);
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
InputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.OwnedFeature.NotEmpty() && targetParameter.First().OwnedFeature.NotEmpty() && targetParameter.First().OwnedFeature.First().Redefines("AssigmentAction::target::startingAt::accessedFeature"));
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
OwnedMember.OfType<Succession>().Select(successions => successions.NotEmpty() && successions.ElementAt(0).TargetFeature.FeatureTarget.All(item => (item is ActionUsage)));
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
(!(OwningfeatureMembership != null && (OwningfeatureMembership is ObjectiveMembership)) || OwningType.OwnedSpecialization.General.All(gen => ((!((gen is CaseDefinition)) || Redefines((gen as CaseDefinition).ObjectiveRequirement))) && ((!((gen is CaseUsage)) || Redefines((gen as CaseUsage).ObjectiveRequirement)))));
```
# CheckTransitionUsageSuccessionSourceSpecialization
### OCL
``` OCL
succession.sourceFeature = source
```
### C#
``` CSharp
Succession.SourceFeature == Source;
```
# ValidatePerformActionUsageReference
### OCL
``` OCL
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(ActionUsage)
```
### C#
``` CSharp
(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is ActionUsage));
```
# CheckDecisionNodeSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::Action::decisions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::Action::decisions");
```
# DeriveConnectorRelatedFeature
### OCL
``` OCL
connectorEnd.ownedReferenceSubsetting->
    select(s | s <> null).subsettedFeature
```
### C#
``` CSharp
ConnectorEnd.OwnedReferenceSubsetting.Where(s => s != null).SubsettedFeature;
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
(RelatedFeature.IsEmpty() ? null : RelatedFeature.First());
```
# ValidateRequirementDefinitionSubjectParameterPosition
### OCL
``` OCL
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp
Input.NotEmpty() && Input.First() == SubjectParameter;
```
# CheckFeatureOwnedCrossFeatureSpecialization
### OCL
``` OCL
isOwnedCrossFeature() implies
    owner.oclAsType(Feature).type->forAll(t | self.specializes(t))
```
### C#
``` CSharp
(!(IsOwnedCrossFeature()) || (Owner as Feature).Type.All(t => this.Specializes(t)));
```
# ValidateEndFeatureMembershipIsEnd
### OCL
``` OCL
ownedMemberFeature.isEnd
```
### C#
``` CSharp
OwnedMemberFeature.IsEnd;
```
# CheckSelectExpressionResultSpecialization
### OCL
``` OCL
arguments->notEmpty() implies
    result.specializes(arguments->first().result)
```
### C#
``` CSharp
(!(Arguments.NotEmpty()) || Result.Specializes(Arguments.First().Result));
```
# ValidateFlowEndNestedFeature
### OCL
``` OCL
ownedFeature->size() = 1
```
### C#
``` CSharp
OwnedFeature.Count() == 1;
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
(!(IsComposite && OwningType != null && ((OwningType is CaseDefinition) || (OwningType is CaseUsage))) || SpecializesFromLibrary("Cases::Case::subcases"));
```
# DeriveUsageNestedConcern
### OCL
``` OCL
nestedUsage->selectByKind(ConcernUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ConcernUsage>();
```
# DeriveActionDefinitionAction
### OCL
``` OCL
usage->selectByKind(ActionUsage)
```
### C#
``` CSharp
Usage.OfType<ActionUsage>();
```
# CheckVerificationCaseSpecialization
### OCL
``` OCL
specializesFromLibrary('VerificationCases::VerificationCase')
```
### C#
``` CSharp
SpecializesFromLibrary("VerificationCases::VerificationCase");
```
# DeriveDefinitionOwnedFlow
### OCL
``` OCL
ownedUsage->selectByKind(FlowConnectionUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<FlowConnectionUsage>();
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
(!(IsComposite && OwningType != null && ((OwningType is ActionDefinition) || (OwningType is ActionUsage)) && Source != null && !(Source is StateUsage)) || SpecializesFromLibrary("Actions::Action::decisionTransitions"));
```
# DeriveUsageNestedOccurrence
### OCL
``` OCL
nestedUsage->selectByKind(OccurrenceUsage)
```
### C#
``` CSharp
NestedUsage.OfType<OccurrenceUsage>();
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
(OwningType is RequirementUsage) && OwningType.OwningFeatureMembership != null && (OwningType.OwningFeatureMembership is ObjectiveMembership);
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
FeatureMembership.OfType<SubjectMembership>().Select(subjects => (subjects.IsEmpty() ? null : subjects.First().OwnedSubjectParameter));
```
# DeriveFeatureOwnedRedefinition
### OCL
``` OCL
ownedSubsetting->selectByKind(Redefinition)
```
### C#
``` CSharp
OwnedSubsetting.OfType<Redefinition>();
```
# CheckAcceptActionUsageSpecialization
### OCL
``` OCL
not isTriggerAction() implies
    specializesFromLibrary('Actions::acceptActions')
```
### C#
``` CSharp
(!(!IsTriggerAction()) || SpecializesFromLibrary("Actions::acceptActions"));
```
# CheckOccurrenceUsageSnapshotSpecialization
### OCL
``` OCL
portionKind = PortionKind::snapshot implies
    specializesFromLibrary('Occurrences::Occurrence::snapshots')
```
### C#
``` CSharp
(!(PortionKind == PortionKind.Snapshot) || SpecializesFromLibrary("Occurrences::Occurrence::snapshots"));
```
# DeriveDefinitionOwnedUsage
### OCL
``` OCL
ownedFeature->selectByKind(Usage)
```
### C#
``` CSharp
OwnedFeature.OfType<Usage>();
```
# DeriveUsageNestedConstraint
### OCL
``` OCL
nestedUsage->selectByKind(ConstraintUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ConstraintUsage>();
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
OwnedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => item.Kind == RequirementConstraintKind.Requirement).OwnedConstraint;
```
# DeriveUsageNestedFlow
### OCL
``` OCL
nestedUsage->selectByKind(FlowConnectionUsage)
```
### C#
``` CSharp
NestedUsage.OfType<FlowConnectionUsage>();
```
# DeriveBehaviorStep
### OCL
``` OCL
feature->selectByKind(Step)
```
### C#
``` CSharp
Feature.OfType<Step>();
```
# DeriveTypeDirectedFeature
### OCL
``` OCL
feature->select(f | directionOf(f) <> null)
```
### C#
``` CSharp
Feature.Where(f => DirectionOf(f) != null);
```
# DeriveUsageNestedState
### OCL
``` OCL
nestedUsage->selectByKind(StateUsage)
```
### C#
``` CSharp
NestedUsage.OfType<StateUsage>();
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
(!(Kind == TransitionFeatureKind.Guard) || (TransitionFeature is Expression)) && (TransitionFeature is Expression).Select(guard => guard.Result.SpecializesFromLibrary("ScalarValues::Boolean") && guard.Result.Multiplicity != null && guard.Result.Multiplicity.HasBounds(1, 1));
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
OwnedSubsetting.OfType<ReferenceSubsetting>().Select(referenceSubsettings => (referenceSubsettings.IsEmpty() ? null : referenceSubsettings.First()));
```
# ValidateExhibitStateUsageReference
### OCL
``` OCL
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(StateUsage)
```
### C#
``` CSharp
(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is StateUsage));
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
(InstantiatedType is Behavior) || (InstantiatedType is Feature) && InstantiatedType.Type.Exists(item => (item is Behavior)) && InstantiatedType.Type.Count() == 1;
```
# ValidateFeatureChainingFeatureNotOne
### OCL
``` OCL
chainingFeature->size() <> 1
```
### C#
``` CSharp
ChainingFeature.Count() != 1;
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
OwnedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => item.Kind == RequirementConstraintKind.Assumption).OwnedConstraint;
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
TargetConnector.OfType<Succession>().Select(item => ConnectorEnd.ElementAt(1).Multiplicity).All(targetMult => MultiplicityHasBounds(targetMult, 1, 1));
```
# DeriveDefinitionVariantMembership
### OCL
``` OCL
ownedMembership->selectByKind(VariantMembership)
```
### C#
``` CSharp
OwnedMembership.OfType<VariantMembership>();
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
(IsNegated ? SpecializesFromLibrary("Constraints::negatedConstraintChecks") : SpecializesFromLibrary("Constraints::assertedConstraintChecks"));
```
# CheckItemUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Items::items')
```
### C#
``` CSharp
SpecializesFromLibrary("Items::items");
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
(OwnedCrossSubsetting == null ? null : (OwnedCrossSubsetting.CrossedFeature.ChainingFeature).Select(chainingFeatures => (chainingFeatures.Count() < 2 ? null : chainingFeatures.ElementAt(1))));
```
# ValidateTypeOwnedUnioningNotOne
### OCL
``` OCL
ownedUnioning->size() <> 1
```
### C#
``` CSharp
OwnedUnioning.Count() != 1;
```
# CheckFeatureCrossingSpecialization
### OCL
``` OCL
ownedCrossFeature() <> null implies
    crossFeature = ownedCrossFeature()
```
### C#
``` CSharp
(!(OwnedCrossFeature() != null) || CrossFeature == OwnedCrossFeature());
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
OwningType != null && ((OwningType is ActionDefinition) || (OwningType is ActionUsage));
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
OwnedMembership.Exists(item => !(item is OwningMembership) && (MemberElement is Feature));
```
# CheckUseCaseDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('UseCases::UseCase')
```
### C#
``` CSharp
SpecializesFromLibrary("UseCases::UseCase");
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
Featuring.Type.AsOrderedSet().Select(featuringTypes => (ChainingFeature.IsEmpty() ? featuringTypes : featuringTypes.Union(ChainingFeature.First().FeaturingType).AsOrderedSet()));
```
# CheckLiteralIntegerSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::literalIntegerEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::literalIntegerEvaluations");
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
FeatureMembership.OfType<ActorMembership>().Select(item => item.OwnedActorParameter);
```
# ValidateAssignmentActionUsage
### OCL
``` OCL
referent <> null implies referent.featureTarget.mayTimeVary
```
### C#
``` CSharp
(!(Referent != null) || Referent.FeatureTarget.MayTimeVary);
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
(!(IsComposite && OwningType != null && ((OwningType is PortDefinition) || (OwningType is PortUsage))) || SpecializesFromLibrary("Ports::Port::subports"));
```
# CheckSuccessionFlowUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Flows::successionFlows')
```
### C#
``` CSharp
SpecializesFromLibrary("Flows::successionFlows");
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
(!(IsComposite && OwningType != null && ((OwningType is RequirementDefinition) || (OwningType is RequirementUsage))) || SpecializesFromLibrary("Requirements::RequirementCheck::subrequirements"));
```
# DeriveUsageVariantMembership
### OCL
``` OCL
ownedMembership->selectByKind(VariantMembership)
```
### C#
``` CSharp
OwnedMembership.OfType<VariantMembership>();
```
# CheckTerminateActionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::terminateSubactions')
```
### C#
``` CSharp
(!(IsSubactionUsage()) || SpecializesFromLibrary("Actions::Action::terminateSubactions"));
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
FeatureMemberships.OfType<ReturnParameterMembership>().Select(item => item.OwnedMemberParameter).Select(resultParams => (resultParams.NotEmpty() ? resultParams.First() : null));
```
# CheckBooleanExpressionSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::booleanEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::booleanEvaluations");
```
# ValidateConjugatedPortDefinitionConjugatedPortDefinitionIsEmpty
### OCL
``` OCL
null
```
### C#
``` CSharp
null;
```
# DeriveUsageNestedUsage
### OCL
``` OCL
ownedFeature->selectByKind(Usage)
```
### C#
``` CSharp
OwnedFeature.OfType<Usage>();
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
OwnedMember.OfType<BindingConnector>().Exists(b => b.RelatedFeatures.Contains(TargetFeature) && b.RelatedFeatures.Contains(Result));
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
(!(IsComposite && OwningType != null && ((OwningType is ViewDefinition) || (OwningType is ViewUsage))) || SpecializesFromLibrary("Views::View::viewpointSatisTestions"));
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
(ElseAction == null ? SpecializesFromLibrary("Actions::ifThenActions") : SpecializesFromLibrary("Actions::ifThenElseActions"));
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
(!(IsComposite && OwnedTyping.Type.Contains((this is Structure)) && OwningType != null && ((OwningType is Structure) || OwningType.Type.Contains((this is Structure)))) || SpecializesFromLibrary("Occurrence::Occurrence::suboccurrences"));
```
# CheckMultiplicityRangeExpressionTypeFeaturing
### OCL
``` OCL
bound->forAll(b | b.featuringType = self.featuringType)
```
### C#
``` CSharp
Bound.All(b => b.FeaturingType == this.FeaturingType);
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
FeatureMembership.OfType<ViewRenderingMembership>().Select(renderings => (renderings.IsEmpty() ? null : renderings.First().ReferencedRendering));
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
OwnedFeatureMembership.OfType<TransitionFeatureMembership>().Where(item => item.Kind == TransitionFeatureKind.Trigger).TransitionFeature.OfType<Expression>();
```
# CheckSuccessionSpecialization
### OCL
``` OCL
specializesFromLibrary('Occurrences::happensBeforeLinks')
```
### C#
``` CSharp
SpecializesFromLibrary("Occurrences::happensBeforeLinks");
```
# DeriveRenderingDefinitionRendering
### OCL
``` OCL
usages->selectByKind(RenderingUsage)
```
### C#
``` CSharp
Usages.OfType<RenderingUsage>();
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
OwnedMember.OfType<BindingConnector>().Where(b => b.RelatedElement.Contains(SubjectParameter) && b.RelatedElement.Exists(r => r != SubjectParameter)).Count() == 1;
```
# CheckNullExpressionSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::nullEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::nullEvaluations");
```
# ValidateFeatureReferenceExpressionResult
### OCL
``` OCL
result.owningType = self
```
### C#
``` CSharp
Result.OwningType == this;
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
new List<dynamic> { this }.Closure(item => item.TypingFeatures()).Typing.Type.AsOrderedSet().Select(types => types.Reject(t1 => types.Exist(T2).T2 != t1 && T2.Specializes(t1)));
```
# CheckPartDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Parts::Part')
```
### C#
``` CSharp
SpecializesFromLibrary("Parts::Part");
```
# ValidateAttributeUsageIsReference
### OCL
``` OCL
isReference
```
### C#
``` CSharp
IsReference;
```
# ValidateElementIsImpliedIncluded
### OCL
``` OCL
ownedRelationship->exists(isImplied) implies isImpliedIncluded
```
### C#
``` CSharp
(!(OwnedRelationship.Exists(item => item.IsImplied)) || IsImpliedIncluded);
```
# ValidateAssociationRelatedTypes
### OCL
``` OCL
not isAbstract implies relatedType->size() >= 2
```
### C#
``` CSharp
(!(!IsAbstract) || RelatedType.Count() >= 2);
```
# DeriveElementDocumentation
### OCL
``` OCL
ownedElement->selectByKind(Documentation)
```
### C#
``` CSharp
OwnedElement.OfType<Documentation>();
```
# ValidateSubsettingFeaturingTypes
### OCL
``` OCL
subsettingFeature.canAccess(subsettedFeature)
```
### C#
``` CSharp
SubsettingFeature.CanAccess(SubsettedFeature);
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
NestedUsage.Reject(item => (item is PortUsage)).All(item => !item.IsComposite);
```
# DeriveTypeOwnedUnioning
### OCL
``` OCL
    ownedRelationship->selectByKind(Unioning)
```
### C#
``` CSharp
OwnedRelationship.OfType<Unioning>();
```
# DeriveRequirementUsageText
### OCL
``` OCL
documentation.body
```
### C#
``` CSharp
Documentation.Body;
```
# ValidateBindingConnectorIsBinary
### OCL
``` OCL
relatedFeature->size() = 2
```
### C#
``` CSharp
RelatedFeature.Count() == 2;
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
FeatureMembership.OfType<ObjectiveMembership>().Select(item => item.OwnedRequirement).Select(objectives => (objectives.IsEmpty() ? null : objectives.First().OwnedObjectiveRequirement));
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
FeatureMembership.OfType<ActorMembership>().Select(item => item.OwnedActorParameter);
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
InstantiatedType.Feature.Select(f => Result.OwnedFeatures.Where(item => item.Redefines(f)).Valuation.Where(v => v != null).Value);
```
# ValidateTypeOwnedMultiplicity
### OCL
``` OCL
ownedMember->selectByKind(Multiplicity)->size() <= 1
```
### C#
``` CSharp
OwnedMember.OfType<Multiplicity>().Count() <= 1;
```
# CheckPortUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Ports::ports')
```
### C#
``` CSharp
SpecializesFromLibrary("Ports::ports");
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
InputParameter(1).Select(parameter => (parameter != null && (parameter is Expression) ? (parameter as Expression) : null));
```
# ValidateIncludeUseCaseUsageReference
### OCL
``` OCL
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(UseCaseUsage)
```
### C#
``` CSharp
(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is UseCaseUsage));
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
(ReferencedFeatureTarget() == null ? this : ((ReferencedFeatureTarget() is ConstraintUsage) ? (ReferencedFeatureTarget() as ConstraintUsage) : null));
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
Feature.Where(f => DirectionOf(f).Select(direction => direction == FeatureDirectionKind.In || direction == FeatureDirectionKind.Inout));
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
OwnedRequirement.OfType<ViewpointUsage>().Where(item => item.IsComposite);
```
# CheckAcceptActionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() and not isTriggerAction() implies
    specializesFromLibrary('Actions::Action::acceptSubactions')
```
### C#
``` CSharp
(!(IsSubactionUsage() && !IsTriggerAction()) || SpecializesFromLibrary("Actions::Action::acceptSubactions"));
```
# CheckJoinNodeSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::Action::join')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::Action::join");
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
(!(IsComposite && OwningType != null && ((OwningType is AnalysisCaseDefinition) || (OwningType is AnalysisCaseUsage))) || SpecializesFromLibrary("AnalysisCases::AnalysisCase::subAnalysisCases"));
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
OwnedMembership.Reject(m => (m is ParameterMembership)).Select(membership => membership.NotEmpty() && (membership.ElementAt(0).MemberElement is Feature));
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
OwnedFeatures.Where(item => item.Direction == in).Select(inputParameters => OwningExpression.SourceTargetFeature().Select(sourceTargetFeature => sourceTargetFeature != null && Result.SubsetsChain(inputParameters.First(), sourceTargetFeature) && Result.OwningType == this));
```
# DeriveUsageNestedTransition
### OCL
``` OCL
nestedUsage->selectByKind(TransitionUsage)
```
### C#
``` CSharp
NestedUsage.OfType<TransitionUsage>();
```
# DeriveTypeFeature
### OCL
``` OCL
featureMembership.ownedMemberFeature
```
### C#
``` CSharp
FeatureMembership.OwnedMemberFeature;
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
SourceConnector.OfType<Succession>().Select(item => ConnectorEnd.ElementAt(0).Multiplicity).All(sourceMult => MultiplicityHasBounds(sourceMult, 1, 1));
```
# ValidateObjectiveMembershipOwningType
### OCL
``` OCL
owningType.oclIsType(CaseDefinition) or
owningType.oclIsType(CaseUsage)
```
### C#
``` CSharp
(OwningType is CaseDefinition) || (OwningType is CaseUsage);
```
# CheckMultiplicitySpecialization
### OCL
``` OCL
specializesFromLibrary('Base::naturals')
```
### C#
``` CSharp
SpecializesFromLibrary("Base::naturals");
```
# ValidateImportTopLevelVisibility
### OCL
``` OCL
importOwningNamespace.owner = null implies
    visibility = VisibilityKind::private
```
### C#
``` CSharp
(!(ImportOwningNamespace.Owner == null) || Visibility == VisibilityKind.Private);
```
# CheckStateDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('States::StateAction')
```
### C#
``` CSharp
SpecializesFromLibrary("States::StateAction");
```
# ValidateOccurrenceUsageIsPortion
### OCL
``` OCL
portionKind <> null implies isPortion
```
### C#
``` CSharp
(!(PortionKind != null) || IsPortion);
```
# CheckConstraintDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Constraints::ConstraintCheck')
```
### C#
``` CSharp
SpecializesFromLibrary("Constraints::ConstraintCheck");
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
OwnedMembership.OfType<StateSubactionMembership>().IsUnique(item => item.Kind);
```
# ValidateFeatureCrossFeatureType
### OCL
``` OCL
crossFeature <> null implies
    crossFeature.type->asSet() = type->asSet()
```
### C#
``` CSharp
(!(CrossFeature != null) || CrossFeature.Type.ToHashSet() == Type.ToHashSet());
```
# DeriveAssociationRelatedType
### OCL
``` OCL
associationEnd.type
```
### C#
``` CSharp
AssociationEnd.Type;
```
# ValidateDefinitionVariationOwnedFeatureMembership
### OCL
``` OCL
isVariation implies ownedFeatureMembership->isEmpty()
```
### C#
``` CSharp
(!(IsVariation) || OwnedFeatureMembership.IsEmpty());
```
# DeriveRelationshipRelatedElement
### OCL
``` OCL
source->union(target)
```
### C#
``` CSharp
Source.Union(Target);
```
# DeriveUsageNestedInterface
### OCL
``` OCL
nestedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ReferenceUsage>();
```
# ValidateDecisionNodeIncomingSuccessions
### OCL
``` OCL
targetConnector->selectByKind(Succession)->size() <= 1
```
### C#
``` CSharp
TargetConnector.OfType<Succession>().Count() <= 1;
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
OwnedMembership.OfType<StateSubactionMembership>().IsUnique(item => item.Kind);
```
# CheckMergeNodeSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::Action::merges')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::Action::merges");
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
(!(IsVariable) || OwningType != null) && OwningType.Specializes("Occurrences::Occurrence");
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
(!(OwningType != null && ((OwningType is Function) && this == (OwningType as Function).Result || (OwningType is Expression) && this == (OwningType as Expression).Result)) || OwningType.OwnedSpecialization.General.Where(item => (item is Function) || (item is Expression)).All(supertype => Redefines(((SuperType is Function) ? (SuperType as Function).Result : (SuperType as Expression).Result))));
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
(RelatedType.Count() < 2 ? new List<dynamic> {  } : RelatedType.SubSequence(2, RelatedType.Count()).AsOrderedSet());
```
# DeriveDefinitionOwnedCase
### OCL
``` OCL
ownedUsage->selectByKind(CaseUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<CaseUsage>();
```
# ValidateControlNodeIsComposite
### OCL
``` OCL
isComposite
```
### C#
``` CSharp
IsComposite;
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
InstantiatedType.Feature.Where(item => OwningMembership.Visibility == VisibilityKind.Public).Select(features => Result.OwnedFeature.All(f => f.OwnedRedefinition.RedefinedFeature.Intersect(features).Count() == 1));
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
(!(IsParallel) || NestedAction.IncomingTransition.IsEmpty()) && NestedAction.OutgoingTransition.IsEmpty();
```
# CheckRenderingDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Views::Rendering')
```
### C#
``` CSharp
SpecializesFromLibrary("Views::Rendering");
```
# DeriveTypeOwnedFeatureMembership
### OCL
``` OCL
ownedRelationship->selectByKind(FeatureMembership)
```
### C#
``` CSharp
OwnedRelationship.OfType<FeatureMembership>();
```
# DeriveUsageNestedPort
### OCL
``` OCL
nestedUsage->selectByKind(PortUsage)
```
### C#
``` CSharp
NestedUsage.OfType<PortUsage>();
```
# ValidateFlowEndIsEnd
### OCL
``` OCL
isEnd
```
### C#
``` CSharp
IsEnd;
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
InputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.OwnedFeature.NotEmpty() && targetParameter.First().OwnedFeature.NotEmpty() && targetParameter.First().OwnedFeature.First().Redefines(Referent));
```
# CheckConcernUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Requirements::concernChecks')
```
### C#
``` CSharp
SpecializesFromLibrary("Requirements::concernChecks");
```
# CheckUsageVariationDefinitionSpecialization
### OCL
``` OCL
owningVariationDefinition <> null implies
    specializes(owningVariationDefinition)
```
### C#
``` CSharp
(!(OwningVariationDefinition != null) || Specializes(OwningVariationDefinition));
```
# CheckConstructorExpressionResultSpecialization
### OCL
``` OCL
result.specializes(instantiatedType)
```
### C#
``` CSharp
Result.Specializes(InstantiatedType);
```
# CheckSendActionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::sendActions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::sendActions");
```
# DeriveUsageNestedEnumeration
### OCL
``` OCL
nestedUsage->selectByKind(EnumerationUsage)
```
### C#
``` CSharp
NestedUsage.OfType<EnumerationUsage>();
```
# CheckCalculationUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Calculations::calculations')
```
### C#
``` CSharp
SpecializesFromLibrary("Calculations::calculations");
```
# CheckOccurrenceDefinitionIndividualSpecialization
### OCL
``` OCL
isIndividual implies specializesFromLibrary('Occurrences::Life')
```
### C#
``` CSharp
(!(IsIndividual) || SpecializesFromLibrary("Occurrences::Life"));
```
# ValidateReferenceUsageIsReference
### OCL
``` OCL
isReference
```
### C#
``` CSharp
IsReference;
```
# CheckForLoopActionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::forLoopActions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::forLoopActions");
```
# ValidateEnumerationDefinitionIsVariation
### OCL
``` OCL
isVariation
```
### C#
``` CSharp
IsVariation;
```
# ValidateForkNodeIncomingSuccessions
### OCL
``` OCL
targetConnector->selectByKind(Succession)->size() <= 1
```
### C#
``` CSharp
TargetConnector.OfType<Succession>().Count() <= 1;
```
# DeriveAnnotatingElementAnnotatedElement
### OCL
``` OCL
 if annotation->notEmpty() then annotation.annotatedElement
 else Sequence{owningNamespace} endif
```
### C#
``` CSharp
(Annotation.NotEmpty() ? Annotation.AnnotatedElement : new List<dynamic> { OwningNamespace });
```
# DeriveLiteralExpressionIsModelLevelEvaluable
### OCL
``` OCL
true
```
### C#
``` CSharp
true;
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
(OwningNamespace == null ? null : (Name != null && OwningNamespace.OwnedMember.Where(m => m.Name == Name).IndexOf(this) != 1 ? null : (OwningNamespace.Owner == null ? EscapedName() : (OwningNamespace.QualifiedName == null || EscapedName() == null ? null : OwningNamespace.QualifiedName + "::" + EscapedName()))));
```
# ValidateMetadataFeatureMetaclass
### OCL
``` OCL
type->selectByKind(Metaclass).size() = 1
```
### C#
``` CSharp
Type.OfType<Metaclass>().Length == 1;
```
# DeriveAssignmentUsageTargetArgument
### OCL
``` OCL
argument(1)
```
### C#
``` CSharp
Argument(1);
```
# CheckConnectorBinarySpecialization
### OCL
``` OCL
connectorEnd->size() = 2 implies
    specializesFromLibrary('Links::binaryLinks')
```
### C#
``` CSharp
(!(ConnectorEnd.Count() == 2) || SpecializesFromLibrary("Links::binaryLinks"));
```
# CheckStepSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::performances')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::performances");
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
OwnedSubsetting.OfType<CrossSubsetting>().Select(crossSubsettings => (crossSubsettings.IsEmpty() ? null : crossSubsettings.First()));
```
# ValidateAnnotationAnnotatingElement
### OCL
``` OCL
ownedAnnotatingElement <> null xor owningAnnotatingElement <> null
```
### C#
``` CSharp
OwnedAnnotatingElement != null ^ OwningAnnotatingElement != null;
```
# CheckConstructorExpressionSpecialization
### OCL
``` OCL
specializes('Performances::constructorEvaluations')
```
### C#
``` CSharp
Specializes("Performances::constructorEvaluations");
```
# ValidateAssertConstraintUsageReference
### OCL
``` OCL
referencedFeaureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(ConstraintUsage)
```
### C#
``` CSharp
(!(ReferencedFeaureTarget() != null) || (ReferencedFeatureTarget() is ConstraintUsage));
```
# ValidateFeaturePortionNotVariable
### OCL
``` OCL
isPortion implies not isVariable
```
### C#
``` CSharp
(!(IsPortion) || !IsVariable);
```
# DeriveTypeInheritedMembership
### OCL
``` OCL
inheritedMemberships(Set{}, Set{}, false)
```
### C#
``` CSharp
InheritedMemberships(new HashSet<dynamic> {  }, new HashSet<dynamic> {  }, false);
```
# DeriveTypeOwnedDisjoining
### OCL
``` OCL
    ownedRelationship->selectByKind(Disjoining)
```
### C#
``` CSharp
OwnedRelationship.OfType<Disjoining>();
```
# DeriveUsageIsReference
### OCL
``` OCL
not isComposite
```
### C#
``` CSharp
!IsComposite;
```
# DeriveTerminateActionUsageTerminatedOccurrenceArgument
### OCL
``` OCL
argument(1)
```
### C#
``` CSharp
Argument(1);
```
# ValidateAttributeDefinitionFeatures
### OCL
``` OCL
feature->forAll(not isComposite)
```
### C#
``` CSharp
Feature.All(item => !item.IsComposite);
```
# DeriveAssignmentActionUsageValueExpression
### OCL
``` OCL
argument(2)
```
### C#
``` CSharp
Argument(2);
```
# ValidateFeatureConstantIsVariable
### OCL
``` OCL
isConstant implies isVariable
```
### C#
``` CSharp
(!(IsConstant) || IsVariable);
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
InputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.OwnedFeature.NotEmpty() && targetParameter.OwnedFeature.First().Redefines("AssignmentAction::target::startingAt"));
```
# CheckForLoopActionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::forLoops')
```
### C#
``` CSharp
(!(IsSubactionUsage()) || SpecializesFromLibrary("Actions::Action::forLoops"));
```
# DeriveNamespaceOwnedMember
### OCL
``` OCL
ownedMembership->selectByKind(OwningMembership).ownedMemberElement
```
### C#
``` CSharp
OwnedMembership.OfType<OwningMembership>().Select(item => item.OwnedMemberElement);
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
OwnedMember.OfType<Multiplicity>().Select(ownedMultiplicities => (ownedMultiplicities.IsEmpty() ? null : ownedMultiplicities.First()));
```
# DeriveDefinitionOwnedCalculation
### OCL
``` OCL
ownedUsage->selectByKind(CalculationUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<CalculationUsage>();
```
# CheckAssignmentActionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::assignmentActions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::assignmentActions");
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
FeatureMembership.OfType<ObjectiveMembership>().Count() <= 1;
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
(Succession.TargetFeature.IsEmpty() ? null : (Succession.TargetFeature.First().FeatureTarget).Select(targetFeature => (!(targetFeature is ActionUsage) ? null : (targetFeature as ActionUsage))));
```
# CheckMetadataUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Metadata::metadataItems')
```
### C#
``` CSharp
SpecializesFromLibrary("Metadata::metadataItems");
```
# DeriveDefinitionOwnedItem
### OCL
``` OCL
ownedUsage->selectByKind(ItemUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ItemUsage>();
```
# CheckActionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::subactions')
```
### C#
``` CSharp
(!(IsSubactionUsage()) || SpecializesFromLibrary("Actions::Action::subactions"));
```
# CheckMetadataFeatureSpecialization
### OCL
``` OCL
specializesFromLibrary('Metaobjects::metaobjects')
```
### C#
``` CSharp
SpecializesFromLibrary("Metaobjects::metaobjects");
```
# DeriveTypeOwnedEndFeature
### OCL
``` OCL
ownedFeature->select(isEnd)
```
### C#
``` CSharp
OwnedFeature.Where(item => item.IsEnd);
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
(!(!(InstantiatedType is Function) && !((InstantiatedType is Feature) && (InstantiatedType as Feature).Type.Exists(item => (item is Function)))) || OwnedFeature.SelectByKind(BindingConnector).Exists(item => RelatedFeature.Contains(this) && RelatedFeature.Contains(Result)));
```
# ValidateInstantiationExpressionResult
### OCL
``` OCL
result.owningType = self
```
### C#
``` CSharp
Result.OwningType == this;
```
# DeriveUsageNestedMetadata
### OCL
``` OCL
nestedUsage->selectByKind(MetadataUsage)
```
### C#
``` CSharp
NestedUsage.OfType<MetadataUsage>();
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
OwnedUsage.Reject(item => (item is PortUsage)).All(item => !item.IsComposite);
```
# CheckForkNodeSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::Action::forks')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::Action::forks");
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
OwnedMember.OfType<BindingConnector>().Exists(b => b.RelatedFeatures.Contains(Succession) && b.RelatedFeatures.Contains(ResolveGlobal("TransitionPerformances::TransitionPerformance::transitionLink")));
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
(!(OwningType == null || !(OwningType is PortDefinition) && !(OwningType is PortUsage)) || IsReference);
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
SourceTargetFeature().Select(sourceParameter => SourceTargetFeature != null && SourceTargetFeature.Redefines(TargetFeature));
```
# DeriveFeatureFeatureTarget
### OCL
``` OCL
if chainingFeature->isEmpty() then self else chainingFeature->last() endif
```
### C#
``` CSharp
(ChainingFeature.IsEmpty() ? this : ChainingFeature.Last());
```
# CheckVerificationCaseUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('VerificationCases::verificationCases')
```
### C#
``` CSharp
SpecializesFromLibrary("VerificationCases::verificationCases");
```
# ValidateCaseUsageSubjectParameterPosition
### OCL
``` OCL
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp
Input.NotEmpty() && Input.First() == SubjectParameter;
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
(!(IsComposite && OwningType != null && ((OwningType is PartDefinition) || (OwningType is PartUsage))) || SpecializesFromLibrary("Parts::Part::ownedActions"));
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
FeatureMembersip.OfType<ResultExpressionMembership>().Select(results => (results.IsEmpty() ? null : results.First().OwnedResultExpression));
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
OwnedMembership.SelectByKind(ResultExpressionMembership).All(mem => OwnedFeature.SelectByKind(BindingConnector).Exists(binding => binding.RelatedFeature.Contains(Result) && binding.RelatedFeature.Contains(mem.OwnedResultExpression.Result)));
```
# CheckFlowUsageFlowSpecialization
### OCL
``` OCL
ownedEndFeatures->notEmpty() implies
    specializesFromLibrary('Flows::flows')
```
### C#
``` CSharp
(!(OwnedEndFeatures.NotEmpty()) || SpecializesFromLibrary("Flows::flows"));
```
# DeriveDefinitionOwnedPort
### OCL
``` OCL
ownedUsage->selectByKind(PortUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<PortUsage>();
```
# CheckAllocationDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Allocations::Allocation')
```
### C#
``` CSharp
SpecializesFromLibrary("Allocations::Allocation");
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
(ResolveGlobal("Metaobjects::Metaobject::annotatedElement").MemberElement as Feature).Select(baseAnnotatedElementFeature => Feature.Where(item => item.Specializes(baseAnnotatedElementFeature)).Excluding(baseAnnotatedElementFeature).Select(annotatedElementFeatures => (!(annotatedElementFeatures.NotEmpty()) || annotatedElementFeatures.Typing.Type.ToHashSet().Select(annotatedElementTypes => AnnotatedElement.GetType().QualifiedName.Select(qn => (ResolveGlobal(qn).MemberElement as Metaclass)).Select(metaclasses => metaclasses.All(m => annotatedElementTypes.Exists(t => m.Specializes(t))))))));
```
# CheckSendActionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::acceptSubactions')
```
### C#
``` CSharp
(!(IsSubactionUsage()) || SpecializesFromLibrary("Actions::Action::acceptSubactions"));
```
# CheckITestionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::ifSubactions')
```
### C#
``` CSharp
(!(IsSubactionUsage()) || SpecializesFromLibrary("Actions::Action::ifSubactions"));
```
# ValidateInstantiationExpressionInstantiatedType
### OCL
``` OCL
instantiatedType() <> null
```
### C#
``` CSharp
InstantiatedType() != null;
```
# DeriveTypeOwnedDifferencing
### OCL
``` OCL
    ownedRelationship->selectByKind(Differencing)
```
### C#
``` CSharp
OwnedRelationship.OfType<Differencing>();
```
# DeriveNamespaceImportedMembership
### OCL
``` OCL
importedMemberships(Set{})
```
### C#
``` CSharp
ImportedMemberships(new HashSet<dynamic> {  });
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
SourceFeature().Select(sourceFeature => (sourceFeature == null ? null : (sourceFeature.FeatureTarget as ActionUsage)));
```
# CheckSuccessionFlowSpecialization
### OCL
``` OCL
specializesFromLibrary('Transfers::flowTransfersBefore')
```
### C#
``` CSharp
SpecializesFromLibrary("Transfers::flowTransfersBefore");
```
# DeriveUsageVariant
### OCL
``` OCL
variantMembership.ownedVariantUsage
```
### C#
``` CSharp
VariantMembership.OwnedVariantUsage;
```
# CheckInvocationExpressionSpecialization
### OCL
``` OCL
specializes(instantiatedType)
```
### C#
``` CSharp
Specializes(InstantiatedType);
```
# CheckTerminateActionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::terminateActions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::terminateActions");
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
InputParameter(1).Select(parameter => (parameter != null && (parameter is Expression) ? (parameter as Expression) : null));
```
# DeriveSendActionUsageReceiverArgument
### OCL
``` OCL
argument(3)
```
### C#
``` CSharp
Argument(3);
```
# ValidateFeatureChainExpressionOperator
### OCL
``` OCL
'.'
```
### C#
``` CSharp
".";
```
# ValidateFunctionResultExpressionMembership
### OCL
``` OCL
membership->selectByKind(ResultExpressionMembership)->size() <= 1
```
### C#
``` CSharp
Membership.OfType<ResultExpressionMembership>().Count() <= 1;
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
(ConnectorEnd.IsEmpty() || ConnectorEnd.OwnedFeature.IsEmpty() ? null : ConnectorEnd.OwnedFeature.First());
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
(OwningAnnotatingRelationship == null ? OwnedAnnotatingRelationship : OwningAnnotatingRelationship.Prepend(OwningAnnotatingRelationship));
```
# ValidateTransitionUsageTriggerActions
### OCL
``` OCL
source <> null and not source.oclIsKindOf(StateUsage) implies
    triggerAction->isEmpty()
```
### C#
``` CSharp
(!(Source != null && !(Source is StateUsage)) || TriggerAction.IsEmpty());
```
# CheckActionDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::Action')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::Action");
```
# DeriveDefinitionOwnedAttribute
### OCL
``` OCL
ownedUsage->selectByKind(AttributeUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<AttributeUsage>();
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
(!(IsEnd && OwningType != null) || OwningType.OwnedEndFeature.ToList().IndexOf(this).Select(i => OwningType.OwnedSpecialization.General.All(supertype => (!(supertype.EndFeature.Count() >= i) || Redefines(supertype.EndFeature.ElementAt(i - 1))))));
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
OwnedRelationship.OfType<Annotation>().Where(a => a.AnnotatedElement != this);
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
(MembershipOwningNamespace is Definition) && (MembershipOwningNamespace as Definition).IsVariation || (MembershipOwningNamespace is Usage) && (MembershipOwningNamespace as Usage).IsVariation;
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
OwnedMembership.OfType<StateSubactionMembership>().Where(item => item.Kind == StateSubactionKind.Exit).Select(exitMemberships => (exitMemberships.IsEmpty() ? null : exitMemberships.ElementAt(0)));
```
# DeriveDefinitionOwnedAnalysisCase
### OCL
``` OCL
ownedUsage->selectByKind(AnalysisCaseUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<AnalysisCaseUsage>();
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
(!(OwnedSpecialization.General.All(item => !(item is DataType)) && !(this is Association)) || OwnedSpecialization.General.All(item => !(item is Association)));
```
# DeriveUsageUsage
### OCL
``` OCL
feature->selectByKind(Usage)
```
### C#
``` CSharp
Feature.OfType<Usage>();
```
# CheckIncludeUseCaseSpecialization
### OCL
``` OCL
owningType <> null and
(owningType.oclIsKindOf(UseCaseDefinition) or
 owningType.oclIsKindOf(UseCaseUsage) implies
    specializesFromLibrary('UseCases::UseCase::includedUseCases'))
```
### C#
``` CSharp
OwningType != null && ((!((OwningType is UseCaseDefinition) || (OwningType is UseCaseUsage)) || SpecializesFromLibrary("UseCases::UseCase::includedUseCases")));
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
OwnedMembership.OfType<StateSubactionMembership>().Where(item => item.Kind == StateSubactionKind.Do).Select(doMemberships => (doMemberships.IsEmpty() ? null : doMemberships.ElementAt(0)));
```
# DeriveItemUsageItemDefinition
### OCL
``` OCL
occurrenceDefinition->selectByKind(Structure)
```
### C#
``` CSharp
OccurrenceDefinition.OfType<Structure>();
```
# DeriveFeatureOwnedFeatureInverting
### OCL
``` OCL
ownedRelationship->selectByKind(FeatureInverting)->
    select(fi | fi.featureInverted = self)
```
### C#
``` CSharp
OwnedRelationship.OfType<FeatureInverting>().Where(fi => fi.FeatureInverted == this);
```
# DeriveDefinitionUsage
### OCL
``` OCL
feature->selectByKind(Usage)
```
### C#
``` CSharp
Feature.OfType<Usage>();
```
# ValidateAttributeUsageFeatures
### OCL
``` OCL
feature->forAll(not isComposite)
```
### C#
``` CSharp
Feature.All(item => !item.IsComposite);
```
# ValidateCaseDefinitionSubjectParameterPosition
### OCL
``` OCL
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp
Input.NotEmpty() && Input.First() == SubjectParameter;
```
# DeriveDefinitionOwnedView
### OCL
``` OCL
ownedUsage->selectByKind(ViewUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ViewUsage>();
```
# DeriveElementOwner
### OCL
``` OCL
owningRelationship.owningRelatedElement
```
### C#
``` CSharp
OwningRelationship.OwningRelatedElement;
```
# DeriveUsageNestedAttribute
### OCL
``` OCL
nestedUsage->selectByKind(AttributeUsage)
```
### C#
``` CSharp
NestedUsage.OfType<AttributeUsage>();
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
FeatureMembership.OfType<StakholderMembership>().Select(item => item.OwnedStakeholderParameter);
```
# ValidateAssociationEndTypes
### OCL
``` OCL
ownedEndFeature->forAll(type->size() = 1)
```
### C#
``` CSharp
OwnedEndFeature.All(item => Type.Count() == 1);
```
# DeriveElementShortName
### OCL
``` OCL
effectiveShortName()
```
### C#
``` CSharp
EffectiveShortName();
```
# DeriveAssociationSourceType
### OCL
``` OCL
    if relatedType->isEmpty() then null
    else relatedType->first() endif
```
### C#
``` CSharp
(RelatedType.IsEmpty() ? null : RelatedType.First());
```
# ValidateTransitionFeatureMembershipEffectAction
### OCL
``` OCL
TransitionFeatureKind::effect implies
    transitionFeature.oclIsKindOf(ActionUsage)
```
### C#
``` CSharp
(!(TransitionFeatureKind.Effect) || (TransitionFeature is ActionUsage));
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
FeatureMembership.OfType<ViewRenderingMembership>().Select(renderings => (renderings.IsEmpty() ? null : renderings.First().ReferencedRendering));
```
# CheckPredicateSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::BooleanEvaluation')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::BooleanEvaluation");
```
# ValidatePartUsagePartDefinition
### OCL
``` OCL
partDefinition->notEmpty()
```
### C#
``` CSharp
PartDefinition.NotEmpty();
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
(!(IsOwnedCrossFeature()) || (Owner as Feature).OwningType.EndFeature.Excluding(this).Select(otherEnds => ((otherEnds.Count() == 1) ? FeaturingType == otherEnds.First().Type : FeaturingType.Count() == 1 && FeaturingType.First().IsCartesianProduct() && FeaturingType.First().AsCartesianProduct() == otherEnds.Type && FeaturingType.First().AllSupertypes().IsSupersetOf((Owner as Feature).OwnedRedefinition.RedefinedFeature.Where(item => item.CrossFeature() != null).CrossFeature().FeaturingType))));
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
(!(IsComposite && OwnedTyping.Type.Contains((this is Class)) && OwningType != null && ((OwningType is Class) || (OwningType is Feature) && (OwningType as Feature).Type.Exists(item => (item is Class)))) || SpecializesFromLibrary("Occurrence::Occurrence::suboccurrences"));
```
# CheckFeatureSpecialization
### OCL
``` OCL
specializesFromLibrary('Base::things')
```
### C#
``` CSharp
SpecializesFromLibrary("Base::things");
```
# CheckActionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::actions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::actions");
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
OwnedMember.OfType<ConjugatedPortDefinition>().Select(conjugatedPortDefinitions => (conjugatedPortDefinitions.IsEmpty() ? null : conjugatedPortDefinitions.First()));
```
# DeriveTypeUnioningType
### OCL
``` OCL
ownedUnioning.unioningType
```
### C#
``` CSharp
OwnedUnioning.UnioningType;
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
InputParameter(2).Select(parameter => (parameter != null && (parameter is Action) ? (parameter as Action) : null));
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
(!(!(InstantiatedType is Function) && !((InstantiatedType is Feature) && (InstantiatedType as Feature).Type.Exists(item => (item is Function)))) || Result.Specializes(InstantiatedType));
```
# CheckInterfaceUsageBinarySpecialization
### OCL
``` OCL
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Interfaces::binaryInterfaces')
```
### C#
``` CSharp
(!(OwnedEndFeature.Count() == 2) || SpecializesFromLibrary("Interfaces::binaryInterfaces"));
```
# CheckCalculationDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Calculations::Calculation')
```
### C#
``` CSharp
SpecializesFromLibrary("Calculations::Calculation");
```
# CheckFeatureDataValueSpecialization
### OCL
``` OCL
ownedTyping.type->exists(selectByKind(DataType)) implies
    specializesFromLibrary('Base::dataValues')
```
### C#
``` CSharp
(!(OwnedTyping.Type.Exists(item => item.SelectByKind(DataType))) || SpecializesFromLibrary("Base::dataValues"));
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
(IsNegated ? SpecializesFromLibrary("Performances::falseEvaluations") : SpecializesFromLibrary("Performances::trueEvaluations"));
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
OwnedMembership.OfType<StateSubactionMembership>().Where(item => item.Kind == StateSubactionKind.Do).Select(doMemberships => (doMemberships.IsEmpty() ? null : doMemberships.ElementAt(0)));
```
# ValidateTypeAtMostOneConjugator
### OCL
``` OCL
ownedRelationship->selectByKind(Conjugation)->size() <= 1
```
### C#
``` CSharp
OwnedRelationship.OfType<Conjugation>().Count() <= 1;
```
# DeriveDefinitionDirectedUsage
### OCL
``` OCL
directedFeature->selectByKind(Usage)
```
### C#
``` CSharp
DirectedFeature.OfType<Usage>();
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
OwnedFeatureMembership.OfType<TransitionFeatureMembership>().Where(item => item.Kind == TransitionFeatureKind.Trigger).TransitionFeatures.OfType<AcceptActionUsage>();
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
FeatureMembership.OfType<ReturnParameterMembership>().Count() == 1;
```
# DeriveUsageNestedUseCase
### OCL
``` OCL
nestedUsage->selectByKind(UseCaseUsage)
```
### C#
``` CSharp
NestedUsage.OfType<UseCaseUsage>();
```
# CheckOccurrenceUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Occurrences::occurrences')
```
### C#
``` CSharp
SpecializesFromLibrary("Occurrences::occurrences");
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
OwnedConstraint.ReferencedFeatureTarget().Select(referencedFeature => (referencedFeature == null ? OwnedConstraint : ((referencedFeature is ConstraintUsage) ? (RefrencedFeature as ConstraintUsage) : null)));
```
# ValidateRequirementUsageSubjectParameterPosition
### OCL
``` OCL
input->notEmpty() and input->first() = subjectParameter
```
### C#
``` CSharp
Input.NotEmpty() && Input.First() == SubjectParameter;
```
# DeriveDefinitionOwnedRendering
### OCL
``` OCL
ownedUsage->selectByKind(RenderingUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<RenderingUsage>();
```
# CheckFlowWithEndsSpecialization
### OCL
``` OCL
ownedEndFeatures->notEmpty() implies
    specializesFromLibrary('Transfers::flowTransfers')
```
### C#
``` CSharp
(!(OwnedEndFeatures.NotEmpty()) || SpecializesFromLibrary("Transfers::flowTransfers"));
```
# ValidateRequirementConstraintMembershipIsComposite
### OCL
``` OCL
ownedConstraint.isComposite
```
### C#
``` CSharp
OwnedConstraint.IsComposite;
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
FeatureMembership.OfType<FramedConcernMembership>().Select(item => item.OwnedConcern);
```
# DeriveTypeEndFeature
### OCL
``` OCL
feature->select(isEnd)
```
### C#
``` CSharp
Feature.Where(item => item.IsEnd);
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
(!(TriggerKind.At) || Argument.NotEmpty()) && Argument.ElementAt(0).Result.SpecializesFromLibrary("Time::TimeInstantValue");
```
# ValidateOccurrenceUsageIndividualUsage
### OCL
``` OCL
isIndividual implies individualDefinition <> null
```
### C#
``` CSharp
(!(IsIndividual) || IndividualDefinition != null);
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
(!(OwningType != null && ((OwningType is Behavior) || (OwningType is Step)) && this.IsComposite) || SpecializesFromLibrary("Performances::Performance::subperformance"));
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
FeatureMembership.OfType<ObjectiveMembership>().Select(item => item.OwnedRequirement).Select(objectives => (objectives.IsEmpty() ? null : objectives.First().OwnedObjectiveRequirement));
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
OwnedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => item.Kind == RequirementConstraintKind.Requirement).OwnedConstraint;
```
# ValidateFlowPayloadFeature
### OCL
``` OCL
ownedFeature->selectByKind(PayloadFeature)->size() <= 1
```
### C#
``` CSharp
OwnedFeature.OfType<PayloadFeature>().Count() <= 1;
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
(!(IsPortion && OwnedTyping.Type.Contains((this is Class)) && OwningType != null && ((OwningType is Class) || (OwningType is Feature) && (OwningType as Feature).Type.Exists(item => (item is Class)))) || SpecializesFromLibrary("Occurrence::Occurrence::portions"));
```
# CheckInterfaceDefinitionBinarySpecialization
### OCL
``` OCL
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Interfaces::BinaryInterface')
```
### C#
``` CSharp
(!(OwnedEndFeature.Count() == 2) || SpecializesFromLibrary("Interfaces::BinaryInterface"));
```
# CheckMetaclassSpecialization
### OCL
``` OCL
specializesFromLibrary('Metaobjects::Metaobject')
```
### C#
``` CSharp
SpecializesFromLibrary("Metaobjects::Metaobject");
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
FeatureMembership.OfType<ObjectiveMembership>().Count() <= 1;
```
# CheckAnalysisCaseDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('AnalysisCases::AnalysisCase')
```
### C#
``` CSharp
SpecializesFromLibrary("AnalysisCases::AnalysisCase");
```
# ValidateUsageIsReferential
### OCL
``` OCL
direction <> null or isEnd or featuringType->isEmpty() implies
    isReference
```
### C#
``` CSharp
(!(Direction != null || IsEnd || FeaturingType.IsEmpty()) || IsReference);
```
# CheckAssociationStructureSpecialization
### OCL
``` OCL
specializesFromLibrary('Objects::LinkObject')
```
### C#
``` CSharp
SpecializesFromLibrary("Objects::LinkObject");
```
# DeriveUsageNestedAction
### OCL
``` OCL
nestedUsage->selectByKind(ActionUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ActionUsage>();
```
# DeriveDefinitionOwnedTransition
### OCL
``` OCL
ownedUsage->selectByKind(TransitionUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<TransitionUsage>();
```
# CheckConnectionDefinitionBinarySpecialization
### OCL
``` OCL
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Connections::BinaryConnections')
```
### C#
``` CSharp
(!(OwnedEndFeature.Count() == 2) || SpecializesFromLibrary("Connections::BinaryConnections"));
```
# DeriveUsageNestedAnalysisCase
### OCL
``` OCL
nestedUsage->selectByKind(AnalysisCaseUsage)
```
### C#
``` CSharp
NestedUsage.OfType<AnalysisCaseUsage>();
```
# CheckViewpointDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Views::Viewpoint')
```
### C#
``` CSharp
SpecializesFromLibrary("Views::Viewpoint");
```
# ValidateViewRenderingMembershipOwningType
### OCL
``` OCL
owningType.oclIsKindOf(ViewDefinition) or
owningType.oclIsKindOf(ViewUsage)
```
### C#
``` CSharp
(OwningType is ViewDefinition) || (OwningType is ViewUsage);
```
# DeriveUsageNestedView
### OCL
``` OCL
nestedUsage->selectByKind(ViewUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ViewUsage>();
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
FeatureMembership.OfType<SubjectMembership>().Select(subjects => (subjects.IsEmpty() ? null : subjects.First().OwnedSubjectParameter));
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
(OwningType is Behavior) || (OwningType is Step) || (OwningType.OwningMembership is ReturnParameterMembership) && (OwningType.OwningNamespace is ConstructorExpression);
```
# CheckStateUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('States::stateActions')
```
### C#
``` CSharp
SpecializesFromLibrary("States::stateActions");
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
FeatureMembership.OfType<SubjectMembership>().Count() <= 1;
```
# CheckFeatureOccurrenceSpecialization
### OCL
``` OCL
ownedTyping.type->exists(selectByKind(Class)) implies
    specializesFromLibrary('Occurrences::occurrences')
```
### C#
``` CSharp
(!(OwnedTyping.Type.Exists(item => item.SelectByKind(Class))) || SpecializesFromLibrary("Occurrences::occurrences"));
```
# CheckConstraintUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Constraints::constraintChecks')
```
### C#
``` CSharp
SpecializesFromLibrary("Constraints::constraintChecks");
```
# DeriveFeatureChainingFeature
### OCL
``` OCL
ownedFeatureChaining.chainingFeature
```
### C#
``` CSharp
OwnedFeatureChaining.ChainingFeature;
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
FeatureMembership.OfType<SubjectMembership>().Count() <= 1;
```
# CheckOccurrenceUsageTimeSliceSpecialization
### OCL
``` OCL
portionKind = PortionKind::timeslice implies
    specializesFromLibrary('Occurrences::Occurrence::timeSlices')
```
### C#
``` CSharp
(!(PortionKind == PortionKind.Timeslice) || SpecializesFromLibrary("Occurrences::Occurrence::timeSlices"));
```
# CheckConnectionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Connections::connections')
```
### C#
``` CSharp
SpecializesFromLibrary("Connections::connections");
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
TriggerAction.All(item => item.SpecializesFromLibrary("Actions::TransitionAction::accepter") && GuardExpression.All(item => item.SpecializesFromLibrary("Actions::TransitionAction::guard") && EffectAction.All(item => item.SpecializesFromLibrary("Actions::TransitionAction::effect"))));
```
# CheckLiteralInfinitySpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::literalIntegerEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::literalIntegerEvaluations");
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
                redefines(ownedParameters->at(i))))
```
### C#
``` CSharp
OwningType != null && !(OwningFeatureMembership is ReturnParameterMembership) && ((!((OwningType is Behavior) || (OwningType is Step) && ((!((OwningType is InvocationExpression)) || !OwnedRedefinition.Exists(item => !item.IsImplied)))) || OwningType.OwnedFeature.Where(item => item.Direction != null).Reject(item => (OwningFeatureMembership is ReturnParameterMembership)).ToList().IndexOf(this).Select(i => OwningType.OwnedSpecialization.General.All(supertype => supertype.OwnedFeature.Where(item => item.Direction != null).Reject(item => (OwningFeatureMembership is ReturnParameterMembership)).Select(ownedParameters => (!(ownedParameters.Count() >= i) || Redefines(ownedParameters.ElementAt(i - 1))))))));
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
(LowerBound == null ? OwnedMember.NotEmpty() && OwnedMember.ElementAt(0) == UpperBound : OwnedMember.Count() > 1 && OwnedMember.ElementAt(0) == LowerBound && OwnedMember.ElementAt(1) == UpperBound);
```
# DeriveDefinitionOwnedState
### OCL
``` OCL
ownedUsage->selectByKind(StateUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<StateUsage>();
```
# ValidateExposeOwningNamespace
### OCL
``` OCL
importOwningNamespace.oclIsType(ViewUsage)
```
### C#
``` CSharp
(ImportOwningNamespace is ViewUsage);
```
# CheckStepOwnedPerformanceSpecialization
### OCL
``` OCL
isComposite and owningType <> null and
(owningType.oclIsKindOf(Structure) or
 owningType.oclIsKindOf(Feature) and
 owningType.oclAsType(Feature).type->
    exists(oclIsKindOf(Structure)) implies
    specializesFromLibrary('Objects::Object::ownedPerformance'))
```
### C#
``` CSharp
IsComposite && OwningType != null && ((!((OwningType is Structure) || (OwningType is Feature) && (OwningType as Feature).Type.Exists(item => (item is Structure))) || SpecializesFromLibrary("Objects::Object::ownedPerformance")));
```
# DeriveConjugatedPortTypingPortDefinition
### OCL
``` OCL
conjugatedPortDefinition.originalPortDefinition
```
### C#
``` CSharp
ConjugatedPortDefinition.OriginalPortDefinition;
```
# ValidateConjugatedPortDefinitionOriginalPortDefinition
### OCL
``` OCL
ownedPortConjugator.originalPortDefinition = originalPortDefinition
```
### C#
``` CSharp
OwnedPortConjugator.OriginalPortDefinition == OriginalPortDefinition;
```
# DeriveStateDefinitionState
### OCL
``` OCL
action->selectByKind(StateUsage)
```
### C#
``` CSharp
Action.OfType<StateUsage>();
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
(!(PortionKind != null) || OwningType != null) && ((OwningType is OccurrenceDefinition) || (OwningType is OccurrenceUsage));
```
# DeriveDefinitionOwnedAllocation
### OCL
``` OCL
ownedUsage->selectByKind(AllocationUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<AllocationUsage>();
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
FeatureMemberships.OfType<ReturnParameterMembership>().Select(item => item.OwnedMemberParameter).Select(resultParams => (resultParams.NotEmpty() ? resultParams.First() : null));
```
# CheckFeatureObjectSpecialization
### OCL
``` OCL
ownedTyping.type->exists(selectByKind(Structure)) implies
    specializesFromLibary('Objects::objects')
```
### C#
``` CSharp
(!(OwnedTyping.Type.Exists(item => item.SelectByKind(Structure))) || SpecializesFromLibary("Objects::objects"));
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
(InstantiatedType.Input).Select(parameters => Input.All(inp => inp.OwnedRedefinition.RedefinedFeature.Intersect(parameters).Count() == 1));
```
# ValidateEventOccurrenceUsageIsReference
### OCL
``` OCL
isReference
```
### C#
``` CSharp
IsReference;
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
InputParameter(2).Select(parameter => (parameter != null && (parameter is ActionUsage) ? (parameter as ActionUsage) : null));
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
(!(OwningType != null && ((OwningType is PartDefinition) || (OwningType is PartUsage))) || SpecializesFromLibrary("Parts::Part::performedActions"));
```
# DeriveTypeOwnedSpecialization
### OCL
``` OCL
ownedRelationship->selectByKind(Specialization)->
    select(s | s.special = self)
```
### C#
``` CSharp
OwnedRelationship.OfType<Specialization>().Where(s => s.Special == this);
```
# DeriveDefinitionOwnedViewpoint
### OCL
``` OCL
ownedUsage->selectByKind(ViewpointUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ViewpointUsage>();
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
(TriggerAction.IsEmpty() ? InputParameters().Count() >= 1 : InputParameters().Count() >= 2);
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
FeatureMembership.OfType<ReturnParameterMembership>().Count() == 1;
```
# CheckControlNodeSpecialization
### OCL
``` OCL
specializesFromLibrary('Action::Action::controls')
```
### C#
``` CSharp
SpecializesFromLibrary("Action::Action::controls");
```
# DeriveFeatureOwnedSubsetting
### OCL
``` OCL
ownedSpecialization->selectByKind(Subsetting)
```
### C#
``` CSharp
OwnedSpecialization.OfType<Subsetting>();
```
# DeriveUsageNestedRendering
### OCL
``` OCL
nestedUsage->selectByKind(RenderingUsage)
```
### C#
``` CSharp
NestedUsage.OfType<RenderingUsage>();
```
# ValidateForLoopActionUsageParameters
### OCL
``` OCL
inputParameters()->size() = 2
```
### C#
``` CSharp
InputParameters().Count() == 2;
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
OwnedUseCase.OfType<IncludeUseCaseUsage>().Select(item => item.UseCaseIncluded);
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
(OwnedMembership.Reject(item => (item is FeatureMembership)).MemberElement).Select(elements => (elements.IsEmpty() ? null : elements.First()));
```
# CheckUsageVariationUsageTypeFeaturing
### OCL
``` OCL
owningVariationUsage <> null implies
    featuringType->asSet() = owningVariationUsage.featuringType->asSet()
```
### C#
``` CSharp
(!(OwningVariationUsage != null) || FeaturingType.ToHashSet() == OwningVariationUsage.FeaturingType.ToHashSet());
```
# DeriveClassifierOwnedSubclassification
### OCL
``` OCL
    ownedSpecialization->selectByKind(Subclassification)
```
### C#
``` CSharp
OwnedSpecialization.OfType<Subclassification>();
```
# ValidateMetadataFeatureMetaclassNotAbstract
### OCL
``` OCL
not metaclass.isAbstract
```
### C#
``` CSharp
!Metaclass.IsAbstract;
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
(!(!IsDefault) || FeatureWithValue.OwnedMember.OfType<BindingConnector>().Exists(b => b.RelatedFeature.Contains(FeatureWithValue) && b.RelatedFeature.Exists(f => f.ChainingFeature == new List<dynamic> { Value, Value.Result }) && (!IsInitial ? b.FeaturingType == FeatureWithValue.FeaturingType : b.FeaturingType.Exists(t => (t is Feature) && (t as Feature).ChainingFeature == new List<dynamic> { ResolveGlobal("Base::things::that").MemberElement, ResolveGlobal("Occurrences::Occurrence::startShot").MemberElement }))));
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
(!(OwningType != null && ((OwningType is OccurrenceDefinition) || (OwningType is OccurrenceUsage))) || SpecializesFromLibrary("Occurrences::Occurrence::timeEnclosedOccurrences"));
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
(!(TriggerKind.When) || Argument.NotEmpty()) && (Argument.ElementAt(0) is FeatureReferenceExpression) && ((Argument.ElementAt(0) as FeatureReferenceExpression).Referent).Select(referent => (referent is Expression) && (referent as Expression).Result.SpecializesFromLibrary("ScalarValues::Boolean"));
```
# ValidateFeatureValueIsInitial
### OCL
``` OCL
isInitial implies featureWithValue.isVariable
```
### C#
``` CSharp
(!(IsInitial) || FeatureWithValue.IsVariable);
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
OwnedFeature.OfType<PayloadFeature>().Select(payloadFeatures => (payloadFeatures.IsEmpty() ? null : payloadFeatures.First()));
```
# DerivePartUsagePartDefinition
### OCL
``` OCL
itemDefinition->selectByKind(PartDefinition)
```
### C#
``` CSharp
ItemDefinition.OfType<PartDefinition>();
```
# CheckInterfaceDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Interfaces::Interface')
```
### C#
``` CSharp
SpecializesFromLibrary("Interfaces::Interface");
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
(!(OwningFeatureMembership != null && (OwningFeatureMembership is ActorMembership)) || ((OwningType is RequirementDefinition) || (OwningType is RequirementUsage) ? SpecializesFromLibrary("Requirements::RequirementCheck::actors") : SpecializesFromLibrary("Cases::Case::actors")));
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
TargetConnector.OfType<Succession>().All(item => item.SubsetsChain(this, ResolveGlobal("ControlPerformances::MergePerformance::incomingHBLink")));
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
(InstantiatedType.Feature).Select(features => Input.All(inp1 => Input.All(inp2 => (!(inp1 != inp2) || inp1.OwnedRedefinition.RedefinedFeature.Intersect(inp2.OwnedRedefinition.RedefinedFeature).Intersect(features).IsEmpty()))));
```
# DeriveTransitionUsageSuccession
### OCL
``` OCL
ownedMember->selectByKind(Succession)->at(1)
```
### C#
``` CSharp
OwnedMember.OfType<Succession>().ElementAt(0);
```
# DeriveDefinitionOwnedMetadata
### OCL
``` OCL
ownedUsage->selectByKind(MetadataUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<MetadataUsage>();
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
FeatureMembership.OfType<AStakholderMembership>().Select(item => item.OwnedStakeholderParameter);
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
(!(OwningType != null && ((OwningType is PartDefinition) || (OwningType is PartUsage))) || SpecializesFromLibrary("Parts::Part::ownedPorts"));
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
(RelatedFeature.Count() < 2 ? new List<dynamic> {  } : RelatedFeature.SubSequence(2, RelatedFeature.Count()).AsOrderedSet());
```
# DeriveUsageNestedRequirement
### OCL
``` OCL
nestedUsage->selectByKind(RequirementUsage)
```
### C#
``` CSharp
NestedUsage.OfType<RequirementUsage>();
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
TargetConnector.OfType<Succession>().Select(item => ConnectorEnd.ElementAt(0)).All(sourceMult => MultiplicityHasBounds(sourceMult, 0, 1));
```
# CheckCaseUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Cases::cases')
```
### C#
``` CSharp
SpecializesFromLibrary("Cases::cases");
```
# CheckLiteralExpressionSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::literalEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::literalEvaluations");
```
# CheckPartUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Parts::parts')
```
### C#
``` CSharp
SpecializesFromLibrary("Parts::parts");
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
OwnedMembership.OfType<StateSubactionMembership>().Where(item => item.Kind == StateSubactionKind.Entry).Select(entryMemberships => (entryMemberships.IsEmpty() ? null : entryMemberships.ElementAt(0)));
```
# DeriveDefinitionOwnedInterface
### OCL
``` OCL
ownedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ReferenceUsage>();
```
# DeriveDefinitionOwnedVerificationCase
### OCL
``` OCL
ownedUsage->selectByKind(VerificationCaseUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<VerificationCaseUsage>();
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
OwnedRequirement.OfType<ViewpointUsage>().Where(item => item.IsComposite);
```
# CheckRequirementUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Requirements::requirementChecks')
```
### C#
``` CSharp
SpecializesFromLibrary("Requirements::requirementChecks");
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
RelatedFeature.Closure(item => item.FeaturingType).Where(t => RelatedFeature.All(f => f.IsFeaturedWithin(t))).Select(commonFeaturingTypes => commonFeaturingTypes.Reject(t1 => commonFeaturingTypes.Exists(t2 => t2 != t1 && t2.Closure(item => item.FeaturingType).Contains(t1))).Select(nearestCommonFeaturingTypes => (nearestCommonFeaturingTypes.IsEmpty() ? null : nearestCommonFeaturingTypes.First())));
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
(!(IsOwnedCrossFeature()) || OwnedSubsetting.SubsettedFeature.IsSupersetOf((Owner as Feature).OwnedRedefinition.RedefinedFeature.Where(item => item.CrossFeature != null).CrossFeature));
```
# DeriveFeatureOwnedTyping
### OCL
``` OCL
ownedGeneralization->selectByKind(FeatureTyping)
```
### C#
``` CSharp
OwnedGeneralization.OfType<FeatureTyping>();
```
# CheckFlowDefinitionBinarySpecialization
### OCL
``` OCL
flowEnd->size() = 2 implies
    specializesFromLibrary('Flows::Message')
```
### C#
``` CSharp
(!(FlowEnd.Count() == 2) || SpecializesFromLibrary("Flows::Message"));
```
# ValidateSendActionParameters
### OCL
``` OCL
inputParameters()->size() >= 3
```
### C#
``` CSharp
InputParameters().Count() >= 3;
```
# DeriveDefinitionOwnedConcern
### OCL
``` OCL
ownedUsage->selectByKind(ConcernUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ConcernUsage>();
```
# CheckFeatureReferenceExpressionResultSpecialization
### OCL
``` OCL
result.owningType() = self and result.specializes(referent)
```
### C#
``` CSharp
Result.OwningType() == this && Result.Specializes(Referent);
```
# ValidateRedefinitionEndConformance
### OCL
``` OCL
redefinedFeature.isEnd implies redefiningFeature.isEnd
```
### C#
``` CSharp
(!(RedefinedFeature.IsEnd) || RedefiningFeature.IsEnd);
```
# DeriveUsageNestedConnection
### OCL
``` OCL
nestedUsage->selectByKind(ConnectorAsUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ConnectorAsUsage>();
```
# CheckFunctionSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::Evaluation')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::Evaluation");
```
# ValidateElementFilterMembershipConditionIsModelLevelEvaluable
### OCL
``` OCL
condition.isModelLevelEvaluable
```
### C#
``` CSharp
Condition.IsModelLevelEvaluable;
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
OwnedUseCase.OfType<IncludeUseCaseUsage>().Select(item => item.UseCaseIncluded);
```
# CheckLiteralRationalSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::literalRationalEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::literalRationalEvaluations");
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
(!(OwningType != null && ((OwningType is CalculationDefinition) || (OwningType is CalculationUsage))) || SpecializesFromLibrary("Calculations::Calculation::subcalculations"));
```
# CheckMetadataDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Metadata::MetadataItem')
```
### C#
``` CSharp
SpecializesFromLibrary("Metadata::MetadataItem");
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
SourceConnector.OfType<Succession>().Select(item => ConnectorEnd.ElementAt(1)).All(targetMult => MultiplicityHasBounds(targetMult, 0, 1));
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
(!(OwningType != null && ((OwningType is RenderingDefinition) || (OwningType is RenderingUsage))) || SpecializesFromLibrary("Views::Rendering::subrenderings"));
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
(!(OwningType != null && (OwningType is FlowEnd) && OwningType.OwnedFeature.ElementAt(0) == this) || (OwningType.OwningType).Select(flowType => (!(flowType != null) || flowType.OwnedFeature.IndexOf(OwningType).Select(i => ((!(i == 1) || RedefinesFromLibrary("Transfers::Transfer::source::sourceOutput"))) && ((!(i == 2) || RedefinesFromLibrary("Transfers::Transfer::source::targetInput")))))));
```
# DeriveUsageNestedViewpoint
### OCL
``` OCL
nestedUsage->selectByKind(ViewpointUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ViewpointUsage>();
```
# ValidateElementFilterMembershipConditionIsBoolean
### OCL
``` OCL
condition.result.specializesFromLibrary('ScalarValues::Boolean')
```
### C#
``` CSharp
Condition.Result.SpecializesFromLibrary("ScalarValues::Boolean");
```
# CheckPortDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Ports::Port')
```
### C#
``` CSharp
SpecializesFromLibrary("Ports::Port");
```
# ValidateConnectionDefinitionIsSufficient
### OCL
``` OCL
isSufficient
```
### C#
``` CSharp
IsSufficient;
```
# DeriveElementIsLibraryElement
### OCL
``` OCL
libraryNamespace() <> null
```
### C#
``` CSharp
LibraryNamespace() != null;
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
(ConnectorEnd.Count() < 2 || ConnectorEnd.ElementAt(1).OwnedFeature.IsEmpty() ? null : ConnectorEnd.ElementAt(1).OwnedFeature.First());
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
FeatureMembership.OfType<SubjectMembership>().Count() <= 1;
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
OwnedSpecialization.General.All(item => !(item is Class) && !(item is Association));
```
# ValidateFeatureChainingFeatureConformance
### OCL
``` OCL
Sequence{2..chainingFeature->size()}->forAll(i |
    chainingFeature->at(i).isFeaturedWithin(chainingFeature->at(i-1)))
```
### C#
``` CSharp
new List<dynamic> { 2 /* .. */ ChainingFeature.Count() }.All(i => ChainingFeature.ElementAt(i - 1).IsFeaturedWithin(ChainingFeature.ElementAt(i - 1 - 1)));
```
# DeriveDefinitionOwnedUseCase
### OCL
``` OCL
ownedUsage->selectByKind(UseCaseUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<UseCaseUsage>();
```
# ValidateCollectExpressionOperator
### OCL
``` OCL
'collect'
```
### C#
``` CSharp
"collect";
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
SourceTargetFeature().Select(sourceParameter => SourceTargetFeature != null && SourceTargetFeature.RedefinesFromLibrary("ControlFunctions::\'.\'::source::target"));
```
# DeriveDefinitionOwnedPart
### OCL
``` OCL
ownedUsage->selectByKind(PartUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<PartUsage>();
```
# DeriveDefinitionVariant
### OCL
``` OCL
variantMembership.ownedVariantUsage
```
### C#
``` CSharp
VariantMembership.OwnedVariantUsage;
```
# ValidateReturnParameterMembershipOwningType
### OCL
``` OCL
owningType.oclIsKindOf(Function) or owningType.oclIsKindOf(Expression)
```
### C#
``` CSharp
(OwningType is Function) || (OwningType is Expression);
```
# CheckAssignmentActionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::assignments')
```
### C#
``` CSharp
(!(IsSubactionUsage()) || SpecializesFromLibrary("Actions::Action::assignments"));
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
OwnedMembership.OfType<StateSubactionMembership>().Where(item => item.Kind == StateSubactionKind.Exit).Select(exitMemberships => (exitMemberships.IsEmpty() ? null : exitMemberships.ElementAt(0)));
```
# ValidateUsageVariationIsAbstract
### OCL
``` OCL
isVariation implies isAbstract
```
### C#
``` CSharp
(!(IsVariation) || IsAbstract);
```
# ValidateResultExpressionMembershipOwningType
### OCL
``` OCL
owningType.oclIsKindOf(Function) or owningType.oclIsKindOf(Expression)
```
### C#
``` CSharp
(OwningType is Function) || (OwningType is Expression);
```
# CheckAcceptActionUsageTriggerActionSpecialization
### OCL
``` OCL
isTriggerAction() implies
    specializesFromLibrary('Actions::TransitionAction::accepter')
```
### C#
``` CSharp
(!(IsTriggerAction()) || SpecializesFromLibrary("Actions::TransitionAction::accepter"));
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
OwnedMember.OfType<Expression>().Select(ownedExpressions => (ownedExpressions.Count() < 2 ? null : ownedExpressions.First()));
```
# DeriveDefinitionOwnedOccurrence
### OCL
``` OCL
ownedUsage->selectByKind(OccurrenceUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<OccurrenceUsage>();
```
# ValidateIndexExpressionOperator
### OCL
``` OCL
'#'
```
### C#
``` CSharp
"#";
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
OwnedMembership.Reject(item => (item is FeatureMembership)).MemberElement.OfType<Feature>().Select(unownedFeatures => (unownedFeatures.IsEmpty() ? null : (unownedFeatures.First() as Feature)));
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
(!(OwningMembership != null && (OwningMembership is FeatureValue)) || ((OwningMembership as FeatureValue).FeatureWithValue).Select(featureWithValue => FeaturingType == featureWithValue.FeaturingType));
```
# DeriveDefinitionOwnedEnumeration
### OCL
``` OCL
ownedUsage->selectByKind(EnumerationUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<EnumerationUsage>();
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
FeatureMembership.OfType<ActorMembership>().Select(item => item.OwnedActorParameter);
```
# ValidateDefinitionVariationIsAbstract
### OCL
``` OCL
isVariation implies isAbstract
```
### C#
``` CSharp
(!(IsVariation) || IsAbstract);
```
# CheckTransitionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::transitionActions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::transitionActions");
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
FeatureMembersip.OfType<ResultExpressionMembership>().Select(results => (results.IsEmpty() ? null : results.First().OwnedResultExpression));
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
OwnedFeatureMembership.OfType<RequirementConstraintMembership>().Where(item => item.Kind == RequirementConstraintKind.Assumption).OwnedConstraint;
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
OwnedFeature.Closure(item => item.OwnedFeature).All(f => (!(f.DeclaredName == null && f.DeclaredShortName == null && f.Valuation != null) || f.Valuation.Value.IsModelLevelEvaluable) && f.Redefinition.RedefinedFeature.Count() == 1);
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
(!(IsEnd && OwningType != null && ((OwningType is Association) || (OwningType is Connector))) || SpecializesFromLibrary("Links::Link::participant"));
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
OwnedMember.OfType<Expression>().Select(ownedExpressions => (ownedExpressions.IsEmpty() ? null : (ownedExpressions.Count() == 1 ? ownedExpressions.ElementAt(0) : ownedExpressions.ElementAt(1))));
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
OwnedMembership.OfType<StateSubactionMembership>().Where(item => item.Kind == StateSubactionKind.Entry).Select(entryMemberships => (entryMemberships.IsEmpty() ? null : entryMemberships.ElementAt(0)));
```
# CheckDataTypeSpecialization
### OCL
``` OCL
specializesFromLibrary('Base::DataValue')
```
### C#
``` CSharp
SpecializesFromLibrary("Base::DataValue");
```
# CheckViewUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Views::views')
```
### C#
``` CSharp
SpecializesFromLibrary("Views::views");
```
# DeriveMembershipImportImportedElement
### OCL
``` OCL
importedMembership.memberElement
```
### C#
``` CSharp
ImportedMembership.MemberElement;
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
(!(IsEnd) || Multiplicities().AllSuperTypes().Flatten().OfType<MultiplicityRange>().Exists(item => item.HasBounds(1, 1)));
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
(RedefiningFeature.ResolveGlobal("Base::Anything").ModelElement as Type).Select(anythingType => (RedefiningFeature.IsVariable ? new HashSet<dynamic> { RedefiningFeature.OwningType } : RedefiningFeature.FeaturingTypes.ToHashSet().Append(anythingType)).Select(redefiningFeaturingTypes => (RedefinedFeature.IsVariable ? new HashSet<dynamic> { RedefinedFeature.OwningType } : RedefinedFeature.FeaturingTypes.ToHashSet().Append(anythingType)).Select(redefinedFeaturingTypes => redefiningFeaturingTypes != RedefinedFeaturingType)));
```
# ValidateFeatureEndNotDerivedAbstractCompositeOrPortion
### OCL
``` OCL
isEnd implies not (isDerived or isAbstract or isComposite or isPortion)
```
### C#
``` CSharp
(!(IsEnd) || !(IsDerived || IsAbstract || IsComposite || IsPortion));
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
(!(CrossingFeature.IsEnd && CrossingFeature.OwningType != null) || (CrossingFeature.OwningType.EndFeature).Select(endFeatures => (CrossedFeature.ChainingFeature).Select(chainingFeatures => (!(chainingFeatures.Count() == 2 && endFeatures.Count() == 2) || chainingFeatures.ElementAt(0) == endFeatures.Excluding(CrossingFeature).ElementAt(0)))));
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
(!(Arguments.NotEmpty() && !Arguments.First().Result.SpecializesFromLibrary("Collections::Array")) || Result.Specializes(Arguments.First().Result));
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
(UpperBound == null ? new List<dynamic> {  } : (LowerBound == null ? new List<dynamic> { UpperBound } : new List<dynamic> { LowerBound, UpperBound }));
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
(ReferencedFeatureTarget() == null ? this : ((ReferencedFeatureTarget() is OccurrenceUsage) ? (ReferencedFeatureTarget() as OccurrenceUsage) : null));
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
FramedConcern.FeatureMemberhsip.OfType<StakeholderMembership>().Select(item => item.OwnedStakeholderParameter);
```
# CheckViewDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Views::View')
```
### C#
``` CSharp
SpecializesFromLibrary("Views::View");
```
# CheckAssociationSpecialization
### OCL
``` OCL
specializesFromLibrary('Links::Link')
```
### C#
``` CSharp
SpecializesFromLibrary("Links::Link");
```
# DeriveAcceptActionUsageReceiverArgument
### OCL
``` OCL
argument(2)
```
### C#
``` CSharp
Argument(2);
```
# ValidateMergeNodeOutgoingSuccessions
### OCL
``` OCL
sourceConnector->selectAsKind(Succession)->size() <= 1
```
### C#
``` CSharp
SourceConnector.OfType<Succession>().Count() <= 1;
```
# CheckWhileLoopActionUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Actions::whileLoopActions')
```
### C#
``` CSharp
SpecializesFromLibrary("Actions::whileLoopActions");
```
# DeriveUsageNestedCase
### OCL
``` OCL
nestedUsage->selectByKind(CaseUsage)
```
### C#
``` CSharp
NestedUsage.OfType<CaseUsage>();
```
# CheckExpressionSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::evaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::evaluations");
```
# DeriveRequirementDefinitionText
### OCL
``` OCL
documentation.body
```
### C#
``` CSharp
Documentation.Body;
```
# ValidateExposeIsImportAll
### OCL
``` OCL
isImportAll
```
### C#
``` CSharp
IsImportAll;
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
(!(IsComposite && OwningType != null && ((OwningType is ItemDefinition) || (OwningType is ItemUsage))) || SpecializesFromLibrary("Items::Item::subparts"));
```
# DeriveNamespaceOwnedImport
### OCL
``` OCL
ownedRelationship->selectByKind(Import)
```
### C#
``` CSharp
OwnedRelationship.OfType<Import>();
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
(!(IsVariation) || !OwnedSpecialization.Specific.Exists(item => (item is Definition) && (item as Definition).IsVariation));
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
(!(IsComposite && OwningType != null && ((OwningType is PartDefinition) || (OwningType is PartUsage))) || SpecializesFromLibrary("Parts::Part::ownedStates"));
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
(!(IsComposite && OwningType != null && ((OwningType is UseCaseDefinition) || (OwningType is UseCaseUsage))) || SpecializesFromLibrary("UseCases::UseCase::subUseCases"));
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
(!(OwningType != null && ((OwningType is ViewDefinition) || (OwningType is ViewUsage))) || SpecializesFromLibrary("Views::View::subviews"));
```
# DeriveDefinitionOwnedRequirement
### OCL
``` OCL
ownedUsage->selectByKind(RequirementUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<RequirementUsage>();
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
FeatureMembership.OfType<ActorMembership>().Select(item => item.OwnedActorParameter);
```
# CheckLiteralStringSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::literalStringEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::literalStringEvaluations");
```
# ValidateFeatureMultiplicityDomain
### OCL
``` OCL
multiplicity <> null implies multiplicity.featuringType = featuringType
```
### C#
``` CSharp
(!(Multiplicity != null) || Multiplicity.FeaturingType == FeaturingType);
```
# ValidateJoinNodeOutgoingSuccessions
### OCL
``` OCL
sourceConnector->selectByKind(Succession)->size() <= 1
```
### C#
``` CSharp
SourceConnector.OfType<Succession>().Count() <= 1;
```
# DeriveUsageNestedItem
### OCL
``` OCL
nestedUsage->selectByKind(ItemUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ItemUsage>();
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
FeatureMembership.OfType<ViewRenderingMembership>().Count() <= 1;
```
# DeriveAcceptActionUsagePayloadArgument
### OCL
``` OCL
argument(1)
```
### C#
``` CSharp
Argument(1);
```
# DeriveElementTextualRepresentation
### OCL
``` OCL
ownedElement->selectByKind(TextualRepresentation)
```
### C#
``` CSharp
OwnedElement.OfType<TextualRepresentation>();
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
(!(PayloadArgument != null && (PayloadArgument is TriggerInvocationExpression)) || (PayloadArgument as Expression).Select(invocation => Parameter.Count() >= 2 && invocation.Parameter.Count() >= 2 && OwnedFeature.OfType<BindingConnector>().Exists(b => b.RelatedFeatures.Contains(Parameter.ElementAt(1)) && b.RelatedFeatures.Contains(invocation.Parameter.ElementAt(1)))));
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
OwnedMembership.SelectByKind(ResultExpressionMembership).All(mem => OwnedFeature.SelectByKind(BindingConnector).Exists(binding => binding.RelatedFeature.Contains(Result) && binding.RelatedFeature.Contains(mem.OwnedResultExpression.Result)));
```
# DeriveTypeIntersectingType
### OCL
``` OCL
ownedIntersecting.intersectingType
```
### C#
``` CSharp
OwnedIntersecting.IntersectingType;
```
# ValidateExposeVisibility
### OCL
``` OCL
VisibilityKind::protected
```
### C#
``` CSharp
VisibilityKind.Protected;
```
# ValidateMetadataAccessExpressionReferencedElement
### OCL
``` OCL
ownedMembership->exists(not oclIsKindOf(FeatureMembership))
```
### C#
``` CSharp
OwnedMembership.Exists(item => !(item is FeatureMembership));
```
# CheckBindingConnectorSpecialization
### OCL
``` OCL
specializesFromLibrary('Links::selfLinks')
```
### C#
``` CSharp
SpecializesFromLibrary("Links::selfLinks");
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
OwnedMembership.OfType<ElementFilterMembership>().Select(item => item.Condition);
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
InstantiatedType.Input.Select(inp => OwnedFeatures.Where(item => item.Redefines(inp)).Valuation.Where(v => v != null).Value);
```
# DeriveDefinitionOwnedAction
### OCL
``` OCL
ownedUsage->selectByKind(ActionUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ActionUsage>();
```
# ValidateFlowDefinitionFlowEnds
### OCL
``` OCL
flowEnd->size() <= 2
```
### C#
``` CSharp
FlowEnd.Count() <= 2;
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
Type.OfType<Metaclass>().Select(metaclassTypes => (metaclassTypes.IsEmpty() ? null : MetaClassTypes.First()));
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
Feature.Where(f => DirectionOf(f).Select(direction => direction == FeatureDirectionKind.Out || direction == FeatureDirectionKind.Inout));
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
(!(IsSemantic()) || AnnotatedElement.OfType<Type>().Select(annotatedTypes => EvaluateFeature((ResolveGlobal("Metaobjects::SemanticMetadata::baseType").MemberElement as Feature)).OfType<MetadataFeature>().Select(baseTypes => (!(annotatedTypes.NotEmpty() && baseTypes().NotEmpty() && baseTypes().First().IsSyntactic()) || annotatedTypes.First().Select(annotatedType => baseTypes.First().SyntaxElement().Select(baseType => ((annotatedType is Classifier) && (baseType is Feature) ? (baseType as Feature).Type.All(t => annotatedType.Specializes(t)) : ((baseType is Type) ? annotatedType.Specializes((baseType as Type)) : true))))))));
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
(!(IsComposite && OwningType != null && ((OwningType is ItemDefinition) || (OwningType is ItemUsage))) || SpecializesFromLibrary("Items::Item::subitem"));
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
(IsNegated ? SpecializesFromLibrary("Requirements::notSatisfiedRequirementChecks") : SpecializesFromLibrary("Requirements::satisfiedRequirementChecks"));
```
# CheckViewpointUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Views::viewpoints')
```
### C#
``` CSharp
SpecializesFromLibrary("Views::viewpoints");
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
(!(IsVariation) || !OwnedSpecialization.Specific.Exists(item => (item is Definition) && (item as Definition).IsVariation || (item is Usage) && (item as Usage).IsVariation));
```
# CheckFlowDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Flows::MessageAction')
```
### C#
``` CSharp
SpecializesFromLibrary("Flows::MessageAction");
```
# DeriveAcceptActionUsagePayloadParameter
### OCL
``` OCL
 if parameter->isEmpty() then null
 else parameter->first() endif
```
### C#
``` CSharp
(Parameter.IsEmpty() ? null : Parameter.First());
```
# ValidateTransitionFeatureMembershipTriggerAction
### OCL
``` OCL
TransitionFeatureKind::trigger implies
    transitionFeature.oclIsKindOf(AcceptActionUsage)
```
### C#
``` CSharp
(!(TransitionFeatureKind.Trigger) || (TransitionFeature is AcceptActionUsage));
```
# DeriveExpressionIsModelLevelEvaluable
### OCL
``` OCL
modelLevelEvaluable(Set(Element){})
```
### C#
``` CSharp
ModelLevelEvaluable((Element));
```
# CheckCaseDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Cases::Case')
```
### C#
``` CSharp
SpecializesFromLibrary("Cases::Case");
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
Bound.All(b => b.Result.SpecializesFromLibrary("ScalarValues::Integer") && ValueOf(b).Select(value => (!(value != null) || value >= 0)));
```
# CheckWhileLoopActionUsageSubactionSpecialization
### OCL
``` OCL
isSubactionUsage() implies
    specializesFromLibrary('Actions::Action::whileLoops')
```
### C#
``` CSharp
(!(IsSubactionUsage()) || SpecializesFromLibrary("Actions::Action::whileLoops"));
```
# DeriveSendActionUsageSenderArgument
### OCL
``` OCL
argument(2)
```
### C#
``` CSharp
Argument(2);
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
OccurrenceDefinition.OfType<OccurrenceDefinition>().Where(item => item.IsIndividual).Select(individualDefinitions => (individualDefinitions.IsEmpty() ? null : individualDefinitions.First()));
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
CrossingFeature.IsEnd && CrossingFeature.OwningType != null && CrossingFeature.OwningType.EndFeature.Count() > 1;
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
(!(OwningFeatureMembership != null && (OwningFeatureMembership is RequirementConstraintMembership)) || ((OwningFeatureMembership as RequirementConstraintMembership).Kind == RequirementConstraintKind.Assumption ? SpecializesFromLibrary("Requirements::RequirementCheck::assumptions") : SpecializesFromLibrary("Requirements::RequirementCheck::constraints")));
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
(!(ConnectorEnds.Count() == 2 && Association.Exists(item => (item is AssociationStructure))) || SpecializesFromLibrary("Objects::binaryLinkObjects"));
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
InputParameter(3).Select(parameter => (parameter != null && (parameter is Expression) ? (parameter as Expression) : null));
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
(PayloadFeature == null ? new List<dynamic> {  } : PayloadFeature.Type);
```
# CheckConnectionUsageBinarySpecialization
### OCL
``` OCL
ownedEndFeature->size() = 2 implies
    specializesFromLibrary('Connections::binaryConnections')
```
### C#
``` CSharp
(!(OwnedEndFeature.Count() == 2) || SpecializesFromLibrary("Connections::binaryConnections"));
```
# CheckInterfaceUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Interfaces::interfaces')
```
### C#
``` CSharp
SpecializesFromLibrary("Interfaces::interfaces");
```
# ValidateStateSubactionMembershipOwningType
### OCL
``` OCL
owningType.oclIsKindOf(StateDefinition) or
owningType.oclIsKindOf(StateUsage)
```
### C#
``` CSharp
(OwningType is StateDefinition) || (OwningType is StateUsage);
```
# ValidateParameterMembershipParameterDirection
### OCL
``` OCL
ownedMemberParameter.direction = parameterDirection()
```
### C#
``` CSharp
OwnedMemberParameter.Direction == ParameterDirection();
```
# CheckFeatureFeatureMembershipTypeFeaturing
### OCL
``` OCL
owningFeatureMembership <> null implies
    featuringTypes->exists(t | isFeaturingType(t))
```
### C#
``` CSharp
(!(OwningFeatureMembership != null) || FeaturingTypes.Exists(t => IsFeaturingType(t)));
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
(ObjectiveRequirement == null ? new List<dynamic> {  } : ObjectiveRequirement.FeatureMembership.OfType<RequirementVerificationMembership>().Select(item => item.VerifiedRequirement).AsOrderedSet());
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
SourceConnector.OfType<Succession>().All(item => item.SubsetsChain(this, ResolveGlobal("ControlPerformances::MergePerformance::outgoingHBLink")));
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
(!(OwningType != null && ((OwningType is Behavior) || (OwningType is Step))) || SpecializesFromLibrary("Performances::Performance::enclosedPerformance"));
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
(!(OwningFeatureMembership != null && (OwningFeatureMembership is StateSubactionMembership)) || ((OwningFeatureMembership as StateSubactionMembership).Kind).Select(kind => (kind == StateSubactionKind.Entry ? RedefinesFromLibrary("States::StateAction::entryAction") : (kind == StateSubactionKind.Do ? RedefinesFromLibrary("States::StateAction::doAction") : RedefinesFromLibrary("States::StateAction::exitAction")))));
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
(!(IsComposite && OwningType != null && ((OwningType is Class) || (OwningType is OccurrenceUsage) || (OwningType is Feature) && (OwningType as Feature).Type.Exists(item => item.OclIsKind(Class)))) || SpecializesFromLibrary("Occurrences::Occurrence::suboccurrences"));
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
OwnedFeatureMembership.OfType<TransitionFeatureMembership>().Where(item => item.Kind == TransitionFeatureKind.Trigger).TransitionFeature.OfType<AcceptActionUsage>();
```
# ValidateAcceptActionUsageParameters
### OCL
``` OCL
inputParameters()->size() >= 2
```
### C#
``` CSharp
InputParameters().Count() >= 2;
```
# CheckRequirementDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Requirements::RequirementCheck')
```
### C#
``` CSharp
SpecializesFromLibrary("Requirements::RequirementCheck");
```
# ValidateFeatureEndIsConstant
### OCL
``` OCL
isEnd and isVariable implies isConstant
```
### C#
``` CSharp
(!(IsEnd && IsVariable) || IsConstant);
```
# CheckAttributeUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Base::dataValues')
```
### C#
``` CSharp
SpecializesFromLibrary("Base::dataValues");
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
InstantiatedType.Feature.Where(item => item.Visibility == VisibilityKind.Public).Select(features => Result.OwnedFeature.All(f1 => Result.OwnedFeature.All(f2 => (!(f1 != f2) || f1.OwnedRedefinition.RedefinedFeature.Intersect(f2.OwnedRedefinition.RedefinedFeature).Intersect(features).IsEmpty()))));
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
OwnedMember.OfType<BindingConnector>().Exists(b => b.RelatedFeatures.Contains(Source) && b.RelatedFeatures.Contains(InputParameter(1)));
```
# CheckFlowUsageSpecialization
### OCL
``` OCL
specializesFromLibrary('Flows::messages')
```
### C#
``` CSharp
SpecializesFromLibrary("Flows::messages");
```
# ValidateStakeholderMembershipOwningType
### OCL
``` OCL
owningType.oclIsKindOf(RequirementUsage) or
owningType.oclIsKindOf(RequirementDefinition)
```
### C#
``` CSharp
(OwningType is RequirementUsage) || (OwningType is RequirementDefinition);
```
# DeriveSendActionUsagePayloadArgument
### OCL
``` OCL
argument(1)
```
### C#
``` CSharp
Argument(1);
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
OccurrenceDefinition.OfType<OccurrenceDefinition>().Where(item => item.IsIndividual).Length <= 1;
```
# DeriveTypeFeatureMembership
### OCL
``` OCL
ownedFeatureMembership->union(
    inheritedMembership->selectByKind(FeatureMembership))
```
### C#
``` CSharp
OwnedFeatureMembership.Union(InheritedMembership.OfType<FeatureMembership>());
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
(RedefiningFeature.IsVariable ? new List<dynamic> { RedefiningFeature.OwningType } : RedefiningFeature.FeaturingType).Select(featuringTypes => featuringTypes.All(t => t.DirectionOf(RedefinedFeature).Select(direction => ((!((direction == FeatureDirectionKind.In || direction == FeatureDirectionKind.Out)) || RedefiningFeature.direction == direction)) && ((!(direction == FeatureDirectionKind.Inout) || RedefiningFeature.direction != null)))));
```
# CheckTypeSpecialization
### OCL
``` OCL
specializesFromLibrary('Base::Anything')
```
### C#
``` CSharp
SpecializesFromLibrary("Base::Anything");
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
(OwnedFeature.IsEmpty() || !(OwnedFeature.First() is ReferenceUsage) ? null : (OwnedFeature.First() as ReferenceUsage));
```
# DeriveDefinitionOwnedReference
### OCL
``` OCL
ownedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp
OwnedUsage.OfType<ReferenceUsage>();
```
# ValidateClassifierMultiplicityDomain
### OCL
``` OCL
multiplicity <> null implies multiplicity.featuringType->isEmpty()
```
### C#
``` CSharp
(!(Multiplicity != null) || Multiplicity.FeaturingType.IsEmpty());
```
# DeriveSatisfyRequirementUsageSatisfyingFeature
### OCL
``` OCL
    let bindings: BindingConnector = ownedMember->
        selectByKind(BindingConnector)->
        select(b | b.relatedElement->includes(subjectParameter)) in
    if bindings->isEmpty() or
       bindings->first().relatedElement->exists(r | r <> subjectParameter)
    then null
    else bindings->first().relatedElement->any(r | r <> subjectParameter)
    endif
```
### C#
``` CSharp
OwnedMember.OfType<BindingConnector>().Where(b => b.RelatedElement.Contains(SubjectParameter)).Select(bindings => (bindings.IsEmpty() || bindings.First().RelatedElement.Exists(r => r != SubjectParameter) ? null : bindings.First().RelatedElement.FirstOrDefault(r => r != SubjectParameter)));
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
FeatureMembership.OfType<SubjectMembership>().Select(subjects => (subjects.IsEmpty() ? null : subjects.First().OwnedSubjectParameter));
```
# ValidateFramedConcernMembershipConstraintKind
### OCL
``` OCL
RequirementConstraintKind::requirement
```
### C#
``` CSharp
RequirementConstraintKind.Requirement;
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
RelatedFeature.All(f => (FeaturingType.IsEmpty() ? f.IsFeaturedWithin(null) : FeaturingType.All(t => f.IsFeaturedWithin(t))));
```
# DeriveFlowFlowEnd
### OCL
``` OCL
connectorEnd->selectByKind(FlowEnd)
```
### C#
``` CSharp
ConnectorEnd.OfType<FlowEnd>();
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
(OwningType != null && (OwningType is Feature) ? FeaturingType == (OwningType as Feature).FeaturingType : FeaturingType.IsEmpty());
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
(!(OwningType != null && ((OwningType is ItemDefinition) || (OwningType is ItemUsage))) || SpecializesFromLibrary("Items::Item::checkedConstraints"));
```
# DerivePackageFilterCondition
### OCL
``` OCL
ownedMembership->
    selectByKind(ElementFilterMembership).condition
```
### C#
``` CSharp
OwnedMembership.OfType<ElementFilterMembership>().Select(item => item.Condition);
```
# CheckAssociationStructureBinarySpecialization
### OCL
``` OCL
endFeature->size() = 2 implies
    specializesFromLibrary('Objects::BinaryLinkObject')
```
### C#
``` CSharp
(!(EndFeature.Count() == 2) || SpecializesFromLibrary("Objects::BinaryLinkObject"));
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
OwnedRelationship.OfType<Annotation>().Where(a => a.AnnotatedElement == this);
```
# ValidateSelectExpressionOperator
### OCL
``` OCL
'select'
```
### C#
``` CSharp
"select";
```
# ValidateTypeOwnedDifferencingNotOne
### OCL
``` OCL
ownedDifferencing->size() <> 1
```
### C#
``` CSharp
OwnedDifferencing.Count() != 1;
```
# CheckStructureSpecialization
### OCL
``` OCL
specializesFromLibrary('Objects::Object')
```
### C#
``` CSharp
SpecializesFromLibrary("Objects::Object");
```
# ValidateRequirementVerificationMembershipKind
### OCL
``` OCL
RequirementConstraintKind::requirement
```
### C#
``` CSharp
RequirementConstraintKind.Requirement;
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
(!(OwningFeatureMembership != null && (OwningFeatureMembership is ViewRenderingMembership)) || RedefinesFromLibrary("Views::View::viewRendering"));
```
# DeriveUsageNestedCalculation
### OCL
``` OCL
nestedUsage->selectByKind(CalculationUsage)
```
### C#
``` CSharp
NestedUsage.OfType<CalculationUsage>();
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
InputParameter(3).Select(parameter => (parameter != null && (parameter is ActionUsage) ? (parameter as ActionUsage) : null));
```
# ValidateForLoopActionUsageLoopVariable
### OCL
``` OCL
ownedFeature->notEmpty() and
ownedFeature->at(1).oclIsKindOf(ReferenceUsage)
```
### C#
``` CSharp
OwnedFeature.NotEmpty() && (OwnedFeature.ElementAt(0) is ReferenceUsage);
```
# ValidateWhileLoopActionUsage
### OCL
``` OCL
inputParameters()->size() >= 2
```
### C#
``` CSharp
InputParameters().Count() >= 2;
```
# CheckConcernDefinitionSpecialization
### OCL
``` OCL
specializesFromLibrary('Requirements::ConcernCheck')
```
### C#
``` CSharp
SpecializesFromLibrary("Requirements::ConcernCheck");
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
FramedConcern.FeatureMemberhsip.OfType<StakeholderMembership>().Select(item => item.OwnedStakeholderParameter);
```
# CheckStateUsageSubstateSpecialization
### OCL
``` OCL
isSubstateUsage(true) implies
    specializesFromLibrary('States::StateAction::substates')
```
### C#
``` CSharp
(!(IsSubstateUsage(true)) || SpecializesFromLibrary("States::StateAction::substates"));
```
# DeriveUsageDirectedUsage
### OCL
``` OCL
directedFeature->selectByKind(Usage)
```
### C#
``` CSharp
DirectedFeature.OfType<Usage>();
```
# DeriveFeatureOwnedFeatureChaining
### OCL
``` OCL
ownedRelationship->selectByKind(FeatureChaining)
```
### C#
``` CSharp
OwnedRelationship.OfType<FeatureChaining>();
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
FeatureMembership.OfType<ViewRenderingMembership>().Count() <= 1;
```
# ValidateFeatureChainExpressionConformance
### OCL
``` OCL
argument->notEmpty() implies
    targetFeature.isFeaturedWithin(argument->first().result)
```
### C#
``` CSharp
(!(Argument.NotEmpty()) || TargetFeature.IsFeaturedWithin(Argument.First().Result));
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
Membership.All(m1 => Membership.All(m2 => (!(m1 != m2) || m1.IsDistinguishableFrom(m2))));
```
# ValidateRequirementConstraintMembershipOwningType
### OCL
``` OCL
owningType.oclIsKindOf(RequirementDefinition) or
owningType.oclIsKindOf(RequirementUsage)
```
### C#
``` CSharp
(OwningType is RequirementDefinition) || (OwningType is RequirementUsage);
```
# CheckLiteralBooleanSpecialization
### OCL
``` OCL
specializesFromLibrary('Performances::literalBooleanEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::literalBooleanEvaluations");
```
# CheckStateUsageExclusiveStateSpecialization
### OCL
``` OCL
isSubstateUsage(false) implies
    specializesFromLibrary('States::StateAction::exclusiveStates')
```
### C#
``` CSharp
(!(IsSubstateUsage(false)) || SpecializesFromLibrary("States::StateAction::exclusiveStates"));
```
# CheckConnectionDefinitionSpecializations
### OCL
``` OCL
specializesFromLibrary('Connections::Connection')
```
### C#
``` CSharp
SpecializesFromLibrary("Connections::Connection");
```
# ValidateITestionUsageParameters
### OCL
``` OCL
inputParameters()->size() >= 2
```
### C#
``` CSharp
InputParameters().Count() >= 2;
```
# CheckAssociationBinarySpecialization
### OCL
``` OCL
associationEnd->size() = 2 implies
    specializesFromLibrary('Links::BinaryLink')
```
### C#
``` CSharp
(!(AssociationEnd.Count() == 2) || SpecializesFromLibrary("Links::BinaryLink"));
```
# ValidateObjectiveMembershipIsComposite
### OCL
``` OCL
ownedObjectiveRequirement.isComposite
```
### C#
``` CSharp
OwnedObjectiveRequirement.IsComposite;
```
# DeriveForLoopActionUsageSeqArgument
### OCL
``` OCL
argument(1)
```
### C#
``` CSharp
Argument(1);
```
# ValidateTransitionFeatureMembershipOwningType
### OCL
``` OCL
owningType.oclIsKindOf(TransitionUsage)
```
### C#
``` CSharp
(OwningType is TransitionUsage);
```
# DeriveUsageNestedReference
### OCL
``` OCL
nestedUsage->selectByKind(ReferenceUsage)
```
### C#
``` CSharp
NestedUsage.OfType<ReferenceUsage>();
```
# CheckClassSpecialization
### OCL
``` OCL
specializesFromLibrary('Occurrences::Occurrence')
```
### C#
``` CSharp
SpecializesFromLibrary("Occurrences::Occurrence");
```
# ValidateSubsettingConstantConformance
### OCL
``` OCL
subsettedFeature.isConstant and subsettingFeature.isVariable implies
    subsettingFeature.isConstant
```
### C#
``` CSharp
(!(SubsettedFeature.IsConstant && SubsettingFeature.IsVariable) || SubsettingFeature.IsConstant);
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
(!(TriggerKind.After) || Argument.NotEmpty()) && Argument.ElementAt(0).Result.SpecializesFromLibrary("Quantities::ScalarQuantityValue") && (ResolveGlobal("Quantities::TensorQuantityValue::mRef").OwnedMemberElement).Select(mRef => Argument.ElementAt(0).Result.Feature.Where(item => OwnedRedefinition.RedefinedFeature.Closure(item => item.OwnedRedefinition.RedefinedFeature).Contains(mRef)).Exists(item => item.SpecializesFromLibrary("ISQBase::DurationUnit")));
```
# CheckConnectorSpecialization
### OCL
``` OCL
specializesFromLibrary('Links::links')
```
### C#
``` CSharp
SpecializesFromLibrary("Links::links");
```
# ValidateStructureSpecialization
### OCL
``` OCL
ownedSpecialization.general->forAll(not oclIsKindOf(Behavior))
```
### C#
``` CSharp
OwnedSpecialization.General.All(item => !(item is Behavior));
```
