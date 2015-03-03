using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Hashing
{

    /// <summary>
    /// Contract for implementing hash functions based on the SHA family of hash algorithms.
    /// SHA is a family of one-way cryptographic algorithms.
    /// </summary>
    public interface IShaHash
    {

        /// <summary>
        /// Compute the hash code for the string of data.
        /// </summary>
        /// <param name="plainText">The string of data to hash</param>
        /// <param name="salt">Initial value to help with the generation of random numbers used in the hash computation.</param>
        /// <returns>The encoded string.</returns>
        string Hash(string plainText, byte[] salt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        bool VerifyHash(string plainText, byte[] salt, string hash);
    }
}
