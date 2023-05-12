using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class StudentsForExitSurveyViewModel
    {
        public StudentsForExitSurveyViewModel()
        {
        }

        [Key, DataMember]
        public virtual Guid ExitStudentId { get; set; }

        [DataMember]
        public virtual string AcademicYear { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual string Sector { get; set; }

        [DataMember]
        public virtual string JobRole { get; set; }

        [DataMember]
        public virtual string NameOfSchool { get; set; }

        [DataMember]
        public virtual string UdiseCode { get; set; }

        [DataMember]
        public virtual string District { get; set; }

        [DataMember]
        public virtual string Class { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string MiddleName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string StudentFullName { get; set; }

        [DataMember]
        public virtual string FatherName { get; set; }

        [DataMember]
        public virtual string MotherName { get; set; }

        [DataMember]
        public virtual string StudentUniqueId { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual DateTime? DOB { get; set; }

        [DataMember]
        public virtual string Category { get; set; }

        [DataMember]
        public virtual string AssessmentConducted { get; set; }

        [DataMember]
        public virtual int IsExitSurveyFilled { get; set; }
    }
}