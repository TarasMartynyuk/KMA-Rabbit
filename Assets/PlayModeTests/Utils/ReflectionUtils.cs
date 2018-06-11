using System;
using System.Reflection;

namespace PlayModeTests.Utils
{
    static class ReflectionUtils
    {
        public static void InsertPrivateMember<T>(string memberName, object mock, T instance)
        {
            var field = typeof(T).GetField(memberName,
                BindingFlags.Instance | 
                BindingFlags.NonPublic);

            if(field == null) 
            { throw new ArgumentException($"{typeof(T)} does not have private member with name {memberName}"); }

            field.SetValue(instance, mock);
        }

        public static void InsertProperty<T>(string propname, object mock, T instance)
        {
            var prop = typeof(T).GetProperty(propname,
                BindingFlags.Instance | 
                BindingFlags.Public |
                BindingFlags.NonPublic);

            if(prop == null) 
            { throw new ArgumentException($"{typeof(T)} does not have property with name {propname}"); }

            prop.SetValue(instance, mock);
        }

        public static void InsertPrivateStaticField<T>(string memberName, object mock)
        {
            var field = typeof(T).GetField(memberName, 
                BindingFlags.Static | 
                BindingFlags.NonPublic);

            if(field == null) 
            { throw new ArgumentException($"{typeof(T)} does not have private member with name {memberName}"); }

            field.SetValue(null, mock);
        }
    }
}