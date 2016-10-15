using System.Runtime.Serialization;

namespace VkApi
{
    [DataContract]
    class ContentPost
    {
        [DataMember(Name = "id")]
        public int id;

        [DataMember(Name = "owner_id")]     /* идентификатор владельца стены, на которой размещена запись */
        public int owner_id;

        [DataMember(Name = "from_id")]      /* идентификатор автора записи */
        public int from_id;

        [DataMember(Name = "text")]         /* текст записи */
        public string text;

        [DataMember(Name = "comments")]
        public ContentPostReaction comments;

        [DataMember(Name = "likes")]
        public ContentPostReaction likes;

        [DataMember(Name = "reposts")]
        public ContentPostReaction reposts;
    }
}
