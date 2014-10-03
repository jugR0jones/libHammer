using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;

namespace libHammer.FluentEmail
{

    public class RazorRenderer : IFluentTemplateRenderer
    {
        public RazorRenderer()
        {
        }

        public string Parse<T>(string template, T model, bool isHtml = true)
        {
            return Razor.Parse(template, model, template.GetHashCode().ToString());
        }
    }

}
