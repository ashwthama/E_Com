using AutoMapper;
using E_Commerce.Domain.Models;
using E_Commerce.Repository.UserRepo;
using E_Commerce.Services.EncDec;
using E_Commerce.ViewModels;
using E_Commerce.ViewModels.ResponseViewModel;
using E_Commerce.ViewModels.UserVM;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace E_Commerce.Services.AccountServices
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _usrRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;
        EncryptionDecryption codeDecode = new EncryptionDecryption();



        public UserService(IUserRepository usrRepo, IMapper mapper, IConfiguration configuration)
        {
            _usrRepo = usrRepo;
            _mapper = mapper;
            _iconfiguration = configuration;
        }


        // auto mapper
        public ResponseVM GetAllUser()
        {
            var response = new ResponseVM();
            try
            {
                var userlist = _usrRepo.GetUsers();
                var usersViewModel = _mapper.Map<List<User>>(userlist);
                response.Response = usersViewModel;
            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while fetching data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;

            }
            return response;

        }
        public async Task<ResponseVM> Register(UserViewModel registerVm)

        {
            var response = new ResponseVM();
            
            try
            {
                if (registerVm.FirstName == null || registerVm.FirstName == "" || registerVm.LastName == null || registerVm.LastName == "")
                {
                    response.Message = "All detaisls is required.";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Response = null;


                }
                else
                {
                    
                    registerVm.Password = Password(8);
                    registerVm.Password = codeDecode.EncryptString(registerVm.Password);
                    
                    var user = _mapper.Map<User>(registerVm);
                    user.UserName = codeDecode.EncryptString(user.UserName);

                    _usrRepo.AddUser(user);

                    registerVm.UserName=codeDecode.DecryptString(user.UserName);
                    registerVm.Password=codeDecode.DecryptString(user.Password);

                    var body = "User Name:" + registerVm.UserName
                               + "   Password:" + registerVm.Password;

                    var mailMessage = new MimeMessage();
                    mailMessage.From.Add(new MailboxAddress("Abu Hamza", _iconfiguration["SMTPGmailEmail"]));
                    mailMessage.To.Add(new MailboxAddress(registerVm.FirstName + " " + registerVm.LastName, registerVm.Email));
                    mailMessage.Subject = "Login Ceredential";



                    var builder = new BodyBuilder();

                    builder.HtmlBody = body;

                    mailMessage.Body = builder.ToMessageBody();


                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.CheckCertificateRevocation = false;
                        await smtpClient.ConnectAsync(_iconfiguration["GmailHost"], Convert.ToInt16(_iconfiguration["GmailPort"]), false);
                        await smtpClient.AuthenticateAsync(_iconfiguration["SMTPGmailEmail"], _iconfiguration["SMTPGmailPassword"]);
                        //smtpClient.SslProtocols = 

                        //await smtpClient.SendAsync(mailMessage);
                        await smtpClient.DisconnectAsync(true);

                    }

                    response.Message = "Register Successfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Response = user;
                }


            }
            catch (Exception ex)
            {
                response.Message = "Exception occur while adding data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;
            }
            return response;

        }

        public ResponseVM Login(LoginVM UserLoginVm)
        {
            var response = new ResponseVM();
            try
            {
                if (UserLoginVm.UserName == null || UserLoginVm.UserName == "" || UserLoginVm.Password == null || UserLoginVm.Password == "")
                {
                    response.Message = "UserName and Password cannot be null and Empty.";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Response = null;
                }
                else
                {
                    string UserNmae = codeDecode.EncryptString( UserLoginVm.UserName);
                    string password = codeDecode.EncryptString(UserLoginVm.Password);
                    
                    var obj = _usrRepo.Login(UserNmae, password);
                    if (obj != null)
                    {
                        //generate Otp
                        string Generatedotp = Password(6);  

                        //update otp data base
                        var Otpdata = new OTPTable()
                        {
                            OTP = Generatedotp,
                            OtpGenerationDatetime = DateTime.Now,
                            OtpExpireDatetime = DateTime.UtcNow.AddMinutes(10),
                            UserID = obj.UserId
                        };

                        _usrRepo.InsertOtp(Otpdata);   //adding data to otp tabel                                                                              
                        OTPVM oTPVM = new OTPVM();
                        oTPVM.Otp = Generatedotp;
                        var user = ValidateOTP(oTPVM);



                        ////sending otp to mobile number
                        //string accountSid = Environment.GetEnvironmentVariable("Sid");
                        //string authToken = Environment.GetEnvironmentVariable("Token");

                        //TwilioClient.Init(accountSid, authToken);

                        //var message = MessageResource.Create(
                        //    body: $"The Otp is Valid for 10 min only {Generatedotp}. ",
                        //    from: new Twilio.Types.PhoneNumber("+15617695980"),
                        //    to: new Twilio.Types.PhoneNumber("+916306983426")
                        //);


                        //var res = message.Sid;


                        //response.StatusCode = (int)HttpStatusCode.OK;
                        //response.Message = "Otp Sent. ";
                        //response.Response = res;


                        return user;

                        
                    
                    }


                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "User not Found";
                    response.Response = null;



                }

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "something went wrong while login";

                response.Response = null;
            }
            return response;




        }

        //OTP validation
        public ResponseVM ValidateOTP(OTPVM OTP)
        {
            var response = new ResponseVM();
            var res = _usrRepo.ValidateOtp(OTP.Otp);
            var userid = res.UserID;

            //Expiration Time
            //if (res.OtpExpireDatetime < DateTime.UtcNow)
            //{
            //    string Generatedotp = Password(6);

            //    //update otp data base
            //    var Otpdata = new OTPTable()
            //    {
            //        OTP = Generatedotp,
            //        OtpGenerationDatetime = DateTime.Now,
            //        OtpExpireDatetime = DateTime.UtcNow.AddMinutes(10),
            //        UserID = userid
            //    };

            //    _usrRepo.InsertOtp(Otpdata);
            //    //time excedded resend otp
            //    string accountSid = Environment.GetEnvironmentVariable("Sid");
            //    string authToken = Environment.GetEnvironmentVariable("Token");

            //    TwilioClient.Init(accountSid, authToken);

            //    var message = MessageResource.Create(
            //        body: $"The Otp is Valid for 10 min only {Generatedotp}. ",
            //        from: new Twilio.Types.PhoneNumber("+15617695980"),
            //        to: new Twilio.Types.PhoneNumber("+916306983426")
            //    );

            //    response.Message = message.Sid;
            //    return response;
            //}

            //getting user details
            var UserRes = _usrRepo.GetDataByID(res.UserID);

            ////getting roles based on user id
            //var UserRolls = _userRepo.GetRolls(res.UserID);

            if (res != null)
            {
                //generate token

                //token generate
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                     new Claim(ClaimTypes.Name, UserRes.FirstName+UserRes.LastName),
                     new Claim("Id",UserRes.UserId.ToString()),
                     new Claim(ClaimTypes.Role ,UserRes.UserType)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var data = new TokenResponse { Token = tokenHandler.WriteToken(token) };
                response.Response = data.Token.ToString();
                response.Message = res.UserID.ToString();
                response.StatusCode = 200;

                return response;

            }


            return response;


        }


        public async Task<ResponseVM> UpdatePassword(string email,EmailVM uservm)
        {
            ResponseVM response = new ResponseVM();
            try
            {
                if (email == null)
                {
                    response.Message = "Enter Email";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return response;
                }
                User olddata = _usrRepo.EmailVerification(email);
                if (olddata != null)
                {
                    uservm.password = Password(8);
                    uservm.password = codeDecode.EncryptString(uservm.password);
                    var newdata = _mapper.Map<EmailVM, User>(uservm, olddata);
                    _usrRepo.UpdatePassword(olddata, newdata);
                    //response.Response = newdata;

                    uservm.password=codeDecode.DecryptString(newdata.Password);

                    var body = "Password:" + uservm.password;


                    var mailMessage = new MimeMessage();
                    mailMessage.From.Add(new MailboxAddress("Abu Hamza", _iconfiguration["SMTPGmailEmail"]));
                    mailMessage.To.Add(new MailboxAddress(olddata.FirstName + " " + olddata.LastName, olddata.Email));
                    mailMessage.Subject = "New Password";



                    var builder = new BodyBuilder();

                    builder.HtmlBody = body;

                    mailMessage.Body = builder.ToMessageBody();


                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.CheckCertificateRevocation = false;
                        await smtpClient.ConnectAsync(_iconfiguration["GmailHost"], Convert.ToInt16(_iconfiguration["GmailPort"]), false);
                        await smtpClient.AuthenticateAsync(_iconfiguration["SMTPGmailEmail"], _iconfiguration["SMTPGmailPassword"]);
                        //smtpClient.SslProtocols = 
                        await smtpClient.SendAsync(mailMessage);
                        await smtpClient.DisconnectAsync(true);

                    }
                    response.Message = "Updted Successfully";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Response = newdata;
                }

            }
           
            catch (Exception ex)
            {
                response.Message = "Exception occur while updating data";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Response = null;
            }

            return response;
        }

        //Random string Generator
        public string Password(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString; 
        }


        public string DecodePass(string encPass)
        {

            return codeDecode.DecryptString(encPass);
        }

    }
}

