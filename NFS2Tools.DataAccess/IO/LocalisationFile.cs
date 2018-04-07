﻿using System.Collections.Generic;
using System.IO;

using NFS2Tools.DataAccess.DataObjects;

namespace NFS2Tools.DataAccess.IO
{
    /// <summary>
    /// Language file.
    /// </summary>
    public class LocalisationFile
    {
        readonly List<string> EntryOrder = new List<string>
        {
            "GAME_SETUP", "LOCATION", "PLAYER1", "PLAYER2", "OPPONENTS", "START_RACE", "CONNECT", "OPTIONS", "EXIT",
            "GAME_TYPE", "RACE_TYPE", "STYLE", "CATCH_UP", "BACKWARDS", "MIRRORED", "NAVIGATION_DONE", "NAVIGATION_CANCEL", "ONE_PLAYER",
            "SPLIT_SCREEN", "MODEM", "SERIAL_LINK", "NETWORK", "SINGLE_RACE", "TOURNAMENT", "KNOCKOUT", "SIMULATION",
            "ARCADE", "WILD", "DIFFICULTY_BEGINNER", "DIFFICULTY_ADVANCED", "SIMULATION_SHORT", "ARCADE_SHORT", "SERIAL_LINK_BIG",
            "COM_PORT", "CONNECT_SMALL", "CANCEL_SMALL", "NR1", "NR2", "NR3", "NR4", "MODEM_BIG", "MODEM_SMALL2", "PHONE_LIST",
            "DELETE_NUMBER", "DIAL", "ANSWER", "CANCEL_BIG2", "NETWORK_BIG", "COLOUR.899", "PROTOCOL", "PLAYERS", "CREATE_GAME",
            "JOIN_GAME", "NO_GAMES_AVAILABLE", "2PeerToPeer", "2-8ClientServer", "CANCEL_BIG.512", "TRACK", "LAPS", "TRACK_INFO",
            "TRACK_RECORDS", "PERSONAL_STATISTICS", "DONE_BIG.821", "CANCEL_BIG.716", "TRACK_NAME_OVAL", "TRACK_NAME_OZ",
            "TRACK_NAME_PAC", "TRACK_NAME_LRT", "TRACK_NAME_NORT", "TRACK_NAME_MED", "TRAK_NAME_MYST", "TRACK_NAME_MONO",
            "LAPS_COUNT_4", "LAPS_COUNT_8", "LAPS_COUNT_2", "PLAYER1_BIG", "PLAYER2_BIG", "CAR", "TRANSMISSION", "COLOUR.218",
            "GRAPH", "SETTINGS", "SHOWCASE", "SLOT_CAR", "DONE_BIG.617", "DONE_BIG.918", "CANCEL_BIG.816", "CANCEL_BIG.168",
            "ACCELERATION", "TOP_SPEED", "BREAKING", "HANDLING", "YES.186", "NO.195", "AUTOMATIC", "MANUAL", "CAR_NAME_MCF1",
            "CAR_NAME_FF50", "CAR_NAME_F355", "CAR_NAME_GT90", "CAR_NAME_IDGO", "CAR_NAME_MACH", "CAR_NAME_JAGR", "CAR_NAME_LGT1",
            "CAR_NAME_ESPR", "CAR_NAME_NAZC", "CAR_NAME_CALA", "CAR_NAME_ISDE", "CAR_NAME_BBFS", "CAR_NAME_DAYR", "CAR_NAME_FZR2",
        };

        /// <summary>
        /// Reads the STF file.
        /// </summary>
        /// <returns>The stf.</returns>
        /// <param name="path">Path.</param>
        public LocalisationEntity Read(string path)
        {
            int offsetUnknown1End = 0x4668;

            LocalisationEntity locale = new LocalisationEntity();

            using (NfsFileReader reader = new NfsFileReader(path, FileMode.Open))
            {
                locale.Unknown1 = reader.ReadBytes(offsetUnknown1End);

                locale.Entries = new Dictionary<string, string>();
                foreach (string key in EntryOrder)
                {
                    string str = reader.ReadString();
                    locale.Entries.Add(key, str);
                }

                locale.Unknown2 = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
            }

            return locale;
        }

        public void Write(string path, LocalisationEntity locale)
        {
            using (NfsFileWriter writer = new NfsFileWriter(path, FileMode.OpenOrCreate))
            {
                writer.WriteBytes(locale.Unknown1);

                foreach (string key in EntryOrder)
                {
                    string str = locale.Entries[key];
                    writer.WriteString(str);
                }

                writer.WriteBytes(locale.Unknown2);
            }
        }
    }
}
