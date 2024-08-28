using cadastro.Repositories;
using cadastro.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentControl.data;
using PaymentControl.Repositories;
using PaymentControl.Repositories.Interface;
using PaymentControl.Services;

namespace PaymentControl
{
    public class PaymentControl
    {
        public PaymentControl(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddHealthChecks();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            services.AddScoped<IRelatorioCobranca, RelatorioCobrancaRepository>();
            services.AddTransient<CsvService>();

            services.AddDbContext<PaymentControlContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Default"),
                assembly => assembly.MigrationsAssembly(typeof(PaymentControlContext).Assembly.FullName))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });
            services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Control", Version = "v1" }));

            services.AddAuthentication();
            services.AddAuthorization();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Control v1"));
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Control v1"));
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}