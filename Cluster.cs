using System.Collections.Generic;
using System.Linq;
using Data;

namespace DBScan.TestData
{
    public class Cluster
    {
        public List<DBScanPoint> Points { get; set; }

        public void ExtractClustersFromPointList(List<DBScanPoint> pointsData, int ClusterId)
        {
            Cluster cluster = new Cluster();
            Points = pointsData.Where(p => p.ClusterId == ClusterId).ToList();
        }

        public void printCluster()
        {

        }
    }
}