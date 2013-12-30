using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    public sealed class AmfObject:IAmfValue,IDictionary<string,IAmfValue>
    {
        #region Constructor

        public AmfObject() : this(string.Empty, false) { }

        public AmfObject(bool isDynamic) : this(string.Empty, isDynamic) { }

        public AmfObject(string typeName, bool isdynamic)
        {
            this.TypeName = typeName;
            this.IsDynamic = isdynamic;
            this.propertyMap_ = new Dictionary<string, IAmfValue>();

            if (isdynamic)
            {
                this.dynamicPrpoertyMap_ = new Dictionary<string, IAmfValue>();
            }
            else
            {
                this.dynamicPrpoertyMap_ = null;
            }
        }


        #endregion


        #region Property

        public string TypeName { get; private set; }

        public bool IsDynamic { get; private set; }

        public IDictionary<string, IAmfValue> DynamicPropertys
        {
            get
            {
                if (!this.IsDynamic)
                    throw new InvalidOperationException();

                return this.dynamicPrpoertyMap_;
            }
        }

        #endregion

        #region Method

        public override string ToString()
        {
            return string.Join(",", (from property in this.propertyMap_
                                     select property.Key + ":" + property.Value.ToString()));
        }


        internal Amf3ObjectTraitsInfo GetTraitsInfo()
        {
            if (!this.IsDynamic)
            {
                return new Amf3ObjectTraitsInfo(this.TypeName, this.Keys, this.IsDynamic);
            }
            else
            {
                return new Amf3ObjectTraitsInfo(this.TypeName, new string[0], this.IsDynamic);
            }
        }

        #endregion

        #region Private

        private IDictionary<string, IAmfValue> propertyMap_;

        private IDictionary<string, IAmfValue> dynamicPrpoertyMap_;

        #endregion

        #region IAmfValue

        public AmfValueType ValueType
        {
            get { return AmfValueType.Object; }
        }

        public bool GetBoolean()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public AmfArray GetArray()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public string GetString()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public AmfObject GetObject()
        {
            return this;
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
            return AmfSequencer.Sequencify(this,encodingType);
        }

        public IBuffer Sequencify()
        {
            return AmfSequencer.Sequencify(this);
        }


        #endregion

        #region IDictionary

        public void Add(string key, IAmfValue value)
        {
            this.propertyMap_.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return this.propertyMap_.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return this.propertyMap_.Keys; }
        }

        public bool Remove(string key)
        {
            return this.propertyMap_.Remove(key);
        }

        public bool TryGetValue(string key, out IAmfValue value)
        {
            return this.propertyMap_.TryGetValue(key, out value);
        }

        public ICollection<IAmfValue> Values
        {
            get { return this.propertyMap_.Values; }
        }

        public IAmfValue this[string key]
        {
            get
            {
                return this.propertyMap_[key];
            }
            set
            {
                this.propertyMap_[key] = value;
            }
        }

        public void Add(KeyValuePair<string, IAmfValue> item)
        {
            this.propertyMap_.Add(item);
        }

        public void Clear()
        {
            this.propertyMap_.Clear();
        }

        public bool Contains(KeyValuePair<string, IAmfValue> item)
        {
            return this.propertyMap_.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, IAmfValue>[] array, int arrayIndex)
        {
            this.propertyMap_.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.propertyMap_.Count; }
        }

        public bool IsReadOnly
        {
            get { return this.propertyMap_.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, IAmfValue> item)
        {
            return this.propertyMap_.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, IAmfValue>> GetEnumerator()
        {
            return this.propertyMap_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
