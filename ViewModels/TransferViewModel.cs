using TESTE.API.Enums;

namespace TESTE.API.ViewModels
{
    public class TransferViewModel
    {
        public int IdOriginAccount { get; set; }
        public int IdDestinyAccount { get; set; }
        public double Value { get; set; }     
    }
}
