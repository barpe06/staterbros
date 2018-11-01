using connect.CMService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace connect.Service.ibm.cm
{
    public class CMWebServiceClient
    {
        //<summary> Sets up an array of MTOMAttachments. 
        //The MTOMAttachments are used to send the content
        // of the documents parts to the server in the soap message
        //</summary>
        //<param name="resources"> array containing information 
        //about the document part to be associated 
        //with the policy </param> 
        //<returns> new array of MTOMAttachments</returns>

        MTOMAttachment[] setupAttachments(string[,] resources)
        {   //Create MTOMAttachment objects for all the resources 
            //passed in the resources array provides
            // the information for the document parts to be 
            //sent to the server
            MTOMAttachment[] attachments = new
            MTOMAttachment[resources.GetLength(0)];
            for (int i = 0; i < resources.GetLength(0); i++)
            {  // resources[i,0] is the ID of the attachment. 
                //This should match the label of the document part
                // on the server side, the ID of the attachment and the 
                //label of the document part are used to  
                // match the attachment with its associated document part 
                // resources[i,1] is the mime type of the document part
                // resources[i,2] is the file path to the document part

                attachments[i] = new MTOMAttachment();
                attachments[i].ID = resources[i, 0];
                attachments[i].MimeType = resources[i, 1];
                attachments[i].Value = File.ReadAllBytes(resources[i, 2]);
            }
            return attachments;
        }


    }
}