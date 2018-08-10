using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCommon
{
    public class BitArray
    {
        public const uint NONE = 0;
        public const uint FULL = 0xffffffff;
        public static bool HasFlag(uint value, uint flags)
        {
            return (value & flags) != 0;
        }
        public static bool HasFlagByPos(uint value, uint pos)
        {
            if(pos >= 32)
            {
                return false;
            }
            return (value & (uint)(1 << (byte)pos)) != 0;
        }
        public static uint AddFlag(uint value, uint flags)
        {
            return value | flags;
        }
        public static uint AddFlagByPos(uint value, uint pos)
        {
            if (pos >= 32)
            {
                return value;
            }
            return value | (uint)(1 << (byte)pos);
        }
        public static uint RemoveFlag(uint value, uint flags)
        {
            return value & (~flags);
        }
        public static uint RemoveFlagByPos(uint value, uint pos)
        {
            if (pos >= 32)
            {
                return value;
            }
            return value & (~((uint)(1 << (byte)pos)));
        }
        protected uint m_Value;
        public BitArray()
        {
            m_Value = 0;
        }
        public bool HasFlag(uint flags)
        {
            return HasFlag(m_Value, flags);
        }
        public bool HasFlagByPos(uint pos)
        {
            return HasFlagByPos(m_Value, pos);
        }
        public void AddFlag(uint flags)
        {
            m_Value = AddFlag(m_Value, flags);
        }
        public void AddFlagByPos(uint pos)
        {
            m_Value = AddFlagByPos(m_Value, pos);
        }
        public void RemoveFlag(uint flags)
        {
            m_Value = m_Value & (~flags);
        }
        public void RemoveFlagByPos(uint pos)
        {
            m_Value = RemoveFlagByPos(m_Value, pos);
        }
        public uint GetValue()
        {
            return m_Value;
        }
    }
    public class BitArrayBoolean : BitArray
    {
        public enum EBitArrayBooleanMode
        {
            AND_TURE, OR_TRUE
        }
        protected EBitArrayBooleanMode m_Mode = EBitArrayBooleanMode.OR_TRUE;
        public BitArrayBoolean(bool v, EBitArrayBooleanMode mode = EBitArrayBooleanMode.OR_TRUE)
        {
            m_Mode = mode;
            SetBooleanValue(v);
        }
        public void SetBooleanValue(bool v)
        {
            if (m_Mode == EBitArrayBooleanMode.OR_TRUE)
            {
                m_Value = v ? 1 : (uint)0;
            }
            else
            {
                m_Value = v ? FULL : RemoveFlag(FULL, 1);
            }
        }
        public void SetBooleanValue(uint flag, bool v)
        {
            if (v == false)
            {
                RemoveFlag(flag);
            }
            else
            {
                AddFlag(flag);
            }
        }
        public bool GetBooleanValue()
        {
            if(m_Mode == EBitArrayBooleanMode.OR_TRUE)
            {
                return m_Value != 0;
            }
            else
            {
                return m_Value == FULL;
            }
        }
        public static implicit operator bool(BitArrayBoolean ba)
        {
            return ba.GetBooleanValue();
        }
    }
}
