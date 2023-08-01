namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HOADON_DIENNUOC
    {
        [Key]
        public int ID_HOADONDIENNUOC { get; set; }

        public int ID_CANBO { get; set; }

        public int ID_PHONG { get; set; }

        public int ID_DONGIA { get; set; }

        public int THANG { get; set; }

        public int NAM { get; set; }

        public int TRANGTHAI { get; set; }

        public virtual CANBO CANBO { get; set; }

        public virtual DONGIA DONGIA { get; set; }

        public virtual PHONG PHONG { get; set; }
    }
}
