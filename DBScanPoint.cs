using System;


namespace Data
{
    public enum DBScanPointLabel
    {
        Noise,
        Unprocessed,
        Processed       
    }
    
    public class DBScanPoint : Point
    {
        public int ClusterId;
        public DBScanPointLabel Label { get; set; }
        public DBScanPoint(int x,
                           int y)
        {
            X = x;
            Y = y;
            Label = DBScanPointLabel.Unprocessed;
            ClusterId = 0;
        }
    }
}
