using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travals.Models
{
    public class AccountModel
    {
        public int AccountID { get; set; }
        public string AccountFName { get; set; }
        public string AccountLName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }

        TravelsEntities ctx = new TravelsEntities();
        public void insert_Account(AccountModel c)
        {

            Account cat = new Account();
            cat.Fname = c.AccountFName;
            cat.Lname = c.AccountLName;
            cat.Email = c.AccountEmail;
            cat.Password = c.AccountPassword;
            ctx.Accounts.Add(cat);
            ctx.SaveChanges();

        }
        public List<AccountModel> dispaly_Account()
        {
            List<AccountModel> files = null;

            files = ctx.Accounts.Select(c => new AccountModel()
            {
                AccountID = c.ID,
                AccountFName = c.Fname,
                AccountLName = c.Lname,
                AccountPassword = c.Password
            }).ToList<AccountModel>();

            return files;
        }
        public void delete_Account(int id)
        {

            var u = ctx.Categories.Where(c => c.ID == id).FirstOrDefault();
            ctx.Entry(u).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();

        }
        public AccountModel upadate_Account(int id)
        {
            AccountModel files = null;

            files = ctx.Accounts.Where(c => c.ID == id).Select(c => new AccountModel()
            {
                AccountID = c.ID,
                AccountFName = c.Fname,
                AccountLName = c.Lname,
                AccountPassword = c.Password,
                AccountEmail = c.Email
            }).SingleOrDefault();

            return files;
        }
        public AccountModel Check_Account(string email)
        {
            AccountModel files = null;

            files = ctx.Accounts.Where(c => c.Email == email).Select(c => new AccountModel()
            {
                AccountID = c.ID,
                AccountFName = c.Fname.ToString(),
                AccountLName = c.Lname.ToString(),
                AccountPassword = c.Password.ToString(),
                AccountEmail = c.Email.ToString()
            }).SingleOrDefault();

            return files;
        }
        public void upadate_Account(AccountModel c)
        {

            var result = ctx.Accounts.SingleOrDefault(b => b.ID == c.AccountID);
            if (result != null)
            {
                result.Fname = c.AccountFName;
                result.Lname = c.AccountLName;
                result.Password = c.AccountPassword;
                result.Email = c.AccountEmail;
                ctx.SaveChanges();
            }

        }
        
    }
}