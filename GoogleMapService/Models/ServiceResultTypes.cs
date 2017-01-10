namespace GoogleMapService.Models
{
    public enum ServiceResultTypes : int
    {
        /// <summary> 地址 ex:台北市重慶南路一段2號 </summary>
        street_address = 0,
        /// <summary> 郵遞區號 ex:100 </summary>
        postal_code = 1,
        /// <summary> 國家 </summary>
        country = 3,
        /// <summary> 路 ex: 重慶南路一段 </summary>
        route = 4,
        /// <summary> 門牌號 ex:2 </summary>
        street_number = 5,
        
        political = 2,
        
        administrative_area_level_1 = 6,
        
        administrative_area_level_3 = 7,
        
        administrative_area_level_4 = 8
    }
}
