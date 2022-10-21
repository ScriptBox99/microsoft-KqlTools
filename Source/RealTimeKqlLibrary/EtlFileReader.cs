﻿using System;
using System.IO;

namespace RealTimeKqlLibrary
{
    public class EtlFileReader : EventComponent
    {
        private readonly string _fileName;

        public EtlFileReader(string fileName, IOutput output, params string[] queries) : base(output, queries)
        {
            _fileName = fileName;
        }
        override public bool Start()
        {
            // Check if specified file exists
            if (!File.Exists(_fileName))
            {
                _output.OutputError(new Exception($"ERROR! {_fileName} does not seem to exist."));
                return false;
            }

            var eventStream = Tx.Windows.EtwTdhObservable.FromFiles(_fileName);
            var eventStreamName = _fileName.Split('.');
            return Start(eventStream, eventStreamName[0], false);
        }
    }
}
