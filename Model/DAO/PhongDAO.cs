using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class PhongDAO
    {
        QLKyTucXaDbContext db = null;
        public PhongDAO()
        {
            db = new QLKyTucXaDbContext();
        }
        public long Insert(PHONG entity)
        {
            db.PHONGs.Add(entity);
            db.SaveChanges();
            return entity.ID_PHONG;
        }

        public bool Update(PHONG entity)
        {
            try
            {
                var user = db.PHONGs.Find(entity.ID_PHONG);
                user.MAPHONG = entity.MAPHONG;
                user.DAYPHONG = entity.DAYPHONG;
                user.TAIKHOAN = entity.TAIKHOAN;
                if (!string.IsNullOrEmpty(entity.MATKHAU))
                {
                    user.MATKHAU = entity.MATKHAU;
                }
                user.SOLUONGNV = entity.SOLUONGNV;
                user.TINHTRANG = entity.TINHTRANG;
                user.MOTAKHAC = entity.MOTAKHAC;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.PHONGs.Find(id);
                db.PHONGs.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public PHONG GetById(string userName)
        {
            return db.PHONGs.SingleOrDefault(x => x.TAIKHOAN == userName);
        }
        public int Login(string userName, string passWord)
        {
            var result = db.PHONGs.SingleOrDefault(x => x.TAIKHOAN == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.DAXOA == true)
                {
                    return -2;
                }
                else
                {
                    if (result.MATKHAU == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}
