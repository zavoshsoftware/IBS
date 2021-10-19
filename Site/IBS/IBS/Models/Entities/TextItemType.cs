using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class TextItemType : BaseEntity
    {
        public TextItemType()
        {
            TextItems = new List<TextItem>();
        }
        public string Title { get; set; }

        public string Name { get; set; }
        public virtual ICollection<TextItem> TextItems { get; set; }
    }
}