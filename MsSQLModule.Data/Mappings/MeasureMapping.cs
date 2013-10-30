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
    public class MeasureMapping : EntityTypeConfiguration<Measure>
    {
        public MeasureMapping()
        {
            this.Property(x => x.MeasureId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
