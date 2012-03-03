using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Web.Models.Domain
{
    public class DatabaseRevision : BaseObject
    {
        [ForeignKey("Database")]
        public int DatabaseID { get; set; }

        public virtual Database Database { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

        public int Revision { get; set; }
    }
}