using connect.Models.documentum;
using connect.Service.documentum.utils;
using DocuSign.eSign.Client;
using log4net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace connect.Service.documentum.api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    
    public partial class DocumentumApi : IDocumentumApi
    {
        private DocuSign.eSign.Client.ExceptionFactory _exceptionFactory = (name, response) => null;
        private static readonly ILog Log = LogManager.GetLogger(typeof(DocumentumApi));

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentumApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DocumentumApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            ExceptionFactory = DocuSign.eSign.Client.Configuration.DefaultExceptionFactory;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentumApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public DocumentumApi(Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = DocuSign.eSign.Client.Configuration.DefaultExceptionFactory;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        public Configuration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public DocuSign.eSign.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        public ApiResponse<ContentPropertyResponse> CreateContentlessDocumentWithHttpInfo(string repo, string folderId, ContentProperty contentProperty = null)
        {
            // verify the required parameter 'accountId' is set
            if (repo == null)
                throw new ApiException(400, "Missing required envelope custom field parameter 'documentum repository' when calling DocumentumService->uploadDocument");
            // verify the required parameter 'documentId' is set
            if (folderId == null)
                throw new ApiException(400, "Missing required envelope custom field parameter 'folder' when calling DocumentumService->uploadDocument");

            var localVarPath = "/folders/{folderId}/documents".Replace("{folderId}",folderId);
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.Default.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                 "application/vnd.emc.documentum+json"
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);
           // localVarHttpContentType = "application/vnd.emc.documentum+json";

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/vnd.emc.documentum+json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                if (localVarHttpContentType.Contains("Accept"))
                {
                    localVarHeaderParams.Remove("Accept");
                }
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (localVarHttpContentType != null)
                /*if (localVarHttpContentType.Contains("Content-Type"))
                {
                    localVarHeaderParams.Remove("Content-Type");
                }
                localVarHeaderParams.Add("Content-Type", localVarHttpContentType);*/
            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            //localVarPathParams.Add("format", "json");

            if (contentProperty != null && contentProperty.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(contentProperty); // http body (model) parameter
            }
            else
            {
                localVarPostBody = contentProperty; // byte array
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("CreateContentless", localVarResponse);
                if (exception != null) throw exception;
            }
               

            // DocuSign: Handle for PDF return types
            if (localVarResponse.ContentType != null && !localVarResponse.ContentType.ToLower().Contains("json"))
            {
                return new ApiResponse<ContentPropertyResponse>(localVarStatusCode, localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()), (ContentPropertyResponse)Configuration.ApiClient.Deserialize(localVarResponse.RawBytes, typeof(ContentPropertyResponse)));
            }
            else
            {
                return new ApiResponse<ContentPropertyResponse>(localVarStatusCode, localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()), (ContentPropertyResponse)Configuration.ApiClient.Deserialize(localVarResponse, typeof(ContentPropertyResponse)));
            }



        }

        /// <returns>ContentPropertyResponse</returns>
        public ContentPropertyResponse  CreateContentlessDocument(string repo, string folderId, ContentProperty contentProperty){
            ApiResponse<ContentPropertyResponse> localVarResponse = CreateContentlessDocumentWithHttpInfo(repo, folderId, contentProperty);
            return localVarResponse.Data;
        }
        
        public RestResponse<bool> PutEnvelopesDocuments(ContentPropertyResponse  contentResponse, string documentId, string fullFileName, byte[] documentBytes)
        {
            IList<Link> links = contentResponse.links;
            IEnumerable<Link> query = links.Where(link => link.rel.CompareTo("contents") == 0);
            Link contentsList = query.First();
            var restClient = new RestClient(contentsList.href);
            RestRequest request = new RestRequest(Method.PUT);

            request.AddHeader("Accept", "application/pdf");
            request.AddParameter("application/pdf", documentBytes, ParameterType.RequestBody);

            var response = restClient.Execute(request);

            //return RestResponse<bool>.Create(response, restClientBuildUri(response.Request));
            return null;
        }

        public void UploadDocsWithProperties2(string repo, string folderId, byte[] documentBytes, string fileName, ContentProperty contentProperty = null)
        {
            var request = new RestRequest("/" + repo + "/folders/" + folderId + "/documents", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(contentProperty);
            request.AddFileBytes("content", documentBytes, fileName, "application/pdf");
            /* RestResponse<bool>
             * request.AddParameter("object[name]", "object");
            request.AddParameter("object[tag_number]", tagnr);
            request.AddParameter("machine[serial_number]", machinenr);
            request.AddParameter("machine[safe_token]", safe_token);
            request.AddFile("receipt[receipt_file]", File.ReadAllBytes(path), "Invoice.pdf", "application/octet-stream");
            */
            request.AddHeader("Authorization", DocumentumServiceUtils.CreateBasicBearToken());
            RestClient restClient = new RestClient(
                System.Configuration.ConfigurationManager.AppSettings["documentumUrl"].Replace("{REPO}", repo));
            var response = restClient.Execute(request);
            Log.Info("Called documentum with response: " + response);
        }

        public void UploadDocsWithProperties(IDictionary<string,string> ecf, byte[] documentBytes)
        {
            // let's check that all my envelope custom fields are not null
            foreach(KeyValuePair<string, string> entry in ecf)
            {
                if (entry.Value == null)
                {
                    throw new ApiException(400, "Missing required envelope custom field parameter " +  entry.Key + " when calling DocumentumService->uploadDocument");
                }
            }

            var documentumApi = new DocumentumApi();
            ContentProperty contentProperty = new ContentProperty();
            contentProperty.properties = new PropertiesType();
            contentProperty.properties.r_object_type = ecf[EnvelopeMetaFields.DocumentType];
            contentProperty.properties.object_name = ecf[EnvelopeMetaFields.PolicyNumber] + "-" + ecf[EnvelopeMetaFields.FormID];
            contentProperty.properties.author_creator = ecf[EnvelopeMetaFields.AuthorCreator];
            contentProperty.properties.author_date = ecf[EnvelopeMetaFields.AuthoredDate];
            contentProperty.properties.subject = ecf[EnvelopeMetaFields.Subject];
            contentProperty.properties.topic_subject = ecf[EnvelopeMetaFields.FormID] + "-" + ecf[EnvelopeMetaFields.FormDescription];
            contentProperty.properties.business_record = ecf[EnvelopeMetaFields.BusinessRecord];

            string requestHost = System.Configuration.ConfigurationManager.AppSettings["documentumUrl"].Replace("{REPO}", ecf[EnvelopeMetaFields.Repository]);
            var localVarPath = "/folders/{folderId}/documents?overwrite=true".Replace("{folderId}", ecf[EnvelopeMetaFields.FolderId]);

            // Create a http request to the server endpoint that will pick up the
            // file and file description.
            HttpWebRequest requestToServerEndpoint =
                (HttpWebRequest)WebRequest.Create(requestHost + localVarPath);
            string boundaryString = "FFF3F395A90B452BB8BEDC878DDBD152";
            
            // Set the http request header 
            requestToServerEndpoint.Method = WebRequestMethods.Http.Post;
            requestToServerEndpoint.ContentType = "multipart/form-data; boundary=" + boundaryString;
            requestToServerEndpoint.KeepAlive = true;
            requestToServerEndpoint.Headers.Add("Authorization", "Basic " + DocumentumServiceUtils.CreateBasicBearToken()); 
            requestToServerEndpoint.Accept = "application/vnd.emc.documentum+json";

            // Use a MemoryStream to form the post data request,
            // so that we can get the content-length attribute.
            MemoryStream postDataStream = new MemoryStream();
            StreamWriter postDataWriter = new StreamWriter(postDataStream);

            // Include value from the tag_number text area in the post data
            postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
            postDataWriter.Write("Content-Disposition: form-data; name=\"object\"\r\n");
            postDataWriter.Write("Content-Type: application/vnd.emc.documentum+json;charset=UTF-8\r\n\r\n");
            postDataWriter.Write(JsonConvert.SerializeObject(contentProperty));

            // Include the file in the post data
            postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
            postDataWriter.Write("Content-Disposition: form-data; name=\"content\"\r\n");
            postDataWriter.Write("Content-Type: application/pdf\r\n\r\n");
            postDataWriter.Flush();

            postDataStream.Write(documentBytes, 0, documentBytes.Length);
            postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
            postDataWriter.Flush();

            // Set the http request body content length
            requestToServerEndpoint.ContentLength = postDataStream.Length;

            // Dump the post data from the memory stream to the request stream
            Stream s = requestToServerEndpoint.GetRequestStream();

            postDataStream.WriteTo(s);

            postDataStream.Close();
            s.Close();
            try
            {
                var response = requestToServerEndpoint.GetResponse();
                Log.Info("Called documentum with response: " + response);

            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    throw new ApiException((int)GetHttpStatusCode(ex), "Error calling DocumentumApi=>UploadDocsWithProperties ", reader.ReadToEnd());
                }
                    
            }
            
        }

        HttpStatusCode GetHttpStatusCode(WebException we)
        {
            if (we.Response is HttpWebResponse)
            {
                HttpWebResponse response = (HttpWebResponse)we.Response;
                return response.StatusCode;
            }
            return 0;
        }

    }
}