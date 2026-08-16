#if NETSTANDARD2_0
// The compiler emits this marker for init-only properties. netstandard2.0 does
// not provide it, so the package carries the conventional compatibility shim.
namespace System.Runtime.CompilerServices;

internal static class IsExternalInit
{
}
#endif
