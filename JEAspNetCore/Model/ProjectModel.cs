using System.Runtime.Serialization;

namespace JEAspNetCore.Model
{
    [DataContract]
    public class ProjectModel
    {
        [DataMember]
        public string? id { get; set; }

        [DataMember]
        public string? name { get; set; }
        [DataMember]
        public string? location { get; set; }
        [DataMember]
        public string? isFinished { get; set; }
        [DataMember]
        public List<string>? assignProjects { get; set; }
    }
}
