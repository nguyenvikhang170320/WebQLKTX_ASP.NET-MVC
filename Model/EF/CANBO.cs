namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CANBO")]
    public partial class CANBO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CANBO()
        {
            HOADON_DIENNUOC = new HashSet<HOADON_DIENNUOC>();
        }

        [Key]
        public int ID_CANBO { get; set; }

        [Required]
        [StringLength(10)]
        public string MACB { get; set; }

        [Required]
        [StringLength(50)]
        public string TAIKHOAN { get; set; }

        [Required]
        [StringLength(50)]
        public string MATKHAU { get; set; }

        [Required]
        [StringLength(40)]
        public string TENCB { get; set; }

        public bool GIOITINH { get; set; }

        [Required]
        [StringLength(12)]
        public string CMND_CCCD { get; set; }

        [Required]
        [StringLength(50)]
        public string DIACHI { get; set; }

        [Required]
        [StringLength(40)]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(12)]
        public string SDT { get; set; }

        public bool QUANTRI { get; set; }

        public bool? DAXOA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON_DIENNUOC> HOADON_DIENNUOC { get; set; }
    }
}
