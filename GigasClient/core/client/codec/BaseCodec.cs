using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigasClient.core.client.codec
{
     interface BaseCodec
    {
         void decode();
         void encode();
    }
 }
