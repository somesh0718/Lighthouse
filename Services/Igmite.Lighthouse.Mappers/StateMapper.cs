using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class StateMapper
    {
        public static StateModel ToModel(this State state)
        {
            if (state == null)
                return null;

            StateModel stateModel = new StateModel
            {
                StateCode = state.StateCode,
                StateId = state.StateId,
                CountryCode = state.CountryCode,
                StateName = state.StateName,
                Description = state.Description,
                SequenceNo = state.SequenceNo,
                CreatedBy = state.CreatedBy,
                CreatedOn = state.CreatedOn,
                UpdatedBy = state.UpdatedBy,
                UpdatedOn = state.UpdatedOn,
                IsActive = state.IsActive
            };

            //state.Districts.ForEach((district) => stateModel.DistrictModels.Add(district.ToModel()));
            //state.Schools.ForEach((school) => stateModel.SchoolModels.Add(school.ToModel()));

            return stateModel;
        }
        public static State FromModel(this StateModel stateModel, State state)
        {
            state.StateCode = stateModel.StateCode;
            state.StateId = stateModel.StateId;
            state.CountryCode = stateModel.CountryCode;
            state.StateName = stateModel.StateName;
            state.Description = stateModel.Description;
            state.SequenceNo = stateModel.SequenceNo;
            state.IsActive = stateModel.IsActive;
            state.RequestType = stateModel.RequestType;
            state.SetAuditValues(stateModel.RequestType);

            //// Handling multiple state districts
            //foreach (var districtModel in stateModel.DistrictModels)
            //{
            //    District district = state.Districts.FirstOrDefault(f => f.DistrictCode == districtModel.DistrictCode);
            //    if (district == null || stateModel.RequestType == RequestType.New)
            //    {
            //        district = new District();
            //        district.StateCode = state.StateCode;
            //    }
            //    district = districtModel.FromModel(district);
            //    district.SetAuditValues(stateModel.RequestType);

            //    state.Districts.Add(district);
            //}

            //// Handling multiple state schools
            //foreach (var schoolModel in stateModel.SchoolModels)
            //{
            //    School school = state.Schools.FirstOrDefault(f => f.SchoolId == schoolModel.SchoolId);
            //    if (school == null || stateModel.RequestType == RequestType.New)
            //    {
            //        school = new School();
            //        school.SchoolId = Guid.NewGuid();
            //        school.StateCode = state.StateCode;
            //    }
            //    school = schoolModel.FromModel(school);
            //    school.SetAuditValues(stateModel.RequestType);

            //    state.Schools.Add(school);
            //}

            return state;
        }
    }
}
