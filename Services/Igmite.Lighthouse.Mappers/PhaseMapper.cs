using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class PhaseMapper
    {
        public static PhaseModel ToModel(this Phase phase)
        {
            if (phase == null)
                return null;

            PhaseModel phaseModel = new PhaseModel
            {
                PhaseId = phase.PhaseId,
                PhaseName = phase.PhaseName,
                Description = phase.Description,
                CreatedBy = phase.CreatedBy,
                CreatedOn = phase.CreatedOn,
                UpdatedBy = phase.UpdatedBy,
                UpdatedOn = phase.UpdatedOn,
                IsActive = phase.IsActive
            };

            //phase.Schools.ForEach((school) => phaseModel.SchoolModels.Add(school.ToModel()));
            //phase.SectorJobRoles.ForEach((sectorJobRole) => phaseModel.SectorJobRoleModels.Add(sectorJobRole.ToModel()));

            return phaseModel;
        }
        public static Phase FromModel(this PhaseModel phaseModel, Phase phase)
        {
            phase.PhaseId = phaseModel.PhaseId;
            phase.PhaseName = phaseModel.PhaseName;
            phase.Description = phaseModel.Description;
            phase.IsActive = phaseModel.IsActive;
            phase.RequestType = phaseModel.RequestType;
            phase.SetAuditValues(phaseModel.RequestType);

            //// Handling multiple phase schools
            //foreach (var schoolModel in phaseModel.SchoolModels)
            //{
            //    School school = phase.Schools.FirstOrDefault(f => f.SchoolId == schoolModel.SchoolId);
            //    if (school == null || phaseModel.RequestType == RequestType.New)
            //    {
            //        school = new School();
            //        school.SchoolId = Guid.NewGuid();
            //        school.PhaseId = phase.PhaseId;
            //    }
            //    school = schoolModel.FromModel(school);
            //    school.SetAuditValues(phaseModel.RequestType);

            //    phase.Schools.Add(school);
            //}

            //// Handling multiple phase roles
            //foreach (var roleModel in phaseModel.SectorJobRoleModels)
            //{
            //    SectorJobRole role = phase.SectorJobRoles.FirstOrDefault(f => f.SectorJobRoleId == roleModel.SectorJobRoleId);
            //    if (role == null || phaseModel.RequestType == RequestType.New)
            //    {
            //        role = new SectorJobRole();
            //        role.SectorJobRoleId = Guid.NewGuid();
            //        role.PhaseId = phase.PhaseId;
            //    }
            //    role = roleModel.FromModel(role);
            //    role.SetAuditValues(phaseModel.RequestType);

            //    phase.SectorJobRoles.Add(role);
            //}

            return phase;
        }
    }
}
