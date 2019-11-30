using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{
    public static class SQLDataAccess
    {
        private static IConfiguration Configuration;

        public static void SetupConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static List<GameModel> GetPurchasedGames(string Username)
        {
            List<GameModel> games = new List<GameModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_PurchasedGame", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", Username);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GameModel game = new GameModel();
                    game.GameID = Convert.ToInt32(rdr["GameID"]);
                    game.Title = rdr["GameTitle"].ToString(); ;
                    game.Genre = rdr["GameGenre"].ToString(); ;
                    game.price = Convert.ToDouble(rdr["GamePrice"]);
                    game.Desc = rdr["GameDesc"].ToString();
                    games.Add(game);
                }
                conn.Close();
            }

            return games;
        }

        public static List<PlayerModel> GetFriends(string Username)
        {
            List<PlayerModel> friends = new List<PlayerModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_Friends", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", Username);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PlayerModel player = new PlayerModel();
                    player.Username = rdr["PlayerUsername"].ToString();
                    player.DisplayName = rdr["PlayerDisplayName"].ToString();
                    friends.Add(player);
                }
                conn.Close();
            }

            return friends;
        }

        public static List<StreamModel> GetFollowedStreams(string Username)
        {
            List<StreamModel> streams = new List<StreamModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_FollowedStreams", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", Username);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StreamModel stream = new StreamModel();
                    stream.StreamID = Convert.ToInt32(rdr["StreamID"]);
                    stream.Title = rdr["StreamTitle"].ToString();
                    stream.Link = rdr["StreamLink"].ToString();
                    stream.GameID = Convert.ToInt32(rdr["GameID"]);
                    streams.Add(stream);
                }
                conn.Close();
            }

            return streams;
        }

        public static List<GameModel> GetWishlist(string Username)
        {
            List<GameModel> games = new List<GameModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_WishList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", Username);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GameModel game = new GameModel();
                    game.GameID = Convert.ToInt32(rdr["GameID"]);
                    game.Title = rdr["GameTitle"].ToString(); ;
                    game.Genre = rdr["GameGenre"].ToString(); ;
                    game.price = Convert.ToDouble(rdr["GamePrice"]);
                    game.Desc = rdr["GameDesc"].ToString();
                    games.Add(game);
                }
                conn.Close();
            }

            return games;
        }

        public static List<PlayerModel> GetPlayers()
        {
            List<PlayerModel> players = new List<PlayerModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_Players", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PlayerModel player = new PlayerModel();
                    player.Username = rdr["PlayerUsername"].ToString();
                    player.DisplayName = rdr["PlayerDisplayName"].ToString();
                    players.Add(player);
                }
                conn.Close();
            }

            return players;
        }

        public static List<GameModel> GetGames()
        {
            List<GameModel> games = new List<GameModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_GameList", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GameModel game = new GameModel();
                    game.GameID = Convert.ToInt32(rdr["GameID"]);
                    game.Title = rdr["GameTitle"].ToString();
                    game.Genre = rdr["GameGenre"].ToString();
                    game.price = Convert.ToDouble(rdr["GamePrice"]);
                    game.Desc = rdr["GameDesc"].ToString();
                    games.Add(game);
                }
                conn.Close();
            }

            return games;
        }

        public static List<StreamModel> GetStreams()
        {
            List<StreamModel> streams = new List<StreamModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_Streams", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StreamModel stream = new StreamModel();
                    stream.StreamID = Convert.ToInt32(rdr["StreamID"]);
                    stream.Title = rdr["StreamTitle"].ToString();
                    stream.Link = rdr["StreamLink"].ToString();
                    stream.GameID = Convert.ToInt32(rdr["GameID"]);
                    stream.GameTitle = rdr["GameTitle"].ToString();
                    streams.Add(stream);
                }
                conn.Close();
            }

            return streams;
        }

        public static List<DeveloperModel> GetDevelopers()
        {
            List<DeveloperModel> developers = new List<DeveloperModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_Developers", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DeveloperModel developer = new DeveloperModel();
                    developer.ID = Convert.ToInt32(rdr["DevID"]);
                    developer.Name = rdr["DevName"].ToString();
                    developers.Add(developer);
                }
                conn.Close();
            }

            return developers;
        }

        public static List<CompetitionModel> GetCompetitions()
        {
            List<CompetitionModel> competitions = new List<CompetitionModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_Competitions", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CompetitionModel competition = new CompetitionModel();
                    competition.CompID = Convert.ToInt32(rdr["CompID"]);
                    competition.Date = Convert.ToDateTime(rdr["CompDate"]);
                    competition.Title = rdr["CompName"].ToString();
                    competition.GameID = Convert.ToInt32(rdr["GameID"]);
                    competition.GameTitle = rdr["GameTitle"].ToString();
                    competitions.Add(competition);
                }
                conn.Close();
            }

            return competitions;
        }

        public static PlayerModel GetPlayerInfo(string Username)
        {
            PlayerModel player = new PlayerModel();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_PlayerInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", Username);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    player.Username = rdr["PlayerUsername"].ToString();
                    player.DisplayName = rdr["PlayerDisplayName"].ToString();
                    player.Email = rdr["PlayerEmail"].ToString();
                    player.Password = rdr["PlayerPassword"].ToString();
                    player.StreamID = Convert.ToInt32(rdr["StreamID"]);
                }
                conn.Close();
            }

            return player;
        }

        public static bool IsFriend(string playerUsername, string friendUsername)
        {
            int isFriend = 0;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("is_Friend", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@playerUsername", playerUsername);
                cmd.Parameters.AddWithValue("@friendUsername", friendUsername);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    isFriend = Convert.ToInt32(rdr["Friends"]);
                }
                conn.Close();
            }

            return isFriend == 1;
        }

        public static bool IsGameOwned(string Username, int GameID)
        {
            int gameOwned = 0;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("is_GameOwned", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    gameOwned = Convert.ToInt32(rdr["Owned"]);
                }
                conn.Close();
            }

            return gameOwned == 1;
        }

        public static bool IsGameWishListed(string Username, int GameID)
        {
            int wishlisted = 0;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("is_WishListed", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    wishlisted = Convert.ToInt32(rdr["Wishlisted"]);
                }
                conn.Close();
            }

            return wishlisted == 1;
        }

        public static void AddWishlist(string Username, int GameID)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("add_WishList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@gameID", GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void RemoveWishlist(string Username, int GameID)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("remove_Wishlist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void AddFriend(string PlayerUsername, string FriendUsername)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("add_Friend", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@playerUsername", PlayerUsername);
                cmd.Parameters.AddWithValue("@friendUsername", FriendUsername);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void RemoveFriend(string PlayerUsername, string FriendUsername)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("remove_Friend", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@playerUsername", PlayerUsername);
                cmd.Parameters.AddWithValue("@friendUsername", FriendUsername);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void CreatePlayer(PlayerModel player)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("create_Player", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", player.Username);
                cmd.Parameters.AddWithValue("@play_password", player.Password);
                cmd.Parameters.AddWithValue("@display_name", player.DisplayName);
                cmd.Parameters.AddWithValue("@email", player.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void EditPlayer(PlayerModel player)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("update_Player", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", player.Username);
                cmd.Parameters.AddWithValue("@play_password", player.Password);
                cmd.Parameters.AddWithValue("@display_name", player.DisplayName);
                cmd.Parameters.AddWithValue("@email", player.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void DeletePlayer(string Username)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("remove_Account", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_name", Username);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static bool VerifyPlayer(PlayerModel player)
        {
            bool playerExists = false;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("verify_Player", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", player.Username);
                cmd.Parameters.AddWithValue("@password", player.Password);

                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    playerExists = true;
                    player.DisplayName = rdr["PlayerDisplayName"].ToString();
                    player.Permission = Convert.ToInt32(rdr["PlayerPermission"]);
                }
                conn.Close();
            }

            return playerExists;
        }

        public static int getGameID(string gameName)
        {
            int gameID = 0;
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_GameTitle", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@game_title", gameName);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    gameID = Convert.ToInt32(rdr["GameID"]);
                }
                conn.Close();
            }
            return gameID;
        }

        public static void RemoveGame(int GameID)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("remove_Game", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void CreateCompetition(CompetitionModel competition)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("create_Competition", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@compDate", competition.Date);
                cmd.Parameters.AddWithValue("@compName", competition.Title);
                cmd.Parameters.AddWithValue("@compDesc", competition.Description);
                cmd.Parameters.AddWithValue("@compGameID", competition.GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void CreateSale(SaleModel sale)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("create_Sale", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SalePercent", sale.SalePercent);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@SaleGameID", sale.SaleGameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static SaleModel GetSale(int GameID)
        {
            SaleModel sale = new SaleModel();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_Sale", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sale.SaleID = Convert.ToInt32(rdr["SaleID"]);
                    sale.SalePercent = Convert.ToInt32(rdr["SalePercent"]);
                    sale.SaleDate = Convert.ToDateTime(rdr["SaleDate"]);
                    sale.SaleGameID = Convert.ToInt32(rdr["SaleGameID"]);
                }
                conn.Close();
            }

            return sale;
        }

        public static int CreateStream(StreamModel stream)
        {
            int streamID = 0;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("create_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@streamLink", stream.Link);
                cmd.Parameters.AddWithValue("@streamTitle", stream.Title);
                cmd.Parameters.AddWithValue("@gameID", stream.GameID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    streamID = Convert.ToInt32(rdr["StreamID"]);
                }
                conn.Close();
            }

            return streamID;
        }

        public static void HostStream(string Username, int StreamID)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("host_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static StreamModel GetStreamInfo(int StreamID)
        {
            StreamModel stream = new StreamModel();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_StreamInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    stream.StreamID = Convert.ToInt32(rdr["StreamID"]);
                    stream.GameID = Convert.ToInt32(rdr["GameID"]);
                    stream.Title = rdr["StreamTitle"].ToString();
                    stream.Link = rdr["StreamLink"].ToString();
                }
                conn.Close();
            }

            return stream;
        }

        public static List<PlayerModel> GetStreamHosts(int StreamID)
        {
            List<PlayerModel> hosts = new List<PlayerModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_StreamHosts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PlayerModel host = new PlayerModel();
                    host.Username = rdr["PlayerUsername"].ToString();
                    host.DisplayName = rdr["PlayerDisplayName"].ToString();
                    hosts.Add(host);
                }
                conn.Close();
            }

            return hosts;
        }

        public static GameModel GetGameInfo(int GameID)
        {
            GameModel gameInfo = new GameModel();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_GameInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", GameID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    gameInfo.GameID = Convert.ToInt32(rdr["GameID"]);
                    gameInfo.Title = rdr["GameTitle"].ToString();
                    gameInfo.Genre = rdr["GameGenre"].ToString();
                    gameInfo.Desc = rdr["GameDesc"].ToString();
                    gameInfo.price = Convert.ToDouble(rdr["GamePrice"]);
                }
                conn.Close();
            }

            return gameInfo;
        }

        public static List<DeveloperModel> GetGameDevelopers(int GameID)
        {
            List<DeveloperModel> developers = new List<DeveloperModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_GameDevelopers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DeveloperModel developer = new DeveloperModel();
                    developer.Name = rdr["DevName"].ToString();
                    developer.Username = rdr["DevUsername"].ToString();
                    developer.ID = Convert.ToInt32(rdr["DevID"]);
                    developers.Add(developer);
                }
                conn.Close();
            }

            return developers;
        }

        public static List<ForumModel> GetGameForums(int GameID)
        {
            List<ForumModel> forums = new List<ForumModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_GameForums", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ForumModel forum = new ForumModel();
                    forum.Link = rdr["ForumLink"].ToString();
                    forum.Name = rdr["ForumName"].ToString();
                    forums.Add(forum);
                }
                conn.Close();
            }

            return forums;
        }

        public static bool IsStreamHost(int StreamID, string Username)
        {
            int relation = 0;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("is_StreamHost", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                cmd.Parameters.AddWithValue("@Username", Username);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    relation = Convert.ToInt32(rdr["Hosts"]);
                }
                conn.Close();
            }

            return relation == 1;
        }

        public static bool IsStreamFollower(int StreamID, string Username)
        {
            int relation = 0;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("is_StreamFollower", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                cmd.Parameters.AddWithValue("@Username", Username);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    relation = Convert.ToInt32(rdr["Follower"]);
                }
                conn.Close();
            }

            return relation == 1;
        }

        public static void JoinStream(int StreamID, string Username)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("join_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                cmd.Parameters.AddWithValue("@Username", Username);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void LeaveStream(int StreamID, string Username)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("leave_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                cmd.Parameters.AddWithValue("@Username", Username);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void FollowStream(int StreamID, string Username)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("follow_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                cmd.Parameters.AddWithValue("@Username", Username);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void UnfollowStream(int StreamID, string Username)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("unfollow_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                cmd.Parameters.AddWithValue("@Username", Username);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void RemoveStream(int StreamID)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("remove_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", StreamID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void EditStream(StreamModel stream)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("update_Stream", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StreamID", stream.StreamID);
                cmd.Parameters.AddWithValue("@Link", stream.Link);
                cmd.Parameters.AddWithValue("@Title", stream.Title);
                cmd.Parameters.AddWithValue("@GameID", stream.GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void CreatePurchase(PurchaseModel purchase)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("create_Purchase", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@purchaseDate", purchase.PurchaseDate);
                cmd.Parameters.AddWithValue("@Price", purchase.Price);
                cmd.Parameters.AddWithValue("@CardNum", purchase.CardNumber);
                cmd.Parameters.AddWithValue("@CardName", purchase.CardName);
                cmd.Parameters.AddWithValue("@CardExp", purchase.CardExp);
                cmd.Parameters.AddWithValue("@CardSecurity", purchase.CardSecurity);
                cmd.Parameters.AddWithValue("@Username", purchase.Username);
                cmd.Parameters.AddWithValue("@SaleID", purchase.SaleID);
                cmd.Parameters.AddWithValue("@GameID", purchase.GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static List<GameModel> GetDevGames(int DevID)
        {
            List<GameModel> games = new List<GameModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_DevGame", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dev_ID", DevID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GameModel game = new GameModel();
                    game.Title = rdr["GameTitle"].ToString();
                    game.GameID = Convert.ToInt32(rdr["GameID"]);
                    game.price = Convert.ToDouble(rdr["GamePrice"]);
                    games.Add(game);
                }
                conn.Close();
            }

            return games;
        }

        public static DeveloperModel GetDevInfo(int DevID)
        {
            DeveloperModel developer = new DeveloperModel();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_DevInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", DevID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    developer.ID = DevID;
                    developer.Name = rdr["DevName"].ToString();
                    developer.About = rdr["DevAbout"].ToString();
                    developer.Email = rdr["DevEmail"].ToString();
                    developer.Link = rdr["DevLink"].ToString();
                    developer.Phone = rdr["DevPhone"].ToString();
                    developer.Routing = rdr["DevRoutingNum"].ToString();
                    developer.Account = rdr["DevAccountNum"].ToString();
                    developer.Username = rdr["DevUsername"].ToString();
                }
                conn.Close();
            }

            return developer;
        }

        public static DeveloperModel GetDevInfo(string Username)
        {
            DeveloperModel developer = new DeveloperModel();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_DevByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", Username);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    developer.ID = Convert.ToInt32(rdr["DevID"]);
                    developer.Name = rdr["DevName"].ToString();
                    developer.About = rdr["DevAbout"].ToString();
                    developer.Email = rdr["DevEmail"].ToString();
                    developer.Link = rdr["DevLink"].ToString();
                    developer.Phone = rdr["DevPhone"].ToString();
                    developer.Routing = rdr["DevRoutingNum"].ToString();
                    developer.Account = rdr["DevAccountNum"].ToString();
                    developer.Username = Username;
                }
                conn.Close();
            }

            return developer;
        }

        public static List<GameStatModel> GetDevGameStats(int DevID)
        {
            List<GameStatModel> gameStats = new List<GameStatModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_DevGameStat", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DevID", DevID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GameStatModel gameStat = new GameStatModel();
                    gameStat.GameID = Convert.ToInt32(rdr["GameID"]);
                    gameStat.Title = rdr["GameTitle"].ToString();
                    gameStat.Purchases = Convert.ToInt32(rdr["Purchase"]);
                    gameStat.Total = Convert.ToDecimal(rdr["Total"]);
                    gameStats.Add(gameStat);
                }
                conn.Close();
            }

            return gameStats;
        }

        public static List<GenreStatModel> GetDevGenreStats(int DevID)
        {
            List<GenreStatModel> genreStats = new List<GenreStatModel>();

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("get_DevGenreStat", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DevID", DevID);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GenreStatModel genreStat = new GenreStatModel();
                    genreStat.Genre = rdr["GameGenre"].ToString();
                    genreStat.purchases = Convert.ToInt32(rdr["Purchase"]);
                    genreStat.Total = Convert.ToDecimal(rdr["Total"]);
                    genreStats.Add(genreStat);
                }
                conn.Close();
            }

            return genreStats;
        }

        public static int CreateGame(GameModel game)
        {
            int gameID = 0;

            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("create_Game", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@gameTitle", game.Title);
                cmd.Parameters.AddWithValue("@gameDesc", game.Desc);
                cmd.Parameters.AddWithValue("@gamePrice", game.price);
                cmd.Parameters.AddWithValue("@gameGenre", game.Genre);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    gameID = Convert.ToInt32(rdr["GameID"]);
                }
                conn.Close();
            }

            return gameID;
        }

        public static void EditGame(GameModel game)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("update_Game", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GameID", game.GameID);
                cmd.Parameters.AddWithValue("@gameTitle", game.Title);
                cmd.Parameters.AddWithValue("@gameDesc", game.Desc);
                cmd.Parameters.AddWithValue("@gamePrice", game.price);
                cmd.Parameters.AddWithValue("@gameGenre", game.Genre);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void AddGameDev(int DevID, int GameID)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("add_GameDev", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DevID", DevID);
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void RemoveGameDev(int DevID, int GameID)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("remove_GameDev", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DevID", DevID);
                cmd.Parameters.AddWithValue("@GameID", GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void AddGameForum(ForumModel forum)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("create_Forum", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Link", forum.Link);
                cmd.Parameters.AddWithValue("@GameID", forum.GameID);
                cmd.Parameters.AddWithValue("@Name", forum.Name);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void RemoveGameForum(ForumModel forum)
        {
            using (MySqlConnection conn = new MySqlConnection(Configuration["DBConn:ConnectionString"]))
            {
                MySqlCommand cmd = new MySqlCommand("remove_Forum", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Link", forum.Link);
                cmd.Parameters.AddWithValue("@GameID", forum.GameID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
