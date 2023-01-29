using Lab_Routing_5.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
//-------------------1---------------------------
app.Map("/mysite.com/newOrder", context =>
{
    context.UseMiddleware<MyRoutingMiddleware>();
});
//-------------------2---------------------------
app.Map("/Home/{action:maxlength(6)}", async (IConfiguration appConfig, HttpContext context, string action) =>
{
    string color = appConfig["color"];
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync($"<h2 style='color: {color};'>{action}</h2>");
});
//-------------------3---------------------------
app.Map("/mysite.com/usersettings/{id:int:range(1,999)}", (HttpContext context, int id) =>
{
    context.Response.Redirect($"/mysite.com/user/settings/{id}");
});

app.Map("/{controller}/{action}/{settings}/{id}", ActionHandler);
//-------------------4---------------------------
app.Map("/Admin/{action:regex(^\\w+setup$)}", (string action) => $"Action: {action}");
//-------------------5---------------------------
//app.Map("/mysite.com/Archive/{date:regex((((0|1)[0-9]|2[0-9]|3[0-1])-(0[1-9]|1[0-2])-((19|20)\\d\\d))$)}", 
//    async (IConfiguration appConfig, HttpContext context, string date) =>
//{
//    string color = appConfig["color"];
//    StringBuilder sb = new StringBuilder();
//    context.Response.ContentType = "text/html; chrset=utf-8";
//    sb.AppendLine($"<h2 style='color: {color}'>{date}</h2>");
//    sb.AppendLine($"<ul style='color: {color}'><li>Lenovo CORE i3</li>");
//    sb.AppendLine("<li>Apple CORE i5</li>");
//    sb.AppendLine("<li>HP CORE i7</li></ul>");
//    await context.Response.WriteAsync(sb.ToString());
//});

//Date:(2023-12-01)
app.Map("/mysite.com/Archive/{date:datetime}",
    async (IConfiguration appConfig, HttpContext context, DateTime date) =>
    {
        string color = appConfig["color"];
        StringBuilder sb = new StringBuilder();
        context.Response.ContentType = "text/html; chrset=utf-8";
        sb.AppendLine($"<h2 style='color: {color}'>{date}</h2>");
        sb.AppendLine($"<ul style='color: {color}'><li>Lenovo CORE i3</li>");
        sb.AppendLine("<li>Apple CORE i5</li>");
        sb.AppendLine("<li>HP CORE i7</li></ul>");
        await context.Response.WriteAsync(sb.ToString());
    });


app.Run();

static string ActionHandler(string? controller, string? action, string? settings, int? id)
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine($"Controller: {controller}");
    sb.AppendLine($"Action: {action}");
    sb.AppendLine($"Settings: {settings}");
    sb.AppendLine($"Id: {id}");
    return sb.ToString();
}