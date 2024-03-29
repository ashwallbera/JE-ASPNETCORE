﻿

using Firebase.Database;
using Firebase.Database.Query;
using JEAspNetCore.Model;
using JEAspNetCore.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JEAspNetCore.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        string AuthSecret = "A4mpOGwFLos77X7uJMttMaeBPqURI1vKdQ2iwWBK";
        string BasePath = "https://jadocenterprises-default-rtdb.asia-southeast1.firebasedatabase.app";
        private readonly FirebaseClient firebaseClient;

        private AttendanceRepository attendanceRepository;

        public UserController()
        {
            this.attendanceRepository = new AttendanceRepository();
            firebaseClient = new FirebaseClient(
              BasePath,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(AuthSecret)
              });
        }

        //GET: verifypassword
        [HttpPost("{id}")]
        public async Task<ActionResult<IEnumerable<UserModel>>> verifyUser(UserModel model)
        {
            bool isExist = await this.attendanceRepository.isUserExist(model);
            if (isExist)
            {
                return Ok(model);
            }
            else
            {
                return NotFound(model);
            }
            /*try
            {
               
                var result = await firebaseClient
                  .Child("users")
                  .OnceAsync<UserModel>();

                foreach (var data in result)
                {
                    if (data.Object.id.Equals(model.id))
                    {
                        return Accepted(data.Object);
                    }
                }

                return NotFound(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }*/
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            var users = await this.attendanceRepository.getUsers();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserModel>>> Post(UserModel value)
        {
            try
            {
                value.isDeleted = false;
                value.datecreated = DateTime.UtcNow.ToShortDateString();
                value.id = Guid.NewGuid().ToString();
                //Check if the username exist
                var getUsers = await firebaseClient
                 .Child("users")
                 .OnceAsync<UserModel>();
               
                foreach (var userModel in getUsers.ToList())
                {
                    UserModel user = ((UserModel)userModel.Object);
                    if (user.username.Equals(value.username))
                    {
                        return BadRequest("USER ALREADY EXIST!");
                    }

                }

                // If request username is not existing in db
                var result = await firebaseClient
                    .Child("users")
                    .PostAsync(value);
                return Ok(result);
            }
            catch (Exception ex)
            {

               return Ok(ex.StackTrace);
            }

            
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
