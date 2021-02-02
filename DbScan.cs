using System.Collections.Generic;
using Data;

namespace DBScan.TestData
{
    public class DbScan
    {
        public int ClusterId{get;set;}
        public float MaximumDistance{get;set;}
        public int MinimumPoints{get;set;}


        public DbScan(float maximumDistance, int minimumPoints)
        {
            MaximumDistance = maximumDistance;
            MinimumPoints = minimumPoints;
            ClusterId = 0;
        }

        public void Cluster(List<DBScanPoint> setOfPoints){
            for(var i = 0; i < setOfPoints.Count; i++){
                if(setOfPoints[i].Label == DBScanPointLabel.Unclassified)
                {
                    List<DBScanPoint> neighborPoints  = GetRegion(setOfPoints, setOfPoints[i]);
                    if(neighborPoints.Count < MinimumPoints)
                    {
                        setOfPoints[i].Label = DBScanPointLabel.Noise;
                    }
                    else
                    {
                        ClusterId ++;
                        expandCluster(setOfPoints[i], neighborPoints, setOfPoints);
                    }
                }
            }
        }

        private void expandCluster(DBScanPoint point, List<DBScanPoint> neighborPoints, List<DBScanPoint> pointsToCluster)
        {
            point.ClusterId = ClusterId;
            for(int i = 0; i < neighborPoints.Count; i++)
            {
                if(neighborPoints[i].Label == DBScanPointLabel.Unclassified)
                {
                    neighborPoints[i].Label = DBScanPointLabel.Classified;
                    neighborPoints[i].ClusterId = ClusterId;
                    List<DBScanPoint> neighborPointsOfNeighbor = GetRegion(pointsToCluster, neighborPoints[i]);
                    if(neighborPointsOfNeighbor.Count >= MinimumPoints)
                    {
                        neighborPoints.AddRange(neighborPointsOfNeighbor);
                    }
                }
                
            }
        }

        private List<DBScanPoint> GetRegion(List<DBScanPoint> pointsToCluster, DBScanPoint GetRegionFromThisPoint)
        {
            List<DBScanPoint> neighborPoints = new List<DBScanPoint>();
            double maximumDistanceSquared = MaximumDistance * MaximumDistance;
            for(int i = 0; i < pointsToCluster.Count; i++)
            {
                double distanceSquared = pointsToCluster[i].DistanceToPointSquared(GetRegionFromThisPoint);
                if(distanceSquared <= maximumDistanceSquared)
                {
                    neighborPoints.Add(pointsToCluster[i]);
                }
            }
            return neighborPoints;
        }
    }
}
