// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 01/25/2019
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Configuration;
using Apollo.Core.Contracts.Providers;
using Apollo.Core.Messages.Responses;
//using Box.V2.Config;
//using Box.V2.JWTAuth;
//using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;

namespace Apollo.Infrastructure.Providers
{
    public class FileShareProviderBox 
    {
        private ILogManager _logManager;
        private IAuditConfiguration _auditConfiguration;
        private readonly HttpClient _client = new HttpClient();

        public FileShareProviderBox(ILogManager logManager, IAuditConfiguration auditConfiguration)
        {
            _logManager = logManager;
            _auditConfiguration = auditConfiguration;
        }
        public GetResponse<string> CreateShareFolderLink(string folderName, string description)
        {
            return CreateShareFolderLinkAsync(folderName, description).Result;
        }

        public async Task<GetResponse<string>> CreateShareFolderLinkAsync(string folderName, string description)
        { 
            var response = new GetResponse<string>();
            try
            {
                // Authorize
                var token = await GetAccessTokenAsync();


                // Create Folder
                var baseUrl = "https://api.box.com/2.0";
                
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
               
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var foo = await _client.GetAsync($@"{baseUrl}/folder/0/items");
                var fooData = await foo.Content.ReadAsStringAsync();
                var folderResponse = await _client.PostAsJsonAsync(
                    $@"{baseUrl}/folders/0/",
                    new
                    {
                        name = folderName,
                        parent = new
                        {
                            id = 0
                        }
                      
                    });

                var data = await folderResponse.Content.ReadAsStringAsync();

                // Create link
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "FileShareProviderBox.CreateShareLinkAsync");
                response.AddError(e);
            }

            return response;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            // JWT Assertion
            //var config = new BoxConfig();
            //var key = GetKey(config);
            //var claims = GetClaims(config);
            //var authenticationUrl = "https://api.box.com/oauth2/token";
            //var expirationTime = DateTime.UtcNow.AddSeconds(59);
            //var payload = new JwtPayload(
            //    config.BoxSettings.ClientId,
            //    authenticationUrl,
            //    claims,
            //    null,
            //    expirationTime);
            //var credentials = new SigningCredentials(new RsaSecurityKey(key), SecurityAlgorithms.RsaSha512);

            //var header = new JwtHeader(signingCredentials: credentials);
            //var jst = new JwtSecurityToken(header, payload);
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var assertion = tokenHandler.WriteToken(jst);

            //// Authentication
            //var content = new FormUrlEncodedContent(new[]
            //{
            //        // This specifies that we are using a JWT assertion
            //        // to authenticate
            //        new KeyValuePair<string, string>(
            //            "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer"),
            //        // Our JWT assertion
            //        new KeyValuePair<string, string>(
            //            "assertion", assertion),
            //        // The OAuth 2 client ID and secret
            //        new KeyValuePair<string, string>(
            //            "client_id", config.BoxSettings.ClientId),
            //        new KeyValuePair<string, string>(
            //            "client_secret", config.BoxSettings.ClientSecret)
            //    });

            //var authResponse = await _client.PostAsync(authenticationUrl, content);
            //var data = await authResponse.Content.ReadAsStringAsync();
            //var token = JsonConvert.DeserializeObject<Token>(data);
            //return token.access_token;
            return string.Empty;
        }
        private RSA CreateRsaProvider(RSAParameters rp)
        {
            var rsaCsp = RSA.Create();
            rsaCsp.ImportParameters(rp);

            return rsaCsp;
        }

        private RSAParameters ToRsaParameters(RsaPrivateCrtKeyParameters privateKey)
        {
            var rp = new RSAParameters
            {
                Modulus = privateKey.Modulus.ToByteArrayUnsigned(),
                Exponent = privateKey.PublicExponent.ToByteArrayUnsigned(),
                P = privateKey.P.ToByteArrayUnsigned(),
                Q = privateKey.Q.ToByteArrayUnsigned(),
                
            };

            rp.D = ConvertRsaParametersField(privateKey.Exponent, rp.Modulus.Length);
            rp.DP = ConvertRsaParametersField(privateKey.DP, rp.P.Length);
            rp.DQ = ConvertRsaParametersField(privateKey.DQ, rp.Q.Length);
            rp.InverseQ = ConvertRsaParametersField(privateKey.QInv, rp.Q.Length);
            return rp;
        }

        private byte[] ConvertRsaParametersField(BigInteger n, int size)
        {
            var bs = n.ToByteArrayUnsigned();

            if (bs.Length == size)
                return bs;

            if(bs.Length > size)
                throw new ArgumentException("Specified size too small", "size");

            var padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);

