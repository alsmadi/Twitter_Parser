using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using LinqToTwitter;

namespace TwitterScraper
{
    public class Twitter1
    {
        public string OAuthConsumerSecret { get; set; }        
        public string OAuthConsumerKey { get; set; }
        public static TwitterContext twitter;
        private SingleUserAuthorizer auth;
        //  public static TwitterContext twitter;
        public Twitter1(){
             auth = new SingleUserAuthorizer()
        {
            CredentialStore = new SingleUserInMemoryCredentialStore()
            {
                AccessToken = "48120535-ImAePJp1yHpg1GQopv5vJMLP4KQPOgJg7tHsTzSBx",
                AccessTokenSecret = "Alqr0jc4FtNFnxVI3XPMZX51YLlUIIibjxjwXUxJTjMHd",
                ConsumerKey = "vOIB6Pmz3B75VypW8n35L8EgZ",
                ConsumerSecret = "EMoAXFKiySHfYWnkdhwtyatncEQwwfuVpfryQ6MDp0OQobtOmQ"
            }
            };
        twitter = new TwitterContext(auth);
    }
        internal async Task<Dictionary<string, Dictionary<string, object>>> GetTwitts(string userName,int count, string accessToken = null)
        {
            Dictionary<string, Dictionary<string, object>> enumTweets = new Dictionary<string, Dictionary<string, object>>();
            again: if (accessToken == null)
            {
                accessToken = await GetAccessToken();   
            }
            try {
                var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", count, userName));
                requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
                var httpClient = new HttpClient();
                HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
                var serializer = new JavaScriptSerializer();
                dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
               // enumTweets = new Dictionary<string, Dictionary<string, object>>();
                //enumTweets = (List<Tweet>)(json as IEnumerable<dynamic>).ToList();
                var enumerableTwitts1 = (json as IEnumerable<dynamic>);
                int count1 = 0;
                try {
                    foreach (var tweet in enumerableTwitts1)
                    {
                        // string tt = tweet;
                        Dictionary<string, object> tw = tweet;
                        // Tweet tw = tweet;
                        //   enumTweets.Add(count1.ToString(), tw);
                        enumTweets.Add(count1.ToString(), tw);
                        count1++;
                    }
                }
                catch (AggregateException ex)
                {
                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    goto again;
                }
                catch (Exception ex)
                {
                    int l = 6;
                }

                if (enumTweets == null)
                {
                    return null;
                }
            }
            catch (Exception ex1)
            {
                if (ex1.InnerException.Message.Contains("Rate limit"))
                {
                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    goto again;

                }
                string x = ex1.Message;
              //  return friends3;
            }
            //  return enumerableTwitts.Select(t => (string)(t["text"].ToString()));
            // return enumerableTwitts.Select(t => (Tweet)(t));
            return enumTweets;
        }

        static async Task<int> HandleStreamResponse(LinqToTwitter.StreamContent strm)
        {
            switch (strm.EntityType)
            {
                case StreamEntityType.Control:
                    var control = strm.Entity as Control;
                    Console.WriteLine("Control URI: {0}", control.URL);
                    break;
                case StreamEntityType.Delete:
                    var delete = strm.Entity as Delete;
                    Console.WriteLine("Delete - User ID: {0}, Status ID: {1}", delete.UserID, delete.StatusID);
                    break;
                case StreamEntityType.DirectMessage:
                    var dm = strm.Entity as DirectMessage;
                    Console.WriteLine("Direct Message - Sender: {0}, Text: {1}", dm.Sender, dm.Text);
                    break;
                case StreamEntityType.Disconnect:
                    var disconnect = strm.Entity as Disconnect;
                    Console.WriteLine("Disconnect - {0}", disconnect.Reason);
                    break;
                case StreamEntityType.Event:
                    var evt = strm.Entity as Event;
                    Console.WriteLine("Event - Event Name: {0}", evt.EventName);
                    break;
                case StreamEntityType.ForUser:
                    var user = strm.Entity as ForUser;
                    Console.WriteLine("For User - User ID: {0}, # Friends: {1}", user.UserID, user.Friends.Count);
                    break;
                case StreamEntityType.FriendsList:
                    var friends = strm.Entity as FriendsList;
                    Console.WriteLine("Friends List - # Friends: {0}", friends.Friends.Count);
                    break;
                case StreamEntityType.GeoScrub:
                    var scrub = strm.Entity as GeoScrub;
                    Console.WriteLine("GeoScrub - User ID: {0}, Up to Status ID: {1}", scrub.UserID, scrub.UpToStatusID);
                    break;
                case StreamEntityType.Limit:
                    var limit = strm.Entity as Limit;
                    Console.WriteLine("Limit - Track: {0}", limit.Track);
                    break;
                case StreamEntityType.Stall:
                    var stall = strm.Entity as Stall;
                    Console.WriteLine("Stall - Code: {0}, Message: {1}, % Full: {2}", stall.Code, stall.Message, stall.PercentFull);
                    break;
                case StreamEntityType.Status:
                    var status = strm.Entity as Status;
                    Console.WriteLine("Status - @{0}: {1}", status.User.ScreenNameResponse, status.Text);
                    break;
                case StreamEntityType.StatusWithheld:
                    var statusWithheld = strm.Entity as StatusWithheld;
                    Console.WriteLine("Status Withheld - Status ID: {0}, # Countries: {1}", statusWithheld.StatusID, statusWithheld.WithheldInCountries.Count);
                    break;
                case StreamEntityType.TooManyFollows:
                    var follows = strm.Entity as TooManyFollows;
                    Console.WriteLine("Too Many Follows - Message: {0}", follows.Message);
                    break;
                case StreamEntityType.UserWithheld:
                    var userWithheld = strm.Entity as UserWithheld;
                    Console.WriteLine("User Withheld - User ID: {0}, # Countries: {1}", userWithheld.UserID, userWithheld.WithheldInCountries.Count);
                    break;
                case StreamEntityType.ParseError:
                    var unparsedJson = strm.Entity as string;
                    Console.WriteLine("Parse Error - {0}", unparsedJson);
                    break;
                case StreamEntityType.Unknown:
                default:
                    Console.WriteLine("Unknown - " + strm.Content + "\n");
                    break;
            }

            return await Task.FromResult(0);
        }

