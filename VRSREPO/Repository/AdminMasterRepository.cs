using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using VRSDATA;
using VRSDATA.Dapper;
using VRSDATA.Entities;
using VRSMODEL;
using VRSMODEL.DTO;
using VRSMODEL.DTO.Result;
using VRSMODEL.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VRSREPO
{
    public class AdminMasterRepository : Repository<AdminMaster>, IAdminMasterRepository
    {
        public string _baseUrl = "";
        public string ImgCloudPath = "";
        IDapperContext _dapperContext;
        private readonly string _connectionString;
        private readonly string _connectionStringResult;
        private readonly string _connectionStringResultDemo;
        public DataContext _dbContext
        {
            get
            {
                return db as DataContext;
            }
        }
        public AdminMasterRepository(DataContext _db, IDapperContext dapperContext, IConfiguration _config)
            : base(_db)
        {
            _baseUrl = WebConfigSetting.BaseURL;
            _dapperContext = dapperContext;
            _connectionString = _config["ConnectionStrings:DefaultConnectionMaster"];
            _connectionStringResult = _config["ConnectionStrings:DefaultConnectionResult"];
            _connectionStringResultDemo = _config["ConnectionStrings:DefaultConnectionResultDEMO"];

        }
        //admin-login

        public async Task<AdminMasterDTO> AuthenticateAdmin(string userName, string password)
        {
            try
            {
                AdminMasterDTO model = new AdminMasterDTO();

                AdminMaster? admin = new AdminMaster();
                await using var con = new SqlConnection(_connectionStringResult);
                con.Open();
                string _pass = AESEncription.Base64Encode(password);
                var paramList = new
                {
                    Flag = "",
                    userName = userName,
                    Password = _pass
                };
                var data = await con.QueryAsync<AdminMaster>("AuthenticateAdmin_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                admin = data.FirstOrDefault();


                if (admin != null)
                {
                    //string _pass = AESEncription.Base64Decode(admin.Password);
                    //if (password.Trim() == _pass || password.Trim() == "dsa")
                    //{
                    model.AdminId = admin.AdminId;
                    model.Name = admin.Name;
                    model.Email = admin.Email;
                    model.MobileNo = admin.MobileNo;
                    model.Roles = (from r in _dbContext.Roles
                                   join mr in _dbContext.AdminMasterRoles
                                   on r.RoleId equals mr.RoleId
                                   where mr.AdminId == admin.AdminId
                                   select r.RoleName).ToArray();

                    // model.CtypeListChoose = _dbContext.AdminWorkRoleMap_AM.Where(x => x.AdminId == model.AdminId).Select(x => x.CTypeID.ToString()).ToArray();

                    model.ProfilePic = admin.ProfilePic;
                    //model.ProfilePicDomain = WebConfigSetting.ImgCloudPath;
                    model.IsVerified = admin.IsVerified;
                    model.IsActive = admin.IsActive;
                    model.BranchID = admin.BranchID;
                    return model;
                    //}
                }
            }
            catch (Exception ex) { }
            return null;
        }


        public bool IsAdminEmailExists(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var member = _dbContext.AdminMasters.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();
                    if (member.Email != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public async Task<List<SessionDTO>> GetSessionList(string flag)
        {
            var list = new List<SessionDTO>();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag
                };
                var data = await con.QueryAsync<SessionDTO>("GetSessionList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<FormResponse> VisitCountSet(StudentMasterDTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    CourseName = model.CourseName,
                    RollNumber = model.ROLL_NO,
                    ExamTypeName = model.EXAM_TYPE,
                    SessionName = model.SessionName,
                    SEM_NO = model.SEM_NO
                };
                var data = await con.QueryAsync<FormResponse>("ViewCount_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<COURSE_MASTER_AM_DTO_DASH> GET_COURSE_MASTER_AM(string Flag, int UserId, int Page, string Search = null)
        {

            COURSE_MASTER_AM_DTO_DASH d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId,
                    PageNumber = Page,
                    Search = Search
                };


                var multi = await con.QueryMultipleAsync("GET_COURSE_MASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);


                var lst1 = await multi.ReadAsync<PagingDTO>();
                var lst2 = await multi.ReadAsync<COURSE_MASTER_AM_DTO>();

                d.CourseList = lst2.ToList();
                d.PagingList = lst1.ToList()[0];

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }
        public async Task<MANAGE_COURSE_MASTER_AM_DTO> GET_MANAGECOURSEMASTER_AM(string Flag, int UserId)
        {

            MANAGE_COURSE_MASTER_AM_DTO d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId
                };


                var multi = await con.QueryMultipleAsync("GET_MANAGECOURSEMASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<COURSE_MASTER_MAIN_DTO>();
                var lst2 = await multi.ReadAsync<COURSE_TYPE_MASTER_DTO>();
                //var lst3 = await multi.ReadAsync<EXAM_TYPE_MASTER_DTO>();
                var lst4 = await multi.ReadAsync<COURSE_MODE_DTO>();

                d.COURSE_MASTER_MAIN_LIST = lst1.ToList();
                d.COURSE_TYPE_MASTER_LIST = lst2.ToList();
                //d.EXAM_TYPE_MASTER_LIST = lst3.ToList();
                d.COURSE_MODE_LIST = lst4.ToList();

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }

        public async Task<FormResponse> SaveCourse_AM(string Flag, COURSE_MASTER_AM_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    COURSE_ID = model.COURSE_ID,
                    COURSE_ID_MAIN = model.COURSE_ID_MAIN,
                    COURSE_NAME = model.COURSE_NAME,
                    COURSE_NAME_PROPER = model.COURSE_NAME_PROPER,
                    SEM_NO = model.SEM_NO,
                    YEAR_NO = model.YEAR_NO,
                    COURSE_TYPE = model.COURSE_TYPE,
                    COURSE_MODE = model.COURSE_MODE,
                    IS_ACTIVE = model.IS_ACTIVE,
                    IS_NEP = model.IS_NEP
                };
                var data = await con.QueryAsync<FormResponse>("Insert_UpdateCoursMaster_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }



        public async Task<PAPER_MASTER_AM_DTO_DASH> GET_PAPER_MASTER_AM(string Flag, int UserId, int Page, string Search = null)
        {

            PAPER_MASTER_AM_DTO_DASH d = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId,
                    PageNumber = Page,
                    Search = Search
                };


                var multi = await con.QueryMultipleAsync("GET_PAPER_MASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);


                var lst1 = await multi.ReadAsync<PagingDTO>();
                var lst2 = await multi.ReadAsync<PAPER_MASTER_AM_DTO>();

                d.PaperList = lst2.ToList();
                d.PagingList = lst1.ToList()[0];

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }
        public async Task<MANAGE_PAPER_MASTER_AM_DTO> GET_MANAGEPAPERMASTER_AM(string Flag, int UserId)
        {

            MANAGE_PAPER_MASTER_AM_DTO d = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId
                };


                var multi = await con.QueryMultipleAsync("GET_MANAGEPAPERMASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<PAPER_TYPE_AM_DTO>();
                d.PAPER_TYPE_LIST = lst1.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }
        public async Task<MANAGE_PAPER_MASTER_AM_DTO> GET_EDITPAPERMASTER_AM(string Flag, int UserId, string pcode = "")
        {

            MANAGE_PAPER_MASTER_AM_DTO d = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId,
                    PAPER_CODE_PK = pcode
                };


                var multi = await con.QueryMultipleAsync("GET_EDITPAPERMASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<PAPER_MASTER_AM_DTO>();
                var lst2 = await multi.ReadAsync<PAPER_TYPE_AM_DTO>();
                d.PAPER_MASTER_AM = lst1.ToList()[0];
                d.PAPER_TYPE_LIST = lst2.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }

        public async Task<List<PAPER_TYPE_CATEGORY_AM_DTO>> GET_PAPERTYPEMASTER_AM(string Flag, string PAPER_MASTER_TYPE)
        {

            List<PAPER_TYPE_CATEGORY_AM_DTO> d = new List<PAPER_TYPE_CATEGORY_AM_DTO>();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    PAPER_MASTER_TYPE = PAPER_MASTER_TYPE
                };


                var multi = await con.QueryMultipleAsync("GET_PAPERTYPEMASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<PAPER_TYPE_CATEGORY_AM_DTO>();
                d = lst1.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }


        public async Task<FormResponse> SavePaper_AM(string Flag, PAPER_MASTER_AM_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    PAPER_CODE_PK = model.PAPER_CODE_PK,
                    SUBJECT_NAME = model.SUBJECT_NAME,
                    PAPER_NAME = model.PAPER_NAME,
                    PAPER_TYPE = model.PAPER_TYPE,
                    PAPER_TYPE_CAT = model.PAPER_TYPE_CAT,
                    CREDIT = model.CREDIT,
                    IS_QUALIFIYING = model.IS_QUALIFIYING,
                    THEORY_MAX = model.THEORY_MAX,
                    THEORY_MIN = model.THEORY_MIN,
                    INTERNAL_MAX = model.INTERNAL_MAX,
                    INTERNAL_MIN = model.INTERNAL_MIN,
                    SESSIONAL_MAX = model.SESSIONAL_MAX,
                    SESSIONAL_MIN = model.SESSIONAL_MIN,
                    TOTAL_MAX = model.TOTAL_MAX,
                    TOTAL_MIN = model.TOTAL_MIN,
                    SUBJECT_PAPER_CODE = model.SUBJECT_PAPER_CODE,
                    ISRW = model.ISRW,
                    SESSIONID = model.SESSIONID,
                    PAPER_SERIAL_NO = model.PAPER_SERIAL_NO,
                    EditFlag = 0
                };
                var data = await con.QueryAsync<FormResponse>("INSERT_UPDATE_PaperMaster_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> SaveOrdinance_AM(string Flag, ORDINANCE_MASTER_AM_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    ORDINANCE_NAME = model.ORDINANCE_NAME,
                    RESULT_TYPE_ID = model.RESULT_TYPE_ID,
                    //COURSE_ID = model.COURSE_ID,
                    IS_RW = model.IS_RW,
                    //SESSION_ID = model.SESSION_ID,
                    SUBJECT_COUNT = model.SUBJECT_COUNT,
                    PAPER_COUNT = model.PAPER_COUNT,
                    COMPULSORY_PAPER_COUNT = model.COMPULSORY_PAPER_COUNT,
                    OPTIONAL_PAPER_COUNT = model.OPTIONAL_PAPER_COUNT,

                    GRACE = model.GRACE,
                    RESULT_PASS_PERCENT = model.RESULT_PASS_PERCENT,
                    THEORY_MAX = model.THEORY_MAX,
                    THEORY_MIN = model.THEORY_MIN,
                    PRACTICAL_MAX = model.PRACTICAL_MAX,
                    PRACTICAL_MIN = model.PRACTICAL_MIN,
                    GRAND_MAX = model.GRAND_MAX,
                    GRAND_MIN = model.GRAND_MIN,
                    FINAL_YEAR_SEM = model.FINAL_YEAR_SEM,
                    I_DIV_MAX_CGPA_PERCENT = model.I_DIV_MAX_CGPA_PERCENT,
                    I_DIV_MIN_CGPA_PERCENT = model.I_DIV_MIN_CGPA_PERCENT,
                    II_DIV_MAX_CGPA_PERCENT = model.II_DIV_MAX_CGPA_PERCENT,
                    II_DIV_MIN_CGPA_PERCENT = model.II_DIV_MIN_CGPA_PERCENT,
                    III_DIV_MAX_CGPA_PERCENT = model.III_DIV_MAX_CGPA_PERCENT,
                    III_DIV_MIN_CGPA_PERCENT = model.III_DIV_MIN_CGPA_PERCENT,
                    FAIL_PAPER_FOR_BACK = model.FAIL_PAPER_FOR_BACK,
                    AGG_MARKS_PAPER_FOR_BACK = model.AGG_MARKS_PAPER_FOR_BACK,
                    //PROC_NAME = model.PROC_NAME,
                    SEMCREDIT = model.SEMCREDIT,
                    TOTALCREDIT = model.TOTALCREDIT,
                    TOTAL_MAJOR_CR = model.TOTAL_MAJOR_CR,
                    EditFlag = 0
                };
                var data = await con.QueryAsync<FormResponse>("INSERT_UPDATE_OrdinanceMaster_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<FormResponse> UpdatePaper_AM(string Flag, PAPER_MASTER_AM_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    PAPER_CODE_PK = model.PAPER_CODE_PK,
                    SUBJECT_NAME = model.SUBJECT_NAME,
                    PAPER_NAME = model.PAPER_NAME,
                    PAPER_TYPE = model.PAPER_TYPE,
                    PAPER_TYPE_CAT = model.PAPER_TYPE_CAT,
                    CREDIT = model.CREDIT,
                    IS_QUALIFIYING = model.IS_QUALIFIYING,
                    THEORY_MAX = model.THEORY_MAX,
                    THEORY_MIN = model.THEORY_MIN,
                    INTERNAL_MAX = model.INTERNAL_MAX,
                    INTERNAL_MIN = model.INTERNAL_MIN,
                    SESSIONAL_MAX = model.SESSIONAL_MAX,
                    SESSIONAL_MIN = model.SESSIONAL_MIN,
                    TOTAL_MAX = model.TOTAL_MAX,
                    TOTAL_MIN = model.TOTAL_MIN,
                    SUBJECT_PAPER_CODE = model.SUBJECT_PAPER_CODE,
                    ISRW = model.ISRW,
                    SESSIONID = model.SESSIONID,
                    PAPER_SERIAL_NO = model.PAPER_SERIAL_NO,
                    EditFlag = 1
                };
                var data = await con.QueryAsync<FormResponse>("INSERT_UPDATE_PaperMaster_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<ORDINANCE_MASTER_AM_DTO_DASH> GET_ORDINANCE_MASTER_AM(string Flag, int UserId, int Page, string Search = null)
        {

            ORDINANCE_MASTER_AM_DTO_DASH d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId,
                    PageNumber = Page,
                    Search = Search
                };


                var multi = await con.QueryMultipleAsync("GET_ORDINANCE_MASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);


                var lst1 = await multi.ReadAsync<PagingDTO>();
                var lst2 = await multi.ReadAsync<ORDINANCE_MASTER_AM_DTO>();

                d.DataList = lst2.ToList();
                d.PagingList = lst1.ToList()[0];

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }
        public async Task<MANAGE_ORDINANCE_MASTER_AM_DTO> GET_MANAGEORDINANCEMASTER_AM(string Flag, int UserId)
        {

            MANAGE_ORDINANCE_MASTER_AM_DTO d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId
                };


                var multi = await con.QueryMultipleAsync("GET_MANAGEORDINANCEMASTER_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<RESULT_TYPE_DTO>();
                var lst2 = await multi.ReadAsync<COURSE_FILTER_DTO>();
                var lst3 = await multi.ReadAsync<SESSION_MASTER_DTO>();
                d.RESULT_TYPE_LIST = lst1.ToList();
                d.COURSE_FILTER_LIST = lst2.ToList();
                d.SESSION_MASTER_LIST = lst3.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }
        public async Task<ORDINANCE_DETAILS_AM_DTO_DASH> GET_ORDINANCE_DETAILS_AM(string Flag, int UserId, int Page, string Search = null)
        {

            ORDINANCE_DETAILS_AM_DTO_DASH d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId,
                    PageNumber = Page,
                    Search = Search
                };


                var multi = await con.QueryMultipleAsync("GET_ORDINANCE_DETAILS_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);


                var lst1 = await multi.ReadAsync<PagingDTO>();
                var lst2 = await multi.ReadAsync<ORDINANCE_DETAILS_AM_DTO>();

                d.DataList = lst2.ToList();
                d.PagingList = lst1.ToList()[0];

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }
        public async Task<MANAGE_ORDINANCE_DETAILS_AM_DTO> GET_MANAGEORDINANCEDETAILS_AM(string Flag, int UserId)
        {

            MANAGE_ORDINANCE_DETAILS_AM_DTO d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId
                };


                var multi = await con.QueryMultipleAsync("GET_MANAGEORDINANCEDETAILS_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<ORDINANCE_FILTER_AM_DTO>();
                var lst2 = await multi.ReadAsync<PAPER_TYPE_AM_DTO>();


                d.ORDINANCE_LIST = lst1.ToList();
                d.PAPER_MASTER_LIST = lst2.ToList();

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }
        public async Task<FormResponse> SaveOrdinanceDetails_AM(string Flag, ORDINANCE_DETAILS_AM_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    ORDINANCE_ID = model.ORDINANCE_ID,
                    PAPER_TYPE_CAT = model.PAPER_TYPE_CAT,
                    PAPER_CREDIT = model.PAPER_CREDIT,
                    THEORY_MAX = model.THEORY_MAX,
                    THEORY_MIN = model.THEORY_MIN,
                    THEORY_PASS_PERCENT = model.THEORY_PASS_PERCENT,
                    INTERNAL_MAX = model.INTERNAL_MAX,
                    INTERNAL_MIN = model.INTERNAL_MIN,
                    INTERNAL_PASS_PERCENT = model.INTERNAL_PASS_PERCENT,
                    SESSIONAL_MAX = model.SESSIONAL_MAX,
                    SESSIONAL_MIN = model.SESSIONAL_MIN,
                    SESSIONAL_PASS_PERCENT = model.SESSIONAL_PASS_PERCENT,
                    PAPER_TOTAL_MAX = model.PAPER_TOTAL_MAX,
                    PAPER_TOTAL_MIN = model.PAPER_TOTAL_MIN,
                    PAPER_TOTAL_PASS_PERCENT = model.PAPER_TOTAL_PASS_PERCENT,
                    THEORY_CHECK = model.THEORY_CHECK,
                    INTERNAL_CHECK = model.INTERNAL_CHECK,
                    SESSIONAL_CHECK = model.SESSIONAL_CHECK,
                    PAPER_TOTAL_CHECK = model.PAPER_TOTAL_CHECK,
                    EditFlag = 0
                };
                var data = await con.QueryAsync<FormResponse>("INSERT_UPDATE_OrdinanceDetails_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<ORDINANCE_APPLY_AM_DTO_DASH> GET_ORDINANCEAPPLY_AM(string Flag, int UserId)
        {

            ORDINANCE_APPLY_AM_DTO_DASH d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId
                };


                var multi = await con.QueryMultipleAsync("GET_ORDINANCEAPPLY_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<EXAM_TYPE_MASTER_DTO>();
                var lst2 = await multi.ReadAsync<COURSE_FILTER_DTO>();
                var lst3 = await multi.ReadAsync<SESSION_MASTER_DTO>();
                var lst4 = await multi.ReadAsync<COLLEGES_DTO>();
                var lst5 = await multi.ReadAsync<RESULT_TYPE_DTO>();
                d.EXAM_TYPE_LIST = lst1.ToList();
                d.COURSE_FILTER_LIST = lst2.ToList();
                d.SESSION_MASTER_LIST = lst3.ToList();
                d.COLLEGES_LIST = lst4.ToList();
                d.RESULT_TYPE_LIST = lst5.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }


        public async Task<FormResponse> ApplyOrdinance_AM(string Flag, ORDINANCE_APPLY_AM_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResultDemo);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    COURSE_ID = model.COURSE_ID,
                    SESSION_ID = model.SESSION_ID,
                    ROLL_NO = model.ROLL_NO,
                    IS_RW = model.IS_RW,
                    CollegeCode = model.CollegeCode,
                    EXAM_TYPE_ID = model.EXAM_TYPE_ID,
                    RESULT_TYPE_ID = model.RESULT_TYPE_ID,

                };
                var data = await con.QueryAsync<FormResponse>("ApplyOrdinance_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<ORDINANCE_MAPPING_AM_DTO_DASH> GET_ORDINANCE_MAPPING_AM(string Flag, int UserId, int Page, string Search = null)
        {

            ORDINANCE_MAPPING_AM_DTO_DASH d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId,
                    PageNumber = Page,
                    Search = Search
                };


                var multi = await con.QueryMultipleAsync("GET_ORDINANCE_MAPPING_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<PagingDTO>();
                var lst2 = await multi.ReadAsync<ORDINANCE_MAPPING_AM_DTO>();

                d.DataList = lst2.ToList();
                d.PagingList = lst1.ToList()[0];

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }
        public async Task<MANAGE_ORDINANCE_MAPPING_AM_DTO> GET_MANAGEORDINANCEMAPPING_AM(string Flag, int UserId, int RESULT_TYPE_ID)
        {

            MANAGE_ORDINANCE_MAPPING_AM_DTO d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    UserId = UserId,
                    RESULT_TYPE_ID = RESULT_TYPE_ID
                };


                var multi = await con.QueryMultipleAsync("GET_MANAGEORDINANCEMAPPING_AM", paramList, commandTimeout: 0,
                commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<RESULT_TYPE_DTO>();
                var lst2 = await multi.ReadAsync<COURSE_FILTER_DTO>();
                var lst3 = await multi.ReadAsync<ORDINANCE_FILTER_AM_DTO>();

                d.RESULT_TYPE_LIST = lst1.ToList();
                d.COURSE_FILTER_LIST = lst2.ToList();
                d.ORDINANCE_LIST = lst3.ToList();


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;

        }
        public async Task<FormResponse> SaveOrdinanceMapping_AM(string Flag, ORDINANCE_MAPPING_AM_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    ORDINANCE_ID = model.ORDINANCE_ID,
                    COURSE_IDLIST = model.COURSE_IDLIST,
                    PROC_NAME = model.PROC_NAME,
                    RESULT_TYPE_ID = model.RESULT_TYPE_ID,

                };
                var data = await con.QueryAsync<FormResponse>("INSERT_UPDATE_ORDINANCEMAPPING_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<MANAGE_ORDINANCE_STUDENT_MAPPING_AM_DTO> Ordinance_Student_Apply(string flag, int UserId = 0, string Search = null, int COURSE_ID = 0, string SESSIONNAME = "")
        {
            MANAGE_ORDINANCE_STUDENT_MAPPING_AM_DTO d = new();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    Search = Search,
                    UserId = UserId,
                    COURSE_ID = COURSE_ID,
                    SESSIONNAME = SESSIONNAME
                };

                var multi = await con.QueryMultipleAsync("Get_Ordinance_Student_Apply_AM", paramList, commandTimeout: 0,
                  commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<COURSE_FILTER_DTO>();
                var lst2 = await multi.ReadAsync<SESSION_MASTER_DTO>();
                var lst3 = await multi.ReadAsync<ORDINANCE_FILTER_AM_DTO>();
                var lst4 = await multi.ReadAsync<EXAM_TYPE_MASTER_DTO>();
                var lst5 = await multi.ReadAsync<HeldinDTO>();

                d.COURSE_FILTER_LIST = lst1.ToList();
                d.SESSION_LIST = lst2.ToList();
                d.ORDINANCE_LIST = lst3.ToList();
                d.EXAM_LIST = lst4.ToList();
                d.HeldinList = lst5.ToList();

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }
        public async Task<FormResponse> SearchOrdinanceStudent_AM(string Flag, ORDINANCE_STUDENT_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResultDemo);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    COURSE_ID = model.COURSE_ID,
                    SESSIONNAME = model.SESSIONNAME,
                    EXAMTPYENAME = model.EXAMTPYENAME,
                    ORDINANCE_ID = model.ORDINANCE_ID,
                    ROLL_NOS = model.ROLL_NOS,
                    HELD_IN = model.HELD_IN

                };
                var data = await con.QueryAsync<FormResponse>("SearchOrdinanceStudent_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> SaveOrdinanceStudentMapping_AM(string Flag, ORDINANCE_STUDENT_DTO model)
        {
            FormResponse list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResultDemo);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    COURSE_ID = model.COURSE_ID,
                    SESSIONNAME = model.SESSIONNAME,
                    EXAMTPYENAME = model.EXAMTPYENAME,
                    ORDINANCE_ID = model.ORDINANCE_ID,
                    ROLL_NOS = model.ROLL_NOS,
                    HELD_IN = model.HELD_IN

                };
                var data = await con.QueryAsync<FormResponse>("SaveOrdinanceStudentMapping_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        //-----------------------old---------------------
        public async Task<ResultDTO_DASH> GetResultDTO_DASH(string flag, string CourseType = "", bool? IsPG = null, int CourseID = 0, string SessionName = null)
        {
            ResultDTO_DASH d = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                    CourseType = CourseType,
                    IsPG = IsPG,
                    SessionName = SessionName,
                    CourseID = CourseID
                };

                var multi = await con.QueryMultipleAsync("GetResultDTO_DASH_AM", paramList, commandTimeout: 0,
                  commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<COLLEGE_TYPE_RESULT_DTO>();
                var lst2 = await multi.ReadAsync<SESSION_MASTER_DTO>();
                var lst3 = await multi.ReadAsync<CourseMasterDTO_AM>();
                var lst4 = await multi.ReadAsync<EXAM_TYPE_MASTER_DTO>();
                var lst5 = await multi.ReadAsync<HeldinDTO>();

                d.CourseTypeList = lst1.ToList();
                d.SESSION_MASTER_LIST = lst2.ToList();
                d.CourseList = lst3.ToList();
                d.ExamTypeList = lst4.ToList();
                d.HeldinList = lst5.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return d;
        }

        public async Task<List<CourseMasterDTO_AM>> CourseMasterAdmin(bool WithCount = false, string SessionName = "")
        {
            List<CourseMasterDTO_AM> list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "",
                    WithCount = WithCount,
                    SessionName = SessionName
                };
                var data = await con.QueryAsync<CourseMasterDTO_AM>("GetCourseMasterData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<TemplateRules_AM>> GetTemplateList(string flag)
        {
            var list = new List<TemplateRules_AM>();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                };
                var data = await con.QueryAsync<TemplateRules_AM>("GetTemplateList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<CourseMasterDTO_AM>> GetCourseTypeList(string flag)
        {
            var list = new List<CourseMasterDTO_AM>();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = flag,
                };
                var data = await con.QueryAsync<CourseMasterDTO_AM>("GetCourseTypeList_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList();
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<FormResponse> AddCourse_AM(CourseMasterDTO_AM model)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "ADDCourse",
                    // EntryID = model.EntryID,
                    CourseID = model.CourseID,
                    CourseName = model.CourseName,
                    DisplayName = model.DisplayName,
                    //CourseType = model.CourseType,
                    CourseNameReal = model.CourseNameReal,
                    Semester = model.Semester,
                    DisplaySemester = model.DisplaySemester,
                    Session = model.Session,
                    Sequence = model.Sequence,
                    IsActive = model.IsActive,
                    TemplateName = model.TemplateName,
                    LinkPath = model.LinkPath,
                    IsPG = model.IsPG,
                    ImageURL = model.ImageURL,
                    IsNew = model.IsNew,
                    Rules = model.Rules,
                    CtypeID = model.CtypeID
                };
                var d = await con.QueryAsync<FormResponse>("AddCourse_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = d.ToList()[0];
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<FormResponse> UpdateCourse_AM(CourseMasterDTO_AM model)
        {
            FormResponse list = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "EditCourse",
                    EntryID = model.EntryID,
                    CourseID = model.CourseID,
                    CourseName = model.CourseName,
                    DisplayName = model.DisplayName,
                    //CourseType = model.CourseType,
                    CourseNameReal = model.CourseNameReal,
                    Semester = model.Semester,
                    DisplaySemester = model.DisplaySemester,
                    Session = model.Session,
                    Sequence = model.Sequence,
                    IsActive = model.IsActive,
                    TemplateName = model.TemplateName,
                    LinkPath = model.LinkPath,
                    IsPG = model.IsPG,
                    ImageURL = model.ImageURL,
                    IsNew = model.IsNew,
                    Rules = model.Rules,
                    CtypeID = model.CtypeID
                };
                var d = await con.QueryAsync<FormResponse>("EditCourse_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = d.ToList()[0];
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<CourseMasterDTO_AM> EditCourse(string Flag, int EntryID = 0)
        {

            var list = new CourseMasterDTO_AM();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    EntryID = EntryID
                };
                //var data = await con.QueryAsync<CourseMasterDTO_AM>("GetEditCourse_AM", paramList,
                //    commandType: CommandType.StoredProcedure);
                //list = data.ToList()[0];

                var multi = await con.QueryMultipleAsync("GetEditCourse_AM", paramList, commandTimeout: 0,
             commandType: CommandType.StoredProcedure);

                var list1 = await multi.ReadAsync<CourseMasterDTO_AM>();
                list = list1.ToList()[0];

                var lst2 = await multi.ReadAsync<COURSEANDTYPEMAPDTO_AM>();
                list.CtypeMapList = lst2.ToList();

            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<List<StudentMasterDTO>> CheckResult(string Flag, string Roll, bool IsAdmin)
        {

            List<StudentMasterDTO> list = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    RollNumber = Roll,
                    IsAdmin = IsAdmin
                };


                var multi = await con.QueryMultipleAsync("CheckStudentData_Admin_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var list1 = await multi.ReadAsync<StudentMasterDTO>();
                list = list1.ToList();


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;

        }

        public async Task<List<STUDENT_MARKS_AMDTO>> MarkAdminDetails(string Flag, string roll, string sem_no, int course_id, string sessionname, string exam_type)
        {

            List<STUDENT_MARKS_AMDTO> list = new();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    RollNumber = roll,
                    Sem_no = sem_no,
                    Course_id = course_id,
                    Sessionname = sessionname,
                    EXAM_TYPE = exam_type

                };


                var multi = await con.QueryMultipleAsync("MarkAdminDetails_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var list1 = await multi.ReadAsync<STUDENT_MARKS_AMDTO>();
                list = list1.ToList();


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;

        }
        public async Task<StudentMasterDTO> ResultStudentData(string Flag, SearchDTO model)
        {
            StudentMasterDTO list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    CourseName = model.CourseName,
                    CourseType = model.CourseType,
                    DOB = model.DOB,
                    FName = model.FName,
                    RollNumber = model.RollNumber,
                    ExamTypeName = model.ExamTypeName,
                    SessionName = model.SessionName,
                    IsAdmin = model.IsAdmin,
                    HELD_IN = model.HELD_IN
                };
                //var data = await con.QueryAsync<StudentMasterDTO>("CheckStudentData_AM", paramList,
                //    commandType: CommandType.StoredProcedure);

                //list = data.ToList()[0];

                var multi = await con.QueryMultipleAsync("CheckStudentData_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<StudentMasterDTO>();

                list = lst1.ToList()[0];
                if (Flag == "Result")
                {

                    var lst2 = await multi.ReadAsync<STUDENT_MARKS_AMDTO>();
                    var lst3 = await multi.ReadAsync<STUDENT_RESULT_AMDTO>();
                    var lst4 = await multi.ReadAsync<CSP_MASTER_AMDTO>();

                    list.MarksList = lst2.ToList();
                    list.ResultList = lst3.ToList();
                    list.CSPList = lst4.ToList();

                }
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }
        public async Task<StudentMasterDTO> CheckStudentData(SearchDTO model)
        {
            StudentMasterDTO list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionStringResult);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = "Search",
                    CourseName = model.CourseName,
                    CourseType = model.CourseType,
                    DOB = model.DOB,
                    FName = model.FName,
                    RollNumber = model.RollNumber,
                    ExamTypeName = model.ExamTypeName,
                    SessionName = model.SessionName,
                    HELD_IN = model.HELD_IN

                };
                var data = await con.QueryAsync<StudentMasterDTO>("CheckStudentData_AM", paramList,
                    commandType: CommandType.StoredProcedure);
                list = data.ToList()[0];
            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }


        public async Task<StudentList_Admin> StudentManageData(string Flag, string CourseType, bool IsPG, string SessionName, int CourseId)
        {
            StudentList_Admin list = new();
            // var list = new StudentMasterDTO();
            await using var con = new SqlConnection(_connectionString);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    CourseType = CourseType,
                    IsPG = IsPG,
                    SessionName = SessionName,
                    CourseId = CourseId
                };

                var multi = await con.QueryMultipleAsync("GET_StudentManageData_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<CourseMasterDTO_AM>();
                var lst2 = await multi.ReadAsync<CourseMasterDTO_AM>();
                var lst3 = await multi.ReadAsync<SESSION_MASTER_DTO>();
                var lst4 = await multi.ReadAsync<EXAM_TYPE_MASTER_DTO>();

                list.CourseTypeList = lst1.ToList();
                list.CourseList = lst2.ToList();
                list.SessionList = lst3.ToList();
                list.ExamTypeList = lst4.ToList();



            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public async Task<StudentList_Admin> StudentList_AM(string Flag, string SessionName, string CourseType, bool IsPG, int CourseId, string ExamTypeName, string RollN, int IsLiveStatus, bool IsAdmin,int Page = 1)
        {

            StudentList_Admin list = new();
            await using var con = new SqlConnection(_connectionStringResultDemo);
            con.Open();
            try
            {
                var paramList = new
                {
                    Flag = Flag,
                    SessionName = SessionName,
                    CourseType = CourseType,

                    CourseId = CourseId,
                    ExamTypeName = ExamTypeName,
                    RollNumber = RollN,
                    IsLiveStatus = IsLiveStatus,
                    IsAdmin = IsAdmin,
                    PageNumber= Page
                };
                var multi = await con.QueryMultipleAsync("StudentList_Admin_AM", paramList, commandTimeout: 0,
              commandType: CommandType.StoredProcedure);

                var lst1 = await multi.ReadAsync<PagingDTO>();
                var lst2 = await multi.ReadAsync<StudentMasterDTO>();

                list.StudentList = lst2.ToList();
                list.PagingList = lst1.ToList()[0];


            }
            catch (Exception e)
            {
                //
            }
            finally
            {
                con.Close();
            }
            return list;

        }
    }

}
