using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PWH.ExposedField
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExposedFieldAttribute : Attribute
    {
        public string displayName;

        public ExposedFieldAttribute(string displayName)
        {
            this.displayName = displayName;
        }
    }
}
