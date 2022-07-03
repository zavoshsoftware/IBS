using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class UserSelectedAudioViewModel
    {
        public Guid Id { get; set; }
        public int WeekNo { get; set; }
        public string InductionAudio { get; set; }
        public string DeepeningAudio { get; set; }
        public string TherapyAudio { get; set; }
        public string EndingAudio { get; set; }
        public bool IsChoose { get; set; }
        public bool IsAvailable { get; set; }

        public Guid AudioId1 { get; set; }
        public Guid AudioId2 { get; set; }
        public Guid AudioId3 { get; set; }
        public Guid AudioId4 { get; set; }
    }
}