using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Data.Repositories;
using AngelsAutomotive.Helpers;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Controllers
{
    public class AccountController : Controller //everything that has to do with user authentication goes through this controller 
    {
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IStateRepository _stateRepository;

        public AccountController(
            IUserHelper userHelper,
            IMailHelper mailHelper,
            IStateRepository stateRepository)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _stateRepository = stateRepository;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))//here you control which view the user returns, when trying to see the products without being logged in
                    {
                        //Return direction
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }            
            }

            this.ModelState.AddModelError(string.Empty, "Failed to Login"); //sends error message to the user
            return this.View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogOutAsync();
            return this.RedirectToAction("Index", "Home");
        }


        public IActionResult Register(int stateId)//this register is associated with the Register that is at login and RegisterNewUserViewModel
        {
            var model = new RegisterNewUserViewModel
            {
                States = _stateRepository.GetComboStates(),
                Cities = _stateRepository.GetComboCities(0)
            };

            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByEmailAsync(model.Username);
                    if (user == null)//if the user does not exist in the database
                    {

                        var city = await _stateRepository.GetCityAsync(model.CityId);
                        var state = await _stateRepository.GetStateAsync(city);

                        user = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Username,
                            UserName = model.Username,
                            Address = model.Address,
                            PhoneNumber = model.PhoneNumber,
                            CityId = model.CityId,
                            City = city,
                            State = state
                        };

                        var result = await _userHelper.AddUserAsync(user, model.Password);
                        if (result != IdentityResult.Success)//if unable to add, shows this error message
                        {
                            this.ModelState.AddModelError(string.Empty, "The user couldn´t be created.");
                            return this.View(model);//if we want to leave the fields blank, we will not return any view here if we cannot save it in the database
                        }

                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                        var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                        {
                            userid = user.Id,
                            token = myToken,
                        }, protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                           $"To allow the user, " +
                           $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                        this.ViewBag.Message = "The instructions to allow your user has been sent to email.";

                        return this.View(model);
                    }

                    //if the user already exists in the database
                    this.ModelState.AddModelError(string.Empty, "The user already exists.");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("SendMail"))
                    {
                        ModelState.AddModelError(string.Empty, "Can not send email at this time");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }

                }
            }
                //if the model is not valid
                return View(model);        
        }



        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();//TODO:change all not found for this controller to -->return new NotFoundViewResult("VehicleNotFound");
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if(user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }



        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;

                var city = await _stateRepository.GetCityAsync(user.CityId);
                if (city != null)
                {
                    var State = await _stateRepository.GetStateAsync(city);
                    if (State != null)
                    {
                        model.stateId = State.Id;
                        model.Cities = _stateRepository.GetComboCities(State.Id);
                        model.States = _stateRepository.GetComboStates();
                        model.CityId = user.CityId;
                    }
                }
            }

            model.Cities = _stateRepository.GetComboCities(model.stateId);
            model.States = _stateRepository.GetComboStates();
            return this.View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var city = await _stateRepository.GetCityAsync(model.CityId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CityId = model.CityId;
                    user.City = city;

                    var respose = await _userHelper.UpdateUserAsync(user);
                    if (respose.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }



        public IActionResult ChangePassword()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View(model);
        }



        public IActionResult RecoverPassword()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspond to a registered user.");
                    return this.View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Email, "Shop Password Reset", $"<h1>Shop Password Reset</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");
                this.ViewBag.Message = "The instructions to recover your password has been sent to your email.";
                return this.View();

            }

            return this.View(model);
        }



        public IActionResult ResetPassword(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)//returns the ResetPassword view after confirmation by email from the user
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }



        public IActionResult NotAuthorized()
        {
            return View();
        }



        public async Task<JsonResult> GetCitiesAsync(int stateId)//with JSonResult you control the information through the front-end
        {
            var state = await _stateRepository.GetStateWithCitiesAsync(stateId);
            return this.Json(state.Cities.OrderBy(c => c.Id));
        }
    }
}
