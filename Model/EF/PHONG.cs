using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EF
{
    [Table("PHONG")]
    public partial class PHONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONG()
        {
            CONGTODIENs = new HashSet<CONGTODIEN>();
            CONGTONUOCs = new HashSet<CONGTONUOC>();
            HOADON_DIENNUOC = new HashSet<HOADON_DIENNUOC>();
            HOADON_PHONG = new HashSet<HOADON_PHONG>();
            HOTROes = new HashSet<HOTRO>();
            LICH_SU = new HashSet<LICH_SU>();
        }

        [Key]
        public int ID_PHONG { get; set; }

        public int ID_DAY { get; set; }

        [Required]
        [StringLength(10)]
        public string MAPHONG { get; set; }

        [Required]
        [StringLength(30)]
        public string TAIKHOAN { get; set; }

        [Required]
        [StringLength(50)]
        public string MATKHAU { get; set; }

        public int? SOLUONGNV { get; set; }

        public double? DONGIA { get; set; }

        public int? TINHTRANG { get; set; }

        public bool? TRANGTHAI { get; set; }
        [Required]
        [StringLength(50)]
        public string MOTAKHAC { get; set; }

        public bool? DAXOA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONGTODIEN> CONGTODIENs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONGTONUOC> CONGTONUOCs { get; set; }

        public virtual DAYPHONG DAYPHONG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON_DIENNUOC> HOADON_DIENNUOC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON_PHONG> HOADON_PHONG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOTRO> HOTROes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LICH_SU> LICH_SU { get; set; }
    }
}
