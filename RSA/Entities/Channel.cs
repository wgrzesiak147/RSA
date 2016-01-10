using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA.Entities {
    public class Channel {
        static public int NumOfChannels { get; set; } = 0;

        List<Channel> _childrenChannels = null;
        List<Channel> _parentChannels = null;
        public int[] Slots { get; }
        public int Size { get; set; } = -1;
        public int Index { get; set; } = 0;
        public bool isFree { get; set; } = true;

        public Channel(int[] slotArray) {
            Slots = slotArray;
            Index = NumOfChannels;
            NumOfChannels++;
        }

        public bool IsUsingSlot(int slotIndex) {
            for (int i = 0; i < this.Slots.Length; i++) {
                if (this.Slots[i] == slotIndex) { return true; }
            }
            return false;
        }

        public void TakeChannel() {
            isFree = false;
            for(int i = 0; i < _childrenChannels.Count; i++) { _childrenChannels[i].TakeChannel(); }
        }
        public void FreeChannel() {
            isFree = true;
            for (int i = 0; i < _childrenChannels.Count; i++) { _childrenChannels[i].FreeChannel(); }
        }
        public bool isChannelFree() { return isFree; }
    }
}
