﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleFlowerKalewadi.Shared
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public short RoleId { get; set; }
        public string RoleDesc { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
