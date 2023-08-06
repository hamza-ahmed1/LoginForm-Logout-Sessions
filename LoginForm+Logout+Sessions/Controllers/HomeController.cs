using LoginForm_Logout_Sessions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LoginForm_Logout_Sessions.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginDbContext contextvar;

        public HomeController(LoginDbContext contextvar)
        {
            this.contextvar = contextvar;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Login(TblLoginDatum user)
        {

            if (ModelState.IsValid)
            {
                var _user = await contextvar.TblLoginData.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefaultAsync();
                if (_user != null)
                {
                    HttpContext.Session.SetString("_user", _user.UserName);
                    return RedirectToAction("UserDashBoard");
                }
                else
                {
                    TempData["notfound"] = "User Not Found";
                }
               
                
               
            }
            else
            {
               
               

            }
            return View();








        }
        public IActionResult UserDashBoard()
        {
            ViewBag.UserSession=HttpContext.Session.GetString("_user");
            if (ViewBag.UserSession == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult About()
        {
            var _user=HttpContext.Session.GetString("_user");
            ViewBag.Data = _user;
            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Remove("_user");
            return RedirectToAction("Login");
            
        }
        public IActionResult Register()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Register(TblLoginDatum _newuser)
        {

            if (_newuser.UserName == null)
            {
                HttpContext.Session.SetString("_user", "User name is reqired !");
                TempData["UserError"] = HttpContext.Session.GetString("_user");
                return RedirectToAction("Register");
            }
            if (ModelState.IsValid)
            {
                var _user = await contextvar.TblLoginData.Where(x => x.Email == _newuser.Email).FirstOrDefaultAsync();
                if (_user != null)
                {
                    HttpContext.Session.SetString("_user", "User already exist ! Try with another Email ");
                    TempData["UserError"] = HttpContext.Session.GetString("_user");
                    return RedirectToAction("Register");
                }
                else
                {

                   await contextvar.TblLoginData.AddAsync(_newuser);
                   await contextvar.SaveChangesAsync();
                    return RedirectToAction("Login");

                }
            }
        
            return View();
         

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}