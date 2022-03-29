public static class AppDocumentationExtensionBase
{
    public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "APISaudacao v1");
        });

        return app;
    }
}