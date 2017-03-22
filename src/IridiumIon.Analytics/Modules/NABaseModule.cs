using Nancy;

namespace IridiumIon.Analytics.Modules
{
    public abstract class NABaseModule : NancyModule
    {
        internal NABaseModule(string path) : base($"/iia{path}")
        {
        }
    }
}