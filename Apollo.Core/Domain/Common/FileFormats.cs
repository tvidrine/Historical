// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/03/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Domain.Common
{
    [Flags]
    public enum FileFormats
    {
        NotSet = 0,
        Csv = 0x1,
        Pdf= 0x2,
        Xml= 0x4
    }

    public static class FileFormatsExt
    {
        public static FileFormats ToFileFormats(this string value)
        {
            var fileFormats = (FileFormats) Enum.Parse(typeof(FileFormats), value);
            
            return fileFormats;
        }
    }
}