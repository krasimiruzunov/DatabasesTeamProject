using MsSQLModule.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSQLModule.Data.Mappings
{
    public class VendorMapping : EntityTypeConfiguration<Vendor>
    {
        public VendorMapping()
        {
            this.Property(x => x.VendorId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
