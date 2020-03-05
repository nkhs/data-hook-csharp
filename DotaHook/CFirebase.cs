using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHook
{
    class CFireBase
    {
        public static string FIREBASE_KEY = "7D8dTbEZzaSUsWKbL376yqLJQwUXlZXpivKokrVC";//zzbb8855 gmail
        public static string FIREBASE_URL = "https://dota-item.firebaseio.com";

        //public async void AddLogAsync(String log)
        //{
        //    try
        //    {
        //        IFirebaseConfig config = new FirebaseConfig
        //        {
        //            AuthSecret = FIREBASE_KEY,
        //            BasePath = FIREBASE_URL
        //        };
        //        IFirebaseClient client = new FirebaseClient(config);

        //        PushResponse response = await client.PushAsync("bot_mail_log_", log);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        public static string MAIN_KEY = "stack";
        public static async void SetJSONConfigAsync(String log)
        {
            try
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = FIREBASE_KEY,
                    BasePath = FIREBASE_URL
                };
                IFirebaseClient client = new FirebaseClient(config);

                await client.SetAsync(MAIN_KEY + "/config/" + macAddress(), log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async void SetProjectStatusAsync(long projectId, String log)
        {
            try
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = FIREBASE_KEY,
                    BasePath = FIREBASE_URL
                };
                IFirebaseClient client = new FirebaseClient(config);

                await client.SetAsync(MAIN_KEY + "/project_progress/" + macAddress() + "/" + projectId, log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async void SetrojectViewNumberAsync(long projectId, int views)
        {
            try
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = FIREBASE_KEY,
                    BasePath = FIREBASE_URL
                };
                IFirebaseClient client = new FirebaseClient(config);

                await client.SetAsync(MAIN_KEY + "/project_views_current/" + macAddress() + "/" + projectId, "" + views);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public class ProjectReport
        {
            public long id;
            public string url;
            public int views;
            public long time;
            public string mac;

        }
      

        public static async void AddMyLogAsync(String key, String log)
        {
            try
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = FIREBASE_KEY,
                    BasePath = FIREBASE_URL
                };
                IFirebaseClient client = new FirebaseClient(config);

                PushResponse response = await client.PushAsync("dota/" + key, log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async void AddLogAsync(String log)
        {
            String key = macAddress();
            try
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = FIREBASE_KEY,
                    BasePath = FIREBASE_URL
                };
                IFirebaseClient client = new FirebaseClient(config);

                PushResponse response = await client.PushAsync("dota/" + key, log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string macAddress()
        {
            return System.Environment.MachineName;
        }
    }
}
