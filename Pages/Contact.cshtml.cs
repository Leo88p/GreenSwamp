using System.Formats.Asn1;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CsvHelper;

namespace Lab3.Pages
{
    public class ContactModel : PageModel
    {
        public void OnGet()
        {
        }
        [BindProperty]
        public Record Record { get; set; } = new("", "", "", "");

        public void OnPost()
        {
            using var writer = new StreamWriter("messages.csv", true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecord<Record>(Record);
            csv.NextRecord();
        }

    }
    public record class Record(string Name, string Email, string Topic, string Message);
}
