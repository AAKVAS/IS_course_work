using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;
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

        public SectionWidget GetSectionWidget(string sectionKey)
        {
            SectionWidget sectionWidget = new SectionWidget(sectionKey);
            SetSectionWidgetViewModel(sectionWidget);

            return sectionWidget;
        }

        private void SetSectionWidgetViewModel(SectionWidget sectionWidget)
        {
            sectionWidget.SetViewModelAndInitialize(GetSectionWidgetViewModelBySectionKey(sectionWidget));
        }

        private SectionWidgetViewModel GetSectionWidgetViewModelBySectionKey(SectionWidget sectionWidget)
        {
            switch(sectionWidget.SectionKey)
            {
                case "users_general_info":
                    return new ViewModels.Users.GeneralInfo.UsersGeneralInfoViewModel(sectionWidget);
                /*
                 case "users_general_info":
                    return new SectionWidgetViewModel();
                case "users_avg_cost": 
                    return new SectionWidgetViewModel();
                case "users_deffered_products": 
                    return new SectionWidgetViewModel();
                case "order_list":
                    return new SectionWidgetViewModel();
                case "orders_ready_to_receive":
                    return new SectionWidgetViewModel();
                case "order_history":
                    return new SectionWidgetViewModel();
                case "storages_receipts":
                    return new SectionWidgetViewModel();
                case "storages_worker_shifts":
                    return new SectionWidgetViewModel();
                case "storages_product_amount":
                    return new SectionWidgetViewModel();
                case "products_general_info": 
                    return new SectionWidgetViewModel();
                case "products_reviews":
                    return new SectionWidgetViewModel();
                case "products_categories": 
                    return new SectionWidgetViewModel();
                case "products_price_history": 
                    return new SectionWidgetViewModel();
                case "suppliers_general_info":
                    return new SectionWidgetViewModel();
                case "suppliers_profit": 
                    return new SectionWidgetViewModel();
                case "workers_list": 
                    return new SectionWidgetViewModel();
                case "storages_general_info":
                    return new SectionWidgetViewModel();
                */
                default:
                    return null;

            }
        }

        

    }
}
