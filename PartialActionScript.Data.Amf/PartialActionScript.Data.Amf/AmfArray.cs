using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    public sealed class AmfArray : IAmfValue,IDictionary<int,IAmfValue>,IDictionary<string, IAmfValue>,IEnumerable<KeyValuePair<int,IAmfValue>>,IEnumerable<KeyValuePair<string,IAmfValue>>,IEnumerable<IAmfValue>
    {
        #region Constructor

        public AmfArray()
        {
            
            this.array_ = new Dictionary<object, IAmfValue>();
        }

        #endregion

        #region Finalizer


        #endregion

        #region Property


        #endregion

        #region Method

        #region IAmfValue

        public AmfValueType ValueType
        {
            get { return AmfValueType.EcmaArray; }
        }

        public bool GetBoolean()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public AmfArray GetArray()
        {
            return this;
        }

        public string GetString()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public AmfObject GetObject()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public DateTimeOffset GetDate()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public XmlDocument GetXml()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public IReadOnlyList<byte> GetByteArray()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public IReadOnlyList<int> GetVectorInt()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public IReadOnlyList<uint> GetVectorUInt()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public IReadOnlyList<double> GetVectorDouble()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public IReadOnlyList<IAmfValue> GetVectorObject()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public double GetNumber()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public IBuffer Sequencify(AmfEncodingType encodingType)
        {
            return AmfSequencer.Sequencify(this, encodingType);
        }

        public override string ToString()
        {
            return string.Join(",", 
                from item in this.array_
                select item.Value.ToString()
                );
        }

        #endregion


        #endregion

        #region Private

        private IDictionary<object, IAmfValue> array_;

        private IEnumerator<KeyValuePair<T, IAmfValue>> getEnumrator<T>()
        {
            return (from pair in this.array_
                    where pair.Key is T
                    select new KeyValuePair<T, IAmfValue>((T)pair.Key, pair.Value)).GetEnumerator();
        }

        private void copyTo<T>(KeyValuePair<T,IAmfValue>[] array,int arrayIndex)
        {
            var items = from item in this.array_
                        where item.Key is T
                        select new KeyValuePair<T, IAmfValue>((T)item.Key, item.Value);

            Array.Copy(items.ToArray(), array, arrayIndex);
        }

        private ICollection<T> getKeys<T>()
        {
            var keys = new List<T>();
            keys.AddRange(from key in this.array_.Keys
                          where key is T
                          select (T)key);
            return keys;
        }

        #endregion


        #region IDictionary

        IEnumerator<KeyValuePair<string, IAmfValue>> IEnumerable<KeyValuePair<string, IAmfValue>>.GetEnumerator()
        {
            return getEnumrator<string>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.array_.GetEnumerator();
        }

        IEnumerator<KeyValuePair<int, IAmfValue>> IEnumerable<KeyValuePair<int, IAmfValue>>.GetEnumerator()
        {
            return getEnumrator<int>();
        }

        public void Add(string key, IAmfValue value)
        {
            if(key == null)
                 throw new ArgumentNullException();

            int index;

            if (int.TryParse(key, out index))
            {
                this.array_.Add(index, value);
            }
            else
            {
                this.array_.Add(key, value);
            }

            
        }

        public bool ContainsKey(string key)
        {
            return this.array_.ContainsKey(key);
        }


        ICollection<string> IDictionary<string, IAmfValue>.Keys
        {
            get { return this.getKeys<string>(); }
        }

        public void Clear()
        {
            this.array_.Clear();
        }

        void ICollection<KeyValuePair<string, IAmfValue>>.Clear()
        {
            this.array_.Clear();
        }

        public bool Remove(string key)
        {
            return this.array_.Remove(key);
        }

        public bool TryGetValue(string key, out IAmfValue value)
        {
            return this.array_.TryGetValue(key, out value);
        }

        public ICollection<IAmfValue> Values
        {
            get { return this.array_.Values; }
        }

        ICollection<IAmfValue> IDictionary<string, IAmfValue>.Values
        {
            get { return this.array_.Values; }
        }

        public IAmfValue this[string index]
        {
            get
            {
                return this.array_[index];
            }
            set
            {
                this.array_[index] = value;
            }
        }

        public void Add(KeyValuePair<string, IAmfValue> item)
        {
            this.Add(item.Key, item.Value);
        }


        public bool Contains(KeyValuePair<string, IAmfValue> item)
        {
            return this.array_.Contains(new KeyValuePair<object, IAmfValue>(item.Key, item.Value));
        }

        public void CopyTo(KeyValuePair<string, IAmfValue>[] array, int arrayIndex)
        {
            copyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.array_.Count; }
        }

        int ICollection<KeyValuePair<string, IAmfValue>>.Count
        {
            get { return this.Count; }
        }

        public bool IsReadOnly
        {
            get { return this.array_.IsReadOnly; }
        }

        bool ICollection<KeyValuePair<string, IAmfValue>>.IsReadOnly
        {
            get { return this.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, IAmfValue> item)
        {
            return this.array_.Remove(item);
        }

        public void Add(int key, IAmfValue value)
        {
            this.array_.Add(key, value);
        }

        public bool ContainsKey(int key)
        {
            return this.array_.ContainsKey(key);
        }

        ICollection<int> IDictionary<int, IAmfValue>.Keys
        {
            get
            {
                return this.getKeys<int>();
            }
        }

        public bool Remove(int key)
        {
            return this.array_.Remove(key);
        }

        public bool TryGetValue(int key, out IAmfValue value)
        {
            return this.array_.TryGetValue(key, out value);
        }

        public IAmfValue this[int key]
        {
            get
            {
                return this.array_[key];
            }
            set
            {
                this.array_[key] = value;
            }
        }

        public void Add(KeyValuePair<int, IAmfValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<int, IAmfValue> item)
        {
            return this.array_.Contains(new KeyValuePair<object, IAmfValue>(item.Key, item.Value));
        }

        public void CopyTo(KeyValuePair<int, IAmfValue>[] array, int arrayIndex)
        {
            copyTo(array, arrayIndex);
            
        }

        public bool Remove(KeyValuePair<int, IAmfValue> item)
        {
            return this.array_.Remove(new KeyValuePair<object, IAmfValue>(item.Key, item.Value));
        }

        public IEnumerator<IAmfValue> GetEnumerator()
        {
            return this.array_.Values.GetEnumerator();
        }

        #endregion
    }
}
