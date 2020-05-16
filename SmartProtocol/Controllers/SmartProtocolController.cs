using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartProtocol.ViewModels;
using Nancy.Json;
using System.Net.Mail;
using System.Text.RegularExpressions;
using SmartProtocol.Models.ViewModels.WebModels;

namespace SmartProtocol.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("AllowAllHeaders")]
    public class SmartProtocolController : ControllerBase
    {
        SmartProtocolService _smartProtocolService = new SmartProtocolService();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpPost("Login")]

        public ContentResult Login([FromBody] JToken data)
        {
            SingUpViewModel jsonData;
            try
            {
                jsonData = JsonConvert.DeserializeObject<SingUpViewModel>(data.ToString());

                if (!string.IsNullOrEmpty(jsonData.Email) && !string.IsNullOrEmpty(jsonData.Password))
                {
                    var _Login = _smartProtocolService.ValidateLogin(jsonData.Email, jsonData.Password);
                    var response = new ResponseViewModel() { IsSuccess = true, Data = new User() { _userId = _Login.UserId } };
                    return GenerateResponse(response);
                }
                else
                {
                    throw (new Exception("Pass all parameters email and password."));
                }
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                errors.Add(new Error() { ErrorCode = "VAL0125", ErrorDetail = ex.Message });
                return GenerateErrorResponse(errors);
            }
        }

        [HttpPost("Register")]
        
        public ContentResult Register([FromBody] JToken data)
        {
            string email = "";
            string password = "";
            SingUpViewModel jsonData;

            try
            {
                jsonData = JsonConvert.DeserializeObject<SingUpViewModel>(data.ToString());

                if (!string.IsNullOrEmpty(jsonData.Email) && !string.IsNullOrEmpty(jsonData.Password))
                {
                    if(IsValidEmailFormat(jsonData.Email))
                    {
                        email = jsonData.Email;
                    }
                    else
                    {
                        throw (new Exception("Email format invalid."));
                    }

                    if(IsValidPasswordFormat(jsonData.Password))
                    { 
                        password = _smartProtocolService.Encode64(jsonData.Password);
                    }
                    else
                    {
                        throw (new Exception("Password doesn't meet the criteria."));
                    }

                }
                else {
                    throw (new Exception("Invalid/Empty Email or Password."));
                }


                var isEmailRegistered = EmailAlreadyExists(email);
                if (!isEmailRegistered)
                {
                    try
                    {
                        string activationToken = _smartProtocolService.GenerateActivationToken(email, DateTime.Now);
                        var _Login =_smartProtocolService.SaveUser(email, password, activationToken);
                        _smartProtocolService.SendEmailMessage(email, activationToken);
                        var response = new ResponseViewModel() { IsSuccess = true, Data = new User() { _userId = _Login.UserId } };
                        return GenerateResponse(response);
                    }
                    catch (Exception ex)
                    {
                        throw (new Exception("There was an issue while creating the user, try again later."));
                    }
                }
                else {
                    throw (new Exception("The email address already has an account."));

                }
                
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                errors.Add(new Error() { ErrorCode = "ECR0120", ErrorDetail = ex.Message });
                return GenerateErrorResponse(errors);
            }
        }


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public bool EmailAlreadyExists(string email)
        {
           return _smartProtocolService.EmailAlreadyExists(email);
        }

        [HttpPost("EmailExists")]
        public ContentResult EmailExists([FromBody] JToken data)
        {
            var jsonData = JsonConvert.DeserializeObject<SingUpViewModel>(data.ToString());
            bool exists = false;
            if (!string.IsNullOrEmpty(jsonData.Email))
            {
                exists = EmailAlreadyExists(jsonData.Email);
            }
            var response = new ResponseViewModel() { IsSuccess = true, Data = new { emailExists = exists } };
            return GenerateResponse(response);
        }

        [HttpPost("ValidateToken")]
        public ContentResult ValidateToken([FromBody] JToken data)
        {
            try
            {
                var jsonData = JsonConvert.DeserializeObject<TokenValidatorViewModel>(data.ToString());
                User _user = new User();
                Models.User user = new Models.User();
                if (!string.IsNullOrEmpty(jsonData.token))
                {
                    user = _smartProtocolService.ValidateToken(jsonData.token);

                }
                var response = new ResponseViewModel() { IsSuccess = true, Data = new User() { email = user.Email.Where(m => m.IsPrimary == true).FirstOrDefault().EmailAddress, _userId = user.UserId } };
                return GenerateResponse(response);
            }
            catch (Exception)
            {
                var response = new ResponseViewModel() { IsSuccess = true, Data = new User() };
                return GenerateResponse(response);
            }
        }

        [HttpGet("IsValidEmail")]
        public ContentResult IsValidEmail(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (IsValidEmailFormat(email))
                    {
                        var response = new ResponseViewModel() { IsSuccess = true };
                        return GenerateResponse(response);
                    }
                    else
                    {
                        throw (new Exception("The email has invalid format"));
                    }
                }
                else
                {
                    throw (new Exception("Empty email, check parameters."));
                }
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                errors.Add(new Error() { ErrorCode = "VAL0120", ErrorDetail = ex.Message });
                return GenerateErrorResponse(errors);
            }
            
        }

        [HttpGet("IsValidPassword")]
        public ContentResult IsValidPassword(string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    if (IsValidPasswordFormat(password))
                    {
                        var response = new ResponseViewModel() { IsSuccess = true };
                        return GenerateResponse(response);
                    }
                    else
                    {
                        throw (new Exception("The password has invalid format"));
                    }
                }
                else {
                    throw (new Exception("Empty password, check parameters."));
                }
            }
            catch (Exception ex)
            {
                List<Error> errors = new List<Error>();
                errors.Add(new Error() { ErrorCode = "VAL0121", ErrorDetail = ex.Message });
                return GenerateErrorResponse(errors);
            }
        }

        public bool IsValidEmailFormat(string email)
        {
            bool isValid = false;
            try
            {
                MailAddress m = new MailAddress(email);
                isValid = true;
            }
            catch {
                isValid = false; 
            }
            return isValid;
        }

        public bool IsValidPasswordFormat(string password)
        {
            return true;
            bool isValid = false;
            try
            {
                var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
                var match = Regex.Match(password, regex);
                if (match.Success)
                {
                    isValid = true;
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        public ContentResult GenerateResponse(ResponseViewModel data)
        {
            var json = new JavaScriptSerializer().Serialize(data);
            return new ContentResult { Content = json, StatusCode = (int)HttpStatusCode.OK, ContentType = "application/json" };
        }

        public ContentResult GenerateErrorResponse(List<Error> errors)
        {
            var response = new ResponseViewModel() { IsSuccess = false, Errors = errors };
            return GenerateResponse(response);
        }

    }
}
