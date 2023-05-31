
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TableWebApp.Data;
using TableWebApp.Models;


namespace TableWebApp.Controllers
{
    public class CSVController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        private readonly AppDbContext appDbContext;


        public CSVController(IConfiguration configuration, IWebHostEnvironment environment, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _environment = environment;
            this.appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            List<CSVData> dataList;

            dataList = appDbContext.CSVData.ToList();

            return View(dataList);
        }



        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploadDirectory = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var filePath = Path.Combine(uploadDirectory, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var data = CSVData.ReadCSV(filePath);
            // var data = ReadCSV(filePath);

            // Save data to the database

            appDbContext.CSVData.AddRange(data);
            appDbContext.SaveChanges();


            return RedirectToAction("Index");
        }


        private List<CSVData> ReadCSV(string filePath)
        {
            var data = new List<CSVData>();
            var lines = System.IO.File.ReadAllLines(filePath).Skip(1);

            foreach (var line in lines)
            {
                var values = line.Split(',');

                var csvData = new CSVData();
                csvData.Name = values[0];

                DateTime dateOfBirth;
                if (DateTime.TryParse(values[1], out dateOfBirth))
                {
                    csvData.DateOfBirth = dateOfBirth;
                }
                else
                {
                    // Обробка помилки: некоректне значення для дати народження
                    // Можна викинути виключення, пропустити запис або зробити інші дії за вашим вибором
                    continue; // Пропустити поточний запис і перейти до наступного
                }

                bool married;
                if (bool.TryParse(values[2], out married))
                {
                    csvData.Married = married;
                }
                else
                {
                    // Обробка помилки: некоректне значення для положення про одруження
                    continue; // Пропустити поточний запис і перейти до наступного
                }

                csvData.Phone = values[3];

                decimal salary;
                if (decimal.TryParse(values[4], out salary))
                {
                    csvData.Salary = salary;
                }
                else
                {
                    // Обробка помилки: некоректне значення для заробітної плати
                    continue; // Пропустити поточний запис і перейти до наступного
                }

                data.Add(csvData);
            }

            return data;
        }


        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var data = await appDbContext.CSVData.ToListAsync();
            return Json(data);

        }




        public ActionResult Delete(int id)
        {

            var data = appDbContext.CSVData.Find(id);

            if (data != null)
            {
                appDbContext.CSVData.Remove(data);
                appDbContext.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            try
            {
                // Retrieve all records from the database
                var allRecords = appDbContext.CSVData.ToList();

                // Remove all records from the context
                appDbContext.CSVData.RemoveRange(allRecords);

                // Save the changes to the database
                appDbContext.SaveChanges();


                return Ok(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting records: {ex.Message}");
            }
        }


        [HttpPost]
        public ActionResult UpdateField(int id, string field, string value)
        {

            var data = appDbContext.CSVData.Find(id);

            if (data != null)
            {
                switch (field)
                {
                    case "Name":
                        data.Name = value;
                        break;
                    case "DateOfBirth":
                        data.DateOfBirth = DateTime.Parse(value);
                        break;
                    case "Married":
                        data.Married = bool.Parse(value);
                        break;
                    case "Phone":
                        data.Phone = value;
                        break;
                    case "Salary":
                        data.Salary = decimal.Parse(value);
                        break;
                    default:
                        return new StatusCodeResult((int)HttpStatusCode.BadRequest);


                }

                appDbContext.SaveChanges();
            }


            return new StatusCodeResult((int)HttpStatusCode.OK);
        }

    }
}