        internal async Task<User> GetInfo(string username, string accessToken = null)
        {
            again:
          //  try { 
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }
            List<string> info = new List<string>();
            User user1 = null;
            int count = 0;
            var cancelTokenSrc = new System.Threading.CancellationTokenSource();
            try
            {
                /* await
                    (from strm in twitter.Streaming
                     where strm.Type == StreamingType.User
                     select strm)
                    .WithCancellation(cancelTokenSrc.Token)
                    .StartAsync(async strm =>
                    {
                        if (string.IsNullOrEmpty(strm.Content))
                            Console.WriteLine("Keep-Alive");
                        else
                            await HandleStreamResponse(strm);

                        if (count++ == 25)
                            cancelTokenSrc.Cancel();
                    }); */

                var userResponse =
               await
               (from user in twitter.User
                where user.Type == UserType.Lookup &&
                      user.ScreenNameList == username
                select user)
               .ToListAsync();

                if (userResponse != null)
                {
                    userResponse.ForEach(user =>
                        Console.WriteLine("Name: " + user.ScreenNameResponse));

                    user1 = userResponse[0];
                  //  userResponse.
                
                }
                /*      var accounts =
                  from acct in twitter.Account
                  where acct.Type == AccountType.VerifyCredentials
                  select acct;

                      Account account = accounts.SingleOrDefault();


                      user = account.User; */
                /*
                Status tweet = user.Status ?? new Status();
                Console.WriteLine(
                    "User ID: {0}\nScreen Name: {1}\nTweet: {2}\n Tweet ID: {3}",
                     user. .Identifier.ID,
                     user.Identifier.ScreenName,
                     tweet.Text,
                     tweet.StatusID);

                string followerscount = user.FriendsCount.ToString();
                string tweetscount = user.StatusesCount.ToString();
                // friends3 = twitter.Friendship.FirstOrDefault(x => x.Type == friendshipType && x.ScreenName == username && x.Count == 100)
                //   .Users.Select(x => x.ScreenNameResponse)

                info = twitter.Account.FirstOrDefault(x => x.ScreenName == username && x.Count == 100)
                    .Users.Select(x => x.ScreenNameResponse).ToList(); */

                return user1;
            }
            catch (Exception ex1)
            {
                if (ex1.InnerException.Message.Contains("Rate limit"))
                {
                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    goto again;

                }
                string x = ex1.Message;
                return user1;
            }
        }
        internal async Task<List<string>> GetUsers1(string username, FriendshipType friendshipType, string accessToken = null)
        {
            again:   if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            //    public List<string> GetUsers1(string username, FriendshipType friendshipType)
            //    {
            //  Dictionary<string, Dictionary<string, object>> friends = new Dictionary<string, Dictionary<string, object>>();
            List<string> friends3 = new List<string>();
            try
            {
                friends3 = twitter.Friendship.FirstOrDefault(x => x.Type == friendshipType && x.ScreenName == username && x.Count == 100)
                    .Users.Select(x => x.ScreenNameResponse)
                    .ToList();

                return friends3;
            }
            catch (Exception ex1)
            {
                if(ex1.InnerException.Message.Contains("Rate limit")){
                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    goto again;

                }
                string x = ex1.Message;
                return friends3;
            }
        }

        internal async Task<Dictionary<string, Dictionary<string, object>>> GetFollowers(string userName, int count, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/followers/ids.json?cursor=-1&screen_name={1}&trim_user=1", count, userName));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            var serializer = new JavaScriptSerializer();
            dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            Dictionary<string, Dictionary<string, object>> enumTweets = new Dictionary<string, Dictionary<string, object>>();
            var enumerableTwitts1 = (json as IEnumerable<dynamic>);

            int count1 = 0;
            try
            {
                foreach (var tweet in enumerableTwitts1)
                {

                    Dictionary<string, object> tw = tweet;
                    // Tweet tw = tweet;
                    enumTweets.Add(count1.ToString(), tw);
                    count1++;
                }
            }
            catch (Exception ex)
            {
                int l = 6;
            }
            if (enumTweets == null)
            {
                return null;
            }
            //  return enumerableTwitts.Select(t => (string)(t["text"].ToString()));
            // return enumerableTwitts.Select(t => (Tweet)(t));
            return enumTweets;
        }

     /*   public static void LogExceptions(this Task task)
        {
            task.ContinueWith(t =>
            {
                var aggException = t.Exception.Flatten();
                foreach (var exception in aggException.InnerExceptions)
                    LogException(exception);
            },
            TaskContinuationOptions.OnlyOnFaulted);
        } */
        public async Task<string> GetAccessToken()
        {           
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            return  item["access_token"];            
        }
    }
}