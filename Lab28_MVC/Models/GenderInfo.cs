using System;
using System.Collections.Generic;

#nullable disable

namespace Lab28_MVC
{
    public partial class GenderInfo
    {
        public GenderInfo()
        {
            UserInfos = new HashSet<UserInfo>();
        }

        public int IdGender { get; set; }
        public string GenderValue { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}
