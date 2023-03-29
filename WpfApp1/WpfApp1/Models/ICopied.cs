using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface ICopied<T> : ICloneable
    {
        public void Copy(T t);
    }
}
