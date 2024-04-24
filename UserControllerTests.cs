using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList, result.Model);
        }

        [TestMethod]
        public void Details_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Details(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList.FirstOrDefault(u => u.Id == userId), result.Model);
        }

        [TestMethod]
        public void Details_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Details(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Create_GET_ReturnsViewWithNewUser()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(User));
        }

        [TestMethod]
        public void Create_POST_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>();
            UserController.userlist = userList;
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            CollectionAssert.Contains(userList, user);
        }

        [TestMethod]
        public void Create_POST_InvalidUser_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>();
            UserController.userlist = userList;
            var user = new User { Id = 1, Name = "John", Email = "" };

            // Act
            var result = controller.Create(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result.Model);
        }

        [TestMethod]
        public void Edit_GET_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Edit(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList.FirstOrDefault(u => u.Id == userId), result.Model);
        }

        [TestMethod]
        public void Edit_GET_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Edit(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Edit_POST_ExistingUserId_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;
            var user = new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };

            // Act
            var result = controller.Edit(userId, user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            var updatedUser = userList.FirstOrDefault(u => u.Id == userId);
            Assert.AreEqual(user.Name, updatedUser.Name);
            Assert.AreEqual(user.Email, updatedUser.Email);
        }

        [TestMethod]
        public void Edit_POST_ExistingUserId_InvalidUser_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;
            var user = new User { Id = 1, Name = "John Doe", Email = "" };

            // Act
            var result = controller.Edit(userId, user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result.Model);
        }

        [TestMethod]
        public void Delete_GET_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Delete(userId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList.FirstOrDefault(u => u.Id == userId), result.Model);
        }

        [TestMethod]
        public void Delete_GET_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 3;

            // Act
            var result = controller.Delete(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Delete_POST_ExistingUserId_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userList;
            var userId = 1;

            // Act
            var result = controller.Delete(userId, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(userList.Any(u => u.Id == userId));
        }
    }
}
