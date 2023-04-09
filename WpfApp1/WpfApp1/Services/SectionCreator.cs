using WpfApp1.Models;
using WpfApp1.Views;


namespace WpfApp1.Services
{
    public class SectionCreator
    {

        public SectionWidget GetSectionWidget(Sections section)
        {
            SectionWidget sectionWidget = null;
            switch (section.SectionKey)
            {
                case "users_general_info":
                    return new UserGeneralInfoSectionWidget(section);
                case "users_avg_cost": 
                    return new UserAvgCostSectionWidget(section);
                case "users_deffered_products": 
                    return new UserDefferedProductsSectionWidget(section);
                case "order_list":
                    return new OrdersListSectionWidget(section);
                case "orders_ready_to_receive":
                    return new OrdersReadyToReceiveSectionWidget(section);
                case "order_history":
                      return new OrderHistorySectionWidget(section);
                    /*
              case "storages_general_info":
                  return new SectionWidgetViewModel();
              case "storages_receipts":
                  return new SectionWidgetViewModel();
              case "storages_worker_shifts":
                  return new SectionWidgetViewModel();
              case "storages_product_amount":
                  return new SectionWidgetViewModel();
                */
                case "products_general_info": 
                      return new ProductsGeneralInfoSectionWidget(section);
                  case "products_reviews":
                      return new ProductsReviewsSectionWidget(section);
                  case "products_categories": 
                      return new ProductsCategoriesSectionWidget(section);
                    /*
                  case "products_price_history": 
                      return new SectionWidgetViewModel();
                  case "suppliers_general_info":
                      return new SectionWidgetViewModel();
                  case "suppliers_profit": 
                      return new SectionWidgetViewModel();
                    */
                  case "workers_list": 
                      return new WorkersListSectionWidget(section);
            }

            return sectionWidget;
        }

    }
}
