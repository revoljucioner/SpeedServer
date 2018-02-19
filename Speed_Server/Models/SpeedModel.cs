using System;
using System.Collections.Generic;
using Speed_Server.Models;

namespace SpeedServerApi.Models
{
    public class SpeedModel
    {
        public SnappedPoint[] snappedPoints { get; set; }

        public SpeedModel()
        {
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

        public void RemoveExtraPoints(double accuracyLocation, double accuracyTime)
        {
            // TODO
            // или переделать поле класса на лист, чтобі можно біло єлемент ремувать или тут создавать новый массив, заполнять его не лишними точками и переприсваивать его на поле экземпляра класса 
            var snappedPointsLength = snappedPoints.Length;
            if (snappedPointsLength > 3)
            {
                for (var i = 1; i < snappedPointsLength - 1; i++)
                {
                    // TODO
                    // добавить метод получения сущности локации по индексу, а то это капец каждый раз писать столько через точку 
                    bool pointsBelongToSameLine = PointsBelongToSameLine(LocationByIndex(i - 1), LocationByIndex(i), LocationByIndex(i + 1), accuracyLocation);
                    bool pointsMakeSameTimeSpan = PointsMakeSameTimeSpan(TimeByIndex(i - 1), TimeByIndex(i), TimeByIndex(i + 1), accuracyTime);
                    if (pointsBelongToSameLine & pointsMakeSameTimeSpan)
                    {
                        // TODO
                    }
                }
            }
        }

        // TODO
        // проверить учет аккуратности
        private bool PointsBelongToSameLine(Location Point1, Location Point2, Location Point3, double accuracyLocation)
        {
            var sameLatitude = (Point3.latitude - Point1.latitude)/(Point2.latitude - Point1.latitude);
            var sameLongitude = (Point3.longitude - Point1.longitude)/(Point2.longitude - Point1.longitude);

            if ((Math.Abs(sameLatitude-sameLongitude))<= accuracyLocation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // TODO
        // проверить учет аккуратности
        // тут должна быть поправка не на время, а на скорость
        private bool PointsMakeSameTimeSpan(DateTime time1, DateTime time2, DateTime time3, double accuracyTime)
        {
            var diffBetween1and2PointTime = time2 - time1;
            var diffBetween2and3PointTime = time3 - time2;

            if ((diffBetween2and3PointTime - diffBetween1and2PointTime).TotalMilliseconds <= accuracyTime)

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Location LocationByIndex(int i)
        {
            return snappedPoints[i].Location;
        }

        private DateTime TimeByIndex(int i)
        {
            return snappedPoints[i].Location.time;
        }
    }
}