﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Client: UserBase
    {
        public virtual ICollection<ClientReview> DriverReviews { get; set; }
    }
}
