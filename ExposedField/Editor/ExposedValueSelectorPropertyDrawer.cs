using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;
using System.Linq;
using PWH.ExposedField;

namespace PWH.ExposedField
{
    [CustomPropertyDrawer(typeof(ExposedValueSelector))]
    public class ExposedValueSelectorPropertyDrawer : PropertyDrawer
    {
        List<FieldInfo> _fields;
        List<string> _fieldNames;

        bool gotFields = false;

        int index;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!gotFields) { GetFields(); }

            SerializedProperty fieldNameProperty = property.FindPropertyRelative("fieldName");

            index = GetFieldName(fieldNameProperty.stringValue);
            index = EditorGUI.Popup(position, index, _fieldNames.ToArray());
            fieldNameProperty.stringValue = _fields[index].Name;
        }

        void GetFields()
        {
            _fields = new List<FieldInfo>();
            _fieldNames = new List<string>();

            foreach (ExposedFieldInfo info in ExposedFieldUtility.GetExposedMembers())
            {
                _fields.Add(info.field);
                _fieldNames.Add($"{info.member.ReflectedType}/{info.attribute.displayName}");
            }
            gotFields = true;
        }

        int GetFieldName(string value)
        {
            string fieldName = value;
            int count = 0;
            foreach (FieldInfo member in _fields)
            {
                if (member.Name == fieldName)
                {
                    return count;
                }

                count++;
            }

            return 0;
        }
    }
}