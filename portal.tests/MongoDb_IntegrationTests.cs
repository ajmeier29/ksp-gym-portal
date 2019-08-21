using Microsoft.Extensions.Configuration;

namespace portal.tests
{
    public class MongoDb_IntegrationTests
    {
        private string[] _args;
        private IConfiguration _configuration { get; set; }
        public MongoDb_IntegrationTests(string[] args)
        {
            _args = args;
            BuildConfig();
        }

        private void BuildConfig(){
            _configuration = new ConfigurationBuilder() 
                .AddCommandLine(_args)
                .Build();
        }

        public bool RunTests(){
            return ConnectionTestPass() &&
                   ConnectionTestFail();
        }

        private bool ConnectionTestPass()
        {
            string ddUsername = _configuration.GetValue<string>("dbsettings:dbUsername");
            string password = _configuration.GetValue<string>("dbsettings:password");
            string hostname = _configuration.GetValue<string>("dbsettings:hostname");
            string connectionString = _configuration.GetValue<string>("dbsettings:connectionstring");
            return ddUsername != null && password != null && hostname != null && connectionString != null;
        }

        private bool ConnectionTestFail()
        {
            string ddUsername = _configuration.GetValue<string>("dbsettings:dbUsernamexx");
            string password = _configuration.GetValue<string>("dbsettings:passwordxx");
            string hostname = _configuration.GetValue<string>("dbsettings:hostnamexx");
            string connectionString = _configuration.GetValue<string>("dbsettings:connectionstringxx");
            return ddUsername == null && password == null && hostname == null && connectionString == null;
        }
        
    }
}