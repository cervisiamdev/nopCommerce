using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.ShippingDiscount.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Misc.ShippingDiscount.Components;

[ViewComponent(Name = "DiscountShippingWidget")]
public class ShippingDiscountPlugin : NopViewComponent
{
    private readonly IWorkContext _workContext;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public ShippingDiscountPlugin(IWorkContext workContext, IStoreContext storeContext, ISettingService settingService)
    {
        _workContext = workContext;
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var shippingDiscountSettings = await _settingService.LoadSettingAsync<ShippingDiscountSettings>(storeId);
        
        var model = new ShippingDiscountConfigurationModel { Description = shippingDiscountSettings.Description };

        return View("~/Plugins/Misc.ShippingDiscount/Views/ShippingDiscount/ShippingDiscount.cshtml", model);
    }
}