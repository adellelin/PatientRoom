/* Copyright (c) 2017 ExT (V.Sigalkin) */

using UnityEngine;

using System;
using System.Reflection;

namespace extOSC.Core.Internals
{
    [Serializable]
    public class OSCReflectionMember
    {
        #region Public Vars

        public Component Target;

        public string MemberName;

        #endregion

        #region Public Methods

        public bool IsValid()
        {
            return GetMemberInfo() != null;
        }

        public Type GetMemberType()
        {
            var memberInfo = GetMemberInfo();
            if (memberInfo != null)
            {
                if (memberInfo is FieldInfo)
                    return ((FieldInfo)memberInfo).FieldType;

                if (memberInfo is PropertyInfo)
                    return ((PropertyInfo)memberInfo).PropertyType;

                if (memberInfo is MethodInfo)
                    return ((MethodInfo)memberInfo).ReturnType;
            }

            return null;
        }

        public OSCReflectionProperty GetProperty()
        {
            return OSCReflectionProperty.Create(Target, GetMemberInfo());
        }

        #endregion

        #region Private Methods

        private MemberInfo GetMemberInfo()
        {
            if (Target == null || string.IsNullOrEmpty(MemberName))
                return null;

            var members = Target.GetType().GetMember(MemberName);
            return members.Length > 0 ? members[0] : null;
        }

        #endregion
    }
}