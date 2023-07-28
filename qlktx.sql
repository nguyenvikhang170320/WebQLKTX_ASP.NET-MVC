/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     7/11/2023 10:37:04 PM                        */
/*==============================================================*/
use QLKTX;
go


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CONGTODIEN') and o.name = 'FK_CONGTODI_CONGTODIE_PHONG')
alter table CONGTODIEN
   drop constraint FK_CONGTODI_CONGTODIE_PHONG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CONGTONUOC') and o.name = 'FK_CONGTONU_CONGTONUO_PHONG')
alter table CONGTONUOC
   drop constraint FK_CONGTONU_CONGTONUO_PHONG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON_DIENNUOC') and o.name = 'FK_HOADON_D_CANBOTAOH_CANBO')
alter table HOADON_DIENNUOC
   drop constraint FK_HOADON_D_CANBOTAOH_CANBO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON_DIENNUOC') and o.name = 'FK_HOADON_D_HOADONDIE_PHONG')
alter table HOADON_DIENNUOC
   drop constraint FK_HOADON_D_HOADONDIE_PHONG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON_DIENNUOC') and o.name = 'FK_HOADON_D_HOADONDIE_DONGIA')
alter table HOADON_DIENNUOC
   drop constraint FK_HOADON_D_HOADONDIE_DONGIA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOTRO') and o.name = 'FK_HOTRO_HOADONPHO_PHONG')
alter table HOTRO
   drop constraint FK_HOTRO_HOADONPHO_PHONG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LICH_SU') and o.name = 'FK_LICH_SU_LICH_SU_NHANVIEN')
alter table LICH_SU
   drop constraint FK_LICH_SU_LICH_SU_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LICH_SU') and o.name = 'FK_LICH_SU_LICH_SU2_PHONG')
alter table LICH_SU
   drop constraint FK_LICH_SU_LICH_SU2_PHONG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHONG') and o.name = 'FK_PHONG_PHONGTHUO_DAYPHONG')
alter table PHONG
   drop constraint FK_PHONG_PHONGTHUO_DAYPHONG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CANBO')
            and   type = 'U')
   drop table CANBO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CONGTODIEN')
            and   name  = 'CONGTODIENCUAPHONG_FK'
            and   indid > 0
            and   indid < 255)
   drop index CONGTODIEN.CONGTODIENCUAPHONG_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CONGTODIEN')
            and   type = 'U')
   drop table CONGTODIEN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CONGTONUOC')
            and   name  = 'CONGTONUOCCUAPHONG_FK'
            and   indid > 0
            and   indid < 255)
   drop index CONGTONUOC.CONGTONUOCCUAPHONG_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CONGTONUOC')
            and   type = 'U')
   drop table CONGTONUOC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DAYPHONG')
            and   type = 'U')
   drop table DAYPHONG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DONGIA')
            and   type = 'U')
   drop table DONGIA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON_DIENNUOC')
            and   name  = 'HOADONDIENNUOCTHEODONGIA_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON_DIENNUOC.HOADONDIENNUOCTHEODONGIA_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON_DIENNUOC')
            and   name  = 'CANBOTAOHOADON_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON_DIENNUOC.CANBOTAOHOADON_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON_DIENNUOC')
            and   name  = 'HOADONDIENNUOCCUAPHONG_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON_DIENNUOC.HOADONDIENNUOCCUAPHONG_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADON_DIENNUOC')
            and   type = 'U')
   drop table HOADON_DIENNUOC
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOTRO')
            and   name  = 'HOADONPHONG_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOTRO.HOADONPHONG_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOTRO')
            and   type = 'U')
   drop table HOTRO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LICH_SU')
            and   name  = 'LICH_SU2_FK'
            and   indid > 0
            and   indid < 255)
   drop index LICH_SU.LICH_SU2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LICH_SU')
            and   name  = 'LICH_SU_FK'
            and   indid > 0
            and   indid < 255)
   drop index LICH_SU.LICH_SU_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LICH_SU')
            and   type = 'U')
   drop table LICH_SU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHANVIEN')
            and   type = 'U')
   drop table NHANVIEN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHONG')
            and   name  = 'PHONGTHUOCDAY_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHONG.PHONGTHUOCDAY_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHONG')
            and   type = 'U')
   drop table PHONG
go

