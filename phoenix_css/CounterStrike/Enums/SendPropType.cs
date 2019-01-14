using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phoenix.CounterStrike.Enums
{
    enum SendPropType
    {
        DPT_Int = 0,
        DPT_Float,
        DPT_Vector,
        DPT_VectorXY, // Only encodes the XY of a vector, ignores Z
        DPT_String,
        DPT_Array,     // An array of the base types (can't be of datatables).
        DPT_DataTable,
        DPT_Int64,
        DPT_NUMSendPropTypes
    }
}