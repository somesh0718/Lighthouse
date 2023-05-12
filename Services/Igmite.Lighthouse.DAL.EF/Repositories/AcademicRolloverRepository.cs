using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igmite.Lighthouse.DAL.EF
{
    public class AcademicRolloverRepository : GenericRepository<AcademicRollOverResponse>, IAcademicRolloverRepository
    {
        public string GetNextAcademicYear()
        {
            var result = Context.AcademicYears.FromSql<AcademicYear>("CALL GetNextYearForAcademicRollover ()").FirstOrDefault();

            if (result != null)
                return result.YearName;
            else
                return null;
        }

        public SingularResponse<bool> CheckIfVTClassExists(VTClassRequest vtClassRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "SelectedVTId", MySqlDbType = MySqlDbType.VarChar, Value = vtClassRequest.VTId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "SelectedClassId", MySqlDbType = MySqlDbType.VarChar, Value = vtClassRequest.ClassId.StringVal() };

            try
            {
                var result = Context.VTClasses.FromSql<VTClass>("Call CheckIfVTClassExists(@SelectedVTId, @SelectedClassId)", sqlParams);
                if (result != null && result.Count() > 0)
                {
                    response.Result = true;
                    response.Messages.Add("Next class is available with given VT");
                    response.Errors.Add("Next class is available with given VT");
                    response.Success = true;
                }
                else
                {
                    response.Result = false;
                    response.Messages.Add("Next class is not available with given VT, Either register next class with given VT OR Transfer students to correct VT");
                    response.Errors.Add("Next class is not available with given VT, Either register next class with given VT OR Transfer students to correct VT");
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Errors.Add(ex.Message);
                response.Success = false;
            }
            return response;
        }

        public bool VTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = academicRollOverRequest.UserId.StringVal() };

            List<string> vtpSectors = academicRollOverRequest.FromEntityId.Split(",").ToList();

            try
            {
                if (vtpSectors != null && vtpSectors.Count > 0)
                {
                    foreach (string vtpSectorId in vtpSectors)
                    {
                        sqlParams[1] = new MySqlParameter { ParameterName = "VTPSectorId", MySqlDbType = MySqlDbType.VarChar, Value = vtpSectorId };

                        Context.Database.ExecuteSqlCommand("Call VTPSectorTransfer (@UserId, @VTPSectorId)", sqlParams);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SchoolVTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = academicRollOverRequest.UserId.StringVal() };

            List<string> schoolVTPSectors = academicRollOverRequest.FromEntityId.Split(",").ToList();
            try
            {
                if (schoolVTPSectors != null && schoolVTPSectors.Count > 0)
                {
                    foreach (string schoolVTPSectorId in schoolVTPSectors)
                    {
                        sqlParams[1] = new MySqlParameter { ParameterName = "SchoolVTPSectorId", MySqlDbType = MySqlDbType.VarChar, Value = schoolVTPSectorId };

                        Context.Database.ExecuteSqlCommand("Call SchoolVTPSectorTransfer (@UserId, @SchoolVTPSectorId)", sqlParams);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool VCSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = academicRollOverRequest.UserId.StringVal() };

            List<string> vcSchoolSectors = academicRollOverRequest.FromEntityId.Split(",").ToList();
            try
            {
                if (vcSchoolSectors != null && vcSchoolSectors.Count > 0)
                {
                    foreach (string vcSchoolSectorId in vcSchoolSectors)
                    {
                        sqlParams[1] = new MySqlParameter { ParameterName = "VCSchoolSectorId", MySqlDbType = MySqlDbType.VarChar, Value = vcSchoolSectorId };

                        Context.Database.ExecuteSqlCommand("Call VCSchoolSectorTransfer (@UserId, @VCSchoolSectorId)", sqlParams);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool VTSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = academicRollOverRequest.UserId.StringVal() };

            List<string> vtSchoolSectors = academicRollOverRequest.FromEntityId.Split(",").ToList();
            try
            {
                if (vtSchoolSectors != null && vtSchoolSectors.Count > 0)
                {
                    foreach (string vtSchoolSectorId in vtSchoolSectors)
                    {
                        sqlParams[1] = new MySqlParameter { ParameterName = "VTSchoolSectorId", MySqlDbType = MySqlDbType.VarChar, Value = vtSchoolSectorId };

                        Context.Database.ExecuteSqlCommand("Call VTSchoolSectorTransfer (@UserId, @VTSchoolSectorId)", sqlParams);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool VTClassesTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = academicRollOverRequest.UserId.StringVal() };

            List<string> vtClasses = academicRollOverRequest.FromEntityId.Split(",").ToList();
            try
            {
                if (vtClasses != null && vtClasses.Count > 0)
                {
                    foreach (string vtClassId in vtClasses)
                    {
                        sqlParams[1] = new MySqlParameter { ParameterName = "VTClassId", MySqlDbType = MySqlDbType.VarChar, Value = vtClassId };

                        Context.Database.ExecuteSqlCommand("Call VTClassesTransfer(@UserId, @VTClassId)", sqlParams);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool VTPTransfer(AcademicRollOverRequest transferRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = transferRequest.UserId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = transferRequest.AcademicYearId };
            sqlParams[2] = new MySqlParameter { ParameterName = "OldVTPId", MySqlDbType = MySqlDbType.Guid, Value = transferRequest.FromEntityId };
            sqlParams[3] = new MySqlParameter { ParameterName = "NewVTPId", MySqlDbType = MySqlDbType.Guid, Value = transferRequest.ToEntityId };

            try
            {
                LighthouseParams lighthouseParams = new LighthouseParams
                {
                    Param1 = transferRequest.VTPSectorIds,
                    Param2 = transferRequest.SchoolVTPSectorIds,
                    Param3 = transferRequest.VCSchoolSectorIds,
                    CreatedBy = transferRequest.UserId,
                    CreatedOn = Constants.GetCurrentDateTime
                };
                Context.LighthouseParams.Add(lighthouseParams);
                Context.SaveChanges();

                sqlParams[4] = new MySqlParameter { ParameterName = "ParamsId", MySqlDbType = MySqlDbType.Guid, Value = lighthouseParams.LighthouseParamId };

                Context.Database.ExecuteSqlCommand("Call VTPTransfer(@UserId, @AcademicYearId, @OldVTPId, @NewVTPId, @ParamsId)", sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool VTTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = academicRollOverRequest.UserId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "OldVTId", MySqlDbType = MySqlDbType.Guid, Value = academicRollOverRequest.FromEntityId };
            sqlParams[2] = new MySqlParameter { ParameterName = "NewVTId", MySqlDbType = MySqlDbType.Guid, Value = academicRollOverRequest.ToEntityId };
            sqlParams[3] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicRollOverRequest.AcademicYearId };

            try
            {
                Context.Database.ExecuteSqlCommand("Call VocationalTrainersTransfer(@UserId, @OldVTId, @NewVTId, @AcademicYearId)", sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool VCTransfer(AcademicRollOverRequest transferRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = transferRequest.UserId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = transferRequest.AcademicYearId };
            sqlParams[2] = new MySqlParameter { ParameterName = "OldVCId", MySqlDbType = MySqlDbType.Guid, Value = transferRequest.FromEntityId };
            sqlParams[3] = new MySqlParameter { ParameterName = "NewVCId", MySqlDbType = MySqlDbType.Guid, Value = transferRequest.ToEntityId };

            try
            {
                LighthouseParams lighthouseParams = new LighthouseParams
                {
                    Param1 = transferRequest.VCSchoolSectorIds,
                    Param2 = transferRequest.VTIds,
                    CreatedBy = transferRequest.UserId,
                    CreatedOn = Constants.GetCurrentDateTime
                };
                Context.LighthouseParams.Add(lighthouseParams);
                Context.SaveChanges();

                sqlParams[4] = new MySqlParameter { ParameterName = "ParamsId", MySqlDbType = MySqlDbType.Guid, Value = lighthouseParams.LighthouseParamId };

                Context.Database.ExecuteSqlCommand("Call VocationalCoordinatorTransfer(@UserId, @AcademicYearId, @OldVCId, @NewVCId, @ParamsId)", sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool StudentsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];

            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = academicRollOverRequest.UserId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "NewVTId", MySqlDbType = MySqlDbType.Guid, Value = academicRollOverRequest.ToEntityId };

            IList<string> students = academicRollOverRequest.FromEntityId.Split(",").ToList();

            try
            {
                if (students != null && students.Count > 0)
                {
                    foreach (string studentId in students)
                    {
                        sqlParams[2] = new MySqlParameter { ParameterName = "StudentId", MySqlDbType = MySqlDbType.Guid, Value = studentId };

                        Context.Database.ExecuteSqlCommand("Call StudentsTransfer (@UserId, @NewVTId, @StudentId)", sqlParams);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}