using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github.BusinessLayer.Entities;

public class GithubUser
{
    public string Name { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string Followers_Url { get; set; } = string.Empty;

    public string Repos_Url { get; set; } = string.Empty;

    public int NumberOfFollowers { get; set; }

    public int NumberOfRepositories { get; set; }
}
