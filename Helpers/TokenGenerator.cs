using System;

namespace TmxTaskUpdater.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateToken()
        {
            var username = "{your-azure-devops-mail}";
            var password = "{your-PAT-token}";

            return $"Basic {Base64Encode($"{username}:{password}")}";
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
