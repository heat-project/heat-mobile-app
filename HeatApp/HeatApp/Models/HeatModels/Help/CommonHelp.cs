using System;
using System.Collections.Generic;

namespace HeatApp.Models.HeatModels.Help
{
    public class CommonHelp
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public List<HelpContent> Content { get; set; }
    }

    public class HelpContent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public string Image { get; set; }
    }
}
