using System;
using System.Collections.Generic;
using Speed_Server.Models;

namespace SpeedServerApi.Models
{
    public class SpeedModel
    {
        private double EarthRadius = 6371.009;
        public SnappedPoint[] snappedPoints { get; set; }

        public SpeedModel()
        {
        }

        public SpeedModel(LocationTime[] locations)
        {
            List<SnappedPoint> snappedPointsList = new List<SnappedPoint>();

            foreach (var location in locations)
            {
                snappedPointsList.Add(new SnappedPoint(location));
            }

            this.snappedPoints = snappedPointsList.ToArray();
        }

        public SpeedModel(List<SpeedModel> speedModelList)
        {
            List<SnappedPoint> snappedPointsList = new List<SnappedPoint>();

            foreach (var speedModel in speedModelList)
            {
                snappedPointsList.AddRange(speedModel.snappedPoints);
            }

            this.snappedPoints = snappedPointsList.ToArray();
        }

        public void RemoveExtraPoints(double accuracyLocation, double accuracySpeed)
        {
            // TODO
            // или переделать поле класса на лист, чтобы можно было элемент ремувать или тут создавать новый массив, заполнять его не лишними точками и переприсваивать его на поле экземпляра класса 
            //
            // возможно использовать рекурсию
            var snappedPointsLength = snappedPoints.Length;
            if (snappedPointsLength > 3)
            {
                for (var i = 1; i < snappedPointsLength - 1; i++)
                {
                    bool pointsMakeSameSpeedInSameLine = PointsMakeSameSpeedInSameLine(LocationByIndex(i - 1), LocationByIndex(i), LocationByIndex(i + 1), accuracyLocation, accuracySpeed);
                    if (pointsMakeSameSpeedInSameLine)
                    {
                        // TODO
                    }
                }
            }
        }

        private bool PointsBelongToSameLine(Location Point1, Location Point2, Location Point3, double accuracyLocation)
        {
            var sameLatitude = (Point3.latitude - Point1.latitude) / (Point2.latitude - Point1.latitude);
            var sameLongitude = (Point3.longitude - Point1.longitude) / (Point2.longitude - Point1.longitude);

            if ((Math.Abs(sameLatitude - sameLongitude)) <= accuracyLocation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PointsMakeSameSpeed(LocationTime Point1, LocationTime Point2, LocationTime Point3, double accuracySpeed)
        {
            // kilometers
            var Point1Point2Distance = DistanceBetweenPoints(Point1, Point2) * EarthRadius;
            var Point2Point3Distance = DistanceBetweenPoints(Point2, Point3) * EarthRadius;
            // hours
            var Point1Point2Time = (Point2.time - Point1.time).TotalHours;
            var Point2Point3Time = (Point3.time - Point2.time).TotalHours;
            // km/h
            var Point1Point2Speed = Point1Point2Distance / Point1Point2Time;
            var Point2Point3Speed = Point2Point3Distance / Point2Point3Time;

            if ((Math.Abs(Point1Point2Speed - Point2Point3Speed)) <= accuracySpeed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double DistanceBetweenPoints(Location Point1, Location Point2)
        {
            var Point1Point2Distance = Math.Acos(Math.Sin(Point1.latitude) * Math.Sin(Point2.latitude) + Math.Cos(Point1.latitude) * Math.Cos(Point2.latitude) * Math.Cos(Point1.longitude - Point2.longitude));
            return Point1Point2Distance;
        }

        private bool PointsMakeSameSpeedInSameLine(LocationTime Point1, LocationTime Point2, LocationTime Point3, double accuracyLocation, double accuracySpeed)
        {
            bool pointsBelongToSameLine = PointsBelongToSameLine(Point1, Point2, Point3, accuracyLocation);
            bool pointsMakeSameSpeed = PointsMakeSameSpeed(Point1, Point2, Point3, accuracySpeed);
            if (pointsBelongToSameLine & pointsMakeSameSpeed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private LocationTime LocationByIndex(int i)
        {
            return snappedPoints[i].Location;
        }
    }
}