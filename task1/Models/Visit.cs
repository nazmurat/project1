using System;
namespace card.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Specialist { get; set; } = "";
        public string Diagnosis { get; set; } = "";
        public string Complaints { get; set; } = "";

    }
}

