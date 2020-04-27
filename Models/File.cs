using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisOmnicrypt.Models
{
    public class File : IFileRepository
    {
        public string Value { get; set; } = "";
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
