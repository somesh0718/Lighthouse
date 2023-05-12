using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the VTClass entity
    /// </summary>
    public class VTClassRepository : GenericRepository<VTClass>, IVTClassRepository
    {
        /// <summary>
        /// Get list of VTClass
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTClass> GetVTClasses()
        {
            return this.Context.VTClasses.AsQueryable();
        }

        /// <summary>
        /// Get list of VTClass by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTClass> GetVTClassesByName(string name)
        {
            var vtClasses = (from v in this.Context.VTClasses
                             select v).AsQueryable();

            return vtClasses;
        }

        /// <summary>
        /// Get VTClass by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        public VTClass GetVTClassById(Guid vtClassId)
        {
            return this.Context.VTClasses.FirstOrDefault(v => v.VTClassId == vtClassId);
        }

        /// <summary>
        /// Get VTClass by VTClass
        /// </summary>
        /// <param name="vtClassModel"></param>
        /// <returns></returns>
        public VTClass GetVTClassByClass(VTClassModel vtClassModel)
        {
            VTClass vtClass = this.Context.VTClasses.FirstOrDefault(v => v.VTId == vtClassModel.VTId && v.AcademicYearId == vtClassModel.AcademicYearId && v.ClassId == vtClassModel.ClassId);

            return vtClass;
        }

        /// <summary>
        /// Get VTClass by VTClassId using async
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        public async Task<VTClass> GetVTClassByIdAsync(Guid vtClassId)
        {
            var vtClass = await (from v in this.Context.VTClasses
                                 where v.VTClassId == vtClassId
                                 select v).FirstOrDefaultAsync();

            return vtClass;
        }

        /// <summary>
        /// Insert/Update VTClass entity
        /// </summary>
        /// <param name="vtClass"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTClassDetails(VTClass vtClass, VTClassModel vtClassModel)
        {
            try
            {
                IList<Guid> sectionIds = vtClassModel.SectionIds;

                if (RequestType.New == vtClass.RequestType)
                {
                    this.Context.VTClasses.Add(vtClass);

                    // Stoped auto student class mapping with newly VT during Class Mapping
                    //VTClass inactiveVTClass = this.Context.VTClasses.Where(s => s.SchoolId == vtClass.SchoolId && s.ClassId == vtClass.ClassId && s.IsActive == false).OrderByDescending(x => x.UpdatedOn).FirstOrDefault();

                    //if (inactiveVTClass != null && !Guid.Equals(vtClass.VTId, inactiveVTClass.VTId))
                    //{
                    //    IList<StudentClass> students = this.Context.StudentClasses.Where(s => s.SchoolId == inactiveVTClass.SchoolId && s.VTId == inactiveVTClass.VTId && s.ClassId == inactiveVTClass.ClassId).ToList();

                    //    for (int studentIndex = 0; studentIndex < students.Count; studentIndex++)
                    //    {
                    //        students[studentIndex].VTId = vtClass.VTId;
                    //        Context.Entry<StudentClass>(students[studentIndex]).State = EntityState.Modified;

                    //        Context.StudentClassMapping.Add(new StudentClassMapping
                    //        {
                    //            StudentClassMappingId = Guid.NewGuid(),
                    //            AcademicYearId = students[studentIndex].AcademicYearId,
                    //            SchoolId = students[studentIndex].SchoolId,
                    //            ClassId = students[studentIndex].ClassId,
                    //            SectionId = students[studentIndex].SectionId,
                    //            VTId = vtClass.VTId,
                    //            StudentId = students[studentIndex].StudentId,
                    //            CreatedBy = vtClass.CreatedBy,
                    //            CreatedOn = vtClass.CreatedOn,
                    //            IsActive = true
                    //        });
                    //    }
                    //}
                }
                else
                {
                    if (!bool.Equals(vtClassModel.IsActive, vtClass.IsActive))
                    {
                        IList<StudentClass> students = this.Context.StudentClasses.Where(s => s.AcademicYearId == vtClass.AcademicYearId && s.SchoolId == vtClass.SchoolId && s.VTId == vtClass.VTId && s.ClassId == vtClass.ClassId && s.IsActive == true).ToList();

                        students.ForEach(studentItem =>
                        {
                            StudentClassMapping vtStudentClassMapping = this.Context.StudentClassMapping.FirstOrDefault(vs => vs.AcademicYearId == vtClass.AcademicYearId && vs.SchoolId == vtClass.SchoolId && vs.ClassId == vtClass.ClassId && vs.VTId == vtClass.VTId && vs.StudentId == studentItem.StudentId && vs.IsActive == true);
                            if (vtStudentClassMapping != null)
                            {
                                vtStudentClassMapping.IsActive = vtClass.IsActive;
                                vtStudentClassMapping.UpdatedBy = vtClass.UpdatedBy;
                                vtStudentClassMapping.UpdatedOn = Constants.GetCurrentDateTime;
                                Context.Entry<StudentClassMapping>(vtStudentClassMapping).State = EntityState.Modified;
                            }
                        });
                    }

                    Context.Entry<VTClass>(vtClass).State = EntityState.Modified;
                }
                Context.SaveChanges();

                if (sectionIds.Count > 0)
                {
                    IList<VTClassSection> classSections = Context.VTClassSections.Where(v => v.VTClassId == vtClass.VTClassId).ToList();

                    classSections.ForEach((sectionItem) =>
                    {
                        Context.Entry<VTClassSection>(sectionItem).State = EntityState.Deleted;
                    });

                    foreach (Guid sectionId in sectionIds)
                    {
                        Context.VTClassSections.Add(new VTClassSection
                        {
                            VTClassSectionId = Guid.NewGuid(),
                            VTClassId = vtClass.VTClassId,
                            SectionId = sectionId,
                            CreatedBy = vtClass.CreatedBy,
                            CreatedOn = vtClass.CreatedOn,
                            IsActive = vtClass.IsActive
                        });
                    }
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVTClassDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtClassId)
        {
            VTClass vtClass = this.Context.VTClasses.FirstOrDefault(v => v.VTClassId == vtClassId);

            if (vtClass != null)
            {
                IList<StudentClass> studentList = (from vcs in this.Context.StudentClassMapping
                                                   join sc in this.Context.StudentClasses on vcs.StudentId equals sc.StudentId
                                                   where sc.AcademicYearId == vtClass.AcademicYearId && sc.SchoolId == vtClass.SchoolId
                                                   && sc.ClassId == vtClass.ClassId && vcs.VTId == vtClass.VTId
                                                   select sc).ToList();

                if (studentList.Count > 0)
                {
                    vtClass.IsActive = false;
                    vtClass.UpdatedOn = Constants.GetCurrentDateTime;
                    Context.Entry<VTClass>(vtClass).State = EntityState.Modified;

                    var classSections = Context.VTClassSections.Where(c => c.VTClassId == vtClassId).ToList();
                    classSections.ForEach(sectionItem =>
                    {
                        sectionItem.IsActive = false;
                        Context.Entry<VTClassSection>(sectionItem).State = EntityState.Modified;

                        var students = studentList.Where(s => s.SectionId == sectionItem.SectionId).ToList();
                        students.ForEach(studentItem =>
                        {
                            StudentClassMapping vtStudentClassMapping = this.Context.StudentClassMapping.FirstOrDefault(vs => vs.AcademicYearId == vtClass.AcademicYearId && vs.SchoolId == vtClass.SchoolId && vs.ClassId == vtClass.ClassId && vs.VTId == vtClass.VTId && vs.StudentId == studentItem.StudentId);
                            if (vtStudentClassMapping != null)
                            {
                                vtStudentClassMapping.IsActive = false;
                                vtStudentClassMapping.UpdatedBy = vtClass.UpdatedBy;
                                vtStudentClassMapping.UpdatedOn = Constants.GetCurrentDateTime;
                                Context.Entry<StudentClassMapping>(vtStudentClassMapping).State = EntityState.Modified;
                            }
                        });
                    });
                }
                else
                {
                    Context.Entry<VTClass>(vtClass).State = EntityState.Deleted;

                    var classSections = Context.VTClassSections.Where(s => s.VTClassId == vtClassId).ToList();
                    classSections.ForEach(sectionItem =>
                    {
                        Context.Entry<VTClassSection>(sectionItem).State = EntityState.Deleted;
                    });
                }

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTClass by Name
        /// </summary>
        /// <param name="vtClass"></param>
        /// <param name="vtClassModel"></param>
        /// <returns></returns>
        public string CheckVTClassExistByName(VTClass vtClass, VTClassModel vtClassModel)
        {
            List<VTClass> classes = this.Context.VTClasses.Where(v => v.AcademicYearId == vtClassModel.AcademicYearId && v.SchoolId == vtClassModel.SchoolId && v.VTId == vtClassModel.VTId).ToList();

            if (vtClassModel.RequestType == RequestType.New)
            {
                var oldClass = classes.FirstOrDefault(c => c.AcademicYearId == vtClassModel.AcademicYearId && c.ClassId == vtClassModel.ClassId);
                if (oldClass != null)
                {
                    return string.Format("Current VT Class is already mapped to this School");
                }
            }
            else if (vtClassModel.RequestType == RequestType.Edit)
            {
                var currentClass = classes.FirstOrDefault(c => c.AcademicYearId == vtClassModel.AcademicYearId && c.ClassId == vtClassModel.ClassId);

                if (currentClass != null && !Guid.Equals(vtClass.ClassId, vtClassModel.ClassId))
                {
                    return string.Format("Current VT Class is already mapped to this School");
                }

                var existVTSchoolStudents = this.Context.StudentClassMapping.Where(v => v.AcademicYearId == vtClass.AcademicYearId && v.SchoolId == vtClass.SchoolId && v.VTId == vtClass.VTId).ToList();

                if (existVTSchoolStudents.Count > 0 && !Guid.Equals(vtClass.SchoolId, vtClassModel.SchoolId))
                {
                    return string.Format("School cannot be changed beacause Students are already mapped with current school.");
                }

                var existVTClassStudents = existVTSchoolStudents.Where(v => v.ClassId == vtClass.ClassId).ToList();

                if (existVTClassStudents.Count > 0 && Guid.Equals(vtClass.SchoolId, vtClassModel.SchoolId) && !Guid.Equals(vtClass.ClassId, vtClassModel.ClassId))
                {
                    return string.Format("Class cannot be changed beacause VT Student Classes are already mapped with current class.");
                }

                IList<Guid> classSections = this.Context.VTClassSections.Where(v => v.VTClassId == vtClassModel.VTClassId).Select(s => s.SectionId).ToList();
                foreach (var sectionId in classSections)
                {
                    if (!vtClassModel.SectionIds.Contains(sectionId))
                    {
                        var existClassSectionStudents = existVTSchoolStudents.Where(v => v.ClassId == vtClass.ClassId && v.SectionId == sectionId).Count();

                        if (existClassSectionStudents > 0)
                        {
                            Section sectionItem = this.Context.Sections.FirstOrDefault(s => s.SectionId == sectionId);
                            return string.Format("'{0}' cannot be removed beacuase {1} students are mapped with '{0}'", sectionItem.Name, existClassSectionStudents);
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Check VT Class can be inactive
        /// </summary>
        /// <param name="vtClass"></param>
        /// <returns></returns>
        public bool CheckUserCanInactiveVTClassById(VTClass vtClass)
        {
            StudentClass student = (from vcs in this.Context.StudentClassMapping
                                    join sc in this.Context.StudentClasses on vcs.StudentId equals sc.StudentId
                                    where sc.AcademicYearId == vtClass.AcademicYearId && sc.SchoolId == vtClass.SchoolId && sc.ClassId == vtClass.ClassId && vcs.VTId == vtClass.VTId && sc.IsActive == true
                                    select sc).FirstOrDefault();

            return (student == null);
        }

        /// <summary>
        /// Get SectionIds by VTClassId
        /// </summary>
        /// <param name="vtClassId"></param>
        /// <returns></returns>
        public IList<Guid> GetVTClassSectionsById(Guid vtClassId)
        {
            return this.Context.VTClassSections.Where(v => v.VTClassId == vtClassId).Select(s => s.SectionId).ToList();
        }

        /// <summary>}
        /// List of VTClass with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTClassViewModel> GetVTClassesByCriteria(SearchVTClassModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[15];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[5] = new MySqlParameter { ParameterName = "jobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[6] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[7] = new MySqlParameter { ParameterName = "classId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
            sqlParams[8] = new MySqlParameter { ParameterName = "hmId", MySqlDbType = MySqlDbType.Int32, Value = searchModel.HMId };
            sqlParams[9] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[10] = new MySqlParameter { ParameterName = "isRollover", MySqlDbType = MySqlDbType.Bool, Value = searchModel.IsRollover };
            sqlParams[11] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[12] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[13] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[14] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.VTClassViewModels.FromSql<VTClassViewModel>("CALL GetVTClassesByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @sectorId, @jobRoleId, @schoolId, @classId, @hmId, @status, @isRollover, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}