using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskApi.Dto
{
    public class ResourceEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [Range(0, 100)]
        public int CustomProperty1 { get; set; }

        [StringLength(12, MinimumLength = 6)]
        public string CustomProperty2 { get; set; }

        [Range(typeof(DateTime), "1/1/2018", "1/1/9999")]
        public DateTime CustomProperty3 { get; set; }
        public decimal CustomProperty4 { get; set; }
        public int? CustomProperty5 { get; set; }
        public string CustomProperty6 { get; set; }
        public DateTime? CustomProperty7 { get; set; }
    }
}
