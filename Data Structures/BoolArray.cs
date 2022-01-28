using System;
using System.Collections.Generic;

namespace CMDR
{
    struct BoolArray
    {
        private byte[] _data;
        private int _size;
        public bool this[int index]
        {
            get
            {
                int i = index % 8;
                byte b = _data[index / 8];
                return ((b << i) == 0x01);
            }
        }
        public BoolArray(int size)
        {
            _size = size;
            _data = new byte[size/8];
        }
    }
}
