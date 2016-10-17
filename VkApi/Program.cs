using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VkApi
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("https://oauth.vk.com/authorize?client_id=" + VkApi.client_id + "&display=mobile&redirect_uri=" + VkApi.redirect_uri + "&scope=" + VkApi.scope + "&response_type=token&v=" + VkApi.version);



            /*------------------------- OAuth авторизация -------------------------*/
            //Console.WriteLine("Сейчас откроется вкладка браузера, необходимо скопировать из строки браузера access_token, для продолжения нажмите любую клавишу..");
            //Console.ReadKey();

            //Process.Start("https://oauth.vk.com/authorize?client_id=" + VkApi.client_id + "&display=page&redirect_uri=" + VkApi.redirect_uri + "&scope=" + VkApi.scope + "&response_type=token&v=" + VkApi.version);

            //Console.WriteLine("Теперь вставляйте access_token: ");
            //VkApi.SetVkAccessToken(Console.ReadLine());
            /*---------------------------------------------------------------------*/

            VkApi.SetVkAccessToken("13a4f4cedd44593c2d511a5e693fd956b148ba7385b83d7e92cb36cdab4100b8331836ee2998bf01e9b4c");


            // Получение стены пользователя по идентификатору
            // List<ContentPost> mikhailtarrasov = VkApi.GetWall(59281711.ToString());

            //VkUser user = new VkUser(VkApi.GetUserByUsername("mikhailtarrasov").response.items[0]);

            Stopwatch timeGetMembersFriends = new Stopwatch();  /* Старт секундомера */
            timeGetMembersFriends.Start();                      /* составления графа */

            String groupName = "csu_iit";
            //List<VkUser> groupMembersList = VkApi.GetGroupMembersGraph(groupName);
            //groupMembersList[528].GetNews();

            VkUser user = VkApi.GetUserByUsername("maxim_kosenko");
            user.SetFriends(VkApi.GetFriends(user.id.ToString()));

            List<ContentPost> listNews = Class.GetNews(user);
            Console.WriteLine("Количество постов в списке: " + listNews.Count);


            // TODO sort ListNews  
            //groupMebersList[528].GetNews().Sort(delegate(VkApiResponse<ContentPost> x, VkApiResponse<ContentPost> y)
            //{
            //    return .CompareTo(y.PartName);
            //});

            /*---------------------------------------------------------------------*/
            timeGetMembersFriends.Stop();
            Console.WriteLine("Время работы: " + FormatTime(timeGetMembersFriends));
            /*---------------------------------------------------------------------*/


             

            Console.ReadKey();
        }

        public static string UTF8ToWin1251(string source)
        {

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = utf8.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            source = utf8.GetString(win1251Bytes);
            return source;

        }

        public static String FormatTime(Stopwatch time)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = time.Elapsed;

            // Format and display the TimeSpan value.
            return String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
        }
    }
}
