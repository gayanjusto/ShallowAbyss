using System;

namespace Assets.Scripts.Interfaces.Shop
{
    public interface IShopItem
    {
        Func<bool> HasReachedItemMax();
        Action BuyItem();
        int GetPrice();
    }
}
