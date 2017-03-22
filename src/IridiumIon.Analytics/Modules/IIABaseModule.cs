using Nancy;

namespace IridiumIon.Analytics.Modules
{
    public class IIABaseModule : NancyModule
    {
        public IIABaseModule(string path) : base($"/iia/{path}")
        {
        }

        public IIABaseModule() : base(KQRegistry.KQBasePath)
        {
        }
    }
}