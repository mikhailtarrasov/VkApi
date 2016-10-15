using System.Runtime.Serialization;

namespace VkApi
{
    [DataContract]
    class VkApiResponseContent<T>
    {
        [DataMember(Name = "count")]
        public int count;

        [DataMember(Name = "items")]
        public T[] items;        
    }
}
