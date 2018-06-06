using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneWk6TaskListRevisted.Models;

namespace CapstoneWk6TaskListRevisted.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration()
        {

            return View();
        }

        
        public ActionResult RegisterNewUser(User newUser)
        {

            TaskListWk6Entities ORM = new TaskListWk6Entities();

            ORM.Users.Add(newUser);

            ORM.SaveChanges();

            return RedirectToAction("TaskList");
            

        }

        public ActionResult SignIn(User existingUser)
        {
            TaskListWk6Entities ORM = new TaskListWk6Entities();

            List<User> users = ORM.Users.ToList();

            if(users.Where(x=>x.UserName==existingUser.UserName).ToList().Count()== 0)
            {
                ViewBag.Error = "Username does not exist. Did you mean to register?";
                return View("Error");
            }
            User thisUser = ORM.Users.Find(existingUser.UserName);

            if (thisUser.Password != existingUser.Password)
            {
                ViewBag.Error = "Incorrect password. Go to home screen and try again";
                return View("Error");

            }
            ViewBag.Message = $"Welcome {existingUser.UserName}";
            return RedirectToAction("TaskList");


        }

        public ActionResult TaskList()
        {
            TaskListWk6Entities ORM = new TaskListWk6Entities();
            ViewBag.TaskList = ORM.Tasks.ToList();
            return View();

        


        }
        //[ValidateAntiForgeryToken]
        public ActionResult AddNewTask(Task newTask)
        {
            TaskListWk6Entities ORM = new TaskListWk6Entities();

            if (ModelState.IsValid)
            {
                //if (ORM.Tasks.ToList().Count == 0)
                //{
                //    newTask.TaskID = "1";
                //}
                //else
                //{
                //    newTask.TaskID = int.Parse((ORM.Tasks.ToList().Last().TaskID) + 1).ToString();
                   
                //}
                ORM.Tasks.Add(newTask);
                ORM.SaveChanges();
                return RedirectToAction("TaskList");
            }
            else
            {
                ViewBag.ErrorMessage = "Oops! Something went wrong.";
                return View("Error");
            }

        }

        public ActionResult AddTask()
        {

            return View();
        }

        public ActionResult DeleteTask(string TaskID)
        {
            TaskListWk6Entities ORM = new TaskListWk6Entities();
            Task Found = ORM.Tasks.Find(TaskID);
            if(Found != null)
            {
                ORM.Tasks.Remove(Found);
                ORM.SaveChanges();

                return RedirectToAction("TaskList");

            }
            else
            {
                ViewBag.ErrorMessage = "Oops! Something went wrong.";
                    return View("Error");

            }

        }

        public ActionResult ChangeStatus(string TaskID)
        {
            TaskListWk6Entities ORM = new TaskListWk6Entities();
            Task Found = ORM.Tasks.Find(TaskID);
            if (Found.Status == "Incomplete")
            {
                Found.Status="Complete";
                ORM.SaveChanges();

            }
            else if (Found.Status == "Complete")
            {
                Found.Status = "Incomplete";
                ORM.SaveChanges();

            }
            else
            {
                ViewBag.ErrorMessage = "Oops! Something went wrong.";
                return View("Error");
            }
            return RedirectToAction("TaskList");


        }
    }
}