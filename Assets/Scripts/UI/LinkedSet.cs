using System.Collections;
using System.Collections.Generic;

namespace cakeslice {
    public class LinkedSet<T> : IEnumerable<T> {
        private readonly LinkedList<T> list;

        private readonly Dictionary<T, LinkedListNode<T>> dictionary;

        public LinkedSet() {
            list = new LinkedList<T>();
            dictionary = new Dictionary<T, LinkedListNode<T>>();
        }

        public LinkedSet(IEqualityComparer<T> comparer) {
            list = new LinkedList<T>();
            dictionary = new Dictionary<T, LinkedListNode<T>>(comparer);
        }

        public bool Contains(T t) {
            return dictionary.ContainsKey(t);
        }


        public bool Add(T t) {
            if (dictionary.ContainsKey(t)) return false;

            LinkedListNode<T> node = list.AddLast(t);
            dictionary.Add(t, node);
            return true;
        }

        public void Clear() {
            list.Clear();
            dictionary.Clear();
        }

        public AddType AddOrMoveToEnd(T t) {
            LinkedListNode<T> node;

            if (dictionary.Comparer.Equals(t, list.Last.Value)) { return AddType.NO_CHANGE; }

            if (dictionary.TryGetValue(t, out node)) {
                list.Remove(node);
                node = list.AddLast(t);
                dictionary[t] = node;
                return AddType.MOVED;
            }

            node = list.AddLast(t);
            dictionary[t] = node;
            return AddType.ADDED;
        }

        public bool Remove(T t) {
            LinkedListNode<T> node;

            if (!dictionary.TryGetValue(t, out node) || !dictionary.Remove(t)) return false;
            list.Remove(node);
            return true;
        }

        public void ExceptWith(IEnumerable<T> enumerable) {
            foreach (T t in enumerable) Remove(t);
        }

        public IEnumerator<T> GetEnumerator() {
            return list.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() {
            return list.GetEnumerator();
        }


        public enum AddType {
            /// <summary>
            /// No changes were made
            /// </summary>
            NO_CHANGE,

            /// <summary>
            /// The value was added
            /// </summary>
            ADDED,

            /// <summary>
            /// The value was moved to the end.
            /// </summary>
            MOVED
        }
    }
}