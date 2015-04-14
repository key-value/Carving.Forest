// Carving.Domain.Repository.EF TableMap.cs
// writer sundy
// Last Update Time 2015-04-14-20:08
// Create Time 2015-04-14-20:08

using System.Data.Entity.ModelConfiguration;
using Carving.Domain.Model;

namespace Carving.Domain.Repository.EF.Map
{
    public class TableMap : EntityTypeConfiguration<Table>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public TableMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}