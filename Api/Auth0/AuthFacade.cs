using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ganymede.Api.Auth0
{
    public class AuthResult
    {
        public string message { get; }
        public string access_token { get; } = null;

        public AuthResult(string message, string access_token = null)
        {
            this.message = message;
            this.access_token = access_token;
        }
    }

    public class AuthFacade
    {
        private const string HOST = "dev-ganymede.us.auth0.com";
        private const string CLIENT_ID = "JVIhMKkuA9yatlojsqbZYScRpq2c09RN";

        private static readonly HttpClient client = new HttpClient();

        public async Task<AuthResult> LoginWithPassword(string username, string password)
        {
            var payload = new Dictionary<string, string> {
                { "grant_type", "password" },
                { "client_id", CLIENT_ID },
                { "username", username },
                { "password", password },
                { "audience", "ganymede-api" }
            };

            var data = new FormUrlEncodedContent(payload);
            var response = await client.PostAsync(String.Format("https://{0}/oauth/token", HOST), data);

            try {
                var json = (await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync())).RootElement;

                if (response.IsSuccessStatusCode) {
                    var token = json.GetProperty("access_token").GetString();
                    return new AuthResult("Success", token);
                } else {
                    var description = json.GetProperty("error_description").GetString();
                    return new AuthResult(description);
                }
            } catch (Exception) {
                return new AuthResult("Unknown error");
            }
        }
    }

}