using System;

namespace LocalServicePlatform.Domain.ApplicationEnums
{
    public class ApplicationEnums
    {
        public enum ServiceType
        {
            Residential = 0,
            Commercial = 1,
            Both = 2
        }

        public enum WorkType
        {
            Small_Est_1hour = 0,
            Medium_Est_2_to_3hours = 1,
            Large_Est_4hour = 2
        }

        public enum Location
        {
            JaffnaTown = 0,
            PointPedro = 1,
            Mathagal = 2,
            Manipai = 3,
            Sunnakam = 4,
            Mallakam = 5,
            kks = 6
        }

       public enum TasksTypes
        {
            ElectricalWorks= 0,
            Plumbing=1,
            Mounting=2, 
            Assembly= 3,
            Painting= 4
        }
        
    }
}
