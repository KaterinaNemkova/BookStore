using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class Portfolio
    {
        public Guid BookId { get; set; }

        public Guid UserId { get; set; }
        
    }
}
