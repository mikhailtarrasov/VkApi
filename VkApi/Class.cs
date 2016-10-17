using System.Collections.Generic;

namespace VkApi
{
    public class Class
    {
        public static List<ContentPost> GetNews(VkUser user)
        {
            List<ContentPost> newsList = null;
            if (user.GetFriendsList() != null)
            {
                newsList = new List<ContentPost>();

                foreach (VkUser friend in user.GetFriendsList())
                {
                    List<ContentPost> friendWall = VkApi.GetWall(friend.id.ToString());
                    if (friendWall != null)
                    {
                        foreach (ContentPost post in friendWall)
                        {
                            newsList.Add(post);
                        }
                    }
                }
                return newsList;
            }
            return newsList;
        }
    }
}