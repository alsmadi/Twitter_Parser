using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections;

//using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Controllers;
using Tweetinvi.Core.Interfaces.DTO;
using Tweetinvi.Core.Interfaces.Factories;
using Tweetinvi.Core.Interfaces.Models;
using MyHashTags.Lib;
using LinqToTwitter;
using TwitterScraper;

namespace TwitterScraper
{
    class Program
    {
        static MyHashTags.Lib.MyHashTagsClass cut1;
        public static void Init()
        {
            cut1 = new MyHashTags.Lib.MyHashTagsClass("1976215495YyXTEpyECgiwOsL7BCoDAohVYpwGPPAzcyMlnkT",
                "QhwPS2HYkiV2vznZ0yULm2z1x5g4tUctM2JItLH3MEC3h",
                "UyK8wQUDghgW5cnZFFXOQQ",
                "OLepmuMMvnHG6sXLdWYLObwskOBJDCLOUUczgeguEs");
        }
        public static Dictionary<string, Dictionary<string, object>> twitts;
        public static Dictionary<string, Dictionary<string, object>> follows;
        static StreamWriter sw10;
        static StreamWriter sw2;
        static Hashtable friendsHash;
        static Hashtable followersHash;
        static List<string> friendsHash1;
        static List<TwitterScraper.Follow> followersHash1;

        static void CreateFriends(List<string> usernames)
        {
            foreach (var userName in usernames)
            {
                if (friendsHash1.Contains(userName) == false)
                {
                    friendsHash1.Add(userName);
                }
                friends1 = cut1.GetUsers10(userName, FriendshipType.FriendsList);
                foreach (string fr1 in friends1)
                {
                    if (friendsHash1.Contains(fr1) == false)
                    {
                        friendsHash1.Add(fr1);
                    }

                }
            }
        }
        static List<string> friends1;
        static List<string> friends2;
        private static List<string> followersHash2;
        static StreamWriter sw3;

        static Hashtable makeHashes()
        {
            foll = new Hashtable();
            string inputfile1 = "names_2016_2.txt";
            StreamReader sr = new StreamReader(inputfile1);
            //  Settings.Instance.MakeNewIeInstanceVisible = false;
            //  using (var browser = new IE("http://twiangulate.com/search/"))
            //      Browser browser = new WatiN.Core.IE("http://twiangulate.com/search");
            string line = null;
         //   Hashtable foll = new Hashtable();
            int count = 0;
            string temp = null;
            while ((line = sr.ReadLine()) != null)
            {

                string[] temp1 = line.Split(',');
                /*        if (count < 1)
                        {
                            temp = line;
                            count++;
                            continue;
                        }
                        else
                        { */
                Follow fill = new Follow(temp1[0], temp1[1]);
                if (foll.ContainsKey(fill) == false)
                {
                    foll.Add(fill, fill);
                }
                temp = line;
                //  }
            }
            return foll;
        }

        static Hashtable foll;
        [STAThread]
        static void Main3(string[] args)
        {
            //    collectFollowers();

            TwianGulate tw = new TwianGulate();
          //  string outputfile1 = "names_2016_2.txt";
            
           
           

                makeHashes();
            again:
            try
            {

                tw.GetTwitter1(foll);
            }
            catch(Exception ex)
            {
                goto again;
            }


        }
        static string outputfile = "names_2016_2.csv";
        static void collectFollowers()
        {
            outputfile = "names_2016.csv";
            StreamWriter newNames = new StreamWriter(outputfile);

            var twitter = new Twitter1
            {
                OAuthConsumerKey = "vOIB6Pmz3B75VypW8n35L8EgZ",
                OAuthConsumerSecret = "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ"

                //  AccessToken = "48120535-ImAePJp1yHpg1GQopv5vJMLP4KQPOgJg7tHsTzSBx",
                // AccessTokenSecret = "Alqr0jc4FtNFnxVI3XPMZX51YLlUIIibjxjwXUxJTjMHd",
                // ConsumerKey = "vOIB6Pmz3B75VypW8n35L8EgZ",
                // ConsumerSecret = "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ"
            };



            Init();
            //Credentials - Get your own credentials from http://dev.twitter.com
            Twitter.SetCredentials(accessToken: "48120535-ImAePJp1yHpg1GQopv5vJMLP4KQPOgJg7tHsTzSBx", accessTokenSectet: "Alqr0jc4FtNFnxVI3XPMZX51YLlUIIibjxjwXUxJTjMHd",
                                   consumerKey: "vOIB6Pmz3B75VypW8n35L8EgZ", consumerSecret: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ");

            Console.WriteLine("Credentials ready!");
            followersHash1 = new List<Follow>();
            var usernames = FileManagement.GetUsernames();

            //   string userName = "ialsmadi";
            foreach (var userName in usernames)
            {
                try
                {
                 //   newNames.WriteLine(userName);
                    /*   if (followersHash1.Contains(userName) == false)
                       {
                           followersHash1.Add(userName);
                       } */
                    List<string> followers1 = new List<string>();
                    try
                    {
                        followers1 = twitter.GetUsers1(userName, FriendshipType.FollowersList).Result;
                        foreach(string foll1 in followers1)
                        {
                            newNames.WriteLine(userName+","+foll1);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("Rate limit"))
                        {
                            sw2.Flush();
                            int k = 15 * 60 * 1000;
                            System.Threading.Thread.Sleep(k);
                            continue;
                            // goto start1;
                        }
                    }
                }
                catch (System.AggregateException ex){
                    sw2.Flush();
                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    continue;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("Rate limit"))
                    {
                        sw2.Flush();
                        int k = 15 * 60 * 1000;
                        System.Threading.Thread.Sleep(k);
                        continue;
                        // goto start1;
                    }
                }
            }

            newNames.Close();
           // return outputfile;
        }
        static void Main2(string[] args)
        {
            sw2 = new StreamWriter("Followers.csv");
            sw2.WriteLine("user1.UserID" + "," + "user1.Name" + "," + "node1" + "," + "user2.UserID" + "," + "user2.Name" + "," + "node2");
            sw3 = new StreamWriter("FollowersMain.csv");
            sw3.WriteLine("user1.UserID" + "," + "user1.Name" + "," + "node1" + "," + "user1.Count" + ","
                       + "user1.DefaultProfile" + "," + "user1.FavoritesCount" + "," + "user1.FollowersCount" + "," +
                       "user1.Following" + "," + "user1.FollowRequestSent" + "," + "user1.FriendsCount" + "," +
                       "user1.LangResponse" + "," + "user1.Lang" + "," + "user1.ListedCount" + "," + "user1.Location" + ","
                       + "user1.Notifications" + "," + "user1.ScreenName" + "," + "user1.ScreenNameResponse" + "," + "user1.Status" + "," +
                       "user1.Status" + "," + "user1.StatusesCount" + "," + "user1.Type" + "," + "user1.Url" + "," +
                       "user1.UserIdList" + "," + "user1.Verified" + "," +

                       "user2.UserID" + "," + "user2.Name" + "," + "node2" +
                        "user2.Count" + "," + "user2.DefaultProfile" + "," + "user2.FavoritesCount" + "," + "user2.FollowersCount" + "," +
                       "user2.Following" + "," + "user2.FollowRequestSent" + "," + "user2.FriendsCount" + "," +
                       "user2.LangResponse" + "," + "user2.Lang" + "," + "user2.ListedCount" + "," + "user2.Location" + ","
                       + "user2.Notifications" + "," + "user2.ScreenName" + "," + "user2.ScreenNameResponse" + "," + "user2.Status" + "," +
                       "user2.Status" + "," + "user1.StatusesCount" + "," + "user2.Type" + "," + "user2.Url" + "," +
                       "user2.UserIdList" + "," + "user2.Verified"
                       );

            var twitter = new Twitter1
            {
                OAuthConsumerKey = "vOIB6Pmz3B75VypW8n35L8EgZ",
                OAuthConsumerSecret = "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ"
            };



            Init();
            //Credentials - Get your own credentials from http://dev.twitter.com
            Twitter.SetCredentials(accessToken: "vOIB6Pmz3B75VypW8n35L8EgZ", accessTokenSectet: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ",
                                   consumerKey: "vOIB6Pmz3B75VypW8n35L8EgZ", consumerSecret: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ");

            Console.WriteLine("Credentials ready!");
            followersHash1 = new List<Follow>();
            var usernames = FileManagement.GetUsernames();

            //   string userName = "ialsmadi";
            foreach (var userName in usernames)
            {
                try
                {

                    /*   if (followersHash1.Contains(userName) == false)
                       {
                           followersHash1.Add(userName);
                       } */
                    List<string> followers1 = new List<string>();
                    try
                    {
                        followers1 = twitter.GetUsers1(userName, FriendshipType.FollowersList).Result;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("Rate limit"))
                        {
                            sw2.Flush();
                            int k = 15 * 60 * 1000;
                            System.Threading.Thread.Sleep(k);
                            continue;
                            // goto start1;
                        }
                    }
                    foreach (string foll1 in followers1)
                    {

                        Follow follow1 = new Follow(userName, foll1);

                        if (followersHash1.Contains(follow1) == false)
                        {
                            followersHash1.Add(follow1);
                        }



                    }
                }
                //  }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("Rate limit"))
                    {
                        sw2.Flush();
                        int k = 15 * 60 * 1000;
                        System.Threading.Thread.Sleep(k);
                        continue;
                        // goto start1;
                    }
                }

            }

