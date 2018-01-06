using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Corporate.Extension
{
    [MetadataType(typeof(LedgerMetaData))]
    public partial class Ledger
    {
    }
    public class LedgerMetaData
    {
        public double Balance { get; set; }
    }
}