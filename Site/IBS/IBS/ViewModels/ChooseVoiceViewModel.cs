using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ChooseVoiceViewModel
    {
        public List<ChooseVoiceItemViewModel> Audios { get; set; }
        public bool NeedTherapy { get; set; }
    }
    public class ChooseVoiceItemViewModel
    {
        public AudioGroup AudioGroup { get; set; }
        public List<Audio> Audios { get; set; }
        public Guid AudioId1 { get; set; }
        public Guid AudioId2 { get; set; }
        public Guid AudioId3 { get; set; }
        public Guid AudioId4 { get; set; }
    } 
}