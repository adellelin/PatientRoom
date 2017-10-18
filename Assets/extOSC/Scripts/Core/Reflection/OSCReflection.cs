/* Copyright (c) 2017 ExT (V.Sigalkin) */

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace extOSC.Core.Internals
{
    public static class OSCReflection
    {
        #region Static Private Vars

        private static BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.Instance;

        #endregion

        #region Static Public Methods

        [Obsolete("\"GetProperties\" is deprecated, please use \"GetMembersByType\" instead.")]
        public static MemberInfo[] GetProperties(object target, Type propertyType)
        {
            return GetMembersByType(target, propertyType, OSCReflectionAccess.Any, OSCReflectionType.Field | OSCReflectionType.Property);
        }

        [Obsolete("\"GetProperties\" is deprecated, please use \"GetMembers\" instead.")]
        public static MemberInfo[] GetProperties(object target)
        {
            return GetMembers(target, OSCReflectionType.Field | OSCReflectionType.Property);
        }

        public static MemberInfo[] GetMembers(object memberTarget, OSCReflectionType memberTypes)
        {
            return GetMembers(memberTarget.GetType(), memberTypes);
        }

        public static MemberInfo[] GetMembers(Type targetType, OSCReflectionType memberTypes)
        {
            var members = new List<MemberInfo>();
            var typeMembers = targetType.GetMembers(_bindingFlags);

            foreach (var memberInfo in typeMembers)
            {
                if (memberInfo.GetCustomAttributes(typeof(ObsoleteAttribute), true).Count() > 0)
                    continue;

                if ((memberTypes & OSCReflectionType.Field) == OSCReflectionType.Field && memberInfo is FieldInfo)
                {
                    members.Add(memberInfo);
                }
                else if ((memberTypes & OSCReflectionType.Property) == OSCReflectionType.Property && memberInfo is PropertyInfo)
                {
                    members.Add(memberInfo);
                }
                else if ((memberTypes & OSCReflectionType.Method) == OSCReflectionType.Method && memberInfo is MethodInfo)
                {
                    var methodInfo = (MethodInfo)memberInfo;
                    if (methodInfo.IsSpecialName) continue;

                    members.Add(memberInfo);
                }
            }

            return members.ToArray();
        }

        public static MemberInfo[] GetMembersByType(object memberTarget, Type valueType, OSCReflectionAccess valueAccess, OSCReflectionType memberTypes)
        {
            return GetMembersByType(memberTarget.GetType(), valueType, valueAccess, memberTypes);
        }

        public static MemberInfo[] GetMembersByType(Type targetType, Type valueType, OSCReflectionAccess valueAccess, OSCReflectionType memberTypes)
        {
            var members = new List<MemberInfo>();
            var targetMembers = GetMembers(targetType, memberTypes);

            foreach (var memberInfo in targetMembers)
            {
                if (memberInfo is FieldInfo)
                {
                    var fieldInfo = (FieldInfo)memberInfo;

                    if (fieldInfo.FieldType == valueType)
                        members.Add(memberInfo);
                }
                else if (memberInfo is PropertyInfo)
                {
                    var propertyInfo = (PropertyInfo)memberInfo;

                    if (!CheckAccess(propertyInfo, valueAccess))
                        continue;

                    if (propertyInfo.PropertyType == valueType)
                        members.Add(memberInfo);
                }
                else if (memberInfo is MethodInfo)
                {
                    var methodInfo = memberInfo as MethodInfo;

                    if (!CheckAccess(methodInfo, valueAccess))
                        continue;

                    if (GetMethodReadType(methodInfo) == valueType ||
                        GetMethodWriteType(methodInfo) == valueType)
                        members.Add(memberInfo);
                }

            }

            return members.ToArray();
        }

        public static MemberInfo[] GetMembersByAccess(object target, OSCReflectionAccess valueAccess, OSCReflectionType memberTypes)
        {
            return GetMembersByAccess(target.GetType(), valueAccess, memberTypes);
        }

        public static MemberInfo[] GetMembersByAccess(Type targetType, OSCReflectionAccess valueAccess, OSCReflectionType memberTypes)
        {
            var members = new List<MemberInfo>();
            var targetMembers = GetMembers(targetType, memberTypes);

            foreach (var memberInfo in targetMembers)
            {
                if (memberInfo is FieldInfo)
                {
                    members.Add(memberInfo);
                }
                else if (memberInfo is PropertyInfo)
                {
                    var propertyInfo = (PropertyInfo)memberInfo;

                    if (!CheckAccess(propertyInfo, valueAccess))
                        continue;

                    members.Add(memberInfo);
                }
                else if (memberInfo is MethodInfo)
                {
                    var methodInfo = memberInfo as MethodInfo;

                    if (!CheckAccess(methodInfo, valueAccess))
                        continue;

                    members.Add(memberInfo);
                }
            }

            return members.ToArray();
        }

        public static bool CheckAccess(MemberInfo memberInfo, OSCReflectionAccess access)
        {
            if (memberInfo is FieldInfo)
                return true;
            if (memberInfo is PropertyInfo)
                return CheckAccess((PropertyInfo)memberInfo, access);
            if (memberInfo is MethodInfo)
                return CheckAccess((MethodInfo)memberInfo, access);

            return false;
        }

        public static bool CheckAccess(PropertyInfo propertyInfo, OSCReflectionAccess access)
        {
            if (access == OSCReflectionAccess.ReadWrite && propertyInfo.CanWrite && propertyInfo.CanRead)
                return true;
            if (access == OSCReflectionAccess.Read && propertyInfo.CanRead)
                return true;
            if (access == OSCReflectionAccess.Write && propertyInfo.CanWrite)
                return true;
            if (access == OSCReflectionAccess.Any)
                return true;

            return false;
        }

        public static bool CheckAccess(MethodInfo methodInfo, OSCReflectionAccess access)
        {
            if (access == OSCReflectionAccess.Read)
                return !(methodInfo.ReturnType == null || methodInfo.ReturnType == typeof(void)) && GetMethodWriteType(methodInfo) == null;
            if (access == OSCReflectionAccess.Write)
                return GetMethodWriteType(methodInfo) != null;
            if (access == OSCReflectionAccess.ReadWrite)
                return false;
            if (access == OSCReflectionAccess.Any)
                return CheckAccess(methodInfo, OSCReflectionAccess.Read) || CheckAccess(methodInfo, OSCReflectionAccess.Write);

            return false;
        }

        public static Type GetMethodReadType(MethodInfo methodInfo)
        {
            return methodInfo.ReturnType;
        }

        public static Type GetMethodWriteType(MethodInfo methodInfo)
        {
            var methodParameters = methodInfo.GetParameters();
            if (methodParameters.Length == 1)
                return methodParameters[0].ParameterType;

            return null;
        }

        #endregion
    }
}