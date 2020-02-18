using PrimeAssault.ViewModels;

namespace PrimeAssault.Helpers
{
    static public class DataSetsHelper
    {
        static public void WarmUp()
        {
            ScoreIndexViewModel.Instance.GetCurrentDataSource();
            ItemIndexViewModel.Instance.GetCurrentDataSource();
        }
    }
}
