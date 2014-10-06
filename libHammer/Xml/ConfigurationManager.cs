using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace libHammer.Xml
{

    /// <summary>
    /// Manages configuration settings specified both on application config file or on external sources.
    /// </summary>
    /// <typeparam name="T">Type of configuration being managed.</typeparam>
    public static class ConfigurationManager<T> where T : class
    {
        private static T _preLoadedConfig;

        /// <summary>
        /// Saves the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void Save(T configuration)
        {
            lock (typeof(ConfigurationManager<T>))
            {
                IEnumerator<string> configEnum = GetProbableConfigFilePaths().GetEnumerator();
                if (configEnum.MoveNext())
                    using (XmlTextWriter writer = new XmlTextWriter(configEnum.Current, Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        XmlSerializer xs = new XmlSerializer(typeof(T));
                        xs.Serialize(writer, configuration);
                    }
                _preLoadedConfig = configuration;
            }
        }

        /// <summary>
        /// Gets the configuration settings of type <typeparamref name="T"/>
        /// </summary>
        /// <returns>Configuration settings for <typeparamref name="T"/></returns>
        public static T GetConfiguration()
        {
            if (_preLoadedConfig == null)
                lock (typeof(ConfigurationManager<T>))
                    if (_preLoadedConfig == null
                            && null == (_preLoadedConfig = LoadFromConfigSection() ?? LoadFromExternalSource()))
                    {
                        throw new System.Configuration.ConfigurationErrorsException(
                                    string.Format("No configuration data was found for type \"{0}\"", typeof(T).Name));
                    }
            return _preLoadedConfig;
        }

        private static T LoadFromExternalSource()
        {
            foreach (var configPath in GetProbableConfigFilePaths())
                if (File.Exists(configPath))
                    using (FileStream stream = File.OpenRead(configPath))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(T));
                        return xs.Deserialize(stream) as T;
                    }

            return null;
        }

        private static T LoadFromConfigSection()
        {
            return System.Configuration.ConfigurationManager.GetSection(typeof(T).Name) as T;
        }

        /// <summary>
        /// Gets the probable config file paths.
        /// </summary>
        /// <returns>Enumerable collection of file paths on which the configuration may exist.</returns>
        public static IEnumerable<string> GetProbableConfigFilePaths()
        {
            Assembly typeContainer = typeof(T).Assembly;
            string fileName = string.Format("{0}.{1}.config",
                                        Path.GetFileNameWithoutExtension(typeContainer.Location),
                                        typeof(T).Name);
            string fullName = string.Format("{0}\\{1}\\{2}",
                                        RuntimeEnviornment.CompanyName,
                                        RuntimeEnviornment.ProductTitle,
                                        fileName);

            yield return Path.Combine(Path.GetDirectoryName(typeContainer.Location), fileName);
            yield return Path.Combine(Environment.CurrentDirectory, fileName);
            yield return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fullName);
            yield return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fullName);
        }
    }

}
