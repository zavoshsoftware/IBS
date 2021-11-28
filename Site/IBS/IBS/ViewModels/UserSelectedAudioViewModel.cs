using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class UserSelectedAudioViewModel
    {
        public int WeekNo { get; set; }
        public string InductionAudio { get; set; }
        public string DeepeningAudio { get; set; }
        public string TherapyAudio { get; set; }
        public string EndingAudio { get; set; }
        public bool IsChoose { get; set; }
    }
}