namespace Variant6.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaterialCountHistory")]
    public partial class MaterialCountHistory
    {
        [StringLength(8)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MaterialID { get; set; }

        public DateTime ChangeDate { get; set; }

        public double CountValue { get; set; }
    }
}
