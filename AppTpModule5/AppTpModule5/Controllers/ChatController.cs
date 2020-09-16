using AppTpModule5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppTpModule5.Controllers
{
    public class ChatController : Controller
    {
        static List<Chat> ListedeChats;

        public ChatController()
        {
            if(ListedeChats==null)
            {
                ListedeChats = Chat.GetMeuteDeChats();
            }
        }
        // GET: Chat
        public ActionResult Index()
        {
            return View(ListedeChats);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            var Chat = ListedeChats.FirstOrDefault(I => I.Id == id);
            if (Chat != null)
            {
                return View(Chat);
            }
            return RedirectToAction("Index");
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            var Chat = ListedeChats.FirstOrDefault(I => I.Id == id);
            if(Chat != null)
            {
                return View(Chat);
            }
            return RedirectToAction("Index");
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var Chat = ListedeChats.FirstOrDefault(I => I.Id == id);
                if (Chat != null)
                {
                    ListedeChats.Remove(Chat);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            var Chat = ListedeChats.FirstOrDefault(I => I.Id == id);
            if (Chat != null)
            {
                return View(Chat);
            }
            return RedirectToAction("Index");
        }

        // POST: Chat/Edit/5
        [HttpPost]
        public ActionResult Edit(Chat chat)
        {
            Chat Chat = ListedeChats.FirstOrDefault(I => I.Id == chat.Id);
            Chat.Nom = chat.Nom;
            Chat.Couleur = chat.Couleur;
            Chat.Age = chat.Age;
            return RedirectToAction("Index");
        }
    }
}
