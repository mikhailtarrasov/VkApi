using System.Runtime.Serialization;

namespace VkApi
{
    [DataContract]
    public class ContentPostReaction
    {
        [DataMember(Name = "count")] 
        public int count;
    }
}
