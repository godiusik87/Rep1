namespace DetMir
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number_order { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_order { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_delively { get; set; }

        public int? ID_item { get; set; }

        public int? ID_client { get; set; }

        public int? Code_to_get { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public virtual Point Point { get; set; }

        public virtual User User { get; set; }

        public virtual OrderComposition OrderComposition { get; set; }
    }
}
