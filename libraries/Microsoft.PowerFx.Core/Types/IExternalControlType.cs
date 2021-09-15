using Microsoft.AppMagic.Authoring;
using Microsoft.PowerFx.Core.App;

namespace Microsoft.PowerFx.Core.Types
{
    internal interface IExternalControlType
    {
        IExternalControlTemplate ControlTemplate { get; }
        bool IsDataLimitedControl { get; }
        bool IsMetaField { get; }
    }
}