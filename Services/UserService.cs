
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Principal;
using Microsoft.AspNetCore.StaticFiles;

namespace DormitoryAPI.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private EF_DormitoryDb _context;
        public UserService(EF_DormitoryDb context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public static string createHashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12); // คุณสามารถปรับตัวปรับตัวประมาณ (cost factor) (12) เพื่อทำให้มันช้าลง
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }

        public static bool verifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:JWTSecretKey"])),
            };
        }
        private string CreateToken(User user)
        {
            
            List<Claim> claims = new List<Claim>{
                new Claim("role", user.role),
                new Claim("userID", user.Id),
                new Claim("firstname", user.name),
                new Claim("lastname", user.lastname)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["AppSettings:JWTSecretKey"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public bool validateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameter = GetTokenValidationParameters();

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameter, out validatedToken);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<(byte[], string, string)> GetImg(string idUser)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(c => c.Id == idUser);
                var provider = new FileExtensionContentTypeProvider();
                
                if (user != null)
                {
                    if(!provider.TryGetContentType(user.profile,out var _ContentType))
                    {
                        _ContentType = "application/octet-stream";
                    }
                    var _readAllBytesAsync = await File.ReadAllBytesAsync(user.profile);
                    
                    return (_readAllBytesAsync,_ContentType,"contract");
                }
            
            }
            catch(Exception ex)
            {
                
                throw ex;
            }

           return(null,null,null);
            
        }
        public async Task<UserNoPW> registerAsync(UserRegister user)
        {
            //createPasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
            string passwordHash = createHashPassword(user.password);
            var _existUser = await _context.User.FirstOrDefaultAsync(_user => _user.email == user.email);

            if (_existUser != null)
            {
                return null;
            }
            var _user = new User();
            
            _user.Id = Guid.NewGuid().ToString();
            _user.email = user.email;
            _user.passwordHash = passwordHash;
            _user.name = user.name;
            _user.lastname = user.lastname;
            _user.role = user.role;
            _user.phonenumber = user.phonenumber;
            _user.timesTamp = DateTimeOffset.UtcNow;

            await _context.User.AddAsync(_user);
            await _context.SaveChangesAsync();
            User createdUser = await _context.User.SingleOrDefaultAsync(_fuser => _fuser.email == _user.email);
            string token = CreateToken(createdUser);
            _user.token = token;
            UserNoPW _resUser = (UserNoPW) _user;
            return _resUser;
        }
        public async Task<string> loginAsync(UserLogin user)
        {
            var _user = await _context.User.SingleOrDefaultAsync(u => u.email == user.email);
            if (_user == null)
            {
                return "UsernameFalse";
            }
            if (verifyPassword(user.password, _user.passwordHash))
            {
                string token = CreateToken(_user);
                var _userUpdate = _context.User.FirstOrDefault(u => u.Id == _user.Id);
                if (user != null)
                {
                    _userUpdate.token = token;
                    _context.SaveChanges();
                }
                
                return token;
            }
            else
            {
                return "false";
            }
        }

        public List<User> getAllUser()
        {
            List<User> response = new List<User>();
            var dataList = _context.User.ToList();
            dataList.ForEach(row => response.Add(new User()
            {
                Id = row.Id,
                IdRoom = row.IdRoom,
                name = row.name,
                lastname = row.lastname,
                email = row.email,
                role = row.role,
                phonenumber = row.phonenumber,
                
            }));
            return response;
        }


        public async Task<UserNoPW> updateIdRoom(CodeAddRoom res)
        {
           var _user = await _context.User.FirstOrDefaultAsync(user => user.Id == res.idUser);

            if (_user != null)
            {
                var _code = await _context.CodeRoom.FirstOrDefaultAsync(c => c.codeRoom == res.codeRoom);
                if(_code != null)
                {
                    _user.IdRoom = _code.idRoom;

                    _context.CodeRoom.Remove(_code);
                    await _context.SaveChangesAsync();
                    
                    UserNoPW _resUser = (UserNoPW) _user;
                    return _resUser;    
                }      
            }

            return null; // If the user is not found
        }

        public async Task<bool> updateImgUser(IFormFile file,string idUser)
        {
           var _user = await _context.User.FirstOrDefaultAsync(user => user.Id == idUser);

            if (_user != null)
            {
                
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(@"D:\CEPP\files\img", fileName);

            // บันทึกไฟล์ลงในเครื่องเซิร์ฟเวอร์
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                _user.profile = filePath;
                await _context.SaveChangesAsync();  // บันทึกข้อมูลห้อง

                return true; 
            }

            return false; // If the user is not found
        }

        

        public async Task<UserNoPW> getUserById(string userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                UserNoPW _resUser = new UserNoPW
                {
                    Id = user.Id,
                    IdRoom = user.IdRoom,
                    name = user.name,
                    lastname = user.lastname,
                    email = user.email,
                    role = user.role,
                    phonenumber = user.phonenumber,
                    token = user.token
                };

                return _resUser;
            }

            return null;
        }

        public async Task<List<UserNoPW>> getUsersByRoomId(string idRoom)
        {
            var users = await _context.User.Where(u => u.IdRoom == idRoom).ToListAsync();

            if (users != null && users.Any())
            {
                List<UserNoPW> _resUsers = users.Select(user => new UserNoPW
                {
                    Id = user.Id,
                    IdRoom = user.IdRoom,
                    name = user.name,
                    lastname = user.lastname,
                    email = user.email,
                    role = user.role,
                    phonenumber = user.phonenumber,
                    token = user.token
                }).ToList();

                return _resUsers;
            }

            return new List<UserNoPW>();
        }

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                var userToDelete = await _context.User.FindAsync(userId);

                if (userToDelete == null)
                {
                    // User not found
                    return false;
                }

                _context.User.Remove(userToDelete);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                return false;
            }
        }

    }   
    
}