            foreach (Follow fol1 in followersHash1)
            {
                try
                {
                    string node1 = fol1.getNode1();
                    User user1 = twitter.GetInfo(node1).Result;
                    string node2 = fol1.getNode2();
                    User user2 = twitter.GetInfo(node2).Result;
                    sw2.WriteLine(user1.UserID + "," + user1.UserIDResponse+ "," + user1.Name + "," + node1 + "," + user2.UserID + "," + user2.UserIDResponse+ "," + user2.Name + "," + node2);
                    sw3.WriteLine(user1.UserID + ","+user1.UserIDResponse + "," + user1.Name + "," + node1 + "," + user1.Count + ","
                        + user1.DefaultProfile + "," + user1.FavoritesCount + "," + user1.FollowersCount + "," +
                        user1.Following + "," + user1.FollowRequestSent + "," + user1.FriendsCount + "," +
                        user1.LangResponse + "," + user1.Lang + "," + user1.ListedCount + "," + user1.Location + ","
                        + user1.Notifications + "," + user1.ScreenName + "," + user1.ScreenNameResponse + "," + user1.Status + "," +
                        user1.Status + "," + user1.StatusesCount + "," + user1.Type + "," + user1.Url + "," +
                        user1.UserIdList + "," + user1.Verified + "," +

                        user2.UserID + "," + user2.UserIDResponse + "," + user2.Name + "," + node2 +
                         user2.Count + "," + user2.DefaultProfile + "," + user2.FavoritesCount + "," + user2.FollowersCount + "," +
                        user2.Following + "," + user2.FollowRequestSent + "," + user2.FriendsCount + "," +
                        user2.LangResponse + "," + user2.Lang + "," + user2.ListedCount + "," + user2.Location + ","
                        + user2.Notifications + "," + user2.ScreenName + "," + user2.ScreenNameResponse + "," + user2.Status + "," +
                        user2.Status + "," + user1.StatusesCount + "," + user2.Type + "," + user2.Url + "," +
                        user2.UserIdList + "," + user2.Verified
                        );
                }
                //  }
                catch(System.AggregateException ex1)
                {
                    sw2.Flush();
                    sw3.Flush();
                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    continue;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("Rate limit"))
                    {
                        sw2.Flush();
                        int k = 15 * 60 * 1000;
                        System.Threading.Thread.Sleep(k);
                        continue;
                        // goto start1;
                    }
                
                sw2.Flush();
                sw3.Flush();
            }
        }

