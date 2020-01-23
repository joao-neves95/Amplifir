using Ganss.XSS;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.DomainServices
{
    public class SanitizerService : ISanitizerService
    {
        public string SanitizeHTML(string html)
        {
            HtmlSanitizer htmlSanitizer = new HtmlSanitizer();
            return htmlSanitizer.Sanitize( html );
        }
    }
}
