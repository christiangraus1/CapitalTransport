using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github.BusinessLayer.Entities
{
    public class GithubRepo
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
    }
}
