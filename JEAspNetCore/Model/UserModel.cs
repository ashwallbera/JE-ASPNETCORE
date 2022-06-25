using System.Runtime.Serialization;

namespace JEAspNetCore.Model
{
    
    [DataContract]
    public class UserModel
    {
        [DataMember]
        public string? id { get; set; }
        [DataMember]
        public string? username { get; set; }
        [DataMember]
        public string? password { get; set; }
        [DataMember]
        public string? fname { get; set; }
        [DataMember]
        public string? lname { get; set; }
        [DataMember]
        public string? mname { get; set; }

        [DataMember]
        public string? address { get; set; }
        [DataMember]
        public string? contactno { get; set; }
        [DataMember]
        public string? role { get; set; }

        [DataMember]
        public string? position { get; set; }

        [DataMember]
        public string isDeleted { get; set; }

    }
}
