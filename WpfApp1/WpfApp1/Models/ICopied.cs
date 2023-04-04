using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface ICopied<T>
    {
        public void Copy(T t);

        public T Clone();
    }
}
