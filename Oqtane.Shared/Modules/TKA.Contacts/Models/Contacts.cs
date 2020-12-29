using System;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace TKA.Contacts.Models
{
    [Table("TKAContacts")]
    public class Contacts 
    {
        public int ContactsId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Message { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
