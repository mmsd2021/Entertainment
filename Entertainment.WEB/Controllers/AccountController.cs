using Entertainment.DataAccess.Dtos;
using Entertainment.DataAccess.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Entertainment.WEB.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly IAccountRepository accountRepository;

        ILog log = LogManager.GetLogger(typeof(AccountController));
        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null || !ModelState.IsValid)
                {
                    return View(loginDto);
                }

                UserDto userDto = await accountRepository.GetUserByUsernameAndPassword(loginDto.UserName, loginDto.Password);

                if (userDto == null || (userDto.login_user_id == 0))
                {
                    ModelState.AddModelError("","Invalid username or password");
                    return View(loginDto);
                }

                var menuListDtos = await accountRepository.GetUserMenuList(Convert.ToInt64(userDto.group_id));

                Session["menulist"] = menuListDtos;
                Session["user"] = userDto;

                //FormsAuthentication.SetAuthCookie(loginDto.UserName, true);
                //if (Request.QueryString["ReturnUrl"] != null)
                //{
                //    return Redirect(Request.QueryString["ReturnUrl"].ToString());
                //}

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                log.Error("AccountController > LoginAsync", ex);
                ModelState.AddModelError("", "Technical errors occured, Please try again later.");
                return View(loginDto);
            }
        }
    }
}