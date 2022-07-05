using System;

namespace DbInteractionPet.Database
{
    /// <summary>
    /// Атрибут для фиксирования первичных ключей модели
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}
