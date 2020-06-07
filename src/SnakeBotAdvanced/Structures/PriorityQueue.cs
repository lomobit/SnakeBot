using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SnakeBot.Structures
{
    public class PriorityQueue<T>
    {
        private Dictionary<int, List<T>> _priorityQueue;
        private int minPriority;

        public int Count
        {
            get
            {
                int result = 0;
                foreach (var list in this._priorityQueue.Values)
                {
                    result += list.Count;
                }

                return result;
            }
        }

        public PriorityQueue()
        {
            this._priorityQueue = new Dictionary<int, List<T>>();
        }

        public T Peek()
        {
            return this._priorityQueue[this.minPriority][0];
        }

        public T Dequeue()
        {
            T result = this._priorityQueue[this.minPriority][0];
            this._priorityQueue[this.minPriority].RemoveAt(0);

            if (this._priorityQueue[this.minPriority].Count == 0)
            {
                this._priorityQueue.Remove(this.minPriority);
                if (this._priorityQueue.Keys.Count > 0)
                {
                    this.minPriority = this._priorityQueue.Keys.Min();
                }
            }

            return result;
        }

        public void Enqueue(T item, int priority)
        {
            if (this._priorityQueue.ContainsKey(priority))
            {
                this._priorityQueue[priority].Add(item);
            }
            else
            {
                if (priority < this.minPriority || this._priorityQueue.Keys.Count == 0)
                {
                    this.minPriority = priority;
                }

                this._priorityQueue.Add(priority, new List<T>() { item });
            }
        }

        public bool Contains(T item)
        {
            foreach(var list in this._priorityQueue.Values)
            {
                if (list.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void Remove(T item)
        {
            foreach (var key in this._priorityQueue.Keys)
            {
                if (this._priorityQueue[key].Contains(item))
                {
                    this._priorityQueue[key].Remove(item);
                    if (this._priorityQueue[key].Count == 0)
                    {
                        this._priorityQueue.Remove(key);
                    }

                    return;
                }
            }
        }

        public T TryGetEqualItem(T item)
        {
            foreach (var list in this._priorityQueue.Values)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Equals(item))
                    {
                        return list[i];
                    }
                }
            }

            return default;
        }
    }
}
