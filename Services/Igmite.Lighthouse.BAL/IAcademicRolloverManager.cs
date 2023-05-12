using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the AcademicYear entity
    /// </summary>
    public interface IAcademicRolloverManager : IGenericManager<AcademicRollOverResponse>
    {
        /// <summary>
        /// Get Next Academic Year
        /// </summary>
        /// <returns></returns>
        string GetNextAcademicYear();

        /// <summary>
        /// Check If VTClass Exists
        /// </summary>
        /// <returns></returns>
        SingularResponse<bool> CheckIfVTClassExists(VTClassRequest vtClassRequest);

        /// <summary>
        /// AcademicRollover for VTPSectors
        /// </summary>
        /// <returns></returns>
        bool VTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for SchoolVTPSectors
        /// </summary>
        /// <returns></returns>
        bool SchoolVTPSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for VCSchoolSectorsTransfer
        /// </summary>
        /// <returns></returns>
        bool VCSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for VTSchoolSectorsTransfer
        /// </summary>
        /// <returns></returns>
        bool VTSchoolSectorsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for VTClassesTransfer
        /// </summary>
        /// <returns></returns>
        bool VTClassesTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for Students
        /// </summary>
        /// <returns></returns>
        bool StudentsTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for VTPTransfer
        /// </summary>
        /// <returns></returns>
        bool VTPTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for VTTransfer
        /// </summary>
        /// <returns></returns>
        bool VTTransfer(AcademicRollOverRequest academicRollOverRequest);

        /// <summary>
        /// AcademicRollover for VCTransfer
        /// </summary>
        /// <returns></returns>
        bool VCTransfer(AcademicRollOverRequest academicRollOverRequest);
    }
}