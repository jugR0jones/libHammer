using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Extension_Methods
{

    /// <summary>
    /// 
    /// </summary>
    public static class IQueryableExtensions
    {

        /*
    private async void Foo() {
 
        using (var db = new Database1Entities()) {
 
            var qry = await (from emp in db.Employees
                        where emp.Salary > 1
                        select emp).ToListAsync();
 
 
            foreach (var item in qry) {
                Debug.WriteLine(item.Name);
            }
                 
        }
 
    }
         */
        /// <summary>
        /// Async create of a System.Collections.Generic.List<T> from an 
        /// System.Collections.Generic.IQueryable<T>.
        /// </summary>
        /// <author>Fons Sonnemans</author>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="list">The System.Collections.Generic.IEnumerable<T> 
        /// to create a System.Collections.Generic.List<T> from.</param>
        /// <returns> A System.Collections.Generic.List<T> that contains elements 
        /// from the input sequence.</returns>
        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> list)
        {
            return Task.Run(() => list.ToList());
        }

    }
}
