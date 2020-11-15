using Presently.MobileApp.Common.Constants;
using Xamarin.Forms;

namespace Presently.MobileApp.Effects
{
    public class NoBorderEffect : RoutingEffect
    {
        public NoBorderEffect() : base($"{AppConstants.EffectsNamespace}.{nameof(NoBorderEffect)}")
        {

        }
    }
}
