using System;
using System.Collections.Generic;

#nullable disable

namespace Lab28_MVC
{
    public partial class QuestionInfo
    {
        public QuestionInfo()
        {
            UserInfos = new HashSet<UserInfo>();
        }

        public int IdQuestion { get; set; }
        public string QuestionValue { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}
