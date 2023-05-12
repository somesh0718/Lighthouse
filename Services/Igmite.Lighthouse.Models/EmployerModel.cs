using Igmite.Lighthouse.Entities;
using System;
using System.Runtime.Serialization;

namespace Igmite.Lighthouse.Models
{
    [DataContract, Serializable]
    public class EmployerModel : Employer
    {
        public EmployerModel()
        {
        }
    }
}