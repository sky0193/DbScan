using System.Collections.Generic;
using Clustering.Test;
using Data;
using DBScan;
using NUnit.Framework;

namespace Clusters.Test
{
    [TestFixture]
    public class ClustersTest
    {
        private List<DBScanPoint> _pointsData;

        public void TestDataSetUp()
        {   
            _pointsData = new List<DBScanPoint>();

            _pointsData.Add(new DBScanPoint(0, 1, DBScanPointLabel.Noise, 0));
            _pointsData.Add(new DBScanPoint(0, 2, DBScanPointLabel.Noise, 0));
            _pointsData.Add(new DBScanPoint(0, 1.5f, DBScanPointLabel.Classified, 1));
            _pointsData.Add(new DBScanPoint(1, 1.5f, DBScanPointLabel.Classified, 1));
            _pointsData.Add(new DBScanPoint(0, 4, DBScanPointLabel.Noise, 0));
            _pointsData.Add(new DBScanPoint(0, 5, DBScanPointLabel.Noise, 0));
            _pointsData.Add(new DBScanPoint(3, 1.5f, DBScanPointLabel.Classified, 3));
        }

        [SetUp]
        public void SetUp()
        {
            TestDataSetUp();
        }

        [Test]
        public void ExtractClustersFromPointList_ElementsWithDifferentIDs_ElementsWithIDZero()
        {
            var clusters = new ClusteredObjects();
            int clusterID = 0;
            var pointsWithIDZero = clusters.ExtractClustersFromPointList(_pointsData, clusterID);
            
            int numberOfPointsWithIDZero= 4;
            Assert.AreEqual(numberOfPointsWithIDZero, pointsWithIDZero.Count);
        }

        [Test]
        public void SetClusters_TwoClusters_TwoClustersCreated()
        {
            var clusters = new ClusteredObjects();
            clusters.SetClusters(_pointsData);

            int numberOfExpectedClusters = 2;
            Assert.AreEqual(numberOfExpectedClusters, clusters.Clusters.Count);
        }

        [Test]
        public void SetNoise_PointsWithNoise_NoiseSet()
        {
            var clusters = new ClusteredObjects();
            clusters.SetNoise(_pointsData);

            int numberOfExpectedNoisePoints = 4;
            Assert.AreEqual(numberOfExpectedNoisePoints, clusters.Noise.Points.Count);
        }

        [Test]
        public void CreateClustesFromClusteredPoints_emptyPointList_noClustters()
        {
            var emptyPointList = new List<DBScanPoint>();
            var clusters = new ClusteredObjects();
            clusters.CreateClustesFromClusteredPoints(emptyPointList);

            int numberOfExpectedClusters = 0;
            Assert.AreEqual(numberOfExpectedClusters, clusters.Clusters.Count);
            Assert.AreEqual(numberOfExpectedClusters, clusters.Noise.Points.Count);
        }

        [Test]
        public void CreateClustesFromClusteredPoints_PointList_TwoClusttersAndNoiseSet()
        {
            var clusters = new ClusteredObjects();
            clusters.CreateClustesFromClusteredPoints(_pointsData);

            int numberOfExpectedClusters = 2;
            Assert.AreEqual(numberOfExpectedClusters, clusters.Clusters.Count);

            int numberOfExpectedNoisePoints = 4;
            Assert.AreEqual(numberOfExpectedNoisePoints, clusters.Noise.Points.Count);
        }
    }
}