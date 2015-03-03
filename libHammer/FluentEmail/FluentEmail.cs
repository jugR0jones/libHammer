using libHammer.FluentEmail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libHammer.FluentEmail
{

    /// <summary>
    /// Fluent email class.
    /// Taken from https://github.com/lukencode/FluentEmail
    /// </summary>
    public class FluentEmail : IDisposable
    {

        private SmtpClient _smtpClient;
        private bool _usingSsl;
        private bool _bodyIsHtml = true;
        private IFluentTemplateRenderer _templateRenderer;

        public MailMessage Message { get; private set; }

        #region Constructors

        /// <summary>
        /// Creates a new email instance using the default from address in the smtp config settings
        /// </summary>
        public FluentEmail()
            : this(new SmtpClient())
        {

        }

        /// <summary>
        /// Create a new email instance which overrides the default client from the config file.
        /// </summary>
        /// <param name="smtpClient"></param>
        public FluentEmail(SmtpClient smtpClient)
            : this(smtpClient, new RazorRenderer())
        {

        }

        /// <summary>
        /// Creates a new email instance which overrides the default client from a config file, and
        /// </summary>
        /// <param name="smtpClient"></param>
        /// <param name="templateRenderer"></param>
        public FluentEmail(SmtpClient smtpClient, IFluentTemplateRenderer templateRenderer)
        {
            this._smtpClient = smtpClient;
            this._templateRenderer = templateRenderer;

            this.Message = new MailMessage();
        }

        /// <summary>
        /// Creates a new email instance
        /// </summary>
        /// <param name="fromEmailAddress"></param>
        /// <param name="fromName"></param>
        public FluentEmail(string fromEmailAddress, string fromName)
            : this(new SmtpClient(), new RazorRenderer(), fromEmailAddress, fromName)
        {

        }

        public FluentEmail(SmtpClient smtpClient, string fromEmailAddress, string fromName)
            : this(smtpClient, new RazorRenderer(), fromEmailAddress, fromName)
        {

        }

        #endregion

        #region IDisposable Methods

        /// <summary>
        /// Releases all resources
        /// </summary>
        public void Dispose()
        {
            if (this._smtpClient != null)
                this._smtpClient.Dispose();

            if (Message != null)
                Message.Dispose();
        }

        #endregion



        /// <summary>
        ///  Creates a new Email instance and sets the from property with overrides the default  filetemplate rendering: RazorEngine.
        /// </summary>
        /// <param name="defaultRenderer">The template rendering engine</param>
        /// <param name="emailAddress">Email address to send from</param>
        /// <param name="name">Name to send from</param>
        public FluentEmail(IFluentTemplateRenderer defaultRenderer, string emailAddress, string name = "") : this(new SmtpClient(), defaultRenderer, emailAddress, name) { }

        /// <summary>
        /// Creates a new Email instance and sets the from property with overrides the default client from .config and filetemplate rendering: RazorEngine.
        /// </summary>
        /// <param name="client">Smtp client to send from</param>
        /// <param name="defaultRenderer">The template rendering engine</param>
        /// <param name="emailAddress">Email address to send from</param>
        /// <param name="name">Name to send from</param>
        public FluentEmail(SmtpClient client, IFluentTemplateRenderer defaultRenderer, string emailAddress, string name = "")
            : this(client, defaultRenderer)
        {
            Message.From = new MailAddress(emailAddress, name);
        }

        /// <summary>
        /// Adds a reciepient to the email, Splits name and address on ';'
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient</param>
        /// <param name="name">Name of recipient</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail To(string emailAddress, string name)
        {
            if (emailAddress.Contains(";"))
            {
                //email address has semi-colon, try split
                var nameSplit = name.Split(';');
                var addressSplit = emailAddress.Split(';');
                for (int i = 0; i < addressSplit.Length; i++)
                {
                    var currentName = string.Empty;
                    if ((nameSplit.Length - 1) >= i)
                    {
                        currentName = nameSplit[i];
                    }
                    Message.To.Add(new MailAddress(addressSplit[i], currentName));
                }
            }
            else
            {
                Message.To.Add(new MailAddress(emailAddress, name));
            }
            return this;
        }

        /// <summary>
        /// Adds a reciepient to the email
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient (allows multiple splitting on ';')</param>
        /// <returns></returns>
        public FluentEmail To(string emailAddress)
        {
            if (emailAddress.Contains(";"))
            {
                foreach (string address in emailAddress.Split(';'))
                {
                    Message.To.Add(new MailAddress(address));
                }
            }
            else
            {
                Message.To.Add(new MailAddress(emailAddress));
            }

            return this;
        }

        /// <summary>
        /// Adds all reciepients in list to email
        /// </summary>
        /// <param name="mailAddresses">List of recipients</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail To(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Message.To.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Adds a Carbon Copy to the email
        /// </summary>
        /// <param name="emailAddress">Email address to cc</param>
        /// <param name="name">Name to cc</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail CC(string emailAddress, string name = "")
        {
            Message.CC.Add(new MailAddress(emailAddress, name));
            return this;
        }

        /// <summary>
        /// Adds all Carbon Copy in list to an email
        /// </summary>
        /// <param name="mailAddresses">List of recipients to CC</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail CC(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Message.CC.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Adds a blind carbon copy to the email
        /// </summary>
        /// <param name="emailAddress">Email address of bcc</param>
        /// <param name="name">Name of bcc</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail BCC(string emailAddress, string name = "")
        {
            Message.Bcc.Add(new MailAddress(emailAddress, name));
            return this;
        }

        /// <summary>
        /// Adds all blind carbon copy in list to an email
        /// </summary>
        /// <param name="mailAddresses">List of recipients to BCC</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail BCC(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Message.Bcc.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Sets the ReplyTo address on the email
        /// </summary>
        /// <param name="address">The ReplyTo Address</param>
        /// <returns></returns>
        public FluentEmail ReplyTo(string address)
        {
            Message.ReplyToList.Add(new MailAddress(address));

            return this;
        }

        /// <summary>
        /// Sets the ReplyTo address on the email
        /// </summary>
        /// <param name="address">The ReplyTo Address</param>
        /// <param name="name">The Display Name of the ReplyTo</param>
        /// <returns></returns>
        public FluentEmail ReplyTo(string address, string name)
        {
            Message.ReplyToList.Add(new MailAddress(address, name));

            return this;
        }

        /// <summary>
        /// Sets the subject of the email
        /// </summary>
        /// <param name="subject">email subject</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail Subject(string subject)
        {
            Message.Subject = subject;
            return this;
        }

        /// <summary>
        /// Adds a Body to the Email
        /// </summary>
        /// <param name="body">The content of the body</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
        public FluentEmail Body(string body)
        {
            Message.Body = body;
            return this;
        }

        /// <summary>
        /// Marks the email as High Priority
        /// </summary>
        public FluentEmail HighPriority()
        {
            Message.Priority = MailPriority.High;
            return this;
        }

        /// <summary>
        /// Marks the email as Low Priority
        /// </summary>
        public FluentEmail LowPriority()
        {
            Message.Priority = MailPriority.Low;
            return this;
        }

        /// <summary>
        /// Set the template rendering engine to use, defaults to RazorEngine
        /// </summary>
        public FluentEmail UsingTemplateEngine(IFluentTemplateRenderer renderer)
        {
            this._templateRenderer = renderer;
            return this;
        }

        /// <summary>
        /// Adds template to email from embedded resource
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Path the the embedded resource eg [YourAssembly].[YourResourceFolder].[YourFilename.txt]</param>
        /// <param name="model">Model for the template</param>
        /// <param name="assembly">The assembly your resource is in. Defaults to calling assembly.</param>
        /// <returns></returns>
        public FluentEmail UsingTemplateFromEmbedded<T>(string path, T model, Assembly assembly = null)
        {
            CheckRenderer();

            assembly = assembly ?? Assembly.GetCallingAssembly();

            var template = EmbeddedResourceHelper.GetResourceAsString(assembly, path);
            var result = this._templateRenderer.Parse(template, model, _bodyIsHtml);
            Message.Body = result;
            Message.IsBodyHtml = _bodyIsHtml;

            return this;
        }

        /// <summary>
        /// Adds the template file to the email
        /// </summary>
        /// <param name="filename">The path to the file to load</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail UsingTemplateFromFile<T>(string filename, T model)
        {
            var path = GetFullFilePath(filename);
            var template = "";

            TextReader reader = new StreamReader(path);

            try
            {
                template = reader.ReadToEnd();
            }
            finally
            {
                reader.Close();
            }

            CheckRenderer();

            var result = this._templateRenderer.Parse(template, model, _bodyIsHtml);
            Message.Body = result;
            Message.IsBodyHtml = _bodyIsHtml;

            return this;
        }

        /// <summary>
        /// Adds a culture specific template file to the email
        /// </summary>
        /// <param name="filename">The path to the file to load</param>
        /// /// <param name="model">The razor model</param>
        /// <param name="culture">The culture of the template (Default is the current culture)</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail UsingCultureTemplateFromFile<T>(string filename, T model, CultureInfo culture = null)
        {
            var wantedCulture = culture ?? Thread.CurrentThread.CurrentUICulture;
            var cultureFile = GetCultureFileName(filename, wantedCulture);
            return UsingTemplateFromFile(cultureFile, model);
        }

        /// <summary>
        /// Adds razor template to the email
        /// </summary>
        /// <param name="template">The razor template</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail UsingTemplate<T>(string template, T model, bool isHtml = true)
        {
            CheckRenderer();

            var result = this._templateRenderer.Parse(template, model, isHtml);
            Message.Body = result;
            Message.IsBodyHtml = isHtml;

            return this;
        }

        /// <summary>
        /// Adds an Attachment to the Email
        /// </summary>
        /// <param name="attachment">The Attachment to add</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail Attach(Attachment attachment)
        {
            if (!Message.Attachments.Contains(attachment))
                Message.Attachments.Add(attachment);

            return this;
        }

        /// <summary>
        /// Adds Multiple Attachments to the Email
        /// </summary>
        /// <param name="attachments">The List of Attachments to add</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail Attach(IList<Attachment> attachments)
        {
            foreach (var attachment in attachments.Where(attachment => !Message.Attachments.Contains(attachment)))
            {
                Message.Attachments.Add(attachment);
            }
            return this;
        }

        /// <summary>
        /// Over rides the default client from .config file
        /// </summary>
        /// <param name="client">Smtp client to send from</param>
        /// <returns>Instance of the Email class</returns>
        /// [Obsolete("FluentEmail.Email.From.UsingClient(SmtpClient client) is obsolete: 'Please user the constructor'")]
        public FluentEmail UsingClient(SmtpClient client)
        {
            this._smtpClient = client;
            return this;
        }

        public FluentEmail UseSSL()
        {
            this._usingSsl = true;
            return this;
        }

        /// <summary>
        /// Sets Message to html (set by default)
        /// </summary>
        public FluentEmail BodyAsHtml()
        {
            this._bodyIsHtml = true;
            return this;
        }

        /// <summary>
        /// Sets Message to plain text (set by default)
        /// </summary>
        public FluentEmail BodyAsPlainText()
        {
            _bodyIsHtml = false;
            return this;
        }

        /// <summary>
        /// Sends email synchronously
        /// </summary>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail Send()
        {
            
                this._smtpClient.EnableSsl = this._usingSsl;

            Message.IsBodyHtml = _bodyIsHtml;

            this._smtpClient.Send(Message);
            return this;
        }

        /// <summary>
        /// Sends message asynchronously with a callback
        /// handler
        /// </summary>
        /// <param name="callback">Method to call on complete</param>
        /// <param name="token">User token to pass to callback</param>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail SendAsync(SendCompletedEventHandler callback, object token = null)
        {
                this._smtpClient.EnableSsl = this._usingSsl;

            Message.IsBodyHtml = _bodyIsHtml;

            this._smtpClient.SendCompleted += callback;
            this._smtpClient.SendAsync(Message, token);

            return this;
        }

        /// <summary>
        /// Cancels async message sending
        /// </summary>
        /// <returns>Instance of the Email class</returns>
        public FluentEmail Cancel()
        {
            this._smtpClient.SendAsyncCancel();
            return this;
        }

        private void CheckRenderer()
        {
            if (this._templateRenderer != null) return;

            
                this._templateRenderer = new RazorRenderer();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetFullFilePath(string filename)
        {
            if (filename.StartsWith("~"))
            {
                var baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
                return Path.GetFullPath(baseDir + filename.Replace("~", ""));
            }

            return Path.GetFullPath(filename);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        private static string GetCultureFileName(string fileName, CultureInfo culture)
        {
            var fullFilePath = GetFullFilePath(fileName);
            var extension = Path.GetExtension(fullFilePath);
            var cultureExtension = string.Format("{0}{1}", culture.Name, extension);

            var cultureFile = Path.ChangeExtension(fullFilePath, cultureExtension);
            if (File.Exists(cultureFile))
                return cultureFile;
            else
                return fullFilePath;
        }

    }
}
