using Nancy;

namespace IridiumIon.Analytics.Modules
{
    public abstract class IIABaseModule : NancyModule
    {
        internal IIABaseModule(string path) : base($"/iia/{path}")
        {
        }
    }
}