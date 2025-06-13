namespace Rawy.Controllers
{
    public class RecommendationResult
    {
        public string user_id { get; set; }
        public List<int> Recommendations { get; set; }
    }
}
