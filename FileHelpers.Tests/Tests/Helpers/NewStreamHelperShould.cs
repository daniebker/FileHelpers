
using System;
using System.IO;
using System.Text;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.Helpers
{
    [TestFixture]
    public class NewStreamHelperShould
    {
        private readonly string DOUBLE_NEW_LINE = Environment.NewLine + Environment.NewLine;

        [Test]
        public void not_correct_end()
        {
            NewStreamHelper newStreamHelper = new NewStreamHelper();

            var memoryStream = MemoryStreamBuilder.AMemoryStream()
                .withContent(DOUBLE_NEW_LINE).build();
            
            var streamWriter = newStreamHelper
                .CreateFileAppender(memoryStream, Encoding.Default, false, true, 8);

            Check.That(streamWriter.BaseStream.Length).Not.Equals((long)0);
        }

        [Test]
        public void do_nothing_when_stream_is_empty()
        {
            NewStreamHelper newStreamHelper = new NewStreamHelper();

            var memoryStream = MemoryStreamBuilder.AMemoryStream()
                .withContent(string.Empty).build();

            var streamWriter = newStreamHelper
                .CreateFileAppender(memoryStream, Encoding.Default, true, true, 8);

            Check.That(streamWriter.BaseStream.Length).Equals((long)0);
        }

        [Test]
        public void remove_all_content_if_old_content_is_line_endings()
        {
            NewStreamHelper newStreamHelper = new NewStreamHelper();

            var memoryStream = MemoryStreamBuilder.AMemoryStream()
                .withContent(DOUBLE_NEW_LINE).build();

            var streamWriter = newStreamHelper
               .CreateFileAppender(memoryStream, Encoding.Default, false, true, 8);

            Check.That(streamWriter.BaseStream.Length).Equals((long)0);
        }
        
        private class MemoryStreamBuilder
        {
            private string _streamContent;
            private Encoding _encoding = Encoding.Default;

            public static MemoryStreamBuilder AMemoryStream()
            {
                return new MemoryStreamBuilder();
            }

            public MemoryStreamBuilder withEncoding(Encoding encoding)
            {
                _encoding = encoding;

                return this;
            }

            public MemoryStreamBuilder withContent(string content)
            {
                _streamContent = content;

                return this;
            }

            public MemoryStream build()
            {
                return new MemoryStream(_encoding.GetBytes(_streamContent));
            }
        }
    }

    public class NewStreamHelper
    {
        public StreamWriter CreateFileAppender(MemoryStream memoryStream, Encoding encode, bool correctEnd, bool disposeStream, int bufferSize)
        {
            if (!correctEnd)
            {
                return new StreamWriter(memoryStream, encode, bufferSize);
            }

            for(long offset = memoryStream.Length; offset > 0;)
            {
                
            }
            
        }
    }
}