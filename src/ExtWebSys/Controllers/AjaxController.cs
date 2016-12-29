using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using NModel;
using Tool;

namespace WebSys.Controllers
{
	public class AjaxController : Controller
	{
		public ActionResult GSendToEMailCode()
		{
			NCaptcha.Generate(NModel.EnObject.UserCodeSessionName);

			DateTime? dt = (DateTime?)NModel.EnObject.GetTimeValue;

			{
				try
				{
					DateTime? dt1 = DateTime.Now;
					TimeSpan? ts = dt1 - dt;

					double s = 0;
					if (ts != null)
					{
						s = ts.Value.TotalSeconds;
					}

					if (s >= 72 || ts == null)
					{
						NModel.DB_User model = NModel.EnObject.GetFindUserValue as NModel.DB_User;
						string toEmail = model.User_Email + "";
						string code = NModel.EnObject.GetCodeValue;

						bool isOk = Tool.NEmail.SendTo("smtp.126.com", "dict020@126.com",
							 "87658543", "你找谁", toEmail, "",
							 "验证码:" + code, "你找谁找回密码验证码");
						if (isOk)
						{
							NModel.EnObject.SetEmailValue = toEmail;

							NModel.EnObject.SetTimeValue = DateTime.Now;
							this.Response.Write("{ \"info\":\"已发送到邮箱,请查看验证码\", \"status\":\"y\" }");
						}
						else
						{
							this.Response.Write("{ \"info\":\"发送失败\", \"status\":\"n\" }");
						}
					}
					else
					{
						this.Response.Write("{ \"info\":\"发送时间没到" + s + "秒\", \"status\":\"n\" }");
					}
				}
				catch (Exception ex)
				{
				}

				return View();
			}
		}

