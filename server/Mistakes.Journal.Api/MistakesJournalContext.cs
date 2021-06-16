using Microsoft.EntityFrameworkCore;

namespace Mistakes.Journal.Api
{
    public class MistakesJournalContext : DbContext
    {
        public MistakesJournalContext(DbContextOptions<MistakesJournalContext> options)
            : base(options)
        {
        }
    }
}