using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.ScheduleTasks;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Plugins;
using Nop.Services.ScheduleTasks;
using Nop.Services.Stores;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Misc.ShippingDiscount
{
    public class ShippingDiscountPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public ShippingDiscountPlugin(ISettingService settingService, IWebHelper webHelper)
        {
            _settingService = settingService;
            _webHelper = webHelper;
        }

        public bool HideInWidgetList { get; } = false;

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                AdminWidgetZones.CustomerDetailsBlock,
                PublicWidgetZones.ProductDetailsInsideOverviewButtonsBefore,
            });
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "ShippingDiscountWidget";
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/ShippingDiscountAdmin/Configure";
        }
        
        public override async Task InstallAsync()
        {
            await _settingService.SaveSettingAsync(new ShippingDiscountSettings { Description = string.Empty });
        
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<ShippingDiscountSettings>();

            await base.UninstallAsync();
        }
        
    }
}
