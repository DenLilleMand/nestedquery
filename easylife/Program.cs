using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easylife
{

    class Interest
    {
        public Interest(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals(Interest interest)
        {
            return this.Id == interest.Id;
        }

    }

    class Feed
    {
        public Feed(int id, String message, Interest interest)
        {
            this.Message = message;
            this.Id = id;
            this.Interest = interest;
        }
        public int Id { get; set; }
        public String Message { get; set; }
        public Interest Interest { get; set; }
    }

    class User
    {
        public User(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Interest> Interests { get; set; }
        public IEnumerable<FriendShip> FriendShips { get; set; }
        public IEnumerable<Feed> Feeds { get; set; }
    }

    class FriendShip
    {
        public FriendShip(User user, User friend)
        {
            this.User = user;
            this.Friend = friend;
        }
        public User User { get; set; }
        public User Friend { get; set; }

    }



    class Program
    {
        private static void Main(string[] args)
        {
            //create interests
            Interest interest = new Interest(1, "Basketball");
            Interest interest2 = new Interest(2, "Football");
            Interest interest3 = new Interest(3, "Cartoons");
            Interest interest4 = new Interest(4, "Gaming");
            Interest interest5 = new Interest(5, "Running");
            //createing user
            User user = new User(1, "matti nielsen");

            //createing list for friendships
            List<FriendShip> friends = new List<FriendShip>();

            //createing friends
            User friend = new User(2, "Martin petersen");
            User friend2 = new User(3, "Marc holt");
            User friend3 = new User(4, "Christian brink");
            User friend4 = new User(5, "Jon Højlund");

            //adding friends to the friendship lists, and giveing them to the user.
            friends.Add(new FriendShip(user, friend));
            friends.Add(new FriendShip(user, friend2));
            friends.Add(new FriendShip(user, friend3));
            friends.Add(new FriendShip(user, friend4));
            user.FriendShips = friends;

            //createing lists of interests for the user and friends.
            List<Interest> interestsForUser = new List<Interest>();
            interestsForUser.Add(interest3);
            interestsForUser.Add(interest4);

            List<Interest> interestsForFriend = new List<Interest>();
            interestsForFriend.Add(interest);
            interestsForFriend.Add(interest2);

            List<Interest> interestsForFriend2 = new List<Interest>();
            interestsForFriend2.Add(interest3);
            interestsForFriend2.Add(interest4);

            List<Interest> interestsForFriend3 = new List<Interest>();
            interestsForFriend3.Add(interest4);
            interestsForFriend3.Add(interest5);

            List<Interest> interestsForFriend4 = new List<Interest>();
            interestsForFriend4.Add(interest4);
            interestsForFriend4.Add(interest5);

            //Adding the interests to the users
            user.Interests = interestsForUser;

            friend.Interests = interestsForFriend;
            friend2.Interests = interestsForFriend2;
            friend3.Interests = interestsForFriend3;
            friend4.Interests = interestsForFriend4;

            //Adding feeds to the users
            List<Feed> feedsForUser = new List<Feed>();
            feedsForUser.Add(new Feed(1, "Basketball is great", interest));
            feedsForUser.Add(new Feed(2, "Football is great", interest2));
            user.Feeds = feedsForUser;

            List<Feed> feedsForFriend = new List<Feed>();
            feedsForFriend.Add(new Feed(3, "Basketball is great", interest));
            feedsForFriend.Add(new Feed(4, "Football is great", interest2));
            friend.Feeds = feedsForFriend;

            List<Feed> feedsForFriend2 = new List<Feed>();
            feedsForFriend2.Add(new Feed(5, "Cartoons is great", interest3));
            feedsForFriend2.Add(new Feed(6, "Gaming is great", interest4));
            friend2.Feeds = feedsForFriend2;

            List<Feed> feedsForFriend3 = new List<Feed>();
            feedsForFriend3.Add(new Feed(7, "Gaming is great", interest4));
            feedsForFriend3.Add(new Feed(8, "Running", interest5));
            friend3.Feeds = feedsForFriend3;

            List<Feed> feedsForFriend4 = new List<Feed>();
            feedsForFriend4.Add(new Feed(9, "Gaming is great", interest4));
            feedsForFriend4.Add(new Feed(10, "Running", interest5));
            friend4.Feeds = feedsForFriend4;


            //Selecting all of of the friendships => Friend => Feeds => where(user.equal(friend))
            IEnumerable<Feed> friendsFeedsWithSameInterests = user.FriendShips.Where(
                friendship =>
                    friendship.User.Interests.Any(
                        userInterest =>
                            friendship.Friend.Interests.Any(friendInterest => friendInterest.Equals(userInterest))))
                .SelectMany(
                    result => result.Friend.Feeds);

            Console.WriteLine("New run:");
            friendsFeedsWithSameInterests.ToList().ForEach(delegate(Feed feed)
            {
                Console.WriteLine("Feed:" + Environment.NewLine);
                Console.WriteLine(feed.Message + Environment.NewLine);
            });
            Console.WriteLine("End of run");
            Console.ReadKey();


        }
    }
}
