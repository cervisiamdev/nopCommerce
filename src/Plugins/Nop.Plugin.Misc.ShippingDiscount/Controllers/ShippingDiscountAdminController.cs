using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.ShippingDiscount.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.ShippingDiscount.Controllers;

public class ShippingDiscountAdminController : BasePluginController
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;
    private readonly INotificationService _notificationService;
    private readonly ILocalizationService _localizationService;
    
    public ShippingDiscountAdminController(ISettingService settingService, IStoreContext storeContext, INotificationService notificationService, ILocalizationService localizationService)
    {
        _settingService = settingService;
        _storeContext = storeContext;
        _notificationService = notificationService;
        _localizationService = localizationService;
    }
    
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [HttpGet]
    public async Task<IActionResult> Configure()
    {
        var model = new ShippingDiscountConfigurationModel();
        await PrepareModelAsync(model);
        
        return View("~/Plugins/Misc.ShippingDiscount/Views/Configure.cshtml", model);
    }
    
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [HttpPost, ActionName("Configure")]
    public async Task<IActionResult> Configure(ShippingDiscountConfigurationModel model)
    {
        var sss = "123";
        if (!ModelState.IsValid)
            return await Configure();
        
        var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var shippingDiscountSettings = await _settingService.LoadSettingAsync<ShippingDiscountSettings>(storeId);
        
        shippingDiscountSettings.Description = model.Description;
        await _settingService.SaveSettingAsync(shippingDiscountSettings, settings => settings.Description, clearCache: false);
        await _settingService.ClearCacheAsync();

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }

    private async Task PrepareModelAsync(ShippingDiscountConfigurationModel model)
    {
        var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var shippingDiscountSettings = await _settingService.LoadSettingAsync<ShippingDiscountSettings>(storeId);
        
        if (string.IsNullOrEmpty(shippingDiscountSettings.Description))
            return;
        
        model.Description = shippingDiscountSettings.Description;
    }
}