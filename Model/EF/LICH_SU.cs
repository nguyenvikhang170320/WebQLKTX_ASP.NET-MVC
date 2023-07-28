namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LICH_SU
    {
        public int ID { get; set; }

        public int ID_NHANVIEN { get; set; }

        public int ID_PHONG { get; set; }

        public DateTime? NGAYCHUYEN { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual PHONG PHONG { get; set; }
    }
}
