using System.Runtime.Serialization;

namespace VkApi
{
    [DataContract]
    class VkApiResponse<T>
    {
        [DataMember(Name = "response")]
        public VkApiResponseContent<T> response;
    }
}