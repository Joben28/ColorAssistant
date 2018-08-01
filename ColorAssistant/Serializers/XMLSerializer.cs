using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace ColorAssistant.Serializers
{
    class XMLSerializer<T> : ISerializer<T>
    {
        private XmlSerializer _serializer;

        public XMLSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public void Serialize(T obj, string path)
        {
            try
            {
                using (TextWriter writer = new StreamWriter(path))
                {
                    _serializer.Serialize(writer, obj);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public T Deserialize(string path)
        {
            T obj = Activator.CreateInstance<T>();
            try
            {
                using (TextReader writer = new StreamReader(path))
                {
                    obj = (T)_serializer.Deserialize(writer);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return obj;
        }
    }
}
