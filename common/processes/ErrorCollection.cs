using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MCSMLauncher.common.processes
{
    /// <summary>
    /// This class implements an enumerable collection of strings representing error patterns
    /// displayed on the console output
    /// </summary>
    public class ErrorCollection : ICollection<string>
    {
        /// <summary>
        /// The core of the class, an internal list containing all of the error message patterns.
        /// </summary>
        private List<string> InternalErrorCollection { get; } = new List<string>();

        /// <summary>
        /// The amount of items inside the Error Collection
        /// </summary>
        public int Count => this.InternalErrorCollection.Count;

        /// <summary>
        /// Whether the Error Collection is ReadOnly or not
        /// </summary>
        public bool IsReadOnly { get; } = false;

        /// <summary>
        /// These two methods return the InternalErrorCollection's enumerator for iteration
        /// purposes.
        /// </summary>
        public IEnumerator<string> GetEnumerator()
        {
            return this.InternalErrorCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds an error pattern into the Error Collection.
        /// </summary>
        /// <param name="item">The string containing the pattern</param>
        public void Add(string item)
        {
            this.InternalErrorCollection.Add(item);
        }

        /// <summary>
        /// Removes an error pattern from the Error Collection.
        /// </summary>
        /// <param name="item">The item to remove from the collection</param>
        /// <returns></returns>
        public bool Remove(string item)
        {
            return this.InternalErrorCollection.Remove(item);
        }

        /// <summary>
        /// Clears the ErrorCollection
        /// </summary>
        public void Clear()
        {
            this.InternalErrorCollection.Clear();
        }

        /// <summary>
        /// Checks whether the Error Collection contains the specified item or not
        /// </summary>
        /// <param name="item">The string to match with</param>
        /// <returns>Whether the collection contains the item or not</returns>
        public bool Contains(string item)
        {
            return this.InternalErrorCollection.Contains(item);
        }

        /// <summary>
        /// Copies the Collection into an array
        /// </summary>
        /// <param name="array">The array to copy the collection into</param>
        /// <param name="arrayIndex">The starting index of the copying</param>
        public void CopyTo(string[] array, int arrayIndex)
        {
            this.InternalErrorCollection.ToArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Checks if any pattern inside the Error Collection contains the matching string
        /// </summary>
        /// <param name="match">The string to match with the patterns</param>
        /// <returns>Whether there's an error matching the string or not</returns>
        public bool StringMatches(string match)
        {
            return this.InternalErrorCollection.Any(match.Contains);
        }
    }
}