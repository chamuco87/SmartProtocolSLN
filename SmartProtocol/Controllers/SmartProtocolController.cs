using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartProtocol.Models;
using SmartProtocol.ViewModels;
using Nancy.Json;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cors;

namespace SmartProtocol.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("AllowAllHeaders")]
    public class SmartProtocolController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private DB_A57E75_chamucolol87Context dbContext = new DB_A57E75_chamucolol87Context();

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
                        password = Encode64(jsonData.Password);
                    }
                    else
                    {
                        throw (new Exception("Password doesn't meet the criteria."));
                    }

                }
                else {
                    throw (new Exception("Invalid/Empty Email or Password."));
                }


            
                
                var isEmailRegistered = dbContext.Email.Where(m => m.EmailAddress == email).Any();
                if (!isEmailRegistered)
                {
                    try
                    {
                        var user = new User();
                        dbContext.Add(user);
                        dbContext.SaveChanges();
                        var userEmail = new Email()
                        {
                            UserId = user.UserId,
                            EmailAddress = email,
                            IsPrimary = true,
                            IsVerified = false,
                        };
                        dbContext.Email.Add(userEmail);
                        dbContext.SaveChanges();
                        var login = new Login()
                        {
                            UserId = user.UserId,
                            EmailId = userEmail.EmailId,
                            Password = password
                        };
                        dbContext.Login.Add(login);
                        dbContext.SaveChanges();
                        var response = new ResponseViewModel() { IsSuccess = true, Data = dbContext.User.Where(m => m.UserId == user.UserId).Select(m => new { UserId = m.UserId}).FirstOrDefault() };
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

        public string Encode64(string text)
        {
            try
            {
                if (!String.IsNullOrEmpty(text))
                {
                    byte[] textBytes = Encoding.UTF8.GetBytes(text);
                    return Convert.ToBase64String(textBytes);
                }
                else {
                    throw (new Exception("Invalid string to Encode"));
                }

            }
            catch {
                throw (new Exception("Invalid string to Encode"));
            }
        }

        public string Decode64(string encodedText)
        {
            try
            {
                if (!String.IsNullOrEmpty(encodedText))
                {
                    byte[] textBytes = Convert.FromBase64String(encodedText);
                    return Encoding.UTF8.GetString(textBytes); ;
                }
                else
                {
                    throw (new Exception("Invalid string to Decode"));
                }

            }
            catch
            {
                throw (new Exception("Invalid string to Decode"));
            }
        }
    }
}
