using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PhishGuard.Services
{
    public class LinkCheckerService
    {
        public async Task<string> CheckLinkAsync(string url)
        {
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "http://" + url;
            }

            try
            {
                var uri = new Uri(url);

                string[] suspiciousKeywords = { "login", "secure", "verify", "update", "account", "paypal", "bank", "apple", "microsoft", "free", "gift", "auth", "signin", "support", "billing", "customer" };
                
                string host = uri.Host.ToLower();
                string path = uri.AbsolutePath.ToLower();

                if (uri.HostNameType == UriHostNameType.IPv4 || uri.HostNameType == UriHostNameType.IPv6)
                {
                    return "Phishing";
                }

                foreach (var keyword in suspiciousKeywords)
                {
                    if (host.Contains(keyword) && !host.EndsWith($"{keyword}.com") && !host.EndsWith($"{keyword}.org"))
                    {
                         return "Phishing";
                    }
                    if (path.Contains(keyword))
                    {
                         return "Phishing";
                    }
                }

                if (host.Length > 40)
                {
                    return "Phishing";
                }

                if (host.Split('.').Length > 4)
                {
                    return "Phishing";
                }

                if (path.Split('/').Length > 5)
                {
                    return "Phishing";
                }

                if (host.Contains("-") && host.Split('.').Length > 2)
                {
                    if (host.Contains("login") || host.Contains("verify") || host.Contains("secure"))
                        return "Phishing";
                }

                return "Safe";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
