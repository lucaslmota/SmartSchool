using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itensPerPage, int totalitens, int totalPages)
        {
            CurrentPage = currentPage;
            ItensPerPage = itensPerPage;
            Totalitens = totalitens;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }
        public int ItensPerPage { get; set; }
        public int Totalitens { get; set; }
        public int TotalPages { get; set; }
    }
}