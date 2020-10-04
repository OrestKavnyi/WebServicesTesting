using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace WebServices.REST
{
    [TestClass]
    public class Tests
    {
        // REST tests
        [TestMethod]
        public void CheckGetUserById()
        {
            var expectedUser = new UserData()
            {
                ID = 2,
                Email = "janet.weaver@reqres.in",
                FirstName = "Janet",
                LastName = "Weaver",
                Avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/josephstein/128.jpg"
            };
            var response = Requests.RequestForUserById(expectedUser.ID);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            
            var jObject = JObject.Parse(response.Content);
            var actualUser = Requests.ConvertTo<UserData>(jObject, "data");

            Assert.AreEqual(expectedUser, actualUser);
        }
        
        [TestMethod]
        public void CheckGetUsersByPage()
        {
            int page = 2;
            var expectedUsers = new List<UserData>()
            {
                new UserData() {
                ID = 7,
                Email = "michael.lawson@reqres.in",
                FirstName = "Michael",
                LastName = "Lawson",
                Avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/follettkyle/128.jpg" },
                new UserData() {
                ID = 8,
                Email = "lindsay.ferguson@reqres.in",
                FirstName = "Lindsay",
                LastName = "Ferguson",
                Avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/araa3185/128.jpg" },
                new UserData() {
                ID = 9,
                Email = "tobias.funke@reqres.in",
                FirstName = "Tobias",
                LastName = "Funke",
                Avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/vivekprvr/128.jpg" },
                new UserData() {
                ID = 10,
                Email = "byron.fields@reqres.in",
                FirstName = "Byron",
                LastName = "Fields",
                Avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/russoedu/128.jpg" },
                new UserData() {
                ID = 11,
                Email = "george.edwards@reqres.in",
                FirstName = "George",
                LastName = "Edwards",
                Avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/mrmoiree/128.jpg" },
                new UserData() {
                ID = 12,
                Email = "rachel.howell@reqres.in",
                FirstName = "Rachel",
                LastName = "Howell",
                Avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/hebertialmeida/128.jpg" }
            };
            var response = Requests.RequestForUsersByPage(page);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var jObject = JObject.Parse(response.Content);
            var actualUsers = Requests.ConvertToList<UserData>(jObject, "data");

            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.AreEqual(expectedUsers[i], actualUsers[i]);
            }
        }

        [TestMethod]
        public void CheckCreateUser1() // I named it this way just to show different ways to pass body
        {
            var userToCreate = new User() { Name = "Orest", Job = "QA" };
            // Way to provide user(body) by passing our custom object
            var response = Requests.RequestForPostingNewUser(userToCreate);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            var createdUser = Requests.ConvertTo<User>(response);

            Assert.AreEqual(userToCreate, createdUser);
        }

        [TestMethod]
        public void CheckCreateUser2()
        {
            var userToCreate = new JObject
            {
                { "name", "Orest" },
                { "job", "QA" }
            };
            // Another way to provide user(body) by directly passing JSON-typed object
            // Seems like a handy way as we can make use of AddJsonBody() method later with no need to specify RequestFormat
            var response = Requests.RequestForPostingNewUser(userToCreate);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            var createdUser = Requests.ConvertTo<User>(response);
            Assert.AreEqual(userToCreate.ToObject<User>(), createdUser);
        }

        [TestMethod]
        public void CheckDeleteUserById()
        {
            int id = 2;
            var response = Requests.RequestForUserDeletionById(id);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void CheckUpdateUser()
        {
            int id = 10;
            var userToUpdate = new User() { Name = "Matt", Job = "BA" };
            var response = Requests.RequestForUpdatingUserData(id, userToUpdate);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var updatedUser = Requests.ConvertTo<User>(response);
            Assert.AreEqual(userToUpdate, updatedUser);
        }
    }
}