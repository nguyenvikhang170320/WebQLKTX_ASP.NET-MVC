using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class CanBoDAO
    {
        QLKyTucXaDbContext db = null;
        public CanBoDAO()
        {
            db = new QLKyTucXaDbContext();
        }
        public long Insert(CANBO entity)
        {
            db.CANBOes.Add(entity);
            db.SaveChanges();
            return entity.ID_CANBO;
        }

        public bool Update(CANBO entity)
        {
            try
            {
                var user = db.CANBOes.Find(entity.ID_CANBO);
                user.MACB = entity.MACB;
                user.TAIKHOAN = entity.TAIKHOAN;
                if (!string.IsNullOrEmpty(entity.MATKHAU))
                {
                    user.MATKHAU = entity.MATKHAU;
                }
                user.TENCB = entity.TENCB;
                user.CMND_CCCD = entity.CMND_CCCD;
                user.SDT = entity.SDT;
                user.GIOITINH = entity.GIOITINH;
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
                var user = db.CANBOes.Find(id);
                db.CANBOes.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public CANBO GetById(string userName)
        {
            return db.CANBOes.SingleOrDefault(x => x.TAIKHOAN == userName);
        }
        public int Login(string userName, string passWord)
        {
            var result = db.CANBOes.SingleOrDefault(x => x.TAIKHOAN == userName);
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