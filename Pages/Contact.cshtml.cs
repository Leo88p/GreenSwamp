using System.Formats.Asn1;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CsvHelper;
using System.Text.RegularExpressions;

namespace Lab3.Pages
{
    public class ContactModel : PageModel
    {
        public void OnGet()
        {
        }
        [BindProperty]
        public Record Record { get; set; } = new("", "", "", "");

        public IActionResult OnPost()
        {
            using var writer = new StreamWriter("messages.csv", true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            Regex regex = new Regex(@".+@.+\.edu");
            if (String.IsNullOrWhiteSpace(Record.Name) || String.IsNullOrWhiteSpace(Record.Message) || !regex.IsMatch(Record.Email))
            {
                return new JsonResult("wrong");
            }
            else
            {
                csv.WriteRecord<Record>(Record);
                csv.NextRecord();
                return new JsonResult("correct");
            }
        }

    }
    public record class Record(string Name, string Email, string Topic, string Message);
}
