using Assets.Scripts.Enums;

namespace Assets.Scripts.Interfaces.Shop
{
    public interface IShopSelectedObject
    {
        string GetName();
        void SelectObject();
        ShopSelectedObjectEnum GetObjectType();
        void DeselectObject();
    }
}
