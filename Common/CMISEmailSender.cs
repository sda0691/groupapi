using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Collections;
using System.Net;
using API.Filters;
/// <summary>
/// Summary description for CMISEmailSender
/// </summary>
namespace API.Common
{
    [CustomExceptionFilterAttribute]
    public class CMISEmailSender
    {
        private String _Host;
        private String Subject = "";
        private String Body = "";
        private String FromName = "";
        private String FromAddress = "";
        private String To = "";

        public String Cc = "";
        public String Bcc = "";
        public String BodyFormat = "";

        private String errormsg = "";
        private ArrayList AttachFiles = new ArrayList();

        public String Host
        {
            set { _Host = value; }
            get
            {
                if (_Host == null || _Host.Trim() == "")
                    _Host = ConfigurationManager.AppSettings["mailSMTP"];

                return _Host;
            }
        }


        public CMISEmailSender()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void AddAttachment(String filepath)
        {
            if (AttachFiles == null)
                AttachFiles = new ArrayList();
            AttachFiles.Add(filepath);
        }
        public void ClearAttachments()
        {
            AttachFiles.Clear();
        }
        public CMISEmailSender(String mFromName, String mFromAddress, String mTo, String mSubject, String mBody)
        {
            SetEmail(mFromName, mFromAddress, mTo, mSubject, mBody, this.Cc, this.Bcc);
        }


        public void SetEmail(String mFromName, String mFromAddress, String mTo, String mSubject, String mBody)
        {
            SetEmail(mFromName, mFromAddress, mTo, mSubject, mBody, this.Cc, this.Bcc);
        }

        public void SetEmail(String mFromName, String mFromAddress, String mTo, String mSubject, String mBody, String mCc, String mBcc)
        {
            this.Subject = mSubject;
            this.Body = mBody;
            this.FromName = mFromName;
            this.FromAddress = mFromAddress; //
            this.To = mTo;
            this.Cc = mCc;
            this.Bcc = mBcc;
        }


        public bool Send()
        {
            errormsg = "";

            if (this.To.Length == 0 || this.Host.Length == 0)
            {
                errormsg = "No email address.";
                return false;
            }
            String from = (FromName.Length > 0 ? (FromName + "<" + FromAddress + ">") : FromAddress);

            MailMessage msg = new MailMessage();

            msg.IsBodyHtml = true;
            msg.From = new MailAddress(from);
            msg.Subject = Subject;
            msg.Body = Body;

            //Multiple receipients
            ArrayList recipient = new ArrayList();
            recipient.AddRange(To.Split(';'));
            foreach (string to in recipient)
            {
                msg.To.Add(to);
            }
            //////////////////////////////////////////////
            if (this.Cc != null && this.Cc.Length > 0)
                msg.CC.Add(new MailAddress(this.Cc));
            if (this.Bcc != null && this.Bcc.Length > 0)
                msg.Bcc.Add(new MailAddress(this.Bcc));
            msg.IsBodyHtml = (BodyFormat.ToUpper().Equals("HTML") ? true : false);


            //attachments
            for (int i = 0; AttachFiles.Count > 0 && i < AttachFiles.Count; i++)
            {
                Attachment attach = new Attachment(AttachFiles[i].ToString());
                msg.Attachments.Add(attach);
            }

            if (msg == null) return false;

            // create SMTP Client and add credentials 
            SmtpClient smtpClient = new SmtpClient(this.Host);

            string mailSMTPUID = (ConfigurationManager.AppSettings["mailSMTPUID"] == null) ? "" : ConfigurationManager.AppSettings["mailSMTPUID"];
            string mailSMTPPW = (ConfigurationManager.AppSettings["mailSMTPPW"] == null) ? "" : ConfigurationManager.AppSettings["mailSMTPPW"];

            if (mailSMTPUID == "" || mailSMTPPW == "")
                smtpClient.UseDefaultCredentials = false;
            else
                // Email with Authentication 
                smtpClient.Credentials = new NetworkCredential(mailSMTPUID, mailSMTPPW);

            //Send the message 
            bool result = false;
            try
            {
                smtpClient.Send(msg);
                result = true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                { errormsg = "InnerException: " + ex.InnerException.Message; }

                errormsg = errormsg + " " + ex.Message;
                //throw new ArgumentNullException(ex.Message);
            }
            //catch (System.Web.HttpException ehttp)
            //{
            //    errormsg = "Email Server: " + ehttp.Message;
            //}
            finally
            {
                //clear attachement after email is sent
                if (AttachFiles.Count > 0)
                {
                    ClearAttachments();
                    msg.Attachments.Dispose();
                }
            }
            return result;

        }

        public String GetMessage()
        {
            return this.errormsg;
        }

        public bool SendWithHttpFiles(ArrayList HttpFileList, String ServerSaveFolder)
        {
            //temporally saved file
            ArrayList attachs = new ArrayList();

            for (int i = 0; HttpFileList != null && i < HttpFileList.Count; i++)
            {
                HttpPostedFile pfile = (HttpPostedFile)(HttpFileList[i]);
                // Make sure file existed and the size > 0
                if (pfile != null && pfile.ContentLength > 0)
                {
                    String saveas = ServerSaveFolder + System.IO.Path.GetFileName(pfile.FileName);
                    pfile.SaveAs(saveas);

                    this.AddAttachment(saveas);
                    attachs.Add(saveas);
                }
            }

            //send email
            bool result = Send();


            for (int i = 0; attachs.Count > 0 && i < attachs.Count; i++)
                System.IO.File.Delete((String)attachs[i]);
            return result;
        }
    }

}
