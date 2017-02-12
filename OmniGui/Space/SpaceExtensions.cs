namespace OmniGui.Space
{
    public static class SpaceExtensions
    {
        public static Size UniformResize(this Size size, Size target)
        {
            var originalFactor = size.GetProportion();

            
            if (size.Width * originalFactor > target.Width)
            {
                return new Size(target.Width, size.Height * originalFactor);
            }
            else
            {
                return new Size(size.Width * originalFactor, target.Height);
            }
        }

        public static double GetProportion(this Size size)
        {
            return size.Width / size.Height;
        }
    }
}