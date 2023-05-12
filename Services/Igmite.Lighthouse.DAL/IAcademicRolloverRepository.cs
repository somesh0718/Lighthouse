using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;

namespace Igmite.Lighthouse.DAL
{
    public interface IAcademicRolloverRepository : IGenericRepository<AcademicRollOverResponse>
    {
        /// <summary>
        /// Get Next AcademicYear For AcademicRollOver
        /// </summary>
        /// <returns></returns>
        string GetNextAcademicYear();

        /// <summary>
        /// Check If VTClass Exists
        /// </summary>
        /// <returns></returns>
        SingularResponse<bool> CheckIfVTClassExists(VTClassRequest vtClassRequest);

        /// <summary>
        /// AcademicRollOver For VTPSectors
        /// </summary>
        /// <returns></returns>
        bool VTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For SchoolVTPSectors
        /// </summary>
        /// <returns></returns>
        bool SchoolVTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For VCSchoolSectors
        /// </summary>
        /// <returns></returns>
        bool VCSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For VTSchoolSectors
        /// </summary>
        /// <returns></returns>
        bool VTSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For VTClasses
        /// </summary>
        /// <returns></returns>
        bool VTClassesTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For VTP
        /// </summary>
        /// <returns></returns>
        bool VTPTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For VT
        /// </summary>
        /// <returns></returns>
        bool VTTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For VC
        /// </summary>
        /// <returns></returns>
        bool VCTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollOver For Students
        /// </summary>
        /// <returns></returns>
        bool StudentsTransfer(AcademicRollOverRequest academicRollOverRequest);
    }
}