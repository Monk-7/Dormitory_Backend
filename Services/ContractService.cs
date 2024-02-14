
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace DormitoryAPI.Services
{
    public class ContractService
    {
        private EF_DormitoryDb _context;
        public ContractService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public async Task<bool> CreateContract(string idRoom,IFormFile file)
        {

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(@"D:\CEPP\DormitoryAPI\files\pdf", fileName);

        // บันทึกไฟล์ลงในเครื่องเซิร์ฟเวอร์
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var _contract = new Contract{
                idContract = Guid.NewGuid().ToString(),
                idRoom = idRoom,
                pdfFileName = filePath,
                timesTamp = DateTimeOffset.UtcNow
            };

            await _context.Contract.AddRangeAsync(_contract);
            await _context.SaveChangesAsync();  // บันทึกข้อมูลห้อง

            return true;
                  
        }

        public async Task<FileStream> GetPdf(string idRoom)
        {
            // Find the contract by ID
            var contract = await _context.Contract.FirstOrDefaultAsync(c => c.idRoom == idRoom);

            // If the contract is not found, return null
            if (contract != null)
            {
                return new FileStream(contract.pdfFileName, FileMode.Open);
                
            }

            // Open the PDF file and return the FileStream
            return null;
        }

        public async Task<(byte[], string, string)> GetFile(string idRoom)
        {
            try
            {
                var contract = await _context.Contract.FirstOrDefaultAsync(c => c.idRoom == idRoom);
                var provider = new FileExtensionContentTypeProvider();
                
                if (contract != null)
                {
                    if(!provider.TryGetContentType(contract.pdfFileName,out var _ContentType))
                    {
                        _ContentType = "application/octet-stream";
                    }
                    var _readAllBytesAsync = await File.ReadAllBytesAsync(contract.pdfFileName);
                    
                    return (_readAllBytesAsync,_ContentType,"contract");
                }
            
            }
            catch(Exception ex)
            {
                
                throw ex;
            }

           return(null,null,null);
            
        }
    }   
    
}