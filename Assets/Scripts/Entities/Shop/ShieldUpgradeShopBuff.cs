using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Interfaces.Shop;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Services;
using System;

namespace Assets.Scripts.Entities.Shop
{
    public class ShieldUpgradeShopBuff : ShopBuff, IShopBuff, ILanguageUI
    {

        public override Func<bool> CanIncreaseBuff()
        {
            return () => PlayerStatusManager.PlayerDataInstance.CanUpgradeShield();
        }

        public override Action BuyBuff()
        {
            return () => PlayerStatusManager.PlayerDataInstance.IncreaseShieldUpgrade();
        }

        public override void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                this.buffName = ld.shieldUpgradeItem;
            }
        }
    }
}
