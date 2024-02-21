using UnityEditor;
using UnityEngine;

namespace MbsCore.LightWeightRx.Editor
{
    [CustomPropertyDrawer(typeof(SerializedCallbackProperty<>), true)]
    internal sealed class SerializedCallbackPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("_value");
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, valueProperty, label);
            EditorGUI.EndProperty();
        }
    }
}