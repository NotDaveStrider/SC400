using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TrueMarbleData
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
  internal class TMDataControllerImpl : ITMDataController
  {
    TMDataControllerImpl()
    {
      Console.WriteLine("a new client has connected");

    }
    int ITMDataController.GetTileWidth()
    {
      int width = 0;
      int height;
      try
      {
        
        int check = TMDLLWrapper.GetTileSize(out width, out height);
        if (check == 0) throw new FaultException("Could not get tile size");
        
      }
      catch (FaultException ex)
      {
        Console.Write(ex.StackTrace);

      }
      return width;

    }

    int ITMDataController.GetTileHeight()
    {
      int width;
      int height = 0;
      try
      {
        
        int check = TMDLLWrapper.GetTileSize(out width, out height);
        if (check == 0) throw new FaultException("Could not get tile size");
        
      }
      catch (FaultException ex)
      {
        Console.Write(ex.StackTrace);

      }
      return height;

    }

    int ITMDataController.GetNumTilesAcross(int zoom)
    {
      int numTilesX = 0;
      int numTilesY;
      try
      {
        
        int check = TMDLLWrapper.GetNumTiles(zoom, out numTilesX, out numTilesY);
        if (check == 0) throw new FaultException("Could not get number of tiles");
        
      }catch (FaultException ex)
      {
        Console.Write(ex.StackTrace);

      }
      return numTilesX;

    }

    int ITMDataController.GetNumTilesDown(int zoom)
    {
      int numTilesX;
      int numTilesY = 0;
      try { 
        
        int check = TMDLLWrapper.GetNumTiles(zoom, out numTilesX, out numTilesY);
        if (check == 0) throw new FaultException("Could not get number of tiles");
        
      }catch (FaultException ex)
      {
        Console.Write(ex.StackTrace);

      }
      return numTilesY;

    }

    byte[] ITMDataController.LoadTile(int zoom, int x, int y)
    {
      int width;
      int height;
      int jpgSize;
      byte[] data = null;
      try
      {
        
        int check = TMDLLWrapper.GetTileSize(out width, out height);
        if (check == 0) throw new FaultException("could not get tile size");
        int bufSize = width * height * 3;
        data = new byte[bufSize];

        //breaks here
        check = TMDLLWrapper.GetTileImageAsRawJPG(zoom, x, y, ref data, bufSize, out jpgSize);

        if (check == 0) throw new FaultException("Could not retrieve image data");
      }catch (FaultException ex)
      {
        Console.Write(ex.StackTrace);

      }

      return data;

    }
  }
}

