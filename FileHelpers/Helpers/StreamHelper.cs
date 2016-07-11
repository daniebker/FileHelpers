using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileHelpers
{
    internal enum LineEnd
    {
        Dos,
        Unix,
        Macintosh,
        Other
    }


    internal static class StreamHelper
    {


        /// <summary>
        /// open a stream with optional trim extra blank lines
        /// </summary>
        /// <param name="fileName">Filename to open</param>
        /// <param name="encode">encoding of the file</param>
        /// <param name="correctEnd">do we trim blank lines from end?</param>
        /// <param name="disposeStream">do we close stream after trimming</param>
        /// <param name="bufferSize">Buffer size to read</param>
        /// <returns>TextWriter ready to write to</returns>
        internal static TextWriter CreateFileAppender(string fileName,
            Encoding encode,
            bool correctEnd,
            bool disposeStream,
            int bufferSize)
        {
            TextWriter res;

            if (correctEnd) {
                FileStream fs = null;

                try {
                    fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    
                    // read the file backwards using SeekOrigin.Begin...
                    long offset;
                    for (offset = fs.Length - 1; offset >= 0; offset--) {
                        fs.Seek(offset, SeekOrigin.Begin);
                        int value = fs.ReadByte();
                        if (value == '\r') {
                            //CarriageReturn = true;
                        }
                        else if (value == '\n') {
                            //LineFeed = true;
                        }
                        else
                            break;
                    }
                    if (offset >= 0) // read something else other than line ends...
                    {
                        var newline = new byte[StringHelper.NewLine.Length];
                        int count = 0;
                        foreach (var ch in StringHelper.NewLine) {
                            newline[count] = Convert.ToByte(ch);
                            count++;
                        }

                        fs.Write(newline, 0, count);
                    }
                    res = new StreamWriter(fs, encode, bufferSize);
                }
                finally {
                    if (disposeStream && fs != null)
                        fs.Close();
                }
            }
            else
                res = new StreamWriter(fileName, true, encode, bufferSize);
            return res;
        }
    }
}