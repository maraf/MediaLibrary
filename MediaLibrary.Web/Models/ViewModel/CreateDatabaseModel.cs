using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MediaLibrary.Web.Models.Domain;
using System.Web.Mvc;

namespace MediaLibrary.Web.Models.ViewModel
{
    public class CreateDatabaseModel
    {
        [Required]
        [Display(Name="Database name")]
        public string Name { get; set; }

        [Display(Name="Serialized file content")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }
    }
}