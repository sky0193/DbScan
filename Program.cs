using System;
using System.Collections.Generic;
using Data;
using DBScan;
using DBScan.TestData;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var pointsData = new TestDataGenerator();
            var clusteringAlgorithm = new DbScan(2, 2);
            clusteringAlgorithm.ClusterPoints(pointsData.points);

            List<Cluster> clusters = new List<Cluster>();

            for (int i = 0; i <= clusteringAlgorithm.ClusterCount; i++)
            {
                var cluster = new Cluster();
                cluster.ExtractClustersFromPointList(pointsData.points, i);
                clusters.Add(cluster);
            }
        }
    }
}
