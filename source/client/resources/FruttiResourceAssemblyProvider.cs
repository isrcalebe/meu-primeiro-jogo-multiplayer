using System.Reflection;

namespace Frutti.Game.Resources;

public static class FruttiResourceAssemblyProvider
{
    public static Assembly Assembly => typeof(FruttiResourceAssemblyProvider).Assembly;
}
