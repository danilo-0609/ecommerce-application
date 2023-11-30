using System.Reflection;

namespace Ecommerce.UserAccess.Application;

internal sealed class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
