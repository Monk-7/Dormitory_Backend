
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Principal;


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

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = new byte[64]; // สร้างข้อมูลสุ่มความยาว 64 ไบต์
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetBytes(passwordSalt); // เติมค่าสุ่มลงใน passwordSalt
                }

                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var loginHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return loginHash.SequenceEqual(passwordHash);
            }
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
            string id = "";
            if (user.Id != Guid.Empty)
            {
                id = user.Id.ToString();
            }

            List<Claim> claims = new List<Claim>{
                new Claim("role", user.role),
                new Claim("userID", id)
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
        public async Task<UserNoPW> registerAsync(UserRegister user)
        {
            createPasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);
            var _existUser = await _context.User.FirstOrDefaultAsync(_user => _user.email == user.email);

            if (_existUser != null)
            {
                return null;
            }
            var _user = new User();

            _user.email = user.email;
            _user.passwordHash = passwordHash;
            _user.passwordSalt = passwordSalt;
            _user.name = user.name;
            _user.lastname = user.lastname;
            _user.role = user.role;
            _user.phonenumber = user.phonenumber;

            await _context.User.AddAsync(_user);
            await _context.SaveChangesAsync();
            User createdUser = await _context.User.SingleOrDefaultAsync(_fuser => _fuser.email == _user.email);
            string token = CreateToken(createdUser);
            _user.token = token;
            UserNoPW _resUser = (UserNoPW) _user;
            return _resUser;
        }

        public List<User> GetAllUser()
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

        
    }   
    
}