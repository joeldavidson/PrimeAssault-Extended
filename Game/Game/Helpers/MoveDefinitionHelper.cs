using PrimeAssault.Models;

namespace PrimeAssault.Helpers
{
    class MoveDefinitionHelper
    {
        public static MoveEnum GetLocationByPosition(int moveNum)
        {
            switch (moveNum)
            {
                case 1:
                    return MoveEnum.Crackshot;

                case 2:
                default:
                    return MoveEnum.Iron_Grip;
            }
        }
    }
}
