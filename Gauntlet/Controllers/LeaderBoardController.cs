using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gauntlet.Models;
namespace Gauntlet.Controllers
{
    public class LeaderBoardController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LeaderBoard
        public ActionResult LeaderBoard()
        {
            var user = db.Users.ToList();
            return View(db.Users.OrderByDescending(x => x.Points));         


        }
    }
}