/*==============================================================*/
/* Table: CANBO                                                 */
/*==============================================================*/
create table CANBO (
   ID_CANBO             int         IDENTITY(1,1),
   MACB                 varchar(10)          not null,
   TAIKHOAN             varchar(50)          not null,
   MATKHAU              varchar(50)          not null,
   TENCB                varchar(40)          not null,
   GIOITINH             bit					 not null,
   CMND_CCCD            varchar(12)          not null,
   DIACHI               varchar(50)          not null,
   EMAIL                varchar(40)          not null,
   SDT                  varchar(12)          not null,
   QUANTRI				bit					 not null, 
   DAXOA				bit					 null,
   constraint PK_CANBO primary key nonclustered (ID_CANBO)
)
go

/*==============================================================*/
/* Table: CONGTODIEN                                            */
/*==============================================================*/
create table CONGTODIEN (
   ID_DIEN              int                  IDENTITY(1,1),
   ID_PHONG             int                  not null,
   CHISODAU             int                  not null,
   CHISOCUOI            int                  not null,
   THANG                int                  not null,
   NAM                  int                  not null,
   TRANGTHAI            varchar(50)          not null,
   constraint PK_CONGTODIEN primary key nonclustered (ID_DIEN)
)
go

/*==============================================================*/
/* Index: CONGTODIENCUAPHONG_FK                                 */
/*==============================================================*/
create index CONGTODIENCUAPHONG_FK on CONGTODIEN (
ID_PHONG ASC
)
go

/*==============================================================*/
/* Table: CONGTONUOC                                            */
/*==============================================================*/
create table CONGTONUOC (
   ID_NUOC              int                  IDENTITY(1,1),
   ID_PHONG             int                  not null,
   CHISODAU             int                  not null,
   CHISOCUOI            int                  not null,
   THANG                int                  not null,
   NAM                  int                  not null,
   TRANGTHAI            varchar(50)          not null
   constraint PK_CONGTONUOC primary key nonclustered (ID_NUOC)
)
go

/*==============================================================*/
/* Index: CONGTONUOCCUAPHONG_FK                                 */
/*==============================================================*/
create index CONGTONUOCCUAPHONG_FK on CONGTONUOC (
ID_PHONG ASC
)
go

/*==============================================================*/
/* Table: DAYPHONG                                              */
/*==============================================================*/
create table DAYPHONG (
   ID_DAY               int                  IDENTITY(1,1),
   MADAYPHONG           varchar(10)          not null,
   DAXOA				bit					 null,
   constraint PK_DAYPHONG primary key nonclustered (ID_DAY)
)
go

/*==============================================================*/
/* Table: DONGIA                                                */
/*==============================================================*/
create table DONGIA (
   ID_DONGIA            int                  IDENTITY(1,1),
   MADONGIA				varchar(10)          not null,
   DONGIADIEN           float                not null,
   DONGIANUOC           float                not null,
   TRANGTHAI            bit                  not null,
   NGAYAPDUNG           datetime             not null,
   DAXOA				bit					 null,
   constraint PK_DONGIA primary key nonclustered (ID_DONGIA)
)
go

/*==============================================================*/
/* Table: HOADON_DIENNUOC                                       */
/*==============================================================*/
create table HOADON_DIENNUOC (
   ID_HOADONDIENNUOC    int                  IDENTITY(1,1),
   ID_CANBO             int                  not null,
   ID_PHONG             int                  not null,
   ID_DONGIA            int                  not null,
   THANG                int                  null,
   NAM                  int                  null,
   TRANGTHAI            int                  null,
   constraint PK_HOADON_DIENNUOC primary key nonclustered (ID_HOADONDIENNUOC)
)
go

create table HOADON_PHONG (
   ID_HOADONPHONG	   int                  IDENTITY(1,1),
   ID_PHONG            int                  not null,
   NAM                 int                  null,
   KY                  int                  null,
   TRANGTHAI           int                  null,
   constraint PK_HOADON_PHONG primary key nonclustered (ID_HOADONPHONG)
)
go

/*==============================================================*/
/* Index: HOADONPHONGCUAPHONG_FK                             */
/*==============================================================*/
create index HOADONPHONGCUAPHONG_FK on HOADON_PHONG (
ID_PHONG ASC
)
go
/*==============================================================*/
/* Index: HOADONDIENNUOCCUAPHONG_FK                             */
/*==============================================================*/
create index HOADONDIENNUOCCUAPHONG_FK on HOADON_DIENNUOC (
ID_PHONG ASC
)
go

/*==============================================================*/
/* Index: CANBOTAOHOADON_FK                                     */
/*==============================================================*/
create index CANBOTAOHOADON_FK on HOADON_DIENNUOC (
ID_CANBO ASC
)
go

/*==============================================================*/
/* Index: HOADONDIENNUOCTHEODONGIA_FK                           */
/*==============================================================*/
create index HOADONDIENNUOCTHEODONGIA_FK on HOADON_DIENNUOC (
ID_DONGIA ASC
)
go

