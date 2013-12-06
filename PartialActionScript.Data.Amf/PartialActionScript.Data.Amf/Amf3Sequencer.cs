﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal sealed class Amf3Sequencer
    {
        #region Constractor

        private Amf3Sequencer(IDataWriter writer)
        {
            this.objectRemains_ = new List<IAmfValue>();
            this.stringRemains_ = new List<string>();
            this.writer_ = new Amf3Writer(writer);
        }


        #endregion

        #region Private

        private List<IAmfValue> objectRemains_;

        private List<string> stringRemains_;

        private Amf3Writer writer_;

        private IBuffer sequencify( IAmfValue value)
        {
            this.writeValue(value);
            return this.writer_.DetatchBuffer();
        }


        private void writeValue(IAmfValue input)
        {
            switch (input.ValueType)
            {
                case AmfValueType.String:
                    this.writeStringValue(input);
                    break;

                case AmfValueType.Number:
                    this.writeNumberValue(input);
                    break;

                case AmfValueType.Boolean:
                    this.writeBooleanValue(input);
                    break;

                case AmfValueType.Xml:
                    this.writeXmlValue(input);
                    break;

                case AmfValueType.Null:
                    this.writeNullValue(input);
                    break;

                case AmfValueType.Undefined:
                    this.writeUndefinedValue(input);
                    break;

                case AmfValueType.Date:
                    this.writeDateValue(input);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private void writeDateValue(IAmfValue input)
        {


            this.writeObjectRemainOrValue(input, input.GetDate, this.writer_.WriteDate, this.writer_.WriteRemainDate);
            
        }

        private void writeStringValue(IAmfValue input)
        {

            var value = input.GetString();


            writeRemainOrValue(value, this.stringRemains_, () =>
            {
                this.writer_.WriteString(value);

            }, (remainIndex) =>
            {
                this.writer_.WirteRemainString(remainIndex);
            });

        }

        private void writeNumberValue(IAmfValue input)
        {
            var value = input.GetNumber();
            this.writer_.WriteNumber(value);
        }

        private void writeBooleanValue(IAmfValue input)
        {
            var value = input.GetBoolean();

            this.writer_.WriteBoolean(value);

        }

        private void writeXmlValue(IAmfValue input)
        {

            


            var amfValue = (AmfValue)input;
            var context = amfValue.GetXmlContext();

            writeRemainOrValue(input,this.objectRemains_, () =>
            {
                if (context.Newer)
                {
                    this.writer_.WriteXml(context.Document);
                }
                else
                {
                    this.writer_.WriteXmlDocument(context.Document);
                }

                this.objectRemains_.Add(input);

            }, (remainIndex) =>
            {
                if (context.Newer)
                {
                    this.writer_.WriteRemainXml(remainIndex);
                }
                else
                {
                    this.writer_.WriteRemainXmlDocument(remainIndex);
                }
            });


        }

        private void writeNullValue(IAmfValue input)
        {
            this.writer_.WriteNull();
        }

        private void writeUndefinedValue(IAmfValue input)
        {
            this.writer_.WriteUndefined();
        }

        private void remainString(string value)
        {

            this.stringRemains_.Add(value);
        }

        private void writeObjectRemainOrValue<T>(IAmfValue input,Func<T> getFunc, Action<T> valueAction, Action<uint> remainAction)
        {
            var value = getFunc();
            writeRemainOrValue(input, this.objectRemains_,()=>{
                valueAction(value);
            },remainAction);
        }

        private static void writeRemainOrValue<T>(T input,IList<T> remains ,Action valueAction, Action<uint> remainAction)
        {
            var remainIndex = remains.IndexOf(input);

            if (remainIndex < 0)
            {
                valueAction();
                remains.Add(input);
            }
            else
            {
                remainAction((uint)remainIndex);
            }
        }

        #endregion

        #region Static
        internal static IBuffer Sequencify( IAmfValue value)
        {
            var sequencer = new Amf3Sequencer(new DataWriter());
            return sequencer.sequencify(value);

            
        }
        #endregion
    }
}
