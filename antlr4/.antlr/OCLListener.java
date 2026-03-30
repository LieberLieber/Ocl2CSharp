// Generated from d:/Projects/External/Ocl2CSharp/antlr4/OCL.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link OCLParser}.
 */
public interface OCLListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link OCLParser#type}.
	 * @param ctx the parse tree
	 */
	void enterType(OCLParser.TypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#type}.
	 * @param ctx the parse tree
	 */
	void exitType(OCLParser.TypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#expressionList}.
	 * @param ctx the parse tree
	 */
	void enterExpressionList(OCLParser.ExpressionListContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#expressionList}.
	 * @param ctx the parse tree
	 */
	void exitExpressionList(OCLParser.ExpressionListContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterExpression(OCLParser.ExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitExpression(OCLParser.ExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#conditionalExpression}.
	 * @param ctx the parse tree
	 */
	void enterConditionalExpression(OCLParser.ConditionalExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#conditionalExpression}.
	 * @param ctx the parse tree
	 */
	void exitConditionalExpression(OCLParser.ConditionalExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#letExpression}.
	 * @param ctx the parse tree
	 */
	void enterLetExpression(OCLParser.LetExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#letExpression}.
	 * @param ctx the parse tree
	 */
	void exitLetExpression(OCLParser.LetExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#letBinding}.
	 * @param ctx the parse tree
	 */
	void enterLetBinding(OCLParser.LetBindingContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#letBinding}.
	 * @param ctx the parse tree
	 */
	void exitLetBinding(OCLParser.LetBindingContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#basicExpression}.
	 * @param ctx the parse tree
	 */
	void enterBasicExpression(OCLParser.BasicExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#basicExpression}.
	 * @param ctx the parse tree
	 */
	void exitBasicExpression(OCLParser.BasicExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#logicalExpression}.
	 * @param ctx the parse tree
	 */
	void enterLogicalExpression(OCLParser.LogicalExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#logicalExpression}.
	 * @param ctx the parse tree
	 */
	void exitLogicalExpression(OCLParser.LogicalExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#equalityExpression}.
	 * @param ctx the parse tree
	 */
	void enterEqualityExpression(OCLParser.EqualityExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#equalityExpression}.
	 * @param ctx the parse tree
	 */
	void exitEqualityExpression(OCLParser.EqualityExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#additiveExpression}.
	 * @param ctx the parse tree
	 */
	void enterAdditiveExpression(OCLParser.AdditiveExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#additiveExpression}.
	 * @param ctx the parse tree
	 */
	void exitAdditiveExpression(OCLParser.AdditiveExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#multiplicativeExpression}.
	 * @param ctx the parse tree
	 */
	void enterMultiplicativeExpression(OCLParser.MultiplicativeExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#multiplicativeExpression}.
	 * @param ctx the parse tree
	 */
	void exitMultiplicativeExpression(OCLParser.MultiplicativeExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#unaryExpression}.
	 * @param ctx the parse tree
	 */
	void enterUnaryExpression(OCLParser.UnaryExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#unaryExpression}.
	 * @param ctx the parse tree
	 */
	void exitUnaryExpression(OCLParser.UnaryExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#navigationExpression}.
	 * @param ctx the parse tree
	 */
	void enterNavigationExpression(OCLParser.NavigationExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#navigationExpression}.
	 * @param ctx the parse tree
	 */
	void exitNavigationExpression(OCLParser.NavigationExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#primaryFactor}.
	 * @param ctx the parse tree
	 */
	void enterPrimaryFactor(OCLParser.PrimaryFactorContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#primaryFactor}.
	 * @param ctx the parse tree
	 */
	void exitPrimaryFactor(OCLParser.PrimaryFactorContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#postfixSuffix}.
	 * @param ctx the parse tree
	 */
	void enterPostfixSuffix(OCLParser.PostfixSuffixContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#postfixSuffix}.
	 * @param ctx the parse tree
	 */
	void exitPostfixSuffix(OCLParser.PostfixSuffixContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#identOptType}.
	 * @param ctx the parse tree
	 */
	void enterIdentOptType(OCLParser.IdentOptTypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#identOptType}.
	 * @param ctx the parse tree
	 */
	void exitIdentOptType(OCLParser.IdentOptTypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#identOptTypeList}.
	 * @param ctx the parse tree
	 */
	void enterIdentOptTypeList(OCLParser.IdentOptTypeListContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#identOptTypeList}.
	 * @param ctx the parse tree
	 */
	void exitIdentOptTypeList(OCLParser.IdentOptTypeListContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#setExpression}.
	 * @param ctx the parse tree
	 */
	void enterSetExpression(OCLParser.SetExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#setExpression}.
	 * @param ctx the parse tree
	 */
	void exitSetExpression(OCLParser.SetExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#identifier}.
	 * @param ctx the parse tree
	 */
	void enterIdentifier(OCLParser.IdentifierContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#identifier}.
	 * @param ctx the parse tree
	 */
	void exitIdentifier(OCLParser.IdentifierContext ctx);
	/**
	 * Enter a parse tree produced by {@link OCLParser#qualified_name}.
	 * @param ctx the parse tree
	 */
	void enterQualified_name(OCLParser.Qualified_nameContext ctx);
	/**
	 * Exit a parse tree produced by {@link OCLParser#qualified_name}.
	 * @param ctx the parse tree
	 */
	void exitQualified_name(OCLParser.Qualified_nameContext ctx);
}