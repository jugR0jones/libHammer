using System.Collections.Generic;

namespace libHammer
{
    
    /// <summary>
    /// Extensions to the C# List
    /// </summary>
    public static class ListExtensions<T>
    {

        /// <summary>
        /// Insert an item at the top of the list.
        /// NOTE: This creates a new list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        //public static List<T> InsertFirst<T>(this List<T> list, T item)
        //{
        //    List<T> newList = new List<T>();

        //    newList.Add(item);
        //    newList.AddRange(list);

        //    return newList;
        //}

        /// <summary>
        /// Converts a list of objects to a Comma Separated String (CSV).
        /// NOTE: Care my be taken when using objects! The results may not be 100% correct.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToCsv<T>(this List<T> list)
        {
            return string.Join(",", list);
        }
        
    }
}
