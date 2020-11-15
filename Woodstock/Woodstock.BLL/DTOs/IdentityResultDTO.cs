using System.Collections.Generic;

namespace Woodstock.BLL.DTOs
{
    public class IdentityResultDTO
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityErrorDTO> Errors { get; set; }
    }
}
