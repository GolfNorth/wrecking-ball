using Game.SDK.Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.SDK.Editor
{
    /// <summary>
    /// Отрисовщик атрибута <see cref="TypeRestrictionAttribute"/>.
    /// Проверяет тип поля и если он не соответствует, то пытается найти нужный объект или сбрасывает значение.
    /// </summary>
    [CustomPropertyDrawer(typeof(TypeRestrictionAttribute))]
    public class TypeRestrictionAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Отрисовывает GUI для свойства с учетом ограничений типа.
        /// </summary>
        /// <param name="rect">Прямоугольник, в котором будет отрисовано свойство.</param>
        /// <param name="property">Сериализованное свойство, которое будет отрисовано.</param>
        /// <param name="label">Метка для свойства.</param>
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference ||
                attribute is not TypeRestrictionAttribute restriction)
                return;

            EditorGUI.BeginChangeCheck();

            var referenceValue = EditorGUI.ObjectField(rect, label, property.objectReferenceValue, typeof(Object),
                restriction.AllowSceneObjects);

            if (!EditorGUI.EndChangeCheck())
                return;

            if (referenceValue != null)
            {
                var oldValue = referenceValue;
                var type = referenceValue.GetType();

                if (!restriction.Type.IsAssignableFrom(type))
                {
                    if (referenceValue is GameObject gameObject)
                    {
                        referenceValue = gameObject.GetComponent(restriction.Type);
                    }
                    else if (referenceValue is Component component)
                    {
                        referenceValue = component.gameObject.GetComponent(restriction.Type);
                    }
                    else
                    {
                        referenceValue = null;
                    }
                }

                if (referenceValue == null && !restriction.SetToNull)
                {
                    referenceValue = oldValue;
                }
            }

            property.objectReferenceValue = referenceValue;
        }
    }
}