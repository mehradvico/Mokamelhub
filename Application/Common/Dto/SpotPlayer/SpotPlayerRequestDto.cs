using System.Collections.Generic;

namespace Application.Common.Dto.SpotPlayer
{
    public class SpotPlayerRequestDto
    {
        public SpotPlayerRequestDto(string course, string name, string payload, string texts, bool test = false)
        {
            this.Test = test;
            this.Name = name;
            this.Payload = payload;
            this.Course = course.Split(',');
            string[] parts = texts.Split(",");
            foreach (string part in parts)
            {
                this.Watermark.texts.Add(new SpotPlayer.texts() { text = part });
            }
        }
        public bool Test { get; set; } = false;
        public string[] Course { get; set; }
        public int Offline { get; set; } = 30;
        public string Name { get; set; }
        public string Payload { get; set; }
        public Watermark Watermark { get; set; } = new Watermark();
        public device device { get; set; } = new device();
    }
    public class Watermark
    {
        public Watermark()
        {
            texts = new List<texts>();
        }
        public int position { get; set; } = 511;
        public int reposition { get; set; } = 15;
        public int margin { get; set; } = 40;
        public List<texts> texts { get; set; }

    }
    public class texts
    {
        public string text { get; set; }
        public int repeat { get; set; } = 10;
        public int font { get; set; } = 1;
        public int weight { get; set; } = 1;
        public long color { get; set; } = 2164260863;
        public int size { get; set; } = 50;
        public stroke stroke { get; set; } = new stroke();
    }
    public class stroke
    {
        public long color { get; set; } = 2164260863;
        public int size { get; set; } = 1;
    }
    public class device
    {
        public int p0 { get; set; } = 2;
        public int p1 { get; set; } = 1;
        public int p2 { get; set; } = 1;
        public int p3 { get; set; } = 1;
        public int p4 { get; set; } = 1;
        public int p5 { get; set; } = 1;
        public int p6 { get; set; }
    }

}
