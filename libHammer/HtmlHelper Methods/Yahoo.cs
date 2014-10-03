using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace libHammer.HtmlHelper_Methods
{
    public static class Yahoo
    {

        public static string YahooCalendar(this HtmlHelper helper, string linkText, string what, DateTime start, DateTime? end, string description, string venue, string street, string city, string attributes)
        {
            //parse duration
            var duration = "";
            if (end.HasValue && end > start)
            {
                var span = (TimeSpan)(end - start);
                duration = "&dur=" + span.ToString("HHMM");
            }

            var path = string.Format("http://calendar.yahoo.com/?v=60&view=d&type=10&title={0}&st={1}{2}&desc={3}&in_loc={4}&in_st={5}&in_csz={6}'",
                                what,
                                start.ToString("yyyyMMddTHHmmssZ"),
                                duration,
                                description,
                                venue,
                                street,
                                city);

            var calendar = string.Format("<a href='{0}' target='_blank' {1}>{2}</a>",
                                            HttpUtility.UrlPathEncode(path),
                                            helper.AttributeEncode(attributes),
                                            linkText);

            return calendar;
        }

        public static string YahooCalendar(this HtmlHelper helper, string linkText, string what, DateTime start, DateTime? end, string description, string venue, string street, string city)
        {
            return YahooCalendar(helper, linkText, what, start, end, description, venue, street, city, "");
        }

        public static string YahooCalendar(this HtmlHelper helper, string linkText, string what, DateTime start, string description)
        {
            return YahooCalendar(helper, linkText, what, start, null, description, "", "", "");
        }

    }
}
