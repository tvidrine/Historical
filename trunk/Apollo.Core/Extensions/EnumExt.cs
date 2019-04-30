// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/23/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Apollo.Core.Extensions
{
    public static class EnumExt
    {
        public static string ToFriendlyName(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            if (name != null)
            {
                var fieldInfo = type.GetField(name);

                if (fieldInfo != null)
                {
                    if (Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                        return attribute.Description;
                }
            }

            return value.ToString();
        }

        public static string ToCategory(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            if (name != null)
            {
                var fieldInfo = type.GetField(name);

                if (fieldInfo != null)
                {
                    if (Attribute.GetCustomAttribute(fieldInfo, typeof(CategoryAttribute)) is CategoryAttribute attribute)
                        return attribute.Category;
                }
            }

            return value.ToString();
        }

        public static T FromCategory<T>(this string category) where T 
            : struct
        {
            Debug.Assert(typeof(T).IsEnum);

            return (T)typeof(T)
                .GetFields()
                .First(f => f.GetCustomAttributes(typeof(CategoryAttribute), false)
                    .Cast<CategoryAttribute>()
                    .Any(a => a.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                )
                .GetValue(null);
        }
    }
}