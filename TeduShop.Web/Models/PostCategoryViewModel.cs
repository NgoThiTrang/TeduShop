using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class PostCategoryViewModel
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; } // cái này k đuợc null
        public int? ParentID { get; set; }
        public int? DisplayOder { get; set; }

        public string Description { set; get; }

        public string Image { get; set; }
        public bool? HomeFlag { get; set; }
        public virtual IEnumerable<PostViewModel> Posts { set; get; }
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }
        public bool Status { get; set; }
    }
}