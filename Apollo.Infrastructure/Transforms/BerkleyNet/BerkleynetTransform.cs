// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) ZoomAudits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 05/18/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Apollo.Core.Configuration;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.Core.Contracts.Data;
using Apollo.Core.Contracts.Domain.Audit;
using Apollo.Core.Contracts.Domain.Policies;
using Apollo.Core.Domain.Audit;
using Apollo.Core.Domain.Communication;
using Apollo.Core.Domain.Policies;
using Apollo.Core.Messages.Requests;
using Apollo.Core.Messages.Responses;
using Apollo.Core.Messages.Results;

namespace Apollo.Infrastructure.Transforms.BerkleyNet
{
    public class BerkleynetTransform : IClientTransform
    {
        internal static Guid ClientKey = Guid.Parse("{B47D81EA-A1A7-48F0-BF3C-C06A7785AE71}");
        private readonly ILogManager _logManager;
        private readonly IAuditApplicationService _auditApplicationService;
        

        public BerkleynetTransform(ILogManager logManager, 
            IAuditApplicationService auditApplicationService)
        {
            _logManager = logManager;
            _auditApplicationService = auditApplicationService;
            
        }

        /// <summary>
        ///     Generates packages to be sent to Berkley Net.
        /// </summary>
        /// <param name="audits"></param>
        /// <param name="clientConfiguration"></param>
        /// <returns></returns>
        public TransformResult<IList<Packet>> From(IReadOnlyList<IAudit> audits, ClientConfiguration clientConfiguration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Transforms data packets into audits.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="packets"></param>
        /// <param name="clientConfiguration"></param>
        /// <returns></returns>
        public TransformResult<IReadOnlyList<IAudit>> To(IReadOnlyList<Packet> packets, ClientConfiguration clientConfiguration)
        {
            var result = new TransformResult<IReadOnlyList<IAudit>>();
            

            try
            {
                var audits = new List<Audit>();
                var serializer = new XmlSerializer(typeof(BracAuditOrder));

                foreach (var packet in packets)
                {
                    using (var ms = new MemoryStream(packet.Data))
                    {
                        using (var reader = new XmlTextReader(ms))
                        {
                            reader.Namespaces = true;
                            var auditOrder = (BracAuditOrder)serializer.Deserialize(reader);

                            // Populate a new audit
                            var auditTransformResult = TransformToAudit(auditOrder);

                            // Add any issues to the result to report back
                            result.Join<TransformResult<IReadOnlyList<IAudit>>>(auditTransformResult);

                            // if the result is successful, add it to the list of new audits
                            if(result.IsSuccessful)
                                audits.Add(auditTransformResult.Content);

                        }
                    }
                }
                   

                result.Content = audits;
            }
            catch (Exception e)
            {
                result.AddError(e);
                _logManager.LogError(e, "BerkleynetTransform.To");
            }

            return result;
        }

        private TransformResult<Audit> TransformToAudit(BracAuditOrder order)
        {
            var result = new TransformResult<Audit>();
            try
            {
                var auditRequest = new AuditRequest(order.PolicyNumber, order.PolicyEffDate, order.PolicyExpDate);

                var createResult = _auditApplicationService.CreateAsync(auditRequest).Result;
                
                result.Join<TransformResult<Policy>>(createResult);

                if (result.IsSuccessful)
                {
                    var audit = order.ToAudit(createResult.Content);
                    
                }
                    
            }
            catch (Exception e)
            {
                _logManager.LogError(e, "BerkleyTransform.TransformToAudit");

                result.AddError(e);
            }

            return result;
        }
    }
}