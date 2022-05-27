using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Utils
{
    public enum RoleName
    {
        CLIENT,
        DRIVER,
        ADMIN
    }

    public enum NotificationCategories
    {
        GENERAL,
        NEW_RIDE,
        ALERT,
        RIDE_CONFIRMED,
        NEWS
    }
}
