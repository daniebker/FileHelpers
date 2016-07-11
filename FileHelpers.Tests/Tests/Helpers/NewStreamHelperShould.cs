
using System.IO;
using System.Text;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.Helpers
{
    [TestFixture]
    public class NewStreamHelperShould
    {
        [Test]
        public void not_correct_end()
        {
            NewStreamHelper newStreamHelper = new NewStreamHelper();

            Check.That(newStreamHelper
                        .CreateFileAppender(new MemoryStream(), Encoding.ASCII, false, true, 8)
                    .Encoding)
                .Equals(Encoding.ASCII);
        }
    }

    public class NewStreamHelper
    {
        public StreamWriter CreateFileAppender(MemoryStream memoryStream, Encoding encode, bool correctEnd, bool disposeStream, int bufferSize)
        {
            return new StreamWriter(memoryStream, encode, bufferSize);
        }
    }
}