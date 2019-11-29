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

            return streams;
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

    }
}
