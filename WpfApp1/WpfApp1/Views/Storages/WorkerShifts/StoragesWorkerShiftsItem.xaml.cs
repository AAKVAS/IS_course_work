using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Storages;

namespace WpfApp1.Views.Storages.WorkerShifts
{
    public partial class StoragesWorkerShiftsItem: ItemForm
    {
        public StoragesWorkerShiftsItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (StoragesWorkerShiftsViewModel)_sectionWidgetViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void SetFormModeToInsert()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Сохранить";
        }

        protected override void SetFormModeToUpdate()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Изменить";
        }

        protected override void SetFormModeToRead()
        {
            btnDataAction.Visibility = Visibility.Collapsed;
            DisableAllInputs();
        }

        private void DisableAllInputs()
        {
            cbStorage.IsEnabled = false;
            cbWorker.IsEnabled = false;
            calendarFinishedShiftAt.IsEnabled = false;
            calendarStartedShiftAt.IsEnabled = false;
        }

    }
}
