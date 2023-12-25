using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Country
    {
        public Guid Id { get; set; }
        [StringLength(40)]
        public string? Name { get; set; }



    }
}
