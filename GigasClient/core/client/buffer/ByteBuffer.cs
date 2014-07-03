using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigasClient.exception;

namespace GigasClient.core.client.buffer
{
    public class ByteBuffer
    {
        private int _readerIndex = 0;
        private int _writerIndex = 0;
        private byte[] _content;
        private int _capacity;
        private int _readerIndexMarker;
        private int _writerIndexMarker;
        private ByteBuffer(int capacity)
        {
            if (capacity < 0)
            {
                throw new ByteBufferException("capacity must greater than 0");
            }
            _content = new byte[capacity];
            this._capacity = capacity;
        }
        public static ByteBuffer allocBuffer(int capacity)
        {
            return new ByteBuffer(capacity);
        }
        public static ByteBuffer allocBuffer()
        {
            return new ByteBuffer(2048);
        }
        public int capacity()
        {
            return this._capacity;
        }
        public void capacity(int value)
        {
            needExpand(value);
        }
        public ByteBuffer markReaderIndex()
        {
            this._readerIndexMarker = _readerIndex;
            return this;
        }
        public ByteBuffer resetReaderIndex()
        {
            this._readerIndex = this._readerIndexMarker;
            return this;
        }
        public ByteBuffer markWriterIndex()
        {
            this._writerIndexMarker = this._writerIndex;
            return this;
        }
        public ByteBuffer resetWriterIndex()
        {
            this._writerIndex = this._writerIndexMarker;
            return this;
        }
        public int readableBytes()
        {
            return _writerIndex - _readerIndex;
        }
        public int writableBytes()
        {
            return this._capacity - this._writerIndex;
        }
        public void readIndex(int value)
        {
            this._readerIndex = value;
        }
        public int readIndex()
        {
            return this._readerIndex;
        }
        public bool isReadable()
        {
            return _writerIndex > _readerIndex;
        }

        public bool isReadable(int numBytes)
        {
            return _writerIndex - _readerIndex >= numBytes;
        }
        public bool isWritable()
        {
            return this._capacity > this._writerIndex;
        }

        public bool isWritable(int numBytes)
        {
            return this._capacity - _writerIndex >= numBytes;
        }
        public void discardReadBytes()
        {
            if (this._readerIndex > 0)
            {
                byte[] tempBytes = new byte[(this._capacity - this._readerIndex)];
                Array.Copy(this._content, this._readerIndex, tempBytes, 0, tempBytes.Length);
                this._readerIndex = 0;
                this._content = tempBytes;
                this._capacity = this._content.Length;
            }
        }


        //buffer handle

        public byte readByte()
        {
            canRead(1);
            return this._content[this._readerIndex++];
        }
        public void writeByte(byte value)
        {
            needExpand(1);
            this._content[this._writerIndex++] = value;
        }
        public void readBytes(byte[] bytes)
        {
            int length = bytes.Length;
            canRead(length);
            Array.Copy(this._content, this._readerIndex, bytes, 0, length);
            this._readerIndex += length;
        }
        public void writeBytes(byte[] bytes)
        {
            int length = bytes.Length;
            needExpand(length);
            Array.Copy(bytes, 0, this._content, this._readerIndex + 1, length);
            this._writerIndex += length;
        }
        public char readChar() { return 'a'; }
        public void writeChar(char value) { }
        public short readShort() { return 0; }
        public void writeShort(short value) { }
        public int readInt() { return 0; }
        public void writeInt(int value) { }
        public long readLong() { return 0; }
        public void writeLong(long value) { }
        public float readFloat() { return 0; }
        public void writeFloat(float value) { }
        public double readDouble() { return 0; }
        public void writeDouble(double value) { }
        //private 
        private bool canRead(int readNum)
        {
            if (this._readerIndex + readNum > this._writerIndex)
            {
                throw new ByteBufferException("can not read exception");
            }
            else
            {
                return true;
            }

        }
        private void needExpand(int numBytes)
        {
            if (isWritable())
            {
                return;
            }
            this._capacity += this._capacity >> 1;
            byte[] tempBytes = new byte[this._capacity];
            Array.Copy(this._content, tempBytes, this._content.Length);
            this._content = tempBytes;
        }
        private void needShrink()
        {
            if (this._capacity > this._writerIndex)
            {
                byte[] tempBytes = new byte[this._writerIndex + 1];
                Array.Copy(this._content, tempBytes, this._content.Length);
                this._content = tempBytes;
            }
        }

    }
}
