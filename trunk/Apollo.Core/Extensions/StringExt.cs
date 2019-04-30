// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/05/2018
// ------------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace Apollo.Core.Extensions
{
    public static class StringExt
    {
        public static string RemoveWhitespace(this string buffer)
        {
            return Regex.Replace(buffer, @"\s+", "");
        }
    }
}