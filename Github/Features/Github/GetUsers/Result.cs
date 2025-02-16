using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Github.Features.Github.GetUsers
{
    public record Result
        (
          string Name,
          string Login,
          string Company,
          int NumberOfFollowers,
          int NumberOfRepositories
        );
}
