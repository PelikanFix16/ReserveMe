using System.Dynamic;
using System.Reflection;

namespace SharedKernel.Domain.Aggregate
{
    internal class PrivateReflectionDynamicObject : DynamicObject
    {
        public object? RealObject { get; set; }

        private const BindingFlags BindingFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        internal static object? WrapObjectIfNeeded(object? o)
        {
            // Don't wrap primitive types, which don't have many interesting internal APIs
            if (o?.GetType().IsPrimitive != false || o is string)
                return o;

            return new PrivateReflectionDynamicObject() { RealObject = o };
        }

        // Called when a method is called
        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (RealObject == null)
                throw new ArgumentNullException($"RealObject {RealObject}");

            result = InvokeMemberOnType(RealObject.GetType(), RealObject, binder.Name, args);
            // Wrap the sub object if necessary. This allows nested anonymous objects to work.
            result = WrapObjectIfNeeded(result);

            return true;
        }

        private static object? InvokeMemberOnType(Type type, object target, string name, object?[]? args)
        {
            try
            {
                // Try to invoke the method
                return type.InvokeMember(
                    name,
                    BindingFlags.InvokeMethod | BindingFlag,
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
