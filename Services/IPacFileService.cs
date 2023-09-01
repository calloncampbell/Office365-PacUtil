using Office365.PacUtil.Options;
using System;
using System.Linq;

namespace Office365.PacUtil.Services
{
    public interface IPacFileService
    {
        Task<int> PacFileActionAsync(PacFileOptions options, CancellationToken token);
    }
}
