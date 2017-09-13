using System;

namespace Assets.Scripts.Interfaces.Shop
{
    public interface IShopItem
    {
        Func<bool> HasEnoughCreditsToBuy();
        Action BuyItem();
        int GetPrice();
    }
}
