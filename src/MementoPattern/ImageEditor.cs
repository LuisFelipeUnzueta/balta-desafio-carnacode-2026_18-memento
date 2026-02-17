using System;

namespace MementoPattern
{
    /// <summary>
    /// The Originator class that has some internal state which changes over time.
    /// It also defines a method for saving the state inside a memento and another
    /// method for restoring the state from it.
    /// </summary>
    public class ImageEditor
    {
        // Core state
        private byte[] _pixels;
        private int _width;
        private int _height;
        private int _brightness;
        private string _filterApplied;
        private double _rotation;

        public ImageEditor(int width, int height)
        {
            _width = width;
            _height = height;
            _pixels = new byte[width * height * 3]; // RGB
            _brightness = 0;
            _filterApplied = "None";
            _rotation = 0;
            
            Console.WriteLine($"[Editor] Image created: {width}x{height}");
        }

        // Methods that change state
        public void ApplyBrightness(int value)
        {
            _brightness += value;
            Console.WriteLine($"[Editor] Brightness adjusted to {_brightness}");
        }

        public void ApplyFilter(string filter)
        {
            _filterApplied = filter;
            Console.WriteLine($"[Editor] Filter applied: {filter}");
        }

        public void Rotate(double degrees)
        {
            _rotation += degrees;
            Console.WriteLine($"[Editor] Rotation: {_rotation}°");
        }

        public void Crop(int newWidth, int newHeight)
        {
            _width = newWidth;
            _height = newHeight;
            Array.Resize(ref _pixels, newWidth * newHeight * 3);
            Console.WriteLine($"[Editor] Image cropped to {newWidth}x{newHeight}");
        }

        public void DisplayInfo()
        {
            Console.WriteLine("\n--- Current State ---");
            Console.WriteLine($"Dimensions: {_width}x{_height}");
            Console.WriteLine($"Brightness: {_brightness}");
            Console.WriteLine($"Filter: {_filterApplied}");
            Console.WriteLine($"Rotation: {_rotation}°");
            Console.WriteLine("--------------------\n");
        }

        /// <summary>
        /// Saves the current state inside a memento.
        /// </summary>
        public IMemento Save()
        {
            return new ImageMemento(
                (byte[])_pixels.Clone(), 
                _width, 
                _height, 
                _brightness, 
                _filterApplied, 
                _rotation);
        }

        /// <summary>
        /// Restores the Originator's state from a memento object.
        /// </summary>
        public void Restore(IMemento memento)
        {
            if (memento is not ImageMemento imageMemento)
            {
                throw new Exception("Unknown memento class.");
            }

            _pixels = (byte[])imageMemento.Pixels.Clone();
            _width = imageMemento.Width;
            _height = imageMemento.Height;
            _brightness = imageMemento.Brightness;
            _filterApplied = imageMemento.FilterApplied;
            _rotation = imageMemento.Rotation;

            Console.WriteLine($"[Editor] State restored: {memento.GetName()}");
        }

        /// <summary>
        /// The Concrete Memento implementation. 
        /// It's a nested class to give the Originator full access to its members,
        /// while keeping it hidden from the rest of the application.
        /// </summary>
        private class ImageMemento : IMemento
        {
            private readonly DateTime _date;
            
            // Externalize state through immutable properties only for the Originator
            internal byte[] Pixels { get; }
            internal int Width { get; }
            internal int Height { get; }
            internal int Brightness { get; }
            internal string FilterApplied { get; }
            internal double Rotation { get; }

            public ImageMemento(byte[] pixels, int width, int height, int brightness, string filter, double rotation)
            {
                Pixels = pixels;
                Width = width;
                Height = height;
                Brightness = brightness;
                FilterApplied = filter;
                Rotation = rotation;
                _date = DateTime.Now;
            }

            public DateTime GetDate() => _date;

            public string GetName() => 
                $"{_date:HH:mm:ss} / {Width}x{Height} / Brightness: {Brightness} / Filter: {FilterApplied}";
        }
    }
}
