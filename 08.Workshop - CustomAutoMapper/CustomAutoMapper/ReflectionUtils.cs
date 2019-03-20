using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomAutoMapper
{
    public class ReflectionUtils
    {
        private static readonly HashSet<string> types = new HashSet<string>()
        {
            typeof(int).ToString(),
            typeof(string).ToString(),
            typeof(decimal).ToString(),
            typeof(double).ToString(),
            typeof(long).ToString(),
            typeof(ulong).ToString(),
            typeof(DateTime).ToString(),
            typeof(Guid).ToString(),
            typeof(int[]).ToString(),
            typeof(string[]).ToString(),
            typeof(decimal[]).ToString(),
            typeof(double[]).ToString(),
            typeof(long[]).ToString(),
            typeof(ulong[]).ToString(),
            typeof(DateTime[]).ToString(),
            typeof(Guid[]).ToString(),
            "System.Single",
            "System.Single[]",

        };

        public static bool IsPrimitive(Type type)
        {
            return types.Contains(type.FullName) || type.IsPrimitive
                || IsNullable(type) && IsPrimitive(Nullable.GetUnderlyingType(type))
                || type.IsEnum;
        }

        public static bool IsGenericCollection(Type type)
        {
            return (type.IsGenericType && (
                type.GetGenericTypeDefinition() == typeof(List<>)
                ||
                type.GetGenericTypeDefinition() == typeof(ICollection<>)
                ||
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                ||
                type.GetGenericTypeDefinition() == typeof(IList<>)))
                ||
                typeof(IList<>).IsAssignableFrom(type)
                ||
                typeof(HashSet<>).IsAssignableFrom(type);
        }

        public static bool IsNonGenericCollection(Type type)
        {
            return type.IsArray ||
                type == typeof(ArrayList) ||
                typeof(IList).IsAssignableFrom(type);
        }

        private static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>); 
        }
    }
}
