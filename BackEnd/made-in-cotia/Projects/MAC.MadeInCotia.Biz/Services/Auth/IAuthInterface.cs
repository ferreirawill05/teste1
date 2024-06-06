using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC.MadeInCotia.Biz.Services.Auth
{
    public interface IAuthInterface
    {
        string Login(string username, string password);
    }
}
