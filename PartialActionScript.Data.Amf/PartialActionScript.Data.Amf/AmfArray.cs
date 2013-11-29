using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PartialActionScript.Data.Amf
{
    public sealed class AmfArray
    {
        #region Constructor

        AmfArray()
        {

        }


        #endregion

        #region Finalizer

        ~AmfArray()
        {

        }

        #endregion

        #region Property


        #endregion

        #region Method

        public AmfValueType ValueType
        {
            get { throw new NotImplementedException(); }
        }

        public bool GetBoolean()
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }

        public AmfArray GetArray()
        {
            return this;
        }

        public double GetNumber()
        {
            throw new NotImplementedException();
        }

        public AmfObject GetObject()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private

        #endregion










        
    }
}
