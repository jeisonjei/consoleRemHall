﻿namespace consoleRemHall.SimpleObjects
{
    public class DoorRoom
    {
        private double area;

        public DoorRoom(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public double Area
        {
            get
            {
                area = Width * Height;
                return area;
            }

            set
            {
                area = value;
            }
        }

    }
}