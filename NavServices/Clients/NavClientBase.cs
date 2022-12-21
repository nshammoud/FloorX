using KQF.Floor.NavServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.ServiceModel;

namespace KQF.Floor.Web.NavServices.Clients
{
    public abstract partial class NavClientBase<T, ClientType> : ClientBase<T>
        where T : class
        where ClientType : class
    {
        protected readonly ILogger<T> _logger;

        public NavClientBase(IOptions<NavServiceConfig> config, string enpointName, ClaimsPrincipal user, ILogger<T> logger) :
            base(
                GetBindingForEndpoint(config.Value),
                GetEndpointAddress(config.Value, typeof(ClientType).Name))
        {
            _logger = logger;
            this.Endpoint.Name = enpointName;

            var credentials = user.GetCredentials();
            if (credentials == null)
            {
                logger.LogInformation("user has no credentials");
            }
            else
            {

                System.Net.NetworkCredential userCreds = new System.Net.NetworkCredential(credentials.Username, credentials.Password, credentials.Domain.Replace("@", ""));
                _logger.LogInformation("NavClient Created using {0}", userCreds.UserName);

                ClientCredentials.Windows.ClientCredential = userCreds;
                ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

                ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
            }
        }


        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }

        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);


        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(NavServiceConfig config)
        {

            System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;

            result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;

            result.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Ntlm;
            //result.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.Windows;
            result.Security.Message.ClientCredentialType = System.ServiceModel.BasicHttpMessageCredentialType.UserName;


            return result;
        }

        private static System.ServiceModel.EndpointAddress GetEndpointAddress(NavServiceConfig config, string typeName)
        {
            var address = config.Endpoints.ContainsKey(typeName) ?
                config.Endpoints[typeName] :
                string.Empty;

            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(config.Host))
            {
                return new System.ServiceModel.EndpointAddress($"{ config.Host }{ address}");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", typeName));
        }
    }
}
