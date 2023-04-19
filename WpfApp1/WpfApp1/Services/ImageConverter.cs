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
    /// <summary>
    /// Класс, предоставляющий возможность формировать ImageSource на основе массива байтов.
    /// </summary>
    public class ImageConverter
    {
        /// <summary>
        /// Метод, создающий ImageSource для изображения в виде массива байтов. В качестве параметра принимает массив байтов.
        /// </summary>
        /// <param name="img">Изображение в виде массива байтов, для которого нужно создать ImageSource.</param>
        /// <returns>Объект класса ImageSource.</returns>
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
