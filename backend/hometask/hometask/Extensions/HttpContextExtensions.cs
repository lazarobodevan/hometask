namespace hometask.Extensions {
    public static class HttpContextExtensions {

        public static string GetUserId(this HttpContext context) {
            if (context.Request.Headers.TryGetValue("X-User-Id", out var userId)) {
                return userId.ToString();
            }

            throw new Exception("X-User-Id header não informado");
        }
    }
}
