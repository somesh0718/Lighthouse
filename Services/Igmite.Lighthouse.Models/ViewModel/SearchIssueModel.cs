using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Igmite.Lighthouse.Models
{
    public class SearchIssueModel : BaseSearchModel
    {
        [DataMember]
        [Display(Name = "Reported By", Description = "Reported By")]
        public string ReportedBy { get; set; }
    }
}
