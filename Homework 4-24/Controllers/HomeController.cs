using HomeWork_4_24.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleData;
using System;
using System.Diagnostics;


namespace HomeWork_4_24.Controllers
{
    public class HomeController : Controller
    {

        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {
            var repo = new PeopleRepo(_connectionString);
            List<Person> people = repo.GetAll();
            return Json(people);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.Add(person);
        }
        [HttpPost]
        public void Delete(int Id)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.Delete(Id);
        }
        public IActionResult GetSpecificPerson(int Id)
        {
            var repo = new PeopleRepo(_connectionString);
            Person person = repo.GetSpecificPerson(Id);
            return Json(person);

        }
        [HttpPost]
        public void Edit(Person person)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.EditPerson(person);
        }

    }
}