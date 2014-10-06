using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Design_Patterns
{

    /// <summary>
    /// Holds key-value combinations of types to be built via <see cref="Factory<`>"/> pattern.
    /// </summary>
    public class FactorySettings : Codoxide.Common.Configuration.ConfigurationSectionBase
    {
        private readonly Dictionary<Type, MethodBase> _builderRegistry = new Dictionary<Type, MethodBase>();
        private readonly Regex _whitespaceRegEx = new Regex(@"\s", RegexOptions.Compiled);

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public Map<string> Settings { get; set; }

        /// <summary>
        /// Gets the builder.
        /// </summary>
        /// <param name="requestType">Type of the request.</param>
        /// <returns>Method capable of building an object of the requested type.</returns>
        public MethodBase GetBuilder(Type requestType)
        {
            // Do we have a ready made builder?
            if (_builderRegistry.ContainsKey(requestType))
                return _builderRegistry[requestType];
            else
            {
                MethodBase retVal = LoadBuilderMethod(requestType);
                _builderRegistry[requestType] = retVal;
                return retVal;
            }
        }

        private MethodBase LoadBuilderMethod(Type requestType)
        {
            lock (_builderRegistry)
            {
                // Redundant check for thread-safety
                if (_builderRegistry.ContainsKey(requestType))
                    return _builderRegistry[requestType];

                bool buildDirectly = !(requestType.IsAbstract || requestType.IsInterface);
                Type concreteType = requestType;

                if (Settings == null)
                    throw new PatternException("Factory configuration is invalid. Settings field is set to null.");
                else if (!buildDirectly && !Settings.ContainsKey(requestType.FullName))
                    throw new PatternException("Factory does not know how to build type: " + requestType.FullName);
                else if (!buildDirectly)
                    concreteType = GetConcreteType(requestType);

                object[] matches = concreteType.GetCustomAttributes(typeof(BuilderAttribute), true);
                MethodBase builderMethod = (matches.Length == 0)
                                            ? concreteType.GetConstructor(Type.EmptyTypes) as MethodBase
                                            : (matches[0] as BuilderAttribute).GetBuilderMethod(concreteType) as MethodBase;
                if (builderMethod == null && 0 == matches.Length)
                {
                    throw new PatternException(string.Format("Factory cannot build type: {0}. "
                                                                + "Type has no public parameterless constructor and "
                                                                + " does not use the BuilderAttribute.", concreteType.FullName));
                }
                else if (builderMethod == null)
                {
                    throw new PatternException(string.Format("Factory cannot build type: {0}. "
                                                                + "Method specified in BuilderAttribute is inaccessible.",
                                                                concreteType.FullName));
                }
                return builderMethod;
            }
        }

        private Type GetConcreteType(Type requestType)
        {
            string typeName = _whitespaceRegEx.Replace(Settings[requestType.FullName], "");
            try
            {
                return Type.GetType(typeName, true);
            }
            catch (Exception ex)
            {
                throw new PatternException("Factory cannot load the specified concrete type: " + typeName, ex);
            }
        }

        internal void Register(Type baseType, Type concreteType)
        {
            Settings[baseType.FullName] = concreteType.AssemblyQualifiedName;
        }
    }

}
