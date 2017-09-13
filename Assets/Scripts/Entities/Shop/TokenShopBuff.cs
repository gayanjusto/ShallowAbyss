using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Interfaces.Shop;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Services;
using System;

namespace Assets.Scripts.Entities.Shop
{
    public class TokenShopBuff : ShopBuff, IShopItem, ILanguageUI
    {
        public override Func<bool> HasEnoughCreditsToBuy()
        {
            return () => true;
        }

        public override Action BuyItem()
        {
            return () => PlayerStatusManager.PlayerDataInstance.IncreaseJackpotTokens(1);

        }

        public override void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                this.buffName = ld.tokenItem;
            }
        }
    }
}
