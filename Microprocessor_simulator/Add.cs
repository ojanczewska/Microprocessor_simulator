using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microprocessor_simulator
{
    public class Add
    {
        private int addressingType = -1;
        private int instructionType = -1;
        private int sourceType = -1;
        private int destinationType = -1;
        private int pushOrPopType = -1;
        private int value;
        private int registerType;
        private int interruptIndex;
        private string ahValue = "NONE";
        private bool isInterrupt = false;

        public Add(int addressingType, int instructionType, int sourceType, int destinationType, int value)
        {
            this.addressingType = addressingType;
            this.instructionType = instructionType;
            this.sourceType = sourceType;
            this.destinationType = destinationType;
            this.value = value;
        }

        public Add(int pushOrPopType, int registerType)
        {
            this.pushOrPopType = pushOrPopType;
            this.registerType = registerType;
        }

        public Add(bool isInterrupt, int interrupt)
        {
            this.interruptIndex = interrupt;
            this.isInterrupt = isInterrupt;
        }

        public Add(int destinationType, string ahValue)
        {
            this.destinationType = destinationType;
            this.ahValue = ahValue;
        }

        public int GetAddressingType()
        {
            return addressingType;
        }

        public int GetInstructionType()
        {
            return instructionType;
        }

        public int GetSourceType()
        {
            return sourceType;
        }

        public int GetDestinationType()
        {
            return destinationType;
        }

        public int GetValue()
        {
            return value;
        }

        public int GetPushOrPopType()
        {
            return pushOrPopType;
        }

        public int GetRegisterType()
        {
            return registerType;
        }

        public string GetAHValue()
        {
            return ahValue;
        }

        public bool GetIsInterrupt()
        {
            return isInterrupt;
        }

        public int GetInterruptIndex()
        {
            return interruptIndex;
        }
    }
}


