using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;

namespace PWH.ExposedField.Examples
{
    /// <summary>
    /// This replaces {0} with the value of [0] in the ExposedValueSelector array.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [ExecuteAlways]
    public class ExposedValueText : MonoBehaviour
    {
        TextMeshProUGUI _textField;

        [TextArea()]
        public string textValue;

        public ExposedValueSelector[] exposedValues;

        Dictionary<string, FieldInfo> _fieldNameDictionary;
        private List<string> _instancedValues;

        void Awake()
        {
            _textField = GetComponent<TextMeshProUGUI>();
            if (_fieldNameDictionary == null)
            {
                CreateDictionary();
            }
        }

        void Start()
        {
            if (Application.isPlaying)
            {
                _textField.text = GetFormattedString();
            }
        }

        void Update()
        {
            if (!Application.IsPlaying(gameObject))
            {
                _textField.text = textValue;
            }
        }

        void CreateDictionary()
        {
            _fieldNameDictionary = new Dictionary<string, FieldInfo>();

            foreach(ExposedFieldInfo info in ExposedFieldUtility.GetExposedMembers())
            {
                _fieldNameDictionary.Add(info.member.Name,info.field);
            }
        }

        string GetFormattedString()
        {
            _instancedValues = new List<string>();

            for (int i = 0; i < exposedValues.Length; i++)
            {
                FieldInfo field = _fieldNameDictionary[exposedValues[i].fieldName];
                string value = GetValue(field);

                _instancedValues.Add(value);
            }

            return string.Format(textValue, _instancedValues.ToArray());
        }

        // Very important Example. Without doing this first you won't be getting the actual values
        string GetValue(FieldInfo field)
        {
            object obj = null;
            string value = "N/A";

            if (field.ReflectedType == SingletonDataHolder.instance.playerData.GetType())
            {
                obj = field.GetValue(SingletonDataHolder.instance.playerData);
            }
            if (field.ReflectedType == SingletonDataHolder.instance.marketCostsData.GetType())
            {
                obj = field.GetValue(SingletonDataHolder.instance.marketCostsData);
            }

            if (obj != null)
            {
                value = obj.ToString();
            }

            return value;
        }
    }
}