            return padded;
        }

        private RSA GetKey(BoxConfig config)
        {
            
            var appAuth = config.BoxSettings.AppAuth;
            var stringReader = new StringReader(appAuth.PrivateKey);
            var passwordFinder = new PasswordFinder(appAuth.PassPhrase);
            var pemReader = new PemReader(stringReader, passwordFinder);
            var keyParams = (RsaPrivateCrtKeyParameters)pemReader.ReadObject();
            return CreateRsaProvider(ToRsaParameters(keyParams));
        }

        private IList<Claim> GetClaims(BoxConfig config)
        {
            var randomNumber = new byte[64];
            RandomNumberGenerator.Create().GetBytes(randomNumber);
            var jti = Convert.ToBase64String(randomNumber);

            return new List<Claim>
            {
                new Claim("sub", config.EnterpriseId),
                new Claim("box_sub_type", "enterprise"),
                new Claim("jti", jti)
            };
        }
    }

    public class BoxConfig
    {
        public BoxConfig()
        {
            BoxSettings = new BoxSettings1();
        }
        public string EnterpriseId => "160706641";
        public BoxSettings1 BoxSettings { get; set; }

        public class BoxSettings1
        {
            public BoxSettings1()
            {
                AppAuth = new AppAuth1();
            }
            public string ClientId => "uwlxl89u9gkrj5hqcgwsm2ft5pfqnhv2";
            public string ClientSecret => "gYRTvX8c7BxZYRO2MB0nRmY7XwK33AzK";
            public AppAuth1 AppAuth { get; set; }
            public class AppAuth1
            {
                public string PrivateKey => "-----BEGIN ENCRYPTED PRIVATE KEY-----\nMIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIALaAJlWtyQcCAggA\nMBQGCCqGSIb3DQMHBAhu4FeaIFh7KwSCBMjZdMGgnp/uM442vR1/oHbTwhterkf2\nY9hzPul8Yh0TNVY1JmFhor+v/zjC0nUZURHbV8G9+MyigsfOa73c2Pq3ohU3XU1s\nbiKKxnhFNXj9VuZVyC142H6TOmqImE0jLu5bjEv+Wfz/UgFvJaIXk4rsedjsDCqT\n6o+E0Lr6dFw6IIvRj0ZhF+Cd9BrEVoQ/Ai0A0KSuZMJ0r7A5DkAEUAsYFWHGBdSU\nwDMRPajRQ0K/6eyjTuIbLlzGMxJ5AJVbQzZA9OFEI6Jc4d9PDq8VK5LnyUZpAOpj\n3xzdw0U0fOf5crUWd9F3EzHoT3yc0J1ToeALEMQYfRZGr2P3Wxuka/W46y1VazPS\ntuuSvxCD47EXYCK/az92M0soGi9EfDc/DeixzMWPL5+6kLF6+sb40Cdd9bcZzE+V\n91KMzCjASioVSHZPgYn81ZFeYjlsfOB7Gi3cZXPxwOvk6B+ZFibiKMriF6TKcV5t\nKkf1ISECncaNp9ej94DY9BvynY2cGG2IXH1WWrOMF+2hz/1HdXbJ2pIZVERx/FWo\nz7VqyFRXkC8NizsPHV/AE6rysb0r1TZ6CVXYZ771Tid9ub170wJHPHOW/ZnYyUfH\ncxKfzhXeJ70V+c2kpz5WoNv0IVi38x65RdJc9B0U2/Xm0IP9jyQPWSuXIeuudAET\ntHynK2NreFP3y3aYOT52gX3t1hO199UyrQcleeoejlfV+iNkecj3w7nRKLR0JT8h\nQK0JDLwzojEojUFOPSyalrCr7qs53SjtlHUMCyZxetvn+MUoU56it34+4YN9tag0\nykTuQuzgHExrqHgTk21UeZFcHaW3bpUYfru7QolQwIzsHmWQeOV5VxrbVLF1ZxNZ\nf56N1NPlAROMukxzHMBwyVFMrtRZQ6L1ypBc+M+E51eB3AI7TCjGdEAyBrqz5w0l\nP/J12e09jxpDZpXSOO5ShWnzzwFdGzzw/gQyRX8my9NxdRWkYBwysXf2ds8b8q78\nei0BCJRMTbZP4esxWyuewnz9+fkBoEz/kA7Pqqoe21tW4DB3rXMTb6TCAIYo+Rg4\nFZBjzyZqOUoyNhvI11CRCtewnUf2NzX8fr80miwJ5wzrAHKm954laZYbxTTTqpaa\n7JiSZq5ADpJi9etc+4nGxCyUn7Odrn4OkI8yRPTs3bkeRaAKWwu2Wne3xZDL0fg5\nBFEO5lHAaozLtSHAvToeh7tJze2JWeMFfl6h95sWUESjo4o7fzI6G5ULGn/KbnTc\ntJrE0KM18kD9fDrIaVEbMo6zwnpTeDtQgAVKhYPkoPQtzf94tjsLe8NdeyI6H6RA\nSdz/+BH32rVZjUdXeu6SVdPHYXEIl9ow8wLryhbT3MNHwsJauWncJLteCzCFvACp\nv23pfhcZdHF4L+EMUMvdjiDzfrGxZdyJi0WE/j20N9BczuS8b+LsjxSfm+c45k9Y\nyAE+o6X3DzFMH1PgZ+ACx1CDNk37UnjnYtkjw8P+NuLNAQbzYflP8W07cdv4ix6K\nTOhtbYQSp2PABinEI1iQ5Z7N7p4RyTK74lpZ7q547ytBlPPwpnzneu+2RQBtgewy\nRZ8F49HDsvQTmHwdgFp2Y60BafTnI8uaFXUqCSkUtnv/iq+sLVQw9/rdJS/e0+lz\nYgQ=\n-----END ENCRYPTED PRIVATE KEY-----\n";
                public string PassPhrase => "0a779b14a32dce78bff8bbb4fd6219c7";
                public string PublicKeyId => "urzri07p";
            }
        }
    
    }
    
    public class Token
    {
        public string access_token { get; set; }
    }
}