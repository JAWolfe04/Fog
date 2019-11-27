using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Fog.Models;

namespace Fog.Controllers
{
    public class StreamController : Controller
    {
        public IActionResult StreamInfo(int StreamID)
        {
            StreamInfoModelView streamInfo = new StreamInfoModelView();

            if (HttpContext.Session.Get("Username") != null)
            {
                string Username = HttpContext.Session.GetString("Username");

                if (StreamID == 0)
                    StreamID = DataLibrary.DataAccess.SQLDataAccess.GetPlayerInfo(Username).StreamID;;
                streamInfo.IsHost = DataLibrary.DataAccess.SQLDataAccess.IsStreamHost(StreamID, Username);
                streamInfo.IsFollower = DataLibrary.DataAccess.SQLDataAccess.IsStreamFollower(StreamID, Username); ;
            }
            else
            {
                streamInfo.IsHost = false;
                streamInfo.IsFollower = false;
            }

            streamInfo.stream = DataLibrary.DataAccess.SQLDataAccess.GetStreamInfo(StreamID);
            streamInfo.game = DataLibrary.DataAccess.SQLDataAccess.GetGameInfo(streamInfo.stream.GameID);
            streamInfo.players = DataLibrary.DataAccess.SQLDataAccess.GetStreamHosts(StreamID);

            return View(streamInfo);
        }

        public IActionResult CreateStream()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStream(StreamModel stream)
        {
            int gameID = DataLibrary.DataAccess.SQLDataAccess.getGameID(stream.GameTitle);

            if (gameID < 0)
                ModelState.AddModelError("Game", "The game was not found. Please check the game and try again.");
                
            if(ModelState.IsValid)
            {
                DataLibrary.Models.StreamModel streamData = new DataLibrary.Models.StreamModel();
                streamData.Title = stream.Title;
                streamData.Link = stream.Link;
                streamData.GameID = gameID;

                int streamID = DataLibrary.DataAccess.SQLDataAccess.CreateStream(streamData);
                string Username = HttpContext.Session.GetString("Username");

                DataLibrary.DataAccess.SQLDataAccess.HostStream(Username, streamID);

                return RedirectToAction("PlayerHome", "Home");
            }
            
            return View(stream);
        }
        
        public IActionResult EditStream(int StreamID)
        {
            DataLibrary.Models.StreamModel streamData = DataLibrary.DataAccess.SQLDataAccess.GetStreamInfo(StreamID);
            StreamModel streamView = new StreamModel();
            streamView.StreamID = StreamID;
            streamView.GameTitle = DataLibrary.DataAccess.SQLDataAccess.GetGameInfo(streamData.GameID).Title;
            streamView.Link = streamData.Link;
            streamView.Title = streamData.Title;
            return View(streamView);
        }

        [HttpPost]
        public IActionResult EditStream(StreamModel stream)
        {

            int gameID = DataLibrary.DataAccess.SQLDataAccess.getGameID(stream.GameTitle);

            if (gameID < 0)
                ModelState.AddModelError("Game", "The game was not found. Please check the game and try again.");

            if (ModelState.IsValid)
            {
                DataLibrary.Models.StreamModel streamData = new DataLibrary.Models.StreamModel();
                streamData.Title = stream.Title;
                streamData.Link = stream.Link;
                streamData.GameID = gameID;
                streamData.StreamID = stream.StreamID;

                DataLibrary.DataAccess.SQLDataAccess.EditStream(streamData);

                return RedirectToAction("StreamInfo", "Stream", new { StreamID = stream.StreamID });
            }

            return View(stream);
        }

        public IActionResult LeaveStream(int StreamID)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.LeaveStream(StreamID, Username);
            if (DataLibrary.DataAccess.SQLDataAccess.GetStreamHosts(StreamID).Count == 0)
            {
                DataLibrary.DataAccess.SQLDataAccess.RemoveStream(StreamID);
                return RedirectToAction("PlayerHome", "Home");
            }
            return RedirectToAction("StreamInfo", "Stream", new { StreamID = StreamID });
        }

        public IActionResult UnfollowStream(int StreamID)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.UnfollowStream(StreamID, Username);
            return RedirectToAction("StreamInfo", "Stream", new { StreamID = StreamID });
        }

        public IActionResult FollowStream(int StreamID)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.FollowStream(StreamID, Username);
            return RedirectToAction("StreamInfo", "Stream", new { StreamID = StreamID });
        }

        public IActionResult JoinStream(int StreamID)
        {
            if (HttpContext.Session.Get("Username") == null)
                return RedirectToAction("Index", "Login");

            string Username = HttpContext.Session.GetString("Username");
            DataLibrary.DataAccess.SQLDataAccess.JoinStream(StreamID, Username);
            return RedirectToAction("StreamInfo", "Stream", new { StreamID = StreamID });
        }
    }
}