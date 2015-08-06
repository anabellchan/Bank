using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModel
{
    public class ClientBankAccount
    {
        public int AccountNumber { get; set; }
        public int ClientID { get; set; }

        //  Last names and first names may only include alphabetical characters, spaces, and hyphens.  
        //  Last names and first names must start with an alphabetical character.

        [Required(ErrorMessage = "Firstname cannot be empty.")]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z\- ]+$", ErrorMessage = "Invalid firstname.  Numbers and symbols are not allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lastname cannot be empty.")]
        public string FirstName { get; set; }
        public string AccountType { get; set; }
        [Required(ErrorMessage = "Balance amount cannot be empty.")]
        [RegularExpression(@"^[0-9]+.[0-9]{2}$", ErrorMessage="Balance amount should have 2 decimal numbers.")]
        public decimal Balance { get; set; }

        public ClientBankAccount() { }
    }
}