using System.Reflection;

namespace Ecommerce.Catalog.Application;

public sealed class AssemblyReference
{
    public static Assembly Assembly => typeof(Assembly).Assembly;
}
