using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class AccountTransactionMapper
    {
        public static AccountTransactionModel ToModel(this AccountTransaction accountTransaction)
        {
            if (accountTransaction == null)
                return null;

            AccountTransactionModel accountTransactionModel = new AccountTransactionModel
            {
                AccountTransactionId = accountTransaction.AccountTransactionId,
                AccountId = accountTransaction.AccountId,
                TransactionId = accountTransaction.TransactionId,
                Rights = accountTransaction.Rights,
                CanAdd = accountTransaction.CanAdd,
                CanEdit = accountTransaction.CanEdit,
                CanDelete = accountTransaction.CanDelete,
                CanView = accountTransaction.CanView,
                CanExport = accountTransaction.CanExport,
                ListView = accountTransaction.ListView,
                BasicView = accountTransaction.BasicView,
                DetailView = accountTransaction.DetailView,
                IsPublic = accountTransaction.IsPublic,
                Remarks = accountTransaction.Remarks,
                CreatedBy = accountTransaction.CreatedBy,
                CreatedOn = accountTransaction.CreatedOn,
                UpdatedBy = accountTransaction.UpdatedBy,
                UpdatedOn = accountTransaction.UpdatedOn,
                IsActive = accountTransaction.IsActive
            };

            return accountTransactionModel;
        }
        public static AccountTransaction FromModel(this AccountTransactionModel accountTransactionModel, AccountTransaction accountTransaction)
        {
            accountTransaction.AccountTransactionId = accountTransactionModel.AccountTransactionId;
            accountTransaction.AccountId = accountTransactionModel.AccountId;
            accountTransaction.TransactionId = accountTransactionModel.TransactionId;
            accountTransaction.Rights = accountTransactionModel.Rights;
            accountTransaction.CanAdd = accountTransactionModel.CanAdd;
            accountTransaction.CanEdit = accountTransactionModel.CanEdit;
            accountTransaction.CanDelete = accountTransactionModel.CanDelete;
            accountTransaction.CanView = accountTransactionModel.CanView;
            accountTransaction.CanExport = accountTransactionModel.CanExport;
            accountTransaction.ListView = accountTransactionModel.ListView;
            accountTransaction.BasicView = accountTransactionModel.BasicView;
            accountTransaction.DetailView = accountTransactionModel.DetailView;
            accountTransaction.IsPublic = accountTransactionModel.IsPublic;
            accountTransaction.Remarks = accountTransactionModel.Remarks;
            accountTransaction.IsActive = accountTransactionModel.IsActive;
            accountTransaction.RequestType = accountTransactionModel.RequestType;
            accountTransaction.SetAuditValues(accountTransactionModel.RequestType);

            return accountTransaction;
        }
    }
}
