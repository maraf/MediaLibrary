using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MediaLibrary.Web.Models.ViewModel
{
    public class EditRevisionModel
    {
        [HiddenInput(DisplayValue=false)]
        public int DatabaseID { get; set; }

        [HiddenInput(DisplayValue=false)]
        public int RevisionID { get; set; }

        [Display(Name="Serialized file content")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }
    }
}