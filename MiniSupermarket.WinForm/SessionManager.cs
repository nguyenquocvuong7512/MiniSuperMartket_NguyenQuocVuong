using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public static class SessionManager
    {
        public static string JwtToken { get; set; } = string.Empty;

        public static string CurrentUsername { get; set; } = string.Empty;

        public static string CurrentRole { get; set; } = string.Empty;
    }
}

