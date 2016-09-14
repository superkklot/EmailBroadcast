using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBroadcast
{
    public static class ConfigHelper
    {
        private const int _defaultUnitToAddressNumber = 20;
        private const int _defaultWaitSeconds = 30;
        public static int GetUnitToAddressNumber()
        {
            var unitToAddress = ConfigurationManager.AppSettings.Get("UnitToAddressNumber");
            if (unitToAddress == null)
                return _defaultUnitToAddressNumber;
            int intUnitToAddress = _defaultUnitToAddressNumber;
            int.TryParse(unitToAddress, out intUnitToAddress);
            return intUnitToAddress;
        }

        public static int GetWaitSeconds()
        {
            var waitSeconds = ConfigurationManager.AppSettings.Get("WaitSeconds");
            if (waitSeconds == null)
                return _defaultWaitSeconds;
            int intwaitSeconds = _defaultWaitSeconds;
            int.TryParse(waitSeconds, out intwaitSeconds);
            return intwaitSeconds;
        }
    }
}
