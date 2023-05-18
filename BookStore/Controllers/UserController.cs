using BookStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly BookDbContext _context;

        public UserController(BookDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        
        public async Task<ActionResult> Login(string username,string password, string ReturnUrl)
        {
            
            
            if (ModelState.IsValid)
            {
                 var tempuser = _context.Users.Include(a => a.Roles).Where(b => b.username == username && b.password == password).FirstOrDefault();
                if (tempuser != null)
                {
                    Claim c1 = new Claim(ClaimTypes.Name, tempuser.username);
                    Claim c2 = new Claim(ClaimTypes.Email, tempuser.email);
                    var role = tempuser.Roles.FirstOrDefault();
                    Claim c3 = new Claim("A", "A");
                    if (role != null)
                        c3 = new Claim(ClaimTypes.Role, role.Name);
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsIdentity.AddClaims(new List<Claim> { c1, c2, c3 });
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (ReturnUrl != null)
                        return LocalRedirect(ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "invalid user name or password");
                    return View(tempuser);
                }
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        public async  Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index),"Home");
            }
            return View(user);
        }
       
        public ActionResult Index()
        {
     
            return View(_context.Users.ToList());
        }
        public ActionResult AddRole(int Id)
        {
            var user = _context.Users.Include(a => a.Roles).FirstOrDefault(a => a.Id == Id);
            var roles = _context.Roles.ToList().Except(user.Roles);
            ViewBag.roles = new SelectList(roles, "Name", "Name");
            ViewBag.mine = new SelectList(user.Roles, "Name", "Name");
            return View(user);
        }
        [HttpPost]
        public ActionResult AddRole(int Id, string[] roles, string[] mine) 
        {
            var user = _context.Users.Include(b=>b.Roles).FirstOrDefault(a => a.Id == Id);
            foreach (var item in roles)
            {
                var temprole = _context.Roles.FirstOrDefault(b => b.Name == item);
                user.Roles.Add(temprole);
            }
            foreach (var item in mine)
            {
              var temp=  _context.Roles.FirstOrDefault(b => b.Name==item);
               bool x= user.Roles.Remove(temp);

            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult test()
        {
            return Json("A");
        }
    }
}
