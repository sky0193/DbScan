using System;


namespace Data
{
    public enum DBScanPointLabel
    {
        Noise,
        Unclassified,
        Classified       
    }

    public class DBScanPoint : Point
    {
        public int ClusterId;
        public DBScanPointLabel Label { get; set; }
        public DBScanPoint(float x,
                           float y)
        {
            X = x;
            Y = y;
            Label = DBScanPointLabel.Unclassified;
            ClusterId = 0;
        }
    }
}
