using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

namespace MyHashTags.Lib
{

    public class MyHashTagsClass
    {
        private SingleUserAuthorizer auth;
        public static TwitterContext twitter;

        public MyHashTagsClass(string accessToken, string accessTokenSecret, string consumerKey, string consumerSecret )
        {
            auth = new SingleUserAuthorizer()
            {
                CredentialStore = new SingleUserInMemoryCredentialStore()
                {
                    AccessToken = "1976215495-YyXTEpyECgiwOsL7BCoDAohVYpwGPPAzcyMlnkT",
                    AccessTokenSecret = "QhwPS2HYkiV2vznZ0yULm2z1x5g4tUctM2JItLH3MEC3h",
                    ConsumerKey = "UyK8wQUDghgW5cnZFFXOQQ",
                    ConsumerSecret = "OLepmuMMvnHG6sXLdWYLObwskOBJDCLOUUczgeguEs"
                }
            };
            twitter = new TwitterContext(auth);
        }

        public List<string> GetUsers(string username, FriendshipType friendshipType)
        {
            var friends3 =
                twitter.Friendship.FirstOrDefault(x => x.Type == friendshipType && x.ScreenName == username && x.Count == 100)
                    .Users.Select(x => x.ScreenNameResponse)
                    .ToList();

            return friends3;
        }

        
        public System.Linq.IQueryable<Status> GetTweets(ulong username)
        {
            var statusTweets =
                 from tweet in twitter.Status
                 where tweet.Type == StatusType.User
                       && tweet.UserID == username
                       && tweet.Count == 200
                 select tweet;

            statusTweets.ToList().ForEach(
                tweet => Console.WriteLine(
                "Name: {0}, Tweet: {1}\n",
                tweet.User.Name, tweet.Text));

            return statusTweets;
        }

  //      internal async Task<Dictionary<string, Dictionary<string, object>>> GetUsers1(string userName, int count, string accessToken = null)
    //    {
      //      if (accessToken == null)
        //    {
          //      accessToken = await GetAccessToken();
           // }

        public List<string> GetUsers10(string username, FriendshipType friendshipType)
        {
            List<string> friends3 = new List<string>();
            try {
                friends3 = twitter.Friendship.FirstOrDefault(x => x.Type == friendshipType && x.ScreenName == username && x.Count == 100)
                    .Users.Select(x => x.ScreenNameResponse)
                    .ToList();

                return friends3; }
            catch (Exception ex1)
            {
                string x = ex1.Message;
                return friends3;
            }
        }

        public Dictionary<string, int> GetHashtags(string username)
        {
            List<Status> tweets = twitter.Status
                .Where(x => x.Type == StatusType.User && x.ScreenName == username && x.Count == 100)
                .ToList();

            Dictionary<string,int> retval = new Dictionary<string, int>();

            foreach (Status s in tweets)
            {
                if (s.Entities != null && s.Entities.HashTagEntities != null && s.Entities.HashTagEntities.Count > 0)
                {
                    foreach (HashTagEntity ht in s.Entities.HashTagEntities)
                    {
                        if (!retval.ContainsKey(ht.Tag.ToLower()))
                            retval.Add(ht.Tag.ToLower(),0);
                        ++retval[ht.Tag.ToLower()];
                    }
                }
            }
            return retval;
        }

        public Dictionary<string, int> GetHashtags(List<string> usernames)
        {
            Dictionary<string, int> retval = new Dictionary<string, int>();
            foreach (string username in usernames)
            {
                try
                {
                    List<Status> tweets = twitter.Status
                        .Where(x => x.Type == StatusType.User && x.ScreenName == username && x.Count == 100)
                        .ToList();

                    foreach (Status s in tweets)
                    {
                        if (s.Entities != null && s.Entities.HashTagEntities != null && s.Entities.HashTagEntities.Count > 0)
                        {
                            foreach (HashTagEntity ht in s.Entities.HashTagEntities)
                            {
                                if (!retval.ContainsKey(ht.Tag.ToLower()))
                                    retval.Add(ht.Tag.ToLower(), 0);
                                ++retval[ht.Tag.ToLower()];
                            }
                        }
                    }

                }
                catch (Exception)
                {
                }
            }
            return retval;
        }

    }
}
