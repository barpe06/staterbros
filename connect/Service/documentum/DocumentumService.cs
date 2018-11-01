using connect.Models.documentum;
using connect.Service.documentum.api;
using connect.Service.documentum.utils;
using DocuSign.eSign.Client;
using Emc.Documentum.FS.DataModel.Core;
using Emc.Documentum.FS.Runtime.Context;
using Emc.Documentum.FS.Services.Core;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;


namespace connect.Service.documentum
{
    public static class DocumentumService
    {
        /* The username to login to the repository */
        private static String documentumUserName;

        /* The password for the username */
        private static String documentumPassword;

        /* The address where the DFS services are located */
        private static string documentumUrl;

        /* The module name for the DFS core services */
        private static String moduleName = "core";
        private static IServiceContext serviceContext;
        private static IObjectService objectService;
        private static IQueryService querySvc;
        private static readonly ILog Log = LogManager.GetLogger(typeof(DocumentumService));

        static DocumentumService()
        {
            documentumUrl = System.Configuration.ConfigurationManager.AppSettings["documentumFCPort"];
            documentumUserName = System.Configuration.ConfigurationManager.AppSettings["documentumUserName"];
            documentumPassword = System.Configuration.ConfigurationManager.AppSettings["documentumPassword"];
        }

        public static ContentPropertyResponse CreateContentlessDocumentLinkToFolder(string repo, string folderId)
        {
            DocumentumServiceUtils.ConfigureApiClient(repo);
            ContentProperty contentProperty = new ContentProperty();
            contentProperty.properties = new PropertiesType();
            contentProperty.properties.r_object_type = "dwr_gen_doc";
            contentProperty.properties.object_name = "readme6";
            contentProperty.properties.author_creator = "Pedro Barroso";
            contentProperty.properties.author_date = "2017-09-26"; 
            contentProperty.properties.topic_subject = "The subject";

            IDocumentumApi documentumApi = new DocumentumApi();
            ContentPropertyResponse response = null;
            try
            {
                response = documentumApi.CreateContentlessDocument(repo, folderId, contentProperty);
            } 
            catch (ApiException ex)
            {
                Log.Error(ex);
            }
            return response;

        }
        
         public static DataObject uploadDocument(IDictionary<string,string> ecf, byte[] documentBytes){
             var documentumApi = new DocumentumApi();
             try
             {
                 documentumApi.UploadDocsWithProperties(ecf, documentBytes);
             }
             catch (ApiException ex)
             {
                 Log.Error(ex.ErrorContent);
                 throw ex;
             }
            
             return null;
         }
    }
}