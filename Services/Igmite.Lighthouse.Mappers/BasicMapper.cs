using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Igmite.Lighthouse.Mappers
{
    public static class BasicMapper
    {
        public static T SetAuditValues<T>(this T obj, RequestType requestType)
        {
            string userId = "System";
            //if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    AccountPrincipal userPrincipal = System.Web.HttpContext.Current.User as AccountPrincipal;
            //    userId = userPrincipal.UserId;
            //}

            userId = obj.GetType().GetProperty("AuthUserId").GetValue(obj).ToString();

            if (requestType == RequestType.New)
            {
                obj.GetType().GetProperty("CreatedBy").SetValue(obj, userId, null);
                obj.GetType().GetProperty("CreatedOn").SetValue(obj, Constants.GetCurrentDateTime, null);
            }

            if (obj.GetType().GetProperty("UpdatedBy") != null)
                obj.GetType().GetProperty("UpdatedBy").SetValue(obj, userId, null);

            if (obj.GetType().GetProperty("UpdatedOn") != null)
                obj.GetType().GetProperty("UpdatedOn").SetValue(obj, Constants.GetCurrentDateTime, null);

            return obj;
        }

        public static object ToModel<T>(this object obj, T type)
        {
            var tmp = Activator.CreateInstance(Type.GetType(type.ToString()));

            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                try
                {
                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
                                              pi.GetValue(obj, null), null);
                }
                catch { }
            }

            return tmp;
        }

        public static object ToModels<T>(this IList<T> list, Type t)
        {
            var genericType = typeof(List<>).MakeGenericType(t);

            var l = Activator.CreateInstance(genericType);

            MethodInfo addMethod = l.GetType().GetMethod("Add");

            foreach (T item in list)
            {
                addMethod.Invoke(l, new object[] { item.ToModel(t) });
            }

            return l;
        }
    }
}