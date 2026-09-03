# null, implies, oclIsKindOf
### OCL
``` OCL
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(OccurrenceUsage)
```
### C#
``` CSharp
(!(ReferencedFeatureTarget() != null) || (ReferencedFeatureTarget() is OccurrenceUsage));
```
# function call with parameter
### OCL
``` OCL
action->selectByKind(CalculationUsage)
```
### C#
``` CSharp
Action.OfType<CalculationUsage>();
```
# oclIsType, or
### OCL
``` OCL
owningType.oclIsType(RequirementDefinition) or
owningType.oclIsType(CaseUsage)
```
### C#
``` CSharp
(OwningType is RequirementDefinition) || (OwningType is CaseUsage);
```
# let with sequence, reject, oclIsKindOf, if, first
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
# not, implies, selectByKind, size
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
# null, forAll, impies, selectByKind
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
# selectByKind, select, self
### OCL
``` OCL
ownedRelationship->selectByKind(TypeFeaturing)->
    select(tf | tf.featureOfType = self)
```
### C#
``` CSharp
OwnedRelationship.OfType<TypeFeaturing>().Where(tf => tf.FeatureOfType == this);
```
# function call with string param
### OCL
``` OCL
specializesFromLibrary('Performances::metadataAccessEvaluations')
```
### C#
``` CSharp
SpecializesFromLibrary("Performances::metadataAccessEvaluations");
```
# excluding, isEmpty
### OCL
``` OCL
ownedFeatures->excluding(result)->isEmpty()
```
### C#
``` CSharp
OwnedFeatures.Excluding(Result).IsEmpty();
```
# let with sequence, reject, oclIsKindOf, or, oclIsKindOf, oclAsType
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
# forAll containing implies, enum with keyword
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
# selectByKind, empty set, select, asOrderedSet
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
# let, functioncall with select on return, at
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
# closure, forAll
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
# let with OrderedSet, selectByKind, isEmpty, first
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
# oclIsKindOf, exists, size with parameter
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
# let with OrderedSet, asOrderedSet, if, isEmpty, union
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
# selectByKind, exists, includes
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
# selectByKind, "empty" select
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
# let with OrderedSet, new OrderedSet, comment, closure, asOrderedSet, reject, exist
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
# select containing let
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
# selectByKind, collect, forAll
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
# selectByKind, isUnique
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
# implies, asSet
### OCL
``` OCL
crossFeature <> null implies
    crossFeature.type->asSet() = type->asSet()
```
### C#
``` CSharp
(!(CrossFeature != null) || CrossFeature.Type.ToHashSet() == Type.ToHashSet());
```
# The big one
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
# if, size, OrderedSet
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

# let with OrderedSet, select, forAll, intersection
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
# let with functioncall, notEmpty, first
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
# if, else if, select
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
# notEqual, xor, null,
### OCL
``` OCL
ownedAnnotatingElement <> null xor owningAnnotatingElement <> null
```
### C#
``` CSharp
OwnedAnnotatingElement != null ^ OwningAnnotatingElement != null;
```
# empty sets as parameter
### OCL
``` OCL
inheritedMemberships(Set{}, Set{}, false)
```
### C#
``` CSharp
InheritedMemberships(new HashSet<dynamic> {  }, new HashSet<dynamic> {  }, false);
```
# nested if with let
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
# simplest if ever
### OCL
``` OCL
if chainingFeature->isEmpty() then self else chainingFeature->last() endif
```
### C#
``` CSharp
(ChainingFeature.IsEmpty() ? this : ChainingFeature.Last());
```
# selectByKind, forAll with selector, exists with selector
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
# second big if
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
