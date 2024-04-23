using System.Reflection;

namespace PWPA.CallLogging.BackEnd.ApplicationCore;

public static class ApplicationCoreAssemblyReference
{
    public static Assembly Assembly { get => typeof(ApplicationCoreAssemblyReference).Assembly; }
}