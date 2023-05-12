using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionModel ToModel(this Transaction transaction)
        {
            if (transaction == null)
                return null;

            TransactionModel transactionModel = new TransactionModel
            {
                TransactionId = transaction.TransactionId,
                Code = transaction.Code,
                Name = transaction.Name,
                PageTitle = transaction.PageTitle,
                PageDescription = transaction.PageDescription,
                UrlAction = transaction.UrlAction,
                UrlController = transaction.UrlController,
                UrlPara = transaction.UrlPara,
                RouteUrl = transaction.RouteUrl,
                DisplayOrder = transaction.DisplayOrder,
                Remarks = transaction.Remarks,
                CreatedBy = transaction.CreatedBy,
                CreatedOn = transaction.CreatedOn,
                UpdatedBy = transaction.UpdatedBy,
                UpdatedOn = transaction.UpdatedOn,
                IsActive = transaction.IsActive
            };

            //transaction.AccountTransactions.ForEach((accountTransaction) => transactionModel.AccountTransactionModels.Add(accountTransaction.ToModel()));
            //transaction.RoleTransactions.ForEach((roleTransaction) => transactionModel.RoleTransactionModels.Add(roleTransaction.ToModel()));
            //transaction.SiteSubHeaders.ForEach((siteSubHeader) => transactionModel.SiteSubHeaderModels.Add(siteSubHeader.ToModel()));

            return transactionModel;
        }
        public static Transaction FromModel(this TransactionModel transactionModel, Transaction transaction)
        {
            transaction.TransactionId = transactionModel.TransactionId;
            transaction.Code = transactionModel.Code;
            transaction.Name = transactionModel.Name;
            transaction.PageTitle = transactionModel.PageTitle;
            transaction.PageDescription = transactionModel.PageDescription;
            transaction.UrlAction = transactionModel.UrlAction;
            transaction.UrlController = transactionModel.UrlController;
            transaction.UrlPara = transactionModel.UrlPara;
            transaction.RouteUrl = transactionModel.RouteUrl;
            transaction.DisplayOrder = transactionModel.DisplayOrder;
            transaction.Remarks = transactionModel.Remarks;
            transaction.IsActive = transactionModel.IsActive;
            transaction.RequestType = transactionModel.RequestType;
            transaction.SetAuditValues(transactionModel.RequestType);

            //// Handling multiple transaction transactionItems
            //foreach (var transactionItemModel in transactionModel.AccountTransactionModels)
            //{
            //    AccountTransaction transactionItem = transaction.AccountTransactions.FirstOrDefault(f => f.AccountTransactionId == transactionItemModel.AccountTransactionId);
            //    if (transactionItem == null || transactionModel.RequestType == RequestType.New)
            //    {
            //        transactionItem = new AccountTransaction();
            //        transactionItem.AccountTransactionId = Guid.NewGuid();
            //        transactionItem.TransactionId = transaction.TransactionId;
            //    }
            //    transactionItem = transactionItemModel.FromModel(transactionItem);
            //    transactionItem.SetAuditValues(transactionModel.RequestType);

            //    transaction.AccountTransactions.Add(transactionItem);
            //}

            //// Handling multiple transaction transactionItems
            //foreach (var transactionItemModel in transactionModel.RoleTransactionModels)
            //{
            //    RoleTransaction transactionItem = transaction.RoleTransactions.FirstOrDefault(f => f.RoleTransactionId == transactionItemModel.RoleTransactionId);
            //    if (transactionItem == null || transactionModel.RequestType == RequestType.New)
            //    {
            //        transactionItem = new RoleTransaction();
            //        transactionItem.RoleTransactionId = Guid.NewGuid();
            //        transactionItem.TransactionId = transaction.TransactionId;
            //    }
            //    transactionItem = transactionItemModel.FromModel(transactionItem);
            //    transactionItem.SetAuditValues(transactionModel.RequestType);

            //    transaction.RoleTransactions.Add(transactionItem);
            //}

            //// Handling multiple transaction headers
            //foreach (var headerModel in transactionModel.SiteSubHeaderModels)
            //{
            //    SiteSubHeader header = transaction.SiteSubHeaders.FirstOrDefault(f => f.SiteSubHeaderId == headerModel.SiteSubHeaderId);
            //    if (header == null || transactionModel.RequestType == RequestType.New)
            //    {
            //        header = new SiteSubHeader();
            //        header.SiteSubHeaderId = Guid.NewGuid();
            //        header.TransactionId = transaction.TransactionId;
            //    }
            //    header = headerModel.FromModel(header);
            //    header.SetAuditValues(transactionModel.RequestType);

            //    transaction.SiteSubHeaders.Add(header);
            //}

            return transaction;
        }
    }
}
