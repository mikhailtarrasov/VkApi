using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

//using System.Threading.Tasks;


/* Как вариант сделать, чтобы обертки на вкшные функции не строку вызывали, а в ретурне вызывали SendRequest*/


namespace VkApi
{
    class VkApi
    {
        public const int client_id = 5658746;
        public const string redirect_uri = "https://oauth.vk.com/blank.html";
        public const string scope = "wall,friends";
        public const string clientSecret = "RXPswbMZNISw6pVpp56H";
        public const string version = "5.57";

        private static string vkAccessToken;

        public static void SetVkAccessToken(string token)
        {
            vkAccessToken = token;
        }

        private static string SendRequest(string method_name, Hashtable parameters)
        {
            WebClient webClient = new WebClient();

            foreach (DictionaryEntry param in parameters)
            {
                webClient.QueryString.Add(param.Key.ToString(), param.Value.ToString());
            }
            webClient.QueryString.Add("v", version);
            webClient.QueryString.Add("access_token", vkAccessToken);
            return webClient.DownloadString("https://api.vk.com/method/" + method_name);
        }
        
        private static T ParseResponse<T>(string jsonResponse)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonResponse)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

        private static VkApiResponse<ContentUser> GetGroupsMembers(String groupId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("group_id", groupId);
            parameters.Add("fields", "lists, photo_50");
            String json = SendRequest("groups.getMembers", parameters);
            json = Program.UTF8ToWin1251(json);
            return ParseResponse<VkApiResponse<ContentUser>>(json);
        }

        private static VkApiResponse<ContentUser> GetFriends(String userId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("user_id", userId);
            parameters.Add("fields", "lists, photo_50");
            String json = SendRequest("friends.get", parameters);
            json = Program.UTF8ToWin1251(json);
            return ParseResponse<VkApiResponse<ContentUser>>(json);
        }

        private static VkApiResponse<ContentPost> Get100Posts(String ownerId, int offset, int count)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("owner_id", ownerId);
            parameters.Add("offset", offset);
            parameters.Add("count", count);
            parameters.Add("filter", "owner");
            String json = SendRequest("wall.get", parameters);
            json = Program.UTF8ToWin1251(json);
            return ParseResponse<VkApiResponse<ContentPost>>(json);
        }

        public static List<ContentPost> GetWall(String ownerId)
        {
            int offset = 0;
            int count = 100;
            List<ContentPost> postsList = null;

            VkApiResponse<ContentPost> resp = Get100Posts(ownerId, offset, count);

            if (resp.response == null || resp.response.count == 0)
            {
                return postsList;
            }
            else
            {
                postsList = new List<ContentPost>(resp.response.count);

                int i = resp.response.count;
                while (i > 0)
                {
                    foreach (ContentPost post in resp.response.items)
                    {
                        postsList.Add(post);
                    }

                    if (i > 100)
                    {
                        offset += 100;
                        resp = Get100Posts(ownerId, offset, count);
                    }
                    i -= 100;
                }

                return postsList;
            }
        }

        public static List<VkUser> GetGroupMembersGraph(String groupName)
        {
            /*-------------------- Получение участников группы --------------------*/
            VkApiResponse<ContentUser> resp = GetGroupsMembers(groupName);
            /*---------------------------------------------------------------------*/

            List<VkUser> groupMembersGraph = new List<VkUser>(resp.response.count);
            int i = 0;

            /*---------------------- Получение друзей членов группы ---------------*/
            foreach (ContentUser user in resp.response.items)
            {
                groupMembersGraph.Insert(i, new VkUser(user));
                groupMembersGraph[i].SetFriends(GetFriends(user.id.ToString()));
               
                i++;
                Thread.Sleep(220);
            }
            /*---------------------------------------------------------------------*/

            return groupMembersGraph;
        }
    }
}
