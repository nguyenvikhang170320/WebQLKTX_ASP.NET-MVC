namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOTRO")]
    public partial class HOTRO
    {
        [Key]
        public int ID_PHIEU { get; set; }

        public int ID_PHONG { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string NOIDUNG { get; set; }

        public DateTime? NGAYGUI { get; set; }

        public int? TRANGTHAI { get; set; }

        public virtual PHONG PHONG { get; set; }
    }
}
