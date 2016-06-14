using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class GoodsMovement
    {
        public AnsiString GoodsMovementID{ get;set; }
        public AnsiStringFixedLength CreateDate{ get;set; }
        public AnsiString GoodsMovementCode{ get;set; }
        public AnsiStringFixedLength BusinessType{ get;set; }
        public AnsiStringFixedLength MoveTypeCode{ get;set; }
        public AnsiStringFixedLength SourceBillType{ get;set; }
        public AnsiString PurCompanyID{ get;set; }
        public AnsiString PurDeptID{ get;set; }
        public AnsiString PurEmpID{ get;set; }
        public AnsiString SupplierID{ get;set; }
        public AnsiStringFixedLength ReceiptDate{ get;set; }
        public AnsiString RecCompanyID{ get;set; }
        public AnsiString RecDeptID{ get;set; }
        public AnsiString RecHandler{ get;set; }
        public AnsiString RecWHID{ get;set; }
        public AnsiString RecWHEmpID{ get;set; }
        public AnsiString SalesCompanyID{ get;set; }
        public AnsiString SalesDepID{ get;set; }
        public AnsiString SalesEmpID{ get;set; }
        public AnsiString CustomerID{ get;set; }
        public AnsiStringFixedLength IssDate{ get;set; }
        public AnsiString IssCompanyID{ get;set; }
        public AnsiString IssDeptID{ get;set; }
        public AnsiString IssHandler{ get;set; }
        public AnsiString IssWHID{ get;set; }
        public AnsiString IssWHEmpID{ get;set; }
        public AnsiString ProCompanyID{ get;set; }
        public AnsiString ProDepID{ get;set; }
        public AnsiString ProEmpID{ get;set; }
        public AnsiString ConCompanyID{ get;set; }
        public AnsiString ConDepID{ get;set; }
        public AnsiString ConEmpID{ get;set; }
        public AnsiString Creator{ get;set; }
        public DateTime CreateTime{ get;set; }
        public AnsiString Editor{ get;set; }
        public DateTime EditTime{ get;set; }
        public AnsiStringFixedLength BillState{ get;set; }
        public AnsiString FirstChecker{ get;set; }
        public DateTime FirstCheckTime{ get;set; }
        public AnsiString FirstCheckView{ get;set; }
        public AnsiStringFixedLength IsRed{ get;set; }
        public AnsiString Remark{ get;set; }
        public AnsiString SecondCheckerName{ get;set; }
        public AnsiString ReaderName{ get;set; }
    }
}