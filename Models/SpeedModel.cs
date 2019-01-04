using System;
using System.Collections.Generic;

namespace Models
{
    public class SpeedModel
    {
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
                throw new Exception("разная длинна дополняемых моделей");
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
            var snappedPointResponseList = new List<SnappedPointResponse>();

            foreach (var snappedPointRequest in snappedPointsRequests)
            {
                snappedPointResponseList.Add(new SnappedPointResponse(snappedPointRequest));
            }

            this.snappedPoints = snappedPointResponseList.ToArray();
        }

        public SpeedModel(IEnumerable<SpeedModel> speedModelList)
        {
            var snappedPointsList = new List<SnappedPointResponse>();

            foreach (var speedModel in speedModelList)
            {
                snappedPointsList.AddRange(speedModel.snappedPoints);
            }

            this.snappedPoints = snappedPointsList.ToArray();
        }

        [Obsolete]
        private double DistanceBetweenPoints(Location Point1, Location Point2)
        {
            var Point1Point2Distance = Math.Acos(Math.Sin(Point1.latitude) * Math.Sin(Point2.latitude) + Math.Cos(Point1.latitude) * Math.Cos(Point2.latitude) * Math.Cos(Point1.longitude - Point2.longitude));
            return Point1Point2Distance;
        }
    }
}