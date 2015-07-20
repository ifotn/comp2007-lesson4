using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Data.Entity;
using lesson4.Models;

using System.Net.Mail;

namespace lesson4
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpException objError = Server.GetLastError() as HttpException;

            if (objError.GetHttpCode() == 404)
            //if (objError is HttpException)
            {
               //HttpException objE =  HttpException(objError);

                //if (objError.Message == "The resource cannot be found.")
                //if (objError.HResult == -2147467259)
                //if (String.Compare(objError.Message, "file does not exist") > 0)
                //{
                    Server.Transfer("/404.aspx");
                    return;
                //}
            }

            //email the error
            MailMessage objMail = new MailMessage();

            objMail.Subject = "Contoso Error";
            objMail.Body = "Type: " + objError.GetType() + "<br />Source: " + objError.Source + "<br />Message: " + objError.Message + "<br />StackTrace: " + objError.StackTrace;
            objMail.From = new MailAddress("rfreeman@infrontofthenet.com");
            objMail.To.Add("rich.freeman20@yahoo.com");
            objMail.IsBodyHtml = true;

            SmtpClient objClient = new SmtpClient();
            objClient.Send(objMail);

            Server.ClearError();
            Response.Redirect("/error.aspx");
        }
    }
}