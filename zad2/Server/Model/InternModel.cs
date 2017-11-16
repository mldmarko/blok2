using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.IO;

namespace Server.Model
{
    public class InternModel
    {
        private Dictionary<uint, Block> blocks;
        private uint maxBlocks, maxVectors, maxAlarms;
        private string filePath;
        private object locker;

        public InternModel(uint maxBlocks, uint maxVectors, uint maxAlarms, string filePath)
        {
            this.maxBlocks = maxBlocks;
            this.maxVectors = maxVectors;
            this.maxAlarms = maxAlarms;
            this.filePath = filePath;

            blocks = new Dictionary<uint, Block>();
            locker = new object();
        }

        public bool SetAlarm(uint blockIndex, uint vectorIndex, uint alarmKey, Alarm alarm)
        {
            if (blockIndex > maxBlocks || vectorIndex > maxVectors || alarmKey > maxAlarms)
            {
                return false;
            }

            Block block;

            lock (locker)
            {
                if (!blocks.TryGetValue(blockIndex, out block))
                {
                    block = new Block();
                    blocks[blockIndex] = block;
                }
            }

            block.SetAlarm(vectorIndex, alarmKey, alarm);
            return true;
        }

        public bool GetAlarm(uint blockIndex, uint vectorIndex, uint alarmKey, out Alarm alarm)
        {
            if(blockIndex > maxBlocks || vectorIndex > maxVectors || alarmKey > maxAlarms)
            {
                alarm = null;
                return false;
            }

            Block block;
            lock (locker)
            {
                if (!blocks.TryGetValue(blockIndex, out block))
                {
                    alarm = null;
                    return false;
                }
            }

            return block.GetAlarm(vectorIndex, alarmKey, out alarm);
            
        }

        public void SaveAllToFile()
        {
            List<uint> keys;
            lock (locker)
            {
               keys = blocks.Keys.ToList();
            }

            using (StreamWriter fileWritter = new StreamWriter(filePath)) 
            {
                foreach (uint blockKey in keys)
                {
                    blocks[blockKey].SaveAllToFile(fileWritter);
                }
            }
           
        }

    }
}
