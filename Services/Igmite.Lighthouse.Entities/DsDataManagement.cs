using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Entities
{
    [DataContract, Serializable]
    [Table("DsDataManagement")]
    public partial class DsDataManagement
    {
        public DsDataManagement()
        {
        }

        #region Public Properties

        // Primary key
        [Key, DataMember]
        [Column("AcademicYearId", Order = 1)]
        [Display(Name = "Academic Year", Description = "Academic Year", Order = 1)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual Guid AcademicYearId { get; set; }

        // Primary key
        [Key, DataMember]
        [Column("ReportDate", TypeName = "DateTime", Order = 2)]
        [Display(Name = "Report Date", Description = "Report Date", Order = 2)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        public virtual DateTime ReportDate { get; set; }

        [DataMember]
        [Column("DataType", Order = 3)]
        [Display(Name = "Data Type", Description = "Data Type", Order = 3)]
        [Required(ErrorMessage = EntityConstants.RequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = EntityConstants.MaxLengthErrorMessage)]
        public virtual string DataType { get; set; }

        [DataMember]
        [Column("RowCount", TypeName = "INT", Order = 4)]
        [Display(Name = "Row Count", Description = "Row Count", Order = 4)]
        public virtual int RowCount { get; set; }

        [DataMember]
        [Column("CreatedOn", TypeName = "DateTime", Order = 5)]
        [Display(Name = "Row Count", Description = "Row Count", Order = 5)]
        public virtual DateTime CreatedOn { get; set; }

        #endregion Public Properties
    }
}