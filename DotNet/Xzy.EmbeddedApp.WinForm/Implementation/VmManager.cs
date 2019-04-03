using System;
using System.Collections.Generic;
using Xzy.EmbeddedApp.WinForm.Abstract;

namespace Xzy.EmbeddedApp.WinForm.Implementation
{
    public class VmManager : IVmManager
    {
        private bool _initialized = false;
        private int _maxVmNumber;
        private int _row;
        private int _column;
        private int[,] _vmIndexArray;
        private int _runningGroupIndex = -1;
        private Dictionary<int, VmModel> _vmModels;

        public int MaxVmNumber
        {
            get
            {
                CheckInitialization();
                return _maxVmNumber;
            }

            private set
            {
                _maxVmNumber = value;
            }
        }

        public int Row
        {
            get
            {
                CheckInitialization();
                return _row;
            }

            private set
            {
                _row = value;
            }
        }

        public int Column
        {
            get
            {
                CheckInitialization();
                return _column;
            }

            private set
            {
                _column = value;
            }
        }

        public int[,] VmIndexArray
        {
            get
            {
                CheckInitialization();
                return _vmIndexArray;
            }

            private set
            {
                _vmIndexArray = value;
            }
        }


        public int RunningGroupIndex
        {
            get
            {
                CheckInitialization();
                return _runningGroupIndex;
            }

            set
            {
                _runningGroupIndex = value;
            }
        }

        public Dictionary<int, VmModel> VmModels
        {
            get
            {
                CheckInitialization();
                return _vmModels;
            }

            private set
            {
                _vmModels = value;
            }
        }

        private VmManager()
        {

        }

        public readonly static VmManager Instance = new VmManager();

        public void Initialize(int totalVmNumber, int groupCapacity)
        {
            if (totalVmNumber <= 0)
            {
                throw new ArgumentException("总数不能为零或负数！", nameof(totalVmNumber));
            }

            if (groupCapacity <= 0)
            {
                throw new ArgumentException("每组数量不能为零或负数！", nameof(groupCapacity));
            }

            _maxVmNumber = totalVmNumber;
            _column = groupCapacity;

            double tempRow = (double)totalVmNumber / (double)groupCapacity;

            _row = (int)Math.Ceiling(tempRow);

            _vmIndexArray = new int[_row, _column];

            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _column; j++)
                {
                    int index = i * _column + j;

                    _vmIndexArray[i, j] = index < _maxVmNumber ? index : -1;
                }
            }

            _initialized = true;
        }

        private void CheckInitialization()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("虚拟机管理器未初始化！");
            }
        }

        public void AddVmModel(int vmIndex, VmModel vmModel)
        {
            CheckInitialization();

            if (vmIndex < 0 || vmIndex >= _maxVmNumber)
            {
                throw new ArgumentException("无效的模拟器索引！", nameof(vmIndex));
            }

            if (_vmModels == null)
            {
                _vmModels = new Dictionary<int, VmModel>(_maxVmNumber);
            }

            if (_vmModels.ContainsKey(vmIndex))
            {
                _vmModels[vmIndex] = vmModel;
            }
            else
            {
                _vmModels.Add(vmIndex, vmModel);
            }
        }
    }
}
