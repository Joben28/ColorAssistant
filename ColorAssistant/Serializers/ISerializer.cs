using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorAssistant.Serializers
{
    interface ISerializer<T>
    {
        void Serialize(T obj, string path);
        T Deserialize(string path);
    }
}
