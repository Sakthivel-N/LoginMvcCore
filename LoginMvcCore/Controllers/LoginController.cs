using Microsoft.AspNetCore.Mvc;
using LoginMvcCore.Models;
using System.Linq;


namespace LoginMvcCore.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(ILogger<LoginController> logger,AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        
        

        private readonly ILogger<LoginController> _logger;

        //public LoginController(ILogger<LoginController> logger)
        //{
        //    _logger = logger;
        //}


        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                var sessionval = HttpContext.Session.GetString("Email");
                ViewBag.SessionUser = sessionval;

                return View();

            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserLogInfo user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                ViewBag.RegMessage = "Success";
                return View();
            }
            else
            {
                ViewBag.RegMessage = "Failed";
                return View();
            }

        }
        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(UserLogInfo user)
        {
            var checkUser =  _context.UserLogInfos.Where(a => a.Email.Equals(user.Email) &&
                                    a.Password.Equals(user.Password)).FirstOrDefault();
            if (checkUser != null)
            {


                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                {
                    HttpContext.Session.SetString("Email", checkUser.Email);

                }
                ViewBag.SessionMsg = HttpContext.Session.GetString("Email");

                return RedirectToAction("UserDashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed";
                return View();
            }
            
        }
        
        public IActionResult UserDashboard()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                var sessionval = HttpContext.Session.GetString("Email");
                ViewBag.SessionUser = sessionval;

                var details = _context.UserLogInfos.FirstOrDefault(m => m.Email == sessionval.ToString());
                return View(details);

            }

            return RedirectToAction("Index");
            
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            return View() ;
        }
    }
}
