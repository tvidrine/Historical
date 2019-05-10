// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/25/2019
// ------------------------------------------------------------------------------------------------------------------------

using Org.BouncyCastle.OpenSsl;

namespace Apollo.Infrastructure
{
    public class PasswordFinder : IPasswordFinder
    {
        private readonly string _password;

        public PasswordFinder(string password)
        {
            _password = password;
        }

        public char[] GetPassword()
        {
            return _password.ToCharArray();
        }
    }
}