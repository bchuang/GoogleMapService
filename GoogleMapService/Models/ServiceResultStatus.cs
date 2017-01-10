namespace GoogleMapService.Models
{
    public enum ServiceResultStatus : int
    {
        /// <summary> 聯絡 Google 伺服器時出現問題 </summary>
        ERROR = 0,

        /// <summary> 此要求無效 </summary>
        INVALID_REQUEST = 1,

        /// <summary> 回應包含有效的結果 </summary>
        OK = 2,

        /// <summary> 網頁已超出要求配額的量 </summary>
        OVER_QUERY_LIMIT = 3,

        /// <summary> 網頁不允許使用 PlacesService </summary>
        REQUEST_DENIED = 4,

        /// <summary> 由於發生伺服器錯誤，而無法處理 PlacesService 要求。重新嘗試該要求或許會成功 </summary>
        UNKNOWN_ERROR = 5,

        /// <summary> 此要求找不到結果 </summary>
        ZERO_RESULTS = 6,

        /// <summary> 未回傳 google status </summary>
        NOT_DEFINED = 7
    }
}
