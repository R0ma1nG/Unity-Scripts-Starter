namespace Utils {
    public static class Math {
        public static double ConvertRange(
            double originalStart,
            double originalEnd,
            double newStart,
            double newEnd,
            double value
        ) {
            var scale = (newEnd - newStart) / (originalEnd - originalStart);
            return newStart + (value - originalStart) * scale;
        }
        
        public static float LimitToRange(
            float value, 
            float inclusiveMinimum, 
            float inclusiveMaximum)
        {
            if (value < inclusiveMinimum) { return inclusiveMinimum; }
            if (value > inclusiveMaximum) { return inclusiveMaximum; }
            return value;
        }
    }
}