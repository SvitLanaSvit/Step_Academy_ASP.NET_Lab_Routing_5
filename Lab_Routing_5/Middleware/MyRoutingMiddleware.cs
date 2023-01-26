namespace Lab_Routing_5.Middleware
{
    public class MyRoutingMiddleware
    {
        public MyRoutingMiddleware(RequestDelegate _) { }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8;";
            await context.Response.WriteAsync("<h2>Hello</h2>");
        }
    }
}
