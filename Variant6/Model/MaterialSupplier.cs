namespace Variant6.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaterialSupplier")]
    public partial class MaterialSupplier
    {
        [Required]
        [StringLength(50)]
        public string MaterialID { get; set; }

        [Required]
        [StringLength(50)]
        public string SupplierID { get; set; }

        public int ID { get; set; }

        public virtual Material Material { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
