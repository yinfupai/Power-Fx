using Microsoft.AppMagic.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AppMagic.Authoring
{

    [TransportType(TransportKind.ByValue)]
    public interface IErrorHelpLink
    {
        string DisplayText { get; }
        string Url { get; }
    }

    internal sealed class ErrorHelpLink : IErrorHelpLink
    {
        public string DisplayText { get; }
        public string Url { get; }

        public ErrorHelpLink(string displayText, string url)
        {
            Contracts.AssertNonEmpty(displayText);
            Contracts.AssertNonEmpty(url);

            DisplayText = displayText;
            Url = url;
        }
    }
}
