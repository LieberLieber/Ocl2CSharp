grammar OCL;

// Parser Rules

oclFile
    : packageDeclaration* contextDeclaration* EOF
    ;

packageDeclaration
    : 'package' pathName contextDeclaration* 'endpackage'
    ;

contextDeclaration
    : 'context' contextDeclarationTail
    ;

contextDeclarationTail
    : pathName (classifierContextBody | operationContextBody)
    ;

classifierContextBody
    : (invClause | defClause)+
    ;

invClause
    : 'inv' NAME? ':' oclExpression
    ;

defClause
    : 'def' NAME? ':' defBody
    ;

defBody
    : NAME ':' typeSpecifier '=' oclExpression
    ;

operationContextBody
    : '::' operationName '(' formalParameterList? ')' ':' returnTypeSpecifier?
      (preClause | postClause | bodyClause)*
    ;

preClause
    : 'pre' NAME? ':' oclExpression
    ;

postClause
    : 'post' NAME? ':' oclExpression
    ;

bodyClause
    : 'body' NAME? ':' oclExpression
    ;

oclExpression
    : letExpression
    | ifExpression
    | logicalExpression
    ;

letExpression
    : 'let' variableDeclaration (',' variableDeclaration)* 'in' oclExpression
    ;

variableDeclaration
    : NAME (':' typeSpecifier)? '=' oclExpression
    ;

ifExpression
    : 'if' oclExpression 'then' oclExpression 'else' oclExpression 'endif'
    ;

logicalExpression
    : notExpression
    | logicalExpression 'and' notExpression
    | logicalExpression 'or' notExpression
    | logicalExpression 'xor' notExpression
    | logicalExpression 'implies' notExpression
    ;

notExpression
    : 'not' notExpression
    | relationalExpression
    ;

relationalExpression
    : additiveExpression
    | additiveExpression '=' additiveExpression
    | additiveExpression '<>' additiveExpression
    | additiveExpression '<' additiveExpression
    | additiveExpression '>' additiveExpression
    | additiveExpression '<=' additiveExpression
    | additiveExpression '>=' additiveExpression
    ;

additiveExpression
    : multiplicativeExpression
    | additiveExpression '+' multiplicativeExpression
    | additiveExpression '-' multiplicativeExpression
    ;

multiplicativeExpression
    : unaryExpression
    | multiplicativeExpression '*' unaryExpression
    | multiplicativeExpression '/' unaryExpression
    | multiplicativeExpression 'mod' unaryExpression
    | multiplicativeExpression 'div' unaryExpression
    ;

unaryExpression
    : '-' unaryExpression
    | postfixExpression
    ;

postfixExpression
    : primaryExpression
    | postfixExpression '.' callExpression
    | postfixExpression '->' callExpression
    ;

primaryExpression
    : literalExpression
    | 'self'
    | NAME
    | '(' oclExpression ')'
    ;

callExpression
    : NAME '(' argumentList? ')'
    | NAME
    ;

argumentList
    : oclExpression (',' oclExpression)*
    ;

literalExpression
    : IntegerLiteral
    | RealLiteral
    | StringLiteral
    | BooleanLiteral
    | NullLiteral
    | collectionLiteralExpression
    ;

collectionLiteralExpression
    : collectionKind '{' collectionLiteralParts? '}'
    ;

collectionLiteralParts
    : collectionLiteralPart (',' collectionLiteralPart)*
    ;

collectionLiteralPart
    : oclExpression ('..' oclExpression)?
    ;

pathName
    : NAME ('::' NAME)*
    ;

operationName
    : NAME
    ;

returnTypeSpecifier
    : typeSpecifier
    ;

typeSpecifier
    : pathName
    | collectionTypeSpecifier
    ;

collectionTypeSpecifier
    : collectionKind '(' typeSpecifier ')'
    ;

collectionKind
    : 'Set'
    | 'Bag'
    | 'Sequence'
    | 'Collection'
    | 'OrderedSet'
    ;

formalParameterList
    : formalParameter (',' formalParameter)*
    ;

formalParameter
    : NAME ':' typeSpecifier
    ;

// Lexer Rules

IntegerLiteral : [0-9]+;
RealLiteral    : [0-9]+ '.' [0-9]+;
StringLiteral  : '\'' (~['\\\r\n] | '\\' .)* '\'';
BooleanLiteral : 'true' | 'false';
NullLiteral    : 'null';

NAME : [a-zA-Z_][a-zA-Z0-9_]*;

WS         : [ \t\r\n]+ -> skip;
COMMENT    : '--' ~[\r\n]* -> skip;
ML_COMMENT : '/*' .*? '*/' -> skip;
