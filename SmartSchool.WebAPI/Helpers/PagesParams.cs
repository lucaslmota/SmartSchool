using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Helpers
{
    public class PagesParams
    {
        public const int MaxPagaSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPagaSize) ? MaxPagaSize : value; }
        }

        public int? Matricula { get; set; } = null;
        public string Nome { get; set; } = string.Empty;
        public int? Ativo { get; set; } = null;
    }
}