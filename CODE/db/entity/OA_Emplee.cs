using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_Emplee
    {
        public AnsiString EmpID{ get;set; }
        public AnsiString EmpCode{ get;set; }
        public AnsiString EmpName{ get;set; }
        public AnsiStringFixedLength EmpSex{ get;set; }
        public Int32 EmpAge{ get;set; }
        public AnsiStringFixedLength EmpBrithday{ get;set; }
        public AnsiString EmpNative{ get;set; }
        public AnsiString EmpNation{ get;set; }
        public AnsiString EmpPolitics{ get;set; }
        public AnsiString EmpPosition{ get;set; }
        public AnsiString EmpEducation{ get;set; }
        public AnsiString EmpSpecialty{ get;set; }
        public AnsiString EmpSchool{ get;set; }
        public AnsiStringFixedLength EmpState{ get;set; }
        public AnsiString EmpTel{ get;set; }
        public AnsiString EmpMobile{ get;set; }
        public AnsiString EmpEmail{ get;set; }
        public AnsiString Remart{ get;set; }
        public DateTime CreateTime{ get;set; }
        public AnsiString CreateUserID{ get;set; }
        public AnsiString EmpCard{ get;set; }
        public String EmpAddress{ get;set; }
        public AnsiStringFixedLength EmpType{ get;set; }
        public AnsiStringFixedLength EmpDate{ get;set; }
        public AnsiStringFixedLength EmpContractStart{ get;set; }
        public AnsiStringFixedLength EmpContractEnd{ get;set; }
    }
}