﻿// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 12/12/2018
// ------------------------------------------------------------------------------------------------------------------------

namespace Apollo.Core.Contracts.Domain
{
    public interface IActivityNoteInfo
    {
        int Id { get; set; }
        string Note { get; set; }
    }
}