namespace DetMir
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderComposition")]
    public partial class OrderComposition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number_order { get; set; }

        [StringLength(10)]
        public string Article2 { get; set; }

        public int? Number_of_prices { get; set; }

        public virtual Order Order { get; set; }
    }
}
