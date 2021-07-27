using System.Collections.Generic;

namespace RpnModels
{
    public class StackModel
    {
        /// <summary>
        /// Id of the stack
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Operands of the stack
        /// </summary>
        public IList<decimal> Operands { get; set; }
    }
}
