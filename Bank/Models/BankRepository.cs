using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bank.ViewModel;

namespace Bank.Models
{
    public class BankRepository
    {
        public const string ALL = "all";
        public BankEntities db = new BankEntities();
        public List<ClientBankAccount> ListAccounts(string accountType)
        {
            accountType = accountType.ToLower();
            var query = from ba in db.BankAccounts
                        from c in ba.Clients
                        select new ClientBankAccount
                        {
                            AccountNumber = ba.accountNum,
                            ClientID = c.clientID,
                            LastName = c.lastName,
                            FirstName = c.firstName,
                            AccountType = ba.accountType,
                            Balance = (decimal)ba.balance
                        };
            if (accountType != ALL)
            {
                query = query.Where(ba => ba.AccountType == accountType);
            }
            return query.ToList();
        }
        internal ClientBankAccount Find(int? id)
        {
            var query = from ba in db.BankAccounts
                        from c in ba.Clients
                        where id == ba.accountNum
                        select new ClientBankAccount
                        {
                            ClientID = c.clientID,
                            LastName = c.lastName,
                            FirstName = c.firstName,
                            AccountNumber = ba.accountNum,
                            AccountType = ba.accountType,
                            Balance = (decimal)ba.balance
                        };
            if (query == null)
            {
                return null;
            }
            return query.FirstOrDefault();
        }

        internal bool Update(ClientBankAccount bankAccount)
        {
            if (bankAccount == null) return false;

            BankAccount ba = db.BankAccounts.Find(bankAccount.AccountNumber);
            Client c = db.Clients.Find(bankAccount.ClientID);

            try
            {
                ba.balance = bankAccount.Balance;
                c.firstName = bankAccount.FirstName;
                c.lastName = bankAccount.LastName;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}