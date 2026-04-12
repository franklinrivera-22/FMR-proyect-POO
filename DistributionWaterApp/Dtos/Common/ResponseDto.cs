namespace DistributionWaterApp.Dtos.Common
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool status { get; set; }
        public T Data { get; set; }
    }
}