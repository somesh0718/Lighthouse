using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTDailyMonthlyModel
    {
        public VTDailyMonthlyModel()
        {
        }

        // Primary key
        [Key, DataMember]
        public virtual Int64 SrNo { get; set; }

        [DataMember]
        public virtual string VTName { get; set; }

        [DataMember]
        public virtual string VTEmailId { get; set; }

        [DataMember]
        public virtual string VTMobile { get; set; }

        [DataMember]
        public virtual DateTime VTDateOfJoining { get; set; }

        [DataMember]
        public virtual DateTime? VTDateOfResignation { get; set; }

        [DataMember]
        public virtual string VTStatus { get; set; }

        [DataMember]
        public virtual string VCName { get; set; }

        [DataMember]
        public virtual string VCEmailId { get; set; }

        [DataMember]
        public virtual string VCMobile { get; set; }

        [DataMember]
        public virtual string VTPName { get; set; }

        [DataMember]
        public virtual string SectorName { get; set; }

        [DataMember]
        public virtual string JobRoleName { get; set; }

        [DataMember]
        public virtual string ReportMonth { get; set; }

        [DataMember]
        public virtual string SchoolName { get; set; }

        [DataMember]
        public virtual string UDISE { get; set; }

        [DataMember]
        public virtual string DivisionName { get; set; }

        [DataMember]
        public virtual string DistrictName { get; set; }

        [DataMember]
        public virtual string Day1 { get; set; }

        [DataMember]
        public virtual string Day2 { get; set; }

        [DataMember]
        public virtual string Day3 { get; set; }

        [DataMember]
        public virtual string Day4 { get; set; }

        [DataMember]
        public virtual string Day5 { get; set; }

        [DataMember]
        public virtual string Day6 { get; set; }

        [DataMember]
        public virtual string Day7 { get; set; }

        [DataMember]
        public virtual string Day8 { get; set; }

        [DataMember]
        public virtual string Day9 { get; set; }

        [DataMember]
        public virtual string Day10 { get; set; }

        [DataMember]
        public virtual string Day11 { get; set; }

        [DataMember]
        public virtual string Day12 { get; set; }

        [DataMember]
        public virtual string Day13 { get; set; }

        [DataMember]
        public virtual string Day14 { get; set; }

        [DataMember]
        public virtual string Day15 { get; set; }

        [DataMember]
        public virtual string Day16 { get; set; }

        [DataMember]
        public virtual string Day17 { get; set; }

        [DataMember]
        public virtual string Day18 { get; set; }

        [DataMember]
        public virtual string Day19 { get; set; }

        [DataMember]
        public virtual string Day20 { get; set; }

        [DataMember]
        public virtual string Day21 { get; set; }

        [DataMember]
        public virtual string Day22 { get; set; }

        [DataMember]
        public virtual string Day23 { get; set; }

        [DataMember]
        public virtual string Day24 { get; set; }

        [DataMember]
        public virtual string Day25 { get; set; }

        [DataMember]
        public virtual string Day26 { get; set; }

        [DataMember]
        public virtual string Day27 { get; set; }

        [DataMember]
        public virtual string Day28 { get; set; }

        [DataMember]
        public virtual string Day29 { get; set; }

        [DataMember]
        public virtual string Day30 { get; set; }

        [DataMember]
        public virtual string Day31 { get; set; }

        [DataMember]
        public virtual int WorkingDays { get; set; }

        [DataMember]
        public virtual int Sundays { get; set; }

        [DataMember]
        public virtual int Holidays { get; set; }

        [DataMember]
        public virtual int Leaves { get; set; }

        [DataMember]
        public virtual int DaysInMonth { get; set; }
    }
}