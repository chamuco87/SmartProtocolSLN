using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SmartProtocol.Models;

namespace SmartProtocol.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmartProtocolController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private DB_A57E75_chamucolol87Context dbContext = new DB_A57E75_chamucolol87Context();

        [HttpPost("Register")]
        public long Register([FromBody] JToken data)
        {
            string email = "jose.carbajal.salinas@gmail.com";
            string password = "Lom@s246";
            var user = new User();
            dbContext.Add(user);
            dbContext.SaveChanges();
            var userEmail = new Email() { 
                UserId= user.UserId,
                EmailAddress = email,
                IsPrimary = true,
                IsVerified = false,
            };
            dbContext.Email.Add(userEmail);
            dbContext.SaveChanges();
            var login = new Login() { 
                UserId = user.UserId,
                EmailId = userEmail.EmailId,
                Password = password
            };

            dbContext.SaveChanges();
            return login.UserId;
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
    }
}
