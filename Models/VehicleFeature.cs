namespace vega.Models
{
    public class VehicleFeature
    {
        public int FeatureId { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Features Feature { get; set; }
    }
}