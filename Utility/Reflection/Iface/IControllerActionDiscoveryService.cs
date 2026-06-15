using System.Xml.Linq;
using Utility.Reflection.Dto;

namespace Utility.Reflection.Iface
{
    public interface IControllerActionDiscoveryService
    {
        List<ControllerActionInfoDto> GetControllerActions(XDocument _xmlComments);
    }
}
