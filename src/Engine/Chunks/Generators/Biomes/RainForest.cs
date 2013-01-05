﻿/*
 * Copyright (C) 2011 - 2013 Voxeliq Engine - http://www.voxeliq.org - https://github.com/raistlinthewiz/voxeliq
 *
 * This program is free software; you can redistribute it and/or modify 
 * it under the terms of the Microsoft Public License (Ms-PL).
 */

using System;
using VoxeliqEngine.Blocks;
using VoxeliqEngine.Chunks.Generators.Terrain;

namespace VoxeliqEngine.Chunks.Generators.Biomes
{
    /// <summary>
    /// Rain forest generator.
    /// </summary>
    public class RainForest : BiomeGenerator
    {
        private readonly Random _treePlanter = new Random(TerrainGenerator.DefaultSeed);

        public override void ApplyBiome(Chunk chunk, int groundLevel, int groundOffset, int worldPositionX, int worldPositionZ)
        {
            BlockStorage.Blocks[groundOffset + 1].Type = BlockType.Grass;

            if (groundLevel + 1 > chunk.HighestSolidBlockOffset)
                chunk.HighestSolidBlockOffset = (byte)(groundLevel + 1);

            //bool plantTree = _treePlanter.Next(1000) == 1;

            //if (plantTree)
                //this.PlantTree(chunk, groundLevel + 1, groundOffset + 1, worldPositionX, worldPositionZ);
        }

        private void PlantTree(Chunk chunk, int grassLevel, int grassOffset, int worldPositionX, int worldPositionZ)
        {
            // based on: http://techcraft.codeplex.com/SourceControl/changeset/view/5c1888588e5d#TechCraft%2fNewTake%2fNewTake%2fmodel%2fterrain%2fSimpleTerrain.cs

            var trunkHeight = (byte) (10 + (byte) _treePlanter.Next(15));

            var offset2 = BlockStorage.BlockIndexByWorldPosition(worldPositionX, worldPositionZ);

            // trunk
            for (byte y = 1; y <= trunkHeight; y++)
            {
                BlockStorage.Blocks[grassOffset + y].Type = BlockType.Tree;
            }

            var foliage = 3;
            Console.WriteLine("pos:{0}-{1}|foliage:{2}-{3}", worldPositionX, worldPositionZ, worldPositionX - foliage, worldPositionX + foliage);
            for (int x = -foliage; x <=  foliage; x++)
            {                
                for (int z = -foliage; z <= foliage; z++)
                {
                    var offset = BlockStorage.BlockIndexByWorldPosition(worldPositionX + x, worldPositionZ + z);
                    for (int y = grassLevel; y < grassLevel + trunkHeight; y++ )
                    {
                        BlockStorage.Blocks[offset + y].Type = BlockType.Leaves;
                    }
                }
            }


            if (grassLevel + trunkHeight > chunk.HighestSolidBlockOffset)
                chunk.HighestSolidBlockOffset = (byte) (grassLevel + trunkHeight + 1);
        }
    }
}