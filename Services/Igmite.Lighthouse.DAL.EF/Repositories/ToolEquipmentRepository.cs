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
    /// Repository of the ToolEquipment entity
    /// </summary>
    public class ToolEquipmentRepository : GenericRepository<ToolEquipment>, IToolEquipmentRepository
    {
        /// <summary>
        /// Get list of ToolEquipment
        /// </summary>
        /// <returns></returns>
        public IQueryable<ToolEquipment> GetToolEquipments()
        {
            return this.Context.ToolEquipments.AsQueryable();
        }

        /// <summary>
        /// Get list of ToolEquipment by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ToolEquipment> GetToolEquipmentsByName(string name)
        {
            var toolEquipments = (from t in this.Context.ToolEquipments
                                  select t).AsQueryable();

            return toolEquipments;
        }

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        public ToolEquipment GetToolEquipmentById(Guid toolEquipmentId)
        {
            return this.Context.ToolEquipments.FirstOrDefault(t => t.ToolEquipmentId == toolEquipmentId);
        }

        /// <summary>
        /// Get ToolEquipment by ToolEquipmentId using async
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        public async Task<ToolEquipment> GetToolEquipmentByIdAsync(Guid toolEquipmentId)
        {
            var toolEquipment = await (from t in this.Context.ToolEquipments
                                       where t.ToolEquipmentId == toolEquipmentId
                                       select t).FirstOrDefaultAsync();

            return toolEquipment;
        }

        /// <summary>
        /// Get ToolList by ToolEquipmentId
        /// </summary>
        /// <param name="ToolEquipmentId"></param>
        /// <returns></returns>
        public IList<TEToolListModel> GetToolListById(Guid toolEquipmentId)
        {
            var tools=this.Context.TEToolLists.Where(v => v.ToolEquipmentId == toolEquipmentId).AsEnumerable()
                .Select(i => new TEToolListModel
                {
                    TEToolListId = i.TEToolListId,
                    ToolEquipmentId = i.ToolEquipmentId,
                    ToolListId = i.ToolListId,
                    ToolListName = i.ToolListName,
                    ToolListStatus = i.ToolListStatus,
                    TLActionNeeded1 = i.TLActionNeeded1,
                    TLActionNeeded2 = i.TLActionNeeded2,
                    RequestType = RequestType.Edit
                }).ToList();

            return tools;
        }

        /// <summary>
        /// Get Rawmaterial by ToolEquipmentId
        /// </summary>
        /// <param name="ToolEquipmentId"></param>
        /// <returns></returns>
        public IList<TEMaterialListModel> GetMaterialListById(Guid toolEquipmentId)
        {
            var rawMaterial = this.Context.TEMaterialLists.Where(v => v.ToolEquipmentId == toolEquipmentId).AsEnumerable()
                .Select(i => new TEMaterialListModel
                {
                    TEMaterialListId=i.TEMaterialListId,
                    ToolEquipmentId=i.ToolEquipmentId,
                    RawMaterialId = i.RawMaterialId,
                    RawMaterialName = i .RawMaterialName,
                    RawMaterialStatus = i.RawMaterialStatus,
                    RMLastReceivedDate =i.RMLastReceivedDate,
                    RawMaterialAction =i.RawMaterialAction,
                    QuantityCount= i.QuantityCount,
                    RequestType = RequestType.Edit
                }).ToList();

            return rawMaterial;
        }

        /// <summary>
        /// Get RoomDamaged by ToolEquipmentId
        /// </summary>
        /// <param name="ToolEquipmentId"></param>
        /// <returns></returns>
        public IList<string> GetRoomDamagedById(Guid toolEquipmentId)
        {
            return this.Context.ToolEquipmentsRoomDamaged.Where(v => v.ToolEquipmentId == toolEquipmentId).Select(s => s.RoomDamaged).ToList();
        }

        /// <summary>
        /// Insert/Update ToolEquipment entity
        /// </summary>
        /// <param name="toolEquipment"></param>
        /// <returns></returns>
        public bool SaveOrUpdateToolEquipmentDetails(ToolEquipment toolEquipment, ToolEquipmentModel toolEquipmentModel)
        {
            try
            {
                IList<string> roomdamaged = toolEquipmentModel.RoomDamaged;
                if (RequestType.New == toolEquipment.RequestType)
                {
                    Context.ToolEquipments.Add(toolEquipment);
                    if (toolEquipmentModel.RoomDamaged != null)
                    {
                        if (roomdamaged.Count > 0)
                        {
                            foreach (string roomdamage in roomdamaged)
                            {
                                Context.Add(new ToolEquipmentRoomDamaged
                                {
                                    ToolEquipmentRDId = Guid.NewGuid(),
                                    ToolEquipmentId = toolEquipment.ToolEquipmentId,
                                    RoomDamaged = roomdamage,
                                    CreatedBy = toolEquipment.CreatedBy,
                                    CreatedOn = toolEquipment.CreatedOn,
                                    IsActive = true
                                });
                            }
                        }
                    }
                    // Add tools from Models
                    if (toolEquipmentModel.TEToolList != null)
                    {
                        toolEquipmentModel.TEToolList.ForEach(tEToolList =>
                        {

                            Context.Add(new TEToolList
                            {
                                TEToolListId = Guid.NewGuid(),
                                ToolEquipmentId = toolEquipment.ToolEquipmentId,
                                ToolListId = tEToolList.ToolListId,
                                ToolListName = tEToolList.ToolListName,
                                ToolListStatus = tEToolList.ToolListStatus,
                                TLActionNeeded1 = tEToolList.TLActionNeeded1,
                                TLActionNeeded2 = tEToolList.TLActionNeeded2,
                                CreatedBy = toolEquipment.CreatedBy,
                                CreatedOn = toolEquipment.CreatedOn,
                                IsActive = true
                             });
                            
                        });
                    }

                    // Add material from Models
                    if (toolEquipmentModel.TEMaterialList != null)
                    {
                        toolEquipmentModel.TEMaterialList.ForEach(tEMaterialList =>
                        {
                            Context.Add(new TEMaterialList
                            {
                                TEMaterialListId = Guid.NewGuid(),
                                ToolEquipmentId = toolEquipment.ToolEquipmentId,
                                RawMaterialId = tEMaterialList.RawMaterialId,
                                RawMaterialName= tEMaterialList.RawMaterialName,
                                RawMaterialStatus = tEMaterialList.RawMaterialStatus,
                                RMLastReceivedDate = tEMaterialList.RMLastReceivedDate,
                                RawMaterialAction = tEMaterialList.RawMaterialAction,
                                QuantityCount = tEMaterialList.QuantityCount,
                                CreatedBy = toolEquipment.CreatedBy,
                                CreatedOn = toolEquipment.CreatedOn,
                                IsActive = true
                            });
                           
                        });
                    }
                }
                else
                {
                    // Get list of TEToolList from Database
                    IList<TEToolList> TEToolLists = this.Context.TEToolLists.Where(t => t.ToolEquipmentId == toolEquipmentModel.ToolEquipmentId).ToList();

                    // Add/Update TEToolList from Models
                    if (toolEquipmentModel.TEToolList != null)
                    {
                        toolEquipmentModel.TEToolList.ForEach(teToollist =>
                        {
                            if (teToollist.RequestType == RequestType.Edit)
                            {

                                // Update new TEToolList
                                var tEToolList = this.Context.TEToolLists.Where(t => t.TEToolListId == teToollist.TEToolListId).FirstOrDefault();
                                tEToolList.TEToolListId = teToollist.TEToolListId;
                                tEToolList.ToolListName = teToollist.ToolListName;
                                tEToolList.TLActionNeeded1 = teToollist.TLActionNeeded1;
                                tEToolList.TLActionNeeded2 = teToollist.TLActionNeeded2;
                                tEToolList.ToolEquipmentId = teToollist.ToolEquipmentId;
                                tEToolList.ToolListStatus = teToollist.ToolListStatus;
                                tEToolList.ToolListId = teToollist.ToolListId;
                                Context.Entry<TEToolList>(tEToolList).State = EntityState.Modified;
                            }
                            else
                            {
                                // Add new TEToolList
                                Context.Add(new TEToolList
                                {
                                    TEToolListId = Guid.NewGuid(),
                                    ToolEquipmentId = toolEquipment.ToolEquipmentId,
                                    ToolListId = teToollist.ToolListId,
                                    ToolListName=teToollist.ToolListName,
                                    ToolListStatus = teToollist.ToolListStatus,
                                    TLActionNeeded1 = teToollist.TLActionNeeded1,
                                    TLActionNeeded2 = teToollist.TLActionNeeded2,
                                    CreatedBy = toolEquipment.CreatedBy,
                                    CreatedOn = toolEquipment.CreatedOn,
                                    IsActive = true
                                });
                               
                            }
                        });
                    }

                    // Add/Update TEMaterialList from Models
                    if (toolEquipmentModel.TEMaterialList != null)
                    {
                        toolEquipmentModel.TEMaterialList.ForEach(teMaterialList =>
                        {
                            if (teMaterialList.RequestType == RequestType.Edit)
                            {
                                // Update new TEMaterialList
                                var tEMaterialList = this.Context.TEMaterialLists.Where(t => t.TEMaterialListId == teMaterialList.TEMaterialListId).FirstOrDefault();
                                tEMaterialList.TEMaterialListId = teMaterialList.TEMaterialListId;
                                tEMaterialList.ToolEquipmentId = teMaterialList.ToolEquipmentId;
                                tEMaterialList.RawMaterialId = teMaterialList.RawMaterialId;
                                tEMaterialList.RawMaterialName = teMaterialList.RawMaterialName;
                                tEMaterialList.RawMaterialStatus = teMaterialList.RawMaterialStatus;
                                tEMaterialList.RMLastReceivedDate = teMaterialList.RMLastReceivedDate;
                                tEMaterialList.RawMaterialAction = teMaterialList.RawMaterialAction;
                                tEMaterialList.QuantityCount = teMaterialList.QuantityCount;
                                Context.Entry<TEMaterialList>(tEMaterialList).State = EntityState.Modified;
                            }
                            else
                            {
                                // Add new TEMaterialList
                                Context.Add(new TEMaterialList
                                {
                                    TEMaterialListId = Guid.NewGuid(),
                                    ToolEquipmentId = toolEquipment.ToolEquipmentId,
                                    RawMaterialId = teMaterialList.RawMaterialId,
                                    RawMaterialName = teMaterialList.RawMaterialName,
                                    RawMaterialStatus = teMaterialList.RawMaterialStatus,
                                    RMLastReceivedDate = teMaterialList.RMLastReceivedDate,
                                    RawMaterialAction = teMaterialList.RawMaterialAction,
                                    QuantityCount = teMaterialList.QuantityCount,
                                    CreatedBy = toolEquipment.CreatedBy,
                                    CreatedOn = toolEquipment.CreatedOn,
                                    IsActive = true
                                });
                               
                            }
                        });

                    }

                    // Delete TEToolList from Database if any TEToolList removed from Models
                    if (toolEquipmentModel.DeletedTEToolListIds != null)
                    {
                        toolEquipmentModel.DeletedTEToolListIds.ForEach(toolListId =>
                        {
                            var toolListModel = toolEquipmentModel.TEToolList.FirstOrDefault(tet => tet.TEToolListId == toolListId);

                            if (toolListModel != null)
                                Context.Entry<TEToolList>(toolListModel).State = EntityState.Deleted;
                        });
                    }


                    // Delete TEMaterialList from Database if any TEMaterialList removed from Models
                    if (toolEquipmentModel.DeletedTEMaterialListIds != null)
                    {
                        toolEquipmentModel.DeletedTEMaterialListIds.ForEach(materialListId =>
                        {
                            var materialListModel = toolEquipmentModel.TEMaterialList.FirstOrDefault(mat => mat.TEMaterialListId == materialListId);

                            if (materialListModel != null)
                                Context.Entry<TEMaterialList>(materialListModel).State = EntityState.Deleted;
                        });
                    }

                    List<ToolEquipmentRoomDamaged> RoomDamaged = this.Context.ToolEquipmentsRoomDamaged.Where(v => v.ToolEquipmentId == toolEquipment.ToolEquipmentId).ToList();
                    if (RoomDamaged != null){
                        foreach (ToolEquipmentRoomDamaged roomdamage in RoomDamaged)
                        {
                            Context.Entry<ToolEquipmentRoomDamaged>(roomdamage).State = EntityState.Deleted;
                            Context.SaveChanges();
                        }
                    }

                    if (toolEquipmentModel.RoomDamaged != null)
                    {
                        if (roomdamaged.Count > 0)
                        {
                            foreach (string roomdamage in roomdamaged)
                            {
                                Context.Add(new ToolEquipmentRoomDamaged
                                {
                                    ToolEquipmentRDId = Guid.NewGuid(),
                                    ToolEquipmentId = toolEquipment.ToolEquipmentId,
                                    RoomDamaged = roomdamage,
                                    CreatedBy = toolEquipment.CreatedBy,
                                    CreatedOn = toolEquipment.CreatedOn,
                                    IsActive = true
                                });
                            }
                        }
                    }

                    Context.Entry<ToolEquipment>(toolEquipment).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateToolEquipmentDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by ToolEquipmentId
        /// </summary>
        /// <param name="toolEquipmentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid toolEquipmentId)
        {
            ToolEquipment toolEquipment = this.Context.ToolEquipments.FirstOrDefault(t => t.ToolEquipmentId == toolEquipmentId);

            if (toolEquipment != null)
            {
                Context.Entry<ToolEquipment>(toolEquipment).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate ToolEquipment by Name
        /// </summary>
        /// <param name="toolEquipmentModel"></param>
        /// <returns></returns>
        public bool CheckToolEquipmentExistByName(ToolEquipmentModel toolEquipmentModel)
        {
            ToolEquipment toolEquipment = this.Context.ToolEquipments.FirstOrDefault(t => t.AcademicYearId == toolEquipmentModel.AcademicYearId && t.SectorId == toolEquipmentModel.SectorId && t.JobRoleId == toolEquipmentModel.JobRoleId && t.VTId == toolEquipmentModel.VTId);

            return toolEquipment != null;
        }

        /// <summary>
        /// VT School Sector is not assign for this VT
        /// </summary>
        /// <param name="toolEquipmentModel"></param>
        /// <returns></returns>
        public bool CheckVTSchoolSectorExistByVTId(ToolEquipmentModel toolEquipmentModel)
        {
            VTSchoolSector vtSchoolSector = this.Context.VTSchoolSectors.FirstOrDefault(t => t.AcademicYearId == toolEquipmentModel.AcademicYearId && t.SchoolId == toolEquipmentModel.SchoolId && t.VTSchoolSectorId == toolEquipmentModel.SectorId && t.VTId == toolEquipmentModel.VTId && t.IsActive == true);

            return vtSchoolSector != null;
        }

        /// <summary>}
        /// List of ToolEquipment with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ToolEquipmentViewModel> GetToolEquipmentsByCriteria(SearchToolEquipmentModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[12];

            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "hmId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.HMId };
            sqlParams[5] = new MySqlParameter { ParameterName = "sectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "jobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[8] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[9] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[10] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[11] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.ToolEquipmentViewModels.FromSql<ToolEquipmentViewModel>("CALL GetToolEquipmentsByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @hmId, @sectorId, @jobRoleId, @schoolId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get list of TEAndRMList
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEAndRMList> GetTEAndRMList()
        {
            return this.Context.TEAndRMList.AsQueryable();
        }
    }
}