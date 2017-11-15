using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KnockoutStudy.Models;
using KnockoutStudy.Model;

namespace KnockoutStudy.Controllers
{
    public class HomeController : Controller
    {
        private KnockOutEntities koCxt = new KnockOutEntities();

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public JsonResult Save(Person person)
        {
            koCxt.ExecuteStoreCommand("DELETE Friend");
            
            foreach (Models.Friend frnd in person.Friends)
            {                
                //if (!IsAFriend(frnd.Name))
                //{
                    Model.Friend friend = new Model.Friend();
                    friend.Name = frnd.Name;
                    friend.IsOnTwitter = frnd.IsOnTwitter;
                    friend.TwitterName = frnd.TwitterName;

                    koCxt.Friend.AddObject(friend);
                    koCxt.SaveChanges();
            }

            var message = string.Format("Saved {0} {1}", person.FirstName, person.LastName);
            message += string.Format(" with {0} friends", person.Friends.Count);
            message += string.Format(" ({0} on Twitter)", person.Friends.Where(x => x.IsOnTwitter).Count());

            return Json(new { message });
        }

        public JsonResult GetFriendsList()
        {
            var frLinq = from c in koCxt.Friend
                         select c;

            return Json(frLinq);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
