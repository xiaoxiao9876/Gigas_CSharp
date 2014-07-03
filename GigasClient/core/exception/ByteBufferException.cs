using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigasClient.exception
{
    class ByteBufferException:Exception
    {
        public ByteBufferException(string error):base(error) { 
           
        }
    }
}
