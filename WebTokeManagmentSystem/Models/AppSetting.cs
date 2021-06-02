using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class AppSetting
    {
        public int Id { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
