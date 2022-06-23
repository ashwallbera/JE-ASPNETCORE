using System.Runtime.Serialization;

namespace JEAspNetCore.Model
{
    [DataContract]
    public class AttendanceModel
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string projectid { get; set; }
        [DataMember]
        public string userid { get; set; }
        [DataMember]
        public string timeIn { get; set; }
        [DataMember]
        public string timeOut { get; set; }
       
        [DataMember]
        public string datecreated { get; set; }

       

    }
}
