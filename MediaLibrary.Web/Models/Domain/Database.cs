using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Web.Models.Domain
{
    public class Database : BaseObject
    {
        [ForeignKey("Owner")]
        public int OwnerID { get; set; }

        public UserAccount Owner { get; set; }

        public string Name { get; set; }

        public int Revision { get; set; }

        public string PublicIdentifier { get; set; }
    }
}