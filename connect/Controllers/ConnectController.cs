using connect.Models.docusign.connect;
using connect.Service.docusign;
using connect.Service.docusign.utils;
using connect.Service.hangfire.tasks;
using DocuSign.Connect;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using connect.Service.documentum.utils;

namespace connect.Controllers
{
    public class ConnectController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConnectController));

        // GET api/connect
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/connect/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/connect
        //[ResponseType(typeof{string})]
        public HttpResponseMessage Post([FromBody]DocuSignEnvelopeInformation dsEnvInfo)
        {
            var dsEnv = ServiceUtil.buildEnvironment(dsEnvInfo);
            Log.Info("Processing envelopeId " + dsEnvInfo.EnvelopeStatus.EnvelopeID);
            Log.Info("Creating HangFire backgoundJob ");
            IDictionary<string, string> localECF = DocumentumServiceUtils.getDocumentProperties(dsEnvInfo);
            var jobId = Hangfire.BackgroundJob.Enqueue(() => DocumentumUploadTask.uploadDocument(localECF,
                dsEnvInfo.EnvelopeStatus.EnvelopeID,
                "combined",
                DocumentOptions.Combined_No_Cert));
            Log.Info("Hangfire JobId created: " +  jobId);

            ConnectProcessResponse response = new ConnectProcessResponse(dsEnvInfo.EnvelopeStatus.EnvelopeID, jobId);
            Log.Info("Reply HTTP 200 to DocuSign JobId created: " + jobId);

            return this.Request.CreateResponse<string>(HttpStatusCode.OK, dsEnvInfo.EnvelopeStatus.EnvelopeID + " : " + jobId);
        }

        // PUT api/connect/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/connect/5
        public void Delete(int id)
        {
        }
    }
}
