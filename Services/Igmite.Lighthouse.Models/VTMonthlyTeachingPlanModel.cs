using Igmite.Lighthouse.Entities;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class VTMonthlyTeachingPlanModel : VTMonthlyTeachingPlan
    {
        public VTMonthlyTeachingPlanModel()
        {
        }

        public Guid VTId { get; set; }
    }
}