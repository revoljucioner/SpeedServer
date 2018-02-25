using System;
using System.Collections.Generic;
using Speed_Server.Models;

namespace SpeedServerApi.Models
{
    public class SpeedModel
    {
        private double EarthRadius = 6371.009;
        public SnappedPointResponse[] snappedPoints { get; set; }

        public SpeedModel()
        {
        }

        public SpeedModel(SpeedModel standartModel, SpeedModel additionModle)
        {
            List<SnappedPointResponse> listPoints=new List<SnappedPointResponse>();

            var standartSnappedPoints = standartModel.snappedPoints;
            var additionSnappedPoints = additionModle.snappedPoints;

            if (standartSnappedPoints.Length != additionSnappedPoints.Length)
            {
                throw new Exception("разная длинна дополняеміх моделей");
            }

            for (var i=0;i<standartSnappedPoints.Length;i++)
            {
                var standartPoint = standartSnappedPoints[i];
                var additionPoint = additionSnappedPoints[i];

                var newPoint = new SnappedPointResponse();
                var newLocation = new LocationWithElevation();
                // долгота
                if (standartPoint.Location.longitude != additionPoint.Location.longitude)
                {
                    if ((standartPoint.Location.longitude != 0) && (additionPoint.Location.longitude == 0))
                    {
                        newLocation.longitude = standartPoint.Location.longitude;
                    }
                    else if ((standartPoint.Location.longitude == 0) && (additionPoint.Location.longitude != 0))
                    {
                        newLocation.longitude = additionPoint.Location.longitude;
                    }
                    else
                    {
                        newLocation.longitude = additionPoint.Location.longitude;
                    }
                }
                else
                {
                    newLocation.longitude = standartPoint.Location.longitude;
                }
                // широта
                if (standartPoint.Location.latitude != additionPoint.Location.latitude)
                {
                    if ((standartPoint.Location.latitude != 0) && (additionPoint.Location.latitude == 0))
                    {
                        newLocation.latitude = standartPoint.Location.latitude;
                    }
                    else if ((standartPoint.Location.latitude == 0) && (additionPoint.Location.latitude != 0))
                    {
                        newLocation.latitude = additionPoint.Location.latitude;
                    }
                    else
                    {
                        newLocation.latitude = additionPoint.Location.latitude;
                    }
                }
                else
                {
                    newLocation.latitude = standartPoint.Location.latitude;
                }
                // высота

                if (standartPoint.Location.elevation != additionPoint.Location.elevation)
                {
                    if ((standartPoint.Location.elevation != 0) && (additionPoint.Location.elevation == 0))
                    {
                        newLocation.elevation = standartPoint.Location.elevation;
                    }
                    else if ((standartPoint.Location.elevation == 0) && (additionPoint.Location.elevation != 0))
                    {
                        newLocation.elevation = additionPoint.Location.elevation; 
                    }
                    else
                    {
                        throw new Exception("Обе модели не могут иметь заполненное значение?");
                    }
                }
                else
                {
                    newLocation.elevation = standartPoint.Location.elevation; 
                }


                // время

                var nullDateTime = DateTime.Parse("1/1/0001 12:00:00 AM");

                if (standartPoint.time!= additionPoint.time)
                {
                    if ((standartPoint.time != nullDateTime) &&(additionPoint.time== nullDateTime))
                    {
                        newPoint.time = standartPoint.time;
                    }
                    else if ((standartPoint.time == nullDateTime) && (additionPoint.time != nullDateTime))
                    {
                        newPoint.time = additionPoint.time;
                    }
                    else
                    {
                        throw new Exception("Обе модели не могут иметь заполненное время?");
                    }
                }
                else
                {
                    newPoint.time = standartPoint.time;
                }

                //placeID
                if (standartPoint.placeId != additionPoint.placeId)
                {
                    if ((standartPoint.placeId != null) && (additionPoint.placeId == null))
                    {
                        newPoint.placeId = standartPoint.placeId;
                    }
                    else if ((standartPoint.placeId == null) && (additionPoint.placeId != null))
                    {
                        newPoint.placeId = additionPoint.placeId;
                    }
                    else
                    {
                        throw new Exception("Обе модели не могут иметь заполненное время?");
                    }
                }
                else
                {
                    newPoint.placeId = standartPoint.placeId;
                }

                // назначим точке эту локацию 

                newPoint.Location = newLocation;
                //добавим точку в лист
                listPoints.Add(newPoint);
            }

            this.snappedPoints = listPoints.ToArray();
        }

        public SpeedModel(SnappedPointRequest[] snappedPointsRequests)
        {
            List<SnappedPointResponse> snappedPointResponseList = new List<SnappedPointResponse>();

            foreach (var snappedPointRequest in snappedPointsRequests)
            {
                snappedPointResponseList.Add(new SnappedPointResponse(snappedPointRequest));
            }

            this.snappedPoints = snappedPointResponseList.ToArray();
        }


        public SpeedModel(SnappedPointRoad[] snappedPointsRoad)
        {
            List<SnappedPointResponse> snappedPointsRoadList = new List<SnappedPointResponse>();

            foreach (var snappedPointRoad in snappedPointsRoad)
            {
                snappedPointsRoadList.Add(new SnappedPointResponse(snappedPointRoad));
            }

            this.snappedPoints = snappedPointsRoadList.ToArray();
        }

        public SpeedModel(SnappedPointElevation[] snappedPointsElevation)
        {
            List<SnappedPointResponse> snappedPointsRoadList = new List<SnappedPointResponse>();

            foreach (var snappedPointElevation in snappedPointsElevation)
            {
                snappedPointsRoadList.Add(new SnappedPointResponse(snappedPointElevation));
            }

            this.snappedPoints = snappedPointsRoadList.ToArray();
        }


        public SpeedModel(List<SpeedModel> speedModelList)
        {
            List<SnappedPointResponse> snappedPointsList = new List<SnappedPointResponse>();

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
            LocationTime locationTime = new LocationTime(snappedPoints[i].Location, snappedPoints[i].time);
            return locationTime;
        }
    }
}