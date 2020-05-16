using System;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SmartProtocol.Models;
using System.Net.Mail;

namespace SmartProtocol.Controllers
{ 
    public class SmartProtocolService : ControllerBase
    {
       
        private DB_A57E75_chamucolol87Context dbContext = new DB_A57E75_chamucolol87Context();

        public Login SaveUser(string email, string password, string activationToken)
        {
            var user = new User();
            dbContext.Add(user);
            dbContext.SaveChanges();
            var userEmail = new Email()
            {
                UserId = user.UserId,
                EmailAddress = email,
                IsPrimary = true,
                ActivationToken = activationToken,
                IsVerified = false,
            };
            dbContext.Email.Add(userEmail);
            dbContext.SaveChanges();
            var login = new Login()
            {
                UserId = user.UserId,
                EmailId = userEmail.EmailId,
                Password = password,
                CreatedDate = DateTime.Now
            };
            dbContext.Login.Add(login);
            dbContext.SaveChanges();

            return login;
        }

        public Login ValidateLogin(string emailAddress, string password)
        {
            var email = dbContext.Email.FirstOrDefault(m => m.EmailAddress == emailAddress);
            if (email != null)
            {
                var loginDetails = dbContext.Login.FirstOrDefault(m => m.EmailId == email.EmailId);
                if (loginDetails != null)
                {
                    if (loginDetails.Password == Encode64(password))
                    {
                        loginDetails.LastLoginOn = DateTime.Now;
                        dbContext.SaveChanges();
                        return loginDetails;
                        
                    }
                    else
                    {
                        throw (new Exception("The password is incorrect."));
                    }
                }
                else
                {
                    throw (new Exception("No login details found."));
                }
            }
            else
            {
                throw (new Exception("The email is not in registered."));
            }
        }

        public bool EmailAlreadyExists(string email)
        {
            return dbContext.Email.Where(m => m.EmailAddress == email).Any();
        }

        public User ValidateToken(string token)
        {
            string decodedToken = Decode64(token);
            User _user = new User();
            bool isValid = false;
            var verificationElements = decodedToken.Split(':');
            string email = "";
            string tokenV = "";
            if (verificationElements.Count() == 2)
            {
                email = verificationElements[0];
                tokenV = verificationElements[1];
            }
            
            var userEmail = dbContext.Email.FirstOrDefault(m => m.EmailAddress == email && m.ActivationToken == tokenV);
            isValid = userEmail != null;
            if (isValid)
            {
                userEmail.IsVerified = true;
                dbContext.SaveChanges();
                _user = dbContext.User.FirstOrDefault(m => m.UserId == userEmail.UserId);
                
            }

            return _user;
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
                    byte[] textBytes;

                    try
                    {
                        textBytes = Convert.FromBase64String(encodedText);
                    }
                    catch (Exception ex)
                    {
                        encodedText = encodedText.Replace('-', '+').Replace('_', '/').PadRight(4 * ((encodedText.Length + 3) / 4), '=');
                        textBytes = Convert.FromBase64String(encodedText);
                    }
                    return Encoding.UTF8.GetString(textBytes); ;
                }
                else
                {
                    throw (new Exception("Invalid string to Decode"));
                }

            }
            catch(Exception ex)
            {
                throw (new Exception("Invalid string to Decode"));
            }
        }

        public bool SendEmailMessage(string email, string activationToken)
        {
            var fromAddress = new MailAddress("jose@carbajalsalinas.com","Activate your account");
            var toAddress = new MailAddress(email);
            const string fromPassword = "QmericA2468";
            const string subject = "Activate your account";
            string body = "Please activate your account";

            string validateUrl = "http://localhost:4200/auth/confirm-email/" + Encode64(email+":"+activationToken);
            body = System.IO.File.ReadAllText(@"..\SmartProtocol\Templates\ConfirmEmailTemplate.html").ToString();
            body = body.Replace("{mailto:email}", "mailto:" + email);
            body = body.Replace("{email}", email);
            body = body.Replace("{activateUrl}", validateUrl);


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
            return true;
        }

        public string GenerateActivationToken(string email, DateTime Now)
        {
            Random random = new Random();
            int rdmNumber = random.Next();
            string token = rdmNumber.ToString() + email.Substring(2, 2) + Now.ToString().Substring(2, 2);
            return token = Encode64(token);

        }
    }
}
