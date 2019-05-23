using System;
using Puppy.Model.Message;

namespace Puppy.Model.Facade
{
    public class UserAgentModel : IModel
    {
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string OS { get; set; }
        public string OSVersion{ get; set; }
        public string Device {get; set;}
        public string DeviceBrand {get; set;}

        public string DeviceModel {get;set;}

    }
}