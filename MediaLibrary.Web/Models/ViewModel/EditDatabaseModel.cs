using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MediaLibrary.Web.Models.Domain;
using System.Web.Mvc;

namespace MediaLibrary.Web.Models.ViewModel
{
    public class EditDatabaseModel
    {
        [HiddenInput(DisplayValue=false)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Database name")]
        public string Name { get; set; }

        public List<ListDatabaseRevisionModel> Revisions { get; set; }

        public EditDatabaseModel()
        {

        }

        public EditDatabaseModel(Database database, IQueryable<DatabaseRevision> revisions)
        {
            ID = database.ID;
            Name = database.Name;
            Revisions = revisions.Select(r => new ListDatabaseRevisionModel
            {
                ID = r.ID,
                Revision = r.Revision,
                Timestamp = r.Timestamp
            }).ToList();
        }
    }

    public class ListDatabaseRevisionModel
    {
        [Display(Name="ID")]
        public int ID { get; set; }

        [Display(Name="Reivision")]
        public int Revision { get; set; }

        [Display(Name="Modified")]
        public DateTime Timestamp { get; set; }
    }
}