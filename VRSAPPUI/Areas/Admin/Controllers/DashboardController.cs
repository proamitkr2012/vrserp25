using Castle.Core.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using VRSAPPUI.Controllers;
using VRSDATA.Entities;
using VRSMODEL;
using VRSMODEL.DTO;
using VRSMODEL.DTO.Result;
using VRSMODEL.Enum;
using VRSREPO;
using VRSREPO.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VRSAPPUI.Areas.Admin.Controllers
{
    //[Area("Admin")]
    public class DashboardController : BaseController
    {
        int pageSize;
        private IWebHostEnvironment environment;
        public string ImgCloudPath = "";
        private IMailClient _mailClient;
        private readonly ILogger<HomeController> _logger;
        public DashboardController(IHttpContextAccessor _httpContextAccessor, IConfiguration _config,
             IWebHostEnvironment _environment, IUnitOfWork uow, IMailClient mailClient,ILogger<HomeController> logger) : base(_httpContextAccessor, _config, uow)
        {
            environment = _environment;
            string _pageSize = "15";//Convert.ToString(WebConfigSetting.PageSize);
            int PS;
            bool result = Int32.TryParse(_pageSize, out PS);
            pageSize = (result == true) ? PS : 15;
            _mailClient = mailClient;
            _logger = logger;
        }
        [Route("~/admin/dashboard")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            COURSE_MASTER_AM_DTO_DASH data = new COURSE_MASTER_AM_DTO_DASH();
               //data = await UOF.IAdminMaster.GET_COURSE_MASTER_AM((int)CurrentUser.UserId);
            return View(data);
        }

        //[Route("~/admin/dashboard/checkresultdata")]
        public async Task<IActionResult> CheckResultData(string RollNumber = "")
        {
            bool IsAdmin = false;
            if (CurrentUser.Roles.Contains("Admin"))
            {
                IsAdmin = true;
            }
            List<StudentMasterDTO> adminData = new List<StudentMasterDTO>();
            //return ViewComponent("PageAdmin", new { RollNumber, IsAdmin });
            if (!string.IsNullOrEmpty(RollNumber))
            {

                adminData = await UOF.IAdminMaster.CheckResult("", RollNumber.Trim(), IsAdmin);

                if (adminData.Count() > 0)
                {
                    // return View("~/areas/admin/views/components/_pageadmin.cshtml", adminData);
                    return PartialView("_PageData", adminData);
                }
            }
            return PartialView("_PageData", adminData);
        }

        public async Task<IActionResult> GetMarksData(string roll, string sem_no, int course_id, string sessionname, string exam_type)
        {
            bool IsAdmin = false;
            if (CurrentUser.Roles.Contains("Admin"))
            {
                IsAdmin = true;
            }
            List<STUDENT_MARKS_AMDTO> adminData = new List<STUDENT_MARKS_AMDTO>();
            //return ViewComponent("PageAdmin", new { RollNumber, IsAdmin });
            if (!string.IsNullOrEmpty(roll))
            {

                adminData = await UOF.IAdminMaster.MarkAdminDetails("", roll, sem_no, course_id, sessionname, exam_type);

                if (adminData.Count() > 0)
                {
                    // return View("~/areas/admin/views/components/_pageadmin.cshtml", adminData);
                    return PartialView("_MarksData", adminData);
                }
            }
            return PartialView("_MarksData", adminData);
        }

        public async Task<IActionResult> COURSE_MASTER(int Page=1,string Search=null)
        {
            try
            {
                COURSE_MASTER_AM_DTO_DASH data = new COURSE_MASTER_AM_DTO_DASH();
                data = await UOF.IAdminMaster.GET_COURSE_MASTER_AM("", (int)CurrentUser.UserId, Page, Search);
                ViewBag.Search = Search;
                return View(data);
            }
            catch (Exception e)
            {
                Log.Information("course page!" + e);
            }
            return View();

        }
       
        [HttpGet]
        public async Task<IActionResult> ManageCourseMaster()
        {
            MANAGE_COURSE_MASTER_AM_DTO data = new MANAGE_COURSE_MASTER_AM_DTO();
            data = await UOF.IAdminMaster.GET_MANAGECOURSEMASTER_AM("",(int)CurrentUser.UserId);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCourse([FromForm] COURSE_MASTER_AM_DTO model, string ReturnUrl)
        {
            try
            {
                if (model.COURSE_ID_MAIN >0)
                {
                    
                    FormResponse d = await UOF.IAdminMaster.SaveCourse_AM("",model);
                   
                        return Json(d);
                   
                    
                }
                else
                {
                    return Json("Try Again Later!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        public async Task<string> Geoloc()
        {
            try
            {
                string _ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                if (_ipAddress.Contains("::1"))
                    _ipAddress = "127.0.0.1";

                return _ipAddress;
            }
            catch (Exception e)
            {

            }
            return string.Empty;
        }

        public async Task<IActionResult> PAPER_MASTER(int Page = 1, string Search = null)
        {
            try
            {
               PAPER_MASTER_AM_DTO_DASH data = new PAPER_MASTER_AM_DTO_DASH();
                data = await UOF.IAdminMaster.GET_PAPER_MASTER_AM("", (int)CurrentUser.UserId, Page, Search);
                ViewBag.Search = Search;
                return View(data);
            }
            catch (Exception e)
            {
                Log.Information("course page!" + e);
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> ManagePaperMaster()
        {
            MANAGE_PAPER_MASTER_AM_DTO data = new MANAGE_PAPER_MASTER_AM_DTO();
            data = await UOF.IAdminMaster.GET_MANAGEPAPERMASTER_AM("", (int)CurrentUser.UserId);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> EditPaperMaster(string pcode="")
        {
            MANAGE_PAPER_MASTER_AM_DTO data = new MANAGE_PAPER_MASTER_AM_DTO();
            data = await UOF.IAdminMaster.GET_EDITPAPERMASTER_AM("", (int)CurrentUser.UserId, pcode);
            data.PAPERcatlist = await UOF.IAdminMaster.GET_PAPERTYPEMASTER_AM("", data.PAPER_MASTER_AM.PAPER_TYPE.ToString());
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> GetPaperTypeCat(PAPER_TYPE_CATEGORY_AM_DTO model)
        {
            try
            {

                List<PAPER_TYPE_CATEGORY_AM_DTO> data = new List<PAPER_TYPE_CATEGORY_AM_DTO>();
                data = await UOF.IAdminMaster.GET_PAPERTYPEMASTER_AM("", model.PAPER_MASTER_TYPE);
              
                return Json(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePaper([FromForm] PAPER_MASTER_AM_DTO model, string ReturnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.PAPER_CODE_PK))
                {

                    FormResponse d = await UOF.IAdminMaster.SavePaper_AM("", model);
                    return Json(d);


                }
                else
                {
                    return Json("Try Again Later!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdatePaper([FromForm] PAPER_MASTER_AM_DTO model, string ReturnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.PAPER_CODE_PK))
                {

                    FormResponse d = await UOF.IAdminMaster.UpdatePaper_AM("", model);
                    
                            return Json(d);
                }
                else
                {
                    return Json("Try Again Later!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        public async Task<IActionResult> ORDINANCE_MASTER(int Page = 1, string Search = null)
        {
            try
            {
                ORDINANCE_MASTER_AM_DTO_DASH data = new ORDINANCE_MASTER_AM_DTO_DASH();
                data = await UOF.IAdminMaster.GET_ORDINANCE_MASTER_AM("", (int)CurrentUser.UserId, Page, Search);
                ViewBag.Search = Search;
                return View(data);
            }
            catch (Exception e)
            {
                Log.Information("ORDINANCE page!" + e);
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> ManageOrdinanceMaster()
        {
            MANAGE_ORDINANCE_MASTER_AM_DTO data = new MANAGE_ORDINANCE_MASTER_AM_DTO();
            data = await UOF.IAdminMaster.GET_MANAGEORDINANCEMASTER_AM("", (int)CurrentUser.UserId);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrdinance([FromForm] ORDINANCE_MASTER_AM_DTO model, string ReturnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.ORDINANCE_NAME))
                {

                    FormResponse d = await UOF.IAdminMaster.SaveOrdinance_AM("", model);
                    return Json(d);


                }
                else
                {
                    return Json("Try Again Later!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        public async Task<IActionResult> ORDINANCE_DETAILS(int Page = 1, string Search = null)
        {
            try
            {
                ORDINANCE_DETAILS_AM_DTO_DASH data = new ORDINANCE_DETAILS_AM_DTO_DASH();
                data = await UOF.IAdminMaster.GET_ORDINANCE_DETAILS_AM("", (int)CurrentUser.UserId, Page, Search);
                ViewBag.Search = Search;
                return View(data);
            }
            catch (Exception e)
            {
                Log.Information("ORDINANCE page!" + e);
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> ManageOrdinanceDetails()
        {
            MANAGE_ORDINANCE_DETAILS_AM_DTO data = new MANAGE_ORDINANCE_DETAILS_AM_DTO();
            data = await UOF.IAdminMaster.GET_MANAGEORDINANCEDETAILS_AM("", (int)CurrentUser.UserId);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrdinanceDetails([FromForm] ORDINANCE_DETAILS_AM_DTO model, string ReturnUrl)
        {
            try
            {
                if (model.ORDINANCE_ID>0)
                {

                    FormResponse d = await UOF.IAdminMaster.SaveOrdinanceDetails_AM("", model);
                    return Json(d);


                }
                else
                {
                    return Json("Try Again Later!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
     
        public async Task<IActionResult> Ordinance_Apply(string IS_RW="",string SESSION_ID="",string EXAM_TYPE_ID="",string CollegeCodeDDL="",int COURSE_ID=0,int RESULT_TYPE_ID=0)
        {
            ORDINANCE_APPLY_AM_DTO_DASH data = new ORDINANCE_APPLY_AM_DTO_DASH();
            data = await UOF.IAdminMaster.GET_ORDINANCEAPPLY_AM("", (int)CurrentUser.UserId);
            ViewBag.IsRWStatus = IS_RW == "on" ? "checked" : "";
            ViewBag.SESSION_ID = SESSION_ID;
            ViewBag.EXAM_TYPE_ID = EXAM_TYPE_ID;
            ViewBag.COURSE_ID = COURSE_ID;
            ViewBag.RESULT_TYPE_ID = RESULT_TYPE_ID;
            if (IS_RW == "on")
            {
                data.COLLEGES_LIST = data.COLLEGES_LIST.Where(x => x.IsRWCollege == 1).Select(x=>x).ToList();
            }
            else {
                data.COLLEGES_LIST = data.COLLEGES_LIST.Where(x => x.IsRWCollege != 1).ToList();
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyOrdinance([FromForm] ORDINANCE_APPLY_AM_DTO model, string ReturnUrl)
        {
            try
            {
               

                    FormResponse d = await UOF.IAdminMaster.ApplyOrdinance_AM("", model);
                    return Json(d);

            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }

        public async Task<IActionResult> ORDINANCE_MAPPING(int Page = 1, string Search = null)
        {
            try
            {
                ORDINANCE_MAPPING_AM_DTO_DASH data = new ORDINANCE_MAPPING_AM_DTO_DASH();
                data = await UOF.IAdminMaster.GET_ORDINANCE_MAPPING_AM("", (int)CurrentUser.UserId, Page, Search);
                ViewBag.Search = Search;
                return View(data);
            }
            catch (Exception e)
            {
                Log.Information("ORDINANCE page!" + e);
            }
            return View();

        }
    
        public async Task<IActionResult> ManageOrdinanceMapping(int RESULT_TYPE_ID=0)
        {
            MANAGE_ORDINANCE_MAPPING_AM_DTO data = new MANAGE_ORDINANCE_MAPPING_AM_DTO();
            data = await UOF.IAdminMaster.GET_MANAGEORDINANCEMAPPING_AM("", (int)CurrentUser.UserId, RESULT_TYPE_ID);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrdinanceMapping([FromForm] ORDINANCE_MAPPING_AM_DTO model, string ReturnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.PROC_NAME))
                {

                    FormResponse d = await UOF.IAdminMaster.SaveOrdinanceMapping_AM("", model);
                    return Json(d);


                }
                else
                {
                    return Json("Try Again Later!");
                }
            }
            catch (Exception ex)
            {
                return Json("Internal error!");
            }

        }
        public async Task<IActionResult> Ordinance_Student_Apply(string Search = null,int COURSE_ID=0,string SESSIONNAME="")
        {
            try
            {
                MANAGE_ORDINANCE_STUDENT_MAPPING_AM_DTO data = new MANAGE_ORDINANCE_STUDENT_MAPPING_AM_DTO();
                data = await UOF.IAdminMaster.Ordinance_Student_Apply("", (int)CurrentUser.UserId,  Search, COURSE_ID, SESSIONNAME);
                ViewBag.Search = Search;
                ViewBag.COURSE_ID = COURSE_ID;
                ViewBag.SESSIONNAME = SESSIONNAME;
                return View(data);
            }
            catch (Exception e)
            {
                Log.Information("ORDINANCE page!" + e);
            }
            return View();

        }
        public async Task<IActionResult> SearchOrdinanceStudent([FromForm]  ORDINANCE_STUDENT_DTO model)
        {
            try
            {
              FormResponse d = await UOF.IAdminMaster.SearchOrdinanceStudent_AM("", model);
                    return Json(d);
            }
            catch (Exception e)
            {
                Log.Information("ORDINANCE page!" + e);
            }
            return View();

        }
        public async Task<IActionResult> SaveOrdinanceStudentMapping([FromForm] ORDINANCE_STUDENT_DTO model)
        {
            try
            {
                FormResponse d = await UOF.IAdminMaster.SaveOrdinanceStudentMapping_AM("", model);
                return Json(d);
            }
            catch (Exception e)
            {
                Log.Information("ORDINANCE page!" + e);
            }
            return View();

        }
        
        //-----------------------------------------course------------------------
        [HttpGet]
        public async Task<IActionResult> CourseMasterRule()
        {
            try
            {

                var s = await UOF.IAdminMaster.GetSessionList("SessionList");
                if (s.Count() == 0) {
                    
                    SessionDTO ses = new SessionDTO { SessionName = "2024-25" };
                    s.Add(ses);
                }
                ViewBag.SessionList = s;
                List<CourseMasterDTO_AM> adminData = await UOF.IAdminMaster.CourseMasterAdmin(false, s.First().SessionName);
                return View(adminData);
            }
            catch (Exception)
            {

            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CourseMasterRule(bool WithCountH = false, string SessionName = "")
        {
            try
            {
                List<CourseMasterDTO_AM> adminData = await UOF.IAdminMaster.CourseMasterAdmin(WithCountH, SessionName);
                ViewBag.SessionList = await UOF.IAdminMaster.GetSessionList("SessionList");
                ViewBag.IsCount = WithCountH;
                ViewBag.SessionName = SessionName;
                return View(adminData);
            }
            catch (Exception)
            {

            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromForm] CourseMasterDTO_AM Model)
        {
            try
            {

                FormResponse data = new FormResponse();

                data = await UOF.IAdminMaster.AddCourse_AM(Model);


                if (data.ResponseCode == 1 && data.ResponseMessage == "Yes")
                {
                    return Json(new { res = "ok" });
                }
            }
            catch (Exception e)
            {

            }
            return Json("0");
        }
        public async Task<IActionResult> EditCourse(int EntryID = 0)
        {

            if (EntryID > 0)
            {

                CourseMasterDTO_AM adminData = await UOF.IAdminMaster.EditCourse("", EntryID);

                if (adminData.EntryID > 0)
                {
                    var ctlist = await UOF.IAdminMaster.GetCourseTypeList("Type");
                    List<SelectListItem> Select_Listct = new List<SelectListItem>();
                    foreach (var r in ctlist)
                    {
                        SelectListItem obj = new SelectListItem()
                        {
                            Value = r.CourseType.ToString(),
                            Text = r.CourseType,
                            Selected = adminData.CtypeMapList.Where(me => me.CTYPEID.ToString() == r.CtypeID).Count() > 0 ? true : false
                        };

                        Select_Listct.Add(obj);
                    }
                    ViewBag.CtMaster = Select_Listct;
                    // return View("~/areas/admin/views/components/_pageadmin.cshtml", adminData);

                    var TemplateList = await UOF.IAdminMaster.GetTemplateList("ALL");
                    List<SelectListItem> TemplateListselect = new List<SelectListItem>();
                    foreach (var r in TemplateList)
                    {
                        SelectListItem obj = new SelectListItem()
                        {
                            Value = r.TName.ToString(),
                            Text = r.TName,
                            Selected = adminData.TemplateName == r.TName ? true : false
                        };

                        TemplateListselect.Add(obj);
                    }
                    ViewBag.TemplateList = TemplateListselect;

                    return PartialView("_EditCourse", adminData);
                }
            }
            return PartialView("_EditCourse");
        }
        [HttpPost]
        public async Task<IActionResult> EditCourseUpdate([FromForm] CourseMasterDTO_AM Model)
        {
            try
            {

                FormResponse data = new FormResponse();

                data = await UOF.IAdminMaster.UpdateCourse_AM(Model);


                if (data.ResponseCode == 1 && data.ResponseMessage == "Yes")
                {
                    return Json(new { res = "ok" });
                }
            }
            catch (Exception e)
            {

            }
            return Json("0");
        }

        //[HttpGet]
        //public async Task<IActionResult> StudentList()
        //{
        //    try
        //    {
        //        bool IsAdmin = false;
        //        if (CurrentUser.Roles.Contains("Admin"))
        //        {
        //            IsAdmin = true;
        //        }
        //        StudentList_Admin data = new StudentList_Admin();
        //        data = await UOF.IAdminMaster.StudentManageData("", "", false, "", 0);
        //        data.CourseList = new List<CourseMasterDTO_AM>();
        //         data.StudentList = new List<StudentMasterDTO>();
        //         data.PagingList = new PagingDTO();
        //        data.ExamTypeList = new List<EXAM_TYPE_MASTER_DTO>();
        //        return View(data);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return View();
        //}
        //[HttpPost]
        public async Task<IActionResult> StudentList(string SessionName = "", string CourseType = "", bool IsPG = false, int CourseId = 0, string ExamTypeName = "", string CommandNameSrh = null, string RollN = "", int IsLiveStatus = 2, int Page = 1)
        {
            try
            {
                bool IsAdmin = false;
                if (CurrentUser.Roles.Contains("Admin"))
                {
                    IsAdmin = true;
                }
                StudentList_Admin data = new StudentList_Admin();
                data = await UOF.IAdminMaster.StudentManageData("", CourseType, IsPG, SessionName, CourseId);

                // data.ExamTypeList = await UOF.IAdminMaster.GetExamTypeList_ADMIN("ExamTypeList", CourseId);
                data.StudentList = new List<StudentMasterDTO>();
                data.PagingList = new PagingDTO();
                if (CommandNameSrh == "Search")
                {
                    StudentList_Admin data1 = new StudentList_Admin();
                    data1= await UOF.IAdminMaster.StudentList_AM("", SessionName, CourseType, IsPG, CourseId, ExamTypeName, RollN, IsLiveStatus, IsAdmin, Page);
                    data.StudentList = data1.StudentList;
                    data.PagingList = data1.PagingList;
                }
                data.CourseType = CourseType;
                data.CourseID = CourseId;
                data.SessionName = SessionName;
                data.ExamTypeName = ExamTypeName;
                data.RollN = RollN;
                data.IsPG = IsPG;
                data.IsLiveStatus = IsLiveStatus;
                return View(data);
            }
            catch (Exception e)
            {

            }
            return View();
        }
    }
}
