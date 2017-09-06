using Assets.Scripts.Entities.Player;
using System;

namespace Assets.Scripts.Interfaces.Shop
{
    public interface IShopBuff
    {
        Func<bool> CanIncreaseBuff();
        Action BuyBuff();
    }
}
