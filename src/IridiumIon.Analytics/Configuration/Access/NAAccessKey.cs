using OsmiumSubstrate.Configuration.Access;

namespace IridiumIon.Analytics.Configuration.Access
{
    /// <summary>
    /// An enumeration of access scope identifiers, used for granular permission grants on key access
    /// </summary>
    public enum NAApiAccessScope
    {
        None = 1 << 0,
        Read = 1 << 1,
        Query = Read,
        Admin = 1 << 31,
    }

    public class NAAccessKey : AccessKey<NAApiAccessScope>
    {
        public override NAApiAccessScope[] AccessScopes { get; set; } = new[] { NAApiAccessScope.Read };
    }
}