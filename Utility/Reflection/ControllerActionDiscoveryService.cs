using System.Reflection;
using System.Xml.Linq;
using Utility.Reflection.Dto;
using Utility.Reflection.Iface;

namespace Utility.Reflection
{
    public class ControllerActionDiscoveryService : IControllerActionDiscoveryService
    {
        public ControllerActionDiscoveryService()
        {

        }

        public List<ControllerActionInfoDto> GetControllerActions(XDocument _xmlComments)
        {


            XDocument doc = _xmlComments;

            var members = doc.Descendants("member")
                             .Select(m => new
                             {
                                 Name = m.Attribute("name")?.Value,
                                 Summary = m.Element("summary")?.Value.Trim(),
                                 Parent = m.Element("parent")?.Value.Trim()
                             })
                             .ToList();

            var controllers = members
                .Where(m => m.Name.StartsWith("T:Api.Areas.Admin.Controllers"))
                .GroupBy(m => m.Name.Split('.')[4].Replace("Controller", ""));

            var a = controllers.Select(g => new ControllerActionInfoDto
            {
                Name = g.Key,
                Summary = g.FirstOrDefault()?.Summary,
                Parent = g.FirstOrDefault()?.Parent,
                Actions = members.Where(m => m.Name.StartsWith($"M:Api.Areas.Admin.Controllers.{g.Key}Controller"))
                                .Select(m => new ActionInfoDto
                                {
                                    Name = m.Name.Split('(').First().Split('.').Last(),
                                    Summary = m.Summary,
                                    Parent = m.Parent
                                })
                                .ToList()
            })
                 .ToList();
            return a;
        }
    }
}
