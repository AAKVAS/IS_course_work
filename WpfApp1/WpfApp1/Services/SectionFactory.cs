using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Views;

namespace WpfApp1.Services
{
    internal class SectionFactory
    {
        AccessService _accessService;

        public SectionFactory(AccessService accessService)
        {
            _accessService = accessService;
        }

        //TODO: сделать невидимые кнопочки, если нет прав
        public SectionWidget GetSectionWidget(string sectionKey)
        {
            //List<SectionRights> sectionRights = _accessService.GetSectionRightsBySectionKey(sectionKey);

            SectionWidget sectionWidget = new SectionWidget();
            return sectionWidget;
        }
    }
}
