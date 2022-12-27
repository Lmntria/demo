using QuarterApp.DAL;

namespace QuarterApp.Services
{
    public class LayoutService
    {
        private readonly QuarterDbContext _context;
        private readonly IHttpContextAccessor _httpAccessor;

        public LayoutService(QuarterDbContext context,IHttpContextAccessor httpAccessor)
        {
            _context= context;
            _httpAccessor= httpAccessor;
        }

        public Dictionary<string, string> GetSettings() 
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }
    }
}
