using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace task1.Models.Pages
{
    public class SearchViewModel
    {
        [Required]
        public string? SearchQuery { get; set; }

        public List<SearchResult> SearchResults { get; set; } = new List<SearchResult>(); // Инициализация списка

        [Required]
        public string? SearchString { get; set; }
    }

    public class SearchResult
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
    }

}

