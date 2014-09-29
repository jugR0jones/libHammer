using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Design_Patterns
{

    /// <summary>
    /// A strongly typed implementation of the Singleton design pattern.
    /// Supports Lazy initialisation and static builder methods.
    /// Taken from Sameera Perera and his Codoxide library.
    /// </summary>
    /// <typeparam name="T">The type implementing the Singleton pattern.</typeparam>
    public class Singleton<T>
    {

        /// <summary>
        /// 
        /// </summary>
        public static T Instance
        {
            get
            {
                if (!SingletonProvider.IsInitialized)
                {
                    lock (typeof(Singleton<T>))
                    {
                        if (!SingletonProvider.IsInitialized)
                        {
                            SingletonProvider.Initialize();
                        }
                    }
                }
                return SingletonProvider._instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class SingletonProvider
        {
            internal static bool IsInitialized { get; private set; }
            internal static readonly T _instance;

            /// <summary>
            /// 
            /// </summary>
            static SingletonProvider()
            {
                Type type = typeof(T);
                object[] builderAttributes;

                if (type.IsInterface)
                {
                    /* Generate an instance using the interface. */
                    //    _instance = (T)Factory.Construct(type);
                }
                else
                {
                    /* */
                    if (0 < (builderAttributes = type.GetCustomAttributes(typeof(BuilderAttribute), true)).Length)
                    {
                        MethodInfo builderMethodInfo = (builderAttributes[0] as BuilderAttribute).GetBuilderMethod(type);
                        _instance = (T)builderMethodInfo.Invoke(null, null);
                    }
                    else
                    {
                        /* */
                        ConstructorInfo constructorInfo = type.GetConstructor(BindingFlags.CreateInstance
                                                                                        | BindingFlags.Instance
                                                                                        | BindingFlags.NonPublic
                                                                                        , null
                                                                                        , Type.EmptyTypes
                                                                                        , new ParameterModifier[0]);
                        if (constructorInfo == null)
                        {
                            throw new DesignPatternException(String.Format("Type '{0}' does not have an accessible constructor.", type.FullName));
                        }

                        _instance = (T)constructorInfo.Invoke(null);
                    }
                }

                IsInitialized = !(_instance is ISupportLazyInitialization);
            }

            /// <summary>
            /// 
            /// </summary>
            public static void Initialize()
            {
                var lazyInitInstance = (ISupportLazyInitialization)_instance;
                lazyInitInstance.Initialize();
                IsInitialized = true;
            }
        }

    }

}
