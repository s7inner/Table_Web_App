using System.ComponentModel.DataAnnotations;

namespace TableWebApp.Models
{
    public class CSVData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^(?!\d)[A-Za-z0-9\s]{1,30}$", ErrorMessage = "Name cannot contain numbers and should be up to 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^(true|false)$", ErrorMessage = "Married field must be either true or false")]
        public bool Married { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }

        public CSVData() { }

        public CSVData(string name, DateTime dateOfBirth, bool Married, string phone, decimal salary)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Married = Married;
            this.Phone = phone;
            this.Salary = salary;
        }


        public static List<CSVData> ReadCSV(string filePath)
        {
            var csvDataList = new List<CSVData>();

            using (var reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine(); // Skipping the header line

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    string name = fields[0].Trim();
                    DateTime dateOfBirth = DateTime.Parse(fields[1].Trim());
                    bool isMarried = bool.Parse(fields[2].Trim());
                    string phone = fields[3].Trim();
                    decimal salary = decimal.Parse(fields[4].Trim());

                    CSVData csvData = new CSVData(name, dateOfBirth, isMarried, phone, salary);
                    csvDataList.Add(csvData);
                }
            }

            return csvDataList;
        }
    }
}
