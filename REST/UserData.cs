using System;
using Newtonsoft.Json;

namespace WebServices.REST

{
    public class UserData
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Email: {Email}, FirstName: {FirstName}, LastName: {LastName}, Avatar: {Avatar}";
        }
        public override bool Equals(object obj)
        {
            if (!(obj is UserData other))
                return false;
            else
                return (ID == other.ID && Email.Equals(other.Email) && FirstName.Equals(other.FirstName) &&
                                          LastName.Equals(other.LastName) && Avatar.Equals(other.Avatar));
        }
        public override int GetHashCode()
        {
            return (ID.GetHashCode() ^ Email.GetHashCode() ^ FirstName.GetHashCode() ^
                    LastName.GetHashCode() ^ Avatar.GetHashCode());
        }
    }
}