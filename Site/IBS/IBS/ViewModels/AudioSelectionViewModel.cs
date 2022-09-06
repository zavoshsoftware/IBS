using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class AudioSelectionViewModel
    {
        public string userSelectedAudioId { get; set; }
        public string id1 { get; set; }
        public string id2 { get; set; }
        public string id3 { get; set; }
        public string id4 { get; set; }
        public string gender { get; set; }
        public string QuestionnarieId { get; set; }
        public string Audiomen1 { get; set; }
        public string Audiomen2 { get; set; }
        public string Audiomen3 { get; set; }
        public string Audiomen4 { get; set; }
        public string Audiowomen1 { get; set; }
        public string Audiowomen2 { get; set; }
        public string Audiowomen3 { get; set; }
        public string Audiowomen4 { get; set; }
        public int WeekNo { get; set; }
    }
}