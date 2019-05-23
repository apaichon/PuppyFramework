using System;
using UAParser;
using Puppy.Model;
using Puppy.Model.Facade;

namespace Puppy.Utils
{
    public class UserAgent
    {
        public UserAgent()
        {
           
        }

        public static IModel Parse(string uaString)
        {
            Parser  _uaParser = Parser.GetDefault();
            ClientInfo c =_uaParser.Parse(uaString);
            return new UserAgentModel 
            {
                Browser = c.UserAgent.Family,
                BrowserVersion = $"{c.UserAgent.Major}.{c.UserAgent.Minor}",
                OS = c.OS.Family,
                OSVersion = $"{c.OS.Major}.{c.OS.Minor}",
                Device = c.Device.Family,
                DeviceBrand = c.Device.Brand,
                DeviceModel = c.Device.Model
            };
        }
    }
}