		public ActionResult SendToEMailCode()
		{
			NCaptcha.Generate(NModel.EnObject.UserCodeSessionName);

			DateTime? dt = (DateTime?)NModel.EnObject.GetTimeValue;

			try
			{
				DateTime? dt1 = DateTime.Now;
				TimeSpan? ts = dt1 - dt;

				double s = 0;
				if (ts != null)
				{
					s = ts.Value.TotalSeconds;
				}

				if (s >= 62 || ts == null)
				{
					string toEmail = this.Server.UrlDecode(this.Request["to"]);
					string code = NModel.EnObject.GetCodeValue;

					BLL.DB_WebConfig bll_WebConfig = new BLL.DB_WebConfig();

					NModel.DB_WebConfig model_WebConfig = bll_WebConfig.GetModel((long?)4);

					model_WebConfig = model_WebConfig != null ? model_WebConfig : new NModel.DB_WebConfig();
					bll_WebConfig.Close();
					bool isOk = Tool.NEmail.SendTo(model_WebConfig.WebConfig_ServerAdrress, model_WebConfig.WebConfig_SendEmail,
						 model_WebConfig.WebConfig_SendPw, model_WebConfig.WebConfig_SendName + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"), toEmail, model_WebConfig.WebConfig_ToName,
						 model_WebConfig.WebConfig_SubContent + "验证码:" + code, model_WebConfig.WebConfig_SubTitle + "" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"));
					if (isOk)
					{
						NModel.EnObject.SetEmailValue = toEmail;

						NModel.EnObject.SetTimeValue = DateTime.Now;
						this.Response.Write("{ \"info\":\"已发送到邮箱,请查看验证码 邮箱->" + toEmail + "\" , \"status\":\"y\" }");
					}
					else
					{
						this.Response.Write("{ \"info\":\"发送失败\", \"status\":\"n\" }");
					}
				}
				else
				{
					this.Response.Write("{ \"info\":\"发送时间没到" + s + "秒\", \"status\":\"n\" }");
				}
			}
			catch (Exception ex)
			{
			}

			return View();
		}

		//
		// GET: /Ajax/
		public ActionResult UserExistEmail()
		{
			string email = this.Request.Form["param"];
			BLL.DB_User bll_user = new BLL.DB_User();

			bool isHasName = bll_user.ExistsEmail(email);
			if (isHasName)
			{
				this.Response.Write("{ \"info\":\"Email已存在\", \"status\":\"n\" }");
			}
			else
			{
				this.Response.Write("{ \"info\":\"Email已通验证\", \"status\":\"y\" }");
			}

			bll_user.Close();

			return View();
		}

		public ActionResult NUserYesNoCode()
		{
			string validatecode = this.Request.Form["param"];
			bool isCodeYesNo = (validatecode + "").Trim().ToLower().Equals(NModel.EnObject.GetCodeValue.Trim().ToLower());
			if (!isCodeYesNo)
			{
				this.Response.Write("{ \"info\":\"验证码不正确\", \"status\":\"n\" }");
			}
			else
			{
				this.Response.Write("{ \"info\":\"验证码已通验证\", \"status\":\"y\" }");
			}
			return View();
		}

		public ActionResult UserYesNoCode()
		{
			string validatecode = this.Request.Form["param"];
			bool isCodeYesNo = (NTool.IsSessionEquals(NModel.EnObject.UserCodeSessionName, validatecode));
			if (!isCodeYesNo)
			{
				this.Response.Write("{ \"info\":\"验证码不正确\", \"status\":\"n\" }");
			}
			else
			{
				this.Response.Write("{ \"info\":\"验证码已通验证\", \"status\":\"y\" }");
			}
			return View();
		}

		public ActionResult UserExistName()
		{
			string name = this.Request.Form["param"];
			BLL.DB_User bll_user = new BLL.DB_User();

			bool isHasName = bll_user.ExistsName(name);
			if (isHasName)
			{
				this.Response.Write("{ \"info\":\"用户已存在\", \"status\":\"n\" }");
			}
			else
			{
				this.Response.Write("{ \"info\":\"用户名已通验证\", \"status\":\"y\" }");
			}

			bll_user.Close();

			return View();
		}

		public ActionResult ImgCaptcha()
		{
			string color = this.Request.QueryString["color"];
			;
			this.Response.ContentType = "image/jpeg";
			EnImage enImg = new EnImage();
			enImg.ImgFont = new Font(FontFamily.GenericSerif, 30, GraphicsUnit.Pixel);

			try
			{
				enImg.ImgColor = System.Drawing.ColorTranslator.FromHtml("#009B4C");
			}
			catch (Exception ex)
			{
				enImg.ImgColor = EnObject.listColor[7];
			}

			enImg.Height = 40;
			enImg.Width = 120;

			string count = this.Request.QueryString["ct"];

			if (string.IsNullOrEmpty(count))
			{
				count = "4";
			}
			if (!new Regex(@"\d{0,127}").IsMatch(count))
			{
				count = "4";
			}

			NCaptcha cca = NCaptcha.GenerateAndDraw(byte.Parse(count), this.Response.OutputStream, enImg, true);
			NModel.EnObject.SetCodeValue = cca.GenerateString;
			NModel.EnObject.CaptchaSessionName = cca.SetSessionName;

			return View();
		}

		public ActionResult AjaxCaptcha()
		{
			this.Response.ContentType = "image/jpeg";
			EnImage enImg = new EnImage();
			enImg.ImgFont = new Font(FontFamily.GenericSerif, 21, GraphicsUnit.Pixel);
			enImg.ImgColor = EnObject.listColor[0];
			enImg.Height = 30;
			enImg.Width = 80;

			string count = this.Request.QueryString["ct"];

			if (string.IsNullOrEmpty(count))
			{
				count = "4";
			}
			if (!new Regex(@"\d{0,127}").IsMatch(count))
			{
				count = "4";
			}

			NCaptcha cca = NCaptcha.GenerateAndDraw(byte.Parse(count), this.Response.OutputStream, enImg, true);

			NModel.EnObject.CaptchaSessionName = cca.SetSessionName;

			return View();
		}

		public ActionResult UserLogout()
		{
			this.Response.ContentType = "text/plain";

			if (BLL.Fun.ClearLogin_User())
			{
				return RedirectToAction("index", "nlcq");
			}

			return View();
		}

		public ActionResult AjaxAdLogin()
		{
			this.Response.ContentType = "text/plain";

			string name = this.Request.Form["name"];
			string password = this.Request.Form["password"];
			string checkcode = this.Request.Form["checkcode"];
			string action = this.Request.QueryString["action"];

			try
			{
				if (BLL.Fun.Auto_Login_Admin() > 0)
				{
					//登录没过期
					this.Response.Write(Tool.NMsg.SetMsg("登录没过期！", "8"));
					this.Response.End();
				}
				else if (Tool.NTool.IsSessionEquals(NModel.EnObject.CaptchaSessionName, checkcode))
				{
					BLL.Admin_User dal_User = new BLL.Admin_User();
					if (dal_User.CheckLogin(name, password, checkcode))
					{
						this.Response.Write(Tool.NMsg.SetMsg("登录中请稍后……", "1"));
					}
					else
					{
						this.Response.Write(Tool.NMsg.SetMsg("账号或密码错误！", "0"));
					}
					dal_User.Close();
				}
				else
				{
					this.Response.Write(Tool.NMsg.SetMsg("验证码错误！", "2"));
				}
			}
			catch (Exception ex)
			{
				this.Response.Write(Tool.NMsg.SetMsg("错误=" + ex.Message, "7"));
			}
			finally
			{
				Tool.NCaptcha.Generate(true);
				;
			}

			return View();
		}

		private bool iss = false;

		public ActionResult AjaxAdLogout()
		{
			this.Response.ContentType = "text/plain";

			if (BLL.Fun.ClearLogin())
			{
				return RedirectToAction("login", "LCQ");
			}

			return View();
		}
	}
}