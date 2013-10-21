﻿using System;
using System.CodeDom;
using System.ComponentModel.Composition;
using System.IO;
using BruTile;
using DotSpatial.Serialization;

namespace DotSpatial.Plugins.BruTileLayer.Configuration
{
    [Serializable, InheritedExport]
    public class MbTilesConfiguration : IConfiguration
    {
        [Serialize("mbtilesFile", ConstructorArgumentIndex = 0)]
        private readonly string _mbTilesFile;

        public MbTilesConfiguration(string mbtilesFile)
        {
            _mbTilesFile = mbtilesFile;
            LegendText = Path.GetFileNameWithoutExtension(_mbTilesFile);

            TileSource = new MbTilesTileSource(_mbTilesFile);
            TileFetcher = new TileFetcher(TileSource.Provider, 
                BruTileLayerPlugin.Settings.MemoryCacheMinimum,
                BruTileLayerPlugin.Settings.MemoryCacheMaximum, 
                new TileFetcher.NoopCache());
        }

        public string LegendText { get; private set; }

        public ITileSource TileSource { get; private set; }
        public TileFetcher TileFetcher { get; private set; }
        public IConfiguration Clone()
        {
            return new MbTilesConfiguration(_mbTilesFile);
        }

        public void Initialize()
        {
            // nothing to do here
        }
    }
}