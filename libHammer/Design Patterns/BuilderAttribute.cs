using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Design_Patterns
{

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class BuilderAttribute : Attribute
    {
        /// <summary>
        /// Name of the static parameterless builder method.
        /// </summary>
        public readonly string BuilderMethodName;

        /// <summary>
        /// Initialises a new instance of the <see cref="BuilderAttribute"/> class.
        /// </summary>
        /// <param name="builderMethodName"></param>
        public BuilderAttribute(string builderMethodName)
        {
            BuilderMethodName = builderMethodName;
        }

        /// <summary>
        /// Gets the builder method for the specified type.
        /// </summary>
        /// <param name="enclosingType">Type in which the builder method is present.</param>
        /// <returns>The static builder method.</returns>
        public MethodInfo GetBuilderMethod(Type enclosingType)
        {
            return enclosingType.GetMethod(BuilderMethodName, BindingFlags.Public | System.Reflection.BindingFlags.Static);
        }
    }

}
