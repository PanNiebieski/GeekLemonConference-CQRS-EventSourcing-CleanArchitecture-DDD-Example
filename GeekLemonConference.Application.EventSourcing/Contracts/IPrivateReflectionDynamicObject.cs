using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace GeekLemonConference.Application.EventSourcing.Contracts
{
    public interface IPrivateReflectionDynamicObjectBuilder
    {
        object WrapObjectIfNeeded(object o);

        bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result);

        object InvokeMemberOnType(Type type, object target, string name, object[] args);

    }

    public class PrivateReflectionDynamicObject : DynamicObject
    {
        public object RealObject { get; set; }
        private const BindingFlags bindingFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    }

    public class PrivateReflectionDynamicObjectBuilder : DynamicObject,
        IPrivateReflectionDynamicObjectBuilder
    {
        public object RealObject { get; set; }
        private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public object WrapObjectIfNeeded(object o)
        {
            // Don't wrap primitive types, which don't have many interesting internal APIs
            if (o == null || o.GetType().IsPrimitive || o is string)
                return o;

            return new PrivateReflectionDynamicObject() { RealObject = o };
        }

        // Called when a method is called
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = InvokeMemberOnType(RealObject.GetType(), RealObject, binder.Name, args);

            // Wrap the sub object if necessary. This allows nested anonymous objects to work.
            result = WrapObjectIfNeeded(result);

            return true;
        }

        public object InvokeMemberOnType(Type type, object target, string name, object[] args)
        {
            try
            {
                // Try to incoke the method
                return type.InvokeMember(
                    name,
                    BindingFlags.InvokeMethod | bindingFlags,
                    null,
                    target,
                    args);
            }
            catch (MissingMethodException)
            {
                // If we couldn't find the method, try on the base class
                if (type.BaseType != null)
                {
                    return InvokeMemberOnType(type.BaseType, target, name, args);
                }
                //Don't care if the method don't exist.
                return null;
            }
        }
    }
}
