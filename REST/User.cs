using System;
using Newtonsoft.Json;

namespace WebServices.REST
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("job")]
        public string Job { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Job: {Job}";
        }
        public override bool Equals(object obj)
        {
            if (!(obj is User other))
                return false;
            else
                return (Name.Equals(other.Name) && Job.Equals(other.Job));
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Job.GetHashCode();
        }
    }
}