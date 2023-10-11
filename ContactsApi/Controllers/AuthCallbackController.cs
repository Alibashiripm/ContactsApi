using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;
using ContactsApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace  ContactsApi.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData
        {
            get { return new AppFlowMetadata(); }
        }
        public override async Task<ActionResult> IndexAsync(AuthorizationCodeResponseUrl authorizationCode, CancellationToken taskCancellationToken)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.json");

            if (string.IsNullOrEmpty(authorizationCode.Code))
            {
                var errorRespons = new TokenErrorResponse(authorizationCode);
                return OnTokenError(errorRespons);
            }

            var returnUrl = Request.Url.ToString();
            returnUrl = returnUrl.Substring(0, returnUrl.IndexOf("?"));

            var token = await Flow.ExchangeCodeForTokenAsync(UserId, authorizationCode.Code, returnUrl,
                taskCancellationToken).ConfigureAwait(false);

            var credential = new UserCredential(Flow, UserId, token);
            string json = JsonConvert.SerializeObject(credential);
            System.IO.File.WriteAllText(jsonFilePath, json);

            try
            {
             
                return Redirect("/");
            }
            catch (Exception)
            {

                return View("Error");
            }
                
        }
    }
}