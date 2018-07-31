﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel.DataEntity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class alluneedbEntities : DbContext
    {
        public alluneedbEntities()
            : base("name=alluneedbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Classification> Classifications { get; set; }
        public virtual DbSet<ClassificationScheme> ClassificationSchemes { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<ContentComment> ContentComments { get; set; }
        public virtual DbSet<ContentItem> ContentItems { get; set; }
        public virtual DbSet<ContentTag> ContentTags { get; set; }
        public virtual DbSet<DisplayContent> DisplayContents { get; set; }
        public virtual DbSet<MediaContent> MediaContents { get; set; }
        public virtual DbSet<Microsite> Microsites { get; set; }
        public virtual DbSet<RelatedContentItem> RelatedContentItems { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<Userhist> Userhists { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRegistrationToken> UserRegistrationTokens { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<TransactionHi> TransactionHis { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public virtual DbSet<MicroClassification> MicroClassifications { get; set; }
        public virtual DbSet<GeoArea> GeoAreas { get; set; }
        public virtual DbSet<PromotionContent> PromotionContents { get; set; }
        public virtual DbSet<NotifiRequest> NotifiRequests { get; set; }
        public virtual DbSet<CountLike> CountLikes { get; set; }
        public virtual DbSet<ProductInforExt> ProductInforExts { get; set; }
        public virtual DbSet<ContentPackage> ContentPackages { get; set; }
        public virtual DbSet<ContentView> ContentViews { get; set; }
        public virtual DbSet<EmailSupport> EmailSupports { get; set; }
        public virtual DbSet<Ticker> Tickers { get; set; }
    
        public virtual int GetDataUserInfor(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetDataUserInfor", usernameParameter);
        }
    
        public virtual int GetDataUserInforMoney(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetDataUserInforMoney", usernameParameter);
        }
    
        public virtual int GetDataUserInforTransferMoney(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetDataUserInforTransferMoney", usernameParameter);
        }
    
        public virtual int Al_ProcGetDataUserInfor(string u)
        {
            var uParameter = u != null ?
                new ObjectParameter("u", u) :
                new ObjectParameter("u", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Al_ProcGetDataUserInfor", uParameter);
        }
    
        public virtual int Al_ProcGetDataUserInforTransferMoney(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Al_ProcGetDataUserInforTransferMoney", usernameParameter);
        }
    
        public virtual ObjectResult<ContentItemPagination_Result> ContentItemPagination(Nullable<int> rowSkip, Nullable<int> rowTake, Nullable<int> type)
        {
            var rowSkipParameter = rowSkip.HasValue ?
                new ObjectParameter("rowSkip", rowSkip) :
                new ObjectParameter("rowSkip", typeof(int));
    
            var rowTakeParameter = rowTake.HasValue ?
                new ObjectParameter("rowTake", rowTake) :
                new ObjectParameter("rowTake", typeof(int));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ContentItemPagination_Result>("ContentItemPagination", rowSkipParameter, rowTakeParameter, typeParameter);
        }
    
        public virtual int DeleteClassificationById(Nullable<int> parentCate)
        {
            var parentCateParameter = parentCate.HasValue ?
                new ObjectParameter("ParentCate", parentCate) :
                new ObjectParameter("ParentCate", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteClassificationById", parentCateParameter);
        }
    
        public virtual int DeleteContentItemById(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteContentItemById", idParameter);
        }
    
        public virtual int DeleteMicrositeById(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteMicrositeById", idParameter);
        }
    
        public virtual ObjectResult<GetLstProductOederExt_Result> GetLstProductOederExt(Nullable<int> orderId)
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderId", orderId) :
                new ObjectParameter("OrderId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLstProductOederExt_Result>("GetLstProductOederExt", orderIdParameter);
        }
    
        public virtual int GetlstPromotionProductByCateId(Nullable<int> cateId)
        {
            var cateIdParameter = cateId.HasValue ?
                new ObjectParameter("CateId", cateId) :
                new ObjectParameter("CateId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetlstPromotionProductByCateId", cateIdParameter);
        }
    
        public virtual ObjectResult<GetlstPromotionProductByPromotionId_Result> GetlstPromotionProductByPromotionId(Nullable<int> promotionId)
        {
            var promotionIdParameter = promotionId.HasValue ?
                new ObjectParameter("PromotionId", promotionId) :
                new ObjectParameter("PromotionId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetlstPromotionProductByPromotionId_Result>("GetlstPromotionProductByPromotionId", promotionIdParameter);
        }
    
        public virtual int GetLstUserNotIsRole(Nullable<int> roleID, Nullable<int> pageNum, Nullable<int> recorNum)
        {
            var roleIDParameter = roleID.HasValue ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(int));
    
            var pageNumParameter = pageNum.HasValue ?
                new ObjectParameter("PageNum", pageNum) :
                new ObjectParameter("PageNum", typeof(int));
    
            var recorNumParameter = recorNum.HasValue ?
                new ObjectParameter("RecorNum", recorNum) :
                new ObjectParameter("RecorNum", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetLstUserNotIsRole", roleIDParameter, pageNumParameter, recorNumParameter);
        }
    
        public virtual int GetProductByParentCate(Nullable<int> parentCate, Nullable<int> index, Nullable<int> numOfCate, Nullable<int> stateId)
        {
            var parentCateParameter = parentCate.HasValue ?
                new ObjectParameter("ParentCate", parentCate) :
                new ObjectParameter("ParentCate", typeof(int));
    
            var indexParameter = index.HasValue ?
                new ObjectParameter("index", index) :
                new ObjectParameter("index", typeof(int));
    
            var numOfCateParameter = numOfCate.HasValue ?
                new ObjectParameter("NumOfCate", numOfCate) :
                new ObjectParameter("NumOfCate", typeof(int));
    
            var stateIdParameter = stateId.HasValue ?
                new ObjectParameter("StateId", stateId) :
                new ObjectParameter("StateId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetProductByParentCate", parentCateParameter, indexParameter, numOfCateParameter, stateIdParameter);
        }
    
        public virtual int GetProductByParentCateAllType(Nullable<int> parentCate, Nullable<int> numOfCate, Nullable<int> take)
        {
            var parentCateParameter = parentCate.HasValue ?
                new ObjectParameter("ParentCate", parentCate) :
                new ObjectParameter("ParentCate", typeof(int));
    
            var numOfCateParameter = numOfCate.HasValue ?
                new ObjectParameter("NumOfCate", numOfCate) :
                new ObjectParameter("NumOfCate", typeof(int));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("take", take) :
                new ObjectParameter("take", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetProductByParentCateAllType", parentCateParameter, numOfCateParameter, takeParameter);
        }
    
        public virtual int GetProductByParentCateBuyCount(Nullable<int> parentCate, Nullable<int> numOfCate)
        {
            var parentCateParameter = parentCate.HasValue ?
                new ObjectParameter("ParentCate", parentCate) :
                new ObjectParameter("ParentCate", typeof(int));
    
            var numOfCateParameter = numOfCate.HasValue ?
                new ObjectParameter("NumOfCate", numOfCate) :
                new ObjectParameter("NumOfCate", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetProductByParentCateBuyCount", parentCateParameter, numOfCateParameter);
        }
    
        public virtual int GetProductByParentCateDiscount(Nullable<int> parentCate, Nullable<int> numOfCate)
        {
            var parentCateParameter = parentCate.HasValue ?
                new ObjectParameter("ParentCate", parentCate) :
                new ObjectParameter("ParentCate", typeof(int));
    
            var numOfCateParameter = numOfCate.HasValue ?
                new ObjectParameter("NumOfCate", numOfCate) :
                new ObjectParameter("NumOfCate", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetProductByParentCateDiscount", parentCateParameter, numOfCateParameter);
        }
    
        public virtual int GetProductByParentCateViewCount(Nullable<int> parentCate, Nullable<int> numOfCate)
        {
            var parentCateParameter = parentCate.HasValue ?
                new ObjectParameter("ParentCate", parentCate) :
                new ObjectParameter("ParentCate", typeof(int));
    
            var numOfCateParameter = numOfCate.HasValue ?
                new ObjectParameter("NumOfCate", numOfCate) :
                new ObjectParameter("NumOfCate", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetProductByParentCateViewCount", parentCateParameter, numOfCateParameter);
        }
    
        public virtual ObjectResult<GetProductHome_Result> GetProductHome(Nullable<int> idCategory, Nullable<int> limit)
        {
            var idCategoryParameter = idCategory.HasValue ?
                new ObjectParameter("IdCategory", idCategory) :
                new ObjectParameter("IdCategory", typeof(int));
    
            var limitParameter = limit.HasValue ?
                new ObjectParameter("Limit", limit) :
                new ObjectParameter("Limit", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProductHome_Result>("GetProductHome", idCategoryParameter, limitParameter);
        }
    
        public virtual int SearchProductByCate(string keyword, Nullable<int> stateId, Nullable<int> idCate, Nullable<int> micrositeID, Nullable<int> skip, Nullable<int> take)
        {
            var keywordParameter = keyword != null ?
                new ObjectParameter("keyword", keyword) :
                new ObjectParameter("keyword", typeof(string));
    
            var stateIdParameter = stateId.HasValue ?
                new ObjectParameter("stateId", stateId) :
                new ObjectParameter("stateId", typeof(int));
    
            var idCateParameter = idCate.HasValue ?
                new ObjectParameter("idCate", idCate) :
                new ObjectParameter("idCate", typeof(int));
    
            var micrositeIDParameter = micrositeID.HasValue ?
                new ObjectParameter("MicrositeID", micrositeID) :
                new ObjectParameter("MicrositeID", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("skip", skip) :
                new ObjectParameter("skip", typeof(int));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("take", take) :
                new ObjectParameter("take", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SearchProductByCate", keywordParameter, stateIdParameter, idCateParameter, micrositeIDParameter, skipParameter, takeParameter);
        }
    
        public virtual int TABLEVIEWSEARCH(string tABLENAME, string sEARCHSTRING)
        {
            var tABLENAMEParameter = tABLENAME != null ?
                new ObjectParameter("TABLENAME", tABLENAME) :
                new ObjectParameter("TABLENAME", typeof(string));
    
            var sEARCHSTRINGParameter = sEARCHSTRING != null ?
                new ObjectParameter("SEARCHSTRING", sEARCHSTRING) :
                new ObjectParameter("SEARCHSTRING", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TABLEVIEWSEARCH", tABLENAMEParameter, sEARCHSTRINGParameter);
        }
    }
}
