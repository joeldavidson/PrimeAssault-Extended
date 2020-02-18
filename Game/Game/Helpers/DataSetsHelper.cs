using Game.ViewModels;

namespace Game.Helpers
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
