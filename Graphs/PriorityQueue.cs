using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class PriorityQueue<TQueueItem, TPriority>
    {
        private readonly SortedDictionary<TPriority, Queue<TQueueItem>> 
            _sortedDictionary = new SortedDictionary<TPriority, Queue<TQueueItem>>();

        public void Enqueue(TQueueItem value, TPriority priority)
        {
            Queue<TQueueItem> queue;

            if (!_sortedDictionary.TryGetValue(priority, out queue))
            {
                queue = new Queue<TQueueItem>();

                _sortedDictionary.Add(priority, queue);
            }

            queue.Enqueue(value);
        }
        public TPriority Dequeue(out TQueueItem queueItem)
        {
            var keyValuePair = _sortedDictionary.First();

            queueItem = keyValuePair.Value.Dequeue();

            if (keyValuePair.Value.Count == 0) 
                _sortedDictionary.Remove(keyValuePair.Key);

            return keyValuePair.Key;
        }

        public bool IsEmpty
        {
            get { return !_sortedDictionary.Any(); }
        }
    }
}
