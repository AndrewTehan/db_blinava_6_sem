using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucking
{
    interface ISqlCrud
    {
        bool Delete(int id);

        void GetAll();

        int LastId();
    }
}
