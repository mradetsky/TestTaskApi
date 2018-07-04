using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskApi.EF.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int UpdatedBy { get; set; }
        public int CustomProperty1 { get; set; }
        public string CustomProperty2 { get; set; }
        public DateTime CustomProperty3 { get; set; }
        public decimal CustomProperty4 { get; set; }
        public int? CustomProperty5 { get; set; }
        public string CustomProperty6 { get; set; }
        public DateTime? CustomProperty7 { get; set; }
    }
}
