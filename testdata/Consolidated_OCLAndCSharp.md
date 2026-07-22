# null, implies, oclIsKindOf
### OCL
``` OCL 
referencedFeatureTarget() <> null implies
    referencedFeatureTarget().oclIsKindOf(OccurrenceUsage)
```
### C#
``` CSharp 
(!(referencedFeatureTarget() != null) || (referencedFeatureTarget() is OccurrenceUsage))
```
# function call with parameter
### OCL
``` OCL 
action->selectByKind(CalculationUsage)
```
### C#
``` CSharp 
action.OfType<CalculationUsage>()
```
# oclIsType, or
### OCL
``` OCL 
owningType.oclIsType(RequirementDefinition) or
owningType.oclIsType(CaseUsage)
```
### C#
``` CSharp 
(owningType is RequirementDefinition) || (owningType is CaseUsage)
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
ownedMembership.Where(reject => !(reject is ParameterMembership)).Select(nonParameterMemberships => (!nonParameterMemberships.Any() ||
!(nonParameterMemberships.First().memberElement is Feature) ? null :
((Feature)nonParameterMemberships.First().memberElement)))
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
(!(!(this is ConjugatedPortDefinition)) || ownedMember.OfType<ConjugatedPortDefinition>().Count() == 1)
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
(direction == null) && (ownedSpecializations.ForAll(item => item.isImplied) || ownedMembership.OfType<FeatureValue>().ForAll(fv => specializes(fv.value.result)))
```
# selectByKind, select, self
### OCL
``` OCL 
ownedRelationship->selectByKind(TypeFeaturing)->
    select(tf | tf.featureOfType = self)
```
### C#
``` CSharp 
ownedRelationship.OfType<TypeFeaturing>().Where(tf => tf.featureOfType == this)
```
# function call with string param
### OCL
``` OCL 
specializesFromLibrary('Performances::metadataAccessEvaluations')
```
### C#
``` CSharp 
specializesFromLibrary("Performances::metadataAccessEvaluations")
```
# excluding, isEmpty
### OCL
``` OCL 
ownedFeatures->excluding(result)->isEmpty()
```
### C#
``` CSharp 
!ownedFeatures.Excluding(result).Any()
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
ownedMembership.Reject(reject => reject is ParameterMembership).Select(nonParameterMemberships => !nonParameterMemberships.Any() ||  !(nonParameterMemberships.First().memberElement is Feature)? null : nonParameterMemberships.First().memberElement as Feature )
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
ownedFeature.All(f => (!(f != result) || f.direction == FeatureDirectionKind.in))
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
ownedImport.OfType<Expose>().importedMemberships(new Set()).memberElement.Where(elm => includeAsExposed(elm)).AsOrderedSet()
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
(!(triggerAction.Any()) || inputParameter(2).Select(payloadParameter => payloadParameter != null && payloadParameter.subsetsChain(triggerAction->ElementAt(0), triggerPayloadParameter())))
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
featureWithValue.redefinition.redefinedFeature.Closure(item => item.redefinition.redefinedFeature).Select(item2 => item2.valuation.ForAll(item3 => item3.isDefault));
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
featureMembership.OfType<SubjectMembership>().Select(subjects => (subjects.IsEmpty() ? null :
subjects.First().ownedSubjectParameter))
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
(instantiatedType is Behavior) || (instantiatedType is Feature) && instantiatedType.type.Any(item => (item is Behavior)) && instantiatedType.type.Count() == 1
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
featuring.type.AsOrderedSet().Select(featuringTypes => !(chainingFeature.Any()) ? featuringTypes : featuringTypes.Union(chainingFeature.First().featuringType).AsOrderedSet())
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
ownedMember.OfType<BindingConnector>().Any(b => b.relatedFeatures.Contains(targetFeature) && b.relatedFeatures.Contains(result))
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
ownedFeatureMembership.OfType<TransitionFeatureMembership>().Where(item => item.kind == TransitionFeatureKind.trigger).Select(item2 => item2.transitionFeature).OfType<Expression>()
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
(new OrderedSet(this)).Closure(item =>item.typingFeatures()).SelectMany(item2 => item.typing.AsOrderedSet<Types>()).Reject(t1 => t1.types->Any(t2 => t2 <> t1 and t2.specializes(t1)))
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
feature.Where(f => directionOf(f).Select(direction => direction == FeatureDirectionKind._)) || direction == FeatureDirectionKind.inout
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
// TODO
sourceConnector.OfType<Succession>().Select(item => item.connectorEnd.ElementAt(0)).All(sourceMult => multiplicityHasBounds(sourceMult, 1, 1))
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
ownedMembership.OfType<StateSubactionMembership>().IsUnique(item => item.kind);
```
# implies, asSet
### OCL
``` OCL 
crossFeature <> null implies
    crossFeature.type->asSet() = type->asSet()
```
### C#
``` CSharp 
(!(crossFeature != null) || crossFeature.type.AsSet() == type.AsSet())
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
(!(owningType != null && ((owningType is ) && this == (()owningType).result || (owningType is Expression) && this == ((Expression)owningType).result)) || (()owningType.ownedSpecialization.general.Where(item => (item is Function) || (item is Expression)).All(supertype => (redefines() is )).superType).result)
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
(relatedType.Count() < 2 ? new OrderedSet() : relatedType.SubSequence(2, relatedType.Count()).AsOrderedSet())
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
instantiatedType.feature.Where(item => owningMembership.visibility == VisibilityKind.public).Select(features => result.ownedFeature.All(f => f.ownedRedefinition.redefinedFeature.Intersect(features).Count() == 1))
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
inputParameter(1).Select(targetParameter => targetParameter != null && targetParameter.ownedFeature.Any() && targetParameter.First().ownedFeature.Any() && targetParameter.First().ownedFeature.First().redefines(referent))
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
(owningNamespace == null ? null : (name != null && owningNamespace.ownedMember.Where(m => m.name == name).IndexOf(this) != 1
? null : (owningNamespace.owner == null ? escapedName() : (owningNamespace.qualifiedName == null || escapedName() == null ?
null : owningNamespace.qualifiedName + "::" + escapedName()))))
```
# notEqual, xor, null, 
### OCL
``` OCL 
ownedAnnotatingElement <> null xor owningAnnotatingElement <> null
```
### C#
``` CSharp 
ownedAnnotatingElement != null ^ owningAnnotatingElement != null
```
# empty sets as parameter
### OCL
``` OCL 
inheritedMemberships(Set{}, Set{}, false)
```
### C#
``` CSharp 
inheritedMemberships(new Set(), new Set(), false)
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
(!succession.targetFeature.Any() ? null : (succession.targetFeature.First().featureTarget).Select(targetFeature =>
(!(targetFeature is ActionUsage) ? null : ((ActionUsage)targetFeature))))
```
# simplest if ever
### OCL
``` OCL 
if chainingFeature->isEmpty() then self else chainingFeature->last() endif
```
### C#
``` CSharp 
(!chainingFeature.Any() ? this : chainingFeature.Last())
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
ownedMembership.selectByKind(ResultExpressionMembership).All(mem => ownedFeature.selectByKind(BindingConnector).Any(binding => binding.relatedFeature.Contains(result) && binding.relatedFeature.Contains(mem.ownedResultExpression.result)))
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
((Feature)resolveGlobal("Metaobjects::Metaobject::annotatedElement").memberElement).Select(baseAnnotatedElementFeature => feature.Where(item => specializes(baseAnnotatedElementFeature)).Where(item2 => item2 != baseAnnotatedElementFeature).Select(annotatedElementFeatures => (!(annotatedElementFeatures.Any()) || annotatedElementTypes is (Feature) == annotatedElementFeatures.typing.type.ToHashSet())))
```