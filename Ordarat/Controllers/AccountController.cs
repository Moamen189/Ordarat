using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities.Identity;
using Ordarat.Dtos;
using Ordarat.Errors;
using Ordarat.Extensions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager , ITokenServices tokenServices , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           _tokenServices = tokenServices;
            _mapper = mapper;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreateToken(user, _userManager)
            });
        }



        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailExists(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new [] {" This Email is already in used"}});
            var user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0],
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                Address = new Address()
                {
                    FirstName = registerDto.FisrtName,
                    LastName = registerDto.LastName,
                    City = registerDto.City,
                    Country = registerDto.Country,
                    Streeet = registerDto.Street

                }

            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDto()
            {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreateToken(user, _userManager)

            });

        }
        [Authorize]
        [HttpGet]

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByIdAsync(email);
            return Ok(new UserDto()
            {
                DisplayName =user.DisplayName,
                Email=user.Email,
                Token = await _tokenServices.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            return Ok(_mapper.Map<Address,AddressDto>(user.Address));


        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdayeUserAddress(AddressDto newAddress)
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            user.Address = _mapper.Map<AddressDto, Address>(newAddress);
            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new [] {"An Error Occured with updating user adress"}});
            return Ok(newAddress);


        }

        [HttpGet("emailexists")]

        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }





    }


}
