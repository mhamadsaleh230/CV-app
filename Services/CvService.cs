using Hw5forExam.Data;
using Hw5forExam.Models;
using Microsoft.EntityFrameworkCore;

namespace Hw5forExam.Services;

public class CvService:ICvService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CvService(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<int> SaveCvAsync(CvBindingModel form)
    {
        string? photoFile = null;

        if (form.Photo != null && form.Photo.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            photoFile = $"{Guid.NewGuid()}_{Path.GetFileName(form.Photo.FileName)}";
            var path = Path.Combine(uploads, photoFile);

            using var stream = new FileStream(path, FileMode.Create);
            await form.Photo.CopyToAsync(stream);
        }

        var cv = new CvData
        {
            FName = form.FName,
            LName = form.LName,
            BDay = form.BDay ?? DateTime.Now,
            Nationality = form.Nationality,
            Sex = form.Sex,
            Skills = form.Skills ?? new List<string>(),
            Email = form.Email,
            Password = form.Password,
            PhotoFileName = photoFile
        };

        _context.cvs.Add(cv);
        await _context.SaveChangesAsync();
        return cv.Id;
    }
   public async Task<CvData> GetCVAsync(int id)
    {
       
        return await _context.cvs.FindAsync(id);

    }
    public async Task<int> UpdateCvAsync(int id, CvBindingModel form)
    {
        var existingCv = await _context.cvs.FindAsync(id);
        if (existingCv == null)
            return 0;

        existingCv.FName = form.FName;
        existingCv.LName = form.LName;
        existingCv.BDay = form.BDay ?? existingCv.BDay;
        existingCv.Nationality = form.Nationality;
        existingCv.Sex = form.Sex;
        existingCv.Skills = form.Skills ?? new List<string>();
        existingCv.Email = form.Email;
        existingCv.Password = form.Password;

        if (form.Photo != null && form.Photo.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            string newPhotoFile = $"{Guid.NewGuid()}_{Path.GetFileName(form.Photo.FileName)}";
            var path = Path.Combine(uploads, newPhotoFile);

            using var stream = new FileStream(path, FileMode.Create);
            await form.Photo.CopyToAsync(stream);

            // Optional: Delete old photo if it exists
            if (!string.IsNullOrEmpty(existingCv.PhotoFileName))
            {
                var oldPath = Path.Combine(uploads, existingCv.PhotoFileName);
                if (File.Exists(oldPath))
                    File.Delete(oldPath);
            }

            existingCv.PhotoFileName = newPhotoFile;
        }

        await _context.SaveChangesAsync();
        return existingCv.Id;
    }
   public async Task<bool> DeleteCvAsync(int id)
    {
        var cv = await _context.cvs.FindAsync(id);
        if (cv == null)
            return false;

        _context.cvs.Remove(cv);

        // Optional: Delete photo from server
        if (!string.IsNullOrEmpty(cv.PhotoFileName))
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            var photoPath = Path.Combine(uploads, cv.PhotoFileName);
            if (File.Exists(photoPath))
                File.Delete(photoPath);
        }

        await _context.SaveChangesAsync();
        return true;
    }
   public async Task<List<CvData>> GetAllCVsAsync()
    {
        return await _context.cvs.ToListAsync();
    }
}
