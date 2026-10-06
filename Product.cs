namespace DetMir
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [StringLength(50)]
        public string Article { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Unit { get; set; }

        public float? Stoimost { get; set; }

        public float? Max_discount { get; set; }

        public int? Manufacture2 { get; set; }

        public int? Provide2 { get; set; }

        public int? Number_category { get; set; }

        public float? Discount { get; set; }

        public int? Quantity_in_stock { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Photo { get; set; }

        public virtual Manufacture Manufacture { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual Provider Provider { get; set; }

        public string SkidkaStyle
        {
            get
            {
                if (Discount > 15)
                {
                    return "#7fff00";
                }
                else
                {
                    return "Azure";
                }
            }
        }
        public string TextDiscount
        {
            get
            {
                if (Discount == 0)
                {
                    return null;
                }
                else
                {
                    return (Stoimost-Stoimost*Discount/100).ToString();
                }
            }
        }

        public string oldCoststyle
        {
            get
            {
                if (TextDiscount == null)
                {
                    return "";
                }
                else
                {
                    return "Strikethtough";
                }
            }
        }

        public string fixImg
        {
            get
            {
                if (Photo == "")
                {
                    return "../Resource/picture.png";
                }
                else
                {
                    return Photo;
                }
            }
        }
    }


    
}
