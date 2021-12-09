using System;

namespace BookStore.Domain.ViewModels
{
    public class SearchViewModel
    {
        public int CategoryId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string BookName { get; set; }
        
        public int StartRating { get; set; }
        
        public int EndRating { get; set; }
    }
}