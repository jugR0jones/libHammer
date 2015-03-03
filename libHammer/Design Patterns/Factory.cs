using libHammer.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Design_Patterns
{

    /// <summary>
    /// Represents a Factory implementation based on Generics and Reflection.
    /// </summary>
    /// <remarks>
    /// Types to be built can use the <see cref="BuilderAttribute"/>, <see cref="ISupportLazyInitialization"/>, or simply contain a
    /// public parameterless constructor. Interfaces or Abstract classes maybe specified as <typeparamref name="T"/> as long as the
    /// FactorySettings configuration section contians the assembly qualified name of the concrete type.
    /// </remarks>
    public static class Factory
    {
        /// <summary>
        /// Name of the configuration section used to retrieve relevant <see cref="FactorySettings"/>.
        /// </summary>
        public const string FactorySettingsConfigSectionName = "FactorySettings";

        private static FactorySettings __settings;

        private static FactorySettings FactorySettings
        {
            get
            {
                if (__settings == null)
                    lock (typeof(Factory))
                        if (__settings == null)
                            __settings = ConfigurationManager<FactorySettings>.GetConfiguration();
                return __settings;
            }
        }

        /// <summary>Registers the Concrete Type to be constructed for a given Base Type.</summary>
        /// <param name="baseType">The Base Type.</param>
        /// <param name="concreteType">The Concrete Type.</param>
        public static void RegisterType(Type baseType, Type concreteType)
        {
            FactorySettings.Register(baseType, concreteType);
        }

        /// <summary>Builds a new instance of <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">Type to build.</typeparam>
        /// <returns>New instance of type <typeparamref name="T"/></returns>
        public static T Build<T>()
        {
            return (T)Build(typeof(T));
        }

        /// <summary>Builds the specified type.</summary>
        /// <param name="type">The type to build.</param>
        /// <returns>New instance of <paramref name="type"/></returns>
        public static object Build(Type type)
        {
            object retVal = Construct(type);

            if (retVal != null && retVal is ISupportLazyInitialization)
                ((ISupportLazyInitialization)retVal).Initialize();
            return retVal;
        }

        internal static object Construct(Type type)
        {
            MethodBase builderMethod = FactorySettings.GetBuilder(type);
            object retVal;
            if (builderMethod.IsConstructor)
                retVal = ((ConstructorInfo)builderMethod).Invoke(null);
            else
                retVal = builderMethod.Invoke(null, null);
            return retVal;
        }
    }

}
