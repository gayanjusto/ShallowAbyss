using System;

namespace Assets.Scripts.Interfaces.Shop
{
    public interface IShopCharacterSkin
    {
        int GetId();
        void DisableCharacterButton();
        Func<bool> AlreadyHasShip();
    }
}
