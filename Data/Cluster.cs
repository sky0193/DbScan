using System.Collections.Generic;


namespace Data
{
    public class Cluster
    {
        public List<DBScanPoint> Points { get; set; }

        public Cluster(List<DBScanPoint> points)
        {
            Points = points;
        }


        public void printCluster()
        {

        }
    }
}
