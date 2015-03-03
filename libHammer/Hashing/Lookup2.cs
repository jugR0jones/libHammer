using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Hashing
{

    /// <summary>
    /// Implementation of the "lookup2" hash algorithm by Bob Jenkins.
    /// </summary>
    public class Lookup2
    {

        /// <summary>
        /// Generate the hash code for a collection of keys.
        /// </summary>
        /// <param name="keys">An enumerable list of items; GetHashCode() will be called on each of them.</param>
        /// <param name="previousHash">The previous hash result if this method is being called more than once, otherwise any arbitrary value.</param>
        /// <returns>An integer hash code.</returns>
        public static int GetHashCode(System.Collections.IEnumerable keys, int previousHash)
        {
            System.Collections.IEnumerator keyi = keys.GetEnumerator();
            uint a = 0x9e3779b9;
            uint b = a;
            uint c = (uint)previousHash;
            uint[] k = new uint[12];
            uint length = 0;
            uint sublen = 12;

            while (true)
            {
                sublen = 0; while (keyi.MoveNext() && sublen < 12)
                {
                    k[sublen] = (uint)keyi.Current.GetHashCode();
                    sublen++; length++;
                }
                if (sublen == 12)
                {
                    a += k[0] + (k[1] << 8) + (k[2] << 16) + (k[3] << 24);
                    b += k[4] + (k[5] << 8) + (k[6] << 16) + (k[7] << 24);
                    c += k[8] + (k[9] << 8) + (k[10] << 16) + (k[11] << 24);

                    a -= b; a -= c; a ^= (c >> 13); b -= c; b -= a; b ^= (a << 8); c -= a; c -= b; c ^= (b >> 13); a -= b; a -= c; a ^= (c >> 12);

                    b -= c; b -= a; b ^= (a << 16); c -= a; c -= b; c ^= (b >> 5); a -= b; a -= c; a ^= (c >> 3); b -= c; b -= a; b ^= (a << 10);
                    c -= a; c -= b; c ^= (b >> 15);
                }
                else
                {
                    c += length;
                    if (sublen >= 11) c += (k[10] << 24);
                    if (sublen >= 10) c += (k[9] << 16);
                    if (sublen >= 9) c += (k[8] << 8);
                    // the first byte of c is reserved for the sublength
                    if (sublen >= 8) b += (k[7] << 24);
                    if (sublen >= 7) b += (k[6] << 16);
                    if (sublen >= 6) b += (k[5] << 8);
                    if (sublen >= 5) b += (k[4]);
                    if (sublen >= 4) a += (k[3] << 24);
                    if (sublen >= 3) a += (k[2] << 16);
                    if (sublen >= 2) a += (k[1] << 8);
                    if (sublen >= 1) a += (k[0]);

                    a -= b;
                    a -= c;
                    a ^= (c >> 13);
                    b -= c;
                    b -= a;
                    b ^= (a << 8);
                    c -= a;
                    c -= b;
                    c ^= (b >> 13);
                    a -= b;
                    a -= c;
                    a ^= (c >> 12);

                    b -= c; b -= a; b ^= (a << 16);
                    c -= a; c -= b; c ^= (b >> 5);
                    a -= b; a -= c; a ^= (c >> 3);
                    b -= c; b -= a; b ^= (a << 10);
                    c -= a; c -= b; c ^= (b >> 15);
                    return (int)c;
                }
            }
        }
    }
}
