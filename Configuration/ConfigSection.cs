using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Configuration
{
    /// <summary>Represents a section in a configuration file (e.g. app.config, web.config, machine.config).</summary>
    public sealed class ConfigSection : ISettings
    {
        private readonly NameValueCollection settingCollection;
        private readonly string settingNamePrefix;
        private readonly string splitter;

        public ConfigSection(string sectionName, string settingNamePrefix, string splitter)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }

            if (settingNamePrefix == null)
            {
                throw new ArgumentNullException("settingNamePrefix");
            }

            if (splitter == null)
            {
                throw new ArgumentNullException("splitter");
            }

            this.settingCollection = (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            this.settingNamePrefix = settingNamePrefix;
            this.splitter = splitter;
        }

        public ConfigSection(string sectionName, string settingNamePrefix)
            : this(sectionName, settingNamePrefix, ".")
        {
        }

        public ConfigSection(string sectionName)
            : this(sectionName, string.Empty)
        {
        }

        public string this[string name]
        {
            get
            {
                if (!string.IsNullOrEmpty(this.settingNamePrefix))
                {
                    name = string.Format("{0}{1}{2}", this.settingNamePrefix, this.splitter, name);
                }

                return this.settingCollection[name];
            }
        }
    }
}