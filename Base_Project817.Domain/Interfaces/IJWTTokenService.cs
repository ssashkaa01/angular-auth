using Base_Project817.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base_Project817.Domain.Interfaces
{
    public interface IJWTTokenService
    {
        string CreateToken(User user);
    }

}
