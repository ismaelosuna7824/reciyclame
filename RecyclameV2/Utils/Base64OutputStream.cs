using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Utils
{
    public class Base64OutputStream : Stream
    {
        private Base64.Coder coder;
        //private static readonly int BUFFER_SIZE = 2048;
        private int flags;
        Stream sout = null;
        private byte[] buffer = null;
        private int bpos = 0;
        private long count = 0;
        private long count2 = 0;
        private static byte[] EMPTY = new byte[0];

        public override bool CanRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanSeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanWrite
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /**
         * Performs Base64 encoding on the data written to the stream,
         * writing the encoded data to another OutputStream.
         *
         * @param out the OutputStream to write the encoded data to
         * @param flags bit flags for controlling the encoder; see the
         *        constants in {@link Base64}
         */
        public Base64OutputStream(Stream sout, int flags)
            : this(sout, flags, true)
        {

        }

        /**
         * Performs Base64 encoding or decoding on the data written to the
         * stream, writing the encoded/decoded data to another
         * OutputStream.
         *
         * @param out the OutputStream to write the encoded data to
         * @param flags bit flags for controlling the encoder; see the
         *        constants in {@link Base64}
         * @param encode true to encode, false to decode
         *
         * @hide
         */
        public Base64OutputStream(Stream sout, int flags, bool encode)
        {
            this.sout = sout;
            this.flags = flags;
            if (encode)
            {
                coder = new Base64.Encoder(flags, null);
            }
            else
            {
                coder = new Base64.Decoder(flags, null);
            }
        }

        public void write(int b)
        {
            // To avoid invoking the encoder/decoder routines for single
            // bytes, we buffer up calls to write(int) in an internal
            // byte array to transform them into writes of decently-sized
            // arrays.

            if (buffer == null)
            {
                buffer = new byte[1024];
            }
            if (bpos >= buffer.Length)
            {
                // internal buffer full; write it out.
                internalWrite(buffer, 0, bpos, false);
                bpos = 0;
            }
            buffer[bpos++] = (byte)b;
        }

        public void write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        /**
         * Flush any buffered data from calls to write(int).  Needed
         * before doing a write(byte[], int, int) or a close().
         */
        private void flushBuffer()
        {
            if (bpos > 0)
            {
                internalWrite(buffer, 0, bpos, false);
                bpos = 0;
            }
        }

        public override void Write(byte[] b, int off, int len)
        {
            if (len <= 0) return;
            flushBuffer();
            internalWrite(b, off, len, false);
        }

        public void close()
        {
            IOException thrown = null;
            try
            {
                flushBuffer();
                internalWrite(EMPTY, 0, 0, true);
            }
            catch (IOException e)
            {
                thrown = e;
            }

            try
            {
                if ((flags & Base64.NO_CLOSE) == 0)
                {
                    sout.Dispose();
                }
                else
                {
                    sout.Flush();
                }
            }
            catch (IOException e)
            {
                if (thrown != null)
                {
                    thrown = e;
                }
            }

            if (thrown != null)
            {
                throw thrown;
            }
        }

        /**
         * Write the given bytes to the encoder/decoder.
         *
         * @param finish true if this is the last batch of input, to cause
         *        encoder/decoder state to be finalized.
         */
        private void internalWrite(byte[] b, int off, int len, bool finish)
        {
            coder.output = embiggen(coder.output, coder.maxOutputSize(len));
            if (!coder.process(b, off, len, finish))
            {
                throw new Exception("bad base-64");
            }
            count += coder.op;
            sout.Write(coder.output, 0, coder.op);
        }

        public void getSize(byte[] b, int len)
        {
            count2 += coder.process(b, len);
        }

        public void resetCounters()
        {
            count = 0;
            count2 = 0;
        }
        /**
         * If b.length is at least len, return b.  Otherwise return a new
         * byte array of length len.
         */
        private byte[] embiggen(byte[] b, int len)
        {
            if (b == null || b.Length < len)
            {
                return new byte[len];
            }
            else
            {
                return b;
            }
        }

        public long getCount()
        {
            return count;
        }

        public long getCount2()
        {
            return count2;
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }


    }
}
