namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HOADON_PHONG
    {
        public int ID_PHONG { get; set; }

        [Key]
        public int ID_HOADONPHONG { get; set; }

        public int NAM { get; set; }

        public int KY { get; set; }

        public int? TRANGTHAI { get; set; }

        public virtual PHONG PHONG { get; set; }
    }
}
