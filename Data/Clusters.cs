using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class ClusteredObjects
    {
        public List<Cluster> Clusters { get; set; }
        
        public Cluster Noise { get; set; }

        public ClusteredObjects()
        {
            Clusters = new List<Cluster>();
        }

        public void CreateClustesFromClusteredPoints(List<DBScanPoint> dbPointsWithClusterInformation)
        {
            SetNoise(dbPointsWithClusterInformation);
            SetClusters(dbPointsWithClusterInformation);
        }

        internal List<DBScanPoint> ExtractClustersFromPointList(List<DBScanPoint> pointsData, int ClusterId)
        {
            return pointsData.Where(p => p.ClusterId == ClusterId).ToList();
        }

        internal void SetClusters(List<DBScanPoint> dbPointsWithClusterInformation)
        {
            if(!dbPointsWithClusterInformation.Any())
            {
                return;
            }

            int maximalClusterID = dbPointsWithClusterInformation.Max(point => point.ClusterId);
            var classifiedPoints = dbPointsWithClusterInformation.Where(p => p.Label == DBScanPointLabel.Classified).ToList();
            
            Console.WriteLine(maximalClusterID);
            for (int i = 0; i <= maximalClusterID; i++)
            {
                var clusteredPoints = ExtractClustersFromPointList(classifiedPoints, i);
                if (clusteredPoints.Count > 0)
                {
                    var cluster = new Cluster(clusteredPoints);
                    Clusters.Add(cluster);
                }
            }
        }

        internal void SetNoise(List<DBScanPoint> dbPointsWithClusterInformation)
        {
            var noisePoints = dbPointsWithClusterInformation.Where(p => p.Label == DBScanPointLabel.Noise).ToList();
            Noise = new Cluster(noisePoints);
        }
    }
}
