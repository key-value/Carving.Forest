using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Carving.Domain.Model;

namespace Carving.Domain.Repository.EF.Map
{
    public class QrCodeMap : EntityTypeConfiguration<QrCode>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public QrCodeMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}
