using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Data_Packing
{

    /// <summary>
    /// ASN.1 is a standard, a language, describing the rules of data packing techniques
    /// used in banking, telecommunication and different smartcard related industries.
    /// There are several different ASN.1 encoding rules such as BER, CER, DER, XER, etc.
    /// </summary>
    public class Asn1
    {

        #region Basic Encoding Rules (BER)

        //TODO: Add these numbers in properly.
        private enum TlvIdentifierClass {
            Universal = 0,
            Application = 1 << 6,
            Context = 1 << 7,
            Private = (1 << 6 | 1 << 7)
        }

        private enum TlvIdentifierPc
        {
            Primitive = 0,
            Constructed = 1 << 5,
        }

        private class TlvEncodingStructure {
            public byte Identifier { get; set; }
        }

        /// <summary>
        /// Basic Encoding Rules (BER) encoder
        /// Also known as Tag Length Value (TLV) encoding.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string BerEncode(string message)
        {

            return String.Empty;
        }

        #endregion

    }
}
