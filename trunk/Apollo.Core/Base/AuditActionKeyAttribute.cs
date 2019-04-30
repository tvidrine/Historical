// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 08/01/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Apollo.Core.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuditActionKeyAttribute : Attribute
    {
        public AuditActionKeyAttribute(string key)
        {
            Key = key;
        }
        public string Key { get; set; }
    }
}