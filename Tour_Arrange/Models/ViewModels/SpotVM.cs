using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Tour_Arrange.Models.ViewModels
{
    public class SpotVM
    {
        public int SpotId { get; set; }
        [Required, StringLength(50), Display(Name = "Spot Name")]
        public string SpotName { get; set; }
        [Display(Name = "Spot View")]
        public string SpotPicture { get; set; }
        [Display(Name = "Spot View")]
        public HttpPostedFileBase PicturePath { get; set; }
    }
}