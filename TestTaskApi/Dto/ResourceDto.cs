using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskApi.Dto
{
    public class ResourceDto : ResourceEditDto
    {
        
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int UpdatedBy { get; set; }

       
    }
}
