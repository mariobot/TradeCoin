﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DataEntity;
using System.Data.Entity;
using System.Collections;
using DataModel.Extension;
using DataModel.DataViewModel;

namespace DataModel.DataStore
{
    public partial class Ctrl : Core
    {
        public async Task<int> CreateUserPackageAsync(UserPackage ObjUserPackage)
        {
            try
            {
                db.UserPackages.Add(ObjUserPackage);
                return await db.SaveChangesAsync();

            }
            catch(Exception e)
            {
                return (int)EnumCore.Result.action_false;
            }

        }

        public int CreateUserPackage(UserPackage ObjUserPackage)
        {
            try
            {
                db.UserPackages.Add(ObjUserPackage);
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                return (int)EnumCore.Result.action_false;
            }

        }

        public string GetLastUpgradeToken(long UserID, long awaitPackageId)
        {
            UserPackage ObjUserPackage = db.UserPackages.Where(s => s.UpgradeUID.Value == UserID && s.PackageId.Value == awaitPackageId 
                    && s.StateId == (int)EnumCore.StateType.cho_duyet).OrderByDescending(s => s.Id).FirstOrDefault();
            if (ObjUserPackage != null)
                return ObjUserPackage.UpgradeToken;
            else
                return "";

        }

        public UserPackage GetlastAwaitUserPackage(long UserID)
        {
            UserPackage ObjUserPackage = db.UserPackages.Where(s => s.UpgradeUID.Value == UserID && s.StateId == (int)EnumCore.StateType.cho_duyet).OrderByDescending(s => s.Id).FirstOrDefault();
            return ObjUserPackage ;
        }

        public int CreateUpdateUserPackage(User ObjUser, long PackageID,int StateId,string StateName, string code, long OldPackageID, string OldPackageName)
        {
            try
            {
         
                Package ObjNewPackage = this.GetObjPackage(PackageID);

                UserPackage objUserPackage = new UserPackage();
                objUserPackage.CrtdDT = DateTime.Now;
                objUserPackage.PackageId = ObjNewPackage.PackageId;
                objUserPackage.PackageName = ObjNewPackage.PackageName;

                objUserPackage.OldPackageID = OldPackageID;
                objUserPackage.OldPackageName = OldPackageName;

                objUserPackage.UpgradeUID = ObjUser.Id;
                objUserPackage.UpgradeUserName = ObjUser.EMail;
                objUserPackage.StateId = StateId;
                objUserPackage.StateName = StateName;
                objUserPackage.UpgradeToken = code;
                int rs = this.CreateUserPackage(objUserPackage);
                return rs;
            }
            catch (Exception e)
            {
                return (int)EnumCore.Result.action_false;
            }
        }

        public List<UserPackage> GetlstUserPackage(long UserID)
        {
            List<UserPackage> lstResult = new List<UserPackage>();
            lstResult = db.UserPackages.Where(s => s.UpgradeUID == UserID).OrderByDescending(s=>s.Id).ToList();
            return lstResult;

        }

        public  IQueryable<UserPackage> GetlstUserPackageIquery()
        {
            IQueryable<UserPackage> lstResult = lstResult = db.UserPackages;

            return lstResult;

        }


        //public int DeleteUserPackage(long ContentId)
        //{
        //    List<UserPackage> lstUserPackage = db.UserPackages.Where(s => s.ContentId == ContentId && s.ContentType == Type).ToList();
        //    foreach (UserPackage item in lstUserPackage)
        //    {
        //        db.UserPackages.Remove(item);
        //        db.SaveChanges();
        //    }
        //    return (int)EnumCore.Result.action_true;
        //}

    }
}
