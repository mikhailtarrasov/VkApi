using System.Runtime.Serialization;

namespace VkApi
{
    [DataContract]
    class ContentPostReaction
    {
        [DataMember(Name = "count")] 
        public int count;
    }
}
