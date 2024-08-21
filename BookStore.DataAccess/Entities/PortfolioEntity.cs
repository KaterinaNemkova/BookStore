using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Entities
{
    public class PortfolioEntity
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }

       

    }
}
