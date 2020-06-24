using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hanabi.Flow.Common.Helpers
{
    public class AppSettingsWrapper
    {
        private AppSettings _settings;

        public AppSettingsWrapper(IOptionsMonitor<AppSettings> settings)
        {
            _settings = settings.CurrentValue;

            // Hook in on the OnChange event of the monitor
            settings.OnChange(Listener);
        }

        private void Listener(AppSettings settings)
        {
            _settings = settings;
        }
    }
}
