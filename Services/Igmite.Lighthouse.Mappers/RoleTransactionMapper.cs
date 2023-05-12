using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class RoleTransactionMapper
    {
        public static RoleTransactionModel ToModel(this RoleTransaction roleTransaction)
        {
            if (roleTransaction == null)
                return null;

            RoleTransactionModel roleTransactionModel = new RoleTransactionModel
            {
                RoleTransactionId = roleTransaction.RoleTransactionId,
                RoleId = roleTransaction.RoleId,
                TransactionId = roleTransaction.TransactionId,
                Rights = roleTransaction.Rights,
                CanAdd = roleTransaction.CanAdd,
                CanEdit = roleTransaction.CanEdit,
                CanDelete = roleTransaction.CanDelete,
                CanView = roleTransaction.CanView,
                CanExport = roleTransaction.CanExport,
                ListView = roleTransaction.ListView,
                BasicView = roleTransaction.BasicView,
                DetailView = roleTransaction.DetailView,
                IsPublic = roleTransaction.IsPublic,
                Remarks = roleTransaction.Remarks,
                CreatedBy = roleTransaction.CreatedBy,
                CreatedOn = roleTransaction.CreatedOn,
                UpdatedBy = roleTransaction.UpdatedBy,
                UpdatedOn = roleTransaction.UpdatedOn,
                IsActive = roleTransaction.IsActive
            };

            return roleTransactionModel;
        }
        public static RoleTransaction FromModel(this RoleTransactionModel roleTransactionModel, RoleTransaction roleTransaction)
        {
            roleTransaction.RoleTransactionId = roleTransactionModel.RoleTransactionId;
            roleTransaction.RoleId = roleTransactionModel.RoleId;
            roleTransaction.TransactionId = roleTransactionModel.TransactionId;
            roleTransaction.Rights = roleTransactionModel.Rights;
            roleTransaction.CanAdd = roleTransactionModel.CanAdd;
            roleTransaction.CanEdit = roleTransactionModel.CanEdit;
            roleTransaction.CanDelete = roleTransactionModel.CanDelete;
            roleTransaction.CanView = roleTransactionModel.CanView;
            roleTransaction.CanExport = roleTransactionModel.CanExport;
            roleTransaction.ListView = roleTransactionModel.ListView;
            roleTransaction.BasicView = roleTransactionModel.BasicView;
            roleTransaction.DetailView = roleTransactionModel.DetailView;
            roleTransaction.IsPublic = roleTransactionModel.IsPublic;
            roleTransaction.Remarks = roleTransactionModel.Remarks;
            roleTransaction.IsActive = roleTransactionModel.IsActive;
            roleTransaction.RequestType = roleTransactionModel.RequestType;
            roleTransaction.SetAuditValues(roleTransactionModel.RequestType);

            return roleTransaction;
        }
    }
}
