namespace Ocl2CSharp;

/// <summary>
/// Controls how an OCL expression is converted to C# by <see cref="OclToCSharpConverter"/>.
/// </summary>
public sealed class ConversionOptions
{
	/// <summary>
	/// When <see langword="true"/>, OCL <c>if…then…else…endif</c> expressions are emitted as
	/// multiline C# <c>if</c>/<c>else</c> statements (suitable for a method or property getter).
	/// When <see langword="false"/> (the default), they are emitted as inline ternary <c>?:</c> expressions.
	/// </summary>
	public bool UseIfStatement { get; init; }

	/// <summary>
	/// When <see langword="true"/>, the generated C# expression is wrapped as a complete
	/// <c>return</c> statement: <c>return &lt;expr&gt;;</c>
	/// When <see langword="false"/> (the default), the expression is terminated with <c>;</c> only.
	/// </summary>
	public bool CodeWithReturn { get; init; }
}
