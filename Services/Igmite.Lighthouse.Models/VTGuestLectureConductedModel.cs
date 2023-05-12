using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTGuestLectureConductedModel : VTGuestLectureConducted
    {
        public VTGuestLectureConductedModel()
        {
        }


        [DataMember]
        public Guid SectionIds { get; set; }

        [DataMember]
        public IList<string> MethodologyIds { get; set; }

        [DataMember]
        public IList<UnitSessionsModel> UnitSessionsModels { get; set; }

        [DataMember]
        public IList<StudentAttendanceModel> StudentAttendances { get; set; }

        /// <summary>
        /// IFormFile or FileUploadModel
        /// </summary>
        [DataMember]
        public virtual FileUploadModel GLPhotoFile { get; set; }

        [DataMember]
        public virtual FileUploadModel GLLecturerPhotoFile { get; set; }
    }
}