namespace Rfid.Helpers
{
    class StringsMapper
    {
        public static string UpdateImage = "addu_lbi_UpdateImage";
        public static string UserInformation = "addu_lbi_UserInformation";
        public static string ContactPeople = "addu_lbi_ContactPeople";
        public static string WorkTime = "addu_lbi_WorkTime";
        public static string DepartmentInfo = "addu_lbi_DepInfo";
        public static string RfidNumber = "addu_RfidNumber";
        public static string InOfficeStatus = "au_l_StatusOn";
        public static string OutOfficeStatus = "au_l_StatusOff";
        public static string SetEmployeeOut = "au_btnComout";
        public static string SetEmployeeIn = "au_btnComin";

        public static string FirstName = "u_l_FirstName";
        public static string SecondName = "u_l_SecondName";
        public static string ThirdName = "u_l_ThirdName";
        public static string DepartmentName = "u_l_DepartmentName";
        public static string Phone = "u_l_Phone";

        public static string AddingDate = "lin_l_AddingDate";
        public static string RfidNum = "lin_l_rfidCode";
        public static string Comment = "lin_l_Comment";

        public static string Lost = "LR_LouseRfidComment";

        public static string StartTime = "dgc_stat";
        public static string EndTime = "dgc_end";
        public static string TimeLate = "dgc_valid";
        public static string DinnerTime = "dgc_dinner";
        public static string Date = "dgc_date";
        public static string Day = "dgc_day";
        public static string ComeIn = "dgc_timeIn";
        public static string ComeOut =  "dgc_timeOut";
        public static string TimeIn = "dgc_lenghtInside";
        public static string TimeOut = "dgc_leanghtOuside";
        public static string Count = "dgc_count";
        public static string InCount = "dgc_countInSide";
        public static string OutCount = "dgc_countOutSide";

        public static string Holidays = "fp_btn_Holidays";
        public static string HoursWorked = "fp_lbi_HourseWork";
        public static string MainInfo = "fp_lbi_MainInformation";
        public static string MonthReport = "fp_lbi_MonthlyReport";
        public static string DepartmentReport = "fp_lbi_DepReport";
        public static string Excel = "fp_lbi_Excel";

        public static string ErrorHeader = "msb_errorHeader";
        public static string ErrorUserPhoto = "msb_userDontHavePhoto";
        public static string ErrorUserPID = "msb_setPIB";
        public static string ErrorRfidNFound = "msb_rfidNotFound";
        public static string ErrorNoDateSel = "msb_notSelectDate";
        public static string ErrorUserNFound = "msb_userNotFount";
        public static string ErrorUserNSel = "msb_userNotSelected";
        public static string ErrorUserTimeNFound = "msb_userDontHaveTime";
        public static string EnterRfid = "msb_enterRfid";
        public static string EnterNumber = "msb_notEnterNumber";

        public static string ErrorEmptyUserNames = "S_Ex_emptyNameSurname";
        public static string ErrorEmptyDepartment = "S_Ex_emptyDepInfo";
        public static string ErrorBindingRfid = "S_Ex_RfidBindErr";
        public static string ErrorWorkTime = "S_Ex_InvalidTime";

        public static string ErrorEnterRfid = "GRW_Err_EnterRfidPleas";
        public static string AllRfidArchive = "GRW_AllRfidArhive";

        public static string Week = "at_Week";
        public static string Month = "at_Month";
        public static string Year = "at_Year";
        public static string TimePeriod = "at_TimeSelected";

        public static string PhonesRegExp = @"^(\d{7}|\d{10})$";
        public static string RfidRegExp = @"^(\d{1,10})$";
    }
}
