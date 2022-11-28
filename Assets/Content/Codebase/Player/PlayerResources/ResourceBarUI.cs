using System.Globalization;
using TMPro;
using UnityEngine;

namespace Woodman.Player.PlayerResources
{
    public class ResourceBarUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void SetCoinsCount(int count)
        {
            if(count < 1000)
                _text.text = count.ToString(CultureInfo.InvariantCulture);
            else
                _text.text = (count / 1000).ToString(CultureInfo.InvariantCulture) + "k";
        }

        public void SetLogsCount(int count)
        {
            if(count < 1000) 
                _text.text = count.ToString("000");
            else if (count < 100000)
                _text.text = count.ToString(CultureInfo.InvariantCulture);
            else
                _text.text = (count / 1000).ToString(CultureInfo.InvariantCulture) + "k";
        }
    }
}