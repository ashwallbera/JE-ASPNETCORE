using Firebase.Database;
using Firebase.Database.Query;
using JEAspNetCore.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JEAspNetCore.Controllers
{
    [Route("api/attendances")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        string AuthSecret = "A4mpOGwFLos77X7uJMttMaeBPqURI1vKdQ2iwWBK";
        string BasePath = "https://jadocenterprises-default-rtdb.asia-southeast1.firebasedatabase.app";
        private readonly FirebaseClient firebaseClient;

        public AttendanceController()
        {
            firebaseClient = new FirebaseClient(
              BasePath,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(AuthSecret)
              });
        }
        // GET: api/<AttendanceController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceModel>>> getAttendance()
        {
            try
            {
                var getAttendance = await firebaseClient
                 .Child("attendance")
                 .OnceAsync<AttendanceModel>();
                List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
                foreach (var attendanceModel in getAttendance.ToList())
                {
                   
                     attendanceModels.Add((AttendanceModel)attendanceModel.Object);
   
                }
                return Ok(attendanceModels);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AttendanceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AttendanceController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AttendanceModel>>> CreateAttendance(AttendanceModel model)
        {
            try
            {
               
                model.datecreated = DateTime.UtcNow.ToShortDateString();
                //Check if the userid and date are existing to database && project
                //Return Ok response
                //else if the user and date are not existing to database
                var getAttendance = await firebaseClient
                   .Child("attendance")
                   .OnceAsync<AttendanceModel>();

                List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
                foreach(var attendanceModel in getAttendance.ToList())
                {
                    AttendanceModel data = (AttendanceModel)attendanceModel.Object;
                    if (data.id.Equals(model.id)
                        && data.projectid.Equals(model.projectid)
                        && data.userid.Equals(model.userid)
                        && data.datecreated.Equals(model.datecreated))
                    {
                        return BadRequest("Already time in");
                    }
                  /*  attendanceModels.Add((AttendanceModel)attendanceModel.Object);
                    Console.WriteLine(((AttendanceModel)attendanceModel.Object));*/

                }

               model.id = Guid.NewGuid().ToString();

               model.timeIn = DateTime.UtcNow.ToShortTimeString();
                var result = await firebaseClient
                    .Child("attendance").
                    PostAsync(model);
                return Ok(result);
            }catch (Exception ex)
            {
                return Ok(ex.StackTrace);
            }
           
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<AttendanceModel>>> TimeOut(AttendanceModel model)
        {
            try
            {
               
                //Check if the userid and date are existing to database && project
                //Return Ok response
                //else if the user and date are not existing to database
                var getAttendance = (await firebaseClient
                   .Child("attendance")
                  .OnceAsync<AttendanceModel>()).Where(attendance => 
                  attendance.Object.id.Equals(model.id)
                  && attendance.Object.datecreated.Equals(model.datecreated)
                  && attendance.Object.projectid.Equals(model.projectid)
                  ).FirstOrDefault();


                if (getAttendance != null)
                {
                    if (getAttendance.Object.timeOut.ToString().Length == 0)
                    {
                        getAttendance.Object.timeOut = DateTime.UtcNow.ToShortTimeString();
                        await firebaseClient
                            .Child("attendance")
                            .Child(getAttendance.Key)
                            .PutAsync(getAttendance.Object);
                    }
                    else
                    {
                        return BadRequest("Already timeout for this day");
                    }
                    
                }

                return Ok(getAttendance);
            }
            catch (Exception ex)
            {
                return Ok(ex.StackTrace);
            }

        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
