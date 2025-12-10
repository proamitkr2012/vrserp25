using VRSDATA.Entities;
using VRSMODEL;
using Serilog;
using VRSMODEL.DTO;
using VRSREPO;
using VRSREPO.Utilities;
using VRSAPPUI.Helpers;
using VRSAPPUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Newtonsoft.Json;
using VRSAPPUI.Extensions;
using Microsoft.AspNetCore.Antiforgery;
using System.Globalization;
//using Rotativa.AspNetCore.Options;
//using Rotativa.AspNetCore;
using QRCoder;
using System.Drawing;
using VRSMODEL.DTO.Result;
using Castle.Core.Internal;


namespace VRSAPPUI.Controllers
{
    [CanonicalActionFilter]
    public class HomeController : BaseController
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        SmsClient sms = new SmsClient();
        int pageSize;
        int pageSizeJobs;

        private IMailClient _mailClient;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IConfiguration _config, IHttpContextAccessor _httpContextAccessor, IUnitOfWork uow, IMailClient mailClient, ILogger<HomeController> logger) : base(_httpContextAccessor, _config, uow)
        {
            httpContextAccessor = _httpContextAccessor;
            _mailClient = mailClient;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            ResultDTO_DASH data = await UOF.IAdminMaster.GetResultDTO_DASH("","",false,0,"");           
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string CourseType = "", bool IsPG = false, int CourseID = 0, string ExamTypeName = "", string SessionName = "" ,string CourseName="")
        {
            
            ResultDTO_DASH data = await UOF.IAdminMaster.GetResultDTO_DASH("", CourseType, false, CourseID,SessionName);


            data.CourseType = CourseType;
            data.CourseID = CourseID;
            data.CourseName = CourseName;
            data.ExamTypeName = ExamTypeName;
            data.SessionName = SessionName;
            data.IsPG = IsPG;
            return View(data);
        }
        public IActionResult SignIn()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<ActionResult> SignOut()
        {
            if (CurrentUser != null)
            {
                if (CurrentUser.Roles.Contains("Student") == true)
                {
                    string[] Roles = CurrentUser != null ? CurrentUser.Roles : new string[] { "" };

                    await httpContextAccessor.HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string[] Roles = CurrentUser != null ? CurrentUser.Roles : new string[] { "" };

                    await httpContextAccessor.HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    return RedirectToAction("Login", "Account", new { Area = "Admin" });

                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SearchResult([FromBody] SearchDTO model)
        {
            try
            {
                Session.SetObject("resultdata", model);

                if (model.RollNumber.Trim() != "")
                {
                    if (string.IsNullOrEmpty(model.FName))
                    {
                        var datet = Convert.ToDateTime(model.DOBstr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                        model.DOB = datet;
                    }

                    var adminData = await UOF.IAdminMaster.CheckStudentData(model);
                    if (adminData.ROLL_NO == model.RollNumber)
                    {
                        adminData.EncrptedRoll = AESEncription.Base64Encode(adminData.ROLL_NO);


                        return Json(new { data = adminData, res = "success" });
                    }

                }
                else
                {
                    return Json("0");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }
            return Json("Internal error!");
        }

        [HttpPost]
        public async Task<IActionResult> VisitCountSet([FromBody] StudentMasterDTO model)
        {
            try
            {
                if (model.ROLL_NO.Trim() != "")
                {
                    var FormResponse = await UOF.IAdminMaster.VisitCountSet(model);
                    if (FormResponse.ResponseCode == 1)
                    {
                        return Json(new { res = "success" });
                    }

                }
                else
                {
                    return Json("0");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }
            return Json("Internal error!");
        }


        [Route("~/result/{data}")]
        public async Task<IActionResult> Result(string data)
        {
            try
            {

                var s = Session.GetObject<SearchDTO>("resultdata");
                var roll = AESEncription.Base64Decode(data);
                if (roll == s.RollNumber)
                {
                    StudentMasterDTO adminData = new StudentMasterDTO();
                    adminData.ROLL_NO = s.RollNumber;
                    if (string.IsNullOrEmpty(s.FName))
                    {
                        var datet = Convert.ToDateTime(s.DOBstr, CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                        adminData.DOB = datet;
                    }

                   // StudentMasterDTO adminData = await UOF.IAdminMaster.ResultStudentData("Search", s);
                    if (roll == adminData.ROLL_NO)
                    {
                        //adminData.MarksList = await UOF.IAdminMaster.ResultMarksStudentData(s);
                        //adminData.ResultList = await UOF.IAdminMaster.ResultDataStudentData(s);
                        //adminData.CSPList = await UOF.IAdminMaster.CSVDataStudentData(adminData);
                        adminData.EncrptedRoll = data;
                        return View(adminData);
                        //return new ViewAsPdf("Result", adminData)
                        //{
                        //    FileName = adminData.ROLL_NO + "_" + "Result.pdf",
                        //    //PageMargins = new Margins(10, 1, 0, 0),
                        //    PageOrientation = Orientation.Portrait,
                        //};
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }

        [Route("~/resultpreview/{data}")]
        public async Task<IActionResult> ResultPreview(string data)
        {
            try
            {

                var roll = AESEncription.Base64Decode(data);
                var s = roll.Split("$");


                SearchDTO model = new SearchDTO();
                model.CourseName = s[0].ToString();
                model.RollNumber = s[1].ToString();
                model.CourseType = s[2].ToString();
                model.ExamTypeName = s[3].ToString();
                model.SessionName = s[4].ToString();
                model.HELD_IN = s[5].ToString();
                if (s.Length > 6)
                {
                    model.IsAdmin = s[6].ToString();
                }
                else
                {
                    model.IsAdmin = "0";
                }

                model.FName = "dsa";
                if (!string.IsNullOrEmpty(model.RollNumber))
                {
                    Session.SetObject("resultdata", model);
                    data = AESEncription.Base64Encode(model.RollNumber);
                    return RedirectToAction("Result", "Home", new { data = data });
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }
        //[Route("~/result/{data}")]

        [NonAction]
        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
       



    }
}