using System;
using UnityEngine;

namespace Game.SDK.Attributes
{
    /// <summary>
    /// Атрибут, который ограничивает тип значений поля для объектов, производных от <see cref="UnityEngine.Object"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class TypeRestrictionAttribute : PropertyAttribute
    {
        /// <summary>
        /// Тип, которому должно соответствовать значение поля.
        /// </summary>
        public readonly Type Type;

        /// <summary>
        /// Флаг, указывающий, разрешаются ли объекты, находящиеся на сцене.
        /// </summary>
        public readonly bool AllowSceneObjects;

        /// <summary>
        /// Флаг, указывающий, нужно ли сбрасывать значение поля в null, если текущее значение не соответствует указанному типу.
        /// </summary>
        public readonly bool SetToNull;

        /// <summary>
        /// Конструктор атрибута TypeRestriction.
        /// </summary>
        /// <param name="type">Требуемый тип значения поля.</param>
        /// <param name="allowSceneObjects">Флаг, разрешающий использование объектов со сцены. По умолчанию true.</param>
        /// <param name="setToNull">Флаг, указывающий, нужно ли сбрасывать значение в null при несоответствии типа. По умолчанию true.</param>
        public TypeRestrictionAttribute(Type type, bool allowSceneObjects = true, bool setToNull = true)
        {
            Type = type;
            AllowSceneObjects = allowSceneObjects;
            SetToNull = setToNull;
        }
    }
}