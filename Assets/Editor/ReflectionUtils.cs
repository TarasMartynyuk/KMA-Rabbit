using System;
using System.Reflection;

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