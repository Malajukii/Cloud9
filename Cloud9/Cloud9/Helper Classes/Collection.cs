using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloud9
{
    // we use this class instead of a normal list so we can update the list while looping through it, without messing things up

    public class Collection<T> : System.Collections.Generic.List<T>
    {
        #region Properties
        List<T> itemsToAdd = new List<T>(), itemsToRemove = new List<T>();
        #endregion

        #region Methods
        // more may be needed
        public void Add(T item)
        {
            itemsToAdd.Add(item);
        }
        public void AddRange(IEnumerable<T> items)
        {
            itemsToAdd.AddRange(items);
        }
        public void Remove(T item)
        {
            itemsToRemove.Add(item);            
        }
        // removerange maybe

        public void Update()
        {
            foreach (T item in itemsToAdd)
                base.Add(item);
            itemsToAdd.Clear();
            foreach (T item in itemsToRemove)
                base.Remove(item);
            itemsToRemove.Clear();
        }
        #endregion
    }
}
