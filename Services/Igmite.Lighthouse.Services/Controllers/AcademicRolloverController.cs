using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all academicRollOver WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class AcademicRolloverController : BaseController
    {
        private readonly IAcademicRolloverManager academicRolloverManager;

        /// <summary>
        /// Initializes the AcademicRollover controller class.
        /// </summary>
        /// <param name="_academicYearManager"></param>
        public AcademicRolloverController(IAcademicRolloverManager _academicRolloverManager)
        {
            this.academicRolloverManager = _academicRolloverManager;
        }

        /// <summary>
        /// Get next academic year
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetNextAcademicYear")]
        public async Task<SingularResponse<string>> GetNextAcademicYear()
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                string nextAcademicYear = await Task.Run(() =>
                {
                    return this.academicRolloverManager.GetNextAcademicYear();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = nextAcademicYear;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Check If VTClass Exists
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("CheckIfVTClassExists")]
        public async Task<SingularResponse<bool>> CheckIfVTClassExists([FromBody] VTClassRequest vtClassRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();

            try
            {
                response = await Task.Run(() =>
                {
                    return this.academicRolloverManager.CheckIfVTClassExists(vtClassRequest);
                });
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// AcademicRollover for VTPSectors
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferVTPSectors")]
        public async Task<SingularResponse<bool>> VTPSectorsTransfer([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.VTPSectorsTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// AcademicRollover for SchoolVTPSectors
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferSchoolVTPSectors")]
        public async Task<SingularResponse<bool>> SchoolVTPSectorsTransfer([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.SchoolVTPSectorsTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// AcademicRollover for VCSchoolSectors
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferVCSchoolSectors")]
        public async Task<SingularResponse<bool>> VCSchoolSectorsTransfer([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.VCSchoolSectorsTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// AcademicRollover for VTSchoolSectors
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferVTSchoolSectors")]
        public async Task<SingularResponse<bool>> VTSchoolSectorsTransfer([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.VTSchoolSectorsTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// AcademicRollover for VTClasses
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferVTClasses")]
        public async Task<SingularResponse<bool>> TransferVTClasses([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.VTClassesTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// AcademicRollover for Students
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferStudents")]
        public async Task<SingularResponse<bool>> TransferStudents([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.StudentsTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Transfer for VTP (For curent year/next academic year)
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferVTP")]
        public async Task<SingularResponse<bool>> TransferVTP([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.VTPTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Transfer  for VT(For curent year/next academic year)
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferVT")]
        public async Task<SingularResponse<bool>> TransferVT([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.VTTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Transfer for VC(For curent year/next academic year)
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("TransferVC")]
        public async Task<SingularResponse<bool>> TransferVC([FromBody] AcademicRollOverRequest academicRolloverRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                bool success = await Task.Run(() =>
                {
                    return this.academicRolloverManager.VCTransfer(academicRolloverRequest);
                });

                response.Messages.Add(Constants.CreatedMessage);
                response.Result = success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}