﻿using Firebase.Database;
using Firebase.Database.Query;
using JEAspNetCore.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JEAspNetCore.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        string AuthSecret = "A4mpOGwFLos77X7uJMttMaeBPqURI1vKdQ2iwWBK";
        string BasePath = "https://jadocenterprises-default-rtdb.asia-southeast1.firebasedatabase.app";
        private readonly FirebaseClient firebaseClient;

        public ProjectController()
        {
            firebaseClient = new FirebaseClient(
              BasePath,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(AuthSecret)
              });
        }
        // GET: api/<ProjectController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> getProjects()
        {
            try
            {
                var getProjects = await firebaseClient
                 .Child("project")
                 .OnceAsync<ProjectModel>();

                List<ProjectModel> projectModels = new List<ProjectModel>();
                foreach (var project in getProjects)
                {
                    projectModels.Add((ProjectModel)project.Object);
                }

                return Ok(projectModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> Post(ProjectModel value)
        {
            try
            {
                value.id = Guid.NewGuid().ToString();

                var result = await firebaseClient
                    .Child("project")
                    .PostAsync(value);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Ok(ex.StackTrace);
            }
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
