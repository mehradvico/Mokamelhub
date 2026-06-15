namespace Utility.Reflection.Dto
{
    public class ControllerActionInfoDto
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Parent { get; set; }
        public List<ActionInfoDto> Actions { get; set; }
    }
}
