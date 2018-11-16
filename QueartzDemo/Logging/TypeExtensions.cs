using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueartzDemo.Logging
{
    internal static class TypeExtensions
    {
        internal static MethodInfo GetMethodPortable(this Type type,string name)
        {
            return type.GetMethod(name);
        }

        internal static MethodInfo GetMethodPortable(this Type type, string name,params Type[] types)
        {
            return type.GetMethod(name,types);
        }
    }
}
