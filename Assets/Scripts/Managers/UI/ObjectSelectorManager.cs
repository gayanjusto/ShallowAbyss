using Assets.Scripts.Enums;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers.UI
{
    public class ObjectSelectorManager : MonoBehaviour
    {
        ShopSceneManager shopSceneManager;
        ShipSelectorManager shipSelectorManager;
        Action<ShopSelectedObjectEnum, GameObject, string> selectedObjectAction;

        private void Start()
        {
            if (IsInShopScene())
            {
                shopSceneManager = GameObject.Find("ShopSceneManager").GetComponent<ShopSceneManager>();
                selectedObjectAction = shopSceneManager.SetSelectedObject;
            }else if (IsInShipSelectorScene())
            {
                shipSelectorManager = GameObject.Find("ShipSelectorManager").GetComponent<ShipSelectorManager>();
                selectedObjectAction = shipSelectorManager.SetSelectedObject;
            }
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            selectedObjectAction(selectedObj, selectedGameObj, objectName);
        }

        bool IsInShopScene()
        {
            return SceneManager.GetActiveScene().name.ToUpper().Contains("SHOP");
        }

        bool IsInShipSelectorScene()
        {
            return SceneManager.GetActiveScene().name.ToUpper().Contains("SELECTOR");
        }
    }
}
