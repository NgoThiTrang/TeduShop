using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Alias { get; set; }

        public int CategoryID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }
        public bool Status { get; set; }
        public decimal OriginalPrice { set; get; }
        public virtual ProductCategoryViewModel ProductCategory { get; set; }
        public virtual IEnumerable<ProductTagViewModel> ProductTags { get; set; }
        [Required]
        public decimal Price { get;  set; }
        public string MoreImages { get;  set; }
        public decimal? PromotionPrice { get;  set; }
        public int? Warranty { get;  set; }
        public object UpdatesTime { get;  set; }
        public string Tags { get;  set; }
        public int  Quantity { get; set; }

    }
}