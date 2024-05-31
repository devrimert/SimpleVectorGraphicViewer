using Newtonsoft.Json;

namespace SimpleVectorGraphicViewerV2.Model
{
    internal abstract class GraphicModel
    {
        [JsonProperty("type")]
        internal string Type { get; set; }
        [JsonProperty("color")]
        internal string Color { get; set; }
        internal CarthesianRangeBoxModel RangeBox { get; set; }

        internal abstract void Generate();
        internal abstract void GetRectangle();
    }

    internal abstract class GraphicModelShape : GraphicModel
    {
        [JsonProperty("filled")]
        internal string Filled { get; set; }
        internal double Area { get; set; }
        internal double Perimeter { get; set; }

        internal abstract void GetGeo();
    }
}