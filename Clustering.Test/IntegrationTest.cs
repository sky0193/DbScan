using System;
using System.Collections.Generic;
using System.IO;
using Clustering.Test;
using Data;
using DBScan;
using NUnit.Framework;

namespace Integration.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        private List<DBScanPoint> _pointsData;
        private Random _random;
        private int _maxSize;

        public void TestDataSetUp()
        {
            _maxSize = 11;
            _random = new Random();
            _pointsData = new List<DBScanPoint>();

            int firstClusterPointsNumber = 30;
            int secondClusterPointsNumber = 22;
            int thirdClusterPointsNumber = 43;
            int fourthClusterPointsNumber = 12;
            int numberOfNoise = 20;

            CreateCluster(firstClusterPointsNumber, 0, 0);
            CreateCluster(secondClusterPointsNumber, 0, 5);
            CreateCluster(thirdClusterPointsNumber, 4, 8);
            CreateCluster(fourthClusterPointsNumber, 10, 0);
            AddNoise(numberOfNoise);
        }

        [SetUp]
        public void SetUp()
        {
            TestDataSetUp();
        }

        [Test]
        public void IntegrationTest_LotOfPoints_GetOutputCSVForVisualisation()
        {

            string filename =  "clusters.csv";

            float maximumDistance = 1.0f;
            int minimumPoints = 3;
            var clusteringAlgorithm = new DbScan(maximumDistance, minimumPoints);
            clusteringAlgorithm.DbScanClustering(_pointsData);

            var clusters = new ClusteredObjects();
            clusters.CreateClustesFromClusteredPoints(_pointsData);
            clusters.WriteClustersToCSV(filename);

            Assert.True(File.Exists(filename));
            
            int numberOfLinesInCSV = System.IO.File.ReadAllLines(filename).Length;
            Assert.AreEqual(_pointsData.Count, numberOfLinesInCSV);
        }

        
        private void CreateCluster(int pointsInClusterPoints, float offsetX, float offsetY)
        {
            for (int i = 0; i < pointsInClusterPoints; i++)
            {
                DBScanPoint point = CalculatePoint(offsetX, offsetY);
                _pointsData.Add(point);
            }
        }
        
        private DBScanPoint CalculatePoint(float offsetX, float offsetY)
        {
            var angle = (float)(_random.NextDouble() * Math.PI * 2);
            var radius = (float)(_random.NextDouble() * 1.2f);

            float x = (float)(offsetX + radius * Math.Cos(angle));
            float y = (float)(offsetY + radius * Math.Sin(angle));

            return new DBScanPoint(x,y);
        }

        
        private void AddNoise(int numberOfNoise)
        {
            for(int i = 0 ; i < numberOfNoise; i++)
            {
                float x = (float)_random.NextDouble() * 10 % _maxSize;
                float y = (float)_random.NextDouble() * 10 % _maxSize;
                DBScanPoint point = new DBScanPoint(x, y);
                _pointsData.Add(point);
            }
        }
    }
}