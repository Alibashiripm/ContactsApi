using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AuthorizeGoogleContacts(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata())
                .AuthorizeAsync(cancellationToken);
            return new RedirectResult(result.RedirectUri);
        }
        public async Task<ActionResult> GetGoogleContacts(CancellationToken cancellationToken)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.json");
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonContent);
            var flow = new AppFlowMetadata();
            UserCredential googlecredential = new UserCredential(flow.Flow, null,
                new Google.Apis.Auth.OAuth2.Responses.TokenResponse()
                {
                    AccessToken = jsonObject.Token.access_token,
                    TokenType = jsonObject.Token.token_type,
                    ExpiresInSeconds = jsonObject.Token.expires_in,
                    RefreshToken = jsonObject.Token.refresh_token,
                    Issued = jsonObject.Token.Issued,
                    IssuedUtc = jsonObject.Token.IssuedUtc,
                    Scope = "https://www.googleapis.com/auth/contacts https://www.googleapis.com/auth/contacts.other.readonly",
                    IdToken = null
                }
                );
            bool refreshed = await googlecredential.RefreshTokenAsync(cancellationToken);
            string json = JsonConvert.SerializeObject(googlecredential);
            System.IO.File.WriteAllText(jsonFilePath, json);
            GooglePeopleApiHelper.Credential = googlecredential;
            return Redirect("/Contacts/AddNewContact");

        }
    }
}