            sw2.Close();
            sw3.Close();
        }

        static int countOccurences(string needle, string haystack)
        {
            return (haystack.Length - haystack.Replace(needle, "").Length) / needle.Length;
        }
        static void Main5(string[] args)
        {
            sw10 = new StreamWriter("Nodes.csv");
            StreamWriter swHash = File.AppendText("hashtages.csv");
            StreamWriter swCoord = File.AppendText("coordinates.csv");
            StreamWriter swURLS = File.AppendText("urls.csv");
            StreamWriter swCONT = File.AppendText("contributors.csv");
            StreamWriter swMention = File.AppendText("mentions.csv");
            StreamWriter swSymb = File.AppendText("symbols.csv");
            StreamWriter swLang = File.AppendText("langs.csv");
            StreamWriter swUser = File.AppendText("Users.csv");
            StreamWriter swMainUser = File.AppendText("MainUser.csv");
            StreamWriter swRep = File.AppendText("TweetsReplies.csv");
            StreamWriter swFriend = File.AppendText("Friends.csv");
            swMainUser.WriteLine("userName" + "," + "user.Count" + ","
                           + "user.Categories.Count" +
                           "user.FavoritesCount" + "," + "user.FollowersCount" + ","
                           + "user.FriendsCount" + "," + "user.ListedCount" + "," +
                           "user.Location" + "," + "user.Name" + "," + "user.Notifications" + ","
                           + "user.Page" + "," + "user.ScreenName" + "," +
                           "user.ScreenNameList" + "," + "user.ScreenNameResponse" + ","
                           + "user.Slug" + "," + "user.Status" + "," + "user.StatusesCount" + "," +
                           "user.TimeZone" + "," + "user.Type" + "," + "user.Url" + "," + "user.UserID" + "," +
                           "user.UserIdList" + "," + "user.UserIDResponse" + "," + "user.UtcOffset" + "," +
                           "user.Verified");

            sw2 = new StreamWriter("Followers.csv");
            friendsHash = new Hashtable();
            followersHash = new Hashtable();
            friendsHash1 = new List<string>();
            followersHash2 = new List<string>();
            friends1= new List<string>();
           var twitter = new Twitter1
            {
                OAuthConsumerKey = "vOIB6Pmz3B75VypW8n35L8EgZ",
                OAuthConsumerSecret = "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ"
           }; 

           

            Init();
            //Credentials - Get your own credentials from http://dev.twitter.com
            Twitter.SetCredentials(accessToken: "vOIB6Pmz3B75VypW8n35L8EgZ", accessTokenSectet: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ",
                                   consumerKey: "vOIB6Pmz3B75VypW8n35L8EgZ", consumerSecret: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ");

            Console.WriteLine("Credentials ready!");

            //Initial Variables
            var startDate = DateTime.Now;

            //Start program
            while (true)
            {
                //Read Twitter Users List
                //   var usernames = FileManagement.GetUsernames();
                var usernames = FileManagement.GetNodes();
             //   var nextUpdateTime = FileManagement.GetNextUpdateTime();
                var nextUpdateTime1 = FileManagement.GetNextUpdateTime1();
                // var twitter;

                //    CreateFriends(usernames);
                /*       foreach (var userName in usernames)
                       {
                           if (followersHash1.Contains(userName) == false)
                           {
                               followersHash1.Add(userName);
                           }
                           List<string> followers1 = cut.GetUsers1(userName, FriendshipType.FollowersList);

                           foreach (string foll1 in followers1)
                           {
                               if (followersHash1.Contains(foll1) == false)
                               {
                                   followersHash1.Add(foll1);
                               }

                           }
                       } */
                foreach (Friend userName in usernames.Keys)
              //  foreach (var userName in friendsHash1)
                {
                    if (userName.getNode1().Length < 2)
                    {
                        continue;
                    }
                    swMainUser.Flush();
                    swRep.Flush();

                   //  again;
                   //  var twitter = new Twitter1();

                    try
                    {
                        User user = twitter.GetInfo(userName.getNode1()).Result;
                        swMainUser.WriteLine(userName + "," + user.Count + ","
                           + user.Categories.Count +
                           user.FavoritesCount + "," + user.FollowersCount + ","
                           + user.FriendsCount + "," + user.ListedCount + "," +
                           user.Location + "," + user.Name + "," + user.Notifications + ","
                           + user.Page + "," + user.ScreenName + "," +
                           user.ScreenNameList + "," + user.ScreenNameResponse + ","
                           + user.Slug + "," + user.Status + "," + user.StatusesCount + "," +
                           user.TimeZone + "," + user.Type + "," + user.Url + "," + user.UserID + "," +
                           user.UserIdList + "," + user.UserIDResponse + "," + user.UtcOffset + "," +
                           user.Verified);

                  /*      System.IO.File.WriteAllText(@"MainUser.csv", userName + "," + user.Count + ","
                           + user.Categories.Count +
                           user.FavoritesCount + "," + user.FollowersCount + ","
                           + user.FriendsCount + "," + user.ListedCount + "," +
                           user.Location + "," + user.Name + "," + user.Notifications + ","
                           + user.Page + "," + user.ScreenName + "," +
                           user.ScreenNameList + "," + user.ScreenNameResponse + ","
                           + user.Slug + "," + user.Status + "," + user.StatusesCount + "," +
                           user.TimeZone + "," + user.Type + "," + user.Url + "," + user.UserID + "," +
                           user.UserIdList + "," + user.UserIDResponse + "," + user.UtcOffset + "," +
                           user.Verified); */
                    }
                    catch(Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("Rate limit"))
                        {
                            swMainUser.Flush();
                            int k = 15 * 60 * 1000;
                            System.Threading.Thread.Sleep(k);
                        }
                        continue;
                       // goto again;
                    }
                    try
                    {
                      //   friends1 = twitter.GetUsers1(userName.getNode1(), FriendshipType.FriendsList).Result;

                        twitts = twitter.GetTwitts(userName.getNode1(), 100).Result;
                    }
                    catch (Exception ex1)
                    {
                        if (ex1.InnerException.Message.Contains("Rate limit"))
                        {
                            int k = 15 * 60 * 1000;
                            System.Threading.Thread.Sleep(k);
                        }
                            continue;
                            //goto again;

                        
                    }

                    String totalText = "";

                //    follows = twitter.GetFollowers(userName, 100).Result;
                    // IEnumerable<string> twitts = twitter.GetTwitts(userName, 100).Result;


                    //   foreach (var t in twitts)
                    // {
                    //   Console.WriteLine(t + "\n");
                    // }
                    //Directory to save files
                    var file = FileManagement.GetFilename(userName.getNode1());

                    StreamWriter sw= File.AppendText(file); 
                  /*  if (!File.Exists(file))
                    {
                        using (sw = File.CreateText(file)) ; }
                    else
                    { */
                  //      sw = File.AppendText(file);
                    //}
                    //sw.o
                        sw.WriteLine("UserName"+"$"+"Date" + "$" + "ID" + "$" + "ID_str" + "$" + "text" + "$" + "source" + "$" + "truncated" + "$" + "in_reply_to_status_id" + "$" +
                        "in_reply_to_status_id_str" + "$" + "in_reply_to_user_id" + "$" + "in_reply_to_user_id_str" + "$" +
                        "in_reply_to_screen_name" + "$" + "users" + "$" + "geo" + "$" + "coordinates" + "$" + "place" + "$" + "contributors" + "$" + "is_quote_status" +
                        "retweet_count" + "$" + "favourite count" + "$" + "entities" + "$" + "favourited" + "$" + "retweeted" + "$" + "poss_sensitive" + "$" + "lang");

                    swRep.WriteLine("UserName" + "$" + "ID" + "$" + "in_reply_to_status_id" + "$" +
                        "in_reply_to_user_id" + "$" + "in_reply_to_user_id_str" + "$" +
                       "in_reply_to_screen_name");
                    if (twitts!=null && twitts.Count() > 0)
                    {
                        int twCount = 0;
                        String tempFriends = "";
                      //  foreach (var t in twitts)
                            foreach (KeyValuePair<string,Dictionary<string,object>> t in twitts)
                            {
                          //  tempFriends = "";
                            swRep.WriteLine(userName + "$");
                            sw.WriteLine(userName+"$"+twCount+"$");
                            swHash.WriteLine(userName + "$" + twCount + "$");
                            swCONT.WriteLine(userName + "$" + twCount + "$");
                            swCoord.WriteLine(userName + "$" + twCount + "$");
                            swURLS.WriteLine(userName + "$" + twCount + "$");
                            swMention.WriteLine(userName + "$" + twCount + "$");
                            swSymb.WriteLine(userName + "$" + twCount + "$");
                            swLang.WriteLine(userName + "$" + twCount + "$");
                            swUser.WriteLine(userName + "$" + twCount + "$");
                            //swMainUser.WriteLine(userName + "$" + twCount + "$");
                            twCount++;
                            Dictionary<string, object> tw2 = t.Value;
                           // Dictionary<string, object> tw20 = t;
                            foreach (var abc in tw2.Values) 
                            {
                               
                                int k = 9;
                                try {
                                    Dictionary<string, object> selectedCollection = (Dictionary<string, object>)abc;
                                    foreach (KeyValuePair<string, object> s in selectedCollection)
                                    {
                                        try
                                        {
                                            if (s.Key.Contains("ID"))
                                            {
                                                swRep.Write(s.Value +"$");
                                                tempFriends += s.Value;
                                            }

                                            if (s.Key.Contains("in_reply_to"))
                                            {
                                                swRep.Write(s.Value + "$");
                                                tempFriends += s.Value;
                                            }

                                          
                                            

                                          

                                            if (s.Key.ToString() == "coordinates")
                                            {
                                                sw.Write("coordinates" + "$");
                                                try
                                                {
                                                    var coor = s.Value;
                                                    System.Object[] selectedCollection6 = (System.Object[])coor;
                                                    foreach (object ents1 in selectedCollection6)
                                                    {
                                                        var ents2 = ents1;
                                                        Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                        foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                        {

                                                            //int k1 = 9;
                                                            string t3 = s2.Value.ToString();
                                                            totalText += t3 + "#";
                                                            sw.Write(s2.Value.ToString() + "$");
                                                            swCoord.Write(s2.Value.ToString() + "$");
                                                            tempFriends += s2.Value.ToString() ;

                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            if (s.Key.ToString() == "hashtags")
                                            {
                                                sw.Write("hashtags" + "$");
                                                try
                                                {
                                                    var hashs = s.Value;
                                                    System.Object[] selectedCollection6 = (System.Object[])hashs;
                                                    foreach (object ents1 in selectedCollection6)
                                                    {
                                                        var ents2 = ents1;
                                                        Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                        foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                        {

                                                            if (s2.Key.ToString().ToLowerInvariant() == "text")
                                                            {
                                                                string t3 = s2.Value.ToString();
                                                                totalText += t3 + "#";
                                                                sw.Write(s2.Value.ToString() + "$");
                                                                swHash.Write(s2.Value.ToString() + "$");
                                                                tempFriends += s2.Value;
                                                            }
                                                            else
                                                            {
                                                                var inv = s2.Value;
                                                                System.Object[] invariants = (System.Object[])inv;
                                                                foreach (object inv1 in invariants)
                                                                {
                                                                    string t3 = inv1.ToString();
                                                                    totalText += t3 + "#";
                                                                    sw.Write(t3 + "$");
                                                                    swHash.Write(t3 + "$");
                                                                    tempFriends += t3;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            if (s.Key.ToString() == "user_mentions")
                                            {
                                                sw.Write("user_mentions" + "$");
                                                try
                                                {
                                                    var ments = s.Value;
                                                    System.Object[] selectedCollection6 = (System.Object[])ments;
                                                    foreach (object ents1 in selectedCollection6)
                                                    {
                                                        var ents2 = ents1;
                                                        Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                        foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                        {

                                                            //int k1 = 9;
                                                            string t3 = s2.Value.ToString();
                                                            totalText += t3 + "#";
                                                            sw.Write(s2.Value.ToString() + "$");
                                                            swMention.Write(s2.Value.ToString() + "$");
                                                            tempFriends += s2.Value;

                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            if (s.Key.ToString() == "coordinates")
                                            {
                                                sw.Write("coordinates" + "$");
                                                try
                                                {
                                                    var coor = s.Value;
                                                    System.Object[] selectedCollection6 = (System.Object[])coor;
                                                    foreach (object ents1 in selectedCollection6)
                                                    {
                                                        var ents2 = ents1;
                                                        Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                        foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                        {

                                                           // int k1 = 9;
                                                            string t3 = s2.Value.ToString();
                                                            totalText += t3 + "#";
                                                            sw.Write(s2.Value.ToString() + "$");
                                                            swCoord.Write(s2.Value.ToString() + "$");
                                                            tempFriends += s2.Value;

                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            if (s.Key.ToString() == "urls")
                                            {
                                                sw.Write("urls" + "$");
                                                try
                                                {
                                                    var urls = s.Value;
                                                    System.Object[] selectedCollection6 = (System.Object[])urls;
                                                    foreach (object ents1 in selectedCollection6)
                                                    {
                                                        var ents2 = ents1;
                                                        Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                        foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                        {

                                                           // int k1 = 9;
                                                            string t3 = s2.Value.ToString();
                                                            totalText += t3 + "#";
                                                            sw.Write(s2.Value.ToString() + "$");
                                                            swURLS.Write(s2.Value.ToString() + "$");
                                                            tempFriends += s2.Value;

                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            if (s.Key.ToString() == "symbols")
                                            {
                                                sw.Write("symbols" + "$");
                                                try
                                                {
                                                    var sym = s.Value;
                                                    System.Object[] selectedCollection6 = (System.Object[])sym;
                                                    foreach (object ents1 in selectedCollection6)
                                                    {
                                                        var ents2 = ents1;
                                                        Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                        foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                        {

                                                          //  int k1 = 9;
                                                            string t3 = s2.Value.ToString();
                                                            totalText += t3 + "#";
                                                            sw.Write(s2.Value.ToString() + "$");
                                                            swSymb.Write(s2.Value.ToString() + "$");
                                                            tempFriends += s2.Value;

                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            if (s.Key.ToString() == "lang")
                                            {
                                                sw.Write("lang" + "$");
                                                try
                                                {
                                                    var lang = s.Value;
                                                    System.Object[] selectedCollection6 = (System.Object[])lang;
                                                    foreach (object ents1 in selectedCollection6)
                                                    {
                                                        var ents2 = ents1;
                                                        Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                        foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                        {

                                                        //    int k1 = 9;
                                                            string t3 = s2.Value.ToString();
                                                            totalText += t3 + "#";
                                                            sw.Write(s2.Value.ToString() + "$");
                                                            swLang.Write(s2.Value.ToString() + "$");
                                                            tempFriends += s2.Value;

                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            if (s.Key.ToString() == "user")
                                            {
                                                sw.Write("user" + "$");
                                                try
                                                {
                                                    var user = s.Value;
                                                    Dictionary<string, object> selectedCollection6 = (Dictionary <string, object>) user;
                                                    foreach (KeyValuePair<string, object> s2 in selectedCollection6)
                                                    {
                                                   //     var ents2 = ents1;
                                                     //   Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                       // foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                       // {

                                                         //   int k1 = 9;
                                                            string t3 = s2.Value.ToString();
                                                            totalText += t3 + "#";
                                                            sw.Write(s2.Value.ToString() + "$");
                                                            swUser.Write(s2.Value.ToString() + "$");
                                                        tempFriends += s2.Value;

                                                    }

                                                    //}
                                                }
                                                catch (Exception ex)
                                                {
                                                    // continue;

                                                }
                                            }
                                            string test = s.Key.ToString();
                                            if (s.Key.ToString() == "entities")
                                            {
                                                var entities = s.Value;
                                                Dictionary<string, object> selectedCollection00 = (Dictionary<string, object>)entities;
                                                foreach (KeyValuePair<string, object> ents in selectedCollection00)
                                                {
                                                    if (ents.Key.ToString() == "symbols")
                                                    {
                                                        sw.Write("symbols" + "$");
                                                        try
                                                        {
                                                            var sym = ents.Value;
                                                            System.Object[] selectedCollection6 = (System.Object[])sym;
                                                            foreach (object ents1 in selectedCollection6)
                                                            {
                                                                var ents2 = ents1;
                                                                Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                                foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                                {

                                                                 //   int k1 = 9;
                                                                    string t3 = s2.Value.ToString();
                                                                    totalText += t3 + "#";
                                                                    sw.Write(s2.Value.ToString() + "$");
                                                                    swSymb.Write(s2.Value.ToString() + "$");
                                                                    tempFriends += s2.Value;

                                                                }

                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            // continue;

                                                        }
                                                    }
                                                    if (ents.Key.ToString() == "coordinates")
                                                    {
                                                        sw.Write("coordinates" + "$");
                                                        try
                                                        {
                                                            var coor = ents.Value;
                                                            System.Object[] selectedCollection6 = (System.Object[])coor;
                                                            foreach (object ents1 in selectedCollection6)
                                                            {
                                                                var ents2 = ents1;
                                                                Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                                foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                                {

                                                                  //  int k1 = 9;
                                                                    string t3 = s2.Value.ToString();
                                                                    totalText += t3 + "#";
                                                                    sw.Write(s2.Value.ToString() + "$");
                                                                    swCoord.Write(s2.Value.ToString() + "$");
                                                                    tempFriends += s2.Value;

                                                                }

                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            // continue;

                                                        }
                                                    }
                                                    //      }
                                                    // }
                                                    if (ents.Key.ToString() == "urls")
                                                    {
                                                        sw.Write("urls" + "$");
                                                        try
                                                        {
                                                            var urls = ents.Value;
                                                            System.Object[] selectedCollection6 = (System.Object[])urls;
                                                            foreach (object ents1 in selectedCollection6)
                                                            {
                                                                var ents2 = ents1;
                                                                Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                                foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                                {

                                                                //    int k1 = 9;
                                                                    string t3 = s2.Value.ToString();
                                                                    totalText += t3 + "#";
                                                                    sw.Write(s2.Value.ToString() + "$");
                                                                    swURLS.Write(s2.Value.ToString() + "$");
                                                                    tempFriends += s2.Value;

                                                                }

                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            // continue;

                                                        }
                                                    }
                                                    //    }
                                                    //  }
                                                    if (ents.Key.ToString() == "user_mentions")
                                                    {
                                                        sw.Write("user_mentions" + "$");
                                                        try
                                                        {
                                                            var ments = ents.Value;
                                                            System.Object[] selectedCollection6 = (System.Object[])ments;
                                                            foreach (object ents1 in selectedCollection6)
                                                            {
                                                                var ents2 = ents1;
                                                                Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                                foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                                {

                                                              //      int k1 = 9;
                                                                    string t3 = s2.Value.ToString();
                                                                    totalText += t3 + "#";
                                                                    sw.Write(s2.Value.ToString() + "$");
                                                                    swMention.Write(s2.Value.ToString() + "$");
                                                                    tempFriends += s2.Value;
                                                                }

                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            // continue;

                                                        }
                                                    }
                                                    
                                                    if (ents.Key.ToString() == "hashtags")
                                                    {
                                                        sw.Write("hashtags" + "$");
                                                        try
                                                        {
                                                            var hashs = ents.Value;
                                                            System.Object[] selectedCollection6 = (System.Object[])hashs;
                                                            foreach (object ents1 in selectedCollection6)
                                                            {
                                                                var ents2 = ents1;
                                                                Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                                foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                                {

                                                                    if (s2.Key.ToString().ToLowerInvariant() == "text")
                                                                    {
                                                                        string t3 = s2.Value.ToString();
                                                                        totalText += t3 + "#";
                                                                        sw.Write(s2.Value.ToString() + "$");
                                                                        swHash.Write(s2.Value.ToString() + "$");
                                                                        tempFriends += s2.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        var inv = s2.Value;
                                                                        System.Object[] invariants = (System.Object[])inv;
                                                                        foreach (object inv1 in invariants)
                                                                        {
                                                                            string t3 = inv1.ToString();
                                                                            totalText += t3 + "#";
                                                                            sw.Write(t3 + "$");
                                                                            swHash.Write(t3 + "$");
                                                                            tempFriends += t3;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            // continue;

                                                        }
                                                    }
                                     //           }
                                       //         }
                                                   if (ents.Key.ToString() == "contributions")
                                                    {
                                                        sw.Write("contributions" + "$");
                                                        try
                                                        {
                                                            var hashs = ents.Value;
                                                            System.Object[] selectedCollection6 = (System.Object[])hashs;
                                                            foreach (object ents1 in selectedCollection6)
                                                            {
                                                                var ents2 = ents1;
                                                                Dictionary<string, object> selectedCollection10 = (Dictionary<string, object>)ents2;
                                                                foreach (KeyValuePair<string, object> s2 in selectedCollection10)
                                                                {

                                                               //     int k1 = 9;
                                                                    string t3 = s2.Value.ToString();
                                                                    totalText += t3 + "#";
                                                                    sw.Write(s2.Value.ToString() + "$");
                                                                    swCONT.Write(s2.Value.ToString() + "$");
                                                                    tempFriends += s2.Value;


                                                                }

                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            // continue;

                                                        }
                                                    }
                                                }
                                            } 
                                            string t2 = "";
                                            try { t2 = s.Value.ToString(); }
                                            catch (Exception ex)
                                            {
                                                // continue;
                                                sw.Write(abc + "$" + s.Key + "$");
                                                totalText += abc + "#";
                                                tempFriends += abc;

                                            }
                                          //  sw.Write(t.Key + "$" + s.Key + "$" + t2 + "$");
                                            sw.Write(t2 + "$");
                                            totalText += t2 + "#";
                                            tempFriends += t2;
                                        }
                                      //  }
                                        catch (Exception ex)
                                        {
                                            // continue;
                                            sw.Write(abc + "$");
                                            totalText += abc + "#";

                                        }
                                        // sw.Write(s.Key+","+s.Value.ToString() + ",");
                                    }
                                }
                                catch(Exception ex)
                                {
                                    // continue;
                                    sw.Write(abc + "$");
                                    totalText += abc+"#";
                                }
                              
                            }
                            //string[] source = totalText.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] source = totalText.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                       /*     try {
                                friends1 = cut.GetUsers1(userName, FriendshipType.FriendsList);
                            }
                            catch(Exception ex)
                            {
                                int k = 6;
                            } 
                                foreach (string friend in friends1)
                            {
                                var matchQuery = from word1 in source
                                                 where word1.ToLowerInvariant() == friend.ToLowerInvariant()
                                                 select word1;

                                // Count the matches, which executes the query.
                                int wordCount = matchQuery.Count();
                                /*  if (wordCount > 0)
                                  {
                                      int k = 4;
                                  } 

                                string relation1 = userName + friend;
                                if (friendsHash.ContainsKey(relation1) == false)
                                {
                                    friendsHash.Add(relation1, relation1);
                                    sw10.WriteLine(userName + "," + friend + "," + wordCount);
                                }
                            } */
                                
                           

                            /*      List<string> followers = cut.GetUsers1(userName, FriendshipType.FollowersList);

                                  foreach (string follower in followers)
                                  {
                                      var matchQuery1 = from word1 in source
                                                       where word1.ToLowerInvariant() == follower.ToLowerInvariant()
                                                       select word1;

                                      // Count the matches, which executes the query.
                                      int wordCount1 = matchQuery1.Count();
                                      /*  if (wordCount1 > 0)
                                        {
                                            int k = 4;
                                        } */
                            /*         string relation = userName + follower;
                                     if (followersHash.ContainsKey(relation) == false)
                                     {
                                         followersHash.Add(relation, relation);
                                         sw2.WriteLine(userName + "," + follower + "," + wordCount1);
                                     }
                                 } */

                            /*   Tweet tw=new Tweet();
                               string tw1 = "";
                               foreach (KeyValuePair<string, object> pair1 in tw2)
                               {
                                   // {
                                   //   sw.WriteLine(t + "\n");
                                   try {
                                       tw = (Tweet)pair1.Value;
                                   }
                                   catch(Exception ex)
                                   {
                                       tw1 = pair1.Value.ToString();
                                   }
                                   if (tw != null)
                                   {
                                       sw.WriteLine("nameTest" + "," + tw.Favourites + "," + tw.Followers + "," + tw.ID + "," +
                                    tw.Retweets + "," + tw.Text + "," + tw.Time + "," + tw.User);
                                   }
                                   else
                                   {
                                       sw.WriteLine(tw1);
                                   } */
                        }
                        /*      if (tw.Entities.UserMentionEntities.Count > 0)
                              {
                                  foreach (UserMentionEntity t1 in tw.Entities.UserMentionEntities)
                                  {
                                      sw.Write("," + t1.Id + "," + t1.Name + "," + t1.ScreenName);
                                      sw1.Write("," + t1.Id + "," + t1.Name + "," + t1.ScreenName);
                                  }
                              }

                              if (tw.RetweetedStatus.Entities.UserMentionEntities.Count > 0)
                              {
                                  foreach (UserMentionEntity t1 in tw.RetweetedStatus.Entities.UserMentionEntities)
                                  {
                                      sw.Write("," + t1.Id + "," + t1.Name + "," + t1.ScreenName);
                                      sw1.Write("," + t1.Id + "," + t1.Name + "," + t1.ScreenName);
                                  }
                              }  

                          } 
                          } */
                        int k1 = 0;
                        int k2 = 0;
                        try {
                            k1 = countOccurences(userName.getNode2(), tempFriends);
                            k2 = countOccurences(userName.getID2(), tempFriends);
                        }
                        catch(Exception ex)
                        {

                        }
                        int totCount = k1 + k2;
                        if (totCount > 0)
                        {
                            int kk = 8;
                        }

                        swFriend.WriteLine(userName.getNode1() + "," + userName.getID1() + "," + userName.getNode2() + ","
                            + userName.getID2() + totCount);
                    }

                    
                    sw.Close();

                    /*var file1 = FileManagement.GetFilename(userName)+"1";
                    StreamWriter sw1 = new StreamWriter(file1);
                   // StreamWriter sw1 = new StreamWriter(file);
                    sw1.WriteLine("Date" + "," + "ID" + "," + "ID_str" + "," + "text" + "," + "source" + "," + "truncated" + "," + "in_reply_to_status_id" + "," +
                        "in_reply_to_status_id_str" + "," + "in_reply_to_user_id" + "," + "in_reply_to_user_id_str" + "," +
                        "in_reply_to_screen_name" + "," + "users" + "," + "geo" + "," + "coordinates" + "," + "place" + "," + "contributors" + "," + "is_quote_status" +
                        "retweet_count" + "," + "favourite count" + "," + "entities" + "," + "favourited" + "," + "retweeted" + "," + "poss_sensitive" + "," + "lang");
                    if (follows != null && follows.Count() > 0)
                    {
                        foreach (var t in follows)
                        {
                            Dictionary<string, object> tw2 = t.Value;
                            Tweet tw = new Tweet();
                            foreach (KeyValuePair<string, object> pair1 in tw2)
                            {
                                // {
                                //   sw.WriteLine(t + "\n");
                                tw = (Tweet)pair1.Value;
                                sw.WriteLine("nameTest" + "," + tw.Favourites + "," + tw.Followers + "," + tw.ID + "," +
                             tw.Retweets + "," + tw.Text + "," + tw.Time + "," + tw.User);
                            }
                        }
                    }
                    sw1.Close(); */
                    int tweetCount = 0;
                    try {
                        //Check the time for the next download
                        if (nextUpdateTime1[userName] > DateTime.Now)
                        {
                            continue;
                        }
                    }
                    catch(Exception ex)
                    {

                    }

                    //Recalculate time span between tweets
                    var tweets = FileManagement.GetTweets(file);
                    var timeSpan = Twitter.GetAverageTimeSpan(tweets);
                    try
                    {
                        nextUpdateTime1[userName] += timeSpan;

                }
                    catch (Exception ex)
                {

                }
                //Get the tweets for this user
                var tweetList = Twitter.GetTimeline(userName.getNode1(), tweets, ref tweetCount);
                    Twitter.Statistics(tweetCount, userName.getNode1());
                    
                    //Encode each tweet and add them to a list
                    var encodedTweetList = FileManagement.Serializer(tweetList);

                    //Open & Write to file only if there are new tweets
                    FileManagement.Writer(encodedTweetList, file);
                    FileManagement.Logger(encodedTweetList, userName.getNode1());

                   
                }
            }
            sw10.Close();
            sw2.Close();
            swFriend.Close();
            swHash.Close();
            swCoord.Close();
            swCONT.Close();
            swMention.Close();
            swMention.Close();
            swSymb.Close();
            swLang.Close();
            swUser.Close();
            swMainUser.Close();
            swRep.Close();
        }

        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter("Retweets.csv");
            
            friendsHash = new Hashtable();
            followersHash = new Hashtable();
            friendsHash1 = new List<string>();
            followersHash2 = new List<string>();
            friends1 = new List<string>();
            var twitter = new Twitter1
            {
                OAuthConsumerKey = "vOIB6Pmz3B75VypW8n35L8EgZ",
                OAuthConsumerSecret = "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ"
            };



            Init();
            //Credentials - Get your own credentials from http://dev.twitter.com
            Twitter.SetCredentials(accessToken: "vOIB6Pmz3B75VypW8n35L8EgZ", accessTokenSectet: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ",
                                   consumerKey: "vOIB6Pmz3B75VypW8n35L8EgZ", consumerSecret: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ");

            Console.WriteLine("Credentials ready!");

            //Initial Variables
            var startDate = DateTime.Now;
            int NumberofTweets=0;
            SortedDictionary<Friend,Friend> friends = new SortedDictionary<Friend, Friend>();
            //Start program
           
                //Read Twitter Users List
                //   var usernames = FileManagement.GetUsernames();
                var usernames = FileManagement.GetNodes1();
                //   var nextUpdateTime = FileManagement.GetNextUpdateTime();
                var nextUpdateTime1 = FileManagement.GetNextUpdateTime1();
            // var twitter;

            string user2ID = "";
            string user1ID = "";
                foreach (string userName in usernames)
                //  foreach (var userName in friendsHash1)
                {
                /*  if (userName.Length < 6)
                  {
                      continue;
                  }

              */
                again1:
                User user = null;
                try
                {
                    user= twitter.GetInfo(userName).Result;
                }
                catch (AggregateException g)
                {
                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    goto again1;

                }
                user1ID = user.UserIDResponse;
                if (user1ID.Length < 2)
                {
                    user1ID = user.UserID.ToString();
                }

                try
                    {
                        //        friends1 = twitter.GetUsers1(userName.getNode1(), FriendshipType.FriendsList).Result;

                        twitts = twitter.GetTwitts(userName, 100).Result;
                    }
                    catch (Exception ex1)
                    {
                        if (ex1.InnerException.Message.Contains("Rate limit"))
                        {
                            int k = 15 * 60 * 1000;
                            System.Threading.Thread.Sleep(k);
                        }
                        continue;
                        //goto again;


                    }

                    String totalText = "";



                    if (twitts != null && twitts.Count() > 0)
                    {
                    NumberofTweets = twitts.Count();
                        int twCount = 0;
                        String tempFriends = "";
                        //  foreach (var t in twitts)
                        foreach (KeyValuePair<string, Dictionary<string, object>> t in twitts)
                        {
                            foreach(KeyValuePair< string, object> value in t.Value)
                            {
                                string t3 = value.Key;
                                Object ab = value.Value;
                                if(ab!=null && ab.ToString().Contains("RT @"))
                                {
                                List<int> list = new List<int>();
                                for (int i = 0; i < ab.ToString().Length; i++)
                                {
                                    if (ab.ToString()[i] == ':')
                                        list.Add(i);
                                }
                                int tmpk = 0;
                                foreach(int k in list)
                                {
                                    if(k> ab.ToString().IndexOf("RT @"))
                                    {
                                        tmpk = k;
                                        break;
                                    }
                                }
                                int t1 = 0;
                                if (tmpk > ab.ToString().IndexOf("RT @"))
                                {
                                    t1=tmpk - ab.ToString().IndexOf("RT @");
                                }
                                else
                                {
                                    List<int> list1 = new List<int>();
                                    for (int i = 0; i < ab.ToString().Length; i++)
                                    {
                                        if (ab.ToString()[i] == ' ')
                                            list.Add(i);
                                    }

                                    int tmpk1 = 0;
                                    foreach (int k1 in list1)
                                    {
                                        if (k1 > ab.ToString().IndexOf("RT @"))
                                        {
                                            tmpk1 = k1;
                                            t1 = tmpk1 - ab.ToString().IndexOf("RT @");
                                            break;
                                        }

                                    }

                                    if(tmpk< ab.ToString().IndexOf("RT @"))
                                        {
                                        t1 = ab.ToString().Length- ab.ToString().IndexOf("RT @");
                                    }


                                }

                                    string friend = ab.ToString().Substring(ab.ToString().IndexOf("RT @")+4, t1-4);
                                User user2 = null;
                                again:
                                try {
                                    user2 = twitter.GetInfo(friend).Result;
                                }
                                catch(AggregateException g)
                                {
                                    int k = 15 * 60 * 1000;
                                    System.Threading.Thread.Sleep(k);
                                    goto again;

                                }
                                user2ID = user2.UserIDResponse;
                                if (user2ID.Length < 2)
                                {
                                    user2ID = user2.UserID.ToString();
                                }
                                Friend fo = new Friend(userName, friend,1);
                                    if (friends.ContainsKey(fo) == false)
                                    {
                                        friends.Add(fo,fo);
                                    }
                                    else
                                    {
                                        Friend fo1 = (Friend)friends[fo];
                                        int wt = fo1.getWt();
                                        wt++;
                                        fo1.setWT(wt);
                                        friends[fo] = fo1;
                                    }
                                    
                                }

                            }
                            

                        } 
                  
                    } 


                }
                foreach(KeyValuePair<Friend,Friend> de in friends)
            {

                Friend fr = de.Key;
                double twt = NumberofTweets;
                double wt = fr.getWt() / twt;
                sw.WriteLine(fr.getNode1() + "," + user1ID+","+ fr.getNode2() + "," + user2ID+","+ fr.getWt() +"," +wt);
            }
           sw.Close();
        }


          public static void Main4(string[] args)
        {
           
            StreamWriter swFriend = File.AppendText("Friends.csv");
           
            friendsHash = new Hashtable();
            followersHash = new Hashtable();
            friendsHash1 = new List<string>();
            followersHash2 = new List<string>();
            friends1 = new List<string>();
            var twitter = new Twitter1
            {
                OAuthConsumerKey = "vOIB6Pmz3B75VypW8n35L8EgZ",
                OAuthConsumerSecret = "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ"
            };



            Init();
            //Credentials - Get your own credentials from http://dev.twitter.com
            Twitter.SetCredentials(accessToken: "vOIB6Pmz3B75VypW8n35L8EgZ", accessTokenSectet: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ",
                                   consumerKey: "vOIB6Pmz3B75VypW8n35L8EgZ", consumerSecret: "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ");

            Console.WriteLine("Credentials ready!");

            //Initial Variables
            var startDate = DateTime.Now;

            //Start program
            while (true)
            {
                //Read Twitter Users List
                //   var usernames = FileManagement.GetUsernames();
                var usernames = FileManagement.GetNodes();
                //   var nextUpdateTime = FileManagement.GetNextUpdateTime();
                var nextUpdateTime1 = FileManagement.GetNextUpdateTime1();
                
                  /*         List<string> followers1 = cut.GetUsers1(userName, FriendshipType.FollowersList);

                           foreach (string foll1 in followers1)
                           {
                               if (followersHash1.Contains(foll1) == false)
                               {
                                   followersHash1.Add(foll1);
                               }

                           } */
                     
                foreach (Friend userName in usernames.Keys)
                //  foreach (var userName in friendsHash1)
                {
                    if (userName.getNode1().Length < 2)
                    {
                        continue;
                    }
                    

                    //  again;
                    //  var twitter = new Twitter1();

                    try
                    {
                 //       User user = twitter.GetInfo(userName.getNode1()).Result;
                      /* swMainUser.WriteLine(userName + "," + user.Count + ","
                           + user.Categories.Count +
                           user.FavoritesCount + "," + user.FollowersCount + ","
                           + user.FriendsCount + "," + user.ListedCount + "," +
                           user.Location + "," + user.Name + "," + user.Notifications + ","
                           + user.Page + "," + user.ScreenName + "," +
                           user.ScreenNameList + "," + user.ScreenNameResponse + ","
                           + user.Slug + "," + user.Status + "," + user.StatusesCount + "," +
                           user.TimeZone + "," + user.Type + "," + user.Url + "," + user.UserID + "," +
                           user.UserIdList + "," + user.UserIDResponse + "," + user.UtcOffset + "," +
                           user.Verified); */

                        
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("Rate limit"))
                        {
                           
                            int k = 15 * 60 * 1000;
                            System.Threading.Thread.Sleep(k);
                        }
                        continue;
                        // goto again;
                    }
                    try
                    {
                        if (userName.getNode1().Length > 5)
                        {
                            friends1 = twitter.GetUsers1(userName.getNode1(), FriendshipType.FriendsList).Result;
                        }
                        else
                        {
                            continue;
                        }
                        if (userName.getNode2().Length > 5)
                        {
                            friends2 = twitter.GetUsers1(userName.getNode2(), FriendshipType.FriendsList).Result;
                        }
                        else
                        {
                            continue;
                        }
                        int friendsCount = 0;
                       
                        foreach (string fr1 in friends1)
                        {
                            
                            foreach (string fr2 in friends2)
                            {
                               
                                if (fr1.ToLowerInvariant() == fr2.ToLowerInvariant())
                                {
                                    friendsCount++;
                                }

                            }
                        }

                        double fr1Ct = friendsCount / friends1.Count;
                        double fr2CT = friendsCount / friends2.Count;
                        swFriend.WriteLine(userName.getNode1() + "," + userName.getID1() +
                            userName.getNode2() + userName.getID2() + "," + friendsCount+","+ fr1Ct);
                        swFriend.WriteLine(userName.getNode2() + "," + userName.getID2() +
                           userName.getNode1() + userName.getID1() + "," + friendsCount+","+fr2CT);

                        //  twitts = twitter.GetTwitts(userName.getNode1(), 100).Result;
                    }
                    catch (AggregateException ex1)
                    {
                      //  if (ex1.InnerException.Message.Contains("Rate limit"))
                       // {
                            int k = 15 * 60 * 1000;
                            System.Threading.Thread.Sleep(k);
                       // }
                        continue;
                        //goto again;


                    }
                    catch(Exception ex)
                    {

                    }

                    String totalText = "";

                   
                    try
                    {
                        //Check the time for the next download
                        if (nextUpdateTime1[userName] > DateTime.Now)
                        {
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    
                //    var timeSpan = Twitter.GetAverageTimeSpan(tweets);
                    try
                    {
                     //   nextUpdateTime1[userName] += timeSpan;

                    }
                    catch (Exception ex)
                    {

                    }
                   
                //    FileManagement.Writer(encodedTweetList, file);
                  //  FileManagement.Logger(encodedTweetList, userName.getNode1());


                }
            }
            sw10.Close();
            sw2.Close();
            swFriend.Close();
           
        }

    }
}