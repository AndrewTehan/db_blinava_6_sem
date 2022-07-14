using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    abstract class SqlCrud : ISqlCrud
    {
        private static Db db;

        public Db _db
        {
            get { return db; }
            set { db = value; }
        }

        public abstract bool Delete(int id);
        public abstract void GetAll();
        public abstract int LastId();
    }
}
