namespace KQF.Floor.Web.Models
{

    public class MaterialPostModel
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; }

        public object Meta { get; set; } = null;
    }

    public class SplitPostModel : MaterialPostModel
    {
        public decimal Weight { get; set; }

        public int NoOfRods { get; set; }
    }

    public class MoveItemPostModel : MaterialPostModel
    {
        public string ItemNo { get; set; }
        
        public string LotNo { get; set; }
        
        public decimal Qty { get; set; }
    }

    public class MoveBetweenPostModel : MoveItemPostModel
    {
        public bool MovePartial { get; set; }
    }
}
