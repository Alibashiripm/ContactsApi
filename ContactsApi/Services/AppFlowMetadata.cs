using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.PeopleService.v1;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace  ContactsApi.Services
{
    public class AppFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets
                {
                    ClientId = "",
                    ClientSecret = ""
                },
                Scopes = new[] {PeopleServiceService.Scope.Contacts,
                PeopleServiceService.Scope.ContactsOtherReadonly}
            });

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow;}
        }

        public override string GetUserId(Controller controller)
        {
            return controller.User.Identity.GetUserId();
        }
    }
}