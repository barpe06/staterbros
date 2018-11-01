using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocuSign.Connect;
using connect.Service.docusign;
using DocuSign.eSign.Model;
using connect.Service.docusign.utils;
using System.ComponentModel;
using System.IO;
using connect.Models.documentum;
using connect.Service.documentum;
using log4net;
using connect.Service.documentum.utils;

namespace connect.Service.hangfire.tasks
{
    public static class DocumentumUploadTask
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DocumentumUploadTask));

        //[DisplayName("Send order #{envelopeInfo.EnvelopeStatus.EnvelopeID} to warehouse")]
        /*public static void uploadDocuments(DocuSignEnvelopeInformation envelopeInfo)
        {
            Predicate<CustomField> accountFinder = (CustomField p) => { return p.Name == "AccountId"; };
            DocuSignService.GetDocument(ServiceUtil.buildEnvironment(envelopeInfo),
                envelopeInfo.EnvelopeStatus.EnvelopeID,
                envelopeInfo.EnvelopeStatus.DocumentStatuses.DocumentStatus.ID,
                DocumentOptions.Individual);
        }*/
        [DisplayName("Uploading document {3} for envelope {2}")]
        public static void uploadDocument(IDictionary<string, string> localParams,
            string envelopeId, string documentId, DocumentOptions options)
        {
            //Now retrieve the documents for the given envelope from the accountId hosted in environment as combined
            MemoryStream docStream = DocuSignService.GetDocument(localParams[EnvelopeMetaFields.Environment], 
                localParams[EnvelopeMetaFields.AccountId],
                envelopeId,
                documentId,
                options);


            // Now upload the bytes for teh document that we just retrieved in Documentum
            byte[] buffer = ServiceUtil.ReadFully(docStream);
            var response = DocumentumService.uploadDocument(localParams, buffer);
            Log.Debug("Properties created: ");
        }
    }
}   