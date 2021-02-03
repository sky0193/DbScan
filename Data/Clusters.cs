using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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

        public void WriteClustersToCSV(string filename)
        {
            var csv = new StringBuilder();

            WriteNoise(csv);
            WriteClusters(csv);

            File.WriteAllText(filename, csv.ToString());
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
            for (int clusterID = 0; clusterID <= maximalClusterID; clusterID++)
            {
                var clusteredPoints = ExtractClustersFromPointList(classifiedPoints, clusterID);
                if (clusteredPoints.Count > 0)
                {
                    var cluster = new Cluster(clusteredPoints);
                    cluster.ClusterID = clusterID;
                    Clusters.Add(cluster);
                }
            }
        }

        internal void SetNoise(List<DBScanPoint> dbPointsWithClusterInformation)
        {
            var noisePoints = dbPointsWithClusterInformation.Where(p => p.Label == DBScanPointLabel.Noise).ToList();
            Noise = new Cluster(noisePoints);
            Noise.ClusterID = -1;
        }

        private void WriteClusters(StringBuilder csv)
        {
            foreach (Cluster cluster in Clusters)
            {
                WriteOneCluster(csv, cluster);
            }
        }

        private void WriteNoise(StringBuilder csv)
        {
            WriteOneCluster(csv, Noise);
        }

        private static void WriteOneCluster(StringBuilder csv, Cluster cluster)
        {
            for (int i = 0; i < cluster.Points.Count; i++)
            {
                var X = cluster.Points[i].X;
                var Y = cluster.Points[i].Y;
                var ID = cluster.ClusterID;
                var newLine = string.Format(CultureInfo.InvariantCulture, "{0};{1};{2}", X, Y, cluster.ClusterID);
                csv.AppendLine(newLine);
            }
        }
    }
}
