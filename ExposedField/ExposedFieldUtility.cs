using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PWH.ExposedField
{
    public static class ExposedFieldUtility
    {
        public static List<ExposedFieldInfo> GetExposedMembers()
        {
            List<ExposedFieldInfo> exposedFieldInfos = new List<ExposedFieldInfo>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    MemberInfo[] members = type.GetMembers(flags);

                    foreach (MemberInfo member in members)
                    {
                        if (member.CustomAttributes.ToArray().Length > 0)
                        {
                            ExposedFieldAttribute attribute = member.GetCustomAttribute<ExposedFieldAttribute>();
                            if (attribute != null)
                            {
                                exposedFieldInfos.Add(new ExposedFieldInfo(member, attribute, (FieldInfo)member));
                            }
                        }
                    }
                }
            }

            return exposedFieldInfos;
        }
    }

    public struct ExposedFieldInfo
    {
        public MemberInfo member;
        public ExposedFieldAttribute attribute;
        public FieldInfo field;

        public ExposedFieldInfo(MemberInfo info, ExposedFieldAttribute attribute, FieldInfo field)
        {
            this.member = info;
            this.attribute = attribute;
            this.field = field;
        }
    }
}

