using DocuSign.Connect;
using DocuSign.eSign.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using connect.Service.util;

namespace connect.Service.documentum.utils
{
     
    public class DocumentumServiceUtils
    {
        public static void ConfigureApiClient(string repo)
        {
            ApiClient apiClient = new ApiClient(ConfigurationManager.AppSettings["documentumUrl"].Replace("{REPO}", repo));
            string authHeader = " Basic " + CreateBasicBearToken(ConfigurationManager.AppSettings["dsUserName"],
                ConfigurationManager.AppSettings["dsUserPassword"]);
            // set client in global config so we don't need to pass it to each API object.
            DocuSign.eSign.Client.Configuration.Default.ApiClient = apiClient;

            if (DocuSign.eSign.Client.Configuration.Default.DefaultHeader.ContainsKey("X-DocuSign-Authentication"))
            {
                DocuSign.eSign.Client.Configuration.Default.DefaultHeader.Remove("X-DocuSign-Authentication");
            }
            if (DocuSign.eSign.Client.Configuration.Default.DefaultHeader.ContainsKey("Authorization"))
            {
                DocuSign.eSign.Client.Configuration.Default.DefaultHeader.Remove("Authorization");
            }
            DocuSign.eSign.Client.Configuration.Default.AddDefaultHeader("Authorization", authHeader);
            DocuSign.eSign.Client.Configuration.Default.AddDefaultHeader("Content-Type", "application/vnd.emc.documentum+json");
        }


        public static Dictionary<string, string> getDocumentProperties(DocuSignEnvelopeInformation envelopeInfo)
        {
            //L
            Dictionary<string, string> ecf = new Dictionary<string, string>();
            Predicate<CustomField> finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.AccountId; };
            CustomField customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string accountId = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.AccountId, accountId);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.Environment; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string environment = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.Environment, environment);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.BusinessRecord; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string businessRecord = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.BusinessRecord, businessRecord);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.DocumentType; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string documentType = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.DocumentType, documentType);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.FolderId; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string folderId = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.FolderId, folderId);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.FormDescription; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string formDescription = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.FormDescription, formDescription);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.FormID; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string formId = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.FormID, formId);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.PolicyNumber; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string policyNumber = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.PolicyNumber, policyNumber);

            finder = (CustomField p) => { return p.Name == EnvelopeMetaFields.Repository; };
            customField = envelopeInfo.EnvelopeStatus.CustomFields.CustomField.Find(finder);
            string repository = customField == null ? null : customField.Value;
            ecf.Add(EnvelopeMetaFields.Repository, repository);

            ecf.Add(EnvelopeMetaFields.Subject, envelopeInfo.EnvelopeStatus.EnvelopeID);
            //Author-Creator will have the name of each signers concatenated
            envelopeInfo.EnvelopeStatus.RecipientStatuses.RecipientStatus.ForEach((recipient)=> {
                if (ecf.ContainsKey(EnvelopeMetaFields.AuthorCreator)){
                    ecf[EnvelopeMetaFields.AuthorCreator] = ecf[EnvelopeMetaFields.AuthorCreator] + "," + recipient.UserName;
                } else {
                    ecf[EnvelopeMetaFields.AuthorCreator] = recipient.UserName;
                }
            });
            //truncate the author to 255 characters
            ecf[EnvelopeMetaFields.AuthorCreator] = StringExt.Truncate(ecf[EnvelopeMetaFields.AuthorCreator], 255);
            //Author-date should contain the signed date for this envelope
            ecf.Add(EnvelopeMetaFields.AuthoredDate, envelopeInfo.EnvelopeStatus.Completed);
            return ecf;
        }

        public static string CreateBasicBearToken(string userName = null, string password = null)
        {
            string documentumUserName = null;
            string documentumPassword = null;
            if (userName == null) userName = ConfigurationManager.AppSettings["documentumUserName"];
            if (password == null) password = ConfigurationManager.AppSettings["documentumPassword"];
            string baseOauth = userName + ":" + password;
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(baseOauth));
        }
    }
}