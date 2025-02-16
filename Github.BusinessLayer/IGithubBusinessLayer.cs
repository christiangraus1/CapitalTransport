using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Github.BusinessLayer.Entities;

namespace Github.BusinessLayer;

public interface IGithubBusinessLayer
{
    Task<List<GithubUser>> GetUsers(List<string> usernames);
}
