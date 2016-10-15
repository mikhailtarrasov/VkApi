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

            VkApi.SetVkAccessToken("b612f72eeee25f23c7d3ecdfcfd0d56ac2ec111f2dfbc505518b97d30416b104f83d9e68c6c2962464465");


            // Получение стены пользователя по идентификатору
            //List<ContentPost> mikhailtarrasov = VkApi.GetWall(59281711.ToString());


            Stopwatch timeGetMembersFriends = new Stopwatch();  /* Старт секундомера */
            timeGetMembersFriends.Start();                      /* составления графа */

            String groupName = "csu_iit";
            List<VkUser> groupMembersList = VkApi.GetGroupMembersGraph(groupName);
            groupMembersList[528].GetNews();

            // TODO sort ListNews  
            //groupMebersList[528].GetNews().Sort(delegate(VkApiResponse<ContentPost> x, VkApiResponse<ContentPost> y)
            //{
            //    return .CompareTo(y.PartName);
            //});

            /*---------------------------------------------------------------------*/
            timeGetMembersFriends.Stop();
            Console.WriteLine("RunTime " + FormatTime(timeGetMembersFriends));
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
