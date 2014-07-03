using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigasClient.core;
using GigasClient.core.client.buffer;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Bootstrap test = new Bootstrap("127.0.0.1", 8888, true, 3, 10);
               // test.startConnect();
                byte[] bytes=new byte[]{1,2,3,4,5,6,7,8,9,10,11};
                ByteBuffer buffer=ByteBuffer.allocBuffer(10);
                Console.WriteLine(buffer.capacity());
                foreach(byte temp in bytes){
                    buffer.writeByte(temp);
                }
                Console.WriteLine(buffer.capacity());
                byte b=buffer.readByte();
                buffer.discardReadBytes();
                Console.WriteLine(buffer.capacity());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }
    }
}
