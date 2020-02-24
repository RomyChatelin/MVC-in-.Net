using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElvishTranslator.Models
{
    public class ElvishTranslatorModel
    {
        //Model with an extra layer --> Wind10M class, that has two properties
        public string timepoint { get; set; }
        public string cloudcover { get; set; }
        public string seeing { get; set; }
        public string transparency { get; set; }
        public string lifted_index { get; set; }
        public string rh2m { get; set; }
        public wind10m wind10m { get; set; }
        public string temp2m { get; set; }
        public string prec_type { get; set; }

    }
}
