using System;
using System.ComponentModel.DataAnnotations;

namespace TestLogging.Domain
{
    public class Pokemon
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
