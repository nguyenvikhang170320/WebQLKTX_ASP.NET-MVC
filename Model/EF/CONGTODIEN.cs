namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CONGTODIEN")]
    public partial class CONGTODIEN
    {
        [Key]
        public int ID_DIEN { get; set; }

        public int ID_PHONG { get; set; }

        public int? CHISODAU { get; set; }

        public int? CHISOCUOI { get; set; }

        public int? THANG { get; set; }

        public int? NAM { get; set; }

        public int? TRANGTHAI { get; set; }

        public virtual PHONG PHONG { get; set; }
    }
}
