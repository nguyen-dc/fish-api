using FLS.ServerSide.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.SharingObject;
using FLS.ServerSide.Model.Scope;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace FLS.ServerSide.EFCore.Services
{
    public class EFCoreServiceBase : IEFCoreServiceBase
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        public EFCoreServiceBase(FLSDbContext _context, IScopeContext _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
        }
        public IQueryable<T> CallStored<T>(string _storedName, object _params) where T: class
        {
            List<MySqlParameter> lst = null;
            string query = $"CALL {_storedName}";
            
            if (_params != null)
            {
                lst = new List<MySqlParameter>();
                PropertyInfo[] props = _params.GetType().GetProperties();
                string prs = "";
                foreach(var prop in props)
                {
                    prs += "@" + prop.Name + ",";
                    MySqlParameter param = new MySqlParameter("@" + prop.Name, prop.GetValue(_params));
                    lst.Add(param);
                }
                query += $"({prs.Trim(',')})";
            }
            var item = context
                .Query<T>()
                .FromSql($"{query}", lst.ToArray());
            return item;
        }
    }
}
