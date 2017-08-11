using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interfaces.UI
{
    public interface IObjectSelector
    {
        ShopSelectedObjectEnum ShopSelectedObjectEnum { get; set; }
        GameObject SelectedObject { get; set; }
        Text SelectedObjectText { get; set; }
    }
}
