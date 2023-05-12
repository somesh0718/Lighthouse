using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class BaseSearchModel
    {
        public BaseSearchModel()
        {
            this.PageIndex = 1;
            this.PageSize = Constants.PageSize;
        }

        [DataMember]
        [Display(Name = "User Role", Description = "User Role")]
        public string RoleId { get; set; }

        [DataMember]
        [Display(Name = "User Id", Description = "User Id")]
        public string UserId { get; set; }

        [DataMember]
        [Display(Name = "User Type Id", Description = "User Type Id")]
        public Guid? UserTypeId { get; set; }

        [DataMember]
        [Display(Name = "Academic Year", Description = "Academic Year")]
        public Guid? AcademicYearId { get; set; }

        [DataMember]
        [Display(Name = "Vocational Training Provider", Description = "Vocational Training Provider")]
        public Guid? VTPId { get; set; }

        [DataMember]
        [Display(Name = "Vocational Coordinator", Description = "Vocational Coordinator")]
        public Guid? VCId { get; set; }

        [DataMember]
        [Display(Name = "Vocational Trainer", Description = "Vocational Trainer")]
        public Guid? VTId { get; set; }

        [DataMember]
        [Display(Name = "Head Master", Description = "Head Master")]
        public Guid? HMId { get; set; }

        [DataMember]
        [Display(Name = "Sector", Description = "Sector")]
        public Guid? SectorId { get; set; }

        [DataMember]
        [Display(Name = "Job Role", Description = "Job Role")]
        public Guid? JobRoleId { get; set; }

        [DataMember]
        [Display(Name = "School", Description = "School")]
        public Guid? SchoolId { get; set; }

        [DataMember]
        [Display(Name = "Class", Description = "Class")]
        public Guid? ClassId { get; set; }

        [DataMember]
        [Display(Name = "Section", Description = "Section")]
        public Guid? SectionId { get; set; }

        [DataMember]
        [Display(Name = "Name", Description = "Name")]
        public string Name { get; set; }

        [DataMember]
        public bool? Status { get; set; }

        [DataMember]
        [Display(Name = "Is Rollover?", Description = "Is Rollover?")]
        public bool IsRollover { get; set; }

        [DataMember]
        [Display(Name = "Char By", Description = "Char By")]
        public string CharBy { get; set; }

        [DataMember]
        [Display(Name = "Page Index", Description = "Page Index")]
        public int PageIndex { get; set; }

        [DataMember]
        [Display(Name = "Page Size", Description = "Page Size")]
        public int PageSize { get; set; }

        [DataMember]
        [Display(Name = "Request From", Description = "RequestFrom")]
        public string RequestFrom { get; set; }

        [JsonIgnore]
        public bool IgnoreCriteria { get; set; }
    }
}