using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    /// <summary>
    /// Интерфейс копирования объектов.
    /// Используется в модели представления SectionWidgetViewModel для копирования записи.
    /// </summary>
    /// <typeparam name="T">Класс копируемого объекта.</typeparam>
    public interface ICopied<T>
    {
        /// <summary>
        /// Метод копирования данных из переданного объекта в текущий.
        /// </summary>
        /// <param name="t">Объект, чьи данные нужно скопировать в текущий.</param>
        public void Copy(T t);

        /// <summary>
        /// Метод создающий копию текущего объекта.
        /// </summary>
        /// <returns>Копия текущего объекта.</returns>
        public T Clone();
    }
}
