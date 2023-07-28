namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            LICH_SU = new HashSet<LICH_SU>();
        }

        [Key]
        public int ID_NHANVIEN { get; set; }

        [Required]
        [StringLength(10)]
        public string MANV { get; set; }

        [Required]
        [StringLength(40)]
        public string TENNV { get; set; }

        public DateTime NGAYSINH { get; set; }

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

        public bool? DAXOA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LICH_SU> LICH_SU { get; set; }
    }
}
