using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;

namespace WebServices.REST
{
    public static class Requests
    {
        public static IRestResponse RequestForUserById(int id)
        {
            RestClient client = new RestClient("https://reqres.in/api/users/");
            RestRequest request = new RestRequest($"{id}", Method.GET);
            
            return client.Execute(request);
        }

        public static IRestResponse RequestForUsersByPage(int page)
        {
            RestClient client = new RestClient("https://reqres.in/api/users/");
            RestRequest request = new RestRequest(Method.GET);
            request.AddQueryParameter("page", page.ToString());
            
            return client.Execute(request);
        }

        public static IRestResponse RequestForPostingNewUser(User userToCreate)
        {
            RestClient client = new RestClient("https://reqres.in/api/users/");
            RestRequest request = new RestRequest(Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(userToCreate));

            return client.Execute(request);
        }

        public static IRestResponse RequestForPostingNewUser(JObject userToCreate)
        {
            RestClient client = new RestClient("https://reqres.in/api/users/");
            RestRequest request = new RestRequest(Method.POST);
            request.AddJsonBody(userToCreate.ToString());

            return client.Execute(request);
        }

        public static IRestResponse RequestForUserDeletionById(int id)
        {
            RestClient client = new RestClient("https://reqres.in/api/users/");
            RestRequest request = new RestRequest($"{id}", Method.DELETE);

            return client.Execute(request);
        }

        public static IRestResponse RequestForUpdatingUserData(int id, User user)
        {
            RestClient client = new RestClient("https://reqres.in/api/users/");
            RestRequest request = new RestRequest($"{id}", Method.PUT);
            request.AddJsonBody(JsonConvert.SerializeObject(user));

            return client.Execute(request);
        }


        // Three different ways to get the user(s) from IRestResponse/JObject-typed variable
        public static T ConvertTo<T>(IRestResponse response)
        {
            T convertedObj = new JsonDeserializer().Deserialize<T>(response);
            return convertedObj;
        }

        public static T ConvertTo<T>(JObject json, string node)
        {
            T convertedObj = json.GetValue(node).ToObject<T>();
            return convertedObj;
        }
        public static List<T> ConvertToList<T>(JObject json, string node)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json.GetValue(node).ToString());
            return list;
        }
    }
}