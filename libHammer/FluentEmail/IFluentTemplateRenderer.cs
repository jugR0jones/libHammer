using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.FluentEmail
{

    /// <summary>
    /// 
    /// </summary>
    public interface IFluentTemplateRenderer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="isHtml"></param>
        /// <returns></returns>
        string Parse<T>(string template, T model, bool isHtml = true);

    }
}
