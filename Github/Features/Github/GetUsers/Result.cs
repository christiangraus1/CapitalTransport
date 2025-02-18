using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Github.Features.Github.GetUsers
{
    public class Result
    {
        public string Name { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public int NumberOfFollowers { get; set; }

        public int NumberOfRepositories { get; set; }

        public decimal AverageNumberOfFollowers { get; set; }
    }
}
