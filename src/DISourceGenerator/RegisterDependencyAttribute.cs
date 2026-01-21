using System;
using System.Collections.Generic;
using System.Text;

namespace DISourceGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterDependencyAttribute : Attribute
    {
    }
}
