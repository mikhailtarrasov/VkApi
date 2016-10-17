using System.Runtime.Serialization;

namespace VkApi
{
    [DataContract]
    public class VkApiResponse<T>
    {
        [DataMember(Name = "response")]
        public VkApiResponseContent<T> response;
    }
    
    [DataContract]
    public class VkApiResponse
    {
        [DataMember(Name = "response")]
        public ContentUser[] response;
    }
}