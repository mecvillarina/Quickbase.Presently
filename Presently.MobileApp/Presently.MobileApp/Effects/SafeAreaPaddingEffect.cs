using Presently.MobileApp.Common.Constants;
using Xamarin.Forms;

namespace Presently.MobileApp.Effects
{
    public class SafeAreaPaddingEffect : RoutingEffect
    {
        public SafeAreaPaddingEffect() : base($"{AppConstants.EffectsNamespace}.{nameof(SafeAreaPaddingEffect)}")
        {

        }
    }
}
