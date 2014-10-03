using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    
    /// <summary>
    /// Test methods to test the libHammer string functions.
    /// </summary>
    public class StringTests
    {

        [TestMethod()]
        public void TestEmailValidation() {
            Assert.IsTrue("yellowdog@someemail.uk".IsValidEmailAddress());
            Assert.IsTrue("yellow.444@email4u.co.uk".IsValidEmailAddress());
            Assert.IsFalse("adfasdf".IsValidEmailAddress());
            Assert.IsFalse("asd@asdf".IsValidEmailAddress());
        }

        /// <summary>
        ///A test for IsValidUrl
        ///</summary>
        [TestMethod()]
        public void IsValidUrlTest()
        {
            Assert.IsTrue("http://www.codeproject.com".IsValidUrl());
            Assert.IsTrue("https://www.codeproject.com/#some_anchor".IsValidUrl());
            Assert.IsTrue("https://localhost".IsValidUrl());
            Assert.IsTrue("http://www.abcde.nf.net/signs-banners.jpg".IsValidUrl());
            Assert.IsTrue("http://aa-bbbb.cc.bla.com:80800/test/" +
                          "test/test.aspx?dd=dd&id=dki".IsValidUrl());
            Assert.IsFalse("http:wwwcodeprojectcom".IsValidUrl());
            Assert.IsFalse("http://www.code project.com".IsValidUrl());
        }

        public void UrlAvailableTest()
        {
            Assert.IsTrue("www.codeproject.com".UrlAvailable());
            Assert.IsFalse("www.asjdfalskdfjalskdf.com".UrlAvailable());
        }

        public void ReverseTest()
        {
            string input = "yellow dog";
            string expected = "god wolley";
            string actual = input.Reverse();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsNumberTest()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Assert.IsTrue("12345".IsNumber(false));
            Assert.IsTrue("   12345".IsNumber(false));
            Assert.IsTrue("12.345".IsNumber(true));
            Assert.IsTrue("   12,345 ".IsNumber(true));
            Assert.IsTrue("12 345".IsNumber(false));
            Assert.IsFalse("tractor".IsNumber(true));
        }

        /// <summary>
        ///A test for RemoveDiacritics
        ///</summary>
        [TestMethod()]
        public void RemoveDiacriticsTest()
        {
            //contains all czech accents
            ///  input:  "Příliš žluťoučký kůň úpěl ďábelské ódy."
            ///  result: "Prilis zlutoucky kun upel dabelske ody."
            string actual = input.RemoveDiacritics();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Nl2BrTest()
        {
            string input = "yellow dog" + Environment.NewLine + "black cat";
            string expected = "yellow dog<br />black cat";
            string actual = input.Nl2Br();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MD5Test()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            string expected = "9e107d9d372bb6826bd81d3542a419d6";
            string actual = input.MD5();
            Assert.AreEqual(expected, actual);
        }
    }
}
