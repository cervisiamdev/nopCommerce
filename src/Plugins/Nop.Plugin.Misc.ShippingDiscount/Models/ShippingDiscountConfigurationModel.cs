using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.ShippingDiscount.Models;

public record ShippingDiscountConfigurationModel : BaseNopModel
{
    public string Description { get; set; }
}