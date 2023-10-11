using ContactsApi.Models;
using ContactsApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace  ContactsApi.Controllers
{
    public class ContactsController : Controller
    {
        [HttpGet]
        public ActionResult AddNewContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewContact(ContactViewModel contact)
        {
           bool addstate = GooglePeopleApiHelper.AddContact(contact);
            if (addstate)
            {
                ViewBag.IsSuccess= "successful";
            }
            else
            {
                ViewBag.IsSuccess = "unsuccessful";
            }
            return View();
        }

    }
}