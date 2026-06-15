namespace Application.Common.Dto.LocationPoint
{
    public class PointDto
    {
        public PointDto()
        {
            this.Location = string.Format("{0},{1}", x, y);

        }
        public PointDto(double x, double y)
        {
            this.x = x; this.y = y;
            this.Location = string.Format("{0},{1}", x, y);
        }

        public double x { get; set; }
        public double y { get; set; }
        public string Location { get; set; }
        public double DistanceMeter { get; set; }
    }
}
