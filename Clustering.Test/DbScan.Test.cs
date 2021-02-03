using System.Collections.Generic;
using Data;
using DBScan;
using NUnit.Framework;

namespace Clustering.Test
{
    [TestFixture]
    public class DbScan_Test
    {
        private List<DBScanPoint> _pointsData;

        public void TestDataSetUp()
        {   
            _pointsData = new List<DBScanPoint>();

            _pointsData.Add(new DBScanPoint(0, 1));
            _pointsData.Add(new DBScanPoint(0, 2));
            _pointsData.Add(new DBScanPoint(0, 1.5f));
            _pointsData.Add(new DBScanPoint(1, 1.5f));
            _pointsData.Add(new DBScanPoint(5, 0));
            _pointsData.Add(new DBScanPoint(5, 0.5f));
            _pointsData.Add(new DBScanPoint(6, 1));
            _pointsData.Add(new DBScanPoint(5, 2));
            _pointsData.Add(new DBScanPoint(3, 3));
        }

        [SetUp]
        public void SetUp()
        {
            TestDataSetUp();
        }

        [Test]
        public void clusteringAlgorithmClusterPoints_SomePoints_ClustersCreated()
        {
            float maximumDistance = 2;
            int minimumPoints = 2;
            var clusteringAlgorithm = new DbScan(maximumDistance, minimumPoints);
            var clusters = new ClusteredObjects();

            clusteringAlgorithm.DbScanClustering(_pointsData);     

            clusters.CreateClustesFromClusteredPoints(_pointsData);

            int numberOfExpectedClusters = 2;
            Assert.AreEqual(numberOfExpectedClusters, clusters.Clusters.Count);
        }

        [Test]
        public void clusteringAlgorithmClusterPoints_MaximumDistanceZero_NoClusterCreated()
        {
            float maximumDistance = 0;
            int minimumPoints = 2;
            var clusteringAlgorithm = new DbScan(maximumDistance, minimumPoints);
            var clusters = new ClusteredObjects();

            clusteringAlgorithm.DbScanClustering(_pointsData);     

            clusters.CreateClustesFromClusteredPoints(_pointsData);

            int numberOfExpectedClusters = 0;
            Assert.AreEqual(numberOfExpectedClusters, clusters.Clusters.Count);
        }

        [Test]
        public void clusteringAlgorithmClusterPoints_MaximumDistanceLarge_AllObjectsInOneCluster()
        {
            float maximumDistance = 100;
            int minimumPoints = 2;
            var clusteringAlgorithm = new DbScan(maximumDistance, minimumPoints);
            var clusters = new ClusteredObjects();

            clusteringAlgorithm.DbScanClustering(_pointsData);     

            clusters.CreateClustesFromClusteredPoints(_pointsData);

            int numberOfExpectedClusters = 1;
            Assert.AreEqual(numberOfExpectedClusters, clusters.Clusters.Count);
        }
    }
}