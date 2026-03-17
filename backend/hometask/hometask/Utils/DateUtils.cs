namespace hometask.Utils {
    public static class DateUtils {
        public static DateOnly GetStartOfWeek(DateOnly date) {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff);
        }
    }
}
