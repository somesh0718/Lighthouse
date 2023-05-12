using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Igmite.Lighthouse.BAL.Providers
{
    public class AcademicRolloverManager : GenericManager<AcademicRollOverResponse>, IAcademicRolloverManager
    {
        private readonly IAcademicRolloverRepository academicRolloverRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the academicRollOver manager.
        /// </summary>
        /// <param name="academicYearRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public AcademicRolloverManager(IAcademicRolloverRepository _academicRolloverRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.academicRolloverRepository = _academicRolloverRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get Next Academic Year
        /// </summary>
        /// <returns></returns>
        public string GetNextAcademicYear()
        {
            return this.academicRolloverRepository.GetNextAcademicYear();
        }

        /// <summary>
        /// Get Next Academic Year
        /// </summary>
        /// <returns></returns>
        public SingularResponse<bool> CheckIfVTClassExists(VTClassRequest vtClassRequest)
        {
            return this.academicRolloverRepository.CheckIfVTClassExists(vtClassRequest);
        }

        /// <summary>
        /// AcademicRollover for VTPSectors
        /// </summary>
        /// <returns></returns>
        public bool VTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.VTPSectorsTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for SchoolVTPSectors
        /// </summary>
        /// <returns></returns>
        public bool SchoolVTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.SchoolVTPSectorsTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for VCSchoolSectors
        /// </summary>
        /// <returns></returns>
        public bool VCSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.VCSchoolSectorsTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for VTSchoolSectors
        /// </summary>
        /// <returns></returns>
        public bool VTSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.VTSchoolSectorsTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for VTClasses
        /// </summary>
        /// <returns></returns>
        public bool VTClassesTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.VTClassesTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for Students
        /// </summary>
        /// <returns></returns>
        public bool StudentsTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.StudentsTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for VTP
        /// </summary>
        /// <returns></returns>
        public bool VTPTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.VTPTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for VT
        /// </summary>
        /// <returns></returns>
        public bool VTTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.VTTransfer(academicRollOverRequest);
        }

        /// <summary>
        /// AcademicRollover for VC
        /// </summary>
        /// <returns></returns>
        public bool VCTransfer(AcademicRollOverRequest academicRollOverRequest)
        {
            return this.academicRolloverRepository.VCTransfer(academicRollOverRequest);
        }
    }
}