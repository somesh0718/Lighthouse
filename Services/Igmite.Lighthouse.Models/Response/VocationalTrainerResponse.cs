using System;

namespace Igmite.Lighthouse.Models
{
    public class VocationalTrainerRequest : VocationalTrainerModel
    {
    }

    public class VocationalTrainerResponse : VocationalTrainerModel
    {
    }

    public class SearchVocationalTrainerRequest : BaseSearchModel
    {
    }

    public class VTTransferRequest
    {
        public Guid AcademicYearId { get; set; }

        public Guid FromVTPId { get; set; }

        public Guid FromVCId { get; set; }

        public Guid FromVTId { get; set; }

        public Guid FromSchoolId { get; set; }

        public Guid AcademicYearToId { get; set; }

        public Guid ToVTPId { get; set; }

        public Guid ToVCId { get; set; }

        public Guid ToVTId { get; set; }

        public Guid ToSchoolId { get; set; }

        public byte IsVTResigned { get; set; }

        public DateTime DateOfAllocation { get; set; }

        public DateTime? DateOfResignation { get; set; }

        public DateTime? DateOfRemoval { get; set; }

        public DateTime? ToDateOfRemoval { get; set; }

        public string UserId { get; set; }

        public string Remarks { get; set; }

    }
}