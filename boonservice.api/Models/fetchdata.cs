using System;

namespace boonservice.api.Models
{
    /// <summary>
    /// Fetch data
    /// </summary>
    public class fetchdata
    {
        /// <summary>
        /// fetch after row (default 0)
        /// </summary>
        public Int16 after { get; set; }
        
        /// <summary>
        /// Size (default 100)
        /// </summary>
        public int size { get; set; }        
    }
}