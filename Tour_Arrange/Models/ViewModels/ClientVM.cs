using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Tour_Arrange.Models.ViewModels
{
    public class ClientVM
    {
        public ClientVM()
        {
            this.SpotList = new List<int>();
        }
        public int ClientId { get; set; }
        [Required, StringLength(50), Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Picture")]
        public HttpPostedFileBase PicturePath { get; set; }
        [Display(Name = "Marital Status")]
        public bool MaritalStatus { get; set; }
        public List<int> SpotList { get; set; }

    }
}