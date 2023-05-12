using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class RoleMapper
    {
        public static RoleModel ToModel(this Role role)
        {
            if (role == null)
                return null;

            RoleModel roleModel = new RoleModel
            {
                RoleId = role.RoleId,
                Code = role.Code,
                Name = role.Name,
                Description = role.Description,
                LandingPageUrl = role.LandingPageUrl,
                Remarks = role.Remarks,
                CreatedBy = role.CreatedBy,
                CreatedOn = role.CreatedOn,
                UpdatedBy = role.UpdatedBy,
                UpdatedOn = role.UpdatedOn,
                IsActive = role.IsActive
            };

            return roleModel;
        }

        public static Role FromModel(this RoleModel roleModel, Role role)
        {
            role.RoleId = roleModel.RoleId;
            role.Code = roleModel.Code;
            role.Name = roleModel.Name;
            role.Description = roleModel.Description;
            role.LandingPageUrl = roleModel.LandingPageUrl;
            role.Remarks = roleModel.Remarks;
            role.IsActive = roleModel.IsActive;
            role.RequestType = roleModel.RequestType;
            role.SetAuditValues(roleModel.RequestType);

            return role;
        }
    }
}