/*==============================================================*/
/* Table: HOTRO                                                 */
/*==============================================================*/
create table HOTRO (
   ID_PHIEU             int                  IDENTITY(1,1),
   ID_PHONG             int                  not null,
   NOIDUNG              text                 not null,
   NGAYGUI              datetime             null,
   TRANGTHAI            int                  null,
   constraint PK_HOTRO primary key nonclustered (ID_PHIEU)
)
go

/*==============================================================*/
/* Index: HOADONPHONG_FK                                        */
/*==============================================================*/
create index HOADONPHONG_FK on HOTRO (
ID_PHONG ASC
)
go

/*==============================================================*/
/* Table: LICH_SU                                               */
/*==============================================================*/
create table LICH_SU (
	ID					int					IDENTITY(1,1),
   ID_NHANVIEN          int                  not null,
   ID_PHONG             int                  not null,
   NGAYCHUYEN           datetime                null,
   constraint PK_LICH_SU primary key (ID_NHANVIEN, ID_PHONG)
)
go

/*==============================================================*/
/* Index: LICH_SU_FK                                            */
/*==============================================================*/
create index LICH_SU_FK on LICH_SU (
ID_NHANVIEN ASC
)
go

/*==============================================================*/
/* Index: LICH_SU2_FK                                           */
/*==============================================================*/
create index LICH_SU2_FK on LICH_SU (
ID_PHONG ASC
)
go

/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create table NHANVIEN (
   ID_NHANVIEN          int                  not null IDENTITY(1,1),
   MANV                 varchar(20)          not null,
   TENNV                varchar(50)          not null,
   NGAYSINH             datetime             not null,
   GIOITINH             bit                  not null,
   CMND_CCCD            varchar(20)          not null,
   DIACHI               varchar(50)          not null,
   EMAIL                varchar(40)          not null,
   SDT                  varchar(12)          not null,
   DAXOA                bit                  null
   constraint PK_NHANVIEN primary key nonclustered (ID_NHANVIEN)
)
go

/*==============================================================*/
/* Table: PHONG                                                 */
/*==============================================================*/
create table PHONG (
   ID_PHONG             int                  IDENTITY(1,1),
   ID_DAY               int                  not null,
   MAPHONG              varchar(10)          not null,
   TAIKHOAN             varchar(50)          not null,
   MATKHAU              varchar(50)          not null,
   SOLUONGNV           int                   null,
   DONGIA               float                null,
   TINHTRANG            int					 null,
   TRANGTHAI            bit					 null,
   MOTAKHAC             varchar(50)          not null,
   DAXOA				bit					 null,
   constraint PK_PHONG primary key nonclustered (ID_PHONG)
)
go

/*==============================================================*/
/* Index: PHONGTHUOCDAY_FK                                      */
/*==============================================================*/
create index PHONGTHUOCDAY_FK on PHONG (
ID_DAY ASC
)
go

alter table CONGTODIEN
   add constraint FK_CONGTODI_CONGTODIE_PHONG foreign key (ID_PHONG)
      references PHONG (ID_PHONG)
go

alter table CONGTONUOC
   add constraint FK_CONGTONU_CONGTONUO_PHONG foreign key (ID_PHONG)
      references PHONG (ID_PHONG)
go

alter table HOADON_DIENNUOC
   add constraint FK_HOADON_D_CANBOTAOH_CANBO foreign key (ID_CANBO)
      references CANBO (ID_CANBO)
go

alter table HOADON_DIENNUOC
   add constraint FK_HOADON_D_HOADONDIE_PHONG foreign key (ID_PHONG)
      references PHONG (ID_PHONG)
go

alter table HOADON_DIENNUOC
   add constraint FK_HOADON_D_HOADONDIE_DONGIA foreign key (ID_DONGIA)
      references DONGIA (ID_DONGIA)
go

alter table HOTRO
   add constraint FK_HOTRO_HOADONPHO_PHONG foreign key (ID_PHONG)
      references PHONG (ID_PHONG)
go

alter table LICH_SU
   add constraint FK_LICH_SU_LICH_SU_NHANVIEN foreign key (ID_NHANVIEN)
      references NHANVIEN (ID_NHANVIEN)
go

alter table LICH_SU
   add constraint FK_LICH_SU_LICH_SU2_PHONG foreign key (ID_PHONG)
      references PHONG (ID_PHONG)
go

alter table PHONG
   add constraint FK_PHONG_PHONGTHUO_DAYPHONG foreign key (ID_DAY)
      references DAYPHONG (ID_DAY)
go

