using ksp_portal.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace ksp_portal.Services
{
    // This class will return the correct connection string 
    // regardless of if user-secrets are used, or environement variables
    // Enviornment setup:
    //      local: user-secrets
    //      production/CI/CD: Enviornment variables
    public class ConfigurationService
    {
        private IWorkoutsDatabaseSettings _settings;
        private IConfiguration _configuration;
        public ConfigurationService(IWorkoutsDatabaseSettings settings, IConfiguration configuration)
        {
            _settings = settings;
            _configuration = configuration;
        }

        public string ConnectionStringBuilder()
        {
            _settings.ConnectionString = _settings.ConnectionString.Replace($"[[{_settings.DbUserEnvVarName}]]", _configuration[_settings.DbUserEnvVarName]);
            _settings.ConnectionString = _settings.ConnectionString.Replace($"[[{_settings.DbUserPassEnvVarName}]]", _configuration[_settings.DbUserPassEnvVarName]);
            _settings.ConnectionString = _settings.ConnectionString.Replace($"[[{_settings.DbHostNameEnvVarName}]]", _configuration[_settings.DbHostNameEnvVarName]);
            return _settings.ConnectionString;
        }
    }
}