using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElvishTranslator.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElvishTranslator.Controllers
{
    public class PetController : Controller
    {
        //Initiate dbclass and get all data (list)
        public IActionResult IndexList()
        {
            DBclass dbclass = new DBclass();
            var pets = dbclass.getPets();

            //Returns view with list
            return View(pets);
        }      
    }
}