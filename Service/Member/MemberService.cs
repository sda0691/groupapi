﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using System.Linq.Dynamic;
using API.Models;
using System.Web.Http.ModelBinding;

namespace API.Service
{
    public class MemberService : IMemberService
    {
       
        private IGenericRepository<Member> _groupRepository;
        //private IGenericRepository<CallOrderHistory> _callOrderHistoryRepository;
        //private IGenericRepository<OrderTest> _orderTestRepository;

        private System.Data.Entity.DbContext dbContext = new GroupContext(); // new FAMTest.Data.FAMEntities();//

        protected IGenericRepository<Member> MemberRepository
        {
            get
            {
                return _groupRepository == null ? new GenericRepository<Member>(dbContext) : _groupRepository;
            }
            set
            {
                _groupRepository = value;
            }
        }


        

        protected void Save()
        {
            dbContext.SaveChanges();
        }
        // CallOrderHistory
        public IEnumerable<Member> GetAllMember()
        {
            return MemberRepository.GetAll();
        }

        public Member GetMemberByMemberId(int Id)
        {
            var code = MemberRepository.Get(a => a.Id == Id);
            return code;
        }
        //public IEnumerable<CallOrderHistory> GetCallOrderHistoryByCSID(string CSID)
        //{
        //    Expression<Func<CallOrderHistory, bool>> where = a => (a.CS_CustomerID == CSID);
        //    var list = callOrderHistoryRepository.GetMany(where);
        //    list = list.OrderBy("WhenCreated descending");
        //    return list;
        //}

        //public DataResult<OrderHistory> GetOrderHistoryByCMISID(int CMISID, int pageIndex = 1)
        //{
        //    DataResult<OrderHistory> dataResult = new DataResult<OrderHistory>();
        //    Expression<Func<OrderHistory, bool>> where = a => (a.CMIS_CustomerID == CMISID && a.OrderStatus != 0);
        //    var list = orderHistoryRepository.GetMany(where);

        //    dataResult.TotalItems = list.Count();

        //    var sortfield = "PackageID";
        //    //result = result.Take(10);
        //    if (sortfield != null && sortfield.Length > 0)
        //        list = list.OrderBy(sortfield);
        //    list = list.Skip((pageIndex - 1) * 10).Take(10);
        //    var result = list.ToList();

        //    dataResult.Data = result;
        //    return dataResult;
        //}


        //public IEnumerable<OrderHistory> GetOrderHistoryByPackageID(string packageid)
        //{
        //    Expression<Func<OrderHistory, bool>> where = a => (a.PackageID == packageid);
        //    var list = orderHistoryRepository.GetMany(where);
        //    return list;
        //}
        //public IEnumerable<CallOrderHistory> GetCallOrderHistoryByPackageID(string packageid)
        //{
        //    Expression<Func<CallOrderHistory, bool>> where = a => (a.PackageID == packageid && a.TransactionType==1);
        //    var list = callOrderHistoryRepository.GetMany(where);
        //    return list;
        //}

        ////data update with transaction
        //public int CreatetOrder(CallOrderHistory order)
        //{
        //    using (var transaction = dbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            callOrderHistoryRepository.Insert(order);
        //            Save();

        //            transaction.Commit();
        //            return order.ID;
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex.InnerException;
        //        }
        //    }
        //}
    }
}
