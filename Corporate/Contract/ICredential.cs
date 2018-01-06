using Corporate.Models;
using Corporate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.Contract
{
    public interface ICredential
    {
        CorparateResult<Credential> GetLogin(Credential Login);
        string ChangePassword (LoginVM Login);
        DashBoard LoadDashBoard();
    }
}