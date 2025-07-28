using Hw5forExam.Data;
using Hw5forExam.Models;

namespace Hw5forExam.Services;

public interface ICvService
{

    Task<int> SaveCvAsync(CvBindingModel form);
    Task<CvData> GetCVAsync(int id);
    Task<int> UpdateCvAsync(int id, CvBindingModel form);
    Task<bool> DeleteCvAsync(int id);
    Task<List<CvData>> GetAllCVsAsync();
}
