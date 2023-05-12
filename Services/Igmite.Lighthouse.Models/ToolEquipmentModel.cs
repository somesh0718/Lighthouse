using Igmite.Lighthouse.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class ToolEquipmentModel : ToolEquipment
    {
        public ToolEquipmentModel()
        {
        }

        [DataMember]
        public IList<string> RoomDamaged { get; set; }

        [DataMember]
        public IList<TEMaterialListModel> TEMaterialList { get; set; }

        [DataMember]
        public IList<TEToolListModel> TEToolList { get; set; }

        [DataMember]
        public IList<Guid> DeletedTEToolListIds { get; set; }

        [DataMember]
        public IList<Guid> DeletedTEMaterialListIds { get; set; }

        /// <summary>
        /// IFormFile or FileUploadModel
        /// </summary>
        [DataMember]
        public virtual FileUploadModel TLPhotoFile { get; set; }

        [DataMember]
        public virtual FileUploadModel LabPhotoFile { get; set; }
    }
}