using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using YPAPI.Core.DBEntity;
using API.Models;
namespace API.Service
{
    public interface INewsImageService
    {
        IEnumerable<NewsImage> GetAllNewsImage();
        IEnumerable<NewsImage> GetNewsImageByNewsId(int newsid);
        //IEnumerable<OrderHistory> GetOrderHistoryByCMISID(int CMISID); 
        //IEnumerable<Group> GetCallOrderHistoryByCMISID(int CMISID);
        //IEnumerable<CallOrderHistory> GetCallOrderHistoryByCSID(string CSID);
        //DataResult<OrderHistory> GetOrderHistoryByCMISID(int CMISID, int pageIndex = 1);
        //GroupTest GetOrderHistoryByOrderID(int OrderID);
        //IEnumerable<OrderHistory> GetOrderHistoryByPackageID(string packageid);
        //IEnumerable<CallOrderHistory> GetCallOrderHistoryByPackageID(string packageid);
        //int CreatetOrder(CallOrderHistory order);
        //IEnumerable<tbl_orderhistory> GetCodeLookupsByType(string category, string CodeType);

        //tbl_orderhistory GetCodeLookup(int ID);
        //IEnumerable<string> GetCodeTypeList(string category);
        //IEnumerable<string> GetCategoryList();

        //int  InsertCodeLookup(tbl_orderhistory code);
        //void UpdateCodeLookup(tbl_orderhistory code);
        //void DeleteCodeLookup(tbl_orderhistory code);
    }
}
