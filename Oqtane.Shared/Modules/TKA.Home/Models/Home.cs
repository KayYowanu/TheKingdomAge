using System;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace TKA.Home.Models
{
    [Table("TKAHome")]
    public class Home : IAuditable
    {
        public int HomeId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
