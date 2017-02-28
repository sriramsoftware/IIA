using Iridium.PluginEngine.Types;

namespace KQAnalytics3.PluginCore
{
    public abstract class KQComponentPlugin<T> : ComponentPlugin<T>
    {
        /// <summary>
        /// Provides access to the KQAnalytics3 instance's private services
        /// </summary>
        public virtual IKQServiceProvider KQServiceProvider { get; } = new KQServiceProvider();
    }
}