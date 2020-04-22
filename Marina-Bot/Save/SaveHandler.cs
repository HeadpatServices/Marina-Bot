﻿using LibSave;
using LibSave.Types;
using Marina.Utils;
using System.Collections.Generic;
using System.Timers;

namespace Marina.Save
{
    public static class SaveHandler
    {
        public static readonly Dictionary<string, ISaveFile> Saves = new Dictionary<string, ISaveFile>
        {
            {"Logs", new DictionarySaveFile<ulong, ulong>("Logs")},
            {"BlackList", new ListSaveFile<ulong>("BlackList")},
            {"Lockdowns", new DictionarySaveFile<ulong, List<ulong>>("Lockdowns")}
        };

        //30 min. timer
        private static readonly Timer Timer = new Timer(1.8e+6)
        {
            AutoReset = true,
            Enabled = true
        };

        static SaveHandler()
        {
            Timer.Elapsed += delegate { SaveAll(false); };
        }

        //Easy Accessors 
        public static DictionarySaveFile<ulong, ulong> LogSave => (DictionarySaveFile<ulong, ulong>) Saves["Logs"];
        public static ListSaveFile<ulong> BlacklistSave => (ListSaveFile<ulong>) Saves["BlackList"];

        public static DictionarySaveFile<ulong, List<ulong>> LockdownSave =>
            (DictionarySaveFile<ulong, List<ulong>>) Saves["Lockdowns"];

        public static void SaveAll(bool restartTimer = true)
        {
            if (restartTimer)
                Timer.Reset();

            foreach (ISaveFile save in Saves.Values)
                save.Write();
        }
    }
}