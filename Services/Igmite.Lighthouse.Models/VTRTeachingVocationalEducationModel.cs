using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTRTeachingVocationalEducationModel
    {
        public VTRTeachingVocationalEducationModel()
        {
            this.ActivityTypeIds = new List<string>();
            this.ReasonOfNotConductingTheClassIds = new List<string>();
            this.UnitSessionsModels = new List<UnitSessionsModel>();
            this.StudentAttendances = new List<StudentAttendanceModel>();
        }

        [DataMember]
        public virtual int SequenceNo { get; set; }

        [DataMember]
        public virtual bool DidYouTeachToday { get; set; }

        [DataMember]
        public virtual Guid ClassTaughtId { get; set; }

        [DataMember]
        public virtual Guid SectionTaughtId { get; set; }

        [DataMember]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClassTypeId { get; set; }

        [DataMember]
        [MaxLength(50, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ClassTime { get; set; }

        [DataMember]
        public virtual FileUploadModel ClassPictureFile { get; set; }

        [DataMember]
        public virtual FileUploadModel LessonPlanPictureFile { get; set; }

        [DataMember]
        [MaxLength(350, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string ReasonDetails { get; set; }

        [DataMember]
        public Guid ClassSectionIds { get; set; }

        [DataMember]
        public IList<string> ActivityTypeIds { get; set; }

        [DataMember]
        public IList<string> ReasonOfNotConductingTheClassIds { get; set; }

        [DataMember]
        public IList<UnitSessionsModel> UnitSessionsModels { get; set; }

        [DataMember]
        public IList<StudentAttendanceModel> StudentAttendances { get; set; }
    }
}