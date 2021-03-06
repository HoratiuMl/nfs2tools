﻿using System.Collections.Generic;

namespace NFS2Tools.DataAccess.DataObjects
{
    public class LocalisationEntity
    {
        public byte[] Unknown1 { get; set; }

        public byte[] Unknown2 { get; set; }

        public byte[] Unknown3 { get; set; }

        public IList<LocalisationEntryEntity> Entries { get; set; }
    }
}
