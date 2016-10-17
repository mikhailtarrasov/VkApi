using System.Collections.Generic;

namespace VkApi
{
    public class VkUser : ContentUser
    {
        private List<VkUser> friends;

        public VkUser(ContentUser contentUser)
        {   
            id = contentUser.id;
            first_name = contentUser.first_name;
            last_name = contentUser.last_name;
            photo_50 = contentUser.photo_50;
            friends = null;
        }

        public void AddFriend(ContentUser friend)
        {
            VkUser newFriend = new VkUser(friend);
            friends.Add(newFriend);
        }

        public void SetFriends(VkApiResponse<ContentUser> friendsResp)
        {
            if (friendsResp.response != null)
            {
                friends = new List<VkUser>();

                foreach (ContentUser friend in friendsResp.response.items)
                {
                    // TODO insert into DB (INSERT IGNORE) 
                    AddFriend(friend);
                }
            }
        }

        public List<VkUser> GetFriendsList()
        {
            return friends;
        }
    }
}
