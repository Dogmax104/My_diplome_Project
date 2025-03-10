namespace Arctech_Manufaction_Menedgment.Models
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsynk(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("Role"))    // Если существует роль
            {
                context.Response.Redirect("_Layout");   // Загрузить страницу начальную
                return;
            }
            await _next(context);
        }
    }
}
