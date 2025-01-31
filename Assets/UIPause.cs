using Dungeons.Infrastructure;
using Dungeons.Services;
using UnityEngine;

namespace Dungeons
{
    public class UIPause : MonoBehaviour
    {
        public void OnContinueClicked()
        {
            
        }

        public void OnSettingsClicked()
        {
            
        }

        public void OnExitClicked()
        {
            var saver = Locator<SaveSystem>.Instance;
            saver.Save();
        }
    }
}
