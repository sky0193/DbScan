using System.Collections.Generic;
using Data;

namespace DBScan.TestData
{
    public class TestDataGenerator
    {
        public List<DBScanPoint> points;
        // sample data
        public TestDataGenerator()
        {   
            points = new List<DBScanPoint>();

            points.Add(new DBScanPoint(0, 1));
            points.Add(new DBScanPoint(0, 2));
            points.Add(new DBScanPoint(0, 1.5f));
            points.Add(new DBScanPoint(1, 1.5f));
            points.Add(new DBScanPoint(5, 0));
            points.Add(new DBScanPoint(5, 0.5f));
            points.Add(new DBScanPoint(6, 1));
            points.Add(new DBScanPoint(5, 2));
            points.Add(new DBScanPoint(3, 3));
        }
    }
}
