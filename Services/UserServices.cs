
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


        public async Task<UserNoPW> updateIdRoom(string idUser, string idRoom)
        {
            // Use FirstOrDefaultAsync and await to wait for the data retrieval to complete
            var _user = await _context.User.FirstOrDefaultAsync(user => user.Id == idUser);

            if (_user != null)
            {
                // Update the idRoom with the provided value
                _user.IdRoom = idRoom;

                // Save the update to the database
                await _context.SaveChangesAsync();

                // Convert the result to UserNoPW
                UserNoPW _resUser = (UserNoPW) _user;

                return _resUser;
            }

            return null; // If the user is not found
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

    }   
    
}