using System;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;
using Repository;
using Logic;
using Models;
using Repository.DbRepository;
using Logic.Logic;
using System.Security.Cryptography.X509Certificates;

namespace Test
{
    /// <summary>
    /// Test algorithms for CRUD methods
    /// </summary>
    [TestFixture]
    public class Tests
    {
        UserLogic Ulogic;
        Mock<IRepository<User>> mockUsers;
        TaskLogic Tlogic;
        Mock<IRepository<Models.Task>> mockTasks;

        [SetUp]
        public void Init()
        {
            Models.Task t1 = new Models.Task(1, "Task 1") {Description = "This is the first task"};
            Models.Task t2 = new Models.Task(2, "Task 2");
            User a = new User(1, "Jani") { Tasks = new List<Models.Task>() { t1} };
            User b = new User(2, "Dezso");

            mockUsers = new Mock<IRepository<User>>();
            mockUsers.Setup(m => m.ReadAll()).Returns(new List<User>()
            {
                a,
                b,
            }.AsQueryable());
            Ulogic = new UserLogic(mockUsers.Object);
            ;
            mockTasks = new Mock<IRepository<Models.Task>>();
            mockTasks.Setup(m => m.ReadAll()).Returns(new List<Models.Task>()
            {
                t1,
                t2,
            }.AsQueryable());
            Tlogic = new TaskLogic(mockTasks.Object);

        }
        [Test]
        public void UserCreateTest()
        {
            Assert.That(() => Ulogic.CreateUser(new User(3, "test")),Throws.Nothing);
        }
        [Test]
        public void UserUpdateTest()
        {
            User t = Ulogic.ReadUser(2);
            t.Tasks.Add(Tlogic.ReadTask(1));
            Ulogic.UpdateUser(t);
            Assert.That(t.Tasks.Count() == Ulogic.ReadUser(t.Id).Tasks.Count());
        }
        [Test]
        public void UserDeleteTest()
        {
            Assert.That(()=>Ulogic.DeleteUser(2), Throws.Nothing);
        }
        [Test]
        public void UserReadTest()
        {
            Assert.That(Ulogic.ReadUser(2).Username == "Dezso");
        }
        [Test]
        public void UserReadAllTest()
        {
            List<User> t = Ulogic.ReadAllUser().ToList();
            Assert.That(t.Count() == 2);
        }
        [Test]
        public void TaskCreateTest()
        {
            Assert.That(()=> Tlogic.CreateTask(new Models.Task(3, "test")), Throws.Nothing);
        }
        [Test]
        public void TaskUpdateTest()
        {
            Models.Task t = Tlogic.ReadTask(2);
            t.Title = "test";
            Tlogic.UpdateTask(t);
            Assert.That(t.Title == "test");
        }
        [Test]
        public void TaskDeleteTest()
        {
            Assert.That(() => Tlogic.DeleteTask(2), Throws.Nothing);
        }
        [Test]
        public void TaskReadTest()
        {
            Assert.That(Tlogic.ReadTask(2).Title == "Task 2");
        }
        [Test]
        public void TaskReadAllTest()
        {
            Assert.That(Tlogic.ReadAllTask().Count() == 2);
        }
    }
}