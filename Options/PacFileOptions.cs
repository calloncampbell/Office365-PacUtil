using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365.PacUtil.Options
{
    public enum PacFileActions
    {
        Generate,
        UpdateCheck
    }

    public class PacFileOptions
    {
        public PacFileActions Action { get; set; }

        public FileInfo File { get; set; }
    }
}
