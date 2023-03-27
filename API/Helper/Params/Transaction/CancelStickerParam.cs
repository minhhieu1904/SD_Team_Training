namespace API.Helpers.Params.Transaction
{
    public class CancelStickerParam
    {
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short? Serial { get; set; }
    }
}