using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.Model;
using MyBookShopDao.BLL;
using System.Drawing;//绘图
using System.IO;
using System.Web.Security;//流

namespace MyBookShopWeb.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string id, string pwd, string returnUrl)
        {
            User user = new UserManager().Login(id, pwd);
            //if (user != null)
            //{
            //    Session["user"] = user;
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ViewBag.Msg = "请填写用户名!";
            //    ViewBag.Msg1 = "请填写密码!";
            //    return View();
            //}
            if (user != null)
            {
                Session["user"] = user;
                //FormsAuthentication.SetAuthCookie(user.Name, false);
                //手动创建身份票据
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (1, user.Name, DateTime.Now, DateTime.Now.AddMinutes(20), false, "Admin");
                //加密身份票据
                string enc = FormsAuthentication.Encrypt(ticket);
                //创建cookie
                HttpCookie cook = new HttpCookie(FormsAuthentication.FormsCookieName, enc);
                //添加 cookie
                Response.Cookies.Add(cook);
                //重定向
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
                
            else
            {
                ViewBag.Msg = "请填写用户名!";
                ViewBag.Msg1 = "请填写密码!";
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)//验证通过
            {
                new UserManager().AddUser(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        //验证码异步判断
        public string Check()
        {
            //获取post请求的数据
            string code = Request.Form["code"].ToString();
            if (code == Session["code"].ToString())
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //验证码
        public ActionResult ValidateCode()
        {
            Random ran = new Random();
            string code = ran.Next(1000, 9999).ToString();
            Session["code"] = code;
            //绘制验证码
            //定义位图
            Bitmap map = new Bitmap(80, 30);
            //获取图形上下文
            Graphics gx = Graphics.FromImage(map);
            //绘制填充扥矩形框
            gx.FillRectangle(new SolidBrush(Color.White), 1, 1, map.Width - 2, map.Height - 2);

            //绘制字符串
            gx.DrawString(code, new Font("宋体", 16), new SolidBrush(Color.Green), 5, 5);

            //绘制噪音线
            for (int i = 1; i <= 5; i++)
            {
                gx.DrawLine(new Pen(new SolidBrush(Color.Blue)), 0, i * 10, map.Width, i * 10);
            }
            //内存中的图像存放在二进制流
            MemoryStream ms = new MemoryStream();
            map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            gx.Dispose();//释放资源

            //输出到网页
            return File(ms.ToArray(), "image/jpeg");
        }
    }
}
