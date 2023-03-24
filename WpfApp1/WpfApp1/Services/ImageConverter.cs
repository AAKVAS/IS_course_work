using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1.Services
{
    public class ImageConverter
    {
        public static ImageSource ByteArrayToImage(byte[] img)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(img);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            return biImg as ImageSource;
        }
    }
}
