using OsmiumSubstrate.Configuration.Access;

namespace IridiumIon.Analytics.Configuration.Access
{
    /// <summary>
    /// An enumeration of access scope identifiers, used for granular permission grants on key access
    /// </summary>
    public enum NAApiAccessScope
    {
        None = 1 << 0,
        Query = 1 << 1,
        Full = 1 << 3,
        Admin = 1 << 31,
    }

    public class NAAccessKey : AccessKey<NAApiAccessScope>
    {
        public override NAApiAccessScope[] AccessScopes { get; set; } = new[] { NAApiAccessScope.Query };
    }
}