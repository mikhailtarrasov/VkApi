using System.Runtime.Serialization;

namespace VkApi
{
    [DataContract]
    class ContentUser
    {
        [DataMember(Name = "id")]
        public int id;

        [DataMember(Name = "first_name")]
        public string first_name;

        [DataMember(Name = "last_name")]
        public string last_name;

        [DataMember(Name = "photo_50")]
        public string photo_50;
    }
}