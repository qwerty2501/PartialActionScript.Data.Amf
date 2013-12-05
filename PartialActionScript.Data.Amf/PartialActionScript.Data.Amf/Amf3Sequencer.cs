using System;
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
            this.objectRemains_ = new List<object>();
            this.stringRemains_ = new List<string>();
            this.writer_ = new Amf3Writer(writer);
        }


        #endregion

        #region Private

        private List<object> objectRemains_;

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
                    writeStringValue(input);
                    break;
                case AmfValueType.Number:
                    writeNumberValue(input);
                    break;

                case AmfValueType.Boolean:
                    writeBooleanValue(input);
                    break;

                case AmfValueType.Xml:
                    writeXmlValue(input);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private void writeStringValue(IAmfValue input)
        {

            var value = input.GetString();

            

            var remainIndex = this.stringRemains_.IndexOf(value);

            if (remainIndex < 0)
            {

                this.writer_.WriteString(value);
                this.remainString(value);

            }
            else
            {
                this.writer_.WirteRemainString((uint)remainIndex);
            }

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

            var remainIndex = this.objectRemains_.IndexOf(context);

            if (remainIndex < 0)
            {
                if (context.Newer)
                {
                    this.writer_.WriteXml(context.Document);
                }
                else
                {
                    this.writer_.WriteXmlDocument(context.Document);
                }
            }
            else
            {
                if (context.Newer)
                {
                    this.writer_.WriteRemainXml((uint)remainIndex);
                }
                else
                {
                    this.writer_.WriteRemainXmlDocument((uint)remainIndex);
                }
            }

        }


        private void remainString(string value)
        {
            if (this.stringRemains_.Count > UInt29.MaxRemainingValue)
                throw ExceptionHelper.CreateOutOfStringRemainLengthException();

            this.stringRemains_.Add(value);
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
