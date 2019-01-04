using System;
using SpeedServer.Models;

namespace Tests.TestData
{
    public static class TracksStorage
    {
        public static SnappedPointRequest[] TestSnappedPointRequestArray = {
            new SnappedPointRequest(-35.2807341, 149.1291511,
                new DateTime(2018, 2, 18, 1, 0, 0, 0)),
            new SnappedPointRequest(-35.2807342, 149.1291512,
                new DateTime(2018, 2, 18, 1, 0, 1, 0)),
            new SnappedPointRequest(-35.2807343, 149.1291513,
                new DateTime(2018, 2, 18, 1, 0, 2, 0)),
            new SnappedPointRequest(-35.2807344, 149.1291514,
                new DateTime(2018, 2, 18, 1, 0, 3, 0)),
            new SnappedPointRequest(-35.280736, 149.1293,
                new DateTime(2018, 2, 18, 1, 0, 4, 0))
        